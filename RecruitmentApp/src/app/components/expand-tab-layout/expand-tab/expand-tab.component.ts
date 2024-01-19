import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-expand-tab',
  templateUrl: './expand-tab.component.html',
  styleUrls: ['./expand-tab.component.scss']
})
export class ExpandTabComponent implements OnInit {

  constructor() { }
  @Input() name = ''
  @Input() label = ''
  @Input() isActive = false
  @Input() labeltpl?:TemplateRef<any>
  @Output() activate = new EventEmitter()
  ngOnInit(): void {
  }

}
