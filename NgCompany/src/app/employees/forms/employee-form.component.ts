import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/employee';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'app-employee-form',
    templateUrl: './employee-form.component.html'
})
export class EmployeeFormComponent implements OnInit {
    private readonly rootUrl = '/employees'
    private employeeId: number;
    employee = new Employee(0, '','','','', '','',null, null);
    isUpdateMode = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private employeeService: EmployeeService
    ) {}

    ngOnInit(): void {
        this.route.params.subscribe(p => {
            if(p['id']===undefined) return;
            this.employeeId = Number(p['id']);
            this.employeeService.get(this.employeeId).subscribe(
                {
                    next: (item: Employee) => {
                        this.employee = item;
                        this.isUpdateMode = true;
                    }
                })
        });
    }

    onSubmit(form: NgForm): void {
        if(form.invalid) return;
        if (this.isUpdateMode){
            this.employeeService.update(this.employeeId, this.employee).subscribe({
                next: (response) => { this.router.navigateByUrl(this.rootUrl); }
            });
        }
        else{
            this.employeeService.create(this.employee).subscribe({
                next: (response) => { this.router.navigateByUrl(this.rootUrl); }
            });
        }
    }

    onDelete():void{
        this.employeeService.delete(this.employeeId).subscribe({
            next: (response) => this.router.navigateByUrl(this.rootUrl)
        });
    }

    onCancel(): void {
        this.router.navigateByUrl(this.rootUrl);
    }
}