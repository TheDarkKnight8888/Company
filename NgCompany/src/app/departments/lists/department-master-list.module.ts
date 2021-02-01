import { NgModule } from '@angular/core';
import { CommonModule  } from '@angular/common';
import { DepartmentMasterListComponent } from './department-master-list.component';
import { DepartmentInfoModule } from '../items/department-info.module';
import { DepartmentListModule } from './department-list.module';

@NgModule({
    declarations: [DepartmentMasterListComponent],
    imports: [CommonModule, DepartmentListModule, DepartmentInfoModule],
    exports: [DepartmentMasterListComponent]
})
export class DepartmentMasterListModule {}