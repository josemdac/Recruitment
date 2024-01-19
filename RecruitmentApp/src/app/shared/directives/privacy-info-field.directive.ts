import { Directive, ElementRef, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { WebSiteInfoService } from '../services/web-site-info.service';

@Directive({
  selector: '[privacyInfoField]'
})
export class PrivacyInfoFieldDirective {

  @Input() privacyInfoField = ''

  constructor(private elRef: ElementRef<HTMLElement>, 
    private webInfo: WebSiteInfoService, 
    private translate: TranslateService) {

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
      this.webInfo.privacy.subscribe((info:any)=>{
        const content = info[this.privacyInfoField+this.lang]?info[this.privacyInfoField+this.lang]:''
        this.elRef.nativeElement.innerHTML = content
      }),
      this.translate.onLangChange.subscribe(l=>{
        this.check()
      })
    ]
    
    this.check()

  }

  check(){
    const info: any = this.webInfo.privacy.value
    const content = info[this.privacyInfoField+this.lang]?info[this.privacyInfoField+this.lang]:''
    this.elRef.nativeElement.innerHTML = content    
  }
  ngOnChanges(){
    this.check()
  } 

  ngOnDestroy(){
    this.subs.forEach(s=>s.unsubscribe())
  }
}
