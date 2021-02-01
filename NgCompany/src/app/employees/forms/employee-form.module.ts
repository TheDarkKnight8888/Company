import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { EmployeeFormComponent } from './employee-form.component';
import {FormsModule} from '@angular/forms'



@NgModule({
    declarations: [EmployeeFormComponent],
    imports: [CommonModule, FormsModule],
    exports: [EmployeeFormComponent]
})
export class EmployeeFormModule {}