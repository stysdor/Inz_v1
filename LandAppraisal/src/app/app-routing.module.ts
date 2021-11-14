import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';

const routes: Routes = [
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule),
  },
  {
    path: 'land-offers',
    loadChildren: () => import('./land-offers/land-offers.module').then(m => m.LandOffersModule),
  },
  {
    path: 'lands',
    loadChildren: () => import('./lands/lands.module').then(m => m.LandsModule)
  },
  {
    path: '',
    component: AppComponent
  }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
