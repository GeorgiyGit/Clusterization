import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerYoutubeChannelsLoadedListPageComponent } from './customer-youtube-channels-loaded-list-page.component';

describe('CustomerYoutubeChannelsLoadedListPageComponent', () => {
  let component: CustomerYoutubeChannelsLoadedListPageComponent;
  let fixture: ComponentFixture<CustomerYoutubeChannelsLoadedListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerYoutubeChannelsLoadedListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerYoutubeChannelsLoadedListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
