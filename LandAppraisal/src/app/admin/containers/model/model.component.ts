import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { FlatService } from '../../../flats/services/flat.service';
import { LoaderService } from '../../../shared/interceptor/loader.service';
import { FlatParams } from '../../../shared/models/FlatModel';
import { ModelData } from '../../../shared/models/ModelResponse';

@Component({
  selector: 'app-model',
  templateUrl: './model.component.html',
  styleUrls: ['./model.component.css']
})
export class ModelComponent implements OnInit {
  flatParams = new FlatParams();
  data: ModelData[] = [];
  model: ModelData | null = null;
  loading$?: Observable<boolean>;

  constructor(private flatService: FlatService, public loader: LoaderService) { }

  ngOnInit(): void {
    this.loading$ = this.loader.isLoading;
    this.flatService.getModelData().pipe(
      map(response => {
        this.data = response;
        this.model = response[0];
      })
    ).subscribe();
  }

  onClick() {
    this.flatParams.isUsedInModel = false;
    this.flatParams.isAccepted = true;
    this.flatParams.pageSize = 10000;
    this.flatService.feedModel(this.flatParams).pipe(
      tap(response => this.model = response)
    ).subscribe();
  }

}
