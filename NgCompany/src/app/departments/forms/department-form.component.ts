import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Department } from '../models/department';
import { EmployeeListItem } from '../../employees/models/employee-list-item';
import { Observable, of } from 'rxjs';
import { DepartmentService } from '../services/department.service';
import { map } from 'rxjs/operators';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'app-department-form',
    templateUrl: './department-form.component.html'
})
export class DepartmentFormComponent implements OnInit {
    private readonly rootUrl = '/departments';
    private departmentId: number;
    department = new Department(0, '', null, null);
    isUpdateMode = false;
    isSubmited = false;

    freeStaff$: Observable<EmployeeListItem[]>;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private departmentService: DepartmentService
    ) {}

    ngOnInit(): void {
        this.freeStaff$ = of([]);
        this.route.params.subscribe(p => {
            if(p['id']===undefined) return;
            this.departmentId = Number(p['id']);
            this.departmentService.get(Number(p['id'])).subscribe(
                {
                    next: (item: Department) => {
                        this.department = item;
                        this.freeStaff$ = this.departmentService.getFreeEmployees();
                        this.busyEmployees$ = this.departmentService.getDepartmentEmployees(this.departmentId)
                        this.isUpdateMode = true;
                    }
                })
        });
    }

    onSubmit(form: NgForm): void {
        if(form.invalid) return;

        this.isSubmited = true;
        if (this.isUpdateMode){
            this.departmentService.update(this.departmentId, this.department).subscribe({
                next: (response: Department) => { this.router.navigateByUrl(this.rootUrl+'/update/'+response.id) }
            });
        }
        else{
            this.departmentService.create(this.department).subscribe({
                next: (response) => { this.router.navigateByUrl(this.rootUrl+'/update/'+response.id) }
            });
        }
    }

    onDelete():void{
        this.departmentService.delete(this.departmentId).subscribe({
            next: (response) => this.router.navigateByUrl(this.rootUrl)
        });
    }

    onCancel(): void {
        this.router.navigateByUrl(this.rootUrl);
    }

    
    busyEmployees$ : Observable<EmployeeListItem[]>
    

    onClose(): void {
        this.router.navigateByUrl(this.rootUrl);
    }

    onBusyEmployeeClick(employee: EmployeeListItem) :void {
        this.departmentService.unassignEmployFromDepartment(employee.id).subscribe({
            next:(response) => {
                this.freeStaff$ = this.departmentService.getFreeEmployees();
                this.busyEmployees$ = this.departmentService.getDepartmentEmployees(this.departmentId);
            }
        })
    }

    onFreeEmployeeClick(employee: EmployeeListItem) : void {
        this.departmentService.assignEmployToDepartment(this.departmentId, employee.id).subscribe({
            next: (employeeId: number) => {
                console.log(employeeId);
                this.freeStaff$ = this.departmentService.getFreeEmployees();
                this.busyEmployees$ = this.departmentService.getDepartmentEmployees(this.departmentId);
            }
        })
    }
}