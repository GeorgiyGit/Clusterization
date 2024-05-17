import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterDataObjectCardComponent } from './cluster-data-object-card.component';

describe('ClusterDataObjectCardComponent', () => {
  let component: ClusterDataObjectCardComponent;
  let fixture: ComponentFixture<ClusterDataObjectCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClusterDataObjectCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClusterDataObjectCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
