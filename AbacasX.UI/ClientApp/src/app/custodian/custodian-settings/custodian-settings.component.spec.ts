import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianSettingsComponent } from './custodian-settings.component';

describe('CustodianSettingsComponent', () => {
  let component: CustodianSettingsComponent;
  let fixture: ComponentFixture<CustodianSettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
