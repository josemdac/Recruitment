import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject } from 'rxjs';
import { changeFavicon } from './shared/helpers/tools';
import { JsonConfigService } from './shared/services/json-config.service';
import { TimerServiceService } from './shared/services/timer-service.service';
import { WebSiteInfoService } from './shared/services/web-site-info.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'angularfiebase-authentication';
  constructor(public translate: TranslateService, 
    public jsonConf: JsonConfigService, public webConf: WebSiteInfoService,
    public route: ActivatedRoute, private sessionTimer:TimerServiceService) { 
    translate.addLangs(['English', 'Español']);
    translate.setDefaultLang('Español')
      //console.log(this.route, this.translate)

      const canvas = document.createElement('canvas')
      const context = canvas.getContext('2d');
      if(context){
        context.clearRect(0, 0, canvas.width, canvas.height);
      }
      changeFavicon(canvas.toDataURL())
      
    this.sessionTimer.start()

    this.jsonConf.config.subscribe((config:any)=>{
      //console.log(config)
      this.webConf.loadColors(config)
    })

    this.webConf.conf.subscribe(conf=>{
      document.title = conf.siteName?conf.siteName:''
      if(conf.favicon){
        const dataUri = `data:image/png;base64,${conf.favicon}`
        changeFavicon(dataUri)
      }
    })

    
  }


  

  routeUrl = new BehaviorSubject<string>('')

  get currentRoute(){
    //@ts-ignore
    return this.route.snapshot._routerState.url
  }
   

  get ready(){
    return !!this.translate.translations && !!this.translate.defaultLang
  }

}
