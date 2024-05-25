import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFastClusteringFullComponent } from './add-fast-clustering-full.component';

describe('AddFastClusteringFullComponent', () => {
  let component: AddFastClusteringFullComponent;
  let fixture: ComponentFixture<AddFastClusteringFullComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddFastClusteringFullComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddFastClusteringFullComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
