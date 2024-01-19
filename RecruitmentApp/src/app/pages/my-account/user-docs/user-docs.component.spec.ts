import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserDocsComponent } from './user-docs.component';

describe('UserDocsComponent', () => {
  let component: UserDocsComponent;
  let fixture: ComponentFixture<UserDocsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserDocsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserDocsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
