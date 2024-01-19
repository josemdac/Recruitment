import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[dragModal]'
})
export class DragModalDirective {

  headerElement: HTMLDivElement;
  containerElement?: HTMLDivElement;
  modalRect?:DOMRect;
  @Input() width = '500px'
  @Input() dragHeader = false
  mouseMoveListener?: (event:MouseEvent)=>void
  constructor(private elementRef: ElementRef<HTMLDivElement>) {
    this.headerElement = elementRef.nativeElement;
    this.headerElement.style.setProperty('position', 'fixed', 'important')
    this.headerElement.style.setProperty('width', this.width)
    // this.headerElement.style.setProperty('top', '0');
    // this.headerElement.style.setProperty('bottom', '0');
    // this.headerElement.style.setProperty('left', '0');
    // this.headerElement.style.setProperty('right', '0');
    
  }
  movable = false;


  ngOnInit(){

    
    this.mouseMoveListener = (event)=>this.onHeaderMouseMove(event)
    document.addEventListener('mousemove', this.mouseMoveListener)
  }

  ngOnDestroy(){
    if(this.mouseMoveListener){
      document.removeEventListener('mousemove', this.mouseMoveListener)
    }
  }

  ngOnChanges(){
    
    this.headerElement.style.setProperty('width', this.width)
    let width = this.headerElement.clientWidth
    let screenWidth = window.innerWidth;
    let leftPos = (screenWidth - width)/2
    this.headerElement.style.setProperty('left', leftPos+ 'px' )
  }


  ngAfterViewInit(){
    this.setNotSelectable(this.headerElement)
    
    this.headerElement.style.cursor = 'move';
    //this.containerElement.style.position = 'fixed'
    this.modalRect = this.headerElement.getBoundingClientRect();
    
  
  }

  refOffset?: {x: number, y: number}
  initialMousePos?:{x:number, y: number}

  @HostListener('mousedown', ['$event'])
  onHeaderMouseDown(event: any){

    //if(this.dragHeader){
      let header = this.elementRef.nativeElement.querySelector('.header')

      if(header && !header.contains(event.target)){
        return 
      }
    
    //}
    event.stopPropagation()
    if(event.button == 2 || (!this.headerElement.contains(event.target))){
      return 
    }

    var rect = this.headerElement.getBoundingClientRect();
    var x = event.clientX - rect.left; //x position within the element.
    var y = event.clientY - rect.top;  //y position within the element.

    this.refOffset = { x, y }
    if(!this.initialMousePos){
        this.initialMousePos = { x: event.clientX, y: event.clientY }
    }
    
    this.movable = true;

  }
  @HostListener('mouseleave', ['$event'])
  onHeaderMouseLeave(){
    //this.containerElement.draggable = false;
  }

  @HostListener('mouseup', ['$event'])
  onHeaderMouseUp(event:any){
    event.stopPropagation();
    this.movable = false;
  }

  setNotSelectable(el:HTMLElement){
    el.style.setProperty('user-select', 'none');

    const checkChildren = (el: HTMLElement)=>{
      //@ts-ignore
      Array.from(el.children).forEach((child:HTMLElement)=>{
        child?.style?.setProperty('user-select', 'none')
        checkChildren(child)
      })
    }

    checkChildren(el);
    

  }

  onHeaderMouseMove(event:MouseEvent){
    if(this.movable && this.initialMousePos && this.refOffset){
      ////debugger;
      // let oSy = (window.innerHeight - modalRect.height)/2;
      // let oSx = (window.innerWidth - modalRect.width)/2;

      let moveX = this.refOffset.x - event.clientX;
      let moveY = this.refOffset.y - event.clientY
   

      this.headerElement.style.top = (-moveY) + 'px';
      this.headerElement.style.left = (-moveX) + 'px';
    }
  }

}
