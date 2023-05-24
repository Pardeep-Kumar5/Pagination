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
  SaveEmployee(NewEmployee:Employee):Observable<Employee>
  {
    return this.httpclient.post<Employee>("https://localhost:44371/api/Employee",NewEmployee);
  }

  getFilteredData(filterParam: string, filterBy: string): Observable<Employee[]> {
    debugger;
    const url = this.baseurl;
    let params = new HttpParams();
  
    if (filterParam) {
      params = params.set('filterParam', filterParam);
    }
    params = params.set('filterBy', filterBy);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      params: params
    };
    return this.httpclient.get<Employee[]>(url, httpOptions);
  }
}
