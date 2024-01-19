import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Pipe({
  name: 'profileDec'
})
export class ProfileDecPipe implements PipeTransform {


  constructor(private translate:TranslateService){}
  transform(value: any): string {
    if(value){
      if(this.translate.defaultLang == 'English'){
        return value.englishDescription
      }else if(this.translate.defaultLang == 'Español'){
        return value.spanishDescription
      }
    }

    return ''
  }

}
