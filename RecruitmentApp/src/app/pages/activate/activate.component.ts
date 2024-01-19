import { HttpHeaders, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JsonConfigService } from 'src/app/shared/services/json-config.service';

@Component({
  selector: 'app-activate',
  templateUrl: './activate.component.html',
  styleUrls: ['./activate.component.scss']
})
export class ActivateComponent implements OnInit {

  constructor(private route: ActivatedRoute, private jsonc: JsonConfigService, private router: Router) { }

  activationError = false
  attempts = 0
  activated = false
  error = ''
  ngOnInit(): void {
    const token = this.route.snapshot.paramMap.get('token')
    if(token){
      this.activateUser(token)
      return 
    }

    this.activationError = true
  }

  activateUser(token:string){
    let headers:any = new HttpHeaders({ 'ActivationToken': token});
    
    this.jsonc.postHeader<{activated: boolean, error:string}>('HrRcrtUser/Activate', {}, { headers })
    
    . pipe().subscribe((act:any)=>{
      //console.log('act: ' , act)
      if(act.resetPassword){

        if(!act.validToken){
          this.router.navigate(['/'])
        }else{
          this.router.navigate(['/reset-password', { t: token }])
        }
        
      }
      this.attempts++;
      this.activated = act.activated       
      this.error = act.error
      this.activationError = !!this.error
    })
  }


  get isLoading(){
    return !this.activationError && this.attempts == 0 && !this.activated
  }

  get success(){
    return !this.activationError && this.activated && this.attempts > 0
  }


}
