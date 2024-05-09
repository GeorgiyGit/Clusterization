import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramRepliesLoadFromMsgPageComponent } from './doc-telegram-replies-load-from-msg-page.component';

describe('DocTelegramRepliesLoadFromMsgPageComponent', () => {
  let component: DocTelegramRepliesLoadFromMsgPageComponent;
  let fixture: ComponentFixture<DocTelegramRepliesLoadFromMsgPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramRepliesLoadFromMsgPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramRepliesLoadFromMsgPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
