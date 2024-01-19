import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[formInputPassword]'
})
export class FormInputPasswordDirective {


  eye = document.createElement('span')
  constructor(private elRef:ElementRef<HTMLInputElement>) { }

  isPassword = false
  ngOnInit(){
    this.isPassword = this.elRef.nativeElement.type == 'password'
    this.setUp()
  }

  setUp(){
    if(this.isPassword){
      this.eye.classList.add('toggle')
      this.eye.innerHTML = `<i class="fas fa-eye"></i>`
      this.eye.addEventListener('mousedown', ()=>{
        this.elRef.nativeElement.type = 'text'
        this.elRef.nativeElement.classList.remove('passfield')
      })
      this.eye.addEventListener('mouseleave', ()=>{
        this.elRef.nativeElement.type = 'password'
        this.elRef.nativeElement.classList.add('passfield')
      })
      this.eye.addEventListener('mouseup', ()=>{
        this.elRef.nativeElement.type = 'password'
        this.elRef.nativeElement.classList.add('passfield')
      })

      

      this.eye.style.setProperty('position', 'absolute')
      this.eye.style.setProperty('right', '1.3em')
      this.eye.style.setProperty('top', '1.39em')
      this.eye.style.setProperty('bottom', '0')
      this.eye.style.setProperty('display', 'flex')
      this.eye.style.setProperty('align-items', 'center')
      this.eye.style.setProperty('color', '#777')
      this.eye.style.setProperty('cursor', 'pointer')
      this.elRef.nativeElement.style.setProperty('padding-right', '2em')
      //console.log(this.elRef.nativeElement.parentElement)
      this.elRef.nativeElement.parentElement?.appendChild(this.eye)
      this.elRef.nativeElement.parentElement?.style.setProperty('position', 'relative')
    }
  }

}
