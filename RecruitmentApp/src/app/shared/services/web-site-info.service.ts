import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { toJSON } from '@progress/kendo-angular-grid/filtering/operators/filter-operator.base';
import { BehaviorSubject } from 'rxjs';
import { HrRcrtCompanyConfiguration, HrRcrtCompanyConfigurationColors } from '../models/HrRcrtCompanyConfiguration.model';
import { HrRcrtExternalWebsiteInfo } from '../models/HrRcrtExternalWebsiteInfo.model';
import { JsonConfigService } from './json-config.service';

@Injectable({
  providedIn: 'root'
})
export class WebSiteInfoService {

  constructor(private http:HttpClient, public jsonConf: JsonConfigService) {
    const base = document.querySelector('base')
    if(base){
      this.companyToken = base.getAttribute('appkey')+''
      //console.log(this.companyToken)
    }  
  }

  companyToken = ''

  default= new BehaviorSubject<HrRcrtExternalWebsiteInfo>({} as HrRcrtExternalWebsiteInfo)
  privacy = new BehaviorSubject<HrRcrtExternalWebsiteInfo>({} as HrRcrtExternalWebsiteInfo)
  footer = new BehaviorSubject<HrRcrtExternalWebsiteInfo>({} as HrRcrtExternalWebsiteInfo)
  transparency = new BehaviorSubject<HrRcrtExternalWebsiteInfo>({} as HrRcrtExternalWebsiteInfo)
  conf = new BehaviorSubject<HrRcrtCompanyConfiguration>({} as HrRcrtCompanyConfiguration)
  culture = new BehaviorSubject<{ cultureCode:string, description: string, currencyCode: string, countryCurrency: string}>({ cultureCode: 'es', description: '', countryCurrency: '', currencyCode:''})
  logoImage = new BehaviorSubject<{ logoImage:any }>({ logoImage: ''})
  mainSiteImage = new BehaviorSubject<{ mainSiteImage:any }>({ mainSiteImage: ''})
  headerImage = new BehaviorSubject<{ headerImage:any }>({ headerImage: ''})


  
  get defaultInfo(){
    return this.default.value
  }
  loadDefault(){
    if(!this.jsonConf.config.value.serverApi){
      return 
    }
    this.http.get<HrRcrtExternalWebsiteInfo>(this.jsonConf.config.value.serverApi + '/HrRcrtExternalWebsiteInfo/Default').pipe().subscribe(def=>this.default.next(def))
    this.http.get<HrRcrtExternalWebsiteInfo>(this.jsonConf.config.value.serverApi + '/HrRcrtExternalWebsiteInfo/Privacy').pipe().subscribe(def=>this.privacy.next(def))
    this.http.get<HrRcrtExternalWebsiteInfo>(this.jsonConf.config.value.serverApi + '/HrRcrtExternalWebsiteInfo/Footer').pipe().subscribe(def=>this.footer.next(def?def:{} as any))
    this.http.get<HrRcrtExternalWebsiteInfo>(this.jsonConf.config.value.serverApi + '/HrRcrtExternalWebsiteInfo/TranspInCoverage').pipe().subscribe(def=>this.transparency.next(def))
    this.http.get<HrRcrtCompanyConfiguration>(this.jsonConf.config.value.serverApi + '/HrRcrtCompanyConfiguration').pipe().subscribe(conf=>this.conf.next(conf))
    this.jsonConf.get('HrRcrtCompanyConfiguration/Culture').pipe().subscribe(culture=>this.culture.next(culture as any))
    this.jsonConf.get('HrRcrtCompanyConfiguration/LogoImage').pipe().subscribe(image=>{
      this.logoImage.next(image as any)
      
    })
    // this.jsonConf.get('HrRcrtCompanyConfiguration/MainSiteImageCompressed').pipe().subscribe(image=>{
    //   this.mainSiteImage.next(image as any) 
    // })
    const t = encodeURI(this.companyToken).split('+').join('%2B')
    const mainSiteUri = `${this.jsonConf.apiUrl}/HrRcrtCompanyConfiguration/MainSiteImageCompressed?Token=${t}`
    //console.log(mainSiteUri, this.companyToken)
    this.mainSiteImage.next({ mainSiteImage: mainSiteUri })
    //this.jsonConf.get('HrRcrtCompanyConfiguration/HeaderImage').pipe().subscribe(image=>this.headerImage.next(image as any))
  }


  loadConfInc(){
    return new Promise((resolve, reject)=>{
      if(!this.jsonConf.config.value.serverApi){
        return reject()
      }
      this.http.get<HrRcrtCompanyConfiguration>(this.jsonConf.config.value.serverApi + '/HrRcrtCompanyConfiguration?IncImg='+'Y')
      .pipe().subscribe(conf=>{
        this.conf.next(conf)
        resolve(conf)
      })
    })
    
  }

  loadCompanyTimeout(){
    return this.jsonConf.get<{ timeOutMinutes : number }>('HrRcrtExternalWebsiteInfo/Tm')
  }

  get logoImg(){
    const data = this.logoImage.value.logoImage
    if(data){
      return 'data:image/png;base64,'+data
    }
    return ''
  }

  get mainImage(){
    const data = this.mainSiteImage.value.mainSiteImage
    if(data){
      return data
      //return 'data:image/png;base64,'+data
    }
    return ''
  }
  loadFavicon(){
    
  }
  loadColors(config:any){

    if(!config || (config && !config.serverApi)){ return }
    this.http.get<HrRcrtCompanyConfigurationColors>(config.serverApi + '/HrRcrtCompanyConfiguration/Colors').pipe().subscribe(conf=>{
      const props:any = {
        "--maincolor": conf.headerBackgroundColor,
        "--navcolor": conf.subheaderBackgroundColor,
        "--navhovercolor": conf.hoverTabBackground,
        "--navselectedfontcolor": conf.selectedTabTextColor,
        "--navfontcolor": conf.normalTabTextColor,
        "--footercolor": conf.footerBackgroundColor,
        "--loginbackground": 'rgb(40,46,51)',
        "--footerfontcolor": conf.footerTextColor,
        "--footersmallcolor": conf.footerBackgroundColor,
        "--footersmallfontcolor": conf.footerTextColor,
        
        "--normaltabtextcolor":  conf.normalTabTextColor,

        "--buttonscolor": conf.buttonsColor,
        "--buttonstextcolor": conf.buttonsTextColor,
        "--buttonshovercolor": conf.buttonsHoverColor,

        "--buttons2color": conf.buttons2Color,
        "--buttons2textcolor": conf.buttons2TextColor,
        "--buttons2hovercolor": conf.buttons2HoverColor,
        "--sitecontentcolor": conf.contentAreaBackgroundColor,
        "--sitecontenttext": conf.contentAreaTextColor,
        /**StdTitle Color*/
        "--stdtitlecolor": conf.titlesColor,
        "--switchbtncolor": conf.switchBackColor,
        /**My Account*/
        "--tabheaderbg": conf.buttons3Color,
        "--tabheaderhoverbg": conf.buttons3HoverColor,
        "--tabheaderfontcolor": conf.buttons3TextColor,
        "--langbuttoncolor": conf.langButtonColor,

        /**Apply process**/
        "--steppercolor": conf.stepperColor,
        "--stepperfontcolor": conf.stepperFontColor,
      }
      
      Object.keys(props).forEach(k=>{
        document.documentElement.style.setProperty(k, props[k])
      })
      
    })
  }
}
