import { EventEmitter, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CoSecurityDefinitionDTO } from 'src/app/shared/models/CoSecurityDefinition.model';
import { HrRcrtApplicantListItem } from 'src/app/shared/models/HrRcrtApplicant.model';
import { HrRcrtUserUpdateDTO } from 'src/app/shared/models/HrRcrtUser.model';
import { HrRcrtUserDocumentListItemDTO } from 'src/app/shared/models/HrRcrtUserDocument.model';
import { HrRcrtUserEducationListItemDTO } from 'src/app/shared/models/HrRcrtUserEducation.model';
import { HrRcrtUserEmploymentHistListItemDTO } from 'src/app/shared/models/HrRcrtUserEmploymentHist.model';
import { HrRcrtUserLanguageListItemDTO } from 'src/app/shared/models/HrRcrtUserLanguages';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';

@Injectable({
  providedIn: 'root'
})
export class MyAccountService {


  saveAccount = new EventEmitter()
  constructor(private jsonC:JsonConfigService) { }

  currentUser = new BehaviorSubject<HrRcrtUserUpdateDTO>({} as any)
  loadUser(){
    this.jsonC.get<HrRcrtUserUpdateDTO>('HrRcrtUser/Current').subscribe(user=>{
      this.currentUser.next(user)
    })
  }

  loadUserDocs(){
    return this.jsonC.get<HrRcrtUserDocumentListItemDTO[]>('HrRcrtUserDocument')
  }


  getUserDoc(documentId: any){
    return this.jsonC.get<HrRcrtUserDocumentListItemDTO>('HrRcrtUserDocument/'+documentId)
  }

  deleteDoc(id: any){
    return this.jsonC.delete('HrRcrtUserDocument/'+id,{})
  }

  saveDoc(doc:HrRcrtUserDocumentListItemDTO){
    if(doc.documentId){
      return this.jsonC.put('HrRcrtUserDocument/'+doc.documentId, doc)
    }else{
      return this.jsonC.post('HrRcrtUserDocument', doc)
    }
  }

  /** Emp Hist */
  
  loadEmpHist(){
    return this.jsonC.get<HrRcrtUserEmploymentHistListItemDTO[]>('HrRcrtUserEmploymentHist')
  }
  getEmpHIst(employmentId: any){
    return this.jsonC.get<HrRcrtUserEmploymentHistListItemDTO>('HrRcrtUserEmploymentHist/'+employmentId)
  }

  deleteEmpHist(id: any){
    return this.jsonC.delete('HrRcrtUserEmploymentHist/'+id,{})
  }

  saveEmpHist(doc:HrRcrtUserEmploymentHistListItemDTO){
    if(doc.employmentId){
      return this.jsonC.put('HrRcrtUserEmploymentHist/'+doc.employmentId, doc)
    }else{
      return this.jsonC.post('HrRcrtUserEmploymentHist', doc)
    }
  }


  /** HrApp */
  loadHrApp(){
    return this.jsonC.get<HrRcrtApplicantListItem[]>('HrRcrtApplicant')
  }
 
  deleteHrApp(id: any){
    return this.jsonC.delete('HrRcrtApplicant/'+id,{})
  }

  /** HrEduc */
  loadHrEduc(){
    return this.jsonC.get<HrRcrtUserEducationListItemDTO[]>('HrRcrtUserEducation')
  }
  getHrEduc(employmentId: any){
    return this.jsonC.get<HrRcrtUserEducationListItemDTO>('HrRcrtUserEducation/'+employmentId)
  }

  deleteHrEduc(id: any){
    return this.jsonC.delete('HrRcrtUserEducation/'+id,{})
  }

  saveHrEduc(doc:HrRcrtUserEducationListItemDTO){
    if(doc.educationId){
      return this.jsonC.put('HrRcrtUserEducation/'+doc.educationId, doc)
    }else{
      return this.jsonC.post('HrRcrtUserEducation', doc)
    }
  }

  /** HrLang */
  loadHrLang(){
    return this.jsonC.get<HrRcrtUserLanguageListItemDTO[]>('HrRcrtUserLanguage')
  }
  getHrLang(employmentId: any){
    return this.jsonC.get<HrRcrtUserLanguageListItemDTO>('HrRcrtUserLanguage/'+employmentId)
  }

  deleteHrLang(id: any){
    return this.jsonC.delete('HrRcrtUserLanguage/'+id,{})
  }

  saveHrLang(doc:HrRcrtUserLanguageListItemDTO){
    if(doc.recordId){
      return this.jsonC.put('HrRcrtUserLanguage/'+doc.recordId, doc)
    }else{
      return this.jsonC.post('HrRcrtUserLanguage', doc)
    }
  }

  saveProfile(user:any){
    this.jsonC.put<HrRcrtUserUpdateDTO>('HrRcrtUser', user).subscribe((userS:any)=>{
      Object.keys(userS).forEach((k:string)=>{
        user[k] = userS[k]
      })
    })
  }


  getSecDef(){
    return this.jsonC.get<CoSecurityDefinitionDTO>('CoSecurityDefinition/Current')
  }

  validatePassword(password: string){
    return this.jsonC.post<{valid: Boolean}>('HrRcrtUser/ValidatePassword',{ password })
  }

  changePassword(data:any){
    this.jsonC.put<HrRcrtUserUpdateDTO>('HrRcrtUser/ChangePassword', data).subscribe((userS:any)=>{
      data.newPassword = '';
      data.oldPassword = '';
      data.confirmPassword = '';  
    })
  }
}
