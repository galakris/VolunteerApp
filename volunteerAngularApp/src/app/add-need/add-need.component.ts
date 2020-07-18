import { Router } from '@angular/router';
import { User } from '@/_models/user';
import { AuthenticationService } from '@/_services';
import { NeedService } from './../_services/need.service';
import { Need } from './../_models/need';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NeedCategory } from '@/_models/need-category';
import { NeedState } from '@/_models/need-state';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-add-need',
  templateUrl: './add-need.component.html',
  styleUrls: ['./add-need.component.css']
})
export class AddNeedComponent implements OnInit {

  needForm: FormGroup;
  submitted = false;
  loading = false;
  currentUser: User;
  categories = [
    { value: NeedCategory.Shopping, name: 'Zrobienie zakupów' },
    { value: NeedCategory.Medicine, name: 'Kupno leków' },
    { value: NeedCategory.Walk,     name: 'Wyprowadzenie zwierzęcia' },
    { value: NeedCategory.Other,    name: 'Inne' }
  ];

  constructor(
    private needSrevice: NeedService,
    private authenticationService: AuthenticationService,
    private router: Router
  ) {
    this.currentUser = this.authenticationService.currentUserValue;
  }

  ngOnInit() {

    this.needForm = new FormGroup({
      category: new FormControl(this.categories[0].value),
      description: new FormControl('', Validators.required),
      deadlineDate: new FormControl(new Date(new Date().getTime() + 86400000), Validators.required)
    });
  }

  onSubmit() {
    this.submitted = true;

    if (this.needForm.invalid) {
      return;
    }

    this.loading = true;

    const need: Need = ({
      id: null,
      name: this.currentUser.firstName + ' ' + this.currentUser.lastName,
      category: this.needForm.value.category,
      description: this.needForm.value.description,
      deadlineDate: new Date(this.needForm.value.deadlineDate), //current datetime + 1 day
      //deadlineDate: new Date(new Date().getTime() + 86400000), //current datetime + 1 day
      latitude: this.currentUser.latitude,
      longitude: this.currentUser.longitude,
      needStatus: NeedState.New,
      userId: this.currentUser.id
    });

    console.log(need);

    this.needSrevice.addNeed(need)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/my-needs']);
        },
        error => {
          this.loading = false;
          alert('Ups! Coś poszło nie tak...');
        });
  }
}
