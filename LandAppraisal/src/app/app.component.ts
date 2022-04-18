import { DOCUMENT } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { API_KEY } from './secret';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FlatAppraisal';
  API_KEY = API_KEY;

  public constructor(
    @Inject(DOCUMENT) private doc: any
  ) { }

  ngOnInit() {
    this.setMapScript();
  }

  private setMapScript(): void {
    const s = this.doc.createElement('script');
    s.type = 'text/javascript';
    s.src = 'https://maps.googleapis.com/maps/api/js?key=' + API_KEY + '&libraries=places&language=en';
    const head = this.doc.getElementsByTagName('head')[0];
    head.appendChild(s);
  }
}
