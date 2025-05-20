import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PositionRoutingModule } from './position-routing.module';
import { PositionComponent } from './position.component';


import { NzCardModule } from 'ng-zorro-antd/card';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormsModule } from '@angular/forms';  // Để sử dụng ngModel
import { CreatePositionComponent } from '../create-position/create-position.component';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzSpinModule } from 'ng-zorro-antd/spin';


@NgModule({
  declarations: [
    PositionComponent,CreatePositionComponent
  ],
  imports: [
    CommonModule,
    PositionRoutingModule,
    NzCardModule,
    NzButtonModule,
    NzTableModule,
    NzInputModule,
    FormsModule,
    NzModalModule,
    NzSpinModule,
  ]
})
export class PositionModule { }
