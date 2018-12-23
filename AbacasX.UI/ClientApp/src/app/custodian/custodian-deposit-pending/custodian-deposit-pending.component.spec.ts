import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianDepositPendingComponent } from './custodian-deposit-pending.component';

describe('CustodianDepositPendingComponent', () => {
  let component: CustodianDepositPendingComponent;
  let fixture: ComponentFixture<CustodianDepositPendingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianDepositPendingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianDepositPendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
