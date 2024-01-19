import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import { ArticulosService } from 'src/app/shared/services/articulos.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})

export class DashboardComponent implements OnInit {

  articulos:any;
  
  art={
  firstname:"",
  initial:"",
  lastname:"",
  lastname2:"",
  streetmailbox:"", 
  state:"",
  town1:"", 
  zipcode1:"", 
  telephone:"", 
  cellphone:"", 
  email:"",
  firstnameemergency:"", 
  initialnameemergency:"" ,
   lastnameemergency:"", 
   lastname2emergency:"", 
   streetmailbox1:"", 
   state1:"", 
   town:"", 
   zipcode:"", 
   telephoneemergency:"", 
   cellphoneemergency:"", 
   relationshipemergency:""
  }

  constructor(public authService: AuthService,private articulosServicio: ArticulosService) {}

  ngOnInit(): void {}


  modificacionprofile() {
    this.articulosServicio.modificacionprofile(this.art).subscribe((datos:any) => {
      if (datos['resultado']=='OK') {
        alert(datos['mensaje']);
      }
    });    
  }
  
  seleccionar(codigo:number) {
    this.articulosServicio.seleccionar(codigo).subscribe((result:any) => this.art = result[0]);
  }

  hayRegistros() {
    return true;
  } 


}
