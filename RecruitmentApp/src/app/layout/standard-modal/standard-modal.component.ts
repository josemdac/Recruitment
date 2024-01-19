import { Component, ElementRef, OnInit } from '@angular/core';
import { StandardModalService } from './standard-modal.service';

@Component({
  selector: 'standard-modal',
  templateUrl: './standard-modal.component.html',
  styleUrls: ['./standard-modal.component.scss']
})
export class StandardModalComponent implements OnInit {

  
  dragHeader = false
  
  
  constructor(public stModalService: StandardModalService, public elementRef: ElementRef<HTMLElement>) { }

  ngOnInit(): void {
    this.stModalService.modalRef = this.elementRef;
    this.stModalService.onSetModalRef.emit()
  }


  cancel(event:any){
    event.preventDefault()
    this.stModalService.hide()
  }

  submit(form?:any){
    if(this.stModalService.submitFcn){
      this.stModalService.submitFcn(form)
    }
  }

}
