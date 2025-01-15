import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { User } from '../models/user.model';
import { BASE_URL } from '../../../../app.config';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient, private cookieService: CookieService) {}

  register(data: {
    fullName: string;
    email: string;
    password: string;
  }): Observable<any> {
    return this.http.post(`${BASE_URL}/Authentication/register`, data);
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${BASE_URL}/Authentication/login`,
      request
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
    localStorage.setItem('user-email', user.email);
  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  // getUser(): User | undefined {
  //   const email = localStorage.getItem('user-email');

  //   if (email) {
  //     return {
  //       email: email,
  //     };
  //   }

  //   return undefined;
  // }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authentication', '/');
    this.$user.next(undefined);
  }
}
