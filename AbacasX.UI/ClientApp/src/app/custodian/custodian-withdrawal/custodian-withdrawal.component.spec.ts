import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianWithdrawalComponent } from './custodian-withdrawal.component';

describe('CustodianWithdrawalComponent', () => {
  let component: CustodianWithdrawalComponent;
  let fixture: ComponentFixture<CustodianWithdrawalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianWithdrawalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianWithdrawalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
