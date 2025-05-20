import { Component, inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/api2/api.client';


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

  constructor(
    private fb: FormBuilder, 
    private toastr: ToastrService,
    private service: Client,
    private nzModalRef: NzModalRef
  ) {

  }

  ngOnInit(): void {
    this.buildForm();
    if (this.getParams.data) {
      this.data = this.getParams.data;
      this.isEditMode = true;
      this.buildForm();
    }
  }

  buildForm() {
    this.form = this.fb.group({
      username: [this.data.username || null, [Validators.required, Validators.maxLength(255)]],
      password: [this.data.password || null, [Validators.required, Validators.maxLength(255)]],
      employeeID: [this.data.employeeID || null]
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
      this.service.register(this.form.value).then((res: any) =>{
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
