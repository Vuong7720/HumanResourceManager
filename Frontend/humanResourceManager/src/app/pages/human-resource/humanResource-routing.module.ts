import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HumanResourceComponent } from './humanResource.component';
import { MainLayoutComponent } from '../layouts/main-layout/main-layout.component';


const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', component: HumanResourceComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HumanResourceRoutingModule { }
