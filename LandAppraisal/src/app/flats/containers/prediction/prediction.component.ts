import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FlatToPrediction } from '../../../shared/models/FlatModel';

@Component({
  selector: 'app-prediction',
  templateUrl: './prediction.component.html',
  styleUrls: ['./prediction.component.css']
})
export class PredictionComponent  {
  price: number;
  flat: FlatToPrediction;

  constructor(private router: Router) {
    this.price = Math.round(this.router.getCurrentNavigation()?.extras?.state?.price?.prediction);
    this.flat = this.router.getCurrentNavigation()?.extras?.state?.flat;
  }

  goToFlatForm(): void {
    this.router.navigateByUrl('/flats/form', {
      state: { flat: this.flat }
    });
  }

}
