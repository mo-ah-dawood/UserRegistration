import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-branding',
  standalone: true,
  imports: [RouterModule],
  template: `
    <div class="branding">
      <a [routerLink]="['/']"> CvSystem </a>
    </div>
  `,
})
export class BrandingComponent {
  constructor() {}
}
