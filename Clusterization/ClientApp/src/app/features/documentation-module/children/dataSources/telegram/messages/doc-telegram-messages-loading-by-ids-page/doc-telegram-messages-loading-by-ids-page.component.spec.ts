import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramMessagesLoadingByIdsPageComponent } from './doc-telegram-messages-loading-by-ids-page.component';

describe('DocTelegramMessagesLoadingByIdsPageComponent', () => {
  let component: DocTelegramMessagesLoadingByIdsPageComponent;
  let fixture: ComponentFixture<DocTelegramMessagesLoadingByIdsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramMessagesLoadingByIdsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramMessagesLoadingByIdsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
