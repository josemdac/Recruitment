import { Component, OnInit } from '@angular/core';
import { MyAccountService } from '../my-account.service';

@Component({
  selector: 'app-account-save',
  templateUrl: './account-save.component.html',
  styleUrls: ['./account-save.component.scss']
})
export class AccountSaveComponent implements OnInit {

  constructor(private myaccount: MyAccountService) { }

  ngOnInit(): void {
  }

  saveAccount(event:any){
    event.preventDefault()
    this.myaccount.saveAccount.emit()
  }
}
