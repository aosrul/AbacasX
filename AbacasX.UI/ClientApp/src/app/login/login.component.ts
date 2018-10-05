import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';
import { LoginService } from '../../core/login.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RoleTypeEnum } from '../../shared/interfaces';


@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  @Output() roleUpdated = new EventEmitter<RoleTypeEnum>();

  validLogin: boolean;
  username: string;
  password: string;
  userRole: RoleTypeEnum = RoleTypeEnum.Guest;

    constructor(private jwtHelperService: JwtHelperService, private loginService: LoginService, private router: Router, private http: HttpClient) {
  }

  ngOnInit(): void {
    this.validLogin = false;
    this.username = null;
    this.userRole = RoleTypeEnum.Guest;
  }

  login(form: NgForm) {
    let credentials = JSON.stringify(form.value);

    this.loginService.login(this.username, this.password, credentials)
      .subscribe(response => {
        let token = (<any>response).token;
        localStorage.setItem("jwt", token);
        this.validLogin = true;
        this.loginService.validLogin = true;

        var tokenObj = this.jwtHelperService.decodeToken(token);
        console.log(tokenObj); // token
        
        for (var key in tokenObj) {
          var keyValue = tokenObj[key];
          if (typeof key === "string")
          {
            if (key.search('(claims\/role)') != -1)
            {

              switch (keyValue) {
                case 'Investor': this.userRole = RoleTypeEnum.Investor;
                  break;
                case 'Guest': this.userRole = RoleTypeEnum.Guest;
                  break;
                case 'Broker': this.userRole = RoleTypeEnum.Broker;
                  break;
                case 'Admin': this.userRole = RoleTypeEnum.Admin;
                  break;
                case 'Ops': this.userRole = RoleTypeEnum.Ops;
                  break;
                default:
                  this.userRole = RoleTypeEnum.Guest;
              }

              this.roleUpdated.emit(this.userRole);

              console.log(`User Role is ${keyValue}`);
            }
          }
        }

        this.router.navigate(["/"]);
      }, err => {
        this.validLogin = false;
        this.loginService.validLogin = false;
        this.router.navigate(["/"]);
      });
  }

  logOut() {
    localStorage.removeItem("jwt");
    this.validLogin = false;
    this.loginService.validLogin = false;
    this.username = null;
    this.password = null;
    this.userRole = RoleTypeEnum.Guest;
    this.roleUpdated.emit(this.userRole);
  }
}
