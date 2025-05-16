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
  }

  logOut(){
    this.authService.logout();
  }
}
