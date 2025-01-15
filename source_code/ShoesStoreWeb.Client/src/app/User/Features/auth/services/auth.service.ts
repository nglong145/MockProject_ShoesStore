import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { User } from '../models/user.model';
import { BASE_URL } from '../../../../app.config';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private $user = new BehaviorSubject<User | null>(null);

  constructor(private http: HttpClient, private cookieService: CookieService) {
    const userData = localStorage.getItem('user');
    if (userData) {
      this.$user.next(JSON.parse(userData)); // Đưa thông tin người dùng vào BehaviorSubject
    }
  }

  register(data: {
    fullName: string;
    email: string;
    password: string;
  }): Observable<any> {
    return this.http.post(`${BASE_URL}/Authentication/register`, data);
  }

  // login(request: LoginRequest): Observable<LoginResponse> {
  //   return this.http.post<LoginResponse>(
  //     `${BASE_URL}/Authentication/login`,
  //     request
  //   );
  // }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(`${BASE_URL}/Authentication/login`, request)
      .pipe(
        tap((response) => {
          this.cookieService.set(
            'Authentication',
            `Bearer ${response.token}`,
            undefined,
            '/',
            undefined,
            true,
            'Strict'
          );
          localStorage.setItem('token', response.token);

          // Sau khi login, lấy thông tin người dùng và cập nhật vào BehaviorSubject
          this.getUserInfo().subscribe((user) => {
            this.setUser(user); // Cập nhật thông tin người dùng vào BehaviorSubject
          });
        })
      );
  }

  // Lấy thông tin người dùng
  getUserInfo(): Observable<User> {
    return this.http.get<User>(`${BASE_URL}/Authentication/user-info`);
  }

  // // Cập nhật thông tin người dùng
  // updateUserInfo(data: Partial<User>): Observable<void> {
  //   return this.http.put<void>(`${this.apiUrl}/update-user-info`, data);
  // }

  setUser(user: User): void {
    this.$user.next(user);
    localStorage.setItem('user', JSON.stringify(user));
  }

  user(): Observable<User | null> {
    return this.$user.asObservable();
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authentication', '/');
    this.$user.next(null);
  }
}
