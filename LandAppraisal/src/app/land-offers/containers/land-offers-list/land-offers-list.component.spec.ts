import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandOffersListComponent } from './land-offers-list.component';

describe('LandOffersListComponent', () => {
  let component: LandOffersListComponent;
  let fixture: ComponentFixture<LandOffersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandOffersListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LandOffersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
