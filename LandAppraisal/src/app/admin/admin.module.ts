import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './components/menu/menu.component';
import { AdminComponent } from './containers/admin/admin.component';
import { RouterModule, Routes } from '@angular/router';
import { NewOffersComponent } from './containers/new-offers/new-offers.component';
import { OffersListComponent } from './containers/offers-list/offers-list.component';
import { ModelComponent } from './containers/model/model.component';
import { FlatsModule } from '../flats/flats.module';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { CustomPaginator } from '../shared/customs/CustomPaginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DownloadComponent } from './containers/download/download.component';
import { MatStepperModule } from '@angular/material/stepper';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

const routes: Routes = [
  {
    path: '', component: AdminComponent,
    children: [
      {
        path: '', component: DownloadComponent
      },
      {
        path: 'model', component: ModelComponent
      },
      {
        path: 'offers-list', component: OffersListComponent
      },
      {
        path: 'new-offers', component: NewOffersComponent
      },
    ]
  },
];

@NgModule({
  declarations: [
    MenuComponent,
    AdminComponent,
    NewOffersComponent,
    OffersListComponent,
    ModelComponent,
    DownloadComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FlatsModule,
    MatButtonModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatStepperModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  providers: [{ provide: MatPaginatorIntl, useClass: CustomPaginator }],
})
export class AdminModule { }
