import { MapsAPILoader } from '@agm/core';
import { Component, ElementRef, EventEmitter, Input, NgZone, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { center, options } from './map.constants';
import { Options } from 'ngx-google-places-autocomplete/objects/options/options';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit, OnDestroy {
  zoom: number = 12;
  latitude: number = center.lat;
  longitude: number = center.lng;
  userAddress: string = '';
  map!: google.maps.Map;
  mapClickListener!: google.maps.MapsEventListener;
  options: Options = options as Options;
  coordinates!: { latitude: number; longitude: number; };

  @Input() flatLatitude!: number | undefined;
  @Input() flatLongitude!: number | undefined;

  @Output() coordinatesEmmiter: EventEmitter<{ latitude: number; longitude: number }> = new EventEmitter < { latitude: number; longitude: number } >()

  constructor(private mapsAPILoader: MapsAPILoader, private ngZone: NgZone) {
  }

  ngOnInit() {
    this.coordinates = { latitude: this.flatLatitude ?? 0, longitude: this.flatLongitude ?? 0 };
    this.mapsAPILoader.load().then(() => {});
  }

  public mapReadyHandler(map: google.maps.Map): void {
    this.map = map;
    this.mapClickListener = this.map.addListener('click', (e: google.maps.MouseEvent) => {
      this.ngZone.run(() => {
        console.log(e.latLng.lat(), e.latLng.lng());
        this.coordinates.latitude = e.latLng.lat();
        this.coordinates.longitude = e.latLng.lng();
        this.coordinatesEmmiter.emit(this.coordinates);
      });
    });
  }

  public ngOnDestroy(): void {
    if (this.mapClickListener) {
      this.mapClickListener.remove();
    }
  }

  handleAddressChange(address: any) {
    this.userAddress = address.formatted_address;
    this.coordinates.latitude = address.geometry.location.lat();
    this.coordinates.longitude = address.geometry.location.lng();
    this.coordinatesEmmiter.emit(this.coordinates);
  }
}
