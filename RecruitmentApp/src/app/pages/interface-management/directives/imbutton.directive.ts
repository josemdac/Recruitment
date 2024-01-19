import { Directive, ElementRef, Input } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appIMButton]'
})
export class IMButtonDirective {

  @Input() appIMButton: ''|'2'|'3' = ''
  styleTag = document.createElement('style')
  btnId = 'btnHover'
  
  constructor(private elRef: ElementRef<HTMLElement>, private imserv:IManagementService) { }

  ngOnDestroy(){
    this.styleTag.remove()
  }

  ngOnInit(){
    document.body.appendChild(this.styleTag)
    this.elRef.nativeElement.id = this.btnId + this.appIMButton
  }

  ngAfterViewChecked(){

    if(this.appIMButton == '3'){
      const el = document.querySelector('app-expand-tab item label')
      this.styleTag.innerHTML = `
        app-expand-tab-layout#${this.btnId+ this.appIMButton} .item .label:hover { 
          background: ${this.imserv.formData['buttons'+this.appIMButton+'HoverColor']} !important;
        }


        app-expand-tab-layout#${this.btnId+ this.appIMButton} .item .label{
          transition: 500ms;
          background: ${this.imserv.formData['buttons'+this.appIMButton+'Color']};
          color: ${this.imserv.formData['buttons'+this.appIMButton+'TextColor']}
        }
      `
      return 
    }
    this.styleTag.innerHTML = `
      button#${this.btnId+ this.appIMButton}:hover { 
        background: ${this.imserv.formData['buttons'+this.appIMButton+'HoverColor']} !important;
      }


      button#${this.btnId+this.appIMButton}{
        transition: 500ms;
      }
    `

    this.elRef.nativeElement.style.setProperty('background', this.imserv.formData['buttons'+this.appIMButton+'Color'])
    this.elRef.nativeElement.style.setProperty('color', this.imserv.formData['buttons'+this.appIMButton+'TextColor'])
    
  }
}
