import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramChannelsTasksListPageComponent } from './telegram-channels-tasks-list-page.component';

describe('TelegramChannelsTasksListPageComponent', () => {
  let component: TelegramChannelsTasksListPageComponent;
  let fixture: ComponentFixture<TelegramChannelsTasksListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramChannelsTasksListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramChannelsTasksListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
