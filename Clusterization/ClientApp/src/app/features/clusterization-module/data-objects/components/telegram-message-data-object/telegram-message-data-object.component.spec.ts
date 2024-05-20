import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramMessageDataObjectComponent } from './telegram-message-data-object.component';

describe('TelegramMessageDataObjectComponent', () => {
  let component: TelegramMessageDataObjectComponent;
  let fixture: ComponentFixture<TelegramMessageDataObjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramMessageDataObjectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramMessageDataObjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
