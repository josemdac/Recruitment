import { Directive, ElementRef } from '@angular/core';
import { FileSelectComponent } from '@progress/kendo-angular-upload';

@Directive({
  selector: '[formInputFile]'
})
export class FormInputFileDirective {

  constructor(private elementRef: ElementRef<HTMLElement>, private upload: FileSelectComponent ) {

   }

   ngAfterViewChecked(){
     this.checkRestrictions()
   }

   checkRestrictions(){
    let fileInputs:HTMLInputElement[] = Array.from(this.elementRef.nativeElement.querySelectorAll('input[type="file"]'))
  
    const exts = this.upload.restrictions.allowedExtensions?.map(e=>'.'+e).join(',')

    if(exts){
    //  //console.log(fileInputs)
     fileInputs.forEach(f=>{
       f.accept = exts
     })
    }
   }

}
