import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedCatlouge } from './med-catlouge';

describe('MedCatlouge', () => {
  let component: MedCatlouge;
  let fixture: ComponentFixture<MedCatlouge>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedCatlouge]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedCatlouge);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
