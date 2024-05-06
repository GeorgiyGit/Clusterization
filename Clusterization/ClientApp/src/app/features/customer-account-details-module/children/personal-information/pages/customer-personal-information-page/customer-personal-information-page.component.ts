import { Subject } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-customer-personal-information-page',
  templateUrl: './customer-personal-information-page.component.html',
  styleUrl: './customer-personal-information-page.component.scss'
})
export class CustomerPersonalInformationPageComponent implements OnInit{

  isEmailConfirmed:boolean;
  constructor(private accountService:AccountService,
    private toastr:MyToastrService
  ){}
  ngOnInit(): void {
    this.accountService.checkEmailConfirmation().subscribe(res=>{
      this.isEmailConfirmed=res;
    },error=>{
      this.toastr.error(error.error.Message);
    })
  }
  sendEmail(){
    this.accountService.sendConfirmationEmail().subscribe(res=>{
      this.toastr.success($localize`Лист успішно надісланий`);
    },error=>{
      this.toastr.error(error.error.Message);
    })
  }
}
