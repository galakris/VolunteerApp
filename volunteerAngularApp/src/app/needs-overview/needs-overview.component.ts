import { Component, OnInit } from '@angular/core';
import { User } from '@/_models/user';
import { Need } from '@/_models/need';
import { AuthenticationService, NeedService } from '@/_services';

@Component({
  selector: 'app-needs-overview',
  templateUrl: './needs-overview.component.html',
  styleUrls: ['./needs-overview.component.css']
})
export class NeedsOverviewComponent implements OnInit {

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
