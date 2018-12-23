import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianAssetComponent } from './custodian-asset.component';

describe('CustodianAssetComponent', () => {
  let component: CustodianAssetComponent;
  let fixture: ComponentFixture<CustodianAssetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianAssetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianAssetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
