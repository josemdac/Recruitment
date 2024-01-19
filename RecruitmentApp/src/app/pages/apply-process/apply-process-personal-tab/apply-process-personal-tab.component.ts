import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { createBlankApplicant } from 'src/app/shared/models/HrRcrtApplicant.model';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';
import { MyAccountService } from '../../my-account/my-account.service';
import { ApplyProcessService } from '../apply-process.service';
interface IFormAv extends IFormInput{
  av?: boolean,
  dayIdx?: number
}

@Component({
  selector: 'app-apply-process-personal-tab',
  templateUrl: './apply-process-personal-tab.component.html',
  styleUrls: ['./apply-process-personal-tab.component.scss']
})
export class ApplyProcessPersonalTabComponent implements OnInit {


  constructor(public proc:ApplyProcessService, private translate: TranslateService, 
    private route: ActivatedRoute, private myaccount:MyAccountService, private webConf:WebSiteInfoService) { 
      this.validate = this.validate.bind(this)
    }


  subs: Subscription[] = []
  ngOnInit(): void {
    
    this.addAvilability()
    
    this.setLabels()
  }

  setLabels(){

    const interval = setInterval(()=>{

      const labels:any = {
        telephone: this.translate.translations[this.translate.defaultLang]['AlternateTelephone']
      }
  
      this.fields.forEach((f,i)=>{
        if(labels[f.name]){ 
          this.fields[i].label = labels[f.name]
        }
      })
      //console.log(this.translate.translations, labels)
      if(this.translate.translations[this.translate.defaultLang]){
        clearInterval(interval)
      }

    }, 100)
  }

  ngOnDestroy(){
    this.subs.forEach(s=>s.unsubscribe())
    //console.log(this.proc.formData, 'Unsub Personal')
  }


  get f1(){
    return this.fields.slice(0, 5)
  }

  get postalAddr(){
    return this.fields.slice(5, 10)
  }
  get physicAddr(){
    return this.fields.slice(10, 15)
  }

  get f2(){
    
    if(this.hasSocialNetworks){
      return this.fields.slice(15, 18)
    }

    return this.fields.slice(15,18)
  }

  get f3(){
    if(this.hasSocialNetworks){
      return this.fields.slice(18, this.fields.length)
    }

    return this.fields.slice(18, this.fields.length).filter(f=>!f.name.includes('socialNetwork'))
  }

  get locale(){
    if(this.translate.defaultLang == 'English'){
      return 'en'
    }else{
      return 'en'
    }
  }

  get hasSocialNetworks(){
    return this.webConf.conf.value.requestSocialNetworks == 'Y'
  }

  dp = new DatePipe(this.locale)
  fields:IFormAv[] = [
    
    {
      name: 'firstName', type: 'text', col: 3, maxlength: 20, required: true
    },
    {
      name: 'middleName', type: 'text', col: 1, maxlength: 1
    },
    {
      name: '', type: '', col: 2
    },
    {
      name: 'lastName1', type: 'text', col: 3, maxlength: 25, required: true
    },
    {
      name: 'lastName2', type: 'text', col: 3, maxlength: 25, 
    },
    

    //Postal address
    {
      name: 'address1', type: 'text', col: 3, maxlength: 32, required: true
    },
    {
      name: 'address2', type: 'text', col: 3, maxlength: 32, 
    },
    {
      name: 'city', type: 'text', col: 3, maxlength: 15, required: true
    },
    {
      name: 'state', noSort: true, type: 'dropdown', col: '', source: 'SysStatesMaster', required: true
    },
    {
      name: 'zipcode', type: 'text', col: '', required: true
    },

    //Physical Address
    {
      name: 'address1Street', type: 'text', col: 3, maxlength: 32, required: true
    },
    {
      name: 'address2Street', type: 'text', col: 3, maxlength: 32, 
    },
    {
      name: 'cityStreet', type: 'text', col: 3, maxlength: 15, required: true
    },
    {
      name: 'stateStreet', noSort: true, type: 'dropdown', col: '', source: 'SysStatesMaster', required: true
    },
    {
      name: 'zipcodeStreet', type: 'text', col: '', required: true
    },

    //Telephones
    {
      name: 'mobileTelephone', type: 'tel', col: 3, maxlength: 20, required: true
    },
    {
      name: 'telephone', type: 'tel', col: 3, maxlength: 20
    },
    { name: '', col: 6, type: ''},

    

    //Social networks
    {
      name: 'socialNetworkAddress1', type: 'text', col: 3, maxlength: 100, 
    },
    {
      name: 'socialNetworkAddress2', type: 'text', col: 3, maxlength: 100, 
    },
    {
      name: 'socialNetworkAddress3', type: 'text', col: 3, maxlength: 100, 
    }
  ]

  get mobile(){
    return [
      ...this.sunday,
      ...this.monday,
      ...this.tuesday,
      ...this.wednesday,
      ...this.thursday,
      ...this.friday,
      ...this.saturday,
      ...this.fields.slice(this.fields.length-3, this.fields.length).filter(f=>{
        return this.hasSocialNetworks
      })
    ]
  }

