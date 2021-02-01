import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {path: 'departments', loadChildren: () => import('./departments/departments-routing.module').then(i=>i.DepartmentsRoutingModule)},
  {path: 'employees', loadChildren: () => import('./employees/employees-routing.module').then(i=>i.EmployeesRoutingModule)},
  {path: '', loadChildren: () => import('./pages/dummy-routing.module').then(i=>i.DummyRoutingModule)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
