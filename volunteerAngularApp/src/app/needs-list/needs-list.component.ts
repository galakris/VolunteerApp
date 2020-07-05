import { Component, OnInit, Input } from '@angular/core';
import { Need } from '@/_models/need';

@Component({
  selector: 'app-needs-list',
  templateUrl: './needs-list.component.html',
  styleUrls: ['./needs-list.component.css']
})
export class NeedsListComponent implements OnInit {

  @Input() needs: Need[];

  constructor() { }

  ngOnInit() {
  }

}
