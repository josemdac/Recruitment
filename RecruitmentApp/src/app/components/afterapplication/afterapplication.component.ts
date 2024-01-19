import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';

@Component({
  selector: 'app-afterapplication',
  templateUrl: './afterapplication.component.html',
  styleUrls: ['./afterapplication.component.scss']
})
export class AfterapplicationComponent implements OnInit {

  constructor(public authService: AuthService) { }

  ngOnInit(): void {
  }

}
