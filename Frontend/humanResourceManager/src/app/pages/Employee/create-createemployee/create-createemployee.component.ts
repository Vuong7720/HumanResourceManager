import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-createemployee',
  templateUrl: './create-createemployee.component.html',
  styleUrls: ['./create-createemployee.component.scss']
})
export class CreateeployeeComponent implements OnInit {
  isVisible = true; // điều khiển modal
  employeeForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.employeeForm = this.fb.group({
      fullName: ['', Validators.required],
      birthDay: [null, Validators.required],
      gender: [0, Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      address: ['', Validators.required],
      positionId: [null, Validators.required],
      departmentId: [null, Validators.required],
      salary: [null, Validators.required],
      hireDate: [null, Validators.required],
      status: [0, Validators.required],
    });
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  onSubmit(): void {
    if (this.employeeForm.valid) {
      console.log('Dữ liệu gửi đi:', this.employeeForm.value);
      this.isVisible = false; // đóng modal
    } else {
      console.log('Form không hợp lệ');
    }
  }

  handleCancel(): void {
    this.isVisible = false;
  }
}
