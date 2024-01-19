import { Directive, ElementRef, Input } from '@angular/core';
import { AuthService } from 'src/app/shared/services/auth.service';
import { JobListService } from '../job-list.service';

@Directive({
  selector: '[appIsAppliedJob]'
})
export class IsAppliedJobDirective {

  @Input() appIsAppliedJob?: { requestId: number}
  constructor(private js:JobListService, private e:ElementRef<HTMLElement>, private a: AuthService) { }

  appliedIcon = document.createElement('span')

  ngOnInit(){
    this.e.nativeElement.appendChild(this.appliedIcon)
    this.loadState()
  }

  ngOnChanges(){
    this.loadState()
  }

  loadState(){
    //return 
   // if(this.a.isLoggedIn){
      const r = this.appIsAppliedJob?.requestId
      if(!r){ this.appliedIcon.innerHTML = ''; return }
      this.js.loadIsApplied(r)
       .subscribe(v=>{
         if(v.applied){
           this.appliedIcon.innerHTML = `<i class="fas fa-check"></i>`
         }else{
           this.appliedIcon.innerHTML = ''
         }
       })
   // }
    
  }
}
