import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderBoth } from './order-both';

describe('OrderBoth', () => {
  let component: OrderBoth;
  let fixture: ComponentFixture<OrderBoth>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderBoth]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderBoth);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
