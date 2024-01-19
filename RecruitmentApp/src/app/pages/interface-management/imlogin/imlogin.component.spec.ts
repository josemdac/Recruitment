import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IMLoginComponent } from './imlogin.component';

describe('IMLoginComponent', () => {
  let component: IMLoginComponent;
  let fixture: ComponentFixture<IMLoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IMLoginComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IMLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
