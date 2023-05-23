import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {HttpClient, HttpParams}from'@angular/common/http';
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

  // getFilteredData(filterParam: string, filterBy: string): Observable<Employee[]> {
  //   debugger
  //   const url = `${this.baseurl}Employee/filterParam`;

  //   const params = {
  //     filterParam: filterParam,
  //     filterBy: filterBy
  //   };

  //   return this.httpclient.get<Employee[]>(url, { params });
  // }

  getFilteredData(filterParam: string, filterBy: string): Observable<Employee[]> {
    debugger;
    const url = `${this.baseurl}employees/filter`;
  
    let params = {};
  
    if (filterParam) {
      params = {
        filterParam: filterParam,
        filterBy: filterBy
      };
    } else {
      params = {
        filterBy: filterBy
      };
    }
  
    return this.httpclient.get<Employee[]>(url, { params });
  }
  
  SaveEmployee(NewEmployee:Employee):Observable<Employee>
  {
    return this.httpclient.post<Employee>("https://localhost:44371/api/Employee",NewEmployee);
  }
}
