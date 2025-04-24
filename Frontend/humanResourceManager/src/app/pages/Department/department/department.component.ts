import { Component } from '@angular/core';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent {
  departments = [
    { name: 'Phòng Kinh doanh', description: 'Chịu trách nhiệm về bán hàng' },
    { name: 'Phòng Nhân sự', description: 'Quản lý nhân viên và các hoạt động liên quan' },
    { name: 'Phòng Kỹ thuật', description: 'Chịu trách nhiệm về bảo trì và sửa chữa' }
  ];

  // Từ khoá tìm kiếm
  searchKeyword = '';

  // Thêm phòng ban
  onAdd() {
    console.log('Thêm phòng ban mới');
    // Logic thêm phòng ban mới
  }

  // Sửa phòng ban
  onEdit(department: any) {
    console.log('Sửa phòng ban: ', department);
    // Logic sửa phòng ban
  }

  // Xóa phòng ban
  onDelete(department: any) {
    console.log('Xoá phòng ban: ', department);
    // Logic xoá phòng ban
  }
}
