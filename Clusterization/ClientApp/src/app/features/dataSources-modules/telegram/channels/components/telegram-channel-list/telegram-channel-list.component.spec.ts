import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramChannelListComponent } from './telegram-channel-list.component';

describe('TelegramChannelListComponent', () => {
  let component: TelegramChannelListComponent;
  let fixture: ComponentFixture<TelegramChannelListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramChannelListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramChannelListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
