import { Component} from '@angular/core';
import { DepartmentListItem } from '../models/department-list-item';
import { Observable, of } from 'rxjs';
import { Department } from '../models/department';
import { DepartmentService } from '../services/department.service';

@Component({
    selector: 'app-department-master-list',
    templateUrl: './department-master-list.component.html',
    styleUrls: ['../departments.component.css']
})
export class DepartmentMasterListComponent {

    departments$: Observable<DepartmentListItem[]>;

    isSelectedMode = false;

    selectedDepartment$ : Observable<Department>

    constructor(private departmentService: DepartmentService) {}


    ngOnInit(): void {
        this.departments$ = this.departmentService.getAll()
    }

    OnDepartmentClick(department: DepartmentListItem): void{
        this.selectedDepartment$ = this.departmentService.get(department.id);
        this.isSelectedMode = true;
    }

    OnCloseInfoClick(closed: boolean) :void {
        this.isSelectedMode = false;
    }
}