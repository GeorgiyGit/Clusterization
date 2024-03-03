import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MyLocalStorageService {

  workspaceName: 'selectedworkspace';
  userTokenName: 'usertoken';
  constructor() { }

  setSelectedWorkspace(id: number) {
    localStorage.setItem("selectedworkspace", id + "");
  }
  getSelectedWorkspace(): number | undefined {
    let id = localStorage.getItem("selectedworkspace");

    if (id == null) return undefined;

    return parseInt(id);
  }

  setUserToken(token: string) {
    localStorage.setItem("usertoken", token);
  }
  getUserToken(): string | null {
    return localStorage.getItem("usertoken");
  }
  removeUserToken(){
    localStorage.removeItem("usertoken");
  }
}
