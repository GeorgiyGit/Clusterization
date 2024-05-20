import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramReplyDataObjectComponent } from './telegram-reply-data-object.component';

describe('TelegramReplyDataObjectComponent', () => {
  let component: TelegramReplyDataObjectComponent;
  let fixture: ComponentFixture<TelegramReplyDataObjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramReplyDataObjectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramReplyDataObjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
