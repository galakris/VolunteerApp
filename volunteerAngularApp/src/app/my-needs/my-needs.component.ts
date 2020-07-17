import { NeedService } from './../_services/need.service';
import { User } from '@/_models/user';
import { NeedState } from '@/_models/need-state';
import { NeedCategory } from '@/_models/need-category';
import { Need } from '@/_models/need';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@/_services';

@Component({
  selector: 'app-my-needs',
  templateUrl: './my-needs.component.html',
  styleUrls: ['./my-needs.component.css']
})
export class MyNeedsComponent implements OnInit {

  currentUser: User;
  needs: Need[];

  constructor(
    private authenticationService: AuthenticationService,
    private needService: NeedService
  ) {
    this.currentUser = this.authenticationService.currentUserValue;
  }

  // ngOnInit() {
  //   this.needs = [
  //     { id: 1, name: this.currentUser.firstName + ' ' + this.currentUser.lastName, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Daj Pan chleba', deadlineDate: new Date(), latitude: 51.1534, longitude: 17.0712, distance: 5.75 },
  //     { id: 2, name: this.currentUser.firstName + ' ' + this.currentUser.lastName, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Piwko i frytki', deadlineDate: new Date(), latitude: 51.1105, longitude: 17.0312, distance: 5.95 }
  //   ];
  // }

  ngOnInit() {
    this.getMyNeeds();
  }

  public getMyNeeds() {
    this.needService.getMyNeeds().subscribe(
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
