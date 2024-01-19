import { Directive, HostListener, Input } from '@angular/core';
import { JobListService } from './job-list.service';

@Directive({
  selector: '[appJobTv]'
})
export class JobTvDirective {

  @Input() appJobTv = 0
  constructor(private jobserv: JobListService) { }

  @HostListener('click', ['$event'])
  onClick(event: any){
    if(this.appJobTv){
      this.jobserv.countPositionView(this.appJobTv)
    }
  }
}
