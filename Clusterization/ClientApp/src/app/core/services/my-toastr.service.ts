import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class MyToastrService {

  constructor(private toastr:ToastrService){}

  error(msg:string | undefined){
    let str = msg;
    if(msg==undefined)str=$localize`Помилка`;
    this.toastr.error(str);
  }

  success(msg:string){
    this.toastr.success(msg);
  }
}
