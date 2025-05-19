import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/api2/api.client';



@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.scss']
})
export class DeleteComfirmComponent implements OnInit {
 onLoadData: EventEmitter<any> = new EventEmitter();
  form!: FormGroup;
  @Output() success = new EventEmitter();
  getParams: CategoryParams = inject(NZ_MODAL_DATA);
  title!: string;
  content!: string;

  constructor(private nzModalRef: NzModalRef) {}
  ngOnInit(): void {
    this.title = this.getParams.title;
    this.content = this.getParams.content;
  }

  save() {
    this.onLoadData.emit(true);
    this.nzModalRef.destroy();
  }
  onBack() {
    this.nzModalRef.destroy();
  }
}

export interface CategoryParams {
  title: string;
  content: string;
}
