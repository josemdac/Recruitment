import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { StandardModalService } from 'src/app/layout/standard-modal/standard-modal.service';
import { dataURLtoFile } from 'src/app/shared/helpers/tools';
import { HrRcrtCompanyConfiguration } from 'src/app/shared/models/HrRcrtCompanyConfiguration.model';
import { AuthSessionService } from 'src/app/shared/services/auth-session.service';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';

@Injectable({
  providedIn: 'root'
})
export class IManagementService {
  
  constructor(private webconf: WebSiteInfoService, 
    private jconf: JsonConfigService, 
    private translate: TranslateService,
    private stModal:StandardModalService,
    public auth:AuthSessionService) { }


  sessionValid = new BehaviorSubject<boolean>(false)
  sessionToken = ''

  get allowed(){
    return this.sessionValid.value
  }

  t(text:string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }

  setCurrentConfig(useCustom=false){

    //console.log(this.webconf.conf.value)
    const data = this.webconf.conf.value?.logoImage
    const iconData = this.webconf.conf.value?.favicon
    const logoImageFile = data?dataURLtoFile('data:image/png;base64,'+data, 'image.png'):''
    const faviconFile = data?dataURLtoFile('data:image/x-icon;base64,'+iconData, 'favicon.ico'):''
    //console.log('logo', logoImageFile, data)
    if(this.webconf.conf.value){
      Object.keys(this.webconf.conf.value).forEach(k=>{
        this.formData[k] = this.webconf.conf.value[k]
      })
    }else{
      this.formData = {}
    }
    
    //console.log(this.formData)
    
    this.formData.logoImageFile = logoImageFile
    this.formData.favIconFile = faviconFile
    if(useCustom !== false){
      this.formData.useCustomColors = useCustom
    }
    if(this.formData.useCustomColors == 'N'){
      this.setDefaultColors()
    }
  }

  saveConfiguration(){
    const custom = this.formData.useCustomColors == 'N'
    const data = custom?{ ...this.webconf.conf.value, 
      logoImage: this.formData.logoImage,
      logoUrl: this.formData.logoUrl,
      useCustomColors: this.formData.useCustomColors,
      siteName: this.formData.siteName
    }:{ ...this.formData}

    
    const headers = new HttpHeaders({ 'IMSessionToken': this.sessionToken })

    const taskToP = (task:Observable<any>)=>new Promise((resolve, reject)=>{
      try{
        task.subscribe(result=>resolve(result))
      }catch(e){
        reject(e)
      }
    })

    const load = async ()=>{
      await taskToP(this.jconf.postHeader('HrRcrtCompanyConfiguration/SaveConf', data, { headers }))

      const { footer:Text3English, footerEs: Text3Spanish, 
        defaultText1, defaultText1Es, defaultText2, defaultText2Es } = this.formData;
      await taskToP(this.jconf.postHeader('HrRcrtExternalWebsiteInfo/SaveConf/Default', { 
        Text1English: defaultText1, 
        Text1Spanish: defaultText1Es,
        Text2English: defaultText2, 
        Text2Spanish: defaultText2Es,
        Text3English, Text3Spanish }, { headers }))
    }
  
    

    load().then(()=>{
      this.showSuccess(this.t('InterfaceConfiguration'), this.t('SettingsSaved') )
    })



  }

  toggleDefaultColors(){
    
    const data ={ ...this.formData}

    const headers = new HttpHeaders({ 'IMSessionToken': this.sessionToken })
    this.jconf.postHeader('HrRcrtCompanyConfiguration/ToggleDefault', data, { headers }).subscribe((result:any)=>{
      //this.showSuccess(this.t('InterfaceConfiguration'), this.t('SettingsSaved') )
      this.webconf.loadColors(this.jconf.config.value)
    })
    
  }

  

