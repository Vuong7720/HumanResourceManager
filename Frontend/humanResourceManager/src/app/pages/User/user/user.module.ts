import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzInputModule } from 'ng-zorro-antd/input';
import { CreateUserComponent } from '../create-user/create-user.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';  // Để sử dụng ngModel
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzAffixModule } from 'ng-zorro-antd/affix';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzSelectModule } from 'ng-zorro-antd/select';
@NgModule({
  declarations: [UserComponent, CreateUserComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    NzCardModule,
    NzButtonModule,
    NzTableModule,
    NzInputModule,
    FormsModule,
    ReactiveFormsModule,
    NzModalModule,
    NzSpinModule,
    NzFormModule,
    NzAffixModule,
    NzPaginationModule,
    NzSelectModule
  ],
})
export class UserModule {}
