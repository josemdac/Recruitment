import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { IManagementService } from '../imanagement.service';

@Component({
  selector: 'app-interface-management-toggle',
  templateUrl: './interface-management-toggle.component.html',
  styleUrls: ['./interface-management-toggle.component.scss']
})
export class InterfaceManagementToggleComponent implements OnInit {

  constructor(private translate: TranslateService, 
    public imservice: IManagementService, private cdr: ChangeDetectorRef) { }

  t(text:string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }

  ngOnInit(): void {
    setTimeout(()=>{
      this.fields = [
        {
          name: 'useCustomColors', type: 'radio', radioOptions: []
        }
      ]
      this.fields[0].radioOptions = [
        { label: this.t('DefaultColors'), value: 'N'},
        { label: this.t('CustomColors'), value: 'Y'},
      ]
      
    },200)
    
  }


  ngAfterViewChecked(){
    if(this.fields.length){
      this.fields[0].radioOptions = [
        { label: this.t('DefaultColors'), value: 'N'},
        { label: this.t('CustomColors'), value: 'Y'},
      ]

      this.cdr.detectChanges()
    }
  }

  fields:IFormInput[]= [
    
  ]

  changeToggle(event: any, field:IFormInput){
    this.imservice.formData[field.name] = event
    //console.log(this.imservice.formData, field.name)
    this.imservice.toggleDefaultColors()
    this.imservice.setCurrentConfig(event)
    //console.log(this.imservice.formData)
  }
}