  setDefaultColors(){
    this.formData.buttonsColor= '#FFF'
    this.formData.buttonsHoverColor = '#FFF'
    this.formData.titlesColor = 'var(--stdtitlecolor)'
    this.formData.headerBackgroundColor = 'var(--maincolor)'
    this.formData.subheaderBackgroundColor = 'var(--navcolor)'
    this.formData.selectedTabTextColor = 'var(--navselectedfontcolor)'
    this.formData.normalTabTextColor = 'var(--navfontcolor)'
    this.formData.hoverTabBackground =  'var(--navcolor)'
    this.formData.contentAreaBackground = 'var(--footercolor)'
    this.formData.contentAreaTextColor = 'var(--footerfontcolor)'
    this.formData.footerBackgroundColor = 'var(--footersmallcolor)'
    this.formData.footerTextColor = 'var(--footersmallfontcolor)'
    this.formData.buttonsColor = 'var(--buttonscolor)'
    this.formData.buttonsHoverColor = 'var(--buttonshovercolor)'
    this.formData.buttonsTextColor = 'var(--buttonstextcolor)'
    this.formData.buttons2Color = 'var(--buttons2color)'
    this.formData.buttons2HoverColor = 'var(--buttons2hovercolor)'
    this.formData.buttons2TextColor = 'var(--buttons2textcolor)'
    this.formData.buttons3Color = 'var(--tabheaderbg)'
    this.formData.buttons3HoverColor = 'var(--tabheaderhoverbg)'
    this.formData.buttons3TextColor = 'var(--tabheaderfontcolor)'
    this.formData.buttons3TextColor = 'var(--tabheaderfontcolor)'
    this.formData.stepperColor = 'var(--steppercolor)'
    this.formData.stepperFontColor = 'var(--stepperfontcolor)'
  }

  formData:any = {
    useCustomColors: 'N',
    headerImage: '',
    logoImage: '',
    ofccpRequired: '',
    titlesColor: '',
    buttonsColor: '',
    buttonsHoverColor: '',
    buttonsTextColor: '',
    buttons2Color: '',
    buttons2HoverColor: '',
    buttons2TextColor: '',
    buttons3Color: '',
    buttons3HoverColor: '',
    buttons3TextColor: '',
    stepperColor: '',
    stepperFontColor: '',
    logoUrl: '',
    emailText1: '',
    emailText2: '',
    headerBackgroundColor: '',
    subheaderBackgroundColor: '',
    selectedTabTextColor: '',
    normalTabTextColor: '',
    hoverTabBackground: '',
    contentAreaBackgroundColor: '',
    contentAreaTextColor: '',
    homeImageText: '',
    facebookLink: '',
    instagramLink: '',
    linkedinLink: '',
    twitterLink: '',
    sendResumeToEmail: '',
    versionToShow: '',
    requestSocialNetworks: '',
    footerBackgroundColor: '',
    footerTextColor: ''
  }

  showSuccess(title: string, text:string){
    this.stModal.getSwal().fire({
      title: title,
      text: text,
      icon: 'success'
    })
  }


  companyTokenChange = new BehaviorSubject<string>('')
  loginIMToken(token: string, state: any) {
    this.auth.loginIMToken(token, (result)=>{
      //console.log(result)
      this.webconf.companyToken = result.companyToken
      this.sessionToken = result.sessionToken
      this.companyTokenChange.next(result.companyToken)
    
      
      //localStorage.setItem('IMSessionToken', result.sessionToken)
      this.sessionValid.next(true)
    }, (err)=>{
      state.error = err.error
      this.sessionValid.next(false)
    })
  }

  loginIM(password:string, state: any){
    this.auth.loginIM(password, (result)=>{
      //console.log(result)
      this.sessionToken = result.sessionToken
      //localStorage.setItem('IMSessionToken', result.sessionToken)
      this.sessionValid.next(true)
    }, (err)=>{
      state.error = err.error
      this.sessionValid.next(false)
    })
  }
}
