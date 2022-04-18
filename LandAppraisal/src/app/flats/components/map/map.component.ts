import { MapsAPILoader } from '@agm/core';
import { Component, ElementRef, EventEmitter, NgZone, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { center, options } from './map.constants';

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

  coordinates!: {
      latitude: number;
      longitude: number;
  }; 
 
  
  map!: google.maps.Map;
  mapClickListener!: google.maps.MapsEventListener;
  options = options;

  @Output() coordinatesEmmiter: EventEmitter<{ latitude: number; longitude: number }> = new EventEmitter < { latitude: number; longitude: number } >()


  handleAddressChange(address: any) {
    this.userAddress = address.formatted_address;
    this.coordinates.latitude = address.geometry.location.lat();
    this.coordinates.longitude = address.geometry.location.lng();
    this.coordinatesEmmiter.emit(this.coordinates);
  }

  @ViewChild('search')
  public searchElementRef!: ElementRef;


  constructor(
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone
  ) {
    this.coordinates = {
      latitude: 0,
      longitude: 0
    }
  }


  ngOnInit() {
    //load Places Autocomplete
    this.mapsAPILoader.load().then(() => {
    });
  }

  public mapReadyHandler(map: google.maps.Map): void {
    this.map = map;
    this.mapClickListener = this.map.addListener('click', (e: google.maps.MouseEvent) => {
      this.ngZone.run(() => {
        // Here we can get correct event
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
}
