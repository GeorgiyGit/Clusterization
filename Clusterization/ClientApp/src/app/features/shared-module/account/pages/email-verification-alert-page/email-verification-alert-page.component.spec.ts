import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailVerificationAlertPageComponent } from './email-verification-alert-page.component';

describe('EmailVerificationAlertPageComponent', () => {
  let component: EmailVerificationAlertPageComponent;
  let fixture: ComponentFixture<EmailVerificationAlertPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmailVerificationAlertPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmailVerificationAlertPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
