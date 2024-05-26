import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramMessagesTasksListPageComponent } from './telegram-messages-tasks-list-page.component';

describe('TelegramMessagesTasksListPageComponent', () => {
  let component: TelegramMessagesTasksListPageComponent;
  let fixture: ComponentFixture<TelegramMessagesTasksListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramMessagesTasksListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramMessagesTasksListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
