import { Directive, ElementRef } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appIMLogo]'
})
export class IMLogoDirective {

  img = document.createElement('img')
  constructor(private elRef:ElementRef<HTMLElement>, private imservice:IManagementService) { }

  get logoPreview(){
    const data = this.imservice.formData.logoImage
    if(data){
      return 'data:image/png;base64,'+data
    }
    return ''
  }

  ngAfterViewChecked(){
    this.img.src = this.logoPreview
  }

  ngOnInit(){
    this.elRef.nativeElement.appendChild(this.img)
  }


}
