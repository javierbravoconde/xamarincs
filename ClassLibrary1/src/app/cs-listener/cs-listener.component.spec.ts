/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CsListenerComponent } from './cs-listener.component';

describe('CsListenerComponent', () => {
  let component: CsListenerComponent;
  let fixture: ComponentFixture<CsListenerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CsListenerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CsListenerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
