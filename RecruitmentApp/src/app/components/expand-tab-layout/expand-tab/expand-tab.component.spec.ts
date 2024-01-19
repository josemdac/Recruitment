import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpandTabComponent } from './expand-tab.component';

describe('ExpandTabComponent', () => {
  let component: ExpandTabComponent;
  let fixture: ComponentFixture<ExpandTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpandTabComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpandTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
