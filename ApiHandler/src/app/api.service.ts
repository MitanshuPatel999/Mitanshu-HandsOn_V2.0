import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  // private apiUrl: string="http://localhost:5069/mmcontroller";
  // constructor(private http: HttpClient) { }

  // getProducts(): Observable<any[]> {
  //   return this.http.get<any[]>(this.apiUrl);
  // }
}
