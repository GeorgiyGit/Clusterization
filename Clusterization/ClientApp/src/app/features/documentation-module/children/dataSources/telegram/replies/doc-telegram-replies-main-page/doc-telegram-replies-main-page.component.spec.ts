import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramRepliesMainPageComponent } from './doc-telegram-replies-main-page.component';

describe('DocTelegramRepliesMainPageComponent', () => {
  let component: DocTelegramRepliesMainPageComponent;
  let fixture: ComponentFixture<DocTelegramRepliesMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramRepliesMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramRepliesMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
