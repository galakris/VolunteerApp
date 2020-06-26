import { NeedState } from '@/_models/need-state';
import { NeedCategory } from '@/_models/need-category';
import { Need } from '@/_models/need';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-needs',
  templateUrl: './my-needs.component.html',
  styleUrls: ['./my-needs.component.css']
})
export class MyNeedsComponent implements OnInit {

  needs: Need[];

  constructor() { }

  ngOnInit() {
    this.needs = [
      { id: 1, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Daj Pan chleba', lat: 51.1534, lng: 17.0712, distance: 5.75 },
      { id: 1, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Piwko i frytki', lat: 51.1105, lng: 17.0312, distance: 5.95 }
    ];
  }

}
