import { Component } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent {
  // Danh sách người dùng
  users = [
    { code: 'U001', name: 'Nguyễn Văn A', email: 'a@example.com', role: 'Admin', status: 'Active' },
    { code: 'U002', name: 'Trần Thị B', email: 'b@example.com', role: 'Manager', status: 'Inactive' },
    { code: 'U003', name: 'Lê Minh Cường', email: 'cuong.le@example.com', role: 'Staff', status: 'Active' },
    { code: 'U004', name: 'Phạm Thu Hà', email: 'ha.pham@example.com', role: 'HR', status: 'Active' },
    { code: 'U005', name: 'Đặng Hoàng Nam', email: 'nam.dang@example.com', role: 'IT', status: 'Inactive' },
    { code: 'U006', name: 'Vũ Ngọc Lan', email: 'lan.vu@example.com', role: 'Accountant', status: 'Active' },
    { code: 'U007', name: 'Trịnh Quốc Dũng', email: 'dung.trinh@example.com', role: 'Manager', status: 'Active' },
    { code: 'U008', name: 'Ngô Bích Hồng', email: 'hong.ngo@example.com', role: 'Support', status: 'Active' },
    { code: 'U009', name: 'Tạ Văn Hưng', email: 'hung.ta@example.com', role: 'Admin', status: 'Inactive' },
    { code: 'U010', name: 'Lý Thị Mai', email: 'mai.ly@example.com', role: 'Staff', status: 'Active' }
  ];
  

  // Từ khoá tìm kiếm
  searchKeyword = '';

  // Thêm người dùng
  onAdd() {
    console.log('Thêm người dùng mới');
    // Logic thêm người dùng mới
  }

  // Sửa người dùng
  onEdit(user: any) {
    console.log('Sửa người dùng: ', user);
    // Logic sửa người dùng
  }

  // Xóa người dùng
  onDelete(user: any) {
    console.log('Xoá người dùng: ', user);
    // Logic xoá người dùng
  }
}
