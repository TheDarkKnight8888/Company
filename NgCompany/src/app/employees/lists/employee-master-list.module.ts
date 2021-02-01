import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeMasterListComponent } from './employee-master-list.component';
import { EmployeeInfoModule } from '../items/employee-info.module';
import { EmployeeListModule } from './employee-list.module';

@NgModule({
    declarations: [EmployeeMasterListComponent],
    imports: [CommonModule, EmployeeListModule, EmployeeInfoModule],
    exports: [EmployeeMasterListComponent],
})
export class EmployeeMasterListModule {}