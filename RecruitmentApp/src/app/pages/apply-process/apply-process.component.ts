import { ChangeDetectorRef, Component, ContentChildren, ElementRef, OnInit, QueryList, TemplateRef, ViewChild, ViewChildren } from '@angular/core';
import { ApplyProcessStepDirective } from './apply-process-step.directive';
import { StepperActivateEvent, StepperComponent } from "@progress/kendo-angular-layout";
import { ApplyProcessService } from './apply-process.service';
import { createBlankApplicant } from 'src/app/shared/models/HrRcrtApplicant.model';
import { ActivatedRoute, Router } from '@angular/router';
import { JobListService } from '../job-list/job-list.service';
import { goToTop } from 'src/app/shared/helpers/tools';
import { StandardModalService } from 'src/app/layout/standard-modal/standard-modal.service';
import { StepperHeightDirective } from './stepper-height.directive';
import { MyAccountService } from '../my-account/my-account.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-apply-process',
  templateUrl: './apply-process.component.html',
  styleUrls: ['./apply-process.component.scss']
})
export class ApplyProcessComponent implements OnInit {

  stepperHeight = 0
  @ViewChild('buttons') buttons?:ElementRef<HTMLElement>;
  @ViewChild('title') title?:ElementRef<HTMLElement>;
  @ViewChild('perTab') perTab:any
  @ViewChild('docs') docs:any
  @ViewChild('stepper') stepper?:StepperComponent

  @ViewChildren(ApplyProcessStepDirective) steps?: QueryList<ApplyProcessStepDirective>
  currentStep?:any
  constructor(private cdr: ChangeDetectorRef, private stModal: StandardModalService,
    public jobserv: JobListService, public router: Router,
    public elRef: ElementRef<HTMLElement>, private myaccount:MyAccountService, 
    public route:ActivatedRoute,public proc: ApplyProcessService) { 
    }

  loadedApplicant = false
  firstTimeLoad = false
  header?:any = {}
  subs:Subscription[] = []
  saved = false
  ngOnInit(): void {

    this.saved = false
    this.contH = this.elRef.nativeElement.getBoundingClientRect().height + 'px';
    const requestId = this.route.snapshot.paramMap.get('requestId')
    this.loadHeaderPosition()
    this.subs = [
      this.myaccount.currentUser.subscribe(user=>{
        //console.log(user)
        this.proc.formData = user as any
        if(!this.proc.formData.state){
          this.proc.formData.state = 'PR'
        }
        if(!this.proc.formData['stateStreet']){
          this.proc.formData['stateStreet'] = 'PR'
        }
      }),
      
    ]

    this.myaccount.loadUser()
    this.proc.getApplicantByReq(requestId).subscribe(applicant=>{
      this.loadedApplicant = true
      
      if(applicant && applicant.applicantId){
        
        this.proc.formData = applicant
        if(!this.proc.formData.state){
          this.proc.formData.state = 'PR'
        }
        if(!this.proc.formData['stateStreet']){
          this.proc.formData['stateStreet'] = 'PR'
        }
        //console.log('firstTimeLoad', this.firstTimeLoad)
        this.cdr.detectChanges()
        
        return 
      }
      //this.firstTimeLoad = true
      //console.log('firstTimeLoad', this.firstTimeLoad)
      //this.proc.formData = createBlankApplicant()
      this.cdr.detectChanges()
    })

    this.proc.loadQuestions(requestId).subscribe(q=>{
      this.proc.questions = q as any
      if(this.stepper){
        this.stepper.ngOnChanges({})
      }
    })
    
   
    goToTop()
  }

  ngOnDestroy(){
    this.subs.forEach(s=>s.unsubscribe())
  }
  contH = '0px'
  get noQuestions(){
    return !this.proc.questions || (this.proc.questions && !this.proc.questions.length)
  }
  get minHeight(){ 
    return ''
    if(!this.buttons|| !this.title){
      return ''
    }
    
    const stepH = '6em';
    const btnsH = this.buttons?.nativeElement.getBoundingClientRect().height + 'px';
    const titlH = this.title?.nativeElement.getBoundingClientRect().height + 'px';

    //console.log(contH, stepH, btnsH, titlH)
    const h = `calc(${this.contH} - ${stepH?stepH:'0px'} - ${btnsH?btnsH:'0px'} - ${titlH?titlH:'0px'})`

    return h
  }

  loadHeaderPosition(){
    const id  = this.route.snapshot.paramMap.get('requestId')
    if(id){
      this.jobserv.loadHeaderPosition(id).subscribe(p=>{
        if(!p){
          this.router.navigateByUrl('/joblist')
        }
        this.header = p
      })
    }
  }

  current = 0

  get loadedNew(){
    return this.loadedApplicant && !this.proc.formData.applicantId
  }

  get nextDisabled(){
    return (this.currentStep.code == 'PERS' && !(this.proc.formData.firstName && this.proc.formData.lastName1))
  }

  get isLastStep(){
    if(!this.steps?.last){
      return false
    }
    return this.currentStep == this.steps.last
  }
  

  get stepperSteps(){
    if(this.steps){
      return this.steps.toArray().filter(s=>!s.hidden)
    }
    return []
    
  }
  stepChange(event:number){

    const currentStep = this.steps?.toArray().filter(s=>!s.hidden)[this.current]
    if(event > this.current){
      if(currentStep && currentStep.validate && !currentStep.validate()){
        if(event > 0){
          //console.log(this.stepper, 'To Step: ', event)
          this.current = event - 1
          this.currentStep = this.steps?.toArray().filter(s=>!s.hidden)[event - 1]
          
          setTimeout(()=>{ 
            if(this.stepper){
              this.stepper.currentStep = event - 1
            }
          }, 100)
          
          return
        }

        //console.log('event back', event)
       
      }
    }
    //console.log(currentStep)
    this.current = event
    this.currentStep = this.steps?.toArray().filter(s=>!s.hidden)[event]
    //console.log(this.currentStep)
  }

  activate(event:StepperActivateEvent){
    //console.log(event)
    //this.currentStep = event.step
    this.cdr.detectChanges()
    //console.log(event)
  }

  ngAfterViewInit(){
   // //console.log(this.steps)
    this.currentStep = this.steps?.toArray().filter(s=>!s.hidden)[0]
    this.cdr.detectChanges()
  }

  saveApplicant(event: any){
    event.preventDefault()
    this.proc.formData.requestId = this.route.snapshot.paramMap.get('requestId') as  any
    this.proc.formData.ofccpDisability = 'N'
    this.proc.formData.ofccpEthnicity = 'N'
    this.proc.formData.ofccpGender = 'N'
    this.proc.formData.ofccpVeteran = 'N'

    //console.log(this.proc.formData)

    const saveQuetions = ()=>{
      const close = this.stModal.showLoading()
      try{
        
        this.proc.saveQuestions(this.proc.questions).subscribe(applicant=>{
          this.proc.formData = applicant as any
          close()
        }, error =>{
          //console.log(error)
          close()
        })
      }catch(error){
        close()
      }
      
    }
    
    const close = this.stModal.showLoading()
    try{


      this.proc.saveAppicant().subscribe(applicant=>{
        this.proc.formData = applicant as any
        this.saved = true
        saveQuetions()
        close()
      }, error =>{
        //console.log(error)
        close()
      })
    }catch(error){
      close()
    }
    
  }

}
