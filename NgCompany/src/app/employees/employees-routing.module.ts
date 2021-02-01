import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeesComponent } from './employees.component';
import { EmployeeFormComponent } from './forms/employee-form.component';
import { EmployeeMasterListComponent } from './lists/employee-master-list.component';

const routes: Routes = [
    {path: '', component: EmployeesComponent, children: [
        {path: '', component: EmployeeMasterListComponent},
        {path: 'create', component: EmployeeFormComponent},
        {path: 'update/:id', component: EmployeeFormComponent},
    ] }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class EmployeesRoutingModule {}