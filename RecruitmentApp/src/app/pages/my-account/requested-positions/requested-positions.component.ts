import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { StandardModalService } from 'src/app/layout/standard-modal/standard-modal.service';
import { HrRcrtApplicantListItem } from 'src/app/shared/models/HrRcrtApplicant.model';
import { MyAccountService } from '../my-account.service';

@Component({
  selector: 'app-requested-positions',
  templateUrl: './requested-positions.component.html',
  styleUrls: ['./requested-positions.component.scss']
})
export class RequestedPositionsComponent implements OnInit {

  constructor(private myaccount:MyAccountService, 
    private stModal:StandardModalService, private translate: TranslateService) { 
      this.t = this.t.bind(this)
    }

  ngOnInit(): void {
    this.reloadList()
  }

  t(text: string){
    return this.translate.getParsedResult(this.translate.defaultLang, text)
  }

  toDelete?: any
  items: HrRcrtApplicantListItem[] = []
  
  public removeHandler(dataItem:any): void {
    this.toDelete = dataItem
    this.stModal.deletePrompt({
      deleteAction: ()=>new Observable<any>((obs)=>{
        this.myaccount.deleteHrApp(dataItem.applicantId).subscribe(()=>{
            this.reloadList()
            obs.next({})
        }) 
      }),
      errorAction: ()=>{ this.cancelDelete() },
      translate: this.t
    })

  }

  cancelDelete(){
    this.toDelete = null
    this.stModal.hide()
  }

  reloadList(){
    this.myaccount.loadHrApp().subscribe(items=>{
      this.items = items
    })
  }
}
