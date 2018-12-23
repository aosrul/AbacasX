import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustodianDefinitionComponent } from './custodian-definition.component';

describe('CustodianDefinitionComponent', () => {
  let component: CustodianDefinitionComponent;
  let fixture: ComponentFixture<CustodianDefinitionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustodianDefinitionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustodianDefinitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
