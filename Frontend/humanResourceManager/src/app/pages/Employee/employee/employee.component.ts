import { Component } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { CreateeployeeComponent } from '../create-createemployee/create-createemployee.component';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent {

  constructor(private modal: NzModalService) {}

  searchKeyword = '';
  selectAll = false; // Thêm biến chọn tất cả
  checked = false;
  employees = [
    {
      code: 'EMP001',
      name: 'Nguyễn Văn A',
      department: 'Kỹ thuật',
      position: 'Kỹ sư phần mềm',
      startDate: new Date('2022-01-15'),
      isSelected: false
    },
    {
      code: 'EMP002',
      name: 'Trần Thị B',
      department: 'Nhân sự',
      position: 'Chuyên viên tuyển dụng',
      startDate: new Date('2021-10-01'),
      isSelected: false
    },
    {
      code: 'EMP003',
      name: 'Lê Minh Cường',
      department: 'Tài chính',
      position: 'Kế toán trưởng',
      startDate: new Date('2020-06-12'),
      isSelected: false
    },
    {
      code: 'EMP004',
      name: 'Phạm Thị Dung',
      department: 'Marketing',
      position: 'Chuyên viên nội dung',
      startDate: new Date('2023-03-20'),
      isSelected: false
    },
    {
      code: 'EMP005',
      name: 'Hoàng Văn Đức',
      department: 'Kỹ thuật',
      position: 'Lập trình viên backend',
      startDate: new Date('2021-11-05'),
      isSelected: false
    },
    {
      code: 'EMP006',
      name: 'Vũ Thị Hạnh',
      department: 'Nhân sự',
      position: 'Trưởng phòng nhân sự',
      startDate: new Date('2019-07-18'),
      isSelected: false
    },
    {
      code: 'EMP007',
      name: 'Đặng Quang Huy',
      department: 'Kinh doanh',
      position: 'Nhân viên kinh doanh',
      startDate: new Date('2022-09-10'),
      isSelected: false
    },
    {
      code: 'EMP008',
      name: 'Ngô Thị Lan',
      department: 'Chăm sóc khách hàng',
      position: 'Chuyên viên CSKH',
      startDate: new Date('2020-01-25'),
      isSelected: false
    },
    {
      code: 'EMP009',
      name: 'Trịnh Văn Lâm',
      department: 'IT Support',
      position: 'Kỹ thuật viên',
      startDate: new Date('2023-01-02'),
      isSelected: false
    },
    {
      code: 'EMP010',
      name: 'Mai Thị Ngọc',
      department: 'Thiết kế',
      position: 'UI/UX Designer',
      startDate: new Date('2021-05-15'),
      isSelected: false
    }
  ];
  

  onAdd(): void {
    console.log('Thêm nhân viên');
    this.modal.create({
      nzTitle: 'Thêm mới nhân viên',
      nzContent: CreateeployeeComponent,
      nzWidth: 800,
      nzFooter: null, // để footer tự do bên trong component CreateEmployee
    });
  }

  onView(emp: any) {
    console.log('Xem', emp);
  }

  onEdit(emp: any) {
    console.log('Sửa', emp);
  }

  onDelete(emp: any) {
    console.log('Xoá', emp);
  }

  onDeleteMultiple() {
    // Lọc ra những nhân viên được chọn
    const selectedEmployees = this.employees.filter(emp => emp.isSelected);
    if (selectedEmployees.length === 0) {
      alert('Vui lòng chọn ít nhất một nhân viên để xoá!');
      return;
    }
    if (confirm(`Bạn có chắc chắn muốn xoá ${selectedEmployees.length} nhân viên?`)) {
      this.employees = this.employees.filter(emp => !emp.isSelected);
      console.log('Đã xoá các nhân viên:', selectedEmployees);
    }
  }

  toggleAllSelection(event: any) {
    this.selectAll = event.target.checked;
    this.employees.forEach(emp => emp.isSelected = this.selectAll);
  }
}
