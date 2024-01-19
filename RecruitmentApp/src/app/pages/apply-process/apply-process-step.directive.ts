import { Directive, Input, TemplateRef } from '@angular/core';

@Directive({
  selector: '[appApplyProcessStep]'
})
export class ApplyProcessStepDirective {

  @Input() label = ''
  @Input() iconClass = ''
  @Input() code = ''
  @Input() validate?: ()=>boolean 
  @Input() hidden = false
  constructor(public tpl: TemplateRef<any>) { }

  

}
