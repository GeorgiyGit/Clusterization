import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramLoadNewChannelsPageComponent } from './telegram-load-new-channels-page.component';

describe('TelegramLoadNewChannelsPageComponent', () => {
  let component: TelegramLoadNewChannelsPageComponent;
  let fixture: ComponentFixture<TelegramLoadNewChannelsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramLoadNewChannelsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramLoadNewChannelsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
