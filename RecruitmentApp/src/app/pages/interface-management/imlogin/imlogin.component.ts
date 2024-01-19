import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { IManagementService } from '../imanagement.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-imlogin',
  templateUrl: './imlogin.component.html',
  styleUrls: ['./imlogin.component.scss']
})
export class IMLoginComponent implements OnInit {

  constructor(public imservice: IManagementService, 
    private r:ActivatedRoute,
    private translate: TranslateService) { }

  ngOnInit(): void {

   
  }

  


  get showLogin(){
    return !this.imservice.sessionValid.value && !(this.r.snapshot.queryParams['tauth'] == 'yes')
  }

  onSubmit(event: any){
    event.preventDefault()
    this.imservice.loginIM(this.loginState.password, this.loginState)
  }
  loginState = {
    attempts: 0,
    password: '',
    loading: false,
    error: ''
  }
  onFocus(event:any){
    event?.target?.removeAttribute('readonly')
  }

  get lang(){
    return {
      'English': 'en',
      'Español':'es'
    }[this.translate.defaultLang]
  }
}
