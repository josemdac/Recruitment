import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CancelEvent, EditEvent, GridComponent, RemoveEvent, SaveEvent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs';
import { ExpandTabComponent } from 'src/app/components/expand-tab-layout/expand-tab/expand-tab.component';
import { validateFields } from 'src/app/components/form-input/form-input.helpers';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { StandardModalService } from 'src/app/layout/standard-modal/standard-modal.service';
import { getMimeType } from 'src/app/shared/helpers/filte-type.helper';
import { dataURLtoFile, parseDbComment, toBase64 } from 'src/app/shared/helpers/tools';
import { HrRcrtUserDocumentListItemDTO } from 'src/app/shared/models/HrRcrtUserDocument.model';
import { createBlankEmpHist } from 'src/app/shared/models/HrRcrtUserEmploymentHist.model';
import { MyAccountService } from '../my-account.service';


const docToFile = (doc:HrRcrtUserDocumentListItemDTO)=>{
  const mime = getMimeType(doc.documentName)

  const uri = `data:${mime};base64,${doc.document}`
  return dataURLtoFile(uri, doc.documentName)
}


@Component({
  selector: 'app-user-docs',
  templateUrl: './user-docs.component.html',
  styleUrls: ['./user-docs.component.scss']
})
export class UserDocsComponent implements OnInit {

  constructor(private myaccount:MyAccountService, private stModal:StandardModalService, private translate: TranslateService) {
    this.t =this.t.bind(this)
   }

  t(text: string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }
  docs:HrRcrtUserDocumentListItemDTO[] = []
  ngOnInit(): void {
    this.myaccount.loadUserDocs().subscribe(docs=>{
      this.docs = docs.map(d=>({...d, ['docFile']: { name: d.documentName, size: 100}}))
    })

    this.myaccount.saveAccount.subscribe(()=>{

        const valid = validateFields(this.fields, this.formData)
        //console.log(valid, this.formData)
        if(valid && this.showForm){

          
          this.myaccount.saveDoc(this.formData).subscribe((doc)=>{
            if(doc){
              this.myaccount.loadUserDocs().subscribe(docs=>{
                this.docs = docs.map(d=>({...d, ['docFile']: { name: d.documentName, size: 100}}))
              })
              this.formData = { documentTitle: '', status: 'A'}
              this.fields.forEach(f=>f.valid = true)
            }
          })
        }
    })
  }


  downloadDoc(dataItem: HrRcrtUserDocumentListItemDTO){
    this.myaccount.getUserDoc(dataItem.documentId).subscribe(doc=>{
      if(!doc.document || !doc.documentName){
        return
      }
      const mime = getMimeType(doc.documentName)

      const uri = `data:${mime};base64,${doc.document}`
      const link = document.createElement('a')
      link.download = doc.documentName
      link.href = uri
      link.target = 'blank'
      link.click()
    })
  }


  showForm = false
  formData: any= { documentTitle: '', status: 'A'}
  

  fields:IFormInput[]= [
    { name: 'documentTitle', col: '', type: 'text', maxlength: 100, required: true},
    { name: 'documentType', col:'', type:'dropdown', source: [
      ...parseDbComment('R=Resume, C=Cover Letter, X=References, O=Other Documents, A=Awards, T=Certificates')
      .map(t=>({ value: t.id, text: this.t(t.description)}))
    ], required: true }, 
    { name: 'docFile', col:'', type:'file', 
    fileRestrictions: {
      allowedExtensions: [ 'pdf' ],
      maxFileSize: 1024*1024
    },
    fileNameField: 'documentName' },
   
    
  ]

  changeHandler(value: any,field:IFormInput,  model:any){

    //console.log(value)
    if(field.type == 'file' && value){
      model[field.name] = value
      if(field.fileNameField){
        model[field.fileNameField] = value?value.name:undefined
      }
    }

    if(field.name == 'docFile'){
      if(!value){
        this.formData['logoImage'] = ''
        return
      }
      let ext = value.name?value.name.split('.').pop():undefined;
      this.formData['documentFormat'] =  ext?ext.toUpperCase():''
      toBase64(value).then((val)=>{
        this.formData['document'] = (val+'').split('base64,')[1]
      })
    }

  }

  /***Edit */
  public editHandler({ sender, rowIndex, dataItem }: EditEvent): void {
    this.closeEditor(sender);


    this.myaccount.getUserDoc(dataItem.documentId).subscribe(doc=>{
      this.formData =  { ...doc, ['docFile']: docToFile(doc) }
    })
    this.formData = {...dataItem}
    this.showForm = true
    //sender.editRow(rowIndex);
  }

  public cancelHandler({ sender, rowIndex }: CancelEvent): void {
    this.closeEditor(sender, rowIndex);
  }

  addNew(event:any,doctab:ExpandTabComponent){
    event.stopImmediatePropagation()
    event.preventDefault()

    //console.log(event)
    doctab.isActive = true
    this.formData = { documentTitle: '', status: 'A' }
    this.showForm = true
  }

  get isNew(){
    return !!this.formData.documentId
  }
  
  closeForm(event:any){ 
    event.preventDefault()
    this.showForm = false
    this.formData = { documentTitle: '', status: 'A' }
  }
  public removeHandler(dataItem:any, tpls:any ): void {
    this.toDelete = dataItem
    this.stModal.deletePrompt({
      deleteAction: ()=>new Observable<any>((obs)=>{
        this.myaccount.deleteDoc(dataItem.documentId).subscribe(()=>{
          this.myaccount.loadUserDocs().subscribe(docs=>{
            this.docs = docs
            this.formData = { documentTitle: '', status: 'A' }
            this.showForm = false
            obs.next({})
          })
        }) 
      }),
      errorAction: ()=>{ this.cancelDelete() },
      translate: this.t
    })

  }

  toDelete: any
  confirmedDelete(){
    
  }

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
