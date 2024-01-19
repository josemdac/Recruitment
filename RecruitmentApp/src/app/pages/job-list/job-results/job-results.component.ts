import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { PageChangeEvent } from '@progress/kendo-angular-grid';
import { CompositeFilterDescriptor, process as BC, SortDescriptor, State } from '@progress/kendo-data-query';
import { JobListService } from '../job-list.service';

@Component({
  selector: 'app-job-results',
  templateUrl: './job-results.component.html',
  styleUrls: ['./job-results.component.scss']
})
export class JobResultsComponent implements OnInit {

  constructor(public jobserv: JobListService, private route: ActivatedRoute, private tr: TranslateService) { }

  ngOnInit(): void {
    this.jobserv.state.subscribe(state=>{
      
      const data = this.jobserv.data?this.jobserv.data:[]
      //this.view = data
      this.view = BC(data, state?state:{})
     // //console.log('State changed', this.view, this.jobserv.data, state)
    })

    this.jobserv.setData.subscribe(()=>{
      const state = this.jobserv.state.value
      const data = this.jobserv.data?this.jobserv.data:[]
      try{
        //this.view = data
        this.view = { data: data.slice(0,state.take), total: data.length}
      }catch(e){
        //console.log(e, data, state);
        
      }
      

    })

    

    this.jobserv.state.next({ skip: 0, take: 15 })


    const isTop10 = this.isTop10
    this.jobserv.loadState(isTop10, ()=>{
      this.view = BC(this.jobserv.data, this.jobserv.state.value)
      //console.log('View init', this.view, this.jobserv.data, this.jobserv.state.value)
      
      
    })

    this.tr.onDefaultLangChange.subscribe(x=>{
      //console.log('Lang change', x)
      this.view = undefined
      setTimeout(()=>{
        this.view = {...BC(this.jobserv.data, this.jobserv.state.value) }
      },100)
      
    })

    this.jobserv.location.subscribe(()=>{
      this.jobserv.state.next({
        ...this.jobserv.state.value,
        skip: 0,
        take: 15,
        filter: this.getFilterDesc()
      })
      //console.log(this.jobserv.state.value)
    })

    this.jobserv.jobTypes.subscribe(()=>{
      this.jobserv.state.next({
        ...this.jobserv.state.value,
        skip: 0,
        take: 15,
        filter: this.getFilterDesc()
      })
      //console.log(this.jobserv.state.value)
    })
    this.jobserv.keyword.subscribe(()=>{
      this.jobserv.state.next({
        ...this.jobserv.state.value,
        skip: 0,
        take: 15,
        filter: this.getFilterDesc()
      })

      //console.log(this.jobserv.state.value)
    })
  }

  getFilterDesc(){

    const lang = this.tr.defaultLang == 'English'?'English':'Spanish'
    const keywords = this.jobserv.keyword.value.split(' ')
    .map(k=>k.trim()).filter(k=>k)
    .map(k=>({ field: 'positionDescriptionUpper', value: k.toLocaleUpperCase(), operator: 'contains'}))

    const filters = [
      //{ field: 'locationDescription', value: this.jobserv.location.value, operator: "contains"},
       ///{ field: 'jobTypeDescription', value: this.jobserv.jobTypes.value, operator: 'contains'},ç
       ...this.jobserv.location.value.length?[{ logic: 'or', filters: this.jobserv.location.value.map(j=>({ field:'location' + lang, value: j, operator: 'contains'})) }]:[],
       ...this.jobserv.jobTypes.value.length?[{ logic: 'or', filters: this.jobserv.jobTypes.value.map(j=>({ field:'jobType' + lang, value: j, operator: 'contains'})) }]:[],
       ...this.jobserv.keyword.value?[{ logic: 'or', filters: keywords }]:[],

     ]
    return (filters.length?{
      
      filters,
      logic: 'and'
   }:{}) as CompositeFilterDescriptor
  }

  view:any

  get isTop10(){
    return this.route.snapshot.url.toString().includes('top10')
  }

  pageChange(event:PageChangeEvent){
    this.jobserv.state.next({
      ...this.jobserv.state.value,
      ...event
    })
  }

  sortChange(event:SortDescriptor[]){
    this.jobserv.state.next({
      ...this.jobserv.state.value,
      sort: event
    })
  }

  dataStateChange(state:State){
    this.jobserv.state.next({
      ...state,
      filter: this.jobserv.state.value.filter
      
    })
    
  }

  get sort(){
    return this.jobserv.state.value.sort
  }
  get skip(){
    return this.jobserv.state.value.skip
  }
  get pageSize(){
    return this.jobserv.state.value.take
  }
  

}
