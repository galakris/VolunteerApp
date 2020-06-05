import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NeedCategory } from '@/_models/need-category';

@Component({
  selector: 'app-add-need',
  templateUrl: './add-need.component.html',
  styleUrls: ['./add-need.component.css']
})
export class AddNeedComponent implements OnInit {

  needForm: FormGroup;
  submitted = false;
  loading = false;
  categories = [
    {name: NeedCategory.Shopping, value: 'Zrobienie zakupów'},
    {name: NeedCategory.Medicine, value: 'Kupno leków'},
    {name: NeedCategory.Walk, value: 'Wyprowadzenie zwierzęcia'},
    {name: NeedCategory.Other, value: 'Inne'}
  ];

  constructor() { }

  ngOnInit() {

    this.needForm = new FormGroup({
      category: new FormControl(this.categories[0].value),
      description: new FormControl('', Validators.required),
    });
  }

  onSubmit(){

  }
}
