import { Component, EventEmitter, Input, Output } from '@angular/core'
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';

@Component({
    selector: 'app-employee-info',
    templateUrl: './employee-info.component.html'
})
export class EmployeeInfoComponent {
    @Input()
    employee$ : Observable<Employee>

    @Output()
    closeClick = new EventEmitter<boolean>();

    OnCloseClick(clicked:boolean):void {
        this.closeClick.emit(clicked);
    }
}