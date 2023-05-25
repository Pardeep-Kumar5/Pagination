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
  SortedEmployee:Employee[]=[];
EmployeeList:Employee[]=[];
pageNumber = 1;
pageSize = 3;
totalItems = 0 ;
filterParam: any;
  filterBy: any;
  sortColumn: any; 
  sortDirection: any;
EditEmployee:Employee=new Employee();

NewEmployee:Employee=new Employee();

constructor(private employeeservice:EmployeeService){}

ngOnInit()
{
  this.GetAll();

}
GetAll()
{
  this.employeeservice.getEmployeesByPage(this.pageNumber, this.pageSize)
      .subscribe(employees => {
        this.EmployeeList = employees;
      });
}
getFilteredData(filterParam: string, filterBy: string): void {
  debugger;
  this.employeeservice.getFilteredData(filterParam, filterBy)
    .subscribe(employees => this.EmployeeList = employees);
}

sortEmployees(column: string) {
  debugger;
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
  // Retrieve the value of the specified property from the object
  const properties = property.split('.');
  return properties.reduce((o, prop) => o && o[prop], obj);
}



setPage(pageNumber: number) {
  this.pageNumber = pageNumber;
  this.GetAll();
}
previousPage() {
  if (this.pageNumber > 1) {
    this.pageNumber--;
    this.GetAll();
  }
}
nextPage() {
  const totalPages = this.getTotalPages().length;
  if (this.pageNumber < totalPages) {
    this.pageNumber++;
    this.GetAll();
  }
}

getTotalPages(): number[] {
  return Array(Math.ceil(this.totalItems / this.pageSize)).fill(0).map((x, i) => i + 1);
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

