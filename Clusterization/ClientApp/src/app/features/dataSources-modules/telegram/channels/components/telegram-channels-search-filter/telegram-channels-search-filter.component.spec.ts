import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramChannelsSearchFilterComponent } from './telegram-channels-search-filter.component';

describe('TelegramChannelsSearchFilterComponent', () => {
  let component: TelegramChannelsSearchFilterComponent;
  let fixture: ComponentFixture<TelegramChannelsSearchFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramChannelsSearchFilterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramChannelsSearchFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
