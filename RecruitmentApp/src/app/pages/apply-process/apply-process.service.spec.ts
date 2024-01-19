import { TestBed } from '@angular/core/testing';

import { ApplyProcessService } from './apply-process.service';

describe('ApplyProcessService', () => {
  let service: ApplyProcessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApplyProcessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
