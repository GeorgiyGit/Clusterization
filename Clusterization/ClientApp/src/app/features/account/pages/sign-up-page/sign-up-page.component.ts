import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { ISignUpRequest } from '../../models/requests/sign-up-request';
import { ILogInResponse } from '../../models/responses/login-response';

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss'],
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
export class SignUpPageComponent implements OnInit{
  animationState:string='in';

  signUpForm: FormGroup = this.fb.group({
    userName: ['',[Validators.required,Validators.minLength(3), Validators.maxLength(100)]],
    email: ['', [Validators.required, Validators.maxLength(1000)]],
    password: ['', [Validators.required,Validators.minLength(8)]]
  });

  get formValue() {
    return this.signUpForm.value as ISignUpRequest;
  }

  get userName() { return this.signUpForm.get('userName')!; }
  get email() { return this.signUpForm.get('email')!; }
  get password() { return this.signUpForm.get('password')!; }

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
    if (this.signUpForm.invalid) {
      return;
    }

    const request: ISignUpRequest = this.signUpForm.value;

    this.isLoading=true;
    this.accountService.signUp(request).subscribe((result: ILogInResponse) => {
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

  openLogIn(event: MouseEvent) {
    event.stopPropagation();

    this.router.navigate([{ outlets: { overflow: 'log-in' } }]);
  }
}
