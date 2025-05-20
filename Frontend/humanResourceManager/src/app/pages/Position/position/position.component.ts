import { Component, ViewContainerRef } from '@angular/core';
import { DeleteComfirmComponent } from 'src/app/shared/delete-confirm/delete-confirm.component';
import { Client, PagingRequest } from 'src/app/api2/api.client';
import { Message, PagedResultDto } from 'src/app/api2/dto';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { CreatePositionComponent } from './create-position/create-position.component';

@Component({
  selector: 'app-position',
  templateUrl: './position.component.html',
  styleUrls: ['./position.component.scss']
})
export class PositionComponent {
  entityRequest = {
    field: '',
    fieldOption: true,
    pageSize: 10,
    pageNumber: 1,
    keyword: ''
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


  constructor(
    private modal: NzModalService, 
    private viewContainerRef: ViewContainerRef, 
    private service: Client,
    private toastr: ToastrService
  ) { }
  ngOnInit(): void {
    this.loadData();
  }


  loadData() {
    
  const request = PagingRequest.fromJS({
    field: this.sortKey ?? 'CreationTime',
    fieldOption: this.sortValue === 'descend',
    pageSize: this.entityRequest.pageSize,
    pageNumber: this.entityRequest.pageNumber,
    keyword: this.entityRequest.keyword
  });

  this.service.getListDto6(request).then((response: any) => {
    console.log('response',response)
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

  // Thêm phòng ban
  onAdd(data?: any) {
    const modalRef = this.modal.create({
      nzTitle: '',
      nzContent: CreatePositionComponent,
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

  // Sửa phòng ban
  onEdit(department: any) {
    console.log('Sửa phòng ban: ', department);
    // Logic sửa phòng ban
  }

  // Xóa phòng ban
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
        title: 'Xóa phòng ban',
        content: 'Bạn có chắc chắn muốn xóa phòng ban không?',
      },
      nzClassName: 'w-modal-dialog',
    };
    const modalRef = this.modal.create(modalConfig);
    const instanceRef = modalRef.getContentComponent();
    instanceRef.onLoadData.subscribe(response => {
      this.service.delete6(id).then((res: any) => {
        if (res) {
          if (res.status) {
            this.toastr.success(res.message);
            this.loadData();
          } else {
            this.toastr.error(res.message);
          }
        } else {
          this.toastr.error(res.message);
        }
      });
    });
    
  }
}
