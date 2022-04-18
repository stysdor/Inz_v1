export type Flat = {
  id: number;
  area: number;
  price: number;
  description: string;
  offerDateTime: Date;
  location: Location;
  roomNumber: number;
  floor: number;
  floorInBuilding: number;
  constructionYear: number;
  typeOfBuilding: string;
  isBalcony: boolean;
  isGarden: boolean;
  isTarrace: boolean;
  isLoggia: boolean;
  isCellar: boolean;
  isGarage: boolean;
  isParkingSpace: boolean;
  state: string;
  market: string;
  isLift: boolean;
  isAccepted: boolean;
  isUsedInModel: boolean;
}

export class FlatParams {
  sort?: string = 'areaAsc';
  pageIndex: number = 1;
  pageSize: number = 24;
  isAccepted?: boolean;
  isUsedInModel?: boolean;
}

export type FlatToPrediction = {
  area: number;
  n_latitude: number;
  e_longitude: number;
  roomNumber: number;
  floor: number;
  floorInBuilding: number;
  isBalcony: boolean;
  isGarden: boolean;
  isTarrace: boolean;
  isLoggia: boolean;
  constructionYear: number;
  isCellar: boolean;
  isGarage: boolean;
  isParkingSpace: boolean;
  market: string;
  isLift: boolean;
}

