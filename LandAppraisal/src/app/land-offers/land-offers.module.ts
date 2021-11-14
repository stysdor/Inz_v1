import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandOffersListComponent } from './containers/land-offers-list/land-offers-list.component';
import { LandOfferComponent } from './components/land-offer/land-offer.component';
import { RouterModule, Routes } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

const routes: Routes = [
  {
    path: '', component: LandOffersListComponent
  },
  {
    path: '/:id', component: LandOfferComponent
  }
];

@NgModule({
  declarations: [
    LandOffersListComponent,
    LandOfferComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatTableModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatButtonModule,
    MatInputModule
  ]
})
export class LandOffersModule { }
