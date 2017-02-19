import { Component, NgZone, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-cs-listener',
  templateUrl: './cs-listener.component.html',
  styleUrls: ['./cs-listener.component.css']
})
export class CsListenerComponent implements OnInit, OnDestroy {

    constructor(private ngZone: NgZone) { }

  ngOnInit() {
      window.my = window.my || {};
      window.my.namespace = window.my.namespace || {};
      window.my.namespace.publicFunc = this.publicFunc.bind(this);
  }

  ngOnDestroy() {
      window.my.namespace.publicFunc = null;
  }

  publicFunc(input: string) {
      this.ngZone.run(() => this.privateFunc(input));
  }

  privateFunc(input: string) {
      alert(input);
  }

}
