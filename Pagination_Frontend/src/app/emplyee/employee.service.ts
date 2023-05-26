import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {HttpClient, HttpHeaders, HttpParams}from'@angular/common/http';
import { Employee } from '../employee/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = 'https://localhost:44371/api/Employee?pageNumber/&pageSize';
  private baseurl='https://localhost:44371/api/Employee/FilterData?FilterData/&filterBy/';
  constructor(private httpclient:HttpClient) { }
 


  getEmployeesByPage(pageNumber: number, pageSize: number): Observable<Employee[]> {
    
    let params = new HttpParams();
      params = params.append('pageNumber', pageNumber.toString());
       params = params.append('pageSize', pageSize.toString());
  
       return this.httpclient.get<Employee[]>(`${this.apiUrl}/getemployeesbypage`, { params });
  }
  getFilteredEmployees(nameFilter: string, addressFilter: string, salaryFilter: number) {
    debugger
    const url = `https://localhost:44371/api/Employee/GetEmployeesithFilter?nameFilter=${nameFilter}&addressFilter=${addressFilter}&salaryFilter=${salaryFilter}`;
    return this.httpclient.get<Employee[]>(url);
  }
  SaveEmployee(NewEmployee:Employee):Observable<Employee>
  {
    return this.httpclient.post<Employee>("https://localhost:44371/api/Employee",NewEmployee);
  }

}