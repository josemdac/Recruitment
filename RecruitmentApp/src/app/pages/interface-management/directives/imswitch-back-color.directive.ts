import { Directive, ElementRef } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appIMSwitchBackColor]'
})
export class IMSwitchBackColorDirective {

  constructor(private elRef: ElementRef<HTMLElement>, 
    private imserv:IManagementService) { }

  ngAfterViewChecked(){
    const track = this.elRef.nativeElement.querySelector<HTMLElement>('.k-switch-track')
    if(track){
      track.style.setProperty('border-color', this.imserv.formData.switchBackColor, 'important')
      track.style.setProperty('background-color', this.imserv.formData.switchBackColor)
    }
   
  }


}
