import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustHome } from './cust-home';

describe('CustHome', () => {
  let component: CustHome;
  let fixture: ComponentFixture<CustHome>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustHome]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustHome);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
