import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MyToastrService } from '../../services/my-toastr.service';
import { environment } from 'src/environments/environment';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { AccountService } from 'src/app/features/account/services/account.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
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
export class HeaderComponent implements OnInit {
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


  openSignUp(event: MouseEvent){
    event.stopPropagation();

    this.router.navigate([{ outlets: { overflow: 'sign-up' } }]);
  }

  openLogIn(event: MouseEvent) {
    event.stopPropagation();

    this.router.navigate([{ outlets: { overflow: 'log-in' } }]);
  }

  logout(): void {
    this.accountService.logout();
  }

  openLoadChannel(event:MouseEvent){
    event.stopPropagation();

    if(!this.accountService.isAuthenticated()){
      this.toaster.error('You are not authorized!');
    }

    this.router.navigate([{ outlets: { overflow: 'load-channel' } }]);
  }

  openAddAlgorithm(event:MouseEvent){
    event.stopPropagation();

    if(!this.accountService.isAuthenticated()){
      this.toaster.error('You are not authorized!');
    }

    this.router.navigate([{ outlets: { overflow: 'algorithms/add' } }]);
  }
}
