import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DepartmentsComponent } from './departments.component';
import { DepartmentMasterListComponent } from './lists/department-master-list.component';
import { DepartmentFormComponent } from './forms/department-form.component';

const routes: Routes = [
    {path: '', component: DepartmentsComponent, children: [
        {path: '', component: DepartmentMasterListComponent},
        {path: 'create', component: DepartmentFormComponent},
        {path: 'update/:id', component: DepartmentFormComponent},
    ] }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DepartmentsRoutingModule {}


