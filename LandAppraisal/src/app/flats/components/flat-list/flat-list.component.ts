import { Component, Input, OnInit} from '@angular/core';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { faBuilding, faCarAlt, faDoorOpen, faHammer, faHome, faMoneyBill, faPaintRoller } from '@fortawesome/free-solid-svg-icons';
import { Flat } from '../../../shared/models/FlatModel';

@Component({
  selector: 'app-flat-list',
  templateUrl: './flat-list.component.html',
  styleUrls: ['./flat-list.component.css']
})
export class FlatListComponent implements OnInit {
  @Input() flats: Flat[] = [];
  @Input() checkedList?: boolean = false;

  displayedColumns = ['area', 'price', 'roomNumber', 'floor', 'constructionYear', 'car', 'other', 'market','state']

  priceIcon: IconProp = faMoneyBill;
  areaIcon: IconProp = faHome;
  constructionIcon: IconProp = faHammer;
  floorIcon: IconProp = faBuilding;
  carIcon: IconProp = faCarAlt;
  stateIcon: IconProp = faPaintRoller;
  roomIcon: IconProp = faDoorOpen;

  constructor() {

  }
  ngOnInit(): void {
    if (this.checkedList) {
      this.displayedColumns.unshift('isAccepted');
    }
  }

  onSelectionChanged($event: MatCheckboxChange, element: Flat): void {
    element.isAccepted = $event.checked;
  }
}
