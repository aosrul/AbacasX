import { Component } from '@angular/core';
import { LoginService } from '../../core/login.service';
import { RoleTypeEnum } from '../../shared/interfaces';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  userRole: RoleTypeEnum = RoleTypeEnum.Guest;
  RoleTypeEnum = RoleTypeEnum;

  updateRole(role: RoleTypeEnum) {
    this.userRole = role;
    console.log(`Nav Menu Role is ${this.userRole}`);
  }
}
