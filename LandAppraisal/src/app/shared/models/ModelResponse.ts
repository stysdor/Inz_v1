export type ModelData = {
  accuracy: number;
  meanSquareError: number;
  modelData: Date;
}

export type ModelDataResponse = {
  data: ModelData[];
  totalCount: number;
}
