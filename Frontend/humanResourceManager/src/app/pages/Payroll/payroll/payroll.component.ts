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
  { employeeCode: 'E002', employeeName: 'Trần Thị B', monthYear: new Date('2024-01-01'), baseSalary: 8000000, allowance: 1500000, totalSalary: 9500000 },
  { employeeCode: 'E003', employeeName: 'Lê Minh Cường', monthYear: new Date('2024-01-01'), baseSalary: 9000000, allowance: 1000000, totalSalary: 10000000 },
  { employeeCode: 'E004', employeeName: 'Phạm Thu Hà', monthYear: new Date('2024-01-01'), baseSalary: 8500000, allowance: 1200000, totalSalary: 9700000 },
  { employeeCode: 'E005', employeeName: 'Đặng Hoàng Nam', monthYear: new Date('2024-01-01'), baseSalary: 9500000, allowance: 2500000, totalSalary: 12000000 },
  { employeeCode: 'E006', employeeName: 'Vũ Ngọc Lan', monthYear: new Date('2024-01-01'), baseSalary: 7000000, allowance: 1000000, totalSalary: 8000000 },
  { employeeCode: 'E007', employeeName: 'Trịnh Quốc Dũng', monthYear: new Date('2024-01-01'), baseSalary: 11000000, allowance: 3000000, totalSalary: 14000000 },
  { employeeCode: 'E008', employeeName: 'Ngô Bích Hồng', monthYear: new Date('2024-01-01'), baseSalary: 7800000, allowance: 1200000, totalSalary: 9000000 },
  { employeeCode: 'E009', employeeName: 'Tạ Văn Hưng', monthYear: new Date('2024-01-01'), baseSalary: 9200000, allowance: 1300000, totalSalary: 10500000 },
  { employeeCode: 'E010', employeeName: 'Lý Thị Mai', monthYear: new Date('2024-01-01'), baseSalary: 8000000, allowance: 1000000, totalSalary: 9000000 }
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