  get monday(){
    return this.fields.filter(f=>f.dayIdx == 1).slice(0,3)
  }
  get tuesday(){
    return this.fields.filter(f=>f.dayIdx == 2).slice(0,3)
  }
  get wednesday(){
    return this.fields.filter(f=>f.dayIdx == 3).slice(0,3)
  }
  get thursday(){
    return this.fields.filter(f=>f.dayIdx == 4).slice(0,3)
  }
  get friday(){
    return this.fields.filter(f=>f.dayIdx == 5).slice(0,3)
  }
  get saturday(){
    return this.fields.filter(f=>f.dayIdx == 6).slice(0,3)
  }
  get sunday(){
    return this.fields.filter(f=>f.dayIdx == 0).slice(0,3)
  }

  addAvilability(){
    //Availability
    const fieldPairs:IFormAv[] = []; 
    const days = [1,2,3,4,5,6, 0].map((d, i, arr)=>{
      let date = new Date();
      let wd= date.getDay();
      let nd= date.getDate() - wd + d
      date.setDate(nd);
      let dayName = this.dp.transform(date, 'EEEE', undefined, this.locale)

      if(!dayName){ return {name: '', col: '0', type: ''}}

      const f: any = { dayIdx: d, av: true, name: dayName.toLowerCase(), type: '', col: ''};
      const b = { av: true, name: '', type: '', col: 'auto' }
      const a: any = { dayIdx: d, av: true, name: dayName.toLowerCase() + 'Am', type: 'checkbox', checkBoxValues: [{trueValue: 'Y'}, {falseValue: 'N'}]};
      const p: any = { dayIdx: d, av: true, name: dayName.toLowerCase() + 'Pm', type: 'checkbox', checkBoxValues: [{trueValue: 'Y'}, {falseValue: 'N'}]};
      const b2 = { dayIdx: d, av: true, name: '', type: '', col: '' }

      return [f, b, a, p, b2]
    })
    

    days.forEach((p:any)=>{ fieldPairs.push(p[0]); fieldPairs.push(p[1])})
    fieldPairs.push({ name: '', type: '', col: 12, av: true })
    days.forEach((p:any)=>{ fieldPairs.push(p[2]); fieldPairs.push(p[3]); fieldPairs.push(p[4])})
    fieldPairs.push({ name: '', type: '', col: 12, av: true })

    this.fields = [ ...this.fields.slice(0,this.fields.length - 3), 
     // { name: '', col: 12, type: ''},
      ...fieldPairs, 
      ...this.fields.slice(this.fields.length - 3, this.fields.length) ]
  }

  validDays = true  
  invalidFields:any[] = []
  validate(){

    this.invalidFields = this.fields
       .filter(f=>{

         let valid = f.required && !!this.proc.formData[f.name]
         
         f.valid = valid
         return !valid
        }).map(f=>f.name)
    
    this.validDays = [
      ...this.monday, ...this.tuesday,
      ...this.wednesday, ...this.thursday, 
      ...this.friday, ...this.saturday, 
      ...this.sunday
    ].some(f=>this.proc.formData[f.name] == 'Y')
    const requiredMissing = !this.fields.filter(f=>f.required)
    .some(f=>!this.proc.formData[f.name])
    //console.log('Executing Validation', 'Valid Days: ', this.validDays, ' Requried Missing: ', requiredMissing)
    return  requiredMissing &&
    this.validDays
    
  }
  changeHandler(value:any, field:IFormInput){
    //console.log(value, field.name)
    this.proc.formData[field.name] = value
  }

  dayName(d:number){
    let date = new Date();
    let wd= date.getDay();
    let nd= date.getDate() - wd + d
    date.setDate(nd);
    let dayName = this.dp.transform(date, 'EEEE', undefined, this.locale)
    return dayName;
  }

  get allChecked(){
    return ![1,2,3,4,5,6, 0].some(d=>{
      let name = this.dayName(d)?.toLowerCase();
      return !(this.proc.formData[name+'Am']=='Y') || !(this.proc.formData[name+'Pm'] == 'Y')
    })
  }

  get partialChecked(){
    return [1,2,3,4,5,6, 0].some(d=>{
      let name = this.dayName(d)?.toLowerCase();
      return (this.proc.formData[name+'Am']=='Y') || (this.proc.formData[name+'Pm']=='Y')
    }) && !this.allChecked
  }

  changeAll(event:any){
    const v = event.target.checked?'Y':'N';
    [1,2,3,4,5,6, 0].forEach(d=>{
      let name = this.dayName(d)?.toLowerCase();
      this.proc.formData[name+'Am']=v
      this.proc.formData[name+'Pm']=v
    })
  } 
  
  get msChecked(){
    return this.proc.formData['morningShift'] == 'Y'
  }
  changeMorningShift(event:any){
    //console.log(event)
    this.proc.formData['morningShift']= event.target.checked?'Y':'N'
  }

}
