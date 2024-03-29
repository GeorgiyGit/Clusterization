import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RolesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "MyRoles/";
  }

  addModerator(id:string): Observable<any> {
    console.log(this.controllerUrl + "add_moderator/"+id);
    return this.http.post(this.controllerUrl + "add_moderator/"+id,null);
  }
  removeModerator(id:string): Observable<any> {
    console.log(this.controllerUrl + "remove_moderator/"+id);
    return this.http.delete(this.controllerUrl + "remove_moderator/"+id);
  }
}
