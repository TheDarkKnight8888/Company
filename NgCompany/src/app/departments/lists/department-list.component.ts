import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DepartmentListItem } from '../models/department-list-item';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';


@Component({
    selector: 'app-department-list',
    templateUrl: './department-list.component.html',
    styleUrls: ['../departments.component.css']
})
export class DepartmentListComponent {

    @Input()
    list$: Observable<DepartmentListItem[]>;

    @Input()
    isUpdated: boolean;

    @Output()
    itemClick = new EventEmitter<DepartmentListItem>();

    constructor(private router: Router) {}

    onItemClick(item: DepartmentListItem): void {
        this.itemClick.emit(item);
    }

    onUpdateClick(item: DepartmentListItem) :void {
        if (this.isUpdated){
            this.router.navigateByUrl('/departments/update/'+item.id);
        }
    }
}