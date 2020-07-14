import { NeedState } from '@/_models/need-state';
import { environment } from '../../environments/environment';
import { Component, OnInit, ChangeDetectorRef, SystemJsNgModuleLoader, Input, OnChanges, SimpleChanges } from '@angular/core';
import * as mapboxgl from 'mapbox-gl';
import { Need } from '@/_models/need';
import { NeedCategory } from '@/_models/need-category';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit, OnChanges {
  map: mapboxgl.Map;
  style = 'mapbox://styles/mapbox/streets-v11';
  lat = 51.1105;
  lng = 17.0312;
  @Input() needs: Need[];

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    console.log('onChanges');
    for (let propName in changes) {
      // only run when property "data" changed
      if (propName === 'needs') {
        //  this line will update posts values
        this.needs = changes[propName].currentValue;

        this.addMarkers();
      }
    }
  }

  ngOnInit() {
    (mapboxgl as any).accessToken = environment.mapbox.accessToken;
    this.map = new mapboxgl.Map({
      container: 'map',
      style: this.style,
      zoom: 12,
      center: [this.lng, this.lat]
    });
    // Add map controls
    //this.map.addControl(new mapboxgl.NavigationControl());

    this.addMarkers();

  }

  addMarkers() {
    // add markers to map
    this.needs.forEach((need) => {

      console.log('need: ' + need.description + ' ' + need.longitude + ' ' + need.latitude);

      this.map.setCenter([need.longitude, need.latitude]);
      const markers = new mapboxgl.Marker()
        .setLngLat([need.longitude, need.latitude])
        .setPopup(new mapboxgl.Popup({ offset: 25 }) // add popups
          .setHTML('<h3>' + need.category + '</h3><p>' + need.description +
            '</p><button (click)="getNeed(' + need.description + ')">Pomagam</button>'))
        .addTo(this.map);
    });
  }

  getNeed(desc) {
    console.log(desc);
  }
}
