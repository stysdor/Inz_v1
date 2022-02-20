import { createAction, props } from "@ngrx/store";
import { User } from "../models/user";

export enum AuthActionTypes {
  LOGIN = '[Login Page] Login',
  LOGIN_SUCCESS = '[Auth] Login Success',
  LOGIN_FAILURE = '[Auth] Login Failure',
  LOGOUT = '[Auth] Logout',
  LOGOUT_SUCCESS = '[Logout button] Logout success',
  LOGIN_SUCCESS_REDIRECT = '[Auth] Login success redirect',
  LOGIN_REDIRECT = '[Auth Guard] Login Redirect',
  LOAD_USER_FAILURE = '[App] Load User Failure'
}

export const login = createAction( 
  AuthActionTypes.LOGIN,
  props<{ email: string, password: string, returnUrl: string }>()
);

export const loginSuccess = createAction(
  AuthActionTypes.LOGIN_SUCCESS,
  props<{ user: User }>()
);

export const loginSuccessRedirect = createAction(
  AuthActionTypes.LOGIN_SUCCESS_REDIRECT,
  props<{ returnUrl: string }>()
);

export const loginFailure = createAction(
  AuthActionTypes.LOGIN_FAILURE,
  props<{ error: Error }>()
);

export const logout = createAction(
  AuthActionTypes.LOGOUT
);

export const logoutSuccess = createAction(
  AuthActionTypes.LOGOUT_SUCCESS
);

export const loginRedirect = createAction(
  AuthActionTypes.LOGIN_REDIRECT,
  props<{ returnUrl: string }>()
);

export const loadUserFailure = createAction(
  AuthActionTypes.LOAD_USER_FAILURE
);
