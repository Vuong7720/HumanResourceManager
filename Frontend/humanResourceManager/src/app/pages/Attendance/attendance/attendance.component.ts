import { Component } from '@angular/core';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.scss']
})
export class AttendanceComponent {
 // Danh sách điểm danh
 attendances = [
  { employeeCode: 'E001', employeeName: 'Nguyễn Văn A', date: new Date('2024-04-25'), status: 'Có mặt' },
  { employeeCode: 'E002', employeeName: 'Trần Thị B', date: new Date('2024-04-25'), status: 'Vắng' }
];

// Từ khoá tìm kiếm
searchKeyword = '';

// Thêm điểm danh
onAdd() {
  console.log('Thêm điểm danh mới');
  // Logic thêm điểm danh mới
}

// Sửa điểm danh
onEdit(attendance: any) {
  console.log('Sửa điểm danh: ', attendance);
  // Logic sửa điểm danh
}

// Xóa điểm danh
onDelete(attendance: any) {
  console.log('Xoá điểm danh: ', attendance);
  // Logic xoá điểm danh
}

// Xem chi tiết điểm danh
onView(attendance: any) {
  console.log('Xem điểm danh: ', attendance);
  // Logic xem chi tiết điểm danh
}
}
