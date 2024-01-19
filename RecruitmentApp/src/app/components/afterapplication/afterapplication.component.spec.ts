import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AfterapplicationComponent } from './afterapplication.component';

describe('AfterapplicationComponent', () => {
  let component: AfterapplicationComponent;
  let fixture: ComponentFixture<AfterapplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AfterapplicationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AfterapplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
