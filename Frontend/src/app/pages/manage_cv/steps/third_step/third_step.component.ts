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
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatStepperModule } from '@angular/material/stepper';
import { CvApiService } from 'src/app/services/cv.api.service';
import { CV } from 'src/app/types/cv.model';
import { ExperienceInformation } from 'src/app/types/experience-information.model';

@Component({
  selector: 'third-step',
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
    MatListModule,
    MatIconModule,
  ],
  styleUrls: ['./third_step.component.css'],
  templateUrl: './third_step.component.html',
})
export class ThirdStepComponent implements OnInit {
  constructor(
    private _formBuilder: FormBuilder,
    private cvService: CvApiService
  ) {}

  @Input({ required: true }) currentCv: CV;
  @Output() onCompleted = new EventEmitter();
  @Output() onBack = new EventEmitter();

  ngOnInit(): void {
    var experiences = this.currentCv?.experiences ?? [];
    this.form = this._formBuilder.group({
      experiences: this._formBuilder.array(experiences.map(this.newGroup)),
    });
    if (this.experiences.length == 0) {
      this.addExperience();
    }
  }
  loading: boolean = false;
  form: FormGroup;

  get experiences() {
    return this.form.get('experiences') as FormArray;
  }

  newGroup(info?: ExperienceInformation): FormGroup {
    return this._formBuilder.group({
      companyName: [
        info?.companyName,
        [Validators.required, Validators.maxLength(20)],
      ],
      city: [info?.city],
      companyField: [info?.companyField],
      id: [info?.id],
    });
  }
  addExperience() {
    this.experiences.push(this.newGroup());
  }

  removeExperience(index: number) {
    this.experiences.removeAt(index);
  }

  back() {
    this.onBack.emit();
  }

  onSubmit() {
    this.loading = true;
    this.cvService
      .UpdateExperienceInformation(this.currentCv.id, this.experiences.value)
      .subscribe({
        next: (data) => {
          this.loading = false;
          this.onCompleted.emit();
        },
        error: (err) => {
          this.loading = false;
        },
      });
    this.onCompleted.emit();
  }


}
