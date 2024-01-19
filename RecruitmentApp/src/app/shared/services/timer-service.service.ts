import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AutoResume, DEFAULT_INTERRUPTSOURCES, Idle } from '@ng-idle/core';
import { Keepalive } from '@ng-idle/keepalive';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { StandardModalService } from 'src/app/layout/standard-modal/standard-modal.service';
import { AuthSessionService } from './auth-session.service';
import { WebSiteInfoService } from './web-site-info.service';

@Injectable({
  providedIn: 'root'
})
export class TimerServiceService {

  constructor( private idle: Idle, private keepalive: Keepalive,
    private stModal: StandardModalService, private webConf:WebSiteInfoService,
    private auth: AuthSessionService, private translate: TranslateService) { 

    // const resetListener =  (e:any)=>{
    //   this.mouseMoved = true;
    //   if(this.timeoutMinutes){
    //     if(this.remaining > 60000){
    //       this.remaining = this.timeoutMinutes*60000;
    //     }
        
    //   }

    // }
    // document.body.addEventListener('mousemove', resetListener)
    // document.body.addEventListener('mousedown', resetListener)
    // document.body.addEventListener('keydown', resetListener)
    // this.removeListener = ()=>{
    //   document.body.removeEventListener('mousedown', resetListener)
    //   document.body.removeEventListener('mousemove', resetListener)
    //   document.body.removeEventListener('keydown', resetListener)
    // }





    this.init = this.init.bind(this)
    this.getWarningHtml = this.getWarningHtml.bind(this)


   

  }

  start(){
    this.webConf.jsonConf.config.subscribe(conf=>{
      if(conf.serverApi){
            
        
        if(this.auth.validSession){
          //console.log(AuthenticationService.getCurrentUser())
          this.setUpInterval()
        }

        this.auth.sessionChange.subscribe((valid)=>{
          if(valid){
            this.setUpInterval()
          }
        

          
          //this.onMinTimeLeft()
        })
      }
    })
  }

  ngOnDestroy(){
    // if(this.removeListener){
    //   this.removeListener()
    // }
  }

  timeoutMinutes = 0;
  setUpInterval(){
    this.webConf.loadCompanyTimeout().subscribe((tm)=>{
      this.timeoutMinutes = tm.timeOutMinutes
        //this.timeoutMinutes = 2;
        //debugger;
        //console.log(Swal);
        this.init()
    })
  }

  continueListener = (event:MouseEvent)=>{
    this.setUpInterval()
    this.stModal.getSwal().close()
    //console.log('Continued..')
  }

  logOutListener = (event:MouseEvent)=>{
    
    this.logout()
    this.stModal.getSwal().close()
    //console.log('Logged Out..')
    
  }

  swalHandler(){
   
    this.timerInterval = setInterval((() => {
      const content = this.stModal.getSwal().getContainer()
      
      //debugger;
      if (content) {
        const b = content.querySelector('.remaining')
        const button = content.querySelector('.swal2-confirm');
        const continueBtn: HTMLButtonElement = content.querySelector('button#continue-btn');
        const logoutBtn:HTMLButtonElement = content.querySelector('button#logout-btn');

        if(button){
          button.style.setProperty('display', 'none')
        }
        //debugger;
        //console.log(b, this.getRemainingString(Swal.getTimerLeft()))
        if (b) {

          b.textContent = ""+this.getRemainingString(this.stModal.getSwal().getTimerLeft())+""
        }

        if(continueBtn && !continueBtn.onclick){
          continueBtn.onclick = this.continueListener
        }

        if(logoutBtn && !logoutBtn.onclick){
          logoutBtn.onclick = this.logOutListener
        }
      }

      if(this.stModal.getSwal().getTimerLeft()<= 0 && this.timerInterval){
        if(this.stModal.getSwal().isVisible()){
          this.stModal.getSwal().close()
          this.logout()
        }
        clearInterval(this.timerInterval)
        
        
      }

      if(this.mouseMoved){
        //Swal.close();
      }
    }).bind(this), 1000)
  }

  intervalHandler(){
      

      if(this.remaining<=60000 && this.auth.sessionChange.value){
        if(!this.openWarning){
          this.onMinTimeLeft()
          this.openWarning = true;
        }
        
        this.swalHandler()
        

        if(!this.stModal.getSwal().isVisible()){
          this.onMinTimeLeft()
          this.openWarning = true;
        }
      }

      if(this.openWarning && !this.auth.sessionChange.value){
        this.openWarning = false;
        this.stModal.getSwal().close();
      }

      if(this.auth.sessionChange.value){
        this.remaining = this.remaining- 1000
      }
      
      //console.log(this.remaining/1000, ' -- remain')

      if(this.remaining <= 0 && this.auth.sessionChange.value){
        this.logout()
      }

      this.mouseMoved = false;
    
  }
  initStarted = false;
  subscriptions:Subscription[] = []

