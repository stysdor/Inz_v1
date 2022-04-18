import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Flat, FlatParams, FlatToPrediction } from '../../shared/models/FlatModel';
import { environment } from '../../../environments/environment';
import { Pagination } from '../../shared/models/Pagination';
import { Params, Router } from '@angular/router';
import { ModelData } from '../../shared/models/ModelResponse';
import { map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FlatService {
  baseUrl: string = environment.apiUrl;
  predictionUrl: string = environment.apiPredictionUrl;

  constructor(private http: HttpClient, private router: Router) { }

  getFlats(flatParams: FlatParams): Observable<Pagination<Flat>> {
    return this.http.get<Pagination<Flat>>(`${this.baseUrl}flat/flats`, { params: flatParams } as Params);
  }

  downloadFlatLinks(): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}flat/downloadLinks`);
  }

  downloadFlats(count: number): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}flat/downloadOffers`, { params: { count: count } });
  }

  getPricePrediction(flat: FlatToPrediction): Observable<number> {
    return this.http.post<number>(`${this.predictionUrl}predict`, flat).pipe(
      tap((price: number) => this.router.navigateByUrl('/flats/prediction', {
        state: { price: price, flat: flat }
      }))
    );
  }

  getModelData(): Observable<ModelData[]> {
    return this.http.get<ModelData[]>(`${this.baseUrl}model/`)
  }

  feedModel(): Observable<ModelData> {
    return this.http.get<ModelData>(`${this.baseUrl}model/feedModel`);
  }

  saveFlats(flats: Flat[]): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}flat/postFlats`, flats)
  }
 
}
