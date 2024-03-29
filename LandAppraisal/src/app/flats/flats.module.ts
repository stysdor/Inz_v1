import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FlatListComponent } from './components/flat-list/flat-list.component';
import { FlatFormComponent } from './containers/flat-form/flat-form.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MapComponent } from './components/map/map.component';
import { AgmCoreModule } from '@agm/core';
import { GooglePlaceModule } from 'ngx-google-places-autocomplete';
import { PredictionComponent } from './containers/prediction/prediction.component';
import { API_KEY } from '../secret';

const routes: Routes = [
  {
    path: 'form', component: FlatFormComponent
  },
  {
    path: 'prediction', component: PredictionComponent
  }
];

@NgModule({
  declarations: [
    FlatListComponent,
    FlatFormComponent,
    MapComponent,
    PredictionComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatSelectModule,
    MatStepperModule,
    MatCheckboxModule,
    MatCardModule,
    FontAwesomeModule,
    MatTableModule,
    MatSortModule,
    AgmCoreModule.forRoot({
      apiKey: API_KEY,
      libraries: ['places']
    }),
    GooglePlaceModule

  ],
  exports: [
    FlatListComponent
  ]
})
export class FlatsModule { }
