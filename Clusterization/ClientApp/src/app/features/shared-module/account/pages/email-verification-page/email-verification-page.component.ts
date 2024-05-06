import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-email-verification-page',
  templateUrl: './email-verification-page.component.html',
  styleUrl: './email-verification-page.component.scss',
  animations: [
    trigger('popUpAnimation', [
      state('in', style({ transform: 'translateY(0)' })),
      state('hidden', style({ transform: 'translateY(100%)' })),
      transition('void => in', [
        style({ transform: 'translateY(100%)' }),
        animate('300ms cubic-bezier(0.4, 0, 0.2, 1)')
      ]),
      transition('in => hidden', animate('300ms cubic-bezier(0.4, 0, 0.2, 1)'))
    ])
  ]
})
export class EmailVerificationPageComponent implements OnInit{
  animationState:string='in';

  isLoading:boolean=true;
  isSuccess:boolean;

  token:string;
  email:string;

  constructor(private accountService: AccountService,
    private router: Router,
    private toaster:MyToastrService,
    private route:ActivatedRoute) {
  }
  ngOnInit(): void {
    this.animationState='in';
    this.route.queryParams.subscribe(params=>{
      this.token = params['token'];
      this.email = params['email'];
    })

    this.confirmEmail();
  }

  confirmEmail(){
    this.accountService.confirmEmail(this.token,this.email).subscribe(res=>{
      this.isSuccess=true;
      this.isLoading=false;

      this.accountService.saveToken(res.token);
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    })
  }

  isPassVisible = false;

  errorMsg:string;

  eyeClick(event: any) {
    event.stopPropagation();
    this.isPassVisible = !this.isPassVisible;
  }

  close(event: any) {
    this.animationState='hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }
}
