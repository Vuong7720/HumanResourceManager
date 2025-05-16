import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Client } from 'src/app/api2/api.client';
import { AuthService } from 'src/app/AuthService/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(private fb: FormBuilder, private service: Client, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.service.login(this.loginForm.value).subscribe(res => {
        const fakeToken = res.token; // TODO: Replace with real API

        this.authService.login(fakeToken);
        this.router.navigate(['/dashboard']);
      })
    } else {
      console.log('Form không hợp lệ');
    }
  }
}
