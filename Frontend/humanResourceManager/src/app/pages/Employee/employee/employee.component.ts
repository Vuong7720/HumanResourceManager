import { Component, ViewContainerRef } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { CreateeployeeComponent } from '../create-createemployee/create-createemployee.component';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import {
  Client,
  CreateUpdateEmployeesDto,
  PagingRequest,
} from 'src/app/api2/api.client';
import { Message, PagedResultDto } from 'src/app/api2/dto';
import { ToastrService } from 'ngx-toastr';
import { DeleteComfirmComponent } from 'src/app/shared/delete-confirm/delete-confirm.component';
import { CreateDepartmentComponent } from '../../Department/department/create-department/create-department.component';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss'],
})
export class EmployeeComponent {
  entityRequest = {
    field: '',
    fieldOption: true,
    pageSize: 10,
    pageNumber: 1,
    keyword: '',
  } as PagingRequest;

  checked = false;
  setOfCheckedId = new Set<string>();
  indeterminate = false;

  listOfData = { items: [], totalCount: 0 } as PagedResultDto<any>;
  processedData: Array<any> = [];

  loading = true;
  sortValue: string | null = null;
  sortKey: string | null = null;
  isSpinning: boolean = false;
  lstDepartment: any[] = [];
  lstPosition: any[] = [];

  constructor(
    private modal: NzModalService,
    private viewContainerRef: ViewContainerRef,
    private service: Client,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getDeparment();
    this.getPosition();
    // this.loadData();
  }

  loadData() {
    const request = PagingRequest.fromJS({
      field: this.sortKey ?? 'FullName',
      fieldOption: this.sortValue === 'descend',
      pageSize: this.entityRequest.pageSize,
      pageNumber: this.entityRequest.pageNumber,
      keyword: this.entityRequest.keyword,
    });

    this.service.getListDto4(request).then((response: any) => {
      if (response && response.data) {
        this.listOfData.totalCount = response.data.totalCount;
        this.listOfData.items = response.data.items;
        this.processedData = this.listOfData.items!.map((item) => ({
          ...item,
        }));
        this.loading = false;
        this.isSpinning = false;
      } else {
        this.toastr.error(response?.message || 'Lỗi khi tải dữ liệu!');
      }
    });
  }

  // Từ khoá tìm kiếm
  searchKeyword = '';

  // Thêm phòng ban
  onAdd(data?: any) {
    const modalRef = this.modal.create({
      nzTitle: '',
      nzContent: CreateeployeeComponent,
      nzViewContainerRef: this.viewContainerRef,
      nzFooter: null,
      nzCentered: true,
      nzClosable: true,
      nzKeyboard: false,
      nzData: { data },
      nzClassName: 'w-modal-dialog',
    });

    modalRef.afterClose.subscribe((result: Message) => {
      if (result?.Success) this.loadData();
    });
  }

  // Xóa nhân viên
  onDelete(id: number) {
    const modalConfig = {
      nzTitle: '',
      nzContent: DeleteComfirmComponent,
      nzViewContainerRef: this.viewContainerRef,
      nzBackdrop: false,
      nzFooter: null,
      nzCentered: true,
      nzMaskClosable: false,
      nzClosable: false,
      nzKeyboard: false,
      nzData: {
        title: 'Xóa nhân viên',
        content: 'Bạn có chắc chắn muốn xóa nhân viên không?',
      },
      nzClassName: 'w-modal-dialog',
    };
    const modalRef = this.modal.create(modalConfig);
    const instanceRef = modalRef.getContentComponent();
    instanceRef.onLoadData.subscribe((response) => {
      this.service.delete4(id).then(() => {
        this.toastr.success('Xóa thành công!');
        this.loadData();
      });
    });
  }

  searchData(reset: boolean = false): void {
    if (reset) this.entityRequest.pageNumber = 1;
    this.loading = true;
    this.loadData();
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { sort } = params;
    const currentSort = sort.find((item) => item.value !== null);
    this.sortKey = currentSort?.key || null;
    this.sortValue = currentSort?.value || null;
    this.searchData();
  }

  onPageSizeChange(_: number): void {
    this.loadData();
  }

  onPageIndexChange(index: number): void {
    this.entityRequest.pageNumber = index;
    this.loadData();
  }

  updateCheckedSet(id: string, checked: boolean): void {
    if (checked) this.setOfCheckedId.add(id);
    else this.setOfCheckedId.delete(id);
  }

  onItemChecked(id: string, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    this.refreshCheckedStatus();
  }

  onAllChecked(value: boolean): void {
    this.listOfData.items?.forEach((item) =>
      this.updateCheckedSet(item.id, value)
    );
    this.refreshCheckedStatus();
  }

  refreshCheckedStatus(): void {
    const items = this.listOfData.items;
    this.checked = items.every((item) => this.setOfCheckedId.has(item.id));
    this.indeterminate =
      items.some((item) => this.setOfCheckedId.has(item.id)) && !this.checked;
  }

  resetSelected(): void {
    this.checked = false;
    this.setOfCheckedId.clear();
  }

  getNameGender(id: any): string {
    return (
      this.lstGender.find((item) => item.id === id)?.name ?? 'Không xác định'
    );
  }

  getNameContractType(id: any): string {
    return (
      this.lstcontractType.find((item) => item.id === id)?.name ??
      'Không xác định'
    );
  }

  getDeparment() {
    this.service.getListSelect().then((res: any) => {
      if (res) {
        this.lstDepartment = res.data;
      } else {
        this.toastr.error('Lấy danh sách phòng ban');
      }
    });
  }

  getPosition() {
    this.service.getListSelect3().then((res: any) => {
      if (res) {
        this.lstPosition = res.data;
      } else {
        this.toastr.error('Lấy danh sách chức vụ');
      }
    });
  }

  getNamePosition(id: any): string {
    return (
      this.lstPosition.find((item) => item.value === id)?.label ??
      'Không xác định'
    );
  }

  getNameDepartment(id: any): string {
    return (
      this.lstDepartment.find((item) => item.value === id)?.label ??
      'Không xác định'
    );
  }

  lstGender = [
    { id: 1, name: 'Nam' },
    { id: 2, name: 'Nữ' },
    { id: 3, name: 'Khác' },
  ];

  lstcontractType = [
    { id: 1, name: 'Hợp đồng chính thức' },
    { id: 2, name: 'Hợp đồng thử việc' },
    { id: 3, name: 'Hợp đồng thời vụ' },
  ];
}
