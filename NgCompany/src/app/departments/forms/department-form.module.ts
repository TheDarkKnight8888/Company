import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { DepartmentFormComponent } from "./department-form.component";
import { EmployeeListModule } from '../../employees/lists/employee-list.module';

@NgModule({
    declarations: [DepartmentFormComponent],
    imports: [CommonModule, FormsModule, EmployeeListModule ],
    exports: [DepartmentFormComponent]
})
export class DepartmentFormModule {}