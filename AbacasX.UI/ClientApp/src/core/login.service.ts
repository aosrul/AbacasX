import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Router } from '@angular/router';
import { RoleTypeEnum, LoginResults } from '../shared/interfaces';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable()
export class LoginService {

  username: string = "";
  validLogin: boolean = false;
  userRole: RoleTypeEnum = RoleTypeEnum.Guest;

  loginEvent = new EventEmitter<LoginResults>();
  logoutEvent = new EventEmitter<boolean>();


  constructor(private jwtHelperService: JwtHelperService, private router: Router, private http: HttpClient) {
  }

  login(username: string, password: string, credentials: string) {

    this.username = username;
    let loginResults = { } as LoginResults;
 
    this.http.post("/api/auth/login", credentials,
      {
        headers: new HttpHeaders(
          {
            "Content-Type": "application/json"
          })
      }).subscribe(response => {
        /*
         * A successful response, but insure that the role is defined, otherwise they remain as a guest.
         */
        let token = (<any>response).token;
        localStorage.setItem("jwt", token);

        this.validLogin = false;
        this.userRole = RoleTypeEnum.Guest;

        // Extracting the Role from the JWT Token
        var tokenObj = this.jwtHelperService.decodeToken(token);
        console.log(tokenObj); // token

        for (var key in tokenObj) {
          var keyValue = tokenObj[key];
          if (typeof key === "string") {
            if (key.search('(claims\/role)') != -1) {

              switch (keyValue) {
                case 'Investor': this.userRole = RoleTypeEnum.Investor;
                  this.validLogin = true;
                  break;
                case 'Guest': this.userRole = RoleTypeEnum.Guest;
                  this.validLogin = true;
                  break;
                case 'Broker': this.userRole = RoleTypeEnum.Broker;
                  this.validLogin = true;
                  break;
                case 'Admin': this.userRole = RoleTypeEnum.Admin;
                  this.validLogin = true;
                  break;
                case 'Ops': this.userRole = RoleTypeEnum.Ops;
                  this.validLogin = true;
                  break;
                case 'Custodian': this.userRole = RoleTypeEnum.Custodian;
                  this.validLogin = true;
                  break;
                default:
                  this.userRole = RoleTypeEnum.Guest;
                  this.validLogin = false;
              }
              console.log(`User Role is ${keyValue}`);
            }
          }
        }

        loginResults.userName = username;
        loginResults.userRole = this.userRole;
        loginResults.successfulLogin = this.validLogin;

        if (this.validLogin == false) {
          this.username = "Guest";
        }

        this.loginEvent.emit(loginResults);

      }, err => {

        this.username = "Guest";
        this.validLogin = false;

        loginResults.userRole = RoleTypeEnum.Guest;
        loginResults.successfulLogin = false;
        loginResults.userName = this.username;

        this.loginEvent.emit(loginResults);
      });
  }

  logOut() {

    localStorage.removeItem("jwt");
    this.validLogin = false;
    this.username = null;
    this.userRole = RoleTypeEnum.Guest;

    this.logoutEvent.emit(true);
  }
}

