import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramReactionComponent } from './telegram-reaction.component';

describe('TelegramReactionComponent', () => {
  let component: TelegramReactionComponent;
  let fixture: ComponentFixture<TelegramReactionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramReactionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramReactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
