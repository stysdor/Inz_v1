import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { from, of } from "rxjs";
import { catchError, concatMap, exhaustMap, map, tap } from "rxjs/operators";
import { User } from "../models/user";
import { AuthService } from "../services/auth.service";
import * as AuthActions from "./auth.actions";

@Injectable()
export class AuthEffects {

  logIn$ = createEffect(() =>
    this.actions$.pipe(
      ofType<ReturnType<typeof AuthActions.login>>(AuthActions.AuthActionTypes.LOGIN),
      exhaustMap(action =>this.authService.login(action.email, action.password).pipe(
        concatMap((user: User) => from([
          AuthActions.loginSuccess({ user }),
          AuthActions.loginSuccessRedirect({ returnUrl: action.returnUrl })]
        )),
        catchError((error: Error) =>  of(AuthActions.loginFailure({ error })))
        )
      )
    ),
    { useEffectsErrorHandler: false }
  );

  logInSuccessRedirect$ = createEffect(() =>
    this.actions$.pipe(
      ofType<ReturnType<typeof AuthActions.loginSuccessRedirect>>(AuthActions.AuthActionTypes.LOGIN_SUCCESS_REDIRECT),
      tap((action) => this.router.navigateByUrl(action.returnUrl))
    ),
    { dispatch: false }
  );

  logout$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.AuthActionTypes.LOGOUT),
      tap(() =>  this.authService.logout()),
      map(() => AuthActions.logoutSuccess())
    )
  );

  loginRedirect$ = createEffect(() =>
    this.actions$.pipe(
      ofType<ReturnType<typeof AuthActions.loginRedirect>>(AuthActions.AuthActionTypes.LOGIN_REDIRECT),
      tap(action =>
        this.router.navigate(['/auth/login'], { queryParams: { returnUrl: action.returnUrl } })
      )
    ), { dispatch: false }
  );

  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private router: Router
  ) { }

}
