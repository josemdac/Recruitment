import { Directive, ElementRef, Input } from '@angular/core';
import { MultiSelectComponent } from '@progress/kendo-angular-dropdowns';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';

@Directive({
  selector: '[formInputMultiselect]'
})
export class FormInputMultiselectDirective {

  @Input() formInputMultiselect?: { text: string, value: any}[]|string = []
  constructor(private mselect: MultiSelectComponent,private elRef: ElementRef<HTMLElement>, 
    private jconf: JsonConfigService) { }

  ngOnInit(){
    this.setUp()
  }

  ngOnChanges(){
    this.setUp()
  }

  ngAfterViewChecked(){
    this.elRef.nativeElement.style.setProperty( 'display', 'flex')
    this.elRef.nativeElement.style.setProperty( 'padding', '0.17em')
    this.elRef.nativeElement.classList.add('form-control')
    this.elRef.nativeElement.classList.remove('k-input-md', 'k-input','k-rounded-md','k-input-solid')
  }

  setUp(){
    if(!this.formInputMultiselect){
      return
    }

    if(typeof this.formInputMultiselect == 'string'){
      this.jconf.get<{value: any, text: string}[]>(this.formInputMultiselect + '/Dropdown')
        .subscribe(data=>{
          this.mselect.data = data
        })
      
    }else{
      this.mselect.data = this.formInputMultiselect
    }
  }

}
