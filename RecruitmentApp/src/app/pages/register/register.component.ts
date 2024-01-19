import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatePipe, TranslateService } from '@ngx-translate/core';
import { BehaviorSubject } from 'rxjs';
import { validateEmail, validateFields } from 'src/app/components/form-input/form-input.helpers';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { goToTop } from 'src/app/shared/helpers/tools';
import { CoSecurityDefinitionDTO } from 'src/app/shared/models/CoSecurityDefinition.model';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';
import { PassValidationStepperComponent } from '../my-account/change-password/pass-validation-stepper/pass-validation-stepper.component';
import { MyAccountService } from '../my-account/my-account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @ViewChild('reqs') reqs?: ElementRef<HTMLDivElement>
  @ViewChild('stepper') stepper?: PassValidationStepperComponent
  constructor(public translate: TranslateService, public route: ActivatedRoute,
    public cdr:ChangeDetectorRef, public jsonConf:JsonConfigService, public router: Router) { 
      
    }

  
  registered = false
  resent = false
  validatedUser = false

  
  t(text:string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }
  ngOnInit(): void {
    this.reset()
    this.loadSecurityDef()
    this.registered = false
    this.resent = false
    this.fields=  this.getFields()
   
    if(this.email){
      this.formData.userName = this.email
      this.formData.confirmEmail = this.email
      this.validateUserName(this.formData.userName).subscribe(({ valid})=>{
        this.fields[0].valid = valid
        this.validatedUser = true
      })
    }
    goToTop()

  }

  ngAfterViewChecked(){
    this.fields[0].label = this.t('EMAIL')
  }


  get email(){
    return this.route.snapshot.paramMap.get('email')
  }

  validateUserName(value: string){
    return this.jsonConf.get<{valid: boolean}>('HrRcrtUser/ValidateUser/'+value)
  }

  states = new BehaviorSubject<{ text: string, value: any}[]>([])
  resend(event: any){
    event.preventDefault()
    const valid = this.registered
    if(valid){
      //Save user
      let lang = this.translate.defaultLang == "English" ?"en":"es"
      this.jsonConf.post<{sent: boolean}>('HrRcrtUser/Resend?Lang='+lang, this.formData)
        .subscribe(({sent})=>{
          this.resent = sent
        })

      return
    }
  }
  saveUser(event: any){
    event.preventDefault()
    console.log(this.formData)
    const valid = validateFields(this.fields, this.formData)
    if(valid){
      //Save user
      let lang = this.translate.defaultLang == "English" ?"en":"es"
      this.jsonConf.post<{created: boolean}>('HrRcrtUser?Lang='+lang, this.formData)
        .subscribe(({created})=>{
          this.touched = false
          this.passreq = false
          this.registered = created
        })

      return
    }

    //console.log(this.formData['invalidFields'])
  }

 
 
  tp = new TranslatePipe(this.translate, this.cdr)


  changeHandler(event:any, field:IFormInput){
    field.valid = undefined
    switch(field.name){
      case 'userName':
        if(validateEmail(field, this.formData)){
          this.validateUserName(this.formData.userName).subscribe(({ valid})=>{
            field.valid = valid
            this.validatedUser = true
          })
        }else{
          field.valid = false
        }

        break;
      case 'confirmEmail':
        field.valid = validateEmail(field, this.formData) && this.formData.userName == this.formData.confirmEmail
        break;
    }
  }

  reset(){
    this.formData =  {
      password: '',
      confirmPassword: '',
      userName: '',
      confirmEmail: '',
      firstName: '',
      middleName: '',
      lastName1: '',
      lastName2: '',
      telephone: '',
      state: 'PR',
      remainAnonymous: 'N',
      sendFutureInformation: 'Y',
      //sendEmailMatching: 'Y'
  
  
    }
    this.touched = false
    this.passreq = false
  }
  formData:any = { }

  fields:IFormInput[] = []
  getFields: any = ()=> [
    {
      name: 'userName', type: 'email', col: 3, maxlength: 100, required: true
    },
    {
      name: 'confirmEmail', type: 'email', col: 3, maxlength: 100, required: true
    },
    {
      name: 'firstName', type: 'text', col: 10, maxlength: 20, required: true
    },
    {
      name: 'middleName', type: 'text', col: 2, maxlength: 1
    },
    {
      name: 'lastName1', type: 'text', col: 6, maxlength: 25, required: true
    },
    {
      name: 'lastName2', type: 'text', col: 6, maxlength: 25
    },
    {
      name: 'mobileTelephone', type: 'tel', col: 12, maxlength: 20, required: true
    },
    {
      name: 'state',  noSort: true, type: 'dropdown', col: '', source: 'SysStatesMaster'
    },
    {
      name: 'remainAnonymous', type: 'checkbox', checkBoxValues: [
        { trueValue: 'Y'}, { falseValue: 'N'}
      ], col: 12, maxlength: 20, 
    },
    {
      name: 'sendFutureInformation', type: 'checkbox', checkBoxValues: [
        { trueValue: 'Y'}, { falseValue: 'N'}
      ], col: 12, maxlength: 20, 
    },
    // {
    //   name: 'sendEmailMatching', type: 'checkbox', checkBoxValues: [
    //     { trueValue: 'Y'}, { falseValue: 'N'}
    //   ], col: 12, maxlength: 20, 
    // },

    
  ]

  get fU1(){
    return this.fields.slice(0, 1)
  }
  get fU11(){
    return this.fields.slice(1, 2)
  }
  get fU2(){
    return this.fields.slice(2, this.fields.length)
  }



  //Password
  
  ngAfterViewInit(){
    if(this.reqs){
      this.parseDef(this.reqs?.nativeElement)
    }
    
  }

  loadSecurityDef(){
    this.jsonConf.get<CoSecurityDefinitionDTO>('CoSecurityDefinition/Current').subscribe(def=>{
      this.sdef = def
      if(this.reqs){
        this.parseDef(this.reqs?.nativeElement)
      }
    })
  }

  sdef?:CoSecurityDefinitionDTO
 
  

  passFields:IFormInput[] = [
    {
      name: 'password', type: 'password', col: 3, maxlength: 25, required: true
    },
    {
      name: 'confirmPassword', type: 'password', col: 3, maxlength: 25, required: true
    },

    
  ]

  

  get f1(){
    return this.passFields.slice(0, 1)
  }
  get f2(){
    return this.passFields.slice(1, 2)
  }


  keyUpPass(event:any, name:string){
    if(name == 'password'){
      this.formData['password'] = event.target.value

    }
  }
 
  touched = false
  passreq = false
  get passreqMsg(){
    return !this.passreq && this.touched
  }

  changePassHandler(value:string, name:string){
    const v = this.stepper?.validatePassword()
    this.touched = true
    this.passreq = v?.valid
    switch(name){
      case 'confirmPassword':
        this.passFields[1].valid = this.formData.confirmPassword == this.formData.password &&
        v?.valid
        break
      case 'password':
        this.passFields[1].valid = this.formData.confirmPassword == this.formData.password &&
        v?.valid
        break
    }
  }

  get canSave(){
    return this.formData.confirmPassword && this.formData.password && !this.passFields.some(f=>f.valid === false)
    && this.formData.userName && this.formData.confirmEmail && !this.fields.some(f=>(f.valid == false))
  }


  parseDef(element:HTMLDivElement){

    if(this.sdef){
      Array.from(element.children).forEach(c=>{ element.removeChild(c), c.remove()})
      const {
        minLowerCharacters,
        passwordCanBeTheUsername,
        passwordLength,
        canRepeatePassword,
        minNumericCharacters,
        minSymbolCharacters,
        minUpperCharacters
      } = this.sdef
      const rep = (text:string, glue: string, val:string )=>text.split(glue).join(val)

      const reqs = [
      rep(this.t('PasswordRequirement1'), '{passwordLength}', this.sdef.passwordLength+''),
      minLowerCharacters?rep(this.t('PasswordRequirement2'), '{minLowerCharacters}',  minLowerCharacters+''):'',
      minUpperCharacters?rep(this.t('PasswordRequirement3'), '{minUpperCharacters}',  minUpperCharacters+''):'',
      minNumericCharacters?rep(this.t('PasswordRequirement4'), '{minNumericCharacters}',  minNumericCharacters+''):'',
      minSymbolCharacters?rep(this.t('PasswordRequirement5'), '{minSymbolCharacters}',  minSymbolCharacters+''):'',
      rep(this.t('PasswordRequirement6'), '{not}',  canRepeatePassword=='Y'?'':this.t('not')),
      rep(this.t('PasswordRequirement7'), '{not}',  passwordCanBeTheUsername=='Y'?'':this.t('not')),
      ]
      const wr =document.createElement('div')

      reqs.forEach((r, i)=>{
        if(r){
          const span = document.createElement('span')
          span.className = "mr-1"
          span.id = 'req-' + i
          span.textContent = r
          wr.append(span)
        }       
      })

      wr.classList.add('passreqs')
      element.append(wr)     

    }
  }


}
