import { Component } from '@angular/core'
import { Observable, of } from 'rxjs';
import { Employee } from '../models/employee';
import { EmployeeListItem } from '../models/employee-list-item';
import { EmployeeService } from '../services/employee.service';

@Component({
    selector: 'app-employee-master-list',
    templateUrl: './employee-master-list.component.html',
    styleUrls: ['../../departments/departments.component.css']
})
export class EmployeeMasterListComponent {
    employees$: Observable<EmployeeListItem[]>;

    isSelectedMode = false;

    selectedEmployee$ : Observable<Employee>

    constructor(private employeeService: EmployeeService) {}


    ngOnInit(): void {
        this.employees$ = this.employeeService.getAll();
    }

    OnEmployeeClick(department: EmployeeListItem): void{
        this.selectedEmployee$ = this.employeeService.get(department.id);
        this.isSelectedMode = true;
    }

    OnCloseInfoClick(closed: boolean) :void {
        this.isSelectedMode = false;
    }
}