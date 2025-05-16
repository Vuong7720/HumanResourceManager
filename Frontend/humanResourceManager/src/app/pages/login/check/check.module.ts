import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CheckComponent } from './check.component';
import { CheckRoutingModule } from './check-routing.module';

@NgModule({
  declarations: [
    CheckComponent
  ],
  imports: [
    CommonModule,
    CheckRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class CheckModule { }
