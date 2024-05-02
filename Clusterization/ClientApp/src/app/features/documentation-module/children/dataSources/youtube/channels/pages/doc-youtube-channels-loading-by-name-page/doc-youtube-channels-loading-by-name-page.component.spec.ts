import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeChannelsLoadingByNamePageComponent } from './doc-youtube-channels-loading-by-name-page.component';

describe('DocYoutubeChannelsLoadingByNamePageComponent', () => {
  let component: DocYoutubeChannelsLoadingByNamePageComponent;
  let fixture: ComponentFixture<DocYoutubeChannelsLoadingByNamePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeChannelsLoadingByNamePageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeChannelsLoadingByNamePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
