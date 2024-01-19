import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[appLoginTwitter]'
})
export class LoginTwitterDirective {

  img = document.createElement('img')
  constructor(private elementRef: ElementRef<HTMLLinkElement>) { }


  ngOnInit(){
    this.elementRef.nativeElement.appendChild(this.img)
    this.setUp()
  }

  setUp(){
    this.img.src = "assets/img/sociallogin/twitter.png"
    this.img.className = "icon"
    this.img.style.setProperty('opacity', '.5')
    this.img.style.setProperty('cursor', 'default', 'important')
  }

}
