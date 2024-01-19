import { Directive, ElementRef } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appIMFooterSmall]'
})
export class IMFooterSmallDirective {

  constructor(private elRef: ElementRef<HTMLElement>, private imserv:IManagementService) { }

  ngAfterViewChecked(){
    this.elRef.nativeElement.style.setProperty('background', this.imserv.formData.footerBackgroundColor)
    this.elRef.nativeElement.style.setProperty('color', this.imserv.formData.footerTextColor)
  }

}
