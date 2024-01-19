import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmploymentHistComponent } from './employment-hist.component';

describe('EmploymentHistComponent', () => {
  let component: EmploymentHistComponent;
  let fixture: ComponentFixture<EmploymentHistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmploymentHistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmploymentHistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
