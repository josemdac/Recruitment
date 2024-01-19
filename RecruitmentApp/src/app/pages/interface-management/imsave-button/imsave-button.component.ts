import { Component, OnInit } from '@angular/core';
import { IManagementService } from '../imanagement.service';

@Component({
  selector: 'app-imsave-button',
  templateUrl: './imsave-button.component.html',
  styleUrls: ['./imsave-button.component.scss']
})
export class IMSaveButtonComponent implements OnInit {

  constructor(private imserv: IManagementService) { }

  ngOnInit(): void {
  }

  get validSession(){
    return this.imserv.sessionValid.value
  }

  save(event:MouseEvent){
    event.preventDefault()
    //console.log(this.imserv.formData)

    this.imserv.saveConfiguration()
  }

}
