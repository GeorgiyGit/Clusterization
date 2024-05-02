import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocAlgorithmsOneClusterPageComponent } from './doc-algorithms-one-cluster-page.component';

describe('DocAlgorithmsOneClusterPageComponent', () => {
  let component: DocAlgorithmsOneClusterPageComponent;
  let fixture: ComponentFixture<DocAlgorithmsOneClusterPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocAlgorithmsOneClusterPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocAlgorithmsOneClusterPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
