import { Component, ViewContainerRef } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { ToastrService } from 'ngx-toastr';
import { PagingRequest, Client } from 'src/app/api2/api.client';
import { PagedResultDto, Message } from 'src/app/api2/dto';
import { DeleteComfirmComponent } from 'src/app/shared/delete-confirm/delete-confirm.component';
import { CreateDepartmentComponent } from '../../Department/department/create-department/create-department.component';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.scss']
})
export class AttendanceComponent {
  entityRequest = {
    field: '',
    fieldOption: true,
    pageSize: 10,
    pageNumber: 1,
    keyword: '',
    filterDate: new Date(), // 🟢 Ngày hôm nay
    lateAfter: undefined,
    leaveAfter: undefined
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
  date = null;
  lateAfterTime: any;
  leaveAfterTime: any;

  constructor(
    private modal: NzModalService,
    private viewContainerRef: ViewContainerRef,
    private service: Client,
    private toastr: ToastrService
  ) { }
  ngOnInit(): void {
    // this.loadData();
  }

  onChange(result: Date): void {
    this.entityRequest.filterDate = result;
    this.loading = true;
    this.loadData();
  }

  onLateAfterChange(event: any) {
    this.entityRequest.lateAfter = event;
    this.loading = true;
    this.loadData();
  }
  onLeaveAfterChange(event: any) {
    this.entityRequest.leaveAfter = event;
    this.loading = true;
    this.loadData();
  }

  loadData() {
    const request = PagingRequest.fromJS({
      field: this.sortKey ?? 'CreationTime',
      fieldOption: this.sortValue === 'descend',
      pageSize: this.entityRequest.pageSize,
      pageNumber: this.entityRequest.pageNumber,
      keyword: this.entityRequest.keyword,
      filterDate: this.entityRequest.filterDate,
      lateAfter: this.entityRequest.lateAfter,
      leaveAfter: this.entityRequest.leaveAfter
    });

    this.service.getListDto(request).then((response: any) => {
      if (response && response.data) {
        this.listOfData.totalCount = response.data.totalCount;
        this.listOfData.items = response.data.items;
        this.processedData = this.listOfData.items!.map(item => ({ ...item }));
        this.loading = false;
        this.isSpinning = false;
      } else {
        this.toastr.error(response?.message || 'Lỗi khi tải dữ liệu!');
      }
    });
  }


  // Từ khoá tìm kiếm
  searchKeyword = '';



  searchData(reset: boolean = false): void {
    if (reset) this.entityRequest.pageNumber = 1;
    this.loading = true;
    this.loadData();
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { sort } = params;
    const currentSort = sort.find(item => item.value !== null);
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
    this.listOfData.items?.forEach(item => this.updateCheckedSet(item.id, value));
    this.refreshCheckedStatus();
  }

  refreshCheckedStatus(): void {
    const items = this.listOfData.items;
    this.checked = items.every(item => this.setOfCheckedId.has(item.id));
    this.indeterminate = items.some(item => this.setOfCheckedId.has(item.id)) && !this.checked;
  }

  resetSelected(): void {
    this.checked = false;
    this.setOfCheckedId.clear();
  }
}
