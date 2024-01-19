import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { createBlankApplicant, HrRcrtApplicantDTO } from 'src/app/shared/models/HrRcrtApplicant.model';
import { HrRcrtApplicantDocumentListItemDTO } from 'src/app/shared/models/HrRcrtApplicantDocument.model';
import { HrRcrtRequestQuestion } from 'src/app/shared/models/HrRcrtRequestQuestion.model';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';

@Injectable({
  providedIn: 'root'
})
export class ApplyProcessService {

  constructor(private jsonC:JsonConfigService, private http:HttpClient, private translate:TranslateService) { }

  formData = createBlankApplicant()
  questions:HrRcrtRequestQuestion[] = []

  
  loadUserDocs(){
    let headers:any = new HttpHeaders({ 'ApplicantId': this.formData.applicantId+''});
    return this.http.get<HrRcrtApplicantDocumentListItemDTO[]>(this.url('HrRcrtApplicantDocument'), { headers})
  }

  url(str: string){
    return this.jsonC.apiUrl + '/'+ str
  }

  loadQuestions(requestId: any){
    let headers:any = new HttpHeaders({ 'ApplicantId': this.formData.applicantId+''});
    return this.http.get<HrRcrtRequestQuestion[]>(this.url('HrRcrtPositionRequests/Questions/' + requestId), { headers})
  }

  getApplicantByReq(requestId: any){
    return this.jsonC.get<HrRcrtApplicantDTO>('HrRcrtApplicant/ByRequest/'+ requestId)
  }
  saveAppicant(){
    if(this.formData.applicantId){
      return this.http.put(this.url('HrRcrtApplicant/'+this.formData.applicantId), this.formData)
    }else{
      const lang = this.translate.defaultLang == 'English'?'en':'es'
      //console.log(this.formData)
      return this.http.post(this.url('HrRcrtApplicant?lang=' + lang), this.formData)
    }
  }


  getUserDoc(documentId: any){
    let headers:any = new HttpHeaders({ 'ApplicantId': this.formData.applicantId+''});
    return this.http.get<HrRcrtApplicantDocumentListItemDTO>(this.url('HrRcrtApplicantDocument/'+documentId), {headers})
  }

  deleteDoc(id: any){
    let headers:any = new HttpHeaders({ 'ApplicantId': this.formData.applicantId+''});
    return this.http.delete(this.url('HrRcrtApplicantDocument/'+id),{headers})
  }

  saveDoc(doc:HrRcrtApplicantDocumentListItemDTO){
    let headers:any = new HttpHeaders({ 'ApplicantId': this.formData.applicantId+''});
    if(doc.documentId){
      return this.http.put(this.url('HrRcrtApplicantDocument/'+doc.documentId), doc, {headers})
    }else{
      return this.http.post(this.url('HrRcrtApplicantDocument'), doc, {headers})
    }
  }

  saveQuestions(q:HrRcrtRequestQuestion[]){
    let headers:any = new HttpHeaders({ 'ApplicantId': this.formData.applicantId+''});
    const q2 = q.map(x=>{
      if(typeof x.answers == 'string'){
        return { ...x, answers: [{ answer: x.answers, answerId: null}] }
      }
      return x
    }) 
    return this.http.post(this.url('HrRcrtPositionRequests/SaveQuestions'), q2, {headers})
  }
}
