import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { parseDbComment } from '../helpers/tools';

@Pipe({
  name: 'langProfDec'
})
export class LangProfDecPipe implements PipeTransform {
  opts = parseDbComment('B=Beginner, F=Fluent, I=Intermediate')
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
