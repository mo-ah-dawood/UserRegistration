import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { CvApiService } from 'src/app/services/cv.api.service';
import { CV } from 'src/app/types/cv.model';

@Component({
  selector: 'second-step',
  standalone: true,
  imports: [
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatStepperModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonModule,
  ],
  templateUrl: './second_step.component.html',
})
export class SecondStepComponent implements OnInit {
  constructor(
    private _formBuilder: FormBuilder,
    private cvService: CvApiService
  ) {}

  @Input({ required: true }) currentCv: CV;
  @Output() onCompleted = new EventEmitter();
  @Output() onBack = new EventEmitter();

  ngOnInit(): void {
    var info = this.currentCv?.personalInformation;
    this.form = this._formBuilder.group({
      fullName: [info?.fullName, Validators.required],
      cityName: [info?.cityName],
      mobileNumber: [
        info?.mobileNumber,
        [Validators.required, Validators.pattern('^01[0125]\\d{8}$')],
      ],
      emailAddress: [info?.emailAddress, Validators.email],
    });
  }

  loading: boolean = false;
  form: FormGroup;

  back() {
    this.onBack.emit();
  }

  onSubmit() {
    this.loading = true;
    this.cvService
      .UpdatePersonalInformation(this.currentCv.id, this.form.value)
      .subscribe({
        next: (data) => {
          this.loading = false;
          this.currentCv.personalInformation = data;
          this.onCompleted.emit();
        },
        error: (err) => {
          this.loading = false;
        },
      });
  }
}
