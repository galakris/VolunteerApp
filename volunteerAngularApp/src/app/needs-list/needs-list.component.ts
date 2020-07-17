import { NeedService } from './../_services/need.service';
import { Component, OnInit, Input } from '@angular/core';
//import { Component, OnInit, ChangeDetectorRef, SystemJsNgModuleLoader, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Need } from '@/_models/need';


import { User } from '@/_models/user';
import { NeedState } from '@/_models/need-state';
import { NeedCategory } from '@/_models/need-category';
import { AuthenticationService } from '@/_services';
import { Role } from '@/_models/role';

@Component({
  selector: 'app-needs-list',
  templateUrl: './needs-list.component.html',
  styleUrls: ['./needs-list.component.css']
})
export class NeedsListComponent implements OnInit {

  @Input() needs: Need[];
  @Input() parent: string;
  columns: string[];

  currentUser: User;
  selectedNeed: Need;
  hideList: boolean;

  constructor(
    private authenticationService: AuthenticationService,
    private needService: NeedService
  ) {
    this.currentUser = this.authenticationService.currentUserValue;
  }

  ngOnInit() {
    //this.needs = [
    //  { id: 1, name: this.currentUser.firstName + ' ' + this.currentUser.lastName, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Daj Pan chleba', deadlineDate: new Date(), latitude: 51.1534, longitude: 17.0712, distance: 5.75 },
    //  { id: 2, name: this.currentUser.firstName + ' ' + this.currentUser.lastName, userId: 1, category: NeedCategory.Shopping, state: NeedState.New, description: 'Piwko i frytki', deadlineDate: new Date(), latitude: 51.1105, longitude: 17.0312, distance: 5.95 }
    //];

    this.hideList = false;
    // different columns for different page
    this.columns = [];
    console.log('parent: ' + parent);
    if (this.parent === 'needs-overview') {
      this.columns.push('Status');
      this.columns.push('Imie i Nazwisko');
      this.columns.push('Kategoria');
      this.columns.push('Odleglość');
      // details/complete icon
      this.columns.push('');
    } else if (this.parent === 'home') {
      this.columns.push('Imie i Nazwisko');
      this.columns.push('Kategoria');
      this.columns.push('Odleglość');
      // details icon
      this.columns.push('');
    } else if (this.parent === 'my-needs') {
      // status icon
      this.columns.push('Status');
      this.columns.push('Kategoria');
      this.columns.push('Odleglość');
      this.columns.push('Opis');
      // details/delete icon
      this.columns.push('');
    }
  }

  onSelect(need: Need) {
    this.selectedNeed = need;
    this.hideList = true;
  }

  showList() {
    this.selectedNeed = null;
    this.hideList = false;
  }

  completeTask(need: Need) {
    console.log('complete need: ' + need.id);
    this.needService.finishNeed(need).subscribe(
      res => {
        alert('Zakończono pomyślnie :)');
      },
      err => {
        alert('Ups! Coś poszło nie tak');
      }
    );
  }

  deleteNeed(need: Need) {
    this.needService.deleteNeed(need).subscribe(
      res => {
        this.needs = this.needs.filter(n => need !== n);
        alert('Usunięto.');
      },
      err => {
        alert('Ups! Coś poszło nie tak :(');
      }
    );
    console.log('delete need: ' + need.id);
  }
}
