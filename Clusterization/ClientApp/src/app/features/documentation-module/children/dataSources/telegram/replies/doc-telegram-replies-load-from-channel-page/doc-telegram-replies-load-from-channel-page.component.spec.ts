import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramRepliesLoadFromChannelPageComponent } from './doc-telegram-replies-load-from-channel-page.component';

describe('DocTelegramRepliesLoadFromChannelPageComponent', () => {
  let component: DocTelegramRepliesLoadFromChannelPageComponent;
  let fixture: ComponentFixture<DocTelegramRepliesLoadFromChannelPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramRepliesLoadFromChannelPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramRepliesLoadFromChannelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
