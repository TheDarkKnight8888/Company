import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { DepartmentInfoComponent } from "./department-info.component";


@NgModule({
    declarations: [DepartmentInfoComponent],
    imports: [CommonModule],
    exports: [DepartmentInfoComponent]
})
export class DepartmentInfoModule {}