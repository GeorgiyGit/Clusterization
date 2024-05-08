import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramMessagesLoadingGroupPageComponent } from './doc-telegram-messages-loading-group-page.component';

describe('DocTelegramMessagesLoadingGroupPageComponent', () => {
  let component: DocTelegramMessagesLoadingGroupPageComponent;
  let fixture: ComponentFixture<DocTelegramMessagesLoadingGroupPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramMessagesLoadingGroupPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramMessagesLoadingGroupPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
