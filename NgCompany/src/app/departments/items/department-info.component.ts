import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from '../models/department';

@Component({
    selector: 'app-department-info',
    templateUrl: './department-info.component.html'
})
export class DepartmentInfoComponent{
    @Input()
    department$ : Observable<Department>

    @Output()
    closeClick = new EventEmitter<boolean>();

    OnCloseClick(clicked:boolean):void {
        this.closeClick.emit(clicked);
    }
}