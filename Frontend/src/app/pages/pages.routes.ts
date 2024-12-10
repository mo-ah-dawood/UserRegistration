import { Routes } from '@angular/router';
import { CvListPage } from './cv_list/cv_list.component';
import { CreateCVComponent } from './manage_cv/create/create_cv.component';
import { EditCVComponent } from './manage_cv/edit/edit_cv.component';

export const PagesRoutes: Routes = [
  {
    path: '',
    component: CvListPage,
  },
  {
    path: 'create_cv',
    component: CreateCVComponent,
  },
  {
    path: 'edit_cv/:id',
    component: EditCVComponent,
  },
];
