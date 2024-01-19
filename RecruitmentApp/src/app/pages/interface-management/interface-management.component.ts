import { Component, OnInit } from '@angular/core';
import { faFacebook, faFacebookF, faInstagram, faLinkedin, faLinkedinIn, faTwitter } from '@fortawesome/free-brands-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';
import { IManagementService } from './imanagement.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-interface-management',
  templateUrl: './interface-management.component.html',
  styleUrls: ['./interface-management.component.scss']
})
export class InterfaceManagementComponent implements OnInit {

  constructor(private webconf:WebSiteInfoService, 
    private imserv:IManagementService,
    private r: ActivatedRoute,
    private transalate: TranslateService) { }


    get isIfr(){
      return !!window.parent.location
    }
  ngOnInit(): void {
    
    this.listenWindowEvents()
    window.parent.postMessage({ type: 'hr-ready'}, '*')
    this.imserv.companyTokenChange.subscribe((token)=>{
      if(token){
        this.webconf.companyToken = token;
        if(this.isToken && !!(this.imserv.sessionValid) && window.parent){
          console.log(token)
          this.imserv.sessionValid.next(!!token)
        }
      
        this.logged = true
        this.init()
      }
    
      
    })
    if(!this.isToken){
      this.init()
    }
  }

  

  listenWindowEvents(){
    const listener = (event:MessageEvent<{ type: string, token:string}>)=>{
     
      if(event.data.type == 'hr-api-login'){
        console.log('Hr-api-login', event)
        this.imserv.loginIMToken(event.data.token, {})
        
      }
    }
    window.addEventListener('message',  listener)
  }

  logged = false
  get isToken(){
    return this.r.snapshot.queryParams['tauth'] == 'yes'
  }

  init(){
    //this.imserv.setCurrentConfig()
    console.log('Starting Company ', this.webconf.companyToken)

    this.webconf.loadConfInc().then(conf=>{
      this.imserv.setCurrentConfig()
      this.logged = true
    })

    this.webconf.default.subscribe(val=>{
      this.imserv.formData.defaultText1 = val.text1English
      this.imserv.formData.defaultText1Es = val.text1Spanish
      this.imserv.formData.defaultText2 = val.text2English
      this.imserv.formData.defaultText2Es = val.text2Spanish
      this.imserv.formData.footer = val.text3English
      this.imserv.formData.footerEs = val.text3Spanish
    })
    // const sessionToken = localStorage.getItem('IMSessionToken')
    // if(sessionToken){
    //   this.imserv.sessionToken = sessionToken
    //   this.imserv.sessionValid.next(true)
    // }
  }

  sampleSwVal = 'Y'
  sampleSwitchField:IFormInput = {
    name: 'sampleswitch', col: 12, type: 'switch', checkBoxValues: [
      { trueValue: 'Y'}, {falseValue: 'N'}
    ]
  }

  get validSession(){
    return this.imserv.sessionValid.value
  }
  t(text:string){
    return this.transalate.getParsedResult(this.transalate.defaultLang, text)
  }

  get linkedIn(){
    return this.webconf.conf.value.linkedinLink
  }
  get facebook(){
    return this.webconf.conf.value.facebookLink
  }
  get twitter(){
    return this.webconf.conf.value.twitterLink
  }
  get instagram(){
    return this.webconf.conf.value.instagramLink
  }

  shareBtnClick(event:any, link:string){
    
    const a = document.createElement('a')
    a.href = link
    a.target = 'blank'
    a.click()
  }

  instagramIcon = faInstagram
  facebookIcon = faFacebookF
  twitterIcon = faTwitter
  linkedInIcon = faLinkedinIn

  get stepperSteps(){
    return [
      { label: this.t('Step1'), iconClass: 'fa-solid fa-forward-step' },
      { label: this.t('Step2'), iconClass: 'fa-solid fa-shoe-prints' },
      { label: this.t('Step3'), iconClass: 'fa-solid fa-stop' },
    ]
  }
}
