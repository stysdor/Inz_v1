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
  typeOfBuilding: number;
  isBalcony: boolean;
  isGarden: boolean;
  isTarrace: boolean;
  isLoggia: boolean;
  isCellar: boolean;
  isGarage: boolean;
  isParkingSpace: boolean;
  kitchen: string;
  state: string;
  market: string;
  isLift: boolean;
  isAccepted: boolean;
}

export class FlatParams {
  sort?: string = 'areaAsc';
  pageIndex: number = 1;
  pageSize: number = 24;
  isAccepted?: boolean;
}

