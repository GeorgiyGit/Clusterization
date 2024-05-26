import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalPacksTasksPageComponent } from './external-packs-tasks-page.component';

describe('ExternalPacksTasksPageComponent', () => {
  let component: ExternalPacksTasksPageComponent;
  let fixture: ComponentFixture<ExternalPacksTasksPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExternalPacksTasksPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExternalPacksTasksPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
