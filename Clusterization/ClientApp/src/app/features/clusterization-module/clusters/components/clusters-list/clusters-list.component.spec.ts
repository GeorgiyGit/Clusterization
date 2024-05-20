import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClustersListComponent } from './clusters-list.component';

describe('ClustersListComponent', () => {
  let component: ClustersListComponent;
  let fixture: ComponentFixture<ClustersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClustersListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClustersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
