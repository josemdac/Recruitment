import { Component, Input, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CoSecurityDefinitionDTO } from 'src/app/shared/models/CoSecurityDefinition.model';
const range = (size: number)=>{    return Array.from((new Array(size)).keys())}
@Component({
  selector: 'app-pass-validation-stepper',
  templateUrl: './pass-validation-stepper.component.html',
  styleUrls: ['./pass-validation-stepper.component.scss']
})
export class PassValidationStepperComponent implements OnInit {

  @Input() startColor = ''
  @Input() endColor = ''

  @Input() username = ''
  @Input() password = ''
  
  @Input() def: CoSecurityDefinitionDTO = {} as CoSecurityDefinitionDTO
  colorRange:string[] = []

  state:any = { valid: false, invalidReqs: []}
  defMatch = []
  constructor(public translate: TranslateService) { }

  t(text:string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }


  ngOnInit(): void {
    this.parseColors()
  }


  ngOnChanges(){
    this.parseColors()

    this.state = this.validatePassword()
    this.defMatch = this.getDefMatch()

  }
  parseColors(){

    this.colorRange = []
    const getRGB = (str:string)=>{
      if(str.includes('rgb')){
        return str.split('rgb').join('').split('(').join('').split(')').join('').split(',').map(c=>parseFloat(c))
      }

      if(str.includes('#')){
        const rawstr = str.split('#').join('')
        const r = parseInt(rawstr.slice(0,2), 16)
        const g = parseInt(rawstr.slice(2,4), 16)
        const b = parseInt(rawstr.slice(4,6), 16)
        return [r, g, b]
      }
      return [0,0,0]
    }

    const color1 = getRGB(this.startColor)
    const color2 = getRGB(this.endColor)

    //console.log(color1, color2)

    this.colorRange.push(this.endColor)

    const rstep = (color1[0] - color2[0])/7
    const gstep = (color1[1] - color2[1])/7
    const bstep = (color1[2] - color2[2])/7

    //console.log(rstep, gstep, bstep)
    for(let i=1; i<7; i++){
      const r = color2[0] + rstep*i
      const g = color2[1] + gstep*i
      const b = color2[2] + bstep*i

      this.colorRange.push(`rgb(${r.toFixed(0)}, ${g.toFixed(0)}, ${b.toFixed(0)})`)
    }

    //console.log(this.colorRange)

  }

  

  getDefMatch(){


    const m:any = {
      'minLowerCharacters': this.t('Passlower').split('{lower}').join(this.def.minLowerCharacters),
      'passwordCanBeTheUsername': this.t('CanbeUser'),
      'passwordLength': this.t('Passlength').split('{length}').join(this.def.passwordLength),
     // 'canRepeatePassword',
      'minNumericCharacters': this.t('Passnumeric').split('{numbers}').join(this.def.minNumericCharacters),
      'minSymbolCharacters': this.t('Passsymbols').split('{symbols}').join(this.def.minSymbolCharacters),
      'minUpperCharacters': this.t('Passupper').split('{upper}').join(this.def.minUpperCharacters),
    }

    return this.state.invalidReqs.map((req:string)=>m[req])
  }

  validatePassword(){
    
    let isValid:any = true;
    let invalidReqs = [
      'minLowerCharacters',
      'passwordCanBeTheUsername',
      'passwordLength',
     // 'canRepeatePassword',
      'minNumericCharacters',
      'minSymbolCharacters',
      'minUpperCharacters'
    ]

    if(!this.def){
      return { valid: false, invalidReqs}
    }

    //length
    isValid = this.validateLength()
    if(isValid){ invalidReqs = invalidReqs.filter(f=>f!='passwordLength')}

    //Lower
    isValid = isValid && this.validateMinLowerCharacters()
    if(isValid){ invalidReqs = invalidReqs.filter(f=>f!='minLowerCharacters')}

    //upper
    isValid = isValid && this.validateMinUpperCharacters()
    if(isValid){ invalidReqs = invalidReqs.filter(f=>f!='minUpperCharacters')}

    //Numeric
    isValid = isValid && this.validateMinNumericCharacters()
    if(isValid){ invalidReqs = invalidReqs.filter(f=>f!='minNumericCharacters')}

    //Symbol
    isValid = isValid && this.validateMinSymbolCharacters()
    if(isValid){ invalidReqs = invalidReqs.filter(f=>f!='minSymbolCharacters')}

    //UserName
    isValid = isValid && this.validateWithUserName()
    if(isValid){ invalidReqs = invalidReqs.filter(f=>f!='passwordCanBeTheUsername')}
    
    return { valid: isValid, invalidReqs}
  }

  
  validateLength()
  {
      return (this.def.passwordLength <= this.password.length)
  }

  validateMinLowerCharacters(){
      let lowerRegEx = ".*?[a-z]"
      let regex = new RegExp("(?=" +range(this.def.minLowerCharacters).map(()=>{
          return lowerRegEx;
      }).join('') + ")")

      return (regex.test(this.password) && this.reqUpperLower) || !this.reqUpperLower;
  }
  validateMinUpperCharacters(){
      let lowerRegEx = ".*?[A-Z]"


      let regex = new RegExp("(?=" +range(this.def.minUpperCharacters).map(()=>{
          return lowerRegEx;
      }).join('') + ")")

      return (regex.test(this.password) && this.reqUpperLower) || !this.reqUpperLower;
  }

  validateMinNumericCharacters(){
      let lowerRegEx = ".*?[0-9]"
      let regex = new RegExp("(?=" +range(this.def.minNumericCharacters).map(()=>{
          return lowerRegEx;
      }).join('') + ")")

      return regex.test(this.password);
  }

  validateMinSymbolCharacters(){
      let lowerRegEx = ".*?[!@#$%&()^*-+]"
      let regex = new RegExp("(?=" +range(this.def.minSymbolCharacters).map(()=>{
          return lowerRegEx;
      }).join('') + ")")

      return regex.test(this.password);
  }

  validateWithUserName(){
      if(this.passwordUserName){
          return this.password != this.username
      }

      return true;
  }

  get reqUpperLower(){
    return this.def?(this.def.reqUpperLowerCharacters == 'Y'):false
}

get passwordUserName(){
    return this.def?(this.def.passwordCanBeTheUsername == 'Y'):false
}
  

}
