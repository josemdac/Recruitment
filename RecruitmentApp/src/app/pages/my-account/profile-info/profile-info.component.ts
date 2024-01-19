import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TranslatePipe, TranslateService } from '@ngx-translate/core';
import { validateEmail, validateFields } from 'src/app/components/form-input/form-input.helpers';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { HrRcrtUserUpdateDTO } from 'src/app/shared/models/HrRcrtUser.model';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';
import { MyAccountService } from '../my-account.service';

@Component({
  selector: 'app-profile-info',
  templateUrl: './profile-info.component.html',
  styleUrls: ['./profile-info.component.scss']
})
export class ProfileInfoComponent implements OnInit {

  constructor(public translate: TranslateService, 
    public cdr:ChangeDetectorRef, private jsonConf:JsonConfigService,
    public webConf: WebSiteInfoService,
    private myaccount: MyAccountService) { }

  ngOnInit(): void {
    this.loadCurrent()
    this.myaccount.saveAccount.subscribe(()=>this.saveProfile())
    

  }

  saveProfile(){
    const valid = validateFields(this.fields, this.formData)
    if(valid){
      this.myaccount.saveProfile(this.formData)
      return
    }

    //console.log(this.formData['invalidFields'])
  }

 
  loadCurrent(){
    this.formData = this.myaccount.currentUser.value
    this.myaccount.currentUser.subscribe(user=>this.formData = user)
    this.formData = this.myaccount.currentUser.value
  }

  tp = new TranslatePipe(this.translate, this.cdr)


  changeHandler(event:any, field:IFormInput){
    switch(field.name){
      case 'userName':
      case 'email2':
        field.valid = validateEmail(field, this.formData)
        break;
    }

    this.formData[field.name] = event
  }
  formData:any = {}
  get hasSocialNetworks(){
    return this.webConf.conf.value.requestSocialNetworks == 'Y'
  }

  get f1(){
    return this.fields.slice(0,8)
  }

  get postalAddr(){
    return this.fields.slice(8, 13)
  }
  get physicAddr(){
    return this.fields.slice(13, 18)
  }

  get f2(){
    if(this.hasSocialNetworks){
      return this.fields.slice(18, this.fields.length)
    }

    return this.fields.slice(18, this.fields.length).filter(f=>!f.name.includes('socialNetwork'))
    
  }
  fields:IFormInput[] = [
    {
      name: 'userName', type: 'email', col: 3, maxlength: 100, required: true
    },
    {
      name: 'email2', label: this.translate.getParsedResult(this.translate.defaultLang, 'AlternateEmail'), type: 'email', col: 3, maxlength: 100, required: true
    },{ name: '', col: 6, type: '' },
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
      name: 'stateStreet', noSort: true, type: 'dropdown', col: '', source: 'SysStatesMaster',required: true
    },
    {
      name: 'zipcodeStreet', type: 'text', col: '', required: true
    },
    {
      name: 'mobileTelephone', type: 'tel', col: 3, maxlength: 20, required: true
    },
    {
      name: 'telephone', type: 'tel', col: 3, maxlength: 20, label: this.tp.transform('AlternateTelephone')
    },
    { name: '', col: 6, type: ''},
    {
      name: 'socialNetworkAddress1', type: 'text', col: 3, maxlength: 100, 
    },
    {
      name: 'socialNetworkAddress2', type: 'text', col: 3, maxlength: 100, 
    },
    {
      name: 'socialNetworkAddress3', type: 'text', col: 3, maxlength: 100, 
    },
    {
      name: 'comments', type: 'textarea', col: 9, maxlength: 500, 
    },
   
    {
      name: 'emailFormat', type: 'dropdown', source: [
        { value: 'P', text: this.tp.transform('PlainHtml')},
        { value: 'H', text: this.tp.transform('HtmlFormat')}

      ], col: '', maxlength: 15, 
    },
    {
      name: 'emailFrequency', type: 'dropdown', source: [
        { value: 'D', text: this.tp.transform('Daily')},
        { value: 'I', text: this.tp.transform('Immediately')},
        { value: 'W', text: this.tp.transform('Weekly')}

      ], col: '', maxlength: 15, 
    },
   // { name: '', col: 9, type: ''},
    
    {
      name: 'remainAnonymous', type: 'checkbox', checkBoxValues: [
        { trueValue: 'Y'}, { falseValue: 'N'}
      ], col: 3, maxlength: 20, 
    },
    {
      name: 'sendFutureInformation', type: 'checkbox', checkBoxValues: [
        { trueValue: 'Y'}, { falseValue: 'N'}
      ], col: 3, maxlength: 20, 
    },
    // {
    //   name: 'sendEmailMatching', type: 'checkbox', checkBoxValues: [
    //     { trueValue: 'Y'}, { falseValue: 'N'}
    //   ], col: 3, maxlength: 20, 
    // },

    
  ]

}
