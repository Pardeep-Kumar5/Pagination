import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EmployeeComponent } from './employee/employee.component';

const routes: Routes = [
  {path:"",component:EmployeeComponent},
  {path:"home",component:HomeComponent},
  {path:"employee",component:EmployeeComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
