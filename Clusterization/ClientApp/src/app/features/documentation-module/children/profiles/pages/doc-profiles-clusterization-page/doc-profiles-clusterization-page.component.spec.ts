import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocProfilesClusterizationPageComponent } from './doc-profiles-clusterization-page.component';

describe('DocProfilesClusterizationPageComponent', () => {
  let component: DocProfilesClusterizationPageComponent;
  let fixture: ComponentFixture<DocProfilesClusterizationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocProfilesClusterizationPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocProfilesClusterizationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
