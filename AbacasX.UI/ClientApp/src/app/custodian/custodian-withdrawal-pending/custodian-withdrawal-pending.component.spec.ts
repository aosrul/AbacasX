import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianWithdrawalPendingComponent } from './custodian-withdrawal-pending.component';

describe('CustodianWithdrawalPendingComponent', () => {
  let component: CustodianWithdrawalPendingComponent;
  let fixture: ComponentFixture<CustodianWithdrawalPendingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianWithdrawalPendingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianWithdrawalPendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
