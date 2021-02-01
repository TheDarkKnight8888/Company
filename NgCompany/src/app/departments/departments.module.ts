import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentsRoutingModule } from './departments-routing.module';
import { DepartmentsComponent } from './departments.component';
import { DepartmentFormModule } from './forms/department-form.module';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { DepartmentMasterListModule } from './lists/department-master-list.module';

@NgModule({
    declarations: [DepartmentsComponent],
    imports: [
        CommonModule, 
        DepartmentsRoutingModule,
        DepartmentMasterListModule,
        DepartmentFormModule,
        RouterModule,
        HttpClientModule
    ]
})
export class DepartmentsModule {}