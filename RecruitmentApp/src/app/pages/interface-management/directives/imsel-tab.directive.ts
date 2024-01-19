import { Directive, ElementRef } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appIMSelTab]'
})
export class IMSelTabDirective {

  constructor(private elRef: ElementRef<HTMLElement>, private imserv:IManagementService) { }

  ngAfterViewChecked(){
    this.elRef.nativeElement.style.setProperty('color', this.imserv.formData.selectedTabTextColor)
  }
}
