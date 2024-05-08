import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramMessagesDisplayingPageComponent } from './doc-telegram-messages-displaying-page.component';

describe('DocTelegramMessagesDisplayingPageComponent', () => {
  let component: DocTelegramMessagesDisplayingPageComponent;
  let fixture: ComponentFixture<DocTelegramMessagesDisplayingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramMessagesDisplayingPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramMessagesDisplayingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
