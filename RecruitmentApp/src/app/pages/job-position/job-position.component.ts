import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { goToTop } from 'src/app/shared/helpers/tools';
import { HrRcrtPositionRequest } from 'src/app/shared/models/HrRcrtPositionRequest.model';
import { AuthSessionService } from 'src/app/shared/services/auth-session.service';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';
import { WebSiteInfoService } from 'src/app/shared/services/web-site-info.service';
import { JobListService } from '../job-list/job-list.service';

@Component({
  selector: 'app-job-position',
  templateUrl: './job-position.component.html',
  styleUrls: ['./job-position.component.scss']
})
export class JobPositionComponent implements OnInit {

  constructor(private route: ActivatedRoute, private auth:AuthSessionService,
    private router: Router, private jobserv: JobListService,
    private webconf: WebSiteInfoService,
    private translate: TranslateService, private jsons:JsonConfigService) { }

  ngOnInit(): void {
    this.loadPosition()
    goToTop()
  }
  
  position?: HrRcrtPositionRequest

  get url(){
    let token = encodeURIComponent(this.webconf.companyToken);
    let shareUrl = `${this.jsons.apiUrl}/ShareLink?Url=${encodeURIComponent(window.location.href)}&Token=${token}`
    return shareUrl
  }
  loadPosition(){
    const id  = this.route.snapshot.paramMap.get('requestId')
    if(id){
      this.jobserv.loadPosition(id).subscribe(p=>{
        if(!p){
          this.router.navigateByUrl('/joblist')
        }
        this.position = p
      })
    }else{
      this.router.navigateByUrl('/joblist')
    }
  }

  get detail(){
    if(this.translate.defaultLang == 'English'){

      return this.position?this.position.positionDetailsEnglish:''
    }

    if(this.translate.defaultLang == 'Español'){
      return this.position?this.position?.positionDetailsSpanish:''
    }
    return ''
  }

  applyNow(event: any){
    event.preventDefault()
    const id  = this.route.snapshot.paramMap.get('requestId')
    if(!this.auth.validSession){
      this.auth.callLogin('apply-process/'+ id, event.target)
      return 
    }
    
    this.router.navigate(['apply-process', { requestId: id}])
  }

}
