import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { LandOffer, LandProperties } from '../../../shared/models/LandOfferModel';

@Component({
  selector: 'app-land-offer',
  templateUrl: './land-offer.component.html',
  styleUrls: ['./land-offer.component.css']
})
export class LandOfferComponent  {
  @Input() landOffer?: LandOffer;
  @Output() submitEvent: EventEmitter<LandProperties> = new EventEmitter<LandProperties>();
  @Output() nextEvent: EventEmitter<any> = new EventEmitter<any>();
  @Output() previousEvent: EventEmitter<any> = new EventEmitter<any>();

  landOfferForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.landOfferForm = fb.group({
      isSewers: new FormControl(false),
      isWater: new FormControl(false),
      isElectricity: new FormControl(false),
      isGas: new FormControl(false),
      road: new FormControl('') 
    }
    );
  }

  onNext(): void {
    this.nextEvent.emit();  }

  onSubmit(): void {
    console.log(this.landOfferForm.value)
    this.submitEvent.emit(this.landOfferForm.value);
  }

  onPrevious(): void {
    this.previousEvent.emit();
  }

}
