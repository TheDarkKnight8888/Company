import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesRoutingModule } from './employees-routing.module';
import { EmployeesComponent } from './employees.component';
import { EmployeeMasterListModule } from './lists/employee-master-list.module';
import { EmployeeFormModule } from './forms/employee-form.module';

@NgModule({
    declarations: [EmployeesComponent],
    imports: [CommonModule, EmployeesRoutingModule, EmployeeMasterListModule, EmployeeFormModule]
})
export class EmployeesModule {}