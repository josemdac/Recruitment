import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WebSiteInfoService } from '../services/web-site-info.service';
import { SessionToken } from '../constants';



@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private webConf: WebSiteInfoService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
       // // debugger;
        // add auth header with jwt if user is logged in and request is to the api url
        const userToken = localStorage.getItem(SessionToken)
        //console.log(userToken)
        if (this.webConf.companyToken) {

            const origin = window.location.origin
            const base = document.querySelector('base')
            request = request.clone({
                setHeaders: {
                   
                    CompanyToken: this.webConf.companyToken,
                    Authorization: 'Bearer ' + userToken,
                    ActivateRoute: `${origin}${base?.getAttribute('href')}activate`
                }
            });
        }

        return next.handle(request);
    }
}


