import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { IFormInput } from './form-input.model';
import {
  SelectEvent,
  RemoveEvent,
  FileRestrictions,
} from "@progress/kendo-angular-upload";
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';
import { MultiSelectComponent } from '@progress/kendo-angular-dropdowns';

@Component({
  selector: 'form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.scss']
})
export class FormInputComponent implements OnInit {

  @ViewChild(MultiSelectComponent) multiselect?:MultiSelectComponent
  @Input() field: IFormInput = { name: '', col: 0, type: '', label: ''}
  @Input() formData:any = {}
  constructor(private elRef: ElementRef<HTMLElement>, 
    private cdr: ChangeDetectorRef,
    private webInfo: WebSiteInfoService,
    private translate:TranslateService) { }

  @Input() value:any = ''
  @Output() valueChange = new EventEmitter<any>()
  @Output() keyUp  = new EventEmitter<any>()
  @Input() disabled?: boolean | { start: boolean, end: boolean}


  
  telmask = "(999) 000-0000";

  reset(){
    if(this.isMultiselect && this.multiselect){
      this.multiselect.writeValue([])
    }
  }
  ngOnChanges(changes:any){

    this.elRef.nativeElement.setAttribute('type', this.type)
    // if(!this.field.label){
    //   this.field.label = this.t(this.field.name.toUpperCase().trim())
    // }

    if(this.isFile){
      this.files = this.value?[this.value]:[]
    }

    if(this.isDateRange){
      this.range = {
        start: this.value.start,
        end: this.value.end
      }
    }

    if(changes['field']?.radioOptions){
      this.cdr.detectChanges()
    }

    if(changes['field']?.name){
      this.cdr.detectChanges()
    }
    if(changes['field']){
      this.cdr.detectChanges()
    }
  }

  getLabel(){
    let l = this.field.label?this.field.label:this.t(this.field.name.toUpperCase())
    //this.cdr.detectChanges();
    return l
  }

  get currencySymbol(){
    const { currencyCode, cultureCode} = this.webInfo.culture.value
    if(currencyCode && cultureCode){
      return this.getCurrencySymbol(cultureCode, currencyCode)
    }

    return ''
    
  }

  get fieldOptions(){
    if(this.field.radioOptions){
      return this.field.radioOptions
    }
    return []
  }

  ngAfterViewInit(){
    this.cdr.detectChanges()
  }
  getCurrencySymbol = (locale:string, currency: string) => (0).toLocaleString(locale, { style: 'currency', currency, minimumFractionDigits: 0, maximumFractionDigits: 0 }).replace(/\d/g, '').trim()
  ngOnInit(): void {
    this.elRef.nativeElement.setAttribute('type', this.type)
    if(this.isDateRange){
      this.range = {
        start: this.value.start,
        end: this.value.end
      }
    }

    // if(!this.field.label){
    //   this.field.label = this.t(this.field.name.toUpperCase().trim())
    // }
  }

  get type(){
    if(this.field){
      return this.field.type
    }

    return ''
  }
  get checked(){
    if(this.field.checkBoxValues){
      const [tval, fval] = this.field.checkBoxValues
      return this.value == tval.trueValue
    }

    return false
  }

  get isBlank(){
    return this.type == ''
  }
  get isEditor(){
    return this.type == 'editor'
  }

  onKeyUp(event:any){
    this.keyUp.emit(event)
  }

  changeTel(value:any){
    //console.log(value)
    this.formData[this.field.name] = value
    this.valueChange.emit((value+'').trim())
  }

  changeText(event:any){
    this.formData[this.field.name] = event.target.value
    this.valueChange.emit(event.target.value)
  }

  changeEditor(event:any){
    this.formData[this.field.name] = event
    this.valueChange.emit(event)
  }

  changeCurrency(event:any){
    const value = parseFloat(event.target.value+'')
    this.formData[this.field.name] = value
    this.valueChange.emit(value)
  }
  changeCheckbox(event:any){
    const values = this.field.checkBoxValues
    if(!values){
      return
    }
    const trueVal = values[0].trueValue
    const falseVal = values[1].falseValue
    this.formData[this.field.name] = event.target.checked?trueVal:falseVal
    this.valueChange.emit(event.target.checked?trueVal:falseVal)
  }

