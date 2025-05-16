// auth.service.ts
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode'; // ✅ Đúng chuẩn

export interface JwtPayload {
  sub: string;
  name: string;
  email?: string;
  role?: string;
  exp: number;
}

export interface JwtPayloadRaw {
  [key: string]: any;
  exp: number;
  iss?: string;
  aud?: string;
}


@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private router: Router) {}

  // Gọi sau khi login thành công
  login(token: string) {
    localStorage.setItem('access_token', token);
  }

  logout() {
    localStorage.removeItem('access_token');
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('access_token');
  }

  getToken(): string | null {
    return localStorage.getItem('access_token');
  }

  getUserInfo(): JwtPayload | null {
  const token = this.getToken();
  if (!token) return null;

  try {
    const raw = jwtDecode<JwtPayloadRaw>(token);
    return {
      sub: raw["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
      name: raw["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
      role: raw["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
      exp: raw.exp,
    };
  } catch (e) {
    return null;
  }
}

}
