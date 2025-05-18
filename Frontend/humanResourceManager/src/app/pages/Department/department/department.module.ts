import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DepartmentRoutingModule } from './department-routing.module';
import { DepartmentComponent } from './department.component';


import { NzCardModule } from 'ng-zorro-antd/card';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';  // Để sử dụng ngModel
import { CreateDepartmentComponent } from './create-department/create-department.component';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzSpinModule } from 'ng-zorro-antd/spin';

import { NzFormModule } from 'ng-zorro-antd/form';

@NgModule({
  declarations: [
    DepartmentComponent,CreateDepartmentComponent
  ],
  imports: [
    CommonModule,
    DepartmentRoutingModule,
    NzCardModule,
    NzButtonModule,
    NzTableModule,
    NzInputModule,
    FormsModule,
    NzModalModule,
    NzSpinModule,
    ReactiveFormsModule,
    NzFormModule,
  ]
})
export class DepartmentModule { }
