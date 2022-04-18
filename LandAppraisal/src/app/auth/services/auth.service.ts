import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, shareReplay, tap } from 'rxjs/operators';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Params, Router } from '@angular/router';

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
    return this.http.post<User>(this.baseUrl + 'auth/login', { email, password }, { withCredentials: true })
      .pipe(
        tap((response: User) => this.setCurrentUser(response)),
        map((response: User) => response),
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
    const userName = this.getToken('token');
    return this.http.get<User>(this.baseUrl + 'auth/getUser', { params: { userName: userName } } as Params);
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

  private setCurrentUser(response: User): void {
    if (!response) return;
    this.setToken('token', response.userName);
  }

  private setToken(key: string, token: string): void {
    localStorage.setItem(key, token);
  }

  private removeSession(): void {
    localStorage.removeItem('token');
  }
}
