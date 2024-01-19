import { ElementRef, EventEmitter, Injectable, TemplateRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

//@ts-ignore
import Swal from 'ngx-angular8-sweetalert2';
import { Observable } from 'rxjs';


export interface IDeletePrompt {
  deleteAction: ()=>Observable<any>,
  errorAction?: (...args:any[])=>void,
  translate: (input: string) => string

}

@Injectable({
  providedIn: 'root'
})
export class StandardModalService {

  
  onHide = new EventEmitter()
  modalRef?:ElementRef<HTMLElement>;
  constructor(private translate: TranslateService) { }
  accountForm = false;
  showModal = false;
  dragHeader =false;
  hide(){
    this.header = undefined;
    this.footer = undefined;
    this.content = undefined;
    this.submitFcn = undefined;
    this.customClass = '';
    this.showModal = false;
    this.accountForm = false;
    this.dragHeader = false;
    this.width="600px";
    this.ptop = '7em'
    this.top = undefined
    this.onHide.emit()
  }

  header: any;
  footer: any;
  content: any;
  submitFcn: any;
  customClass = ''
  width  = '600px'
  ptop = '7em'
  top?:string
  onSetModalRef = new EventEmitter()
  show(options:{header?:TemplateRef<any>, content?: TemplateRef<any>, footer?: TemplateRef<any>, onSubmit?: (form?:any)=>void, customClass?: string, width?: any, dragHeader?: boolean, pTop?: any, top?: any}){
    this.header = options.header;
    this.footer = options.footer;
    this.content = options.content;
    this.submitFcn = options.onSubmit;
    this.customClass = options.customClass?options.customClass:'';
    this.width = options.width?options.width:'600px'
    this.showModal = true;
    this.dragHeader = options.dragHeader?true:false
    this.ptop = options.pTop?options.pTop:'7em'
    this.top = options.top
  } 

  centerModal(){
    setTimeout(()=>{
      const cls = this.customClass?('.'+this.customClass ):''
      const modal =  document.querySelector(cls + '.standard-modal .container-fluid');
      if(modal){
        let th = window.innerHeight;
        let ch = modal.getBoundingClientRect().height;
        let initialTop = (th- ch)/2
        this.top = initialTop+'px'
      }
    },10)
  }

  loads:any[]  = []
  showLoading(){
    const handler = ()=>{
      const translate = (text:string)=>{
        return this.translate.getParsedResult(this.translate.defaultLang, text)
      }
      Swal.fire({ title: translate('LOADING'), customClass: 'success-modal' });
      Swal.showLoading();
    }
    this.loads.push(handler)
    if(!Swal.isVisible()){
      handler()
    }
    
    return ()=>this.closeLoading(handler)
  }

  closeLoading(handler: any){
    this.loads = this.loads.filter(l=>l!=handler)
    if(!this.loads.length){
      Swal.close()
    }
  }
  
  getSwal(){
    return Swal;
  }
  showError({ title, message }:any){
    return Swal.fire({
      title,
      text: message,
      icon: 'error',
      showCancelButton: false,
      showConfirmButton: true
    })
  }
  deletePrompt = ({deleteAction, errorAction, translate}:IDeletePrompt)=>{
  
    var notifyMsg = translate('CONFIRM_DELETE');
    var notifyTitle = translate('NOTICE');
    var YESbtn = translate('YES');
    var NObtn = translate('NO');

    var SUCCESSFULLY_DELETED = translate('SUCCESSFULLY_DELETED');
    var ERROR_DELETE = translate('ERROR_TRYING_TO_DELETE_RECORD');
    var DELETE = translate('DELETE');
    var CANCEL = translate('CANCEL');

    Swal.fire({
      title: notifyTitle,
      text: notifyMsg,
      icon: 'warning',
      showCancelButton: true,
      showConfirmButton: true,
      confirmButtonText: YESbtn,
      cancelButtonText: NObtn
    }).then((result:any) => {
      if (result.value) {
        Swal.fire({ title: translate('LOADING'), customClass: 'success-modal' });
        Swal.showLoading();
        deleteAction()
        .subscribe(res => {
            
            Swal.fire(
              DELETE,
              SUCCESSFULLY_DELETED,
              'success'
            );
          },
            err => {
                if(errorAction){
                    errorAction(err)
                }
                Swal.fire(
                  DELETE,
                  ERROR_DELETE,
                  'error'
                );
              
            })
      } else if (result.dismiss === Swal.DismissReason.cancel) {

        Swal.close()
        // Swal.fire(
        //   CANCEL,
        //   translate('CANCELLED'),
        //   'error'
        // )
      }
    })

}


}
