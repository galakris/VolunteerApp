import { Role } from './../_models/role';
import { AuthenticationService } from './../_services/authentication.service';
import { Router } from '@angular/router';
import { first, last } from 'rxjs/operators';
import { User } from '@/_models/user';
import { UserService } from '@/_services';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  submitted = false;
  loading = false;
  longitude: string;
  latitude: string;
  roles = [{ value: Role.Volunteer, name: 'Wolontariusz' }, { value: Role.Needy, name: 'Potrzebujący' }];

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

  ngOnInit() {

    this.registerForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      role: new FormControl(this.roles[1].value),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])
    });

    this.getCurrentLocation();
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
      username: this.registerForm.value.email,
      password: this.registerForm.value.password,
      firstName: this.registerForm.value.firstName,
      lastName: this.registerForm.value.lastName,
      role: this.registerForm.value.role,
      latitude: typeof this.latitude === 'undefined' ? '0' : this.latitude,
      longitude: typeof this.longitude === 'undefined' ? '0' : this.longitude
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
        this.latitude = position.coords.latitude.toFixed(4);
        this.longitude = position.coords.longitude.toFixed(4);
      });
    } else {
      alert('Geolocation nie jest wspierane przez tą przeglądarke.');
      return null;
    }
  }

}
