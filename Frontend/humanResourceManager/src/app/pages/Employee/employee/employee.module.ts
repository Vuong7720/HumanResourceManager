import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeeComponent } from './employee.component';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormsModule } from '@angular/forms';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';

import { NzButtonModule } from 'ng-zorro-antd/button';
import { CreateeployeeComponent } from '../create-createemployee/create-createemployee.component';      // 👈 cho các nút thao tác
import { ReactiveFormsModule } from '@angular/forms';
import { NzModalModule } from 'ng-zorro-antd/modal';

import { NzSpinModule } from 'ng-zorro-antd/spin';

import { NzFormModule } from 'ng-zorro-antd/form';


@NgModule({
  declarations: [
    EmployeeComponent,
    CreateeployeeComponent
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
    NzTableModule,
    NzCardModule,
    NzInputModule,
    FormsModule,
    NzPaginationModule,
    NzButtonModule,
    NzToolTipModule,
    NzCheckboxModule,
    ReactiveFormsModule,
    NzModalModule,
    NzSpinModule,
NzFormModule
  ]
})
export class EmployeeModule { }
