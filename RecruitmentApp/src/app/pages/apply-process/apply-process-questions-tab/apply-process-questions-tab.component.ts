import { Component, OnInit } from '@angular/core';
import { ApplyProcessService } from '../apply-process.service';

@Component({
  selector: 'app-apply-process-questions-tab',
  templateUrl: './apply-process-questions-tab.component.html',
  styleUrls: ['./apply-process-questions-tab.component.scss']
})
export class ApplyProcessQuestionsTabComponent implements OnInit {

  constructor(public proc: ApplyProcessService) { }

  ngOnInit(): void {
    
  }

}
