import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyProcessQuestionsTabComponent } from './apply-process-questions-tab.component';

describe('ApplyProcessQuestionsTabComponent', () => {
  let component: ApplyProcessQuestionsTabComponent;
  let fixture: ComponentFixture<ApplyProcessQuestionsTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplyProcessQuestionsTabComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplyProcessQuestionsTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
