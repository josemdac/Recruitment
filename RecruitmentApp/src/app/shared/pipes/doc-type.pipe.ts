import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { parseDbComment } from '../helpers/tools';

@Pipe({
  name: 'docType'
})
export class DocTypePipe implements PipeTransform {
  opts = parseDbComment('R=Resume, C=Cover Letter, X=References, O=Other Documents, A=Awards, T=Certificates')
  constructor(private translate: TranslateService){}
  transform(value:string) : string {
    
    
    if(value){
      const opt = this.opts.find(o=>o.id == value)
      if(opt){
        return this.translate.getParsedResult(this.translate.defaultLang, opt.description)
      }
    }


    return ''
  }

}
