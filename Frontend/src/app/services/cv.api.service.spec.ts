import { TestBed } from '@angular/core/testing';

import { CvApiService } from './cv.api.service';

describe('CvApiService', () => {
  let service: CvApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CvApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
