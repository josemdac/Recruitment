import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPasswordComponent } from 'src/app/components/login/reset-password/reset-password.component';

@Component({
  selector: 'app-reset-password-form',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordFormComponent implements OnInit {

  constructor(private router:Router){}
  @ViewChild(ResetPasswordComponent) resetPassComp?: ResetPasswordComponent

  ngOnInit(): void {
  }
  
  ngAfterViewInit(){
    if(this.resetPassComp){
      this.resetPassComp.state = 'reset'
    }
  }

  backToHome(ev:any){
    this.router.navigate(['/'])
  }


}
