import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramChannelsDisplayingPageComponent } from './doc-telegram-channels-displaying-page.component';

describe('DocTelegramChannelsDisplayingPageComponent', () => {
  let component: DocTelegramChannelsDisplayingPageComponent;
  let fixture: ComponentFixture<DocTelegramChannelsDisplayingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramChannelsDisplayingPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramChannelsDisplayingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
