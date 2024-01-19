import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { goToTop } from '../shared/helpers/tools';
import { WebSiteInfoService } from '../shared/services/web-site-info.service';
import { LayoutService } from './layout.service';
import { StandardModalService } from './standard-modal/standard-modal.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  constructor(private layout: LayoutService, 
    public stModal: StandardModalService,
    public route:ActivatedRoute,
    private webConf: WebSiteInfoService) { }

  ngOnInit(): void {
    //console.log(this.route.snapshot)
  }

  goToTop(event: any){
    event.preventDefault()
    goToTop()
  }

  get showGoTop(){
    const h = window.innerHeight
    return window.scrollY > h/5
  }


  get isIM(){
    //@ts-ignore
    return this.route.snapshot._routerState.url.includes('interface-management')
  }
}
