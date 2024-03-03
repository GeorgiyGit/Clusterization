import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocAlgorithmsDbscanPageComponent } from './doc-algorithms-dbscan-page.component';

describe('DocAlgorithmsDbscanPageComponent', () => {
  let component: DocAlgorithmsDbscanPageComponent;
  let fixture: ComponentFixture<DocAlgorithmsDbscanPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocAlgorithmsDbscanPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocAlgorithmsDbscanPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
