import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeVideoPhoneCardComponent } from './youtube-video-phone-card.component';

describe('YoutubeVideoPhoneCardComponent', () => {
  let component: YoutubeVideoPhoneCardComponent;
  let fixture: ComponentFixture<YoutubeVideoPhoneCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YoutubeVideoPhoneCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(YoutubeVideoPhoneCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
