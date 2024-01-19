import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import { PostService } from 'src/app/shared/services/post.service';
import { ArticulosService } from 'src/app/shared/services/articulos.service';


@Component({
  selector: 'app-applications',
  templateUrl: './applications.component.html',
  styleUrls: ['./applications.component.scss']
})
export class ApplicationsComponent implements OnInit {

  posts:any;
  p: number = 1;
  term : any; 
  departmen: any;
  n:any;
  d:any;

  location : any; 

  articulos:any;
  
  art={
    codigo:0,
    descripcion:"",
    location:"",
    department:""
    
  }

  constructor(public authService: AuthService,private service:PostService,private articulosServicio: ArticulosService) { }

  
  mostrar(i:any){
    
    this.service.getPosts()
    .subscribe(response => {
      this.posts = response;
      this.n = this.posts[i].positionDescription;
      this.d= this.posts[i].positionDetailsEnglish;
      this.art.descripcion = this.posts[i].positionDescription;
      this.art.location = this.posts[i].locationProfile.englishDescription;
      this.art.department = this.posts[i].expertiseProfile.spanishDescription;
      //console.log(this.posts[i].positionDescription);  
    });
  }

  ngOnInit(): void {

    this.service.getPosts()
    .subscribe(response => {
      this.posts = response;
      //console.log(this.posts);
      
    });

    this.recuperarTodos()

  }

  recuperarTodos() {
    this.articulosServicio.recuperarTodos().subscribe((result:any) => this.articulos = result);
  }
  

  alta() {
    this.articulosServicio.alta(this.art).subscribe((datos:any) => {
      if (datos['resultado']=='OK') {
        alert(datos['mensaje']);
        this.recuperarTodos();
      }
    });
  }

  baja(codigo:number) {
    this.articulosServicio.baja(codigo).subscribe((datos:any) => {
      if (datos['resultado']=='OK') {
        alert(datos['mensaje']);
        this.recuperarTodos();
      }
    });
  }

  modificacion() {
    this.articulosServicio.modificacion(this.art).subscribe((datos:any) => {
      if (datos['resultado']=='OK') {
        alert(datos['mensaje']);
        this.recuperarTodos();
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
