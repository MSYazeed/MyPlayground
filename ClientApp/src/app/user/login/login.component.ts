import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./../user.component.scss'],
})
export class LoginComponent {
  private username = '';
  private password = '';
}
