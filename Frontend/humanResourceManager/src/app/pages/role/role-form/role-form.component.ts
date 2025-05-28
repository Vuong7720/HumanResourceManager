import { Component, inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { AuthService, JwtPayload } from 'src/app/AuthService/auth.service';
import { RoleDto, SelectOptionItems } from 'src/app/api2/role/models';
import { RoleService } from 'src/app/api2/role/role.service';

@Component({
  selector: 'app-role-form',
  templateUrl: './role-form.component.html',
  styleUrls: ['./role-form.component.scss'],
})
export class RoleFormComponent implements OnInit {
  isVisible = true; // điều khiển modal
  getParams = inject(NZ_MODAL_DATA);
  form!: FormGroup;
  data = {} as RoleDto;
  isSpinning = true;
  isEditMode = false;

  listPermissionSelectData = [] as SelectOptionItems[];
  groupedPermissions: { [key: string]: SelectOptionItems[] } = {};

  userInfo: JwtPayload | null = null;

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private service: RoleService,
    private nzModalRef: NzModalRef,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.userInfo = this.authService.getUserInfo();
    this.loadDataPermission();
    this.buildForm();
    if (this.getParams.data) {
      this.data = this.getParams.data;
      this.isEditMode = true;
      this.buildForm();
    }
  }

  loadDataPermission() {
    this.service
      .getListSelectPermission()
      .pipe(finalize(() => {}))
      .subscribe((response) => {
        if (response.status) {
          this.listPermissionSelectData = response.data;
          // Nhóm quyền theo module
          this.groupedPermissions = this.listPermissionSelectData.reduce(
            (groups, item) => {
              const groupKey = item.label.includes('_')
                ? item.label.split('_')[0]
                : item.label;
              if (!groups[groupKey]) {
                groups[groupKey] = [];
              }
              groups[groupKey].push(item);
              return groups;
            },
            {} as { [key: string]: SelectOptionItems[] }
          );
        } else {
          this.toastr.error(response.message);
        }
      });
  }

  buildForm() {
    this.form = this.fb.group({
      roleName: [
        { value: this.data.roleName || null, disabled: this.data.isStatic },
        [Validators.required, Validators.maxLength(255)],
      ],
      permissionIds: [this.data.permissionIds || null],
      userName:[this.userInfo?.name || null],
    });
    this.isSpinning = false;
  }

  onSubmit(): void {
    //Khi form có disabled field, Angular không đưa nó vào form.value.
    const formValue = this.form.getRawValue();   // ✅ lấy cả roleName dù bị disable
    this.isSpinning = true;
    if (this.form.valid) {
      this.data.id
        ? this.service
            .update(this.data.id, formValue)
            .subscribe((res: any) => {
              if (res) {
                this.toastr.success('Sửa thành công !');
                this.nzModalRef.close({
                  Success: true,
                  Title: '',
                });
                this.nzModalRef.destroy();
              } else {
                this.isSpinning = true;
                this.toastr.error('Sửa thất bại');
              }
            })
        : this.service.create(formValue).subscribe((res: any) => {
            if (res) {
              this.toastr.success('Thêm mới thành công !');
              this.nzModalRef.close({
                Success: true,
                Title: '',
              });
              this.nzModalRef.destroy();
            } else {
              this.isSpinning = true;
              this.toastr.error('Thêm mới thất bại');
            }
          });
    } else {
      this.isSpinning = true;
      this.toastr.error('Form không hợp lệ');
    }
  }

  onCheckboxChange(id: number, event: any): void {
    const permissionIds: number[] = this.form.get('permissionIds')?.value || [];

    if (event.target.checked) {
      if (!permissionIds.includes(id)) {
        permissionIds.push(id);
      }
    } else {
      const index = permissionIds.indexOf(id);
      if (index > -1) {
        permissionIds.splice(index, 1);
      }
    }

    this.form.get('permissionIds')?.setValue(permissionIds);
  }

  onBack(): void {
    this.nzModalRef.destroy();
  }
}
