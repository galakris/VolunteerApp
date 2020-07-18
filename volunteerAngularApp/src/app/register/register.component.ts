import { Role } from './../_models/role';
import { AuthenticationService } from './../_services/authentication.service';
import { Router } from '@angular/router';
import { first, last } from 'rxjs/operators';
import { User } from '@/_models/user';
import { UserService } from '@/_services';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, OnChanges, SimpleChanges, DoCheck } from '@angular/core';
import * as mapboxgl from 'mapbox-gl';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit, DoCheck {

  registerForm: FormGroup;
  submitted = false;
  loading = false;
  longitude: number;
  latitude: number;
  coordinatesSet: boolean;
  roles = [{ value: Role.Volunteer, name: 'Wolontariusz' }, { value: Role.Needy, name: 'Potrzebujący' }];
  map: mapboxgl.Map;
  marker: mapboxgl.Marker;
  style = 'mapbox://styles/mapbox/streets-v11';

  constructor(
    private userService: UserService,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }
  ngDoCheck(): void {
    console.log('new lng: ' + this.longitude + ', lat: ' + this.latitude);
    if (this.coordinatesSet === false && this.longitude != null && this.latitude != null) {
      console.log('localization');
      this.coordinatesSet = true;
      this.map.setCenter([this.longitude, this.latitude]).setZoom(15);
    }
  }

  ngOnInit() {

    this.registerForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      role: new FormControl(this.roles[1].value),
      email: new FormControl('', [Validators.required, Validators.email]),
      phoneNumber: new FormControl('', [Validators.required, Validators.pattern('[0-9]{9}$')]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])
    });

    this.coordinatesSet = false;
    this.getCurrentLocation();

    (mapboxgl as any).accessToken = environment.mapbox.accessToken;
    this.map = new mapboxgl.Map({
      container: 'map',
      style: this.style,
      zoom: 6,
      center: [17.0712, 51.1534]
    });

    this.map.on('click', e => {
      console.log(e.lngLat.wrap());
      this.longitude = e.lngLat.lng;
      this.latitude = e.lngLat.lat;

      if (this.marker == null) {
        console.log('if null');
        this.marker = new mapboxgl.Marker()
          .setLngLat([e.lngLat.lng, e.lngLat.lat])
          //.setPopup(new mapboxgl.Popup({ offset: 25 }) // add popups
          // .setHTML('<h3>' + need.category + '</h3><p>' + need.description +
          //   '</p><button (click)="getNeed(' + need.description + ')">Pomagam</button>'))
          .addTo(this.map);
      } else {
        console.log('else');
        this.marker.setLngLat([e.lngLat.lng, e.lngLat.lat]);
      }

      // document.getElementById('info').innerHTML =
      // // e.point is the x, y coordinates of the mousemove event relative
      // // to the top-left corner of the map
      // JSON.stringify(e.point) +
      // '<br />' +
      // // e.lngLat is the longitude, latitude geographical position of the event
      // JSON.stringify(e.lngLat.wrap());
    });
  }

  onSubmit() {

    this.submitted = true;

    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;

    // it should works ? then this.user is useless
    // probably i should get coordinates from address here

    const user: User = ({
      id: null,
      email: this.registerForm.value.email,
      username: this.registerForm.value.email,
      password: this.registerForm.value.password,
      firstName: this.registerForm.value.firstName,
      lastName: this.registerForm.value.lastName,
      telephone: this.registerForm.value.phoneNumber,
      role: this.registerForm.value.role,
      latitude: typeof this.latitude === 'undefined' ? 0.0 : this.latitude,
      longitude: typeof this.longitude === 'undefined' ? 0.0 : this.longitude
    });

    console.log(user);

    this.userService.addUser(user)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/login']);
        },
        error => {
          this.loading = false;
        });
  }

  getCurrentLocation() {
    if (navigator.geolocation) {
      return navigator.geolocation.getCurrentPosition(position => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
      });
    } else {
      alert('Geolocation nie jest wspierane przez tą przeglądarke.');
      return null;
    }
  }

}
