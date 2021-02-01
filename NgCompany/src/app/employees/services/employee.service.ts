import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Employee } from '../models/employee';
import { EmployeeListItem } from '../models/employee-list-item';

@Injectable()
export class EmployeeService {

    private readonly url = environment.apiUpl + 'api/employees'

    constructor(private http: HttpClient) {}

    getAll(): Observable<EmployeeListItem[]> {
        return this.http.get<EmployeeListItem[]>(this.url);
    }

    get(id: number) : Observable<Employee>{
        return this.http.get<Employee>(`${this.url}/${id}`);
    }

    create(item: Employee): Observable<Employee>{
        return this.http.post<Employee>(this.url, item);
    }

    update(id: number, item:Employee): Observable<Employee>{
        return this.http.put<Employee>(`${this.url}/${id}`, item);
    }

    delete(id:number): Observable<Employee>{
        return this.http.delete<Employee>(`${this.url}/${id}`);
    }
}