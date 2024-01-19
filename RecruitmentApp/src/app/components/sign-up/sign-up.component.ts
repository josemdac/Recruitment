import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import { UserregisterService } from 'src/app/shared/services/userregister.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
})

export class SignUpComponent implements OnInit {
  registros:any;
  reg={
    id:0,
    EmailAddress:"",
    Password:"",
    FirstName:"",
    LastName:"",
    Country:"Venezuela",
    PhoneNumber:"",
  }

  constructor(public authService: AuthService, private regisrter: UserregisterService) {}

  ngOnInit() {}

  
  resultado!: string;

  formularioContacto = new FormGroup({
    nombre: new FormControl('', [Validators.required, Validators.minLength(10)]),
    lastname: new FormControl('', [Validators.required, Validators.minLength(10)]),
    mail: new FormControl('', [Validators.required, Validators.email]),
    mailconfirmation: new FormControl('', [Validators.required, Validators.email]),
    /*mensaje: new FormControl('', [Validators.required, Validators.maxLength(500)]) */
  });

/*   submit() {
    if (this.formularioContacto.valid)
      this.resultado = "Todos los datos son válidos";
    else
      this.resultado = "Hay datos inválidos en el formulario";
  } */

  altauser() {
    this.regisrter.alta(this.reg).subscribe((datos:any) => {
      if (datos['resultado']=='OK') {
        alert(datos['mensaje']);
        //console.log(this.reg);
       /*  this.recuperarTodos(); */
      }
    });
  }
}
