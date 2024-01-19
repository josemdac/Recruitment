import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CancelEvent, EditEvent, GridComponent } from '@progress/kendo-angular-grid';
import { BehaviorSubject, Observable } from 'rxjs';
import { ExpandTabComponent } from 'src/app/components/expand-tab-layout/expand-tab/expand-tab.component';
import { validateFields } from 'src/app/components/form-input/form-input.helpers';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { StandardModalService } from 'src/app/layout/standard-modal/standard-modal.service';
import { parseDbComment } from 'src/app/shared/helpers/tools';
import { createBlankHrLang, HrRcrtUserLanguageListItemDTO } from 'src/app/shared/models/HrRcrtUserLanguages';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';
import { MyAccountService } from '../my-account.service';

@Component({
  selector: 'app-user-languages',
  templateUrl: './user-languages.component.html',
  styleUrls: ['./user-languages.component.scss']
})
export class UserLanguagesComponent implements OnInit {

 
  
  constructor(private myaccount:MyAccountService, private jsonc:JsonConfigService,
    private stModal:StandardModalService, private translate: TranslateService) {
    this.t =this.t.bind(this)
   }

  t(text: string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }
  userlangs:HrRcrtUserLanguageListItemDTO[] = []

  reloadList(){
    this.myaccount.loadHrLang().subscribe(items=>{
      this.userlangs = items.map(item=>({...item}))
      this.loadedList = true
      this.formData = createBlankHrLang()
      const userLangIds = this.userlangs.map(l=>l.languageId)
      const filtered = this.langs.value.filter(l=>!userLangIds.includes(l.value))
      this.fields[0] = {...this.fields[0], source: filtered }
    })
  }
  langs = new BehaviorSubject<{text: string, value: any}[]>([])
  loadedLangs = false
  loadedList = false
  loadLangs(){
    this.jsonc.get<{ value: any, text: string}[]>('SysLanguages/Dropdown').subscribe(l=>{
      this.langs.next(l)
      this.loadedLangs = true
    })
  }
  ngOnInit(): void {
    this.reloadList()
    this.loadLangs()

    this.myaccount.saveAccount.subscribe(()=>{

        const valid = validateFields(this.fields, this.formData)
        if(valid && this.showForm){

          
          this.myaccount.saveHrLang(this.formData).subscribe((item)=>{
            if(item){
              this.reloadList()
              
              
            }
          })
        }
    })

    const sub = this.langs.subscribe(l=>{
      this.fields[0].source = this.processLangs(l)
    })

    this.ngOnDestroy = ()=>sub.unsubscribe()
  }

  ngOnDestroy(){}

  showForm = false
  formData: any= createBlankHrLang()

  processLangs(l:any[]){
    return l
  }
  fields:IFormInput[]= [
    {
      name: 'languageId', type: 'dropdown', col: 3, source: []
    },
    {
      name: 'readingProficiency', type: 'dropdown', col: 3, source: []
    },
    {
      name: 'writingProficiency', type: 'dropdown', col: 3, source: []
    },
    {
      name: 'speakingProficiency', type: 'dropdown', col: 3, source: []
    },
  ]

  changeHandler(value: any,field:IFormInput,  model:any){
    if(field.type == 'daterange'){
      model[field.startName+''] = value.start
      model[field.endName+''] = value.end

      //console.log(value, model)
    }   

  }

  getLevels(){
    return parseDbComment('B=Beginner, F=Fluent, I=Intermediate').map(i=>({...i, text: this.t(i.description), value: i.id}))
  }

  get canAdd(){
    return this.loadedList && this.loadedLangs
  }

  get isNew(){
    return !!this.formData.educationId
  }
  
  closeForm(event:any){ 
    event.preventDefault()
    this.showForm = false
    this.formData = createBlankHrLang()
  }

  /***Edit */
  public editHandler({ sender, rowIndex, dataItem }: EditEvent): void {
    this.closeEditor(sender);


    this.myaccount.getHrLang(dataItem.recordId).subscribe(item=>{
      this.formData =  { ...item }
    })

    const userLangIds = this.userlangs.map(l=>l.languageId)
    const filtered = this.langs.value.filter(l=>l.value == dataItem.languageId || !userLangIds.includes(l.value))
    this.fields[0] = {...this.fields[0], source: filtered }
    this.fields[1].source = this.getLevels()
    this.fields[2].source = this.getLevels()
    this.fields[3].source = this.getLevels()
    
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

    const userLangIds = this.userlangs.map(l=>l.languageId)
    const filtered = this.langs.value.filter(l=>!userLangIds.includes(l.value))
    this.fields[0] = {...this.fields[0], source: filtered }
    this.fields[1].source = this.getLevels()
    this.fields[2].source = this.getLevels()
    this.fields[3].source = this.getLevels()
    doctab.isActive = true
    this.formData = createBlankHrLang()
    this.showForm = true
  }



  public removeHandler(dataItem:any): void {
    this.toDelete = dataItem
    this.stModal.deletePrompt({
      deleteAction: ()=>new Observable<any>((obs)=>{
        this.myaccount.deleteHrLang(dataItem.recordId).subscribe(()=>{
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
