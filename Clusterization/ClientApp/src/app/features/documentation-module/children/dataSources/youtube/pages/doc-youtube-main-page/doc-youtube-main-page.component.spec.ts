import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeMainPageComponent } from './doc-youtube-main-page.component';

describe('DocYoutubeMainPageComponent', () => {
  let component: DocYoutubeMainPageComponent;
  let fixture: ComponentFixture<DocYoutubeMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
