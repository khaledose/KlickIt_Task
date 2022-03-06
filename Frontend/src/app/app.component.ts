import { Component } from '@angular/core';
import { TokenStorageService } from './_services/token-storage.service';
import { UserStateService } from './_services/user-state.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  private roles: string[] = [];
  title = 'KlickIt';
  state: any = {
    isLoggedIn: false,
    isAdmin: false,
    isUser: false
  }
  username?: string;

  constructor(private tokenStorageService: TokenStorageService, public userStateService:UserStateService) { }
  ngOnInit(): void {
    this.state.isLoggedIn = !!this.tokenStorageService.getToken();

    if (this.state.isLoggedIn) {
      const user = this.tokenStorageService.getUser();
      console.log(user)
      this.roles = user.roles;

      this.state.isUser = this.roles.includes('User');
      this.state.isAdmin = this.roles.includes('Admin');

      this.username = user.username;
    }
    this.userStateService.state = this.state;
  }

  logout(): void {
    this.tokenStorageService.signOut();
    window.location.reload();
  }
}
