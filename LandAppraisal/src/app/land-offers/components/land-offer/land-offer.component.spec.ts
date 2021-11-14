import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandOfferComponent } from './land-offer.component';

describe('LandOfferComponent', () => {
  let component: LandOfferComponent;
  let fixture: ComponentFixture<LandOfferComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandOfferComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LandOfferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
