import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { UserService, AuthenticationService } from '@/_services';
import { User } from '@/_models/user';
import { Need } from '@/_models/need';
import { NeedCategory } from '@/_models/need-category';
import { NeedState } from '@/_models/need-state';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  currentUser: User;
  // userFromApi: User;
  needs: Need[];

  constructor(
    private userService: UserService,
    private authenticationService: AuthenticationService
  ) {
    this.currentUser = this.authenticationService.currentUserValue;
  }

  ngOnInit() {
    // this.userService.getById(this.currentUser.id).pipe(first()).subscribe(user => {
    //   this.userFromApi = user;
    // });
    console.log(this.currentUser);
    this.needs = [
      { id: 1, name: this.currentUser.firstName + ' ' + this.currentUser.lastName, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Daj Pan chleba', deadlineDate: new Date(), lat: 51.1534, lng: 17.0712, distance: 5.75 },
      { id: 1, name: this.currentUser.firstName + ' ' + this.currentUser.lastName, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Piwko i frytki', deadlineDate: new Date(), lat: 51.1105, lng: 17.0312, distance: 5.95 }
    ];

  }
}
