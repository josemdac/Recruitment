import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestedPositionsComponent } from './requested-positions.component';

describe('RequestedPositionsComponent', () => {
  let component: RequestedPositionsComponent;
  let fixture: ComponentFixture<RequestedPositionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestedPositionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestedPositionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
