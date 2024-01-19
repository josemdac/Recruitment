import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyProcessDocumentsTabComponent } from './apply-process-documents-tab.component';

describe('ApplyProcessDocumentsTabComponent', () => {
  let component: ApplyProcessDocumentsTabComponent;
  let fixture: ComponentFixture<ApplyProcessDocumentsTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplyProcessDocumentsTabComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplyProcessDocumentsTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
