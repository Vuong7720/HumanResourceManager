import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; // Thêm dòng này
import { WelcomeRoutingModule } from './welcome-routing.module';

import { WelcomeComponent } from './welcome.component';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzListModule } from 'ng-zorro-antd/list';

@NgModule({
  imports: [
    CommonModule,
    WelcomeRoutingModule, 
    NzIconModule,
    NzListModule
  ],
  declarations: [WelcomeComponent],
  exports: [WelcomeComponent]
})
export class WelcomeModule { }
