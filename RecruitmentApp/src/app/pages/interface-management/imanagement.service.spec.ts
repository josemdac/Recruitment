import { TestBed } from '@angular/core/testing';

import { IManagementService } from './imanagement.service';

describe('IManagementService', () => {
  let service: IManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
