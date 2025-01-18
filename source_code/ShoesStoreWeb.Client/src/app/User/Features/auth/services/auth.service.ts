import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { User } from '../models/user.model';
import { BASE_URL } from '../../../../app.config';
import { UpdateUser } from '../models/update-user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userSubject: BehaviorSubject<User | null> =
    new BehaviorSubject<User | null>(null);
  public user$: Observable<User | null> = this.userSubject.asObservable();

  constructor(private http: HttpClient, private cookieService: CookieService) {
    const userData = localStorage.getItem('user');
    if (userData) {
      this.userSubject.next(JSON.parse(userData)); // Khởi tạo thông tin người dùng từ localStorage
    }
  }

  // Đăng ký người dùng mới
  register(data: {
    fullName: string;
    email: string;
    password: string;
  }): Observable<any> {
    return this.http.post(`${BASE_URL}/Authentication/register`, data);
  }

  // Đăng nhập và lưu thông tin người dùng
  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(`${BASE_URL}/Authentication/login`, request)
      .pipe(
        tap((response) => {
          // Lưu token vào cookie và localStorage
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

          // Lấy thông tin người dùng và cập nhật vào BehaviorSubject
          this.getUserInfo().subscribe((user) => {
            this.setUser(user);
          });
        })
      );
  }

  // Lấy thông tin người dùng từ API
  getUserInfo(): Observable<User> {
    return this.http.get<User>(`${BASE_URL}/Authentication/user-info`);
  }

  // Cập nhật thông tin người dùng
  updateUserInfo(user: UpdateUser): Observable<void> {
    return this.http
      .put<void>(`${BASE_URL}/Authentication/update-user-info`, user)
      .pipe(
        tap(() => {
          // Sau khi cập nhật, đồng bộ thông tin người dùng vào BehaviorSubject
          this.getUserInfo().subscribe((updatedUser) => {
            this.setUser(updatedUser);
          });
        })
      );
  }

  // Upload avatar người dùng
  uploadAvatar(formData: FormData): Observable<any> {
    return this.http
      .post<any>(`${BASE_URL}/Authentication/Upload-Image`, formData)
      .pipe(
        tap((response) => {
          // Sau khi upload avatar, cập nhật thông tin người dùng
          this.getUserInfo().subscribe((user) => {
            this.setUser(user);
          });
        })
      );
  }

  // Đổi mật khẩu
  changePassword(oldPassword: string, newPassword: string): Observable<void> {
    return this.http.put<void>(`${BASE_URL}/Authentication/change-password`, {
      oldPassword,
      newPassword,
    });
  }

  // Đặt thông tin người dùng vào BehaviorSubject và localStorage
  setUser(user: User): void {
    this.userSubject.next(user);
    localStorage.setItem('user', JSON.stringify(user));
  }

  // Lấy thông tin người dùng hiện tại
  getUser(): User | null {
    return this.userSubject.value;
  }

  // Đăng xuất người dùng
  logout(): void {
    localStorage.clear(); // Xóa localStorage
    this.cookieService.delete('Authentication', '/'); // Xóa cookie
    this.userSubject.next(null); // Reset thông tin người dùng
  }
}
