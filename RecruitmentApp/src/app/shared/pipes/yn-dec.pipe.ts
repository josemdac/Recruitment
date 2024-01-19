import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Pipe({
  name: 'ynDec'
})
export class YnDecPipe implements PipeTransform {

  constructor(private translate: TranslateService){}
  transform(value:string) : string {
    if(value){
      return this.translate.getParsedResult(this.translate.defaultLang, {
       Y: 'YES',
       N: 'NO' 
      }[value])
    }

    return ''
  }

}
