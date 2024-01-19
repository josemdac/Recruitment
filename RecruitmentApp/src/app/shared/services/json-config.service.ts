import { HttpClient, HttpContext, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { resourceUsage } from 'process';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JsonConfigService {

  constructor(private http:HttpClient) { 
    this.loadConfig()
  }

  config = new BehaviorSubject<{ serverApi: string}>({ serverApi: ''})
  loaded = false
  

  loadConfig(){

    let request = new XMLHttpRequest();
    request.addEventListener('load', (ev)=>{
      //console.log(ev)
      this.config.next(JSON.parse(request.responseText))
      this.loaded = true
    })
    request.open('GET', './assets/json/config.json?v=' + (new Date().getTime()))
    request.send()
  }

  get apiUrl(){
    return this.config.value.serverApi;
  }

  
  get<T>(endpoint:string){
    return this.http.get<T>(this.apiUrl + '/' + endpoint)
  }
  post<T>(endpoint:string, data: any){
    return this.http.post<T>(this.apiUrl + '/' + endpoint, data)
  }
  postHeader<T>(endpoint:string, data: any, headers: any){
    return this.http.post<T>(this.apiUrl + '/' + endpoint, data, headers)
  }
  put<T>(endpoint:string, data: any){
    return this.http.put<T>(this.apiUrl + '/' + endpoint, data)
  }
  delete<T>(endpoint:string, data: any){
    return this.http.delete<T>(this.apiUrl + '/' + endpoint)
  }



}
