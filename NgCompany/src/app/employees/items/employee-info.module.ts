import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeInfoComponent } from './employee-info.component';

@NgModule({
    declarations: [EmployeeInfoComponent],
    imports: [CommonModule],
    exports: [EmployeeInfoComponent],
})
export class EmployeeInfoModule {}