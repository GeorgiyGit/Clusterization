import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ICustomerPersonalInfo } from '../../models/customer-personal-info';
import { UsersService } from 'src/app/features/admin-panel-module/users/services/users.service';

@Component({
  selector: 'app-customer-personal-information-page',
  templateUrl: './customer-personal-information-page.component.html',
  styleUrl: './customer-personal-information-page.component.scss'
})
export class CustomerPersonalInformationPageComponent implements OnInit{
  personalInfo:ICustomerPersonalInfo;
  constructor(private accountService:AccountService,
    private usersService:UsersService,
    private toastr:MyToastrService
  ){}
  ngOnInit(): void {
    this.usersService.customerGetPersonalInfo().subscribe(res=>{
      this.personalInfo=res;
    },error=>{
      this.toastr.error(error.error.Message);
    })
  }

  isLoading:boolean;
  sendEmail(){
    if(this.isLoading)return;
    this.isLoading=true;
    this.accountService.sendConfirmationEmail().subscribe(res=>{
      this.isLoading=false;
      this.toastr.success($localize`Лист успішно надісланий`);
    },error=>{
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    })
  }
}
