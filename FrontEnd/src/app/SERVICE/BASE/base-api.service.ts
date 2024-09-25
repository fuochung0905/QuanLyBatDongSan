import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError, timeout } from 'rxjs';
import { ResponseData } from '../../MODEL/BASE/response-data';
import { LoginRequest } from '../../REQUEST/HETHONG/TAIKHOAN/login-request';



@Injectable({
  providedIn: 'root'
})
export class BaseAPIService {
  private baseUrl = 'https://localhost:7078/api/';
  private currentUser! : any;
  constructor(private http: HttpClient) { }
  public GetApi<T>(action : string){
    const url = `${this.baseUrl}${action}`;
    return this.http.post<any>(url, {
      withCredentials: true
    }).pipe(
      map((response: any) =>{
        return this.executeApiResponse(response);
      }),
      catchError(error =>{
        return throwError(()=> new Error("Lỗi hệ thống"));
      })
    )
  }
  public PostApi<T>(action : string, model: T) : Observable<ResponseData>{
    const url = `${this.baseUrl}${action}`;
    const header = new HttpHeaders({
      'Content-Type' : 'application/json'
    });
    return this.http.post<ResponseData>(url, model, {
      headers : header,
      withCredentials: true
    }).pipe(
      map((response: any) =>{
        return this.executeApiResponse(response);
      }),
      catchError(error =>{
        return throwError(()=> new Error("Lỗi hệ thống"));
      })
    )
  };
  public LoginApi(action: string, model: LoginRequest):Observable<ResponseData>{
    const url = `${this.baseUrl}${action}`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<ResponseData>(url, model, { headers }).pipe(
      map((response: any) => {
        return this.executeApiResponse(response);
      }),
      catchError(error => {
        return throwError(() => new Error('Lỗi hệ thống'));
      })
    );
  }
  public Logout(action: string): Observable<ResponseData>{
    const url = `${this.baseUrl}${action}`;
    return this.http.post<ResponseData>(url,{}).pipe(
      map((response: any)=>{
        return this.executeApiResponse(response);
      }),catchError(error=>{
        return throwError(()=> new Error("Lỗi hệ thống"));
      })
    )
  };
  private executeApiResponse(response : any): ResponseData{
    let result: ResponseData = {
      status: true,
      message: '',
      data: null
    };
    if(response){
      if(!response.success || response.statusCode !== 200){
        result.status = false;
        result.message = response.message || "Lỗi hệ thống";
      }
      else{
        result.status = true;
        result.data = response.result;
      }
    }
    else {
      result.status = false;
      result.message = 'Lỗi hệ thống';
    }
    return result;
  }
  public setCurrentUser(userName : any){
    this.currentUser = userName;
  }
  public getCurrentUser() : any{
    return this.currentUser;
  }
}
export type { ResponseData };

