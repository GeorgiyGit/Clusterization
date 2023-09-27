import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeLoadAllCommentsPageComponent } from './youtube-load-all-comments-page.component';

describe('YoutubeLoadAllCommentsPageComponent', () => {
  let component: YoutubeLoadAllCommentsPageComponent;
  let fixture: ComponentFixture<YoutubeLoadAllCommentsPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [YoutubeLoadAllCommentsPageComponent]
    });
    fixture = TestBed.createComponent(YoutubeLoadAllCommentsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
