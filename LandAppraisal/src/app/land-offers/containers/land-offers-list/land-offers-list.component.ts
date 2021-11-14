import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Land, LandOffer, LandProperties } from '../../../shared/models/LandOfferModel';
import { LandOfferService } from '../../services/land-offer.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-land-offers-list',
  templateUrl: './land-offers-list.component.html',
  styleUrls: ['./land-offers-list.component.css']
})
export class LandOffersListComponent  {
  landOffers: LandOffer[] = [];
 // dataSource: MatTableDataSource<LandOffer>;
  currentLandOffer: LandOffer | null = null;
  iterator: number = 0;


  constructor(private landOfferService: LandOfferService, private router: Router) {
    this.landOfferService.getLandOffers().subscribe(
      data => {
        this.landOffers = data;
        this.currentLandOffer = this.landOffers[this.iterator];
      }
    );
    
    //this.dataSource = new MatTableDataSource(this.landOffers);
  }
  onNext(): void {
    if (this.iterator === (this.landOffers.length - 1)) return;
    this.iterator++;
    this.currentLandOffer = this.landOffers[this.iterator];
  }

  onPrevious(): void {
    if (this.iterator === 0) return;
    this.iterator--;
    this.currentLandOffer = this.landOffers[this.iterator];
  }

  submit(event: LandProperties): void {
    if (!this.currentLandOffer) {
      return;
    }
    this.landOfferService.saveLandFromLandOffer(this.landFromLandOffer(this.currentLandOffer, event));
  }

  private landFromLandOffer(landOffer: LandOffer, land: LandProperties): LandOffer {
    landOffer.isElectricity = land.isElectricity;
    landOffer.isGas = land.isGas;
    landOffer.isSewers = land.isSewers;
    landOffer.isWater = land.isWater;
    landOffer.road = land.road;
    return landOffer;
  }
}




