import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeHeaderComponent } from './youtube-header.component';

describe('YoutubeHeaderComponent', () => {
  let component: YoutubeHeaderComponent;
  let fixture: ComponentFixture<YoutubeHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YoutubeHeaderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(YoutubeHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
