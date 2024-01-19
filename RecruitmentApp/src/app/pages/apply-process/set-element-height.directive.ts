import { Directive, ElementRef, Input } from '@angular/core';

@Directive({
  selector: '[appSetElementHeight]'
})
export class SetElementHeightDirective {

  @Input() appSetElementHeight = ''
  constructor(public elRef:ElementRef<HTMLElement>) { }

  ngOnChanges(){
    //console.log(this.appSetElementHeight)
    this.elRef.nativeElement.style.setProperty('min-height', this.appSetElementHeight, 'important')
  }
  
  ngOnInit(){
    this.elRef.nativeElement.style.setProperty('min-height', this.appSetElementHeight, 'important')
  }



}
