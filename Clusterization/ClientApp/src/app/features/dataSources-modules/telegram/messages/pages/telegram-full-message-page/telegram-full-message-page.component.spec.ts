import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramFullMessagePageComponent } from './telegram-full-message-page.component';

describe('TelegramFullMessagePageComponent', () => {
  let component: TelegramFullMessagePageComponent;
  let fixture: ComponentFixture<TelegramFullMessagePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramFullMessagePageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramFullMessagePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
