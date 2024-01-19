import { HttpClient } from '@angular/common/http';
import { Directive, ElementRef, HostBinding, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';

@Directive({
  selector: '[formInputSelectSource]'
})
export class FormInputSelectSourceDirective {


  @Input() noSort = false
  @Input('selected') value: any;
  @Input() formInputSelectSource:string|{value: any, text: string}[] = ''
  constructor(private http: HttpClient, private jsonConF: JsonConfigService, private elRef: ElementRef<HTMLSelectElement>, private translate: TranslateService) { }

  ngOnChanges({ formInputSelectSource}:any){
    this.setUp()
    

   
    
    
  }
  ngOnInit(){
    this.setUp()
    
  }

  

  t(text: string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  
  }

  setUp(){
    for(const c of Array.from(this.elRef.nativeElement.children)){
      this.elRef.nativeElement.removeChild(c)
      c.remove()
    }
    if(!this.formInputSelectSource){
      return
    }

    if(typeof this.formInputSelectSource == 'string'){
      this.http.get<{value: any, text: string}[]>(this.jsonConF.apiUrl + '/' + this.formInputSelectSource + '/Dropdown')
        .subscribe(data=>{
          this.createOptions(data)
        })
      
    }else{
      this.createOptions(this.formInputSelectSource)
    }

  }

  
  containsSelect(){

    let target = this.t('Select')
    return Array.from(this.elRef.nativeElement.querySelectorAll('option'))
      .some(o=>o.textContent == target)
  }

  createOptions(data: {value: any, text: string }[]){

    Array.from(this.elRef.nativeElement.children).forEach(c=>{
      this.elRef.nativeElement.removeChild(c)
    })

    //if(!this.containsSelect()){
    const empty = document.createElement('option')
    empty.value = ''
    empty.text = this.t('Select')
    this.elRef.nativeElement.appendChild(empty)
    //}
    

    const data1 = this.noSort?data:
    data.sort((a,b)=>{
      let A = a.text;
      let B = b.text;
      return A.localeCompare(B)
    });
    
    data1.forEach(opt=>{
      const o = document.createElement('option')
      o.value = opt.value;
      o.textContent = opt.text
      o.selected = this.value == opt.value
      this.elRef.nativeElement.appendChild(o)
    })
  }



}
