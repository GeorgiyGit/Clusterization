import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramChannelsMainPageComponent } from './doc-telegram-channels-main-page.component';

describe('DocTelegramChannelsMainPageComponent', () => {
  let component: DocTelegramChannelsMainPageComponent;
  let fixture: ComponentFixture<DocTelegramChannelsMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramChannelsMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramChannelsMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
