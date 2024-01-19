import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { goToTop } from 'src/app/shared/helpers/tools';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';

@Component({
  selector: 'app-privacy',
  templateUrl: './privacy.component.html',
  styleUrls: ['./privacy.component.scss']
})
export class PrivacyComponent implements OnInit {

  @ViewChild('policyContent') content?: ElementRef<HTMLElement>
  constructor(private webInfo:WebSiteInfoService, private ts:TranslateService) { }

  ngOnInit(): void {
    goToTop()
  }

  get lang(){
    const lang= this.ts.defaultLang == 'English'?'en':'es'
    return lang
  }

  ngAfterViewInit(){
    if(this.content){


      this.content.nativeElement.innerHTML = (this.lang=='en')?
        this.webInfo.conf.value.privacyPolicy:
        this.webInfo.conf.value.privacyPolicyEs;
      this.webInfo.conf.subscribe(conf=>{
        if(this.content){
            this.content.nativeElement.innerHTML = (this.lang=='en')?
            conf.privacyPolicy:
            conf.privacyPolicyEs;
        }
      })

      this.ts.onDefaultLangChange.subscribe(()=>{
        if(this.content)
        this.content.nativeElement.innerHTML = (this.lang=='en')?
        this.webInfo.conf.value.privacyPolicy:
        this.webInfo.conf.value.privacyPolicyEs;
      })
    }
  }


}
