import { registerLocaleData } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-prediction',
  templateUrl: './prediction.component.html',
  styleUrls: ['./prediction.component.css']
})
export class PredictionComponent implements OnInit {
  price: number;

  constructor(private router: Router) {
    this.price = Math.round(this.router.getCurrentNavigation()?.extras?.state?.price?.prediction);
    console.log(JSON.stringify(this.router.getCurrentNavigation()?.extras?.state));
  }

  ngOnInit(): void {
  }

}
