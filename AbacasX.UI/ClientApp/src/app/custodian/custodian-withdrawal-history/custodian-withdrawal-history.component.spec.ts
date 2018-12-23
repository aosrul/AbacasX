import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianWithdrawalHistoryComponent } from './custodian-withdrawal-history.component';

describe('CustodianWithdrawalHistoryComponent', () => {
  let component: CustodianWithdrawalHistoryComponent;
  let fixture: ComponentFixture<CustodianWithdrawalHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianWithdrawalHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianWithdrawalHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
