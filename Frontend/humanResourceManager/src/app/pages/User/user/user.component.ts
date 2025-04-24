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
    { code: 'U002', name: 'Trần Thị B', email: 'b@example.com', role: 'Manager', status: 'Inactive' }
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
