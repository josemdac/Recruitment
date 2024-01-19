import { Directive, ElementRef, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { WebSiteInfoService } from '../services/web-site-info.service';

@Directive({
  selector: '[defaultInfoField]'
})
export class DefaultInfoFieldDirective {

  @Input() defaultInfoField = ''

  constructor(private elRef: ElementRef<HTMLElement>, private webInfo: WebSiteInfoService, private translate: TranslateService) {

  }

  get lang(){
    return {
      English: 'English',
      'Español': 'Spanish'
    }[this.translate.defaultLang]
  }

  subs:Subscription[] = []

  ngOnInit(){

    this.subs = [
      this.webInfo.default.subscribe((info:any)=>{

        if(!info){
          return
        }
        const content = info[this.defaultInfoField+this.lang]?info[this.defaultInfoField+this.lang]:''
        this.elRef.nativeElement.innerHTML = content
      }),
      this.translate.onLangChange.subscribe(l=>{
        this.check()
      })
    ]
    
    this.check()

  }

  check(){
    const info: any = this.webInfo.defaultInfo
    if(!info){
      return
    }
    const content = info[this.defaultInfoField+this.lang]?info[this.defaultInfoField+this.lang]:''
    this.elRef.nativeElement.innerHTML = content    
  }
  ngOnChanges(){
    this.check()
  } 

  ngOnDestroy(){
    this.subs.forEach(s=>s.unsubscribe())
  }
}
