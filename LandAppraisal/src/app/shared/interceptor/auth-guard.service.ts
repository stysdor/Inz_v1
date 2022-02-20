import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthState, selectAuthAuthenticationStatus } from '../../auth/store/auth.reducers';
import * as AuthActions from '../../auth/store/auth.actions';


@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

  constructor(
    private store: Store<AuthState>,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.store.select(selectAuthAuthenticationStatus).pipe(
      map((isAuthenticated : boolean) => {
        if (!isAuthenticated) {
          console.log('fail');
          this.store.dispatch(AuthActions.loginRedirect({returnUrl: state.url}))
          return false;
        }
        console.log('success');
        return true;
      })
    );
  }
}