  docListeners:{ event:string, listener: any}[] = []
  init(){
    this.remaining = this.timeoutMinutes*60000;

    this.idle.setIdle((this.timeoutMinutes-1.2)*60)
    this.idle.setTimeout(this.timeoutMinutes * 60)
    this.idle.setInterrupts(DEFAULT_INTERRUPTSOURCES);
    
    this.keepalive.interval(5);
    this.subscriptions.forEach(s=>s.unsubscribe())
    this.subscriptions = [
    this.idle.onIdleEnd.subscribe(() => { 
      //console.log('Idle End' , this.lastDate)
//      //console.log()
      this.init()
    }),

    this.idle.onTimeout.subscribe(() => {
      //console.log('Timeout' , this.lastDate)
      this.logout()
    }),

    this.idle.onInterrupt.subscribe(()=>{
      if(!this.idle.isIdling()){
        //console.log('Interrupt' , this.lastDate)
        this.idle.setAutoResume(AutoResume.notIdle)
        this.reset()
      }
      
    }),

    
    this.idle.onIdleStart.subscribe(() => {
        this.openWarning = true
        this.remaining = 60000
        //console.log('Idle Start Warning' , this.lastDate)

        this.onMinTimeLeft()
    }),
    
    this.keepalive.onPing.subscribe(() =>{
      //console.log('Keeping alive')
     
      // let seconds = moment(this.lastDate).diff(moment(new Date()),'seconds');
      // this.remaining =  this.remaining - (-seconds*1000)
      // if(this.remaining < 0){ this.remaining = 0}
      // this.lastDate = new Date()


    

    })]


    this.docListeners.forEach(dl=>{
      document.removeEventListener(dl.event, dl.listener)
    })

    this.docListeners = [
      { event: 'mousemove', listener: ()=>this.idle.interrupt()},
      { event: 'click', listener: ()=>{
        this.idle.interrupt()
        if(this.idle.isIdling()){ this.idle.onIdleEnd.emit()}
      }}
    ]

    this.docListeners.forEach(dl=>{
      document.addEventListener(dl.event, dl.listener)
    })
  
    

    this.reset()
    //console.log('Idle timer stared')

    //console.warn('Timer started with ', (this.timeoutMinutes).toFixed(2), 'mins')
    // if(!this.initStarted){
    //   setInterval(this.intervalHandler, 1000)
    //   this.initStarted = true
      
    // }
    this.openWarning = false;
    
  }

  logout(){
    this.auth.logOut()
    //this.router.navigateByUrl('/login')
  
  }

  t(text:string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }

  lastDate=new Date()
  reset(){
  //  this.openWarning = false;
    this.lastDate=new Date()
    this.idle.setIdle((this.timeoutMinutes-1)*60)
    this.idle.setTimeout(this.timeoutMinutes * 60)

    
    this.idle.watch();
    this.remaining = this.timeoutMinutes*60000;
    this.idle.setAutoResume(AutoResume.notIdle)
  }

  mouseMoved = false;
  openWarning = false;
  remaining = 60000;
  timerInterval: any;
  onMinTimeLeft() {
    this.lastDate = new Date()
    const interval = setInterval(()=>{

      // let seconds = moment(this.lastDate).diff(moment(new Date()),'seconds');
      // this.lastDate = new Date();
      
      // this.remaining =  this.remaining - (-seconds*1000)
      // if(this.remaining <=0){
      //   this.remaining = 0;
      //   clearInterval(interval)
      // }
//      //console.log('Check' , this.lastDate, 'remain', this.remaining)

      this.swalHandler()


    },500)

    //this.swalHandler()


    this.stModal.getSwal().fire({
      html: this.getWarningHtml(this.remaining),
      timer: 60000,
      closeOnClickOutside: false
      
    })
    this.stModal.getSwal().disableButtons()  
    const content = this.stModal.getSwal().getContainer();
    if(content){
      //content.classList.add('no-button')
      const button = content.querySelector('.swal2-confirm');

        if(button){
          button.style.setProperty('display', 'none')
        }
    }
  
  }

  getRemaining(remaining: number){
    let mins = Math.trunc(remaining/60000)
    let secs =  Math.trunc((remaining/60000 - mins)*60)
    //let secs = this.remaining - mins* 60000
    return [mins, secs]
  }

  getRemainingString(time:any){
    const [mins, secs] = this.getRemaining(time);
    return `${mins?mins:''} ${mins?this.t('MINS'):''} ${secs} ${this.t('SECS')}`
  }

  getWarningHtml(time=60000){

    const t = (k:string)=>this.t(k)

    return `<div class="container-fluid" style="color: #777;">
    <div class="row">
        <div class="col-12 text-center">
            <div class="d-flex"> <span style="font-size: 32px; color: red; margin: auto; margin-right: .5em"><i class="far fa-clock"></i></span><span style="margin-left: 0 !important" class="m-auto d-block">${t('YOURONLINESESSIONWILLEXPIREIN')}</span></div>
        </div>
        <div class="col-12 mt-2 text-center" style="font-size: x-large;">
            <h1 class="remaining">${this.getRemainingString(this.remaining)}</h1>
        </div>
        <div class="col-12 mt-2 text-center">
            <p>${t('PLEASECLICKLOGINORLOGOUT')}</p>
        </div>
       
        <div class="col-12 mt-4">
            <button id="continue-btn" class="m-auto mr-0 btn btn-success" style="color: white !important; background-image: none !important; min-width: 7em">${t('CONTINUE')}</button>
            <button id="logout-btn" class="m-auto mr-0 btn btn-danger" style="color: white !important; background-image: none !important; min-width: 7em">${t('LOGOUT')}</button>
        </div>
       
        
    </div>
</div>`
  }
  
}
