import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianDepositHistoryComponent } from './custodian-deposit-history.component';

describe('CustodianDepositHistoryComponent', () => {
  let component: CustodianDepositHistoryComponent;
  let fixture: ComponentFixture<CustodianDepositHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianDepositHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianDepositHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
