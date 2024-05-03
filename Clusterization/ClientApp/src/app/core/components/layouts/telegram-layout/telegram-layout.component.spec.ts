import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramLayoutComponent } from './telegram-layout.component';

describe('TelegramLayoutComponent', () => {
  let component: TelegramLayoutComponent;
  let fixture: ComponentFixture<TelegramLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramLayoutComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
