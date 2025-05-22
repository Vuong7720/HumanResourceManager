import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Client, CreateUpdateAttendanceDto } from 'src/app/api2/api.client';



interface Employee {
  id: number;
  name: string;
}

interface AttendanceRecord {
  employee: Employee;
  time: Date;
  type: string;
}


@Component({
  selector: 'app-check',
  templateUrl: './check.component.html',
  styleUrls: ['./check.component.scss']
})
export class CheckComponent implements OnInit {
  currentTime: Date = new Date();
  employees: any[] = [];
  selectedEmployee: any | null = null;
  records: any[] = [];

  constructor(
      private service: Client,
      private toastr: ToastrService
    ) {}


  ngOnInit(): void {
  this.getEmployee();
    setInterval(() => {
      this.currentTime = new Date();
    }, 1000);
  }

  getEmployee(){
    this.service.getListSelect2().then((res: any) =>{
      this.employees = res.data;
    })
  }

  checkIn() {
    if (this.selectedEmployee) {
      const attendance = new CreateUpdateAttendanceDto({
        employeeID:this.selectedEmployee.value,
      })
       this.service.create(attendance).then((res: any) =>{
        if(res.status){
          this.records.push({
          employee: this.selectedEmployee,
          time: new Date(),
          type: 'Vào',
        });
        this.toastr.success(res.message);
        }else{
          this.toastr.error(res.message);
        }
       })
    }
  }

  checkOut() {
    if (this.selectedEmployee) {
      
      this.service.checkOut(this.selectedEmployee.value).then((res: any) =>{
        if(res.status){
          this.toastr.success(res.message);
          this.records.push({
          employee: this.selectedEmployee,
          time: new Date(),
          type: 'Ra',
        });
        }else{
           this.toastr.error(res.message);
        }
      })
    }
  }
}
