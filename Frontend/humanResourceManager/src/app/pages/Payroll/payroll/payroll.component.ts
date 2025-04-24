import { Component } from '@angular/core';

@Component({
  selector: 'app-payroll',
  templateUrl: './payroll.component.html',
  styleUrls: ['./payroll.component.scss']
})
export class PayrollComponent {
 // Danh sách bảng lương
 payrolls = [
  { employeeCode: 'E001', employeeName: 'Nguyễn Văn A', monthYear: new Date('2024-01-01'), baseSalary: 10000000, allowance: 2000000, totalSalary: 12000000 },
  { employeeCode: 'E002', employeeName: 'Trần Thị B', monthYear: new Date('2024-01-01'), baseSalary: 8000000, allowance: 1500000, totalSalary: 9500000 }
];

// Từ khoá tìm kiếm
searchKeyword = '';

// Thêm bảng lương
onAdd() {
  console.log('Thêm bảng lương mới');
  // Logic thêm bảng lương mới
}

// Sửa bảng lương
onEdit(payroll: any) {
  console.log('Sửa bảng lương: ', payroll);
  // Logic sửa bảng lương
}

// Xóa bảng lương
onDelete(payroll: any) {
  console.log('Xoá bảng lương: ', payroll);
  // Logic xoá bảng lương
}

// Xem chi tiết bảng lương
onView(payroll: any) {
  console.log('Xem bảng lương: ', payroll);
  // Logic xem chi tiết bảng lương
}
}
