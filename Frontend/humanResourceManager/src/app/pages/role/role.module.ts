import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoleRoutingModule } from './role-routing.module';
import { RoleComponent } from './role.component';

import { NzCardModule } from 'ng-zorro-antd/card';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';  // Để sử dụng ngModel
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzFormModule } from 'ng-zorro-antd/form';
import { RoleFormComponent } from './role-form/role-form.component';


@NgModule({
  declarations: [RoleComponent,RoleFormComponent],
  imports: [
    CommonModule,
    RoleRoutingModule,
    NzFormModule,
    NzCardModule,
    NzButtonModule,
    NzTableModule,
    NzInputModule,
    FormsModule,
    NzModalModule,
    NzSpinModule,
    ReactiveFormsModule
  ]
})
export class RoleModule { }
