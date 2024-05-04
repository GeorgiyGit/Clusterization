import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramLoadGroupRepliesPageComponent } from './telegram-load-group-replies-page.component';

describe('TelegramLoadGroupRepliesPageComponent', () => {
  let component: TelegramLoadGroupRepliesPageComponent;
  let fixture: ComponentFixture<TelegramLoadGroupRepliesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramLoadGroupRepliesPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramLoadGroupRepliesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
