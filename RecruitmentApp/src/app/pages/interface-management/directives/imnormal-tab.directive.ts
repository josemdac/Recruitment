import { Directive, ElementRef } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appIMNormalTab]'
})
export class IMNormalTabDirective {

  constructor(private elRef: ElementRef<HTMLElement>, private imserv:IManagementService) { }

  ngAfterViewChecked(){
    this.elRef.nativeElement.style.setProperty('color', this.imserv.formData.normalTabTextColor)
  }
}
