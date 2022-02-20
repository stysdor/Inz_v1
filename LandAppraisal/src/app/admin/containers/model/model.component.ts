import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs/operators';
import { FlatService } from '../../../flats/services/flat.service';
import { ModelData, ModelDataResponse } from '../../../shared/models/ModelResponse';

@Component({
  selector: 'app-model',
  templateUrl: './model.component.html',
  styleUrls: ['./model.component.css']
})
export class ModelComponent implements OnInit {
  totalCount: number = 0;
  data: ModelData[] = [];

  constructor(private flatService: FlatService) { }

  ngOnInit(): void {
    this.flatService.getModelData().pipe(
      tap((response: ModelDataResponse) => {
        this.totalCount = response.totalCount;
        this.data = response.data;
      })
    ).subscribe();
  }

}
