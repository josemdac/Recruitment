import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { toBase64 } from 'src/app/shared/helpers/tools';
import { IManagementService } from '../imanagement.service';

@Component({
  selector: 'app-interface-management-options',
  templateUrl: './interface-management-options.component.html',
  styleUrls: ['./interface-management-options.component.scss']
})
export class InterfaceManagementOptionsComponent implements OnInit {

  constructor(private translate: TranslateService, public imservice: IManagementService) { }

  t(text:string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }

  ngOnInit(): void {
    this.fields[0].radioOptions = [
      { label: this.t('DefaultColors'), value: 'N'},
      { label: this.t('CustomColors'), value: 'Y'},
    ]
  }

  fields:IFormInput[]= [
    { name: 'titlesColor', col: 12, type: 'color' },
    { name: 'buttonsColor', col: 12, type: 'color' },
    { name: 'buttonsHoverColor', col: 12, type: 'color' },
    { name: 'buttonsTextColor', col: 12, type: 'color' },
    { name: 'buttons2Color', col: 12, type: 'color' },
    { name: 'buttons2HoverColor', col: 12, type: 'color' },
    { name: 'buttons2TextColor', col: 12, type: 'color' },
    { name: 'buttons3Color', col: 12, type: 'color' },
    { name: 'buttons3HoverColor', col: 12, type: 'color' },
    { name: 'buttons3TextColor', col: 12, type: 'color' },
    { name: 'switchBackColor', col: 12, type: 'color' },
    { name: 'langButtonColor', col: 12, type: 'color' },
    { name: 'stepperColor', col: 12, type: 'color' },
    { name: 'stepperFontColor', col: 12, type: 'color' },
    { name: 'headerBackgroundColor', col: 12, type: 'color' },
    { name: 'subheaderBackgroundColor', col: 12, type: 'color' },
    { name: 'selectedTabTextColor', col: 12, type: 'color' },
    { name: 'normalTabTextColor', col: 12, type: 'color' },
    { name: 'hoverTabBackground', col: 12, type: 'color' },
    { name: 'contentAreaBackgroundColor', col: 12, type: 'color' },
    { name: 'contentAreaTextColor', col: 12, type: 'color' },
    { name: 'footerBackgroundColor', col: 12, type: 'color' },
    { name: 'footerTextColor', col: 12, type: 'color' },
    { name: 'siteName', col: 12, type: 'text', maxlength: 100},
    { name: 'logoUrl', col: 12, type: 'url', maxlength: 150},
    { name: 'logoImageFile', col: 12, type: 'file',  fileRestrictions: {
      allowedExtensions: ['png', 'jpg', 'jpeg']
    }},
    { name: 'mainImageFile', col: 12, type: 'file',  fileRestrictions: {
      allowedExtensions: ['jpg', 'png', 'jpeg']
    }},
    { name: 'faviconFile', col: 12, type: 'file', fileRestrictions: {
      allowedExtensions: ['jpg', 'png', 'jpeg', 'ico']
    }},
    { name: 'privacyPolicy', col: 12, type: 'editor' },
    { name: 'privacyPolicyEs', col: 12, type: 'editor' },
    { name: 'defaultText1', col: 12, type: 'editor' },
    { name: 'defaultText1Es', col: 12, type: 'editor' },
    { name: 'defaultText2', col: 12, type: 'editor' },
    { name: 'defaultText2Es', col: 12, type: 'editor' },
    { name: 'footer', col: 12, type: 'editor' },
    { name: 'footerEs', col: 12, type: 'editor' },
    


    
  ]

  getIdx(name:string){
    return this.fields.map(f=>f.name).indexOf(name)
  }

  get f1(){
    return this.fields//.slice(0, this.getIdx('siteName'))
  }


  get f1_1(){
    return this.f1.slice(0, this.getIdx('siteName')+1)
  }
  get f1_2(){
    const start = this.getIdx('logoUrl')
    const idx = this.getIdx('logoImageFile')
    return this.f1.slice(start, idx + 1)
  }
  get f2(){
    const start = this.fields.map(f=>f.name).indexOf('mainImageFile')
    //const idx = this.fields.map(f=>f.name).indexOf('mainImageFile')
    return this.fields.slice(start, start+1)
  }
  get f3(){
    const start = this.getIdx('faviconFile')
    return this.fields.slice(start, start + 1)
  }

  get f4(){
    const idx = this.fields.map(f=>f.name).indexOf('privacyPolicy')
    return this.fields.slice(idx, this.fields.length)
  }
  get logoPreview(){
    const data = this.imservice.formData.logoImage
    if(data){
      return 'data:image/png;base64,'+data
    }
    return ''
  }

  get mainImagePreview(){
    const data = this.imservice.formData.mainSiteImage
    if(data){
      return 'data:image/png;base64,'+data
    }
    return ''
  }
  get faviconPreview(){
    const data = this.imservice.formData.favicon
    if(data){
      return 'data:image/png;base64,'+data
    }
    return ''
  }

  isColor(field:IFormInput){
    return !['logoImageFile', 'logoUrl', 'mainImageFile', 'siteName'].includes(field.name)
  }

  changeOption(event: any, field:IFormInput){
    this.imservice.formData[field.name] = event
    if(field.name == 'logoImageFile'){
      if(!event){
        this.imservice.formData['logoImage'] = ''
        return
      }

      //console.log(event)
      toBase64(event).then(data=>{
        this.imservice.formData['logoImage'] = (data+'').split('base64,')[1]
      })
    }

    if(field.name == 'mainImageFile'){
      if(!event){
        this.imservice.formData['mainSiteImage'] = ''
        return
      }

      //console.log(event)
      toBase64(event).then(data=>{
        this.imservice.formData['mainSiteImage'] = (data+'').split('base64,')[1]
      })
    }

    if(field.name == 'faviconFile'){
      if(!event){
        this.imservice.formData['favicon'] = ''
        return
      }

      //console.log(event)
      toBase64(event).then(data=>{
        this.imservice.formData['favicon'] = (data+'').split('base64,')[1]
      })
    }
  }
}
