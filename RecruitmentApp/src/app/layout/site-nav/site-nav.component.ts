import { ChangeDetectorRef, Component, ElementRef, OnInit } from '@angular/core';
import { TranslatePipe, TranslateService } from '@ngx-translate/core';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import { eventNames } from 'process';
import { AuthSessionService } from 'src/app/shared/services/auth-session.service';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({
  selector: 'app-site-nav',
  templateUrl: './site-nav.component.html',
  styleUrls: ['./site-nav.component.scss']
})
export class SiteNavComponent implements OnInit {

  faBars = faBars
  showMenu = false

  constructor(public translate: TranslateService, private dref: ChangeDetectorRef, 
    private elRef: ElementRef<HTMLElement>, private authSession: AuthSessionService,
    private route: Router, private croute: ActivatedRoute) { 
    this.listener = this.listener.bind(this)
  }

  tp = new TranslatePipe(this.translate, this.dref)

  listener(event:any){
    
    if(!this.elRef.nativeElement.contains(event.target)){
      //console.log('hidding menu')
      this.showMenu = false
    }
  }


  ngOnInit(): void {
    document.addEventListener('click', this.listener)
  }

  ngOnDestroy(){
    document.removeEventListener('click', this.listener)
  }

  get currentUrl(){
    //@ts-ignore
    return this.croute.snapshot._routerState.url
  }
  get links(): { label:string, isActive?: ()=>boolean,onClick: (event:MouseEvent)=>void, children?: any[]}[] {

    return [
      { label: 'FindJobs', onClick: (ev)=>{
        ev.preventDefault()
        this.route.navigate(['/joblist'])

      }, children: [], isActive: ()=>this.currentUrl.includes('joblist')},
      ...(!this.authSession.validSession?[{ label: 'JoinToOurCommunity', onClick: (ev:any)=>{
        ev.preventDefault()
        this.route.navigate(['/register'])
      }, isActive: ()=>this.currentUrl.includes('register'), children: [
       
      ]}]:[{ label: 'MyAccount',isActive: ()=>this.currentUrl.includes('myaccount'), onClick: (ev:any)=>{
        this.route.navigate(['/myaccount'])
      }}]),
      
      !this.authSession.validSession?{ label: 'Login', onClick: (ev)=>{
        this.authSession.callLogin('', ev.target)
      }}:{ label: 'Logout', onClick: (ev)=>{
        this.authSession.logOut()
      }},
      {
        label: 'Home', onClick: (ev)=>{
          this.route.navigate(['/'])
        }, isActive: ()=>this.currentUrl == '/' || this.currentUrl.includes('landing')
      }
    ]
  } 


  

}
