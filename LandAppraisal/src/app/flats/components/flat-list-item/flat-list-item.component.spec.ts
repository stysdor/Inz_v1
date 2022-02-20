import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlatListItemComponent } from './flat-list-item.component';

describe('FlatListItemComponent', () => {
  let component: FlatListItemComponent;
  let fixture: ComponentFixture<FlatListItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlatListItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlatListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
