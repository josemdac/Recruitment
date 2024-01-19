import { ChangeDetectorRef, Directive, Input } from '@angular/core';

@Directive({
  selector: '[appFormInputRadio]'
})
export class FormInputRadioDirective {

  @Input() appFormInputRadio:any[] = []
  constructor(private cdr:ChangeDetectorRef) { }

  ngOnInit(){
    this.cdr.detectChanges()
  }


  ngOnChanges(){
    this.cdr.detectChanges()
    let p  = new Promise(()=>{
      this.cdr.detectChanges()
    })
    p.then(()=>{ 
      //console.log('ref')
    })
  }
}
