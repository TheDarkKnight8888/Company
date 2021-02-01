import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeListComponent } from './employee-list.component';

@NgModule({
    declarations: [EmployeeListComponent],
    imports: [CommonModule],
    exports: [EmployeeListComponent],
})
export class EmployeeListModule {}