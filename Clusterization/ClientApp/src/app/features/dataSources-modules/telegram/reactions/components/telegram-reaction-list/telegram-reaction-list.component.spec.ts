import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramReactionListComponent } from './telegram-reaction-list.component';

describe('TelegramReactionListComponent', () => {
  let component: TelegramReactionListComponent;
  let fixture: ComponentFixture<TelegramReactionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramReactionListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramReactionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
