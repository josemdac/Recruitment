import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';

@Component({
  selector: 'app-smtpconfirmation',
  templateUrl: './smtpconfirmation.component.html',
  styleUrls: ['./smtpconfirmation.component.scss']
})
export class SmtpconfirmationComponent implements OnInit {

  constructor(public authService: AuthService) { }

  ngOnInit(): void {
  }

}
