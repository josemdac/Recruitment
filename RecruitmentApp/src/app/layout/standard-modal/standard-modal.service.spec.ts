import { TestBed } from '@angular/core/testing';

import { StandardModalService } from './standard-modal.service';

describe('StandardModalService', () => {
  let service: StandardModalService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StandardModalService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
