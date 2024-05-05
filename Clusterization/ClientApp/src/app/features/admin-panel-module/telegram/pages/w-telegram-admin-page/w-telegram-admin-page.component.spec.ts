import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WTelegramAdminPageComponent } from './w-telegram-admin-page.component';

describe('WTelegramAdminPageComponent', () => {
  let component: WTelegramAdminPageComponent;
  let fixture: ComponentFixture<WTelegramAdminPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WTelegramAdminPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WTelegramAdminPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
