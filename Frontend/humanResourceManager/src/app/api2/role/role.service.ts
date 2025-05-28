import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagingRequest } from '../api.client';
import { CreateUpdateRoleDto, ResponseResult } from './models';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private baseUrl = 'https://localhost:7259/api/role';

  constructor(private http: HttpClient) {}

  getListDto(input: PagingRequest): Observable<ResponseResult> {
    return this.http.post<ResponseResult>(`${this.baseUrl}/get-list-dto`, input);
  }

  getById(id: number): Observable<ResponseResult> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.get<ResponseResult>(`${this.baseUrl}/get-by-id`, { params });
  }

  create(input: CreateUpdateRoleDto): Observable<ResponseResult> {
    return this.http.post<ResponseResult>(`${this.baseUrl}/create`, input);
  }

  update(id: number, model: CreateUpdateRoleDto): Observable<ResponseResult> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.put<ResponseResult>(`${this.baseUrl}/update`, model, { params });
  }

  delete(id: number): Observable<ResponseResult> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.delete<ResponseResult>(`${this.baseUrl}/delete`, { params });
  }

  deleteByIds(ids: number[]): Observable<ResponseResult> {
    return this.http.delete<ResponseResult>(`${this.baseUrl}/delete-by-ids`, {
      body: ids,
    });
  }

  getListSelectRole(): Observable<ResponseResult> {
    return this.http.get<ResponseResult>(`${this.baseUrl}/get-list-select-role`);
  }

  getListSelectPermission(): Observable<ResponseResult> {
    return this.http.get<ResponseResult>(`${this.baseUrl}/get-list-select-permission`);
  }
}
