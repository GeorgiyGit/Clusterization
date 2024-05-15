import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerYoutubeVideosLoadedListPageComponent } from './customer-youtube-videos-loaded-list-page.component';

describe('CustomerYoutubeVideosLoadedListPageComponent', () => {
  let component: CustomerYoutubeVideosLoadedListPageComponent;
  let fixture: ComponentFixture<CustomerYoutubeVideosLoadedListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerYoutubeVideosLoadedListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerYoutubeVideosLoadedListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
