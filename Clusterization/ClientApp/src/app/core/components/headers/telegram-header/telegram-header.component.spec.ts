import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramHeaderComponent } from './telegram-header.component';

describe('TelegramHeaderComponent', () => {
  let component: TelegramHeaderComponent;
  let fixture: ComponentFixture<TelegramHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramHeaderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
