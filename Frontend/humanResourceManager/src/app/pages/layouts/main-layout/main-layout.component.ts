import { Component, OnInit } from '@angular/core';
import { AuthService, JwtPayload } from 'src/app/AuthService/auth.service';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit {
  isCollapsed = false; 
  userInfo: JwtPayload | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.userInfo = this.authService.getUserInfo();
    console.log(this.userInfo?.permissions);
  }

  logOut(){
    this.authService.logout();
  }

  checkPermission(permission: string): boolean {
    if (!this.userInfo || !this.userInfo.permissions) {
      return false;
    }
    // Tách chuỗi permissions thành mảng, bỏ khoảng trắng nếu có
  const permissionsArray = this.userInfo.permissions.split(',').map(p => p.trim());

  return permissionsArray.includes(permission);
    // return this.userInfo.permissions.includes(permission);
  }
}
