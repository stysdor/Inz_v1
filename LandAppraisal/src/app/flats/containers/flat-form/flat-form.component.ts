import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
  flat: FlatToPrediction | null;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroup: FormGroup;
  stepperOrientation$: Observable<StepperOrientation>;
  error: string = '';
  states: string[] = ['do remontu', 'do odświerzenia', 'do zamieszkania', 'deweloperski']


  constructor(private fb: FormBuilder, breakpointObserver: BreakpointObserver, private flatService: FlatService, private router: Router) {
    this.flat = this.router.getCurrentNavigation()?.extras?.state?.flat;
    this.stepperOrientation$ = breakpointObserver
      .observe('(min-width: 992px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));

    this.firstFormGroup = this.fb.group({
      n_latitude: new FormControl(this.flat?.n_latitude, Validators.required),
      e_longitude: new FormControl(this.flat?.e_longitude, Validators.required)
    });
    this.secondFormGroup = this.fb.group({
      area: new FormControl(this.flat?.area, Validators.required),
      roomNumber: new FormControl(this.flat?.roomNumber, Validators.required),
      floor: new FormControl(this.flat?.floor, Validators.required),
      floorInBuilding: new FormControl(this.flat?.floorInBuilding, Validators.required),
      constructionYear: new FormControl(this.flat?.constructionYear, Validators.required),
      market: new FormControl(this.flat?.market, Validators.required),
      state: new FormControl(),
    });
    this.thirdFormGroup = this.fb.group({
      isBalcony: new FormControl(this.flat?.isBalcony ?? false),
      isTarrace: new FormControl(this.flat?.isTarrace ?? false),
      isLoggia: new FormControl(this.flat?.isLoggia ?? false),
      isParkingSpace: new FormControl(this.flat?.isParkingSpace ?? false),
      isGarage: new FormControl(this.flat?.isGarage ?? false),
      isGarden: new FormControl(this.flat?.isGarden ?? false),
      isCellar: new FormControl(this.flat?.isCellar ?? false),
      isLift: new FormControl(this.flat?.isLift ?? false),
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

    this.flatService.getPricePrediction(flat).subscribe();
  }

  onReset(): void {
    this.flat = null;
  }

}
