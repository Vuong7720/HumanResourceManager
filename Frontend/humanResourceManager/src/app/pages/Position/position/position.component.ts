import { Component } from '@angular/core';

@Component({
  selector: 'app-position',
  templateUrl: './position.component.html',
  styleUrls: ['./position.component.scss']
})
export class PositionComponent {
 // Danh sách chức vụ
 positions = [
  { name: 'Quản lý', description: 'Chức vụ quản lý nhân viên' },
  { name: 'Nhân viên', description: 'Chức vụ nhân viên văn phòng' },
  { name: 'Giám đốc', description: 'Chức vụ cao cấp trong công ty' }
];

// Từ khoá tìm kiếm
searchKeyword = '';

// Thêm chức vụ
onAdd() {
  console.log('Thêm chức vụ mới');
  // Logic thêm chức vụ mới
}

// Sửa chức vụ
onEdit(position: any) {
  console.log('Sửa chức vụ: ', position);
  // Logic sửa chức vụ
}

// Xóa chức vụ
onDelete(position: any) {
  console.log('Xoá chức vụ: ', position);
  // Logic xoá chức vụ
}
}