  changeSwitch(value:any){
    const values = this.field.checkBoxValues
    if(!values){
      return
    }
    const trueVal = values[0].trueValue
    const falseVal = values[1].falseValue
    this.formData[this.field.name] = value?trueVal:falseVal
    this.valueChange.emit(value?trueVal:falseVal)
  }

  changeDateRange(value:any, inp:'start'|'end'){
    //const name:any = ( inp == 'start')?this.field.startName:this.field.endName 
    //this.formData[name] = value
    this.range[inp] = value
    //console.log(value, this.value, this.range)
    this.valueChange.emit(this.range)
  }

  changeSelect(event:any){
    this.formData[this.field.name] = event.target.value
    this.valueChange.emit(event.target.value)
  }
  changeMultiSelect(event:any){
    this.formData[this.field.name] = event
    this.valueChange.emit(event)
  }
  
  changeDate(event:Date){
    this.formData[this.field.name] = event
    this.valueChange.emit(event)
  }

  changeFile(event:any){
    if(!event){
      this.valueChange.emit(undefined)
      return 
    }
    this.valueChange.emit(event[0])
  }

  changeRadio(event:any){
    //console.log(event)
    this.valueChange.emit(event.target.value)
  }
  changeColor(event:any){
    this.valueChange.emit(event)
  }
  get isDropdown(){
    return this.type == 'dropdown'
  }
  get isCheckbox(){
    return this.type == 'checkbox'
  }

  get isTel(){
    return this.type == 'tel'
  }

  get isTextArea(){
    return this.type == 'textarea'
  }

  get isFile(){
    return this.type == 'file'
  }
  get isSwitch(){
    return this.type == 'switch'
  }

  get isDateRange(){
    return this.type  == 'daterange'
  }

  get isCurrency(){
    return this.type == 'currency'
  }

  get isMultiselect(){
    return this.type == 'multiselect'
  }

  get isText(){
    return !!this.type && !this.isCheckbox && !this.isTextArea
    && !this.isDropdown && !this.isTel && !this.isFile && !this.isSwitch
    && !this.isDateRange && !this.isCurrency && !this.isDate && !this.isMultiselect
    && !this.isRadio && !this.isColor && !this.isEditor
  }

  get isColor(){
    return this.type == 'color'
  }
  get isRadio(){
    return this.type == 'radio'
  }

  get col(){
    if(this.field.col){
      return 'col-sm-' + this.field.col;
    }

    return 'col-sm'
  }

  get isInvalid(){
    return this.field.valid === false
  }

  files:File[] = []
  get fileRestrictions(){
    if(this.field.fileRestrictions){
      return this.field.fileRestrictions
    }

    return {} as FileRestrictions
  }

  get isDate(){
    return this.type == "date"
  }


  range:any = {
    start: new Date(),
    end: null
  }

  clickCurrency(event:any){
    if(event.target){
      event.target.select()
    }
  }

  get noInline(){
    if(this.type == 'checkbox'){
      return this.field.noInlineCheckbox?'no-inline-checkbox':''
    }

    return ''
    
  }

  t = (text:string)=>{
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }
  get fileTip(){
    if(this.type == 'file' && this.field.fileRestrictions){
      const tips = []
      if(this.field.fileRestrictions.allowedExtensions){
        tips.push(`${this.t('AllowedExtensions')}: ${this.field.fileRestrictions.allowedExtensions.join(', ')}`)
      }

      if(this.field.fileRestrictions.maxFileSize){

        const kb = this.field.fileRestrictions.maxFileSize/ (1024);
        tips.push(`${this.t('MaxFileSize')}: ${kb.toFixed(0)}kB`)
      }
      if(this.field.fileRestrictions.minFileSize){

        const kb = this.field.fileRestrictions.minFileSize/ (1024);
        tips.push(`${this.t('MinFileSize')}: ${kb.toFixed(0)}kB`)
      }

      return !tips.length?'':`(${tips.join(', ')})`
    }
    return ''
  }

  get dateStartDisabled(){
    if(this.disabled !== true){
      return this.disabled && this.disabled.start
    }
    return undefined
  }
  get dateEndDisabled(){
    if(this.disabled !== true){
      return this.disabled && this.disabled.end
    }
    return undefined
  }
}
