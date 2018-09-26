import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';
import { LoginService } from '../../core/login.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  validLogin: boolean;
  username: string;
  password: string;


  constructor(private loginService: LoginService, private router: Router, private http: HttpClient) {
  }

  ngOnInit(): void {
    this.validLogin = false;
    this.username = null;
  }

  login(form: NgForm) {
    let credentials = JSON.stringify(form.value);

    this.loginService.login(this.username, this.password, credentials)
      .subscribe(response => {
        let token = (<any>response).token;
        localStorage.setItem("jwt", token);
        this.validLogin = true;
        this.loginService.validLogin = true;
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
  }
}
