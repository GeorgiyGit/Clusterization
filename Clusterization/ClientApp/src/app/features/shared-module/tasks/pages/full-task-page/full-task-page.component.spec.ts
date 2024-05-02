import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FullTaskPageComponent } from './full-task-page.component';

describe('FullTaskPageComponent', () => {
  let component: FullTaskPageComponent;
  let fixture: ComponentFixture<FullTaskPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FullTaskPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FullTaskPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
