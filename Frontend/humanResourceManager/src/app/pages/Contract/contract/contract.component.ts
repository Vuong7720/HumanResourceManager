import { Component } from '@angular/core';

@Component({
  selector: 'app-contract',
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.scss']
})
export class ContractComponent {
  contracts = [
    { code: 'HD001', employeeName: 'Nguyễn Văn A', startDate: new Date('2023-01-01'), endDate: new Date('2023-12-31'), position: 'Nhân viên' },
    { code: 'HD002', employeeName: 'Trần Thị B', startDate: new Date('2023-02-01'), endDate: new Date('2024-02-01'), position: 'Quản lý' }
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
}
