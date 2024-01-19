import { Directive, ElementRef } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appIMContentArea]'
})
export class IMContentAreaDirective {

  constructor(private elRef: ElementRef<HTMLElement>, private imserv:IManagementService) { }

  ngAfterViewChecked(){
    this.elRef.nativeElement.style.setProperty('background', this.imserv.formData.contentAreaBackgroundColor)
    this.elRef.nativeElement.style.setProperty('color', this.imserv.formData.contentAreaTextColor)
  }

}
