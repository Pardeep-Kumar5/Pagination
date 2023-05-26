import { Component } from '@angular/core';
import { EmployeeService } from '../emplyee/employee.service';
import { Employee } from './employee';
import { Subscriber, filter } from 'rxjs';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent {
EmployeeList:Employee[]=[];
filteredEmployeeList:Employee[]=[];
EditEmployee=new Employee();
NewEmployee=new Employee();
pageNumber = 1;
pageSize: number = 3; 
totalItems=0;
searchTerm = '';
filterValue:any;
filteredEmployees: any;
filterParam:any;
filterBy:any;
sortColumn: any; 
sortDirection: any;
empList:any[]=[];
SortedEmployee:Employee[]=[];
value:any;
constructor(private employeeservice:EmployeeService){}

ngOnInit()
  {
    this.loadData();
  }
  loadData() {
    this.employeeservice.getEmployeesByPage(this.pageNumber, this.pageSize)
      .subscribe(Employee => {
        this.EmployeeList = Employee;
        this.SortedEmployee=Employee;
      });
  }
  onPageChange(page: number) {
    this.pageNumber = page;
    this.loadData();
  }
  onPageSize(page: number) {
    this.pageSize = page;
    this.loadData();
    location.reload();
  }
  
sortEmployees(column: string) {
  debugger
  if (column === this.sortColumn) {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
  } else {
    this.sortColumn = column;
    this.sortDirection = 'asc';
  }
  this.SortedEmployee = this.EmployeeList.slice().sort((a, b) => {
    const valueA = this.getPropertyValue(a, this.sortColumn);
    const valueB = this.getPropertyValue(b, this.sortColumn);

    if (valueA < valueB) {
      return this.sortDirection === 'asc' ? -1 : 1;
    } else if (valueA > valueB) {
      return this.sortDirection === 'asc' ? 1 : -1;
    } else {
      return 0;
    }
  });
}
getPropertyValue(obj: any, property: string) {
  const properties = property.split('.');
  return properties.reduce((o, prop) => o && o[prop], obj);
}
  getFilteredData(filterParam: any, filterBy: any): void {
    debugger;
    this.employeeservice.getEmployeesByPage(filterParam, filterBy)
      .subscribe(employees => this.EmployeeList = employees);
  }
  
Getall()
{
  this.employeeservice.getEmployeesByPage(this.pageNumber, this.pageSize)
      .subscribe(employees => {
        this.filteredEmployeeList = employees;
      });
}

SaveEmployee()
{
  this.employeeservice.SaveEmployee(this.NewEmployee).subscribe(
    (response)=>{
    alert("Data Saved")
    },
    (error)=>{
      alert("Something went worng with api")
    }
  )
}

EditClick(Employee:Employee)
{
  debugger;
  alert(Employee.name)
  this.EditEmployee=Employee;
}
}

