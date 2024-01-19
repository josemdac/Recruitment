import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyProcessPersonalTabComponent } from './apply-process-personal-tab.component';

describe('ApplyProcessPersonalTabComponent', () => {
  let component: ApplyProcessPersonalTabComponent;
  let fixture: ComponentFixture<ApplyProcessPersonalTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplyProcessPersonalTabComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplyProcessPersonalTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
