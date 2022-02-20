import { createAction, props } from "@ngrx/store";

export enum AppActionTypes {
  START_APP_INITIALIZE = '[App] Start App Initializer',
  FINISH_APP_INITIALIZE = '[App] Finish App Initializer',
  LOAD_USER = '[App] Load User'
}

export const startAppInitialize = createAction(
  AppActionTypes.START_APP_INITIALIZE
);

export const finishAppInitialize = createAction(
  AppActionTypes.FINISH_APP_INITIALIZE
);

export const loadUser = createAction(
  AppActionTypes.LOAD_USER
);
