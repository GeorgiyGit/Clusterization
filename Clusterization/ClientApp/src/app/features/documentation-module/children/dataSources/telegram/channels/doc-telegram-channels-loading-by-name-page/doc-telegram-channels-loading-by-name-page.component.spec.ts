import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramChannelsLoadingByNamePageComponent } from './doc-telegram-channels-loading-by-name-page.component';

describe('DocTelegramChannelsLoadingByNamePageComponent', () => {
  let component: DocTelegramChannelsLoadingByNamePageComponent;
  let fixture: ComponentFixture<DocTelegramChannelsLoadingByNamePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramChannelsLoadingByNamePageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramChannelsLoadingByNamePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
