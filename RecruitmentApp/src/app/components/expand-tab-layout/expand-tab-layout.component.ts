import { Component, ContentChildren, OnInit, QueryList } from '@angular/core';
import { Subscription } from 'rxjs';
import { ExpandTabComponent } from './expand-tab/expand-tab.component';

@Component({
  selector: 'app-expand-tab-layout',
  templateUrl: './expand-tab-layout.component.html',
  styleUrls: ['./expand-tab-layout.component.scss']
})
export class ExpandTabLayoutComponent implements OnInit {

  constructor() { }
  @ContentChildren(ExpandTabComponent) expandTabs?: QueryList<ExpandTabComponent> 

  ngOnInit(): void {
  }

  subs:Subscription[] = []
  
  ngAfterContentInit(){
    const tabs = this.expandTabs?.toArray();
    if(tabs && tabs.length){
      this.subs = tabs.map(t=>{
        return t.activate.subscribe(()=>{
          this.currentTab = t
          t.isActive= !t.isActive;
          //tabs.filter(ot=>ot!=t).forEach(ot=>ot.isActive = false)
        })
      })
    }
  }

  expandAll(event:any){ 
    event.preventDefault()
    const tabs = this.expandTabs?.toArray();
    if(tabs && tabs.length){
      tabs.forEach(t=>t.isActive = true)
    }
  }

  collapseAll(event:any){ 
    event.preventDefault()
    const tabs = this.expandTabs?.toArray();
    if(tabs && tabs.length){
      tabs.forEach(t=>t.isActive = false)
    }
  }


  ngOnDestroy(){
    this.subs.forEach(s=>s.unsubscribe())
  }

  onAcitvate(tab:any){
    this.currentTab = tab
  }
  currentTab?:ExpandTabComponent
}
