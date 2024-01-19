import { TestBed } from '@angular/core/testing';

import { JsonConfigService } from './json-config.service';

describe('JsonConfigService', () => {
  let service: JsonConfigService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JsonConfigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
