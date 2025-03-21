import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; // Thêm dòng này

import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzListModule } from 'ng-zorro-antd/list';
import { HumanResourceComponent } from './humanResource.component';
import { HumanResourceRoutingModule } from './humanResource-routing.module';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';

@NgModule({
  imports: [
    CommonModule,
    HumanResourceRoutingModule, 
    NzIconModule,
    NzListModule,
    NzTableModule,
    NzBreadCrumbModule
  ],
  declarations: [HumanResourceComponent],
  exports: [HumanResourceComponent]
})
export class HumanResourceModule { }
