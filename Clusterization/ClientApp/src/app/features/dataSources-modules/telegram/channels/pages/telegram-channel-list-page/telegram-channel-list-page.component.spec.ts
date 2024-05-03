import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramChannelListPageComponent } from './telegram-channel-list-page.component';

describe('TelegramChannelListPageComponent', () => {
  let component: TelegramChannelListPageComponent;
  let fixture: ComponentFixture<TelegramChannelListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramChannelListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramChannelListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
