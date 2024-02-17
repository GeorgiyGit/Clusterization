import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ILogIn } from '../../models/log-in';
import { Router } from '@angular/router';
import { ILoginResponse } from '../../models/login-response';
import { AccountService } from '../../services/account.service';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-log-in-page',
  templateUrl: './log-in-page.component.html',
  styleUrls: ['./log-in-page.component.scss'],
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
export class LogInPageComponent implements OnInit{
  animationState:string='in';

  logInForm: FormGroup = this.fb.group({
    email: ['', [Validators.required, Validators.maxLength(1000)]],
    password: ['', [Validators.required,Validators.minLength(8)]]
  });

  get formValue() {
    return this.logInForm.value as ILogIn;
  }

  get email() { return this.logInForm.get('email')!; }
  get password() { return this.logInForm.get('password')!; }

  response: { dbPath: '' };

  isLoading:boolean;

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router) {
  }
  ngOnInit(): void {
    this.animationState='in';
  }

  isPassVisible = false;

  errorMsg:string;

  eyeClick(event: any) {
    event.stopPropagation();
    this.isPassVisible = !this.isPassVisible;
  }

  logIn(): void {
    if (this.logInForm.invalid) {
      return;
    }

    const user: ILogIn = this.logInForm.value;

    this.isLoading=true;
    this.accountService.logIn(user).subscribe((result: ILoginResponse) => {
      this.isLoading=false;
      this.accountService.saveToken(result.token);
      this.close(null);
    },error => {
      this.isLoading=false;
      this.errorMsg=error.error.Message;
    });
  }

  close(event: any) {
    this.animationState='hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }

  openSignUp(event: MouseEvent) {
    event.stopPropagation();

    this.router.navigate([{ outlets: { overflow: 'sign-up' } }]);
  }
}
