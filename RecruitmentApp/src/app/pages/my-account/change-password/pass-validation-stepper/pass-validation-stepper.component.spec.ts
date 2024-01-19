import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PassValidationStepperComponent } from './pass-validation-stepper.component';

describe('PassValidationStepperComponent', () => {
  let component: PassValidationStepperComponent;
  let fixture: ComponentFixture<PassValidationStepperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PassValidationStepperComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PassValidationStepperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
