import { Component, ViewChild } from '@angular/core';
import {
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { CommonModule } from '@angular/common';
import {
  MatStep,
  MatStepper,
  MatStepperModule,
} from '@angular/material/stepper';
import { FirstStepComponent } from '../steps/first-step/first-step.component';
import { SecondStepComponent } from '../steps/second_step/second_step.component';
import { ThirdStepComponent } from '../steps/third_step/third_step.component';
import { CV } from 'src/app/types/cv.model';

interface Food {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-forms',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatCheckboxModule,
    CommonModule,
    MatStepperModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FirstStepComponent,
    SecondStepComponent,
    ThirdStepComponent,
  ],
  templateUrl: './create_cv.component.html',
})
export class CreateCVComponent {
  currentCv: CV;

  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('step1') step1: MatStep;
  @ViewChild('step2') step2: MatStep;
  @ViewChild('step3') step3: MatStep;

  get editable() {
    return this.currentCv != null;
  }

  firstStepCompleted(cv: CV) {
    this.currentCv = cv;
    this.step1.completed = true;
    this.stepper.next();
  }

  secondStepCompleted() {
    this.step2.completed = true;
    this.stepper.next();
  }

  thirdStepCompleted() {
    this.step3.completed = true;
    this.stepper.next();
  }
  back() {
    this.stepper.previous();
  }
}
