import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import { ArticulosService } from 'src/app/shared/services/articulos.service';

@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.scss']
})
export class GraphComponent implements OnInit {

  articulos:any;

  constructor(public authService: AuthService,private articulosServicio: ArticulosService) { }

 

  ngOnInit(): void {
    this.recuperarTodos();
  }

  recuperarTodos() {
    this.articulosServicio.numerodeSolicitantes().subscribe((result:any) => this.articulos = result);
    //console.log(this.articulos);
  }
  

}
