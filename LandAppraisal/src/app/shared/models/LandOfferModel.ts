export type LandOffer = {
  id: number;
  area: number;
  price: number;
  electricity: string;
  gas: string;
  water: string;
  sewers: string;
  type: string | null;
  road: string;
  description: string;
  location: Location
  isElectricity: boolean |null;
  isGas: boolean | null;
  isWater: boolean | null;
  isSewers: boolean | null;
  offerDateTime: Date;
};

export type Land = {
  id: number;
  area: number;
  price: number;
  isElectricity: boolean;
  isGas: boolean;
  isWater: boolean;
  isSewers: boolean;
  type?: string;
  road: string;
  location: Location; 
};

export type Location = Readonly<{
  N_latitude: number;
  E_longitude: number;
}>;

export type LandProperties = {
  isElectricity: boolean;
  isGas: boolean;
  isWater: boolean;
  isSewers: boolean;
  road: string;
}

  export enum Roads  {
    Utwardzana = 'utwardzana',
    Polna = 'polna',
    Asfaltowa = 'asfaltowa',
    BrakInformacji = ''
  }

