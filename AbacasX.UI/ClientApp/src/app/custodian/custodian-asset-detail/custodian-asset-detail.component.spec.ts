import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianAssetDetailComponent } from './custodian-asset-detail.component';

describe('CustodianAssetDetailComponent', () => {
  let component: CustodianAssetDetailComponent;
  let fixture: ComponentFixture<CustodianAssetDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianAssetDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianAssetDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
