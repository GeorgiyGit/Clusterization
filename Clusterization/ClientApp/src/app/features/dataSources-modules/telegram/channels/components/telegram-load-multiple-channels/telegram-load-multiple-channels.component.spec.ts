import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramLoadMultipleChannelsComponent } from './telegram-load-multiple-channels.component';

describe('TelegramLoadMultipleChannelsComponent', () => {
  let component: TelegramLoadMultipleChannelsComponent;
  let fixture: ComponentFixture<TelegramLoadMultipleChannelsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramLoadMultipleChannelsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramLoadMultipleChannelsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
