import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ok } from 'assert';


@Injectable()
export class LoginService {

  username: string;
  validLogin: boolean;

  constructor(private router: Router, private http: HttpClient) { }

  login(username: string, password: string, credentials: string): Observable<any> {

    this.username = username;

    return this.http.post("http://localhost:63720/api/auth/login", credentials,
      {
        headers: new HttpHeaders(
          {
            "Content-Type": "application/json"
          })
      });
  }
}

