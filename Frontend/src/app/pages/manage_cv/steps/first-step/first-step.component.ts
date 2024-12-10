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
  selector: 'first-step',
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
  templateUrl: './first-step.component.html',
})
export class FirstStepComponent implements OnInit {
  constructor(
    private _formBuilder: FormBuilder,
    private cvService: CvApiService
  ) {}

  @Input() currentCv: CV;
  @Output() onCompleted = new EventEmitter<CV>();

  ngOnInit(): void {
    console.log(this.currentCv);
    this.form = this._formBuilder.group({
      name: [this.currentCv?.name, Validators.required],
    });
  }

  loading: boolean = false;
  form: FormGroup;

  onSubmit() {
    if (
      this.currentCv != null &&
      this.form.get('name')?.value == this.currentCv.name
    ) {
      this.onCompleted.emit(this.currentCv);
      return;
    }
    this.loading = true;
    var observable = this.currentCv
      ? this.cvService.UpdateCv({ ...this.form.value, id: this.currentCv.id })
      : this.cvService.AddCv(this.form.value);
    observable.subscribe({
      next: (data) => {
        this.loading = false;
        this.currentCv = data;
        this.onCompleted.emit(data);
      },
      error: (err) => {
        this.loading = false;
      },
    });
  }
}
