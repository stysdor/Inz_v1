import { createFeatureSelector, createReducer, createSelector, on } from "@ngrx/store";
import { User } from "../models/user";

import * as AuthActions from "./auth.actions";

export const authFeatureKey = 'auth';

export interface AuthState {
  isAuthenticated: boolean;
  isInitializationPending: boolean;
  user: User | null;
  errorMessage: string | null;
};

export const initialState: AuthState = {
  isAuthenticated: false,
  isInitializationPending: true,
  user: null,
  errorMessage: null
};

export const reducer = createReducer(
  initialState,

  on(AuthActions.loginSuccess, (state, { user }) => ({
    ...state,
    isAuthenticated: true,
    isInitializationPending: false,
    user: user,
    errorMessage: null
  })),

  on(AuthActions.loginFailure, state => ({
    ...state,
    errorMessage: 'Nieprawidłowy email lub hasło'
  })),

  on(AuthActions.logoutSuccess, state => ({
    ...state,
    isAuthenticated: false,
    isInitializationPending: false,
    user: null,
    errorMessage: null
  })),

  on(AuthActions.loadUserFailure, state => ({
    ...state,
    isInitializationPending: false
  }))
);

export const selectAuthState = createFeatureSelector<AuthState>(authFeatureKey);

export const selectAuthUser = createSelector(
  selectAuthState,
  state => state.user
);

export const selectAuthErrorMessage = createSelector(
  selectAuthState,
  state => state.errorMessage
);
export const selectAuthAuthenticationStatus = createSelector(
  selectAuthState,
  state => state.isAuthenticated
);
export const selectAuthInitializationStatus = createSelector(
  selectAuthState,
  state => state.isInitializationPending
);
