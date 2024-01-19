import { Directive, EventEmitter, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[appExpandTabButton]'
})
export class ExpandTabButtonDirective {

  constructor() { }
  @Output() btnClick = new EventEmitter<any>()

  @HostListener('click', ['$event'])
  onClick(event: any){
    event.stopProppation()
    this.btnClick.emit(event)

  }

}
