import { EventEmitter, Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { State, process } from  '@progress/kendo-data-query'
import { request } from 'http';
import { BehaviorSubject } from 'rxjs';
import { HrRcrtPositionRequest, HrRcrtPositionRequestListStateDTO } from 'src/app/shared/models/HrRcrtPositionRequest.model';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';

@Injectable({
  providedIn: 'root'
})
export class JobListService {

  constructor(private jconf:JsonConfigService, private translate: TranslateService) { }

  state = new BehaviorSubject<State>({
    skip: 0,
    take: 15,
  }) 

  keyword = new BehaviorSubject('')
  location = new BehaviorSubject<string[]>([])
  jobTypes = new BehaviorSubject<string[]>([])

  data:any = []

  init(){
    this.state.next({
      skip: 0,
      take: 15,
    })

    this.keyword.next('')
    this.location.next([])
  }

  loadingJobs = false
  loadState(isTop10=false, onLoad?:()=>void){

    this.data = []

    const { skip, take, sort } = this.state.value
    const kw = this.keyword.value
    const loc = this.location.value
    const words = (kw || loc)?[
      kw?'Keyword='+kw:'',
      loc?'Location='+loc:''
    ].join('&'):''

    const state:HrRcrtPositionRequestListStateDTO = {
      keyWord: this.keyword.value,
      location: this.location.value.join(' '),
      skip,
      take,
      sort,
      lang: this.translate.defaultLang,
      jobTypes: this.jobTypes.value,
    }

    this.loadingJobs = true
    if(isTop10){
      this.jconf.get<any>(`HrRcrtPositionRequests/Top10`)
      .subscribe(({data, count})=>{
       // const after = count - (skip?skip:0) - (data.length?data.length:0)
        //console.log(count, after, skip)
        this.data = data.map((d:any)=>({ ...d, 
          positionDescriptionUpper: `${d.positionDescription}`.toLocaleUpperCase()}))
        this.loadingJobs = false
        
        this.setData.emit(data)
        //console.log(this.data)
        if(onLoad){
          onLoad()
        }
      })
      return 
    }

    this.jconf.post<any>(`HrRcrtPositionRequests`, state)
      .subscribe((data)=>{
        //let after = count - (skip?skip:0)

        
        //console.log(data.length)
        // this.data = [
        //   ...skip?(new Array(skip?skip:0)):[],
        //   ...data,
        //   ...after?(new Array(after)):[]
        // ]

        this.data = data.map((d:any)=>({ ...d, 
          positionDescriptionUpper: `${d.positionDescription}`.toLocaleUpperCase()}))
        this.loadingJobs = false
        this.setData.emit(data)
        if(onLoad){
          onLoad()
        }
        //console.log(this.data)
      })
  }

  setData = new EventEmitter()

  loadPosition(requestId:any){
    return this.jconf.get<HrRcrtPositionRequest>('HrRcrtPositionRequests/'+ requestId)
  }

  loadHeaderPosition(requestId:any){
    return this.jconf.get<HrRcrtPositionRequest>('HrRcrtPositionRequests/Header/'+ requestId)
  }

  countPositionView(id:any){
    return this.jconf.post<any>('HrRcrtPositionRequests/PV/'+ id, {}).subscribe(()=>{})
  }

  loadIsApplied(requestId:any){
    return this.jconf.get<{ applied: boolean }>('HrRcrtApplicant/IsApplied/'+ requestId)
  }
}
