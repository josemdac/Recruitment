import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { goToTop } from 'src/app/shared/helpers/tools';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']
})
export class LandingComponent implements OnInit {

  constructor(private webInfo: WebSiteInfoService, private elRef:ElementRef<HTMLElement>, private router:Router) { }

  defaultInfo = new BehaviorSubject<any>({})

  ngOnInit(): void {
    this.webInfo.loadDefault()
    goToTop()
  }

  ngAfterViewChecked(){
    this.elRef.nativeElement.style.setProperty('background', `url(${this.webInfo.mainImage})`)
  }

  searchByKwLoc(event:any, kw: string, loc:string){
    this.router.navigate(['joblist', { keyword: kw, location: loc }])
  }


}
