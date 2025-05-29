import { Component, inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { AuthService, JwtPayload } from 'src/app/AuthService/auth.service';
import { Client } from 'src/app/api2/api.client';


@Component({
  selector: 'app-create-department',
  templateUrl: './create-department.component.html',
  styleUrls: ['./create-department.component.scss']
})
export class CreateDepartmentComponent implements OnInit {
  isVisible = true; // điều khiển modal
  getParams = inject(NZ_MODAL_DATA);
  form!: FormGroup;
  data = {} as any;
  isSpinning = true;
  isEditMode = false;

  userInfo: JwtPayload | null = null;

  constructor(
    private fb: FormBuilder, 
    private toastr: ToastrService,
    private service: Client,
    private nzModalRef: NzModalRef,
    private authService: AuthService,
    private router: Router,
  ) {

  }

  ngOnInit(): void {
    this.userInfo = this.authService.getUserInfo();
  if (!this.userInfo?.permissions?.split(',').map(p => p.trim()).includes("DepartmentManagement_Create") && 
        !this.userInfo?.permissions?.split(',').map(p => p.trim()).includes("DepartmentManagement_Update")) {
      this.router.navigate(['access-deny']);
    }
    
    this.buildForm();
    if (this.getParams.data) {
      this.data = this.getParams.data;
      this.isEditMode = true;
      this.buildForm();
    }
  }

  buildForm() {
    this.form = this.fb.group({
      departmentName: [this.data.departmentName || null, [Validators.required, Validators.maxLength(255)]],
    })
    this.isSpinning = false;
  }

  onSubmit(): void {
     this.isSpinning = true;
    if (this.form.valid) {
      this.data.id?
      this.service.update3(this.data.id,this.form.value).then((res: any) =>{
        if(res){
          this.toastr.success('Sửa thành công !')
          this.nzModalRef.close({
              Success: true,
              Title: '',
            });
          this.nzModalRef.destroy();
        }else{
          this.isSpinning = true;
          this.toastr.error('Sửa thất bại')
        }
      })
      : 
      this.service.create3(this.form.value).then((res: any) =>{
        if(res){
          this.toastr.success('Thêm mới thành công !')
          this.nzModalRef.close({
              Success: true,
              Title: '',
            });
           this.nzModalRef.destroy();
        }else{
          this.isSpinning = true;
          this.toastr.error('Thêm mới thất bại')
        }
      })
    } else {
      this.isSpinning = true;
      this.toastr.error('Form không hợp lệ');
    }
  }

   onBack(): void {
    this.nzModalRef.destroy();
  }
}
