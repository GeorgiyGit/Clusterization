import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeChannelsLoadingByIdPageComponent } from './doc-youtube-channels-loading-by-id-page.component';

describe('DocYoutubeChannelsLoadingByIdPageComponent', () => {
  let component: DocYoutubeChannelsLoadingByIdPageComponent;
  let fixture: ComponentFixture<DocYoutubeChannelsLoadingByIdPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeChannelsLoadingByIdPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeChannelsLoadingByIdPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
