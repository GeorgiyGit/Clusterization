import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeChannelPhoneCardComponent } from './youtube-channel-phone-card.component';

describe('YoutubeChannelPhoneCardComponent', () => {
  let component: YoutubeChannelPhoneCardComponent;
  let fixture: ComponentFixture<YoutubeChannelPhoneCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YoutubeChannelPhoneCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(YoutubeChannelPhoneCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
