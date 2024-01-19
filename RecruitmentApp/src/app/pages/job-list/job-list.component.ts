import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PageChangeEvent } from '@progress/kendo-angular-grid';
import { State, process } from  '@progress/kendo-data-query'
import { Subscription } from 'rxjs';
import { goToTop } from 'src/app/shared/helpers/tools';
import { JobListService } from './job-list.service';

@Component({
  selector: 'app-job-list',
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.scss']
})
export class JobListComponent implements OnInit {

  constructor(public jobServ: JobListService, private route: ActivatedRoute) { }
  ngOnInit(): void {
    goToTop()
  }

  
  ngAfterViewInit(): void {
    this.init()
     
   
  }

  get isTop10(){
    return this.route.snapshot.url.toString().includes('top10')
  }

  // get keyword(){
  //   return this.route.snapshot.paramMap.get('keyword')
  // }
  // get location(){
  //   return this.route.snapshot.paramMap.get('location')
  // }
  subs:Subscription[] = []

  removeSubs(){
    this.subs.forEach(s=>s.unsubscribe())
  }

  firstLoad = true
  init(){
    this.jobServ.data = []
    this.removeSubs()
    //console.log('Init jobs 1')
    this.jobServ.init()
    // if(this.keyword){
    //   this.jobServ.keyword.next(this.keyword)
    // }

    // if(this.location){
    //   this.jobServ.location.next([this.location])
    // }
    
    // setTimeout(()=>{
    //   this.jobServ.loadState(this.isTop10)
    // },10)
    
    if(!this.isTop10){
      this.subs = [
        // this.jobServ.state.subscribe(s=>!this.firstLoad?this.jobServ.loadState():{}),
        // this.jobServ.location.subscribe(s=>!this.firstLoad?this.jobServ.loadState():{}),
        // this.jobServ.keyword.subscribe(s=>!this.firstLoad?this.jobServ.loadState():{}),
        // this.jobServ.jobTypes.subscribe(s=>!this.firstLoad?this.jobServ.loadState():{}),
        
      ]
    }

    this.jobServ.loadState(this.isTop10, ()=>{
      this.firstLoad = false
    })

    

   
  }

  ngOnDestroy(){
    this.removeSubs()
  }

  


  

}
