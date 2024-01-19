import { Directive, ElementRef, Input } from '@angular/core';
import { MaskedTextBoxComponent } from '@progress/kendo-angular-inputs';

@Directive({
  selector: '[formInputTel]'
})
export class FormInputTelDirective {

  @Input() formInputTel = ''
  constructor(private elementRef: ElementRef<HTMLElement>, private maskedTb: MaskedTextBoxComponent) { 

  }

  ngOnChanges({ formInputTel }:any){
    if(formInputTel.currentValue){
      console.log('Setting tel value ',formInputTel)
      this.maskedTb.writeValue(formInputTel.currentValue.trim())
    }else{
      this.maskedTb.value = ''
    }
  }

  ngAfterViewChecked(){
    this.elementRef.nativeElement.className = ''
  }
  ngOnInit(){

   
    const inp = this.elementRef.nativeElement.querySelector('input')
    this.maskedTb.onFocus.subscribe(event=>{
      // setTimeout(()=>{
      // if(inp){
      //   const sel = (this.maskedTb.value +'').length;
      //   inp.setSelectionRange(sel,sel)
      // }
      // },100)
    })
    if(inp){
      inp.classList.add('form-control')
    }
  }

}
