import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { validateFields } from 'src/app/components/form-input/form-input.helpers';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { CoSecurityDefinitionDTO } from 'src/app/shared/models/CoSecurityDefinition.model';
import { MyAccountService } from '../my-account.service';
import { PassValidationStepperComponent } from './pass-validation-stepper/pass-validation-stepper.component';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  @ViewChild('reqs') reqs?: ElementRef<HTMLDivElement>
  @ViewChild('stepper') stepper?: PassValidationStepperComponent
  constructor(public translate: TranslateService, 
    private myaccount: MyAccountService) { }

  t(text:string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }

  ngOnInit(): void {
   this.touched = false
   this.passreq = false
   this.loadSecurityDef()
   this.formData.userName = this.myaccount.currentUser.value['userName']
   this.myaccount.currentUser.subscribe(user=>{
     this.formData.userName = user['userName']
   })
  }


  ngAfterViewInit(){
    if(this.reqs){
      this.parseDef(this.reqs?.nativeElement)
    }
    
  }

  savePassword(event:any){
    event.preventDefault()
    if(this.stepper && this.stepper.validatePassword() && this.canSave){
      this.myaccount.changePassword(this.formData) 
    }
  }

  loadSecurityDef(){
    this.myaccount.getSecDef().subscribe(def=>{
      this.sdef = def
      if(this.reqs){
        this.parseDef(this.reqs?.nativeElement)
      }
    })
  }

  sdef?:CoSecurityDefinitionDTO
 
  

  formData:any = {
    oldPassword: '',
    newPassword: '',
    confirmPassword: ''
  }
  fields:IFormInput[] = [
    {
      name: 'oldPassword', type: 'password', col: 3, maxlength: 20, required: true
    }, 
    {
      name: 'newPassword', type: 'password', col: 3, maxlength: 25, required: true
    },
    {
      name: 'confirmPassword', type: 'password', col: 3, maxlength: 25, required: true
    },
    

    
  ]

  get f1(){
    return this.fields.slice(0, 1)
  }
  get f2(){
    return this.fields.slice(1, 2)
  }
  get f3(){
    return this.fields.slice(2, this.fields.length)
  }


  keyUpPass(event:any, name:string){
    if(name == 'newPassword'){
      this.formData['newPassword'] = event.target.value

    }
  }

  passreq = false
  touched = false
  get reqMessage(){
    return this.touched && !this.passreq
  }
  changeHandler(value:string, name:string){
    switch(name){
      case 'confirmPassword':
        this.fields[2].valid = this.formData.confirmPassword == this.formData.newPassword
        break
      case 'newPassword':
        this.touched = true
        const v = this.stepper?.validatePassword();
        //console.log(v)
        this.fields[2].valid = (this.formData.confirmPassword == this.formData.newPassword)
        && v?.valid

        this.passreq = v?.valid
        break
      case 'oldPassword':
        this.myaccount.validatePassword(value).subscribe(r=>{
          this.fields[0].valid = r.valid as any
        })
        break;
    }
  }

  get canSave(){
    return this.formData.confirmPassword && this.formData.oldPassword && this.formData.newPassword && !this.fields.some(f=>f.valid === false)
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
