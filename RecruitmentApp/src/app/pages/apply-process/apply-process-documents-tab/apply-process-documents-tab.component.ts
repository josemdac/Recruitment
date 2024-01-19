import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CancelEvent, EditEvent, GridComponent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs';
import { ExpandTabComponent } from 'src/app/components/expand-tab-layout/expand-tab/expand-tab.component';
import { validateFields } from 'src/app/components/form-input/form-input.helpers';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { StandardModalService } from 'src/app/layout/standard-modal/standard-modal.service';
import { getMimeType } from 'src/app/shared/helpers/filte-type.helper';
import { dataURLtoFile, parseDbComment, toBase64 } from 'src/app/shared/helpers/tools';
import { createBlankApplicantDoc, HrRcrtApplicantDocumentListItemDTO } from 'src/app/shared/models/HrRcrtApplicantDocument.model';
import { createBlankUserDoc, HrRcrtUserDocumentListItemDTO } from 'src/app/shared/models/HrRcrtUserDocument.model';
import { MyAccountService } from '../../my-account/my-account.service';
import { ApplyProcessService } from '../apply-process.service';





@Component({
  selector: 'app-apply-process-documents-tab',
  templateUrl: './apply-process-documents-tab.component.html',
  styleUrls: ['./apply-process-documents-tab.component.scss']
})
export class ApplyProcessDocumentsTabComponent implements OnInit {

  
  constructor(private proc:ApplyProcessService, private myaccount: MyAccountService, private stModal:StandardModalService, private translate: TranslateService) {
    this.t =this.t.bind(this)
    this.validate = this.validate.bind(this)
   }

  t(text: string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }
  docs:HrRcrtUserDocumentListItemDTO[] = []
  ngOnInit(): void {
    this.reloadDocs()
  }

  docToFile = (doc:HrRcrtUserDocumentListItemDTO)=>{
    let docName = doc.documentName
    const mime = getMimeType(docName)
  
    const uri = `data:${mime};base64,${doc.document}`
    return dataURLtoFile(uri, docName)
  }
  

  validate(){
    const valid = this.docs.filter(d=>d.documentType == 'R').length
    if(!valid){
      this.stModal.showError({
        title: this.t('DOCUMENTS'),
        message: this.t('YOUMUSTINCLUDEATLEASTONERESUME')
      })
    }
    return valid
  }

  reloadDocs(){
    this.myaccount.loadUserDocs().subscribe(docs=>{
      this.docs = docs.map(d=>({...d}))
    })
  }

  saveDocument(event: any){

    event.preventDefault()
    //console.log(this.showForm, this.formData)
    const valid = validateFields(this.fields, this.formData)

    
    if(valid && this.showForm){

      
      this.myaccount.saveDoc(this.formData).subscribe((doc)=>{
        if(doc){
          this.reloadDocs()
          this.formData = createBlankUserDoc()
        }
      })
    }
  }


  getDocName(doc:HrRcrtUserDocumentListItemDTO){
    // const dt = parseDbComment('R=Resume, C=Cover Letter, X=References, O=Other Documents, A=Awards, T=Certificates');
    // dt.forEach(t=>{ if(t.id == 'O'){ t.description = 'Other'}});
    
    // const name = dt.find(d=>d.id == doc.documentType)?.description + '.pdf'
    // return name
    return doc.documentName
  }


  downloadDoc(dataItem: HrRcrtUserDocumentListItemDTO){
    this.myaccount.getUserDoc(dataItem.documentId).subscribe(doc=>{
      if(!doc.document){
        return
      }


      const mime = getMimeType(this.getDocName(doc))

      const uri = `data:${mime};base64,${doc.document}`
      const link = document.createElement('a')
      link.download = this.getDocName(doc)
      link.href = uri
      link.target = 'blank'
      link.click()
    })
  }


  showForm = false
  formData: any= createBlankUserDoc()
  

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

      if(field.fileNameField){
        model[field.fileNameField] = value.name
      }
    }

    if(field.name == 'docFile'){
      let ext = value.name?value.name.split('.').pop():undefined;
      this.formData['documentFormat'] =  ext?ext.toUpperCase():''
      toBase64(value).then((val)=>{
        this.formData['document'] = (val+'').split('base64,')[1]
      })
    }

    if(field.name == 'documentTitle' && value){
      field.valid = true
    }

    if(field.name == 'documentType' && value){
      field.valid = true
    }

  }

  // /***Edit */
  // public editHandler({ sender, rowIndex, dataItem }: EditEvent): void {
  //   this.closeEditor(sender);


  //   if(dataItem.hasFile){
  //     this.myaccount.getUserDoc(dataItem.documentId).subscribe(doc=>{
  //       const documentName = this.getDocName(doc)
  //       this.formData =  { ...doc, ['docFile']: this.docToFile(doc), documentName }
  //     })
  //   }
    
  //   this.formData = {...dataItem}
  //   this.showForm = true
  //   //sender.editRow(rowIndex);
  // }

  // public cancelHandler({ sender, rowIndex }: CancelEvent): void {
  //   this.closeEditor(sender, rowIndex);
  // }

  addNew(event:any){
    event.stopImmediatePropagation()
    event.preventDefault()

    
    this.formData = createBlankUserDoc()
    this.showForm = true
  }

  get isNew(){
    return !!this.formData.documentId
  }

  toDelete:any
  public removeHandler(dataItem:any ): void {
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

  cancelDelete(){
    this.toDelete = null
    this.stModal.hide()
  }
  closeForm(event:any){ 
    event.preventDefault()
    this.showForm = false
    this.formData = { documentTitle: '', status: 'A' }
  }
  private closeEditor(
    grid: GridComponent,
    rowIndex = 0
  ): void {
    grid.closeRow(rowIndex);
   // this.editData = {}
  }
}
