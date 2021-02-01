import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Department  } from '../models/department';
import { DepartmentListItem } from '../models/department-list-item';
import { EmployeeListItem } from '../../employees/models/employee-list-item';

@Injectable()
export class DepartmentService {

    private readonly url = environment.apiUpl + 'api/departments'

    constructor(private http: HttpClient) {}

    getAll(): Observable<DepartmentListItem[]> {
        return this.http.get<DepartmentListItem[]>(this.url);
    }

    get(id: number) : Observable<Department>{
        return this.http.get<Department>(`${this.url}/${id}`);
    }

    create(item: Department): Observable<Department>{
        return this.http.post<Department>(this.url, item);
    }

    update(id: number, item:Department): Observable<Department>{
        return this.http.put<Department>(`${this.url}/${id}`, item);
    }

    delete(id:number): Observable<Department>{
        return this.http.delete<Department>(`${this.url}/${id}`);
    }

    assignEmployToDepartment(departmentId:number, employeeId: number): Observable<number>{
        return this.http.get<number>(`${this.url}/${departmentId}/assignemployee/${employeeId}`);

    }

    unassignEmployFromDepartment(employeeId: number): Observable<number>{
        return this.http.get<number>(`${this.url}/unassignemployee/${employeeId}`);

    }

    getDepartmentEmployees(departmentId:number): Observable<EmployeeListItem[]>{
        return this.http.get<EmployeeListItem[]>(`${this.url}/${departmentId}/employees`);
    }

    getFreeEmployees(): Observable<EmployeeListItem[]>{
        return this.http.get<EmployeeListItem[]>(`${this.url}/freestaff`);
    }
}