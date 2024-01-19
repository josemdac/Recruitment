import { Directive, ElementRef, Input } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Directive({
  selector: '[appIMStepper]'
})
export class IMStepperDirective {

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

    this.styleTag.innerHTML = `
    .stepper-sample kendo-stepper .k-stepper .k-step-indicator k-step-current {
      background: ${this.imserv.formData.stepperColor} !important;
      color: white;
      border: solid 1px ${this.imserv.formData.stepperColor} !important;
    }

    .stepper-sample kendo-stepper.k-stepper .k-step-done .k-step-indicator {
        background-color: ${this.imserv.formData.stepperColor} !important;
        border-color: ${this.imserv.formData.stepperColor} !important;
    }

    .stepper-sample kendo-stepper.k-stepper .k-step-current .k-step-indicator {
        background-color: ${this.imserv.formData.stepperColor} !important;
        border-color: ${this.imserv.formData.stepperColor} !important ;
    }

    .stepper-sample kendo-stepper .k-progressbar .k-state-selected {
        background: ${this.imserv.formData.stepperColor} !important;
        height: 4px !important;
    }

    .stepper-sample kendo-stepper span.k-step-text.ng-star-inserted {
        font-size: .7rem;
    }

    .stepper-sample kendo-stepper.k-widget.k-stepper {
        width: 50%;
        height: 5em;
        
    }

    .stepper-sample kendo-progressbar.k-widget.k-progressbar.k-progressbar-horizontal.ng-star-inserted {
        background: #777;
        height: 2px;
    }

    .stepper-sample kendo-stepper.k-stepper .k-step-indicator {
        background: #ddd;
    }

        
    .stepper-sample kendo-stepper span.k-step-indicator.ng-star-inserted {
        width: 40px !important;
        height: 40px !important;
    }

    .stepper-sample kendo-progressbar.k-widget.k-progressbar.k-progressbar-horizontal.ng-star-inserted {
        top: 22px !important;
    }

    .stepper-sample kendo-stepper span.k-step-text.ng-star-inserted {
        text-overflow: clip !important;
        overflow: visible;
    }

    .stepper-sample kendo-stepper span.k-step-label.ng-star-inserted {
        width: max-content !important;
        overflow: visible !important;
    }

    .stepper-sample .k-stepper .k-step-label span.k-step-text.ng-star-inserted {
        overflow: visible;
    }

    .stepper-sample kendo-stepper span.indicator-icon.ng-star-inserted {
        font-size: 1.5em;
    }
    .stepper-sample kendo-stepper span.k-step-text {
      color: ${this.imserv.formData.stepperFontColor};
    }
    
    `
    
    
  }

}
