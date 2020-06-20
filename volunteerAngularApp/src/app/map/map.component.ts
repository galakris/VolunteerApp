import { NeedState } from '@/_models/need-state';
import { environment } from '../../environments/environment';
import { Component, OnInit, ChangeDetectorRef, SystemJsNgModuleLoader } from '@angular/core';
import * as mapboxgl from 'mapbox-gl';
import { Need } from '@/_models/need';
import { NeedCategory } from '@/_models/need-category';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  map: mapboxgl.Map;
  style = 'mapbox://styles/mapbox/streets-v11';
  lat = 51.1105;
  lng = 17.0312;
  constructor() { }
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

    const needs: Need[] = [
      { id: 1, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Daj Pan chleba', lat: 51.1534, lng: 17.0712, distance: 5.75 },
      { id: 1, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Piwko i frytki', lat: 51.1105, lng: 17.0312, distance: 5.95 }
    ];

    // add markers to map
    needs.forEach((need) => {

      console.log("need: " + need.description + need.lng + need.lat);

      // create a HTML element for each feature
      // var el = document.createElement('div');
      // el.className = 'marker';

      // make a marker for each feature and add to the map
      var markers = new mapboxgl.Marker()
        .setLngLat([need.lng, need.lat])
        .setPopup(new mapboxgl.Popup({ offset: 25 }) // add popups
          .setHTML('<h3>' + need.category + '</h3><p>' + need.description + '</p>'))
        .addTo(this.map);
    });


    // var marker = new mapboxgl.Marker()
    //   .setLngLat([this.lng, this.lat])
    //   .addTo(this.map);

  }
}
