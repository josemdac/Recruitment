import { Component, OnInit } from '@angular/core';
import { goToTop } from 'src/app/shared/helpers/tools';
import { MyAccountService } from './my-account.service';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.scss']
})
export class MyAccountComponent implements OnInit {

  constructor(private myaccount: MyAccountService) { }

  ngOnInit(): void {

    this.myaccount.loadUser()
    goToTop()
  }

}
