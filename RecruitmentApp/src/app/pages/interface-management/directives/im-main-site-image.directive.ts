import { Directive, ElementRef } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appImMainSiteImage]'
})
export class ImMainSiteImageDirective {

  img = document.createElement('img')
  constructor(private elRef:ElementRef<HTMLElement>, private imservice:IManagementService) { }

  get mainSiteImagePreview(){
    const data = this.imservice.formData.mainSiteImage
    if(data){
      return 'data:image/png;base64,'+data
    }
    return ''
  }

  ngAfterViewChecked(){
    this.img.src = this.mainSiteImagePreview
  }

  ngOnInit(){
    this.elRef.nativeElement.appendChild(this.img)
  }

}
