import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadClustersFilePageComponent } from './load-clusters-file-page.component';

describe('LoadClustersFilePageComponent', () => {
  let component: LoadClustersFilePageComponent;
  let fixture: ComponentFixture<LoadClustersFilePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadClustersFilePageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoadClustersFilePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
