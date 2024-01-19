import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CancelEvent, EditEvent, GridComponent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs';
import { ExpandTabComponent } from 'src/app/components/expand-tab-layout/expand-tab/expand-tab.component';
import { validateFields } from 'src/app/components/form-input/form-input.helpers';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { StandardModalService } from 'src/app/layout/standard-modal/standard-modal.service';
import { getMimeType } from 'src/app/shared/helpers/filte-type.helper';
import { parseDbComment, toBase64 } from 'src/app/shared/helpers/tools';
import { createBlankEmpHist, HrRcrtUserEmploymentHistListItemDTO } from 'src/app/shared/models/HrRcrtUserEmploymentHist.model';
import { MyAccountService } from '../my-account.service';

@Component({
  selector: 'app-employment-hist',
  templateUrl: './employment-hist.component.html',
  styleUrls: ['./employment-hist.component.scss']
})
export class EmploymentHistComponent implements OnInit {

  
  constructor(private myaccount:MyAccountService, private stModal:StandardModalService, private translate: TranslateService) {
    this.t =this.t.bind(this)
   }

  t(text: string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }
  histItems:HrRcrtUserEmploymentHistListItemDTO[] = []

  reloadList(){
    this.myaccount.loadEmpHist().subscribe(items=>{
      this.histItems = items.map(item=>({...item, dateRange: { 
        start: new Date(item.startDate as any),
        end: new Date(item.endDate as any),
      }}))
    })
  }
  ngOnInit(): void {
    this.reloadList()

    this.myaccount.saveAccount.subscribe(()=>{

        const valid = validateFields(this.fields, this.formData)
        if(valid && this.showForm){

          
          this.myaccount.saveEmpHist(this.formData).subscribe((item)=>{
            if(item){
              this.reloadList()
              this.formData = createBlankEmpHist()
            }
          })
        }
    })
  }


  showForm = false
  formData: any= createBlankEmpHist()
  

  fields:IFormInput[]= [
    { name: 'positionTitle', col: 3, type: 'text', maxlength: 50, required: true},
    
    { name: 'companyName', col: 3, type: 'text', maxlength: 60, required: true},
    { name: 'supervisorName', col: 3, type: 'text', maxlength: 50},{
      name: 'currentJob', type: 'checkbox', checkBoxValues: [
        { trueValue: 'Y'}, { falseValue: 'N'}
      ], col: 3,
    },
    {
      name: 'telephone', type: 'tel', col: 3, maxlength: 20, 
    },{ name: '', col: 9, type: ''},
    {
      name: 'startDate', type: 'date', col: 6
    },
    {
      name: 'endDate', type: 'date', col: 6
    },
    { name: 'jobDescription', col: 12, type: 'textarea', maxlength: 4000},
    { name: 'terminationReason', col: 12, type: 'textarea', maxlength: 4000},
    { name: 'comments', col: 12, type: 'textarea', maxlength: 4000},


    
    
  ]

  getDisabledField(field:IFormInput){
    if(field.name == 'endDate'){
      return (this.formData.currentJob == 'Y')
    }
    return false
  }

  changeHandler(value: any,field:IFormInput,  model:any){
    if(field.type == 'daterange'){
      model[field.startName+''] = value.start
      model[field.endName+''] = value.end

      //console.log(value, model)
    }   

  }

  get isNew(){
    return !!this.formData.employmentId
  }
  
  closeForm(event:any){ 
    event.preventDefault()
    this.showForm = false
    this.formData = createBlankEmpHist()
  }

  /***Edit */
  public editHandler({ sender, rowIndex, dataItem }: EditEvent): void {
    this.closeEditor(sender);


    this.myaccount.getEmpHIst(dataItem.employmentId).subscribe(item=>{
      this.formData =  { ...item, dateRange: { start: new Date(item.startDate+''), end: new Date(item.endDate+'')}}
    })
    this.formData = {...dataItem, dateRange: { start: new Date(dataItem.startDate+''), end: new Date(dataItem.endDate+'')}}
    this.showForm = true
    //sender.editRow(rowIndex);
  }

  public cancelHandler({ sender, rowIndex }: CancelEvent): void {
    this.closeEditor(sender, rowIndex);
  }

  addNew(event:any,doctab:ExpandTabComponent){
    event.stopImmediatePropagation()
    event.preventDefault()

    
    doctab.isActive = true
    this.formData = createBlankEmpHist()
    this.showForm = true
  }

  public removeHandler(dataItem:any): void {
    this.toDelete = dataItem
    this.stModal.deletePrompt({
      deleteAction: ()=>new Observable<any>((obs)=>{
        this.myaccount.deleteEmpHist(dataItem.employmentId).subscribe(()=>{
            this.reloadList()
            this.formData = { documentTitle: '', status: 'A' }
            this.showForm = false
            obs.next({})
          
        }) 
      }),
      errorAction: ()=>{ this.cancelDelete() },
      translate: this.t
    })

  }

  toDelete: any
 

  cancelDelete(){
    this.toDelete = null
    this.stModal.hide()
  }
  private closeEditor(
    grid: GridComponent,
    rowIndex = 0
  ): void {
    grid.closeRow(rowIndex);
   // this.editData = {}
  }

}
