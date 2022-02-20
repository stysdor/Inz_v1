import { Action, ActionReducerMap, createFeatureSelector, createReducer, on } from "@ngrx/store";
import * as fromAuth from "../auth/store/auth.reducers";
import { InjectionToken } from "@angular/core";

export interface State {
  [fromAuth.authFeatureKey]: fromAuth.AuthState;
}

export const ROOT_REDUCERS = new InjectionToken<ActionReducerMap<State, Action>>(
  'Root reducers token', {
  factory: () => ({
    [fromAuth.authFeatureKey]: fromAuth.reducer,
  })
}
);
