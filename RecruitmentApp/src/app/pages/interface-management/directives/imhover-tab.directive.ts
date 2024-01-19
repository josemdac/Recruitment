import { Directive, ElementRef } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appIMHoverTab]'
})
export class IMHoverTabDirective {

  constructor(private elRef: ElementRef<HTMLElement>, private imserv:IManagementService) { }

  ngAfterViewChecked(){
    this.elRef.nativeElement.style.setProperty('color', this.imserv.formData.hoverTabBackground)
  }

}
