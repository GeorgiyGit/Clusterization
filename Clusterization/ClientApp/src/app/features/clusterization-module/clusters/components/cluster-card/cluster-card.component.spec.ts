import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterCardComponent } from './cluster-card.component';

describe('ClusterCardComponent', () => {
  let component: ClusterCardComponent;
  let fixture: ComponentFixture<ClusterCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClusterCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClusterCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
