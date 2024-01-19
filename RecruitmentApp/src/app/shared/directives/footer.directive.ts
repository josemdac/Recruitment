import { Directive, ElementRef, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { WebSiteInfoService } from '../services/web-site-info.service';

@Directive({
  selector: '[appFooterField]'
})
export class FooterDirective {

  @Input() appFooterField = 'text1'

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
      this.webInfo.footer.subscribe((info:any)=>{
        const content = info[this.appFooterField+this.lang]?info[this.appFooterField+this.lang]:''
        this.elRef.nativeElement.innerHTML = content
      }),
      this.translate.onLangChange.subscribe(l=>{
        this.check()
      })
    ]
    
    this.check()

  }

  check(){
    const info: any = this.webInfo.footer.value
    const content = info[this.appFooterField+this.lang]?info[this.appFooterField+this.lang]:''
    this.elRef.nativeElement.innerHTML = content    
  }
  ngOnChanges(){
    this.check()
  } 

  ngOnDestroy(){
    this.subs.forEach(s=>s.unsubscribe())
  }
}
