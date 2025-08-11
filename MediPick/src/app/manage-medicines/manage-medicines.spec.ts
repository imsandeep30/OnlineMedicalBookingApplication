import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageMedicines } from './manage-medicines';

describe('ManageMedicines', () => {
  let component: ManageMedicines;
  let fixture: ComponentFixture<ManageMedicines>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageMedicines]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageMedicines);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
