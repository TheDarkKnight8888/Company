import { Component, EventEmitter, Input, Output } from '@angular/core'
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { EmployeeListItem } from '../models/employee-list-item';

@Component({
    selector: 'app-employee-list',
    templateUrl: './employee-list.component.html',
    styleUrls: ['../../departments/departments.component.css']
})
export class EmployeeListComponent {
    @Input()
    list$: Observable<EmployeeListItem[]>;

    @Input()
    isUpdated: boolean;

    @Output()
    itemClick = new EventEmitter<EmployeeListItem>();

    constructor(private router: Router) {}

    onItemClick(item: EmployeeListItem): void {
        this.itemClick.emit(item);
    }

    onUpdateClick(item: EmployeeListItem) :void {
        if (this.isUpdated){
            this.router.navigateByUrl('/employees/update/'+item.id);
        }
    }
}