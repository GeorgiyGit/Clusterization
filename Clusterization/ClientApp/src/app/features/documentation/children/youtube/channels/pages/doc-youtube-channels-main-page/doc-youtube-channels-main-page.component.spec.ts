import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeChannelsMainPageComponent } from './doc-youtube-channels-main-page.component';

describe('DocYoutubeChannelsMainPageComponent', () => {
  let component: DocYoutubeChannelsMainPageComponent;
  let fixture: ComponentFixture<DocYoutubeChannelsMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeChannelsMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeChannelsMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
