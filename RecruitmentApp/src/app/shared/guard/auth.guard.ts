import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { AuthService } from '../../shared/services/auth.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthSessionService } from '../services/auth-session.service';
import { JsonConfigService } from '../services/json-config.service';

@Injectable({
  providedIn: 'root',
})

export class AuthGuard implements CanActivate {
  constructor(public authSession: AuthSessionService, private jsonConf:JsonConfigService, public router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {

    if(!this.authSession.initialValidation){
      return new Observable<boolean>(obs=>{

        this.jsonConf.config.subscribe(conf=>{
          this.authSession.validateSession(obs)
        })
        
      })
    }
    if (this.authSession.validSession !== true) {
      this.router.navigate(['/']);
    }
    return true;
  }
}
