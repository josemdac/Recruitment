import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmtpconfirmationComponent } from './smtpconfirmation.component';

describe('SmtpconfirmationComponent', () => {
  let component: SmtpconfirmationComponent;
  let fixture: ComponentFixture<SmtpconfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SmtpconfirmationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SmtpconfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
