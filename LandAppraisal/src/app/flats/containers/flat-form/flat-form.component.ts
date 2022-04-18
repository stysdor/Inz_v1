import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FlatToPrediction } from '../../../shared/models/FlatModel';
import { FlatService } from '../../services/flat.service';

@Component({
  selector: 'app-flat-form',
  templateUrl: './flat-form.component.html',
  styleUrls: ['./flat-form.component.css']
})
export class FlatFormComponent  {
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroup: FormGroup;
  stepperOrientation$: Observable<StepperOrientation>;
  error: string = '';
  states: string[] = ['do remontu', 'do odświerzenia', 'do zamieszkania', 'deweloperski']


  constructor(private fb: FormBuilder, breakpointObserver: BreakpointObserver, private flatService: FlatService) {
    this.stepperOrientation$ = breakpointObserver
      .observe('(min-width: 992px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));

    this.firstFormGroup = this.fb.group({
      n_latitude: new FormControl('', Validators.required),
      e_longitude: new FormControl('', Validators.required)
    });
    this.secondFormGroup = this.fb.group({
      area: new FormControl('', Validators.required),
      roomNumber: new FormControl('', Validators.required),
      floor: new FormControl('', Validators.required),
      floorInBuilding: new FormControl('', Validators.required),
      constructionYear: new FormControl('', Validators.required),
      market: new FormControl('', Validators.required),
      state: new FormControl(''),
    });
    this.thirdFormGroup = this.fb.group({
      isBalcony: new FormControl(false),
      isTarrace: new FormControl(false),
      isLoggia: new FormControl(false),
      isParkingSpace: new FormControl(false),
      isGarage: new FormControl(false),
      isGarden: new FormControl(false),
      isCellar: new FormControl(false),
      isLift: new FormControl(false),
    });
  }

  onLocalizationChosen({ latitude, longitude }:{ latitude: number, longitude: number }): void {
    this.firstFormGroup.controls.n_latitude.setValue(latitude);
    this.firstFormGroup.controls.e_longitude.setValue(longitude);
  }

  onSubmit(): void {
    if (!this.firstFormGroup.valid || !this.secondFormGroup.valid || !this.thirdFormGroup.valid) {
      return;
    }
    const flat: FlatToPrediction = {
      area : this.secondFormGroup.controls.area.value,
      constructionYear : this.secondFormGroup.controls.constructionYear.value,
      e_longitude : this.firstFormGroup.controls.e_longitude.value,
      n_latitude : this.firstFormGroup.controls.n_latitude.value,
      floor : this.secondFormGroup.controls.floor.value,
      floorInBuilding: this.secondFormGroup.controls.floorInBuilding.value,
      roomNumber: this.secondFormGroup.controls.roomNumber.value,
      isBalcony : this.thirdFormGroup.controls.isBalcony.value,
      isCellar : this.thirdFormGroup.controls.isCellar.value,
      isGarage :  this.thirdFormGroup.controls.isGarage.value,
      isGarden : this.thirdFormGroup.controls.isGarden.value,
      isLift : this.thirdFormGroup.controls.isLift.value,
      isLoggia : this.thirdFormGroup.controls.isLoggia.value,
      isParkingSpace : this.thirdFormGroup.controls.isParkingSpace.value,
      isTarrace :  this.thirdFormGroup.controls.isTarrace.value,
      market : this.secondFormGroup.controls.market.value,
    }

    this.flatService.getPricePrediction(flat).subscribe((price) => {
      console.log(price);
    });
  }

}
