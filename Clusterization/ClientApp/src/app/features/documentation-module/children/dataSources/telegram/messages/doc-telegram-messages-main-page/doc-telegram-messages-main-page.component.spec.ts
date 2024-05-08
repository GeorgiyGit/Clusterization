import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramMessagesMainPageComponent } from './doc-telegram-messages-main-page.component';

describe('DocTelegramMessagesMainPageComponent', () => {
  let component: DocTelegramMessagesMainPageComponent;
  let fixture: ComponentFixture<DocTelegramMessagesMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramMessagesMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramMessagesMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
