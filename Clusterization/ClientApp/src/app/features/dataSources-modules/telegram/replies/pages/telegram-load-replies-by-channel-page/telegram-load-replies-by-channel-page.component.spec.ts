import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramLoadRepliesByChannelPageComponent } from './telegram-load-replies-by-channel-page.component';

describe('TelegramLoadRepliesByChannelPageComponent', () => {
  let component: TelegramLoadRepliesByChannelPageComponent;
  let fixture: ComponentFixture<TelegramLoadRepliesByChannelPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramLoadRepliesByChannelPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramLoadRepliesByChannelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
