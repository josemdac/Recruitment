import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApProcQuestionComponent } from './ap-proc-question.component';

describe('ApProcQuestionComponent', () => {
  let component: ApProcQuestionComponent;
  let fixture: ComponentFixture<ApProcQuestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApProcQuestionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApProcQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
