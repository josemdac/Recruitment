import { HttpHeaders } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { RegisterComponent } from 'src/app/pages/register/register.component';
import { validateEmail } from '../../form-input/form-input.helpers';
import { IFormInput } from '../../form-input/form-input.model';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent extends RegisterComponent {

  @Input() state = 'email'
  @Output() back = new EventEmitter()
  @Output() close = new EventEmitter()


  

  backClick(ev:MouseEvent){
    ev.preventDefault()
    ev.stopPropagation()
    this.back.emit()
  }

  override changeHandler(event:any, field:IFormInput){
    //console.log(event, field)
    if(field.name == 'userName'){
      field.valid = validateEmail(field, this.formData)
    }

  }

  closeClick(ev:MouseEvent){
    ev.preventDefault()
    ev.stopPropagation()
    this.close.emit(ev)
  }

  loading = false
  error = ''

  onSubmit(form:any){
    this.error = ''

    if(this.state== 'email'){
      if(this.inviteSent){
        return
      }
      this.loading = true
      try{
        this.jsonConf.post<{ sent:any }>('HrRcrtUser/ResetPassword', this.formData)
        .subscribe((result)=>{  
          this.inviteSent = result.sent
          this.loading = false
        }, error=>{
          this.loading = false
          this.error = "ERRORTRYINGTOSENDRESETPASSWORDORINVALIDUSER"
        })
      }catch(error){
        this.loading = false;
        console.error(error)
      }
      
      
    }else if(this.state == 'reset'){
      if(this.formData['password'] 
        && this.formData['confirmPassword']
        && this.passFields.some(f=>!f.valid)){
    
          const token = this.route.snapshot.params["t"]
          let headers:any = new HttpHeaders({ 'ActivationToken': token});
          const { formData } = this
    
          this.loading = true
          try{
            let lang = this.translate.defaultLang == "English" ?"en":"es"
            this.jsonConf.postHeader<{activated: boolean, error:string}>
              ('HrRcrtUser/ResetPassword?State=reset&Lang=' + lang, formData, { headers })
              .subscribe((result:any)=>{
                this.loading = false
                if(result.invalidToken){
                  this.router.navigate(['/'])
                  return 
                }
                this.registered = result.resetSuccess
                const initialDate = new Date()
                const interval = setInterval(()=>{
                  const diff = (new Date).getTime() - initialDate.getTime()
                  if(diff >= 10000){
                    clearInterval(interval)
                    this.router.navigate(['/'])
                  }
                },100)
                
              }, (err)=>{
                this.loading = false
              })
          }catch(error){
            this.loading = true
          }
          
    
      } 
    }

  }

  cancel(){

  }
  override getFields: any = ()=> [
    {
      name: 'userName', type: 'email', label: 'ENTERYOUREMAIL', col: 12, maxlength: 100, required: true
    }    
  ]

  

  inviteSent = false

  allowChange = false;
  questionValid = true;
  invalidAnswer = false

  account = {
    user: '',
    newPassword: ''
  }




  
  get sentMessage(){
    return 'PLEASECHECKYOURREGISTEREDEMAIL'
  }

  addEmail(msg:string){
    return msg?msg.replace('[EMAIL]', this.formData.userName):''
  }
  get valid(){
    if(this.state == 'email'){
      return this.fields.filter(f=>f.name == 'userName')
      .map(f=>this.formData[f.name] && f.valid).some(x=>x) 
    }else if(this.state == 'reset'){
      return !this.fields.filter(f=>f.type == 'password')
      .map(f=>f.valid).some(x=>!x) 
      && this.formData['password']
      && this.formData['confirmPassword']
      && this.formData["password"] == this.formData["confirmPassword"]
    }4
    return false
  }

  get cancelName(){
    return this.state == 'reset'?'BACK':'CANCEL'
  }

}
