import { ChangeDetectorRef, Directive, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { TranslatePipe, TranslateService } from '@ngx-translate/core';
import { AuthSessionService } from 'src/app/shared/services/auth-session.service';

@Directive({
  selector: '[appCreateAccountLink]'
})
export class CreateAccountLinkDirective {

  constructor(private translate: TranslateService, 
    private cdr: ChangeDetectorRef,
    private elRef:ElementRef<HTMLDivElement>,
    private router:Router, 
    private auth:AuthSessionService) { 
      translate.addLangs(['English', 'Español']);
      translate.setDefaultLang('Español')
      this.setUp = this.setUp.bind(this)

    }

    tp = new TranslatePipe(this.translate, this.cdr)
  t(text:any){
    return this.tp.transform(text)
  }

  ngOnInit(){
    this.setUp()

    this.translate.onDefaultLangChange.subscribe(()=>{
      this.setUp()
    })

  }

  setUp(){
    let text = this.t('CreateAnAccountToApply')
    let createAn = this.t('CreateAnAccount')
    const link = document.createElement('a')
    link.className = 'create-account'
    link.textContent = createAn

    this.elRef.nativeElement.innerHTML = text.split('[CREATEANACCOUNT]').join(link.outerHTML)
    const added = this.elRef.nativeElement.querySelector('a')
    if(added){
      added.addEventListener('click', (e)=>{
        this.router.navigate(['register'])
        this.auth.closeLoginModal()
      })
    }

  }

}
