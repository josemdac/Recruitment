import { ifStmt } from '@angular/compiler/src/output/output_ast';
import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { AuthSessionService } from 'src/app/shared/services/auth-session.service';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  resetPass = false

  constructor(private translate: TranslateService, 
    public elRef:ElementRef<HTMLElement>,
    public router: Router,
    private webInfo: WebSiteInfoService,
    public authSession: AuthSessionService) { }

  
  
  ngOnInit(): void {
    this.authSession.modal = this.elRef.nativeElement
    this.elRef.nativeElement.style.setProperty('display', 'none')

    this.authSession.initialLoginState.subscribe(state=>this.loginState = {
      ...this.loginState ,
      password: state.password,
      attempts: state.attempts,
      error: state.error,
      loading: state.loading,
      inactiveUser: state.inactiveUser,
      resetPass: state.resetPass
    })
  }

  
  ngOnDestroy(){
  
  }

  ngAfterViewChecked(){
    if(this.authSession.showLogin){
      this.elRef.nativeElement.style.setProperty('z-index', '5')
    }else{
      this.elRef.nativeElement.style.setProperty('z-index', '0')
    }
  }

  loginState = {
    attempts: 0,
    error: '',
    user: '',
    password: '',
    loading: false,
    inactiveUser: false,
    resetPass: false
  }

  get lang(){
    return {
      'English': 'en',
      'Español':'es'
    }[this.translate.defaultLang]
  }

  onSubmit(form:any){
    const { user, password} = this.loginState
    this.authSession.login(user, password, (res?:any)=>{
      if(res){
        this.router.navigateByUrl('/'+this.authSession.url)
        this.authSession.closeLoginModal()
        this.loginState.password = ''
      
      }

    }, (err)=>{
      //console.log(err?.error)
      this.loginState.error = err.error
    })
    

  }

  closeClick(event:MouseEvent){
    this.authSession.closeLoginModal()
  }

  onFocus(event:any){
    event?.target?.removeAttribute('readonly')
  }


  forgotPassword(ev:Event){
    ev.preventDefault()
    ev.stopPropagation()

    this.loginState.resetPass = true
  }

  clickLogo(){
    if(this.webInfo.conf.value.logoUrl){
      const link = document.createElement('a')
      link.href = this.webInfo.conf.value.logoUrl
      link.target = 'blank'
      link.click()
      link.remove()
    }
    
  }
}
