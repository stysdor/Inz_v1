import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { FlatService } from '../../../flats/services/flat.service';

@Component({
  selector: 'app-download',
  templateUrl: './download.component.html',
  styleUrls: ['./download.component.css']
})
export class DownloadComponent {
  arrowIcon: IconProp = faArrowRight;
  linkCount$?: Observable<number>;
  countForm: FormGroup;
  isDownload$?: Observable<boolean>;

  stepperOrientation$: Observable<StepperOrientation>;


  constructor(private flatService: FlatService, private fb: FormBuilder, breakpointObserver: BreakpointObserver) {
    this.stepperOrientation$ = breakpointObserver
      .observe('(min-width: 992px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));

    this.countForm = this.fb.group({
      count: new FormControl('')
    }
    );
  }

  downloadLinks(): void {
    this.linkCount$ = this.flatService.downloadFlatLinks().pipe(
      tap((response: number) => {
        this.countForm?.controls.count.setValue(response);
      }));
  }

  downloadFlats(): void {
    this.isDownload$ = this.flatService.downloadFlats(this.countForm.controls.count.value).pipe();
  }
}
