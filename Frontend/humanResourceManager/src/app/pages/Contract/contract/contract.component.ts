import { Component } from '@angular/core';

@Component({
  selector: 'app-contract',
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.scss']
})
export class ContractComponent {
  contracts = [
    { code: 'HD001', employeeName: 'Nguyễn Văn A', startDate: new Date('2023-01-01'), endDate: new Date('2023-12-31'), position: 'Nhân viên' },
    { code: 'HD002', employeeName: 'Trần Thị B', startDate: new Date('2023-02-01'), endDate: new Date('2024-02-01'), position: 'Quản lý' },
    { code: 'HD003', employeeName: 'Lê Minh Cường', startDate: new Date('2023-03-01'), endDate: new Date('2024-03-01'), position: 'Kỹ sư phần mềm' },
    { code: 'HD004', employeeName: 'Phạm Thu Hà', startDate: new Date('2023-04-15'), endDate: new Date('2024-04-15'), position: 'Chuyên viên đào tạo' },
    { code: 'HD005', employeeName: 'Đặng Hoàng Nam', startDate: new Date('2023-05-01'), endDate: new Date('2024-05-01'), position: 'Giám sát kỹ thuật' },
    { code: 'HD006', employeeName: 'Vũ Ngọc Lan', startDate: new Date('2023-06-01'), endDate: new Date('2024-06-01'), position: 'Nhân viên hành chính' },
    { code: 'HD007', employeeName: 'Trịnh Quốc Dũng', startDate: new Date('2023-07-01'), endDate: new Date('2024-07-01'), position: 'Trưởng phòng' },
    { code: 'HD008', employeeName: 'Ngô Bích Hồng', startDate: new Date('2023-08-01'), endDate: new Date('2024-08-01'), position: 'Kế toán' },
    { code: 'HD009', employeeName: 'Tạ Văn Hưng', startDate: new Date('2023-09-01'), endDate: new Date('2024-09-01'), position: 'Kỹ sư QA' },
    { code: 'HD010', employeeName: 'Lý Thị Mai', startDate: new Date('2023-10-01'), endDate: new Date('2024-10-01'), position: 'Trợ lý giám đốc' }
  ];
  

  // Từ khoá tìm kiếm
  searchKeyword = '';

  // Thêm hợp đồng
  onAdd() {
    console.log('Thêm hợp đồng mới');
    // Logic thêm hợp đồng mới
  }

  // Sửa hợp đồng
  onEdit(contract: any) {
    console.log('Sửa hợp đồng: ', contract);
    // Logic sửa hợp đồng
  }

  // Xóa hợp đồng
  onDelete(contract: any) {
    console.log('Xoá hợp đồng: ', contract);
    // Logic xoá hợp đồng
  }
  onView(){}
}
