import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { checkPasswords, EmailValidation, NickValidation, PasswordValidation, RepeatPasswordEStateMatcher, validateEmailNotTaken } from '../../models/validation';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  passwordsMatcher: RepeatPasswordEStateMatcher;
  error?: string;
  signupForm: FormGroup;
  hide = true;

  constructor(private authService: AuthService, private router: Router) {
    this.signupForm = new FormGroup({
      email: new FormControl('', EmailValidation, validateEmailNotTaken(this.authService)),
      password: new FormControl('', PasswordValidation),
      confirmedPassword: new FormControl('', Validators.required),
      nickName: new FormControl('', NickValidation)
    }, checkPasswords);
    this.passwordsMatcher = new RepeatPasswordEStateMatcher;
  }

  onSubmit(): void {
    if (this.checkSignupForm()) {
      this.register();
    }
  }

  public toggleHide(): void {
    this.hide = !this.hide;
  }

  private register(): void {
    this.authService.register(this.signupForm.value)
      .subscribe(() => {
        this.router.navigateByUrl('/');
      }, (error) => {
        this.error = error.message;
      });
  }

  private checkSignupForm() {
    if (this.signupForm?.invalid) {
      this.error = "Nieprawidłowe dane formularza.";
      return false;
    }
    return true;
  }
}
