import { NeedService } from './../_services/need.service';
import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { UserService, AuthenticationService } from '@/_services';
import { User } from '@/_models/user';
import { Need } from '@/_models/need';
import { NeedCategory } from '@/_models/need-category';
import { NeedState } from '@/_models/need-state';
import { ConditionalExpr } from '@angular/compiler';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  currentUser: User;
  // userFromApi: User;
  needs: Need[] = [];

  constructor(
    private userService: UserService,
    private authenticationService: AuthenticationService,
    private needService: NeedService
  ) {
    this.currentUser = this.authenticationService.currentUserValue;
  }

  ngOnInit() {
    this.getNeeds();
    console.log('needs length: ' + this.needs.length)
    for (const need of this.needs) {
      console.log(need.id + ' need ' + need.description);
    }
    // this.userService.getById(this.currentUser.id).pipe(first()).subscribe(user => {
    //   this.userFromApi = user;
    // });
    // console.log(this.currentUser);
    // this.needs = [
    //   { id: 1, name: this.currentUser.firstName + ' ' + this.currentUser.lastName, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Daj Pan chleba', deadlineDate: new Date(), latitude: 51.1534, longitude: 17.0712, distance: 5.75 },
    //   { id: 1, name: this.currentUser.firstName + ' ' + this.currentUser.lastName, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Piwko i frytki', deadlineDate: new Date(), latitude: 51.1105, longitude: 17.0312, distance: 5.95 }
    // ];

  }

  public getNeeds() {
    this.needService.getNeeds().subscribe(
      res => {
        console.log('res lenght: ' + res.length);
        this.needs = res;
      },
      err => {
        alert('Błąd podczas pobierania potrzeb.');
      }
    );
  }
}
