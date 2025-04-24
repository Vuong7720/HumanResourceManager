import { Component } from '@angular/core';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent {
  searchKeyword = '';
  employees = [
    {
      code: 'EMP001',
      name: 'Nguyễn Văn A',
      department: 'Kỹ thuật',
      position: 'Kỹ sư phần mềm',
      startDate: new Date('2022-01-15')
    },
    {
      code: 'EMP002',
      name: 'Trần Thị B',
      department: 'Nhân sự',
      position: 'Chuyên viên tuyển dụng',
      startDate: new Date('2021-10-01')
    }
  ];

  onAdd() {
    // Hiển thị modal hoặc điều hướng form thêm mới
    console.log('Thêm nhân viên');
  }

  onView(emp: any) {
    // Xem chi tiết nhân viên
    console.log('Xem', emp);
  }

  onEdit(emp: any) {
    // Sửa thông tin nhân viên
    console.log('Sửa', emp);
  }

  onDelete(emp: any) {
    // Xác nhận và xoá nhân viên
    console.log('Xoá', emp);
  }
}
