import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; // Thêm dòng này
import { HomeRoutingModule } from './home-routing.module';

import { HomeComponent } from './home.component';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzGridModule } from 'ng-zorro-antd/grid';

import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';


@NgModule({
  imports: [
    CommonModule,
    HomeRoutingModule, 
    NzIconModule,
    NzListModule,
    NzGridModule,
    NzBreadCrumbModule
  ],
  declarations: [HomeComponent],
  exports: [HomeComponent]
})
export class HomeModule { }
