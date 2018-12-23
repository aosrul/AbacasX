import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianReportingComponent } from './custodian-reporting.component';

describe('CustodianReportingComponent', () => {
  let component: CustodianReportingComponent;
  let fixture: ComponentFixture<CustodianReportingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianReportingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianReportingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
