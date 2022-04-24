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
  isTrained: boolean = false;

  constructor(private flatService: FlatService, public loader: LoaderService) { }

  ngOnInit(): void {
    this.loading$ = this.loader.isLoading;
    this.flatService.getModelData().pipe(
      map(response => {
        this.data = response;
        this.model = response[response.length - 1];
      })
    ).subscribe();
  }

  onClick() {
    this.flatService.feedModel().pipe(
      tap(response => { this.model = response; this.isTrained = true; })
    ).subscribe();
  }

}
