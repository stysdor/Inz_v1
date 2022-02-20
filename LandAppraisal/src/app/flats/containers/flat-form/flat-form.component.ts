import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-flat-form',
  templateUrl: './flat-form.component.html',
  styleUrls: ['./flat-form.component.css']
})
export class FlatFormComponent implements OnInit {
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroup: FormGroup;
  stepperOrientation$: Observable<StepperOrientation>;
  error: string = '';
  states: string[] = ['do remontu', 'do odświerzenia', 'do zamieszkania', 'deweloperski']

  constructor(private fb: FormBuilder, breakpointObserver: BreakpointObserver) {
    this.stepperOrientation$ = breakpointObserver
      .observe('(min-width: 992px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));

    this.firstFormGroup = this.fb.group({
      location: new FormControl('', Validators.required),
    });
    this.secondFormGroup = this.fb.group({
      area: new FormControl('', Validators.required),
      roomNumber: new FormControl(''),
      floor: new FormControl(''),
      floorInBuilding: new FormControl(''),
      market: new FormControl(''),
      state: new FormControl(''),
    });
    this.thirdFormGroup = this.fb.group({
      isBalcony: new FormControl(''),
      isTarrace: new FormControl(''),
      isLoggia: new FormControl(''),
      isParkingSpace: new FormControl(''),
      isGarage: new FormControl(''),
      isGarden: new FormControl(''),
    });
  }

  ngOnInit(): void {

  }

}
