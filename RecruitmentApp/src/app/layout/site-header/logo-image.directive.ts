import { Directive, ElementRef } from '@angular/core';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';

@Directive({
  selector: '[appLogoImage]'
})
export class LogoImageDirective {

   img = document.createElement('img')
  constructor(private elRef:ElementRef<HTMLElement>, private wconf: WebSiteInfoService) { }

  
  ngAfterViewChecked(){
    this.img.src = this.wconf.logoImg
    if(!this.wconf.logoImg){
      this.img.style.setProperty('display', 'none')
      return
    }
    this.img.style.setProperty('display', '')
  }

  ngOnInit(){
    this.elRef.nativeElement.appendChild(this.img)
  }

}
