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
pageNumber = 1;
pageSize = 3;
totalItems = 0 ;
filterParam: any;
  filterBy: any;
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
onFilter(): void {
  debugger;
  this.employeeservice.getFilteredData(this.filterParam, this.filterBy)
    .subscribe(employees => {
      this.EmployeeList = employees;
    });
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

