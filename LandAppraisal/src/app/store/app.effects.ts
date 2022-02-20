import { Injectable } from "@angular/core";
import { Actions, createEffect, EffectNotification, ofType, OnRunEffects } from "@ngrx/effects";
import { Observable, of } from "rxjs";
import { catchError, exhaustMap, map, takeUntil } from "rxjs/operators";
import { User } from "../auth/models/user";
import { AuthService } from "../auth/services/auth.service";
import { AppActionTypes } from "./app.actions";
import * as AuthActions from "../auth/store/auth.actions";
import * as AppActions from "./app.actions";

@Injectable()
export class AppEffects implements OnRunEffects {

  ngrxOnRunEffects(resolvedEffects$: Observable<EffectNotification>): Observable<EffectNotification> {
    return this.actions$.pipe(
      ofType(AppActionTypes.START_APP_INITIALIZE),
      exhaustMap(() => resolvedEffects$
        .pipe(
          takeUntil(this.actions$.pipe(ofType(AppActionTypes.FINISH_APP_INITIALIZE)))
        )
      )
    );
  }

  loadUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AppActionTypes.LOAD_USER),
      exhaustMap(action => {
        return this.authService.loadCurrentUser().pipe(
          map((user: User) => {
            if (!user) return AuthActions.loadUserFailure();
            return AuthActions.loginSuccess({ user });
          }),
          catchError((error) => of(AuthActions.loadUserFailure())
          ))
      })
    ));

  constructor(
    private actions$: Actions,
    private authService: AuthService,
  ) { }
}
