import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { faBuilding } from '@fortawesome/free-regular-svg-icons';
import { faCarAlt, faDoorOpen, faHammer, faHome, faMoneyBill, faPaintRoller } from '@fortawesome/free-solid-svg-icons';
import { Flat } from '../../../shared/models/FlatModel';

@Component({
  selector: 'app-flat-list-item',
  templateUrl: './flat-list-item.component.html',
  styleUrls: ['./flat-list-item.component.css']
})
export class FlatListItemComponent  {
  @Input() flat?: Flat;
  @Input() checkedList?: boolean = false;

  @Output() eventChecked: EventEmitter<{ isChecked: boolean, id: number}> = new EventEmitter();

  priceIcon: IconProp = faMoneyBill;
  areaIcon: IconProp = faHome;
  constructionIcon: IconProp = faHammer;
  floorIcon: IconProp = faBuilding;
  carIcon: IconProp = faCarAlt;
  stateIcon: IconProp = faPaintRoller;
  roomIcon: IconProp = faDoorOpen;


  constructor() { }

  onCheckBoxChange(): void {
    if (this.flat) {
      this.flat.isAccepted = !this.flat.isAccepted;
      this.eventChecked.emit({ isChecked: this.flat.isAccepted, id: this.flat.id });
    }
   
  }
}
