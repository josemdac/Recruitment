import { TestBed } from '@angular/core/testing';

import { WebSiteInfoService } from './web-site-info.service';

describe('WebSiteInfoService', () => {
  let service: WebSiteInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WebSiteInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
