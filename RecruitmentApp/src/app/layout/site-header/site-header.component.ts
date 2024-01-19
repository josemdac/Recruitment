import { Component, OnInit } from '@angular/core';
import { LangChangeEvent, TranslateService } from '@ngx-translate/core';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';

@Component({
  selector: 'app-site-header',
  templateUrl: './site-header.component.html',
  styleUrls: ['./site-header.component.scss']
})
export class SiteHeaderComponent implements OnInit {

  constructor(private translate: TranslateService, 
    private jsonConf: JsonConfigService,
    private webInfo:WebSiteInfoService) { }

  ngOnInit(): void {
    this.jsonConf.config.subscribe(()=>{
     this.webInfo.loadDefault()
    
    })
  }

  get currentLang(){
    return this.translate.defaultLang
  }

  toggleLang(event:any){
    event.preventDefault();
    let nextLang = this.translate.getLangs().filter(l=>l!=this.translate.defaultLang)[0]
    this.translate.setDefaultLang(nextLang)
    this.translate.defaultLang = nextLang;
    const langEvent:LangChangeEvent = { lang: nextLang, translations: this.translate.translations }
    this.translate.onLangChange.emit(langEvent)
  }

  clickLogo(){
    if(this.webInfo.conf.value.logoUrl){
      const link = document.createElement('a')
      link.href = this.webInfo.conf.value.logoUrl
      link.click()
      link.remove()
    }
    
  }

  get langButtonText(){
    const lang= this.translate.defaultLang == 'English'?'Español':'English'
    return lang
  }

}
