import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import {RestService } from '../../shared/services/rest.service';
import { ArticulosService } from 'src/app/shared/services/articulos.service';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators} from '@angular/forms';


@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  articulos:any;

  myForm = new FormGroup({
    name: new FormControl(''),
    file: new FormControl(''),
    fileSource: new FormControl('')

  });

  private fileTmp:any;

  constructor(public authService: AuthService,private restService: RestService,private http: HttpClient,private articulosServicio: ArticulosService) { }

  

  get f(){
    return this.myForm.controls;
  }
     
  onFileChange(event:any) {
  
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.myForm.patchValue({
        fileSource: file
      });
    }
  }
     
  submit(){
    const formData = new FormData();
    formData.append('file', this.myForm.get('fileSource')?.value);
   
    this.http.post('http://localhost/articulos/upload.php', formData)
      .subscribe(res => {
        //console.log(res);
        alert('Uploaded Successfully.');
      })
  }

  recuperarTodos() {
    
    this.articulosServicio.recuperarTodos().subscribe((result:any) => this.articulos = result);
  }

  recuperarpdf() {
    
    this.articulosServicio.recuperarpdf().subscribe((result:any) => this.articulos = result);
  }

  getFile($event: any): void {
    //TODO esto captura el archivo!
    const [ file ] = $event.target.files;
    this.fileTmp = {
      fileRaw:file,
      fileName:file.name
    }
  }

  sendFile():void{

    const body = new FormData();
    body.append('myFile', this.fileTmp.fileRaw, this.fileTmp.fileName);
    body.append('email','test@test.com')

    this.restService.sendPost(body)
    .subscribe(res => 
      void(0)
      //console.log(res)
      )
  }

  ngOnInit(): void {
    this.recuperarpdf();
  }

}
