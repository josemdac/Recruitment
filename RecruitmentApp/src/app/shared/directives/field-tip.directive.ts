import { Directive, ElementRef, Input } from '@angular/core';
import { FormInputComponent } from 'src/app/components/form-input/form-input.component';

@Directive({
  selector: '[appFieldTip]'
})
export class FieldTipDirective {

  @Input() appFieldTip =''
  @Input() fieldNames:string[] = []
  span = document.createElement('span')
  constructor(private elRef:ElementRef<HTMLElement>, private input: FormInputComponent) { }


  ngOnInit(){
    this.setUp()
  }


  ngOnChanges(){
    this.setUp()
  }

  setUp(){
    const install = ()=>{
      const fLabel = this.elRef.nativeElement.querySelector('label')
      if(fLabel){
        this.span.textContent = this.appFieldTip;
        this.span.classList.add('add-tip')
        fLabel.classList.add('label-tip')
        fLabel.appendChild(this.span)
      }
    }

    if(!this.elRef.nativeElement.contains(this.span)){
      if(this.fieldNames && this.fieldNames.length){
        if(this.fieldNames.includes(this.input.field.name)){
          install()
        }
        return 
      }
      install()
    }
  }
}
