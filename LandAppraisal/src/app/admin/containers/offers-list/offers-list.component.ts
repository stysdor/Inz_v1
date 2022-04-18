import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { FlatService } from '../../../flats/services/flat.service';
import { LoaderService } from '../../../shared/interceptor/loader.service';
import { Flat, FlatParams } from '../../../shared/models/FlatModel';

@Component({
  selector: 'app-offers-list',
  templateUrl: './offers-list.component.html',
  styleUrls: ['./offers-list.component.css']
})
export class OffersListComponent implements OnInit {
  flats: Flat[] =[];
  flatParams = new FlatParams();
  totalCount: number = 0;
  loading$?: Observable<boolean>;

  sortOptions = [
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
    { name: 'Area: Low to High', value: 'areaAsc' },
    { name: 'Area: High to Low', value: 'areaDesc' }
  ];

  constructor(private flatService: FlatService, public loader: LoaderService) { }

  ngOnInit(): void {
    this.loading$ = this.loader.isLoading;
    this.getFlats();
  }

  getFlats() {
    this.flatService.getFlats(this.flatParams).pipe(
      map(response => {
        this.flats = response.data;
        this.flatParams.pageIndex = response.pageIndex;
        this.flatParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      }),
      catchError((error) => of(console.log(error))
      )).subscribe();
  } 

  onSortSelected(sort: string) {
    this.flatParams.sort = sort;
    this.getFlats();
  }

  onPageChanged(event: PageEvent) {
    this.flatParams.pageIndex = event.pageIndex + 1;
    this.flatParams.pageSize = event.pageSize
    this.getFlats();
  }

}
