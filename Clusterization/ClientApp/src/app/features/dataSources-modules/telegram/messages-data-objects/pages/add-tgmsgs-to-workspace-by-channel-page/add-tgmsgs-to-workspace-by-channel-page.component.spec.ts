import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTGMsgsToWorkspaceByChannelPageComponent } from './add-tgmsgs-to-workspace-by-channel-page.component';

describe('AddTGMsgsToWorkspaceByChannelPageComponent', () => {
  let component: AddTGMsgsToWorkspaceByChannelPageComponent;
  let fixture: ComponentFixture<AddTGMsgsToWorkspaceByChannelPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddTGMsgsToWorkspaceByChannelPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddTGMsgsToWorkspaceByChannelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
