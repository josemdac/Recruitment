import { Directive, ElementRef, Input } from '@angular/core';

@Directive({
  selector: '[innerContent]'
})
export class InnerContentDirective {


  @Input() innerContent= ''
  constructor(private elRef: ElementRef<HTMLElement>) { }

  ngOnInit(){
    this.setContent()
  }

  ngOnChanges(){
    this.setContent()
  }

  setContent(){
    this.elRef.nativeElement.innerHTML = this.innerContent
  }
}
