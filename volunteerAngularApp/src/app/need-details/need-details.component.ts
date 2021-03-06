import { AuthenticationService } from './../_services/authentication.service';
import { User } from './../_models/user';
import { Need } from './../_models/need';
import { Component, OnInit, Input } from '@angular/core';
import { NeedService } from '@/_services';
import { NeedCategory } from '@/_models/need-category';

@Component({
  selector: 'app-need-details',
  templateUrl: './need-details.component.html',
  styleUrls: ['./need-details.component.css']
})
export class NeedDetailsComponent implements OnInit {

  @Input() need: Need;
  currentUser: User;
  categories = [
    { value: NeedCategory.Shopping, name: 'Zrobienie zakupów' },
    { value: NeedCategory.Medicine, name: 'Kupno leków' },
    { value: NeedCategory.Walk,     name: 'Wyprowadzenie zwierzęcia' },
    { value: NeedCategory.Other,    name: 'Inne' }
  ];

  constructor(
    private authenticationService: AuthenticationService,
    private needService: NeedService
  ) {
    this.currentUser = this.authenticationService.currentUserValue;
  }

  ngOnInit() {
  }

  assignNeed(need: Need) {
    console.log('Assign clisked, id: ' + need.id);
    this.needService.assignNeed(need).subscribe(
      res => {
        alert('Przypisałeś się! :)');
      },
      err => {
        alert('Ups! Coś poszło nie tak');
      }
    );
  }

}
