import { trigger, state, style, transition, animate } from '@angular/animations';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';

@Component({
  selector: 'app-external-data-header',
  templateUrl: './external-data-header.component.html',
  styleUrl: './external-data-header.component.scss',
  animations: [
    trigger('popUpAnimation', [
      state('in', style({ transform: 'translate3d(0, 0, 0)' })),
      state('hidden', style({ transform: 'translate3d(-100%, 0, 0)' })),
      transition('void => in', [
        style({ transform: 'translate3d(-100%, 0, 0)' }),
        animate('500ms cubic-bezier(0.4, 0, 0.2, 1)')
      ]),
      transition('in => hidden', animate('500ms cubic-bezier(0.4, 0, 0.2, 1)'))
    ]),
    trigger('inputState', [
      state(
        'closed',
        style({
          width: '0',
          opacity: 0,
        })
      ),
      state(
        'open',
        style({
          width: '200px', // Set your desired width for the input when it's open
          opacity: 1,
        })
      ),
      transition('closed <=> open', [animate('200ms ease-in-out')]), // Adjust the timing here as needed
    ])
  ]
})
export class ExternalDataHeaderComponent implements OnInit {
  animationState: string = 'in';
  
  constructor(private responsive: BreakpointObserver,
    private router: Router,
    private toaster: MyToastrService,
    public accountService:AccountService) {
  }

  isPhoneMenuOpen: boolean;

  ngOnInit(): void {
    this.responsive.observe([
      Breakpoints.XSmall,
      Breakpoints.TabletPortrait])
      .subscribe(result => {

        const breakpoints = result.breakpoints;

        if (breakpoints[Breakpoints.TabletPortrait] || breakpoints[Breakpoints.XSmall]) {
          this.isPhoneMenuOpen = true;
        }
        else this.isPhoneMenuOpen = false;
      });
  }

  isDisplayPhoneMenu: boolean;
  openDisplayPhoneMenu() {
    this.animationState='in';

    this.isDisplayPhoneMenu = true;
    this.router.navigate([{ outlets: { overflow: null } }]);
  }

  isButtonDisplay: boolean = true;
  closeDisplayPhoneMenu() {
    this.animationState = 'hidden';

    setTimeout(()=>{
      this.isDisplayPhoneMenu = false;
    },500);
  }

  openHome(){
    this.animationState = 'hidden';

    setTimeout(()=>{
      this.isDisplayPhoneMenu = false;
      this.router.navigateByUrl('');
    },500);
  }


  openSignUp(event: MouseEvent){
    event.stopPropagation();

    this.closeDisplayPhoneMenu();
    this.router.navigate([{ outlets: { overflow: 'sign-up' } }]);
  }

  openLogIn(event: MouseEvent) {
    event.stopPropagation();

    this.closeDisplayPhoneMenu();
    this.router.navigate([{ outlets: { overflow: 'log-in' } }]);
  }

  logout(): void {
    this.accountService.logout();
  }

  notAuthorizedErrorStr=$localize`Ви не авторизовані!`;
  visitorError = $localize`Недостатній рівень доступу. Для цієї дії необхідно підтвердити email!`;
  openLoadPack(event:MouseEvent){
    event.stopPropagation();

    if(!this.accountService.isAuthenticated()){
      this.toaster.error(this.notAuthorizedErrorStr);
      return;
    }
    if (!this.accountService.isUserUser()) {
      this.toaster.error(this.visitorError);
      return;
    }

    this.closeDisplayPhoneMenu();
    this.router.navigate([{ outlets: { overflow: 'dataSources/externalData/load-objects' } }]);
  }

}
