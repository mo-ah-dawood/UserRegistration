import { CommonModule } from '@angular/common';
import { Component, signal, ViewChild } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MaterialModule } from 'src/app/material.module';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { CvApiService } from 'src/app/services/cv.api.service';
import { CV } from 'src/app/types/cv.model';
import { TableVirtualScrollModule } from 'ng-table-virtual-scroll';
import { CVDataSource } from './data_source';
import { Router } from '@angular/router';

@Component({
  selector: 'cv_list',
  standalone: true,
  imports: [
    MatTableModule,
    CommonModule,
    MatCardModule,
    MaterialModule,
    MatIconModule,
    MatMenuModule,
    MatButtonModule,
    TableVirtualScrollModule,
  ],
  templateUrl: './cv_list.component.html',
})
export class CvListPage {
  constructor(private cvService: CvApiService, private router: Router) {
    this.loadData();
  }
  displayedColumns: string[] = [
    'id',
    'name',
    'emailAddress',
    'mobileNumber',
    'actions',
  ];
  dataSource = new CVDataSource();

  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  loadData(skip: number = 0, query?: string) {
    this.dataSource.beginRequest();
    this.cvService
      .LoadCvs(skip, this.paginator?.pageSize ?? 20, query)
      .subscribe({
        next:
          skip > 0
            ? this.dataSource.add.bind(this.dataSource)
            : this.dataSource.set.bind(this.dataSource),
        error: this.dataSource.error.bind(this.dataSource),
      });
  }

  paginate(event: PageEvent) {
    console.log((event.pageIndex + 1) * event.pageSize);
    if ((event.pageIndex + 1) * event.pageSize >= event.length) {
      this.loadData(event.length);
    }
  }

  applyFilter(event: Event) {
    this.loadData(0, (event.target as HTMLInputElement).value);
    this.paginator.firstPage();
  }

  create() {
    this.router.navigate(['/dashboard/create_cv']);
  }

  edit(item: CV) {
    this.router.navigate([`/dashboard/edit_cv/${item.id}`]);
  }
  delete(item: CV) {
    this.dataSource.setDeleting(item.id, true);
    this.cvService.DeleteCv(item.id).subscribe({
      next: () => {
        this.dataSource.data = this.dataSource.data.filter(
          (x) => x.id != item.id
        );
        this.dataSource.setDeleting(item.id, false);
      },
      error: () => {
        this.dataSource.setDeleting(item.id, false);
      },
    });
  }
}
