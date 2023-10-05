import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MyLocalStorageService {

  workspaceName:'selected_workspace';
  constructor() { }

  setSelectedWorkspace(id:number){
    localStorage.setItem(this.workspaceName,id+"");
  }
  getSelectedWorkspace():number | undefined{
    let id = localStorage.getItem(this.workspaceName);

    if(id==null)return undefined;

    return parseInt(id);
  }
}
