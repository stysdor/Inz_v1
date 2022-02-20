import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { FlatService } from '../../../flats/services/flat.service';
import { LoaderService } from '../../../shared/interceptor/loader.service';
import { Flat, FlatParams } from '../../../shared/models/FlatModel';

@Component({
  selector: 'app-new-offers',
  templateUrl: './new-offers.component.html',
  styleUrls: ['./new-offers.component.css']
})
export class NewOffersComponent implements OnInit {
  flats: Flat[] = [];
  flatParams = new FlatParams();
  totalCount: number = 0;

  sortOptions = [
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
    { name: 'Area: Low to High', value: 'areaAsc' },
    { name: 'Area: High to Low', value: 'areaDesc' }
  ];

  constructor(private flatService: FlatService, public loader: LoaderService) { }

  ngOnInit(): void {
    this.flatParams.isAccepted = false;
    this.flatParams.sort = 'offerData';
    this.getFlats();
  }

  getFlats(): void {
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

  onPageChanged(event: PageEvent): void {
    this.flatParams.pageIndex = event.pageIndex + 1;
    this.flatParams.pageSize = event.pageSize
    this.getFlats();
  }

  downloadFlats() : void {
    this.flatService.downloadFlatLinks().subscribe(
      () => this.getFlats()
    );
  }

  onAllSelected(): void {
    this.flats?.forEach(flat => flat.isAccepted = true);
  }

  save(): void {
    this.flatService.saveFlats(this.flats).subscribe();
  }
}
