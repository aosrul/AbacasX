import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianDepositComponent } from './custodian-deposit.component';

describe('CustodianDepositComponent', () => {
  let component: CustodianDepositComponent;
  let fixture: ComponentFixture<CustodianDepositComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianDepositComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianDepositComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
