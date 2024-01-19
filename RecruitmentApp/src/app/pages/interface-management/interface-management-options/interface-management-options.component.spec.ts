import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InterfaceManagementOptionsComponent } from './interface-management-options.component';

describe('InterfaceManagementOptionsComponent', () => {
  let component: InterfaceManagementOptionsComponent;
  let fixture: ComponentFixture<InterfaceManagementOptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InterfaceManagementOptionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InterfaceManagementOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
