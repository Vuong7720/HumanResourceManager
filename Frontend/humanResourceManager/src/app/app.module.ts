import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { vi_VN } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import vi from '@angular/common/locales/vi';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IconsProviderModule } from './icons-provider.module';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { MainLayoutComponent } from './pages/layouts/main-layout/main-layout.component';
import { BlankLayoutComponent } from './layouts/blank-layout/blank-layout.component'; // Cần nếu dùng icon
import { Client } from './api2/api.client';
import { environment } from 'src/env/environment';
import { ToastrModule } from 'ngx-toastr';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { DeleteComfirmComponent } from './shared/delete-confirm/delete-confirm.component';
import { NzResultModule } from 'ng-zorro-antd/result';

registerLocaleData(vi);

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    BlankLayoutComponent,
    DeleteComfirmComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    IconsProviderModule,
    NzLayoutModule,
    NzMenuModule,
    NzBreadCrumbModule,
    NzDropDownModule,
    NzIconModule,
    NzSpinModule,
    ReactiveFormsModule,
    NzResultModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
  ],
  providers: [
  { provide: NZ_I18N, useValue: vi_VN },
  {
    provide: Client,
    useFactory: () => new Client(environment.apiBaseUrl)
  }
],
  bootstrap: [AppComponent]
})
export class AppModule { }
