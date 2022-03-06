import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserStateService {

  state: any = {
    isLoggedIn: false,
    isAdmin: false,
    isUser: false
  }
  constructor() { }

  get getter(): any{
    return this.state;
  }
  set setter(newState: any){
    this.state = newState;
  }
}
