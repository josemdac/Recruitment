import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-item',
  templateUrl: './nav-item.component.html',
  styleUrls: ['./nav-item.component.scss']
})
export class NavItemComponent implements OnInit {

  constructor() { }

  @Input() link:{ label:string, isActive?: ()=>boolean, onClick: (event:MouseEvent)=>void, children?:any[]} = { label: '', onClick: e=>{}, children: [] }
  ngOnInit(): void {
  }


  get isActive(){
    if(this.link.isActive){
      return this.link.isActive()
    }
    return false;
  }
}
