import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[formInputDate]'
})
export class FormInputDateDirective {

  constructor(private elRef:ElementRef<HTMLElement>) { }

  ngAfterViewChecked(){
    const inp = this.elRef.nativeElement.querySelector('input')


    if(inp){
      inp.className = 'form-control'
    }
  }

}
