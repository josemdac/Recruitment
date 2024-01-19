import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import { ArticulosService } from 'src/app/shared/services/articulos.service';

@Component({
  selector: 'app-myapplications',
  templateUrl: './myapplications.component.html',
  styleUrls: ['./myapplications.component.scss']
})
export class MyapplicationsComponent implements OnInit {

  articulos:any;
  
  art={
    codigo:0,
    descripcion:"",
    location:"",
    department:""
    
  }

  constructor(public authService: AuthService,private articulosServicio: ArticulosService) { }

  ngOnInit(): void {
    this.recuperarTodos();
  }
  recuperarTodos() {
    this.articulosServicio.recuperarTodos().subscribe((result:any) => this.articulos = result);
  }

  hayRegistros() {
    return true;
  } 


}
