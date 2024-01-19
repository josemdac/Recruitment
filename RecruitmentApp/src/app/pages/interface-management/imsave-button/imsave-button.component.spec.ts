import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IMSaveButtonComponent } from './imsave-button.component';

describe('IMSaveButtonComponent', () => {
  let component: IMSaveButtonComponent;
  let fixture: ComponentFixture<IMSaveButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IMSaveButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IMSaveButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
