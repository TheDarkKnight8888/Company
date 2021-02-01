import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DepartmentsModule } from './departments/departments.module'
import { EmployeesModule } from './employees/employees.module';
import { DepartmentServiceModule } from './departments/services/department-service.module';
import { EmployeeServiceModule } from './employees/services/employee-service.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    DepartmentsModule,
    EmployeesModule,
    DepartmentServiceModule,
    EmployeeServiceModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
