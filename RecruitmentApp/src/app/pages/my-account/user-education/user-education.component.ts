import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { EditEvent, CancelEvent, GridComponent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs';
import { ExpandTabComponent } from 'src/app/components/expand-tab-layout/expand-tab/expand-tab.component';
import { validateFields } from 'src/app/components/form-input/form-input.helpers';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { StandardModalService } from 'src/app/layout/standard-modal/standard-modal.service';
import { HrRcrtUserEducationListItemDTO, createBlankHrEduc } from 'src/app/shared/models/HrRcrtUserEducation.model';
import { MyAccountService } from '../my-account.service';

@Component({
  selector: 'app-user-education',
  templateUrl: './user-education.component.html',
  styleUrls: ['./user-education.component.scss']
})
export class UserEducationComponent implements OnInit {


  
  constructor(private myaccount:MyAccountService, private stModal:StandardModalService, private translate: TranslateService) {
    this.t =this.t.bind(this)
   }

  t(text: string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }
  histItems:HrRcrtUserEducationListItemDTO[] = []

  reloadList(){
    this.myaccount.loadHrEduc().subscribe(items=>{
      this.histItems = items.map(item=>({...item}))
    })
  }
  ngOnInit(): void {
    this.reloadList()

    this.myaccount.saveAccount.subscribe(()=>{

        const valid = validateFields(this.fields, this.formData)
        if(valid && this.showForm){

          
          this.myaccount.saveHrEduc(this.formData).subscribe((item)=>{
            if(item){
              this.reloadList()
              this.formData = createBlankHrEduc()
            }
          })
        }
    })
  }


  showForm = false
  formData: any= createBlankHrEduc()
  

  fields:IFormInput[]= [
    { name: 'schoolName', col: 3, type: 'text', maxlength: 60, required: true},
    {
      name: 'degreeId', type: 'dropdown', col: 3, source: 'HrEducationDegree'
    },
    { name: 'graduatedYear', col: 3, type: 'number', min: 0},
    { name: 'yearsCompleted', col: 3, type: 'number', min: 0},
    
    { name: 'major', col: 3, type: 'text', maxlength: 32100},
    
    { name: 'gpa', col: 3, type: 'number', min: 0},
    {
      name: 'countryId', type: 'dropdown', col: 3, source: 'SysCountryMaster'
    },
    {
      name: 'graduated', type: 'checkbox', checkBoxValues: [
        { trueValue: 'Y'}, { falseValue: 'N'}
      ], col: 3, noInlineCheckbox: true
    },
    { name: 'comments', col: 12, type: 'textarea', maxlength: 4000},


    
    
  ]

  changeHandler(value: any,field:IFormInput,  model:any){
    if(field.type == 'daterange'){
      model[field.startName+''] = value.start
      model[field.endName+''] = value.end

      //console.log(value, model)
    }   

  }

  get isNew(){
    return !!this.formData.educationId
  }
  
  closeForm(event:any){ 
    event.preventDefault()
    this.showForm = false
    this.formData = createBlankHrEduc()
  }

  /***Edit */
  public editHandler({ sender, rowIndex, dataItem }: EditEvent): void {
    this.closeEditor(sender);


    this.myaccount.getHrEduc(dataItem.educationId).subscribe(item=>{
      this.formData =  { ...item }
    })
    this.formData = {...dataItem }
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
    this.formData = createBlankHrEduc()
    this.showForm = true
  }

  public removeHandler(dataItem:any): void {
    this.toDelete = dataItem
    this.stModal.deletePrompt({
      deleteAction: ()=>new Observable<any>((obs)=>{
        this.myaccount.deleteHrEduc(dataItem.educationId).subscribe(()=>{
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
