import { Store } from "@ngrx/store";
import * as AppActions from "../../store/app.actions";
import { AuthState, selectAuthInitializationStatus } from "../../auth/store/auth.reducers";
import { AuthService } from "../../auth/services/auth.service";

export function appInitializer(store: Store<AuthState>, auth: AuthService): Function {
  return () => new Promise(resolve => {
    store.dispatch(AppActions.startAppInitialize());
    store.dispatch(AppActions.loadUser());
    store.select(selectAuthInitializationStatus).subscribe((isInitializationPending) => {
      if (!isInitializationPending) {
        store.dispatch(AppActions.finishAppInitialize());
        resolve(true);
      }
    });
  })
}
