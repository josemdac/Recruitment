import { Directive, ElementRef, EventEmitter, Output } from '@angular/core';
import { ShareButton } from 'ngx-sharebuttons/button';

@Directive({
  selector: '[shareBtnCustom]'
})
export class ShareBtnCustomDirective {

  @Output() customClick = new EventEmitter()
  constructor(private shareBtn: ShareButton, private elRef: ElementRef<HTMLElement>) { }
  
  ngOnInit(){
    this.shareBtn.opened.subscribe(()=>{
      this.customClick.emit()
    })
  }

  ngAfterViewChecked(){
    this.shareBtn.url = '';
    const btn = this.elRef.nativeElement.querySelector('button');
    if(btn && btn.removeAllListeners){
      btn.removeAllListeners('click')
      btn.addEventListener('click', e=>{ this.customClick.emit() })
    }
    
  }
}
