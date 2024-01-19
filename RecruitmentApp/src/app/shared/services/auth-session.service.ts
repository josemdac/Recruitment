import { HttpClient } from '@angular/common/http';
import { i18nMetaToJSDoc } from '@angular/compiler/src/render3/view/i18n/meta';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable, Subscriber } from 'rxjs';
import { SessionToken } from '../constants';
import { JsonConfigService } from './json-config.service';


@Injectable({
  providedIn: 'root'
})
export class AuthSessionService {

  constructor(private http: HttpClient, private jsonConf: JsonConfigService, 
    private router: Router) { 
    
    this.jsonConf.config.subscribe(conf=>{
      this.validateSession()
    })
  }

  initialLoginState = new BehaviorSubject({
    attempts: 0,
    error: '',
    user: '',
    password: '',
    loading: false,
    inactiveUser: false,
    resetPass: false
  });

  showLogin = false;

  modal:any;
  url = 'myaccount'
  validSession = false
  sessionChange = new BehaviorSubject(false)
  initialValidation = false


  

  callLogin(url?:string, caller?:any){
    if(url){
      this.url = url;
    }
    this.showLogin = true
    this.initialLoginState.next({
      attempts: 0,
      error: '',
      user: '',
      password: '',
      loading: false,
      resetPass: false,
      inactiveUser: false
    })
    if(this.modal){
      this.modal.style.setProperty('display', 'flex')
    }
    this.setUp(caller)
    
  }

  setUp(caller:any){
    const modal = this.modal.querySelector('.login-modal')
    this.listener = (event:any)=>{
      
      const formInput = event.target.closest('form-input') 
        && event.target.closest('form-input').getAttribute('belongs-to') == 'login-modal'

      
      if(this.showLogin && !modal.contains(caller) && !modal?.contains(event.target) && !formInput){
        this.closeLoginModal()
      }
      
    }
    setTimeout(()=>{
      document.addEventListener('click', this.listener)
    },10)
    
  }

  closeLoginModal(){
    this.showLogin = false;
    document.removeEventListener('click', this.listener)
    if(this.modal){
      this.modal.style.setProperty('display', 'none')
    }
    this.url = 'myaccount'
  }
  listener:any;

  logOut(){
    localStorage.setItem(SessionToken, '')
    this.validSession = false
  
    this.sessionChange.next(false)
    this.router.navigateByUrl('/')
  }

  loginIM(password:string, success:(result?:any)=>void, error: (err?:any)=>void){
    const complete  =(res: { sessionToken: string })=>{
      
      localStorage.setItem(SessionToken, res.sessionToken)
      this.validSession = true
      this.sessionChange.next(true)
      success(res)
    }

    try{
       this.http.post<any>(this.jsonConf.apiUrl + '/Authorize/LoginIM', {password}).pipe().subscribe(
        complete 
        , error
       )
    }catch(err){
      error(err)
    }
  }

  loginIMToken(token:string, success:(result?:any)=>void, error: (err?:any)=>void){
    const complete  =(res: { sessionToken: string, companyToken: string })=>{
      
      localStorage.setItem(SessionToken, res.sessionToken)
      
      this.validSession = true
      this.sessionChange.next(true)
      success(res)
    }

    try{
       this.http.post<any>(this.jsonConf.apiUrl + '/Authorize/LoginIMToken', { sessionToken: token }).pipe().subscribe(
        complete 
        , error
       )
    }catch(err){
      error(err)
    }
  }

  login(userName: string, password: string, success: (result?:any)=>void, error: (error?:any)=>void){

    const complete  =(res: { sessionToken: string })=>{
      
      localStorage.setItem(SessionToken, res.sessionToken)
      this.validSession = true
      this.sessionChange.next(true)
      success(res)
    }

    try{
       this.http.post<any>(this.jsonConf.apiUrl + '/Authorize/Login', {userName, password}).pipe().subscribe(
        complete 
        , error
       )
    }catch(err){
      error(err)
    }
    

  } 

  validateSession(obs?:Subscriber<boolean>){

    if(!this.jsonConf.apiUrl){
      return 
    }
    
    const complete  =(success: {sessionToken:string})=>{
      localStorage.setItem(SessionToken, success.sessionToken)
      this.validSession = true
      this.sessionChange.next(true)
      if(obs){
        obs.next(true)

      }
      
    }

   

    try{
      return this.http.post<any>(this.jsonConf.apiUrl + '/Authorize/ValidateSession', {})
      .subscribe(complete, ()=>{
        if(obs){
          obs.next(false)
        }
      }, ()=>this.initialValidation = true)
    }catch(error){
      this.validSession = false
      if(obs){
        obs.next(false)
      }
      return false
    }
  }
}
