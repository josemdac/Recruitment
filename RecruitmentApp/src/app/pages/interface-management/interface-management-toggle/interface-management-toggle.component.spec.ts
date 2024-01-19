import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InterfaceManagementToggleComponent } from './interface-management-toggle.component';

describe('InterfaceManagementToggleComponent', () => {
  let component: InterfaceManagementToggleComponent;
  let fixture: ComponentFixture<InterfaceManagementToggleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InterfaceManagementToggleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InterfaceManagementToggleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
