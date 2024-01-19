import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Pipe({
  name: 'profileDec2'
})
export class ProfileDec2Pipe implements PipeTransform {

  constructor(private translate:TranslateService){}
  transform(value: any, type: string): string {
    if(value){
      if(this.translate.defaultLang == 'English'){
        return value[type + 'English']
      }else if(this.translate.defaultLang == 'Español'){
        return value[type + 'Spanish']
      }
    }

    return ''
  }

}
