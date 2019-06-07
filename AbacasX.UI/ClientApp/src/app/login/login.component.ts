import { HttpClient} from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';
import { LoginService } from '../../core/login.service';
import { RoleTypeEnum, LoginResults } from '../../shared/interfaces';


@Component({
  selector: 'login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  @Output() roleUpdated = new EventEmitter<RoleTypeEnum>();

  validLogin: boolean;
  username: string;
  password: string;
  userRole: RoleTypeEnum = RoleTypeEnum.Investor;
  userId: number = 0;
  

  constructor(private loginService: LoginService, private router: Router, private http: HttpClient)
  {
    loginService.loginEvent.subscribe((loginResults: LoginResults) => {
      this.validLogin = loginResults.successfulLogin;
      this.userRole = loginResults.userRole;
      this.username = loginResults.userName;
      this.userId = loginResults.userId;

      console.log(`New Login for user ${this.username} with role of ${this.userRole}`);

      this.roleUpdated.emit(this.userRole);
      this.router.navigate(["/"]);

    }, err => {
      this.validLogin = false;
      this.userRole = RoleTypeEnum.Guest;
      this.username = null;
      this.roleUpdated.emit(this.userRole);
      this.userId = 0;

      this.router.navigate(["/"]);
      });

    loginService.logoutEvent.subscribe((results: boolean) => {
      console.log(`Logout for user ${this.username}`);

      this.username = null;
      this.validLogin = false;
      this.userId = 0;
      this.userRole = this.loginService.userRole;
      this.roleUpdated.emit(this.userRole);

      this.router.navigate(["/"]);
    });
    
  }

  ngOnInit(): void {
    this.validLogin = this.loginService.validLogin;
    this.username = this.loginService.username;
    this.userRole = this.loginService.userRole;
  }

  login(form: NgForm)

  {
    let credentials = JSON.stringify(form.value);
    this.loginService.login(this.username, this.password, credentials);
  }

  
  logOut() {
    this.loginService.logOut();
  }
}
