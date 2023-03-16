import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPostsTableComponent } from './view-posts-table.component';

describe('ViewPostsTableComponent', () => {
  let component: ViewPostsTableComponent;
  let fixture: ComponentFixture<ViewPostsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewPostsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewPostsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
