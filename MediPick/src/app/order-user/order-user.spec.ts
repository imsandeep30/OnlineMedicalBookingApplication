import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderUser } from './order-user';

describe('OrderUser', () => {
  let component: OrderUser;
  let fixture: ComponentFixture<OrderUser>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderUser]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderUser);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
