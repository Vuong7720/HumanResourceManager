import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/dashboard' }, // 👈 redirect đúng path
  { path: 'dashboard', loadChildren: () => import('./pages/Dashboard/dashboard/dashboard.module').then(m => m.DashboardModule) },
  { path: 'login', loadChildren: () => import('./pages/login/login/login.module').then(m => m.LoginModule) },
  { path: 'human-resource', loadChildren: () => import('./pages/human-resource/humanResource.module').then(m => m.HumanResourceModule) },
  { path: 'dashboards', loadChildren: () => import('./pages/Dashboard/dashboard/dashboard.module').then(m => m.DashboardModule) },
  { path: 'attendance', loadChildren: () => import('./pages/Attendance/attendance/attendance.module').then(m => m.AttendanceModule) },
  { path: 'contracts', loadChildren: () => import('./pages/Contract/contract/contract.module').then(m => m.ContractModule) },
  { path: 'departments', loadChildren: () => import('./pages/Department/department/department.module').then(m => m.DepartmentModule) },
  { path: 'employees', loadChildren: () => import('./pages/Employee/employee/employee.module').then(m => m.EmployeeModule) },
  { path: 'payroll', loadChildren: () => import('./pages/Payroll/payroll/payroll.module').then(m => m.PayrollModule) },
  { path: 'positions', loadChildren: () => import('./pages/Position/position/position.module').then(m => m.PositionModule) },
  { path: 'users', loadChildren: () => import('./pages/User/user/user.module').then(m => m.UserModule) },


];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
