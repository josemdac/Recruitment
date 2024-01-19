import { Directive, ElementRef, HostBinding } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appIMHeader]'
})
export class IMHeaderDirective {

  constructor(private elRef: ElementRef<HTMLElement>, private imserv:IManagementService) { }

  ngAfterViewChecked(){
    this.elRef.nativeElement.style.setProperty('background', this.imserv.formData.headerBackgroundColor)
  }

}
