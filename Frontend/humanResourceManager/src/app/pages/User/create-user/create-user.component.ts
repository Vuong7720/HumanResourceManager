import { Component, inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { AuthService, JwtPayload } from 'src/app/AuthService/auth.service';
import { Client } from 'src/app/api2/api.client';
import { SelectOptionItems } from 'src/app/api2/role/models';
import { RoleService } from 'src/app/api2/role/role.service';


@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit {
  isVisible = true; // điều khiển modal
  getParams = inject(NZ_MODAL_DATA);
  form!: FormGroup;
  data = {} as any;
  isSpinning = true;
  isEditMode = false;
  lstEmployee: any[] = [];

  listRoleSelectData = [] as SelectOptionItems[];

  userInfo: JwtPayload | null = null;

  constructor(
    private fb: FormBuilder, 
    private toastr: ToastrService,
    private service: Client,
    private nzModalRef: NzModalRef,
    private roleService: RoleService,
    private authService: AuthService,
    private router: Router,
  ) {

  }

  ngOnInit(): void {
    this.userInfo = this.authService.getUserInfo();
    if (!this.userInfo?.permissions?.split(',').map(p => p.trim()).includes("UserManagement_Create") && 
        !this.userInfo?.permissions?.split(',').map(p => p.trim()).includes("UserManagement_Update")) {
      this.router.navigate(['access-deny']);
    }

    this.loadDataRoles();
    this.getEmploy();
    this.buildForm();
    if (this.getParams.data) {
      this.data = this.getParams.data;
      console.log(this.data);
      this.isEditMode = true;
      this.buildForm();
    }
  }

  buildForm() {
    this.form = this.fb.group({
      username: [this.data.username || null, [Validators.required, Validators.maxLength(255)]],
      password: [this.data.password || null, [Validators.required, Validators.maxLength(255)]],
      employeeID: [this.data.employeeID || null],
      roleIds: [this.data.roleIds || null],
      role: [this.data.role || null],
    })
    this.isSpinning = false;
  }

  loadDataRoles() {
    this.roleService
      .getListSelectRole()
      .pipe(finalize(() => {}))
      .subscribe((response) => {
        if (response.status) {
          this.listRoleSelectData = response.data;
          
        } else {
          this.toastr.error(response.message);
        }
      });
  }

  onCheckboxChange(id: number, event: any): void {
    const roleIds: number[] = this.form.get('roleIds')?.value || [];

    if (event.target.checked) {
      if (!roleIds.includes(id)) {
        roleIds.push(id);
      }
    } else {
      const index = roleIds.indexOf(id);
      if (index > -1) {
        roleIds.splice(index, 1);
      }
    }

    this.form.get('roleIds')?.setValue(roleIds);
  }

  onSubmit(): void {
     this.isSpinning = true;
    if (this.form.valid) {
      this.data.id?
      this.service.update7(this.data.id,this.form.value).then((res: any) =>{
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
      this.service.create7(this.form.value).then((res: any) =>{
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

  getEmploy(){
    this.service.getListSelect2().then((res: any) => {
      if(res){
        this.lstEmployee = res.data;
      }else{
        this.toastr.error('Lấy danh sách nhân viên thất bại')
      }
    })
  }

  lstRole = [
    {name: 'Admin', value: 1},
    {name: 'HR', value: 2},
    {name: 'Employee', value: 3},
  ]
}
