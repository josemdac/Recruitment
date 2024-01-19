import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { faInstagram } from '@fortawesome/free-brands-svg-icons';
import { Router } from '@angular/router';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';
import { windowWhen } from 'rxjs';
import { HrRcrtExternalWebsiteInfo } from 'src/app/shared/models/HrRcrtExternalWebsiteInfo.model';


@Component({
  selector: 'app-site-footer',
  templateUrl: './site-footer.component.html',
  styleUrls: ['./site-footer.component.scss']
})
export class SiteFooterComponent implements OnInit {

  icon = faInstagram
  constructor(private translate: TranslateService, 
    private router: Router, private webconf: WebSiteInfoService) { 

  }

  ngOnInit(): void {

    this.webconf.transparency.subscribe((tp:HrRcrtExternalWebsiteInfo)=>{
      if((tp.text1English || tp.text1Spanish) && !this.links.some((l:any)=>l['name'] == 'Transparency')){
        
        const dl = this.translate.store.defaultLang == 'English'?'English':"Spanish"
        const lnk = {
          //@ts-ignore
          ['name']: 'Transparency',
          //@ts-ignore
          label: tp['title1' +dl],
          //@ts-ignore
          onClick: ()=>window.open(tp['text1' + dl])
        }

        //console.log(tp, lnk, 'title1' + dl, 'text1' + dl)
        this.translate.onDefaultLangChange.subscribe(()=>{
          const dl = this.translate.store.defaultLang == 'English'?'English':"Spanish"
          //@ts-ignore
          lnk.label =  tp['title1' +dl]
          //@ts-ignore
          lnk.onClick = ()=>window.open(tp['text1' + dl])
        })
        this.links.push(lnk)
      }
    })
    
  }
  

  get linkedIn(){
    return this.webconf.conf.value.linkedinLink
  }
  get facebook(){
    return this.webconf.conf.value.facebookLink
  }
  get twitter(){
    return this.webconf.conf.value.twitterLink
  }
  get instagram(){
    return this.webconf.conf.value.instagramLink
  }

  shareBtnClick(event:any, link:string){
    
    const a = document.createElement('a')
    a.href = link
    a.target = 'blank'
    a.click()
  }

  links: { label:string, onClick: (event:MouseEvent)=>void, children?: any[]}[] = [
    //{ label: 'RenovaSolutions.com', onClick: (ev)=>{}, children: []},
    { label: 'viewAllJobs', onClick: (ev)=>{
      this.router.navigateByUrl('joblist')
    }},
    { label: 'TopJobSearches', onClick: (ev)=>{
      this.router.navigateByUrl('top10')
    }},
    { label: 'PrivacyPolicy', onClick: (ev)=>{
      this.router.navigateByUrl('privacy')
    }},
  ]

  get lang(){
    return {
      'Español': 'es',
      'English': 'en'
    }[this.translate.defaultLang]
  }

  url = window.location.href


  shareInstagram(event:any){
      //console.log('share on instagram')

  }

  register(email:HTMLInputElement){
    if(email.validity.valid){
      this.router.navigate(['register', { email: email.value}])
    }
  }
}
