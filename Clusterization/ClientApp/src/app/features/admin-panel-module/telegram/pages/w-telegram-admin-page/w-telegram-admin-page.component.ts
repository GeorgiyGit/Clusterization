import { Component, OnInit } from '@angular/core';
import { WTelegramService } from '../../service/w-telegram.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-w-telegram-admin-page',
  templateUrl: './w-telegram-admin-page.component.html',
  styleUrl: './w-telegram-admin-page.component.scss'
})
export class WTelegramAdminPageComponent implements OnInit{
  constructor(private wTelegramService:WTelegramService,
    private toaster:MyToastrService){}
  ngOnInit(): void {
    this.getStatus();
  }

  status:string
  getStatus(){
    this.wTelegramService.getStatus().subscribe(res=>{
      this.status=res;
    },error=>{
      this.toaster.error(error.error.Message);
    })
  }


  code:string='';
  codeChanges(event:any){
    this.code=event.target.value;
  }

  isLoading:boolean;
  setCode(){
    if(this.isLoading)return;

    this.isLoading=true;
    this.wTelegramService.submitCode(this.code).subscribe(res=>{
      this.isLoading=false;
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    })
  }
}
