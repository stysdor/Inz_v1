import { FormControl, Validators, FormGroupDirective, NgForm, ValidatorFn, AbstractControl, ValidationErrors, AsyncValidatorFn } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Observable, ObservableInput, of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

export const EmailValidation = [
  Validators.required,
  Validators.pattern('^[\\w-\.]+@([\\w-]+\.)+[\\w-]{2,4}$')
];

export const PasswordValidation = [
  Validators.required,
  Validators.pattern("^(?=.*[A-Za-z])(?=.*[0-9])[a-zA-Z0-9!@#$%^&*]{8,}$")
];

export const NickValidation = [
  Validators.required,
  Validators.pattern("(^[a-zA-Z])([a-zA-Z0-9]*)$")
];

export class RepeatPasswordEStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    return !!(control?.parent?.get('password')?.value !== control?.parent?.get('confirmedPassword')?.value && control?.dirty)
  }
}

export const checkPasswords: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
  let pass = group.get('password')?.value;
  let confirmPass = group.get('confirmedPassword')?.value;
  return pass === confirmPass ? null : { passwordsNotEqual: true }
}

export function validateEmailNotTaken(auth: AuthService): AsyncValidatorFn {
  return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
    return timer(500).pipe<ValidationErrors | null>(
      switchMap((): ObservableInput<ValidationErrors | null> => {
        if (!control.value) {
          return of(null);
        }
        return auth.checkEmailExists(control.value).pipe(
          map(res => {
            return res ? { emailExist: true } : null;
          }))
      })
    )
  }
}
