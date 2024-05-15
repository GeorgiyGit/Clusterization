import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerYoutubeMainPageComponent } from './customer-youtube-main-page.component';

describe('CustomerYoutubeMainPageComponent', () => {
  let component: CustomerYoutubeMainPageComponent;
  let fixture: ComponentFixture<CustomerYoutubeMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerYoutubeMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerYoutubeMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
