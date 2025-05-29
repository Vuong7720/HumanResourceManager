import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { AuthService, JwtPayload } from 'src/app/AuthService/auth.service';
import { Client } from 'src/app/api2/api.client';

@Component({
  selector: 'app-create-createemployee',
  templateUrl: './create-createemployee.component.html',
  styleUrls: ['./create-createemployee.component.scss'],
})
export class CreateeployeeComponent implements OnInit {
  isVisible = true; // điều khiển modal
  getParams = inject(NZ_MODAL_DATA);
  form!: FormGroup;
  data = {} as any;
  isSpinning = true;
  isEditMode = false;
  lstDepartment: any[] = [];
  lstPosition: any[] = [];

  userInfo: JwtPayload | null = null;

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private service: Client,
    private nzModalRef: NzModalRef,
    private authService: AuthService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.userInfo = this.authService.getUserInfo();
    if (!this.userInfo?.permissions?.split(',').map(p => p.trim()).includes("EmployeeManagement_Create") && 
          !this.userInfo?.permissions?.split(',').map(p => p.trim()).includes("EmployeeManagement_Update")) {
        this.router.navigate(['access-deny']);
      }

    this.getDeparment();
    this.getPosition();
    this.buildForm();
    if (this.getParams.data) {
      this.data = this.getParams.data;
      this.isEditMode = true;
      this.buildForm();
    }
  }

  buildForm() {
    this.form = this.fb.group({
      fullName: [
        this.data.fullName || null,
        [Validators.required, Validators.maxLength(255)],
      ],
      birthDay:[this.data.birthDay || null],
      gender: [this.data.gender || null],
      phoneNumber: [this.data.phoneNumber || null],
      email: [this.data.email || null],
      address: [this.data.address || null],
      positionId: [this.data.positionId || null],
      departmentId: [this.data.departmentId || null],
      salary: [this.data.salary || null],
      hireDate: [this.data.hireDate || null],
      endDate: [this.data.endDate || null],
      contractType: [this.data.contractType || null],
    });
    this.isSpinning = false;
  }

  onSubmit(): void {
    this.isSpinning = true;
    if (this.form.valid) {
      this.data.id
        ? this.service
            .update4(this.data.id, this.form.value)
            .then((res: any) => {
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
        : this.service.create4(this.form.value).then((res: any) => {
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

  onBack(): void {
    this.nzModalRef.destroy();
  }

  getDeparment() {
    this.service.getListSelect().then((res: any) => {
      if (res) {
        this.lstDepartment = res.data;
      } else {
        this.toastr.error('Lấy danh sách phòng ban');
      }
    });
  }

  getPosition() {
    this.service.getListSelect3().then((res: any) => {
      if (res) {
        this.lstPosition = res.data;
      } else {
        this.toastr.error('Lấy danh sách chức vụ');
      }
    });
  }

  lstGender = [
    { id: 1, name: 'Nam' },
    { id: 2, name: 'Nữ' },
    { id: 3, name: 'Khác' },
  ];

  lstcontractType = [
    { id: 1, name: 'Hợp đồng chính thức' },
    { id: 2, name: 'Hợp đồng thử việc' },
    { id: 3, name: 'Hợp đồng thời vụ' },
  ]


  // ----------------------------

  formattedSalary: string = '';

onInputSalary(event: any): void {
  const raw = event.target.value.replace(/\D/g, ''); // chỉ lấy số
  this.form.get('salary')?.setValue(Number(raw));
  this.formattedSalary = this.formatNumber(raw);
}

onFocusSalary(): void {
  const raw = this.form.get('salary')?.value;
  this.formattedSalary = raw ? raw.toString() : '';
}

onBlurSalary(): void {
  const raw = this.form.get('salary')?.value;
  this.formattedSalary = this.formatNumber(raw);
}

formatNumber(value: any): string {
  if (!value) return '';
  return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

}
