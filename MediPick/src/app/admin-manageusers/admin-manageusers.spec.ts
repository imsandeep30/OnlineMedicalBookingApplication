import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminManageUsers } from './admin-manageusers';

describe('AdminManageUsers', () => {
  let component: AdminManageUsers;
  let fixture: ComponentFixture<AdminManageUsers>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminManageUsers]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminManageUsers);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
