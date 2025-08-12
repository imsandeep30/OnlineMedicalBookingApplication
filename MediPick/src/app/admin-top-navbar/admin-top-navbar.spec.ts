import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTopNavbar } from './admin-top-navbar';

describe('AdminTopNavbar', () => {
  let component: AdminTopNavbar;
  let fixture: ComponentFixture<AdminTopNavbar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminTopNavbar]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminTopNavbar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
