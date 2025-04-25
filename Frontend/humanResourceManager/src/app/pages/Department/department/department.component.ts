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
    { name: 'Phòng Kỹ thuật', description: 'Chịu trách nhiệm về bảo trì và sửa chữa' },
    { name: 'Phòng Tài chính', description: 'Quản lý ngân sách và báo cáo tài chính' },
    { name: 'Phòng Marketing', description: 'Lập kế hoạch và triển khai các chiến dịch tiếp thị' },
    { name: 'Phòng IT', description: 'Quản trị hệ thống và hạ tầng công nghệ' },
    { name: 'Phòng Pháp chế', description: 'Đảm bảo tuân thủ pháp luật và quy định' },
    { name: 'Phòng Nghiên cứu & Phát triển', description: 'Phát triển sản phẩm mới và công nghệ' },
    { name: 'Phòng Chăm sóc khách hàng', description: 'Giải đáp và hỗ trợ khách hàng' },
    { name: 'Phòng Hành chính', description: 'Quản lý cơ sở vật chất và hành chính văn phòng' }
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
