import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/Dashboard' },
  { path: 'Dashboard', loadChildren: () => import('./home/home.module').then(m => m.HomeModule) },
  { path: 'human-resource', loadChildren: () => import('./pages/human-resource/humanResource.module').then(m => m.HumanResourceModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
