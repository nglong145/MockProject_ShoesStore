import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const user = authService.getUser();

  let token = cookieService.get('Authentication');

  if (user && token) {
    token = token.replace('Bearer ', '');

    const decodeToken: any = jwtDecode(token);

    const expirationDate = decodeToken.exp * 1000;
    const currentTime = new Date().getTime();

    if (expirationDate < currentTime) {
      authService.logout();

      return router.createUrlTree(['/login'], {
        queryParams: { returnUrl: state.url },
      });
    }
    return true;
  }

  authService.logout();
  return router.createUrlTree(['/login'], {
    queryParams: { returnUrl: state.url },
  });
};
