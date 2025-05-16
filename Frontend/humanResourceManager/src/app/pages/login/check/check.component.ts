import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';



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
  employees: Employee[] = [
    { id: 1, name: 'Nguyễn Văn A' },
    { id: 2, name: 'Trần Thị B' },
    { id: 3, name: 'Phạm Văn C' },
    { id: 4, name: 'Lê Thị D' },
  ];
  selectedEmployee: Employee | null = null;
  records: AttendanceRecord[] = [];

  ngOnInit(): void {
    setInterval(() => {
      this.currentTime = new Date();
    }, 1000);
  }

  checkIn() {
    if (this.selectedEmployee) {
      this.records.push({
        employee: this.selectedEmployee,
        time: new Date(),
        type: 'Vào',
      });
    }
  }

  checkOut() {
    if (this.selectedEmployee) {
      this.records.push({
        employee: this.selectedEmployee,
        time: new Date(),
        type: 'Ra',
      });
    }
  }
}
