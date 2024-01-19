import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Pipe({
  name: 'statusDec'
})
export class StatusDecPipe implements PipeTransform {

  constructor(private translate: TranslateService){}
  transform(value:string) : string {
    if(value){
      return this.translate.getParsedResult(this.translate.defaultLang, {
       A: 'Active',
       I: 'Inactive' 
      }[value])
    }

    return ''
  }

}
