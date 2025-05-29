import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagingRequest } from '../api.client';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private baseUrl = 'https://localhost:7259/api/employee';

  constructor(private http: HttpClient) {}

  exportExcel(input: PagingRequest): Observable<Blob> {
    return this.http.post(`${this.baseUrl}/export-excel`, input, {
      responseType: 'blob', // Quan trọng: để nhận dạng file nhị phân
    });
  }
}
