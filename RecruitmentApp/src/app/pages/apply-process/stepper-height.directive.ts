import { Directive, ElementRef, Input } from '@angular/core';
import { ApplyProcessComponent } from './apply-process.component';

@Directive({
  selector: '[stepperHeight]'
})
export class StepperHeightDirective {

  @Input() stepperHeight?:ApplyProcessComponent


  constructor(private elementRef: ElementRef<HTMLElement>) { }

  ngOnInit(){
    if(this.stepperHeight){
      //console.log(this.height, 'st h')
      this.stepperHeight.stepperHeight = this.height
    }
   
  }
  ngOnChanges(){
    if(this.stepperHeight){
      //console.log(this.height, 'st h')
      this.stepperHeight.stepperHeight = this.height
    }
   
  }
  get height(){
    return this.elementRef.nativeElement.getBoundingClientRect().height
  }
}
