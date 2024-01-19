import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpandTabLayoutComponent } from './expand-tab-layout.component';

describe('ExpandTabLayoutComponent', () => {
  let component: ExpandTabLayoutComponent;
  let fixture: ComponentFixture<ExpandTabLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpandTabLayoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpandTabLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
