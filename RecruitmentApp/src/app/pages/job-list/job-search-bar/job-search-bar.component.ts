import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { text } from '@fortawesome/fontawesome-svg-core';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject } from 'rxjs';
import { FormInputComponent } from 'src/app/components/form-input/form-input.component';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';
import { JobListService } from '../job-list.service';

@Component({
  selector: 'app-job-search-bar',
  templateUrl: './job-search-bar.component.html',
  styleUrls: ['./job-search-bar.component.scss']
})
export class JobSearchBarComponent implements OnInit {

  @ViewChildren(FormInputComponent) formInputs?:QueryList<FormInputComponent>
  constructor(private jobserv:JobListService, private translate:TranslateService,
    private route:ActivatedRoute, private jconf: JsonConfigService) { }

  ngOnInit(): void {
    if(this.location){
      this.formData.location = this.location
    }

    if(this.keyword){
      this.formData.keyword = this.keyword
    }

    this.translate.onDefaultLangChange.subscribe(()=>{

      const loadValuesLang = ()=>{
        // const isEn = this.translate.defaultLang == 'English'
        // const currentJT = this.formData['jobTypes']?this.formData['jobTypes'].map((j:any)=>j.text):[]
        // const currentLOC = this.formData['location']?this.formData['location'].map((j:any)=>j.text):[]

        // const locs = this.jobserv.data
        // .filter((l:any)=>currentLOC.includes(!isEn?l.locationEnglish:l.locationSpanish))
        // .map((j:any)=>!isEn?j.englishDescription:j.spanishDescription)

        // let jt = this.jobTypes.value.filter(j=>currentJT.includes(!isEn?j.englishDescription:j.spanishDescription))
        // let lc = locs.map((t:string)=>({ text: t, value: t, englishDescription: t, spanishDescription: t}))
  //console.log(this.formData)
        this.formData = {
          location: '',
          keyword: '',
          jobTypes: []
        }

        if(this.formInputs){
          this.formInputs.forEach(i=>i.reset())
        }
        this.setJobListItems()
        

        const valueIds = this.formData.jobTypes.map((jt:any)=>jt.value)
        if(this.fields[2].source && typeof this.fields[2].source != 'string'){
          this.formData.jobTypes = this.fields[2].source.filter(v=>valueIds.includes(v.value))
        }

        this.loadLocs()
        
        // this.changeJobType(jt)
        // this.changeLocation(lc)

      }

      loadValuesLang()
      
    })

    this.jobserv.setData.subscribe(()=>this.loadLocs())

    this.jobTypes.subscribe(()=>this.setJobListItems())
    this.locations.subscribe(()=>this.setLocListItems())

    this.loadJobTypesAndLocs()
  }

  setJobListItems(){
    const isEn = this.translate.defaultLang == 'English'
    
    this.fields[2].source = this.jobTypes.value.map(jt=>({...jt, text: isEn?jt.englishDescription:jt.spanishDescription}))
    this.jobserv.jobTypes.next(this.fields[2].source.map(j=>j.text))
  }

  setLocListItems(){
    const isEn = this.translate.defaultLang == 'English'
    
    this.fields[1].source = this.locations.value.map(jt=>({...jt, text: isEn?jt.englishDescription:jt.spanishDescription}))
    this.jobserv.location.next(this.fields[1].source.map(j=>j.value))
  }

  jobTypes = new BehaviorSubject<{ text: string, value: any, englishDescription: string, spanishDescription: string}[]>([])
  locations = new BehaviorSubject<{ text: string, value: any, englishDescription: string, spanishDescription: string}[]>([])

  formData:any = {
    location: '',
    keyword: '',
    jobTypes: []
  }

  get keyword(){
    return this.route.snapshot.paramMap.get('keyword')
  }
  get location(){
    return this.route.snapshot.paramMap.get('location')
  }

  loadJobTypesAndLocs(){
    this.jconf.get<any>('HrRcrtPositionProfileType/Dropdown?Type=JT')
      .subscribe(jtList =>{
        this.jobTypes.next(jtList)
        
      })
    
      
      this.loadLocs()
    
  }

  loadLocs(){
    const isEn = this.translate.defaultLang == 'English'
    const locs = this.jobserv.data
    .map((l:any)=>isEn?l.locationEnglish:l.locationSpanish as string)
    .filter((v:string, i: number, a: any[])=>a.indexOf(v) == i)
    .map((t:string)=>({ text: t, value: t, englishDescription: t, spanishDescription: t}))

    //console.log(locs)
    this.locations.next(locs)
  }

  changeKeyword(word:string){
    this.jobserv.keyword.next(word)
  }

  changeLocation(value:any){
    this.jobserv.location.next(value.map((v:any)=>v.value))
    if(!value.length && this.fields[1].source?.length
      && typeof this.fields[1].source == 'object'){
      this.jobserv.location.next(this.fields[1].source.map(j=>j.text))
    }
    
  }
  changeJobType(value:any){
    this.jobserv.jobTypes.next(value.map((v:any)=>v.text))
    if(!value.length && this.fields[2].source?.length
      && typeof this.fields[2].source == 'object'){
      this.jobserv.jobTypes.next(this.fields[2].source.map(j=>j.text))
    }
  }

  fields: IFormInput[] = [
    { name: 'keyword', type: 'text', maxlength: 100 },
    { name: 'location', type: 'multiselect', source: [] },
    { name: 'jobTypes', type: 'multiselect', col: 'auto', source: []}
  ]

  keyUpField(event:any, field:IFormInput){
    this.changeField(event.target.value, field)
  }

  changeField(value:string, field:IFormInput){
    //console.log(value, field)
    if(field.name == 'keyword'){
      this.changeKeyword(value)
    }

    if(field.name == 'location'){
      this.changeLocation(value)
    }

    if(field.name == 'jobTypes'){
      //console.log(value)
      this.formData.jobTypes = value
      this.changeJobType(value)
    }
  }
}
