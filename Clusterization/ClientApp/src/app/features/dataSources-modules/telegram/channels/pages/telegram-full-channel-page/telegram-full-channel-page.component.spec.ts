import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramFullChannelPageComponent } from './telegram-full-channel-page.component';

describe('TelegramFullChannelPageComponent', () => {
  let component: TelegramFullChannelPageComponent;
  let fixture: ComponentFixture<TelegramFullChannelPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramFullChannelPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramFullChannelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
