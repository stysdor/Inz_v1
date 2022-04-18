import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, shareReplay, tap } from 'rxjs/operators';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';
import { LoginResponse } from '../models/userResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private router: Router
  ) { }

  login(email: string, password: string): Observable<User> {
    return this.http.post<LoginResponse>(this.baseUrl + 'auth/login', { email, password }, { withCredentials: true })
      .pipe(
        tap((response: LoginResponse) => this.setCurrentUser(response)),
        map((response: LoginResponse) => response.user),
        catchError(error => {
          return throwError(error);
        }),
        shareReplay()
      );
  }

  register(values: User): Observable<User> {
    return this.http.post<LoginResponse>(this.baseUrl + 'auth/register', values)
      .pipe(
        tap((response: LoginResponse) => this.setCurrentUser(response)),
        map((response: LoginResponse) => response.user),
        catchError(error => {
          return throwError(error);
        }),
        shareReplay()
      );
  }

  logout(): void {
    this.removeSession();
  }

  loadCurrentUser(): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'auth/getUser',)
      .pipe();
  }

  checkEmailExists(email: string): Observable<boolean> {
    //return this.http.get<boolean>(this.baseUrl + '/account/emailexist?email=' + email);
    return of(false);
  }

  getToken(key: string): string | null {
    return localStorage.getItem(key);
  }

  redirectToForbiddenPage(): void {
    this.router.navigateByUrl(this.baseUrl + 'account/forbidden');
  }

  private setCurrentUser(response: LoginResponse): void {
    if (!response.user) return;
    this.setToken('token', response.user.userName);
  }

  private setToken(key: string, token: string): void {
    localStorage.setItem(key, token);
  }

  private removeSession(): void {
    localStorage.removeItem('token');
  }
}
