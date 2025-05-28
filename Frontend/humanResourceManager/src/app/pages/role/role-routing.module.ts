import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainLayoutComponent } from '../layouts/main-layout/main-layout.component';
import { RoleComponent } from './role.component';

const routes: Routes = [{
  path: '',
  component: MainLayoutComponent,
  children: [
    { path: '', component: RoleComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RoleRoutingModule { }
