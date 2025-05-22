import { Component } from '@angular/core';
import { ChartOptions, ChartType } from 'chart.js';
import { Client } from 'src/app/api2/api.client';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
totalEmployees = 0;
  totalDepartments = 0;
  totalContracts = 0;
  totalPositions = 0;
  totalAttendance = 0;
  totalPayrolls = 0;

  // Dữ liệu biểu đồ
  chartLabels = ['Nhân viên', 'Phòng ban', 'Hợp đồng', 'Chức vụ'];
  chartData = [0, 0, 0, 0, 0, 0];
  chartOptions: ChartOptions = {
    responsive: true,
    plugins: {
      legend: { position: 'top' },
      title: { display: true, text: 'Thống kê tổng quan' }
    }
  };
  chartType: ChartType = 'bar';

  constructor(private service: Client) {}

  ngOnInit(): void {
  this.service.overview().then((res: any)=>{
    if (res) {
      this.totalEmployees = res.totalEmployees;
      this.totalDepartments = res.totalDepartments;
      this.totalContracts = res.totalContracts;
      this.totalPositions = res.totalPositions;
      this.totalAttendance = res.totalAttendance;
      this.totalPayrolls = res.totalPayrolls;

      this.chartData = [
        this.totalEmployees,
        this.totalDepartments,
        this.totalContracts,
        this.totalPositions
      ];
    }
  });
}

}
