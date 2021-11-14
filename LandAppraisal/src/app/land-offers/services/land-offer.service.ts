import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Land, LandOffer } from '../../shared/models/LandOfferModel';
import { Observable } from 'rxjs';
import { filter, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LandOfferService {
  baseUrl: string = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getLandOffers(): Observable<LandOffer[]> {
    return this.http.get<LandOffer[]>(`${this.baseUrl}land/landoffers`).pipe(
      map(response => response.filter((land) => new Date(land.offerDateTime).getFullYear() >= 2021)));
  }     

  saveLandFromLandOffer(landOffer: LandOffer){
    return this.http.post<boolean>(`${this.baseUrl}land/addland`, landOffer)
      .subscribe((response: boolean) => {
        console.log(response);
      }, error => {
        console.log(error);
      });
  }
  
}

const mockLandOffer = [{
  id: 1,
  price: 200000,
  area: 1500,
  description: "Lorem I psum psumpsumpsumpsumpsumpsum. Lorem I psum psumpsumpsumpsumpsumpsum",
  water: "miejska",
  gas: "",
  electricity: "",
  sewers: "brak",
  road: "asfaltowa",
  type: "",
  location: {
    N_latitude: 55,
    E_longitude:22
  }
},
  {
    id: 2,
    price: 130000,
    area: 2300,
    description: "Lorem I psum psumpsumpsumpsumpsumpsum. Lorem I psum psumpsumpsumpsumpsumpsum",
    water: "miejska",
    gas: "",
    electricity: "",
    sewers: "brak",
    road: "asfaltowa",
    type: "",
    location: {
      N_latitude: 55,
      E_longitude: 22
    }
  }
];
