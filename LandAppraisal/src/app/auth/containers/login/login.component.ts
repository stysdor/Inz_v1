import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { EmailValidation } from '../../models/validation';
import { AuthService } from '../../services/auth.service';
import * as fromAuth from '../../store/auth.reducers';
import { login } from '../../store/auth.actions';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  signinForm: FormGroup;
  returnUrl: string;
  error?: string;
  errorAPI$: Observable<string | null>
  hide = true;

  constructor(
    private fb: FormBuilder,
    public authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private store: Store<fromAuth.AuthState>) {
    this.signinForm = this.fb.group({
      email: new FormControl('', EmailValidation),
      password: new FormControl('', Validators.required)
    });
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '/recipes';
    this.errorAPI$ = this.store.select(fromAuth.selectAuthErrorMessage);
  }

  onSubmit(): void {
    if (this.checkSigninForm()) {
      this.login();
    }
  }

  public toggleHide(): void {
    this.hide = !this.hide;
  }

  private checkSigninForm(): boolean {
    if (this.signinForm?.invalid) {
      this.error = "Nieprawidłowe dane formularza.";
      return false;
    }
    return true;
  }

  private login(): void {
    const payload = {
      email: this.signinForm.controls.email.value,
      password: this.signinForm.controls.password.value,
      returnUrl: this.returnUrl
    }
    this.store.dispatch(login(payload));
  }
}
