import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Flat, FlatParams } from '../../shared/models/FlatModel';
import { environment } from '../../../environments/environment';
import { Pagination } from '../../shared/models/Pagonation';
import { Params } from '@angular/router';
import { ModelDataResponse } from '../../shared/models/ModelResponse';

@Injectable({
  providedIn: 'root'
})
export class FlatService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getFlats(flatParams: FlatParams): Observable<Pagination<Flat>> {
    return this.http .get<Pagination<Flat>>(`${this.baseUrl}flat/flats`, { params: flatParams } as Params);
  }

  downloadFlatLinks(): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}flat/downloadLinks`);
  }

  downloadFlats(count: number): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}flat/downloadOffers`, { params: { count: count } });
  }

  postFlat(flat: Flat): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}flat/post`, flat);
  }

  getModelData(): Observable<ModelDataResponse> {
    return of({
      totalCount: 3750,
      data: [
        {
          modelData: new Date(30, 1, 2022),
          meanSquareError: 0.02,
          accuracy: 77
        }
      ]
    });
  }

  saveFlats(flats: Flat[]): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}flat/postFlats`, flats)
  }
 
}
