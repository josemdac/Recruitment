import { Directive, ElementRef, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ProfileDec2Pipe } from '../pipes/profile-dec2.pipe';

@Directive({
  selector: '[appProfileTypeCell]'
})
export class ProfileTypeCellDirective {

  @Input() appProfileTypeCell?: any
  @Input() type: string = ''
  constructor(private translate:TranslateService, private e:ElementRef<HTMLElement>) { }

  ngOnInit(){
    this.translate.onDefaultLangChange.subscribe(()=>{
    
      this.setProfileType()
    })
  }

  ngOnChanges(){
    this.setProfileType()
  }


  setProfileType(){
    
    if(this.appProfileTypeCell){
      const pd = new ProfileDec2Pipe(this.translate)
      this.e.nativeElement.innerText = pd.transform(this.appProfileTypeCell, this.type)
    }
    

  }
}
