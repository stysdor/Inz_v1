import { TestBed } from '@angular/core/testing';

import { LandOfferService } from './land-offer.service';

describe('LandOfferService', () => {
  let service: LandOfferService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LandOfferService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
