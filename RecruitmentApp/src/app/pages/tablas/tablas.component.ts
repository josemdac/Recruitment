import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-tablas',
  templateUrl: './tablas.component.html',
  styleUrls: ['./tablas.component.scss']
})
export class TablasComponent implements OnInit {

  posts:any;
  p: number = 1;
  term : any; 
  departmen: any;
  n:any;
  d:any;

  location : any; 
  constructor(private service:PostService) { 
    
  }

  mostrar(i:any){
    
    this.service.getPosts()
    .subscribe(response => {
      this.posts = response;
      this.n = this.posts[i].positionDescription;
      this.d= this.posts[i].positionDetailsEnglish
      //console.log(this.posts[i].positionDescription);  
    });
  }

  ngOnInit(): void {

    this.service.getPosts()
        .subscribe(response => {
          this.posts = response;
          //console.log(this.posts);
          
        });
    
  }

}
