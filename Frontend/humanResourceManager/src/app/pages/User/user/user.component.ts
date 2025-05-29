import { Component, ViewContainerRef } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { Client, PagingRequest } from 'src/app/api2/api.client';
import { Message, PagedResultDto } from 'src/app/api2/dto';
import { DeleteComfirmComponent } from 'src/app/shared/delete-confirm/delete-confirm.component';
import { CreateDepartmentComponent } from '../../Department/department/create-department/create-department.component';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { CreateUserComponent } from '../create-user/create-user.component';
import { AuthService, JwtPayload } from 'src/app/AuthService/auth.service';
import { Router } from '@angular/router';
import { RoleService } from 'src/app/api2/role/role.service';
import { finalize } from 'rxjs';
import { SelectOptionItems } from 'src/app/api2/role/models';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent {
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

  userInfo: JwtPayload | null = null;
	listRoleSelectData = [] as SelectOptionItems[];
  createPermission:boolean = false;
  updatePermission:boolean = false;
  deletePermission:boolean = false;

  constructor(
    private modal: NzModalService, 
    private viewContainerRef: ViewContainerRef, 
    private service: Client,
    private toastr: ToastrService,
    private authService: AuthService,
    private router: Router,
    private roleService: RoleService,
  ) { }
  ngOnInit(): void {
    this.userInfo = this.authService.getUserInfo();
    this.loadDataRoles();
    if (!this.userInfo?.permissions?.split(',').map(p => p.trim()).includes("UserManagement")) {
      this.router.navigate(['access-deny']);
    }
    if (this.userInfo?.permissions?.split(',').map(p => p.trim()).includes("UserManagement_Create")) {
      this.createPermission = true;
    }
    if (this.userInfo?.permissions?.split(',').map(p => p.trim()).includes("UserManagement_Update")) {
      this.updatePermission = true;
    }
    if (this.userInfo?.permissions?.split(',').map(p => p.trim()).includes("UserManagement_Delete")) {
      this.deletePermission = true;
    }
  }


  loadData() {
  const request = PagingRequest.fromJS({
    field: this.sortKey ?? 'CreationTime',
    fieldOption: this.sortValue === 'descend',
    pageSize: this.entityRequest.pageSize,
    pageNumber: this.entityRequest.pageNumber,
    keyword: this.entityRequest.keyword
  });

  this.service.getListDto7(request).then((response: any) => {
    if (response && response.data) {
      this.listOfData.totalCount = response.data.totalCount;
      this.listOfData.items = response.data.items;
      // console.log('Dữ liệu người dùng:', this.listOfData.items);
      this.processedData = this.listOfData.items!.map(item => ({ ...item }));
      this.loading = false;
      this.isSpinning = false;
    } else {
      this.toastr.error(response?.message || 'Lỗi khi tải dữ liệu!');
    }
  });
}

loadDataRoles() {
    this.roleService
      .getListSelectRole()
      .pipe(finalize(() => {}))
      .subscribe((response) => {
        if (response.status) {
          this.listRoleSelectData = response.data;
        } else {
          this.toastr.error(response.message);
        }
      });
  }

getNameRole2(ids: any[]): string {
  const names = this.listRoleSelectData
    .filter(role => ids.includes(role.value))
    .map(role => role.label);

  return names.join(', ');
}

  // Từ khoá tìm kiếm
  searchKeyword = '';

  // Thêm người dùng
  onAdd(data?: any) {
    const modalRef = this.modal.create({
      nzTitle: '',
      nzContent: CreateUserComponent,
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

  // Sửa người dùng
  onEdit(department: any) {
    console.log('Sửa người dùng: ', department);
    // Logic sửa người dùng
  }

  // Xóa người dùng
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
        title: 'Xóa người dùng',
        content: 'Bạn có chắc chắn muốn xóa người dùng không?',
      },
      nzClassName: 'w-modal-dialog',
    };
    const modalRef = this.modal.create(modalConfig);
    const instanceRef = modalRef.getContentComponent();
    instanceRef.onLoadData.subscribe(response => {
      this.service.delete7(id).then((res: any) => {
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


  // ---------------------
  getNameRole(id: any): string {
    return this.lstRole.find((item) => item.value === id)?.name || '';
  }
  lstRole = [
    {name: 'Admin', value: 1},
    {name: 'HR', value: 2},
    {name: 'Employee', value: 3},
  ]
}
