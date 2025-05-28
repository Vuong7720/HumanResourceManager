import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './AuthService/auth.guard';
import { DenyAccessComponent } from './pages/Deny-access/deny-access.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/dashboard' },

  { path: 'login', loadChildren: () => import('./pages/login/login/login.module').then(m => m.LoginModule) },

  {
    path: 'dashboard',
    loadChildren: () => import('./pages/Dashboard/dashboard/dashboard.module').then(m => m.DashboardModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'human-resource',
    loadChildren: () => import('./pages/human-resource/humanResource.module').then(m => m.HumanResourceModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'attendance',
    loadChildren: () => import('./pages/Attendance/attendance/attendance.module').then(m => m.AttendanceModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'contracts',
    loadChildren: () => import('./pages/Contract/contract/contract.module').then(m => m.ContractModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'departments',
    loadChildren: () => import('./pages/Department/department/department.module').then(m => m.DepartmentModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'employees',
    loadChildren: () => import('./pages/Employee/employee/employee.module').then(m => m.EmployeeModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'payroll',
    loadChildren: () => import('./pages/Payroll/payroll/payroll.module').then(m => m.PayrollModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'positions',
    loadChildren: () => import('./pages/Position/position/position.module').then(m => m.PositionModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'users',
    loadChildren: () => import('./pages/User/user/user.module').then(m => m.UserModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'role',
    loadChildren: () => import('./pages/role/role.module').then(m => m.RoleModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'check',
    loadChildren: () => import('./pages/login/check/check.module').then(m => m.CheckModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'access-deny',
    component: DenyAccessComponent,
    title: 'Truy cập bị từ chối - Hệ thống quản lý nhân sự',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
