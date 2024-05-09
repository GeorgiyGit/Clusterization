import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramChannelsLoadingByUsernamePageComponent } from './doc-telegram-channels-loading-by-username-page.component';

describe('DocTelegramChannelsLoadingByUsernamePageComponent', () => {
  let component: DocTelegramChannelsLoadingByUsernamePageComponent;
  let fixture: ComponentFixture<DocTelegramChannelsLoadingByUsernamePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramChannelsLoadingByUsernamePageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramChannelsLoadingByUsernamePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
