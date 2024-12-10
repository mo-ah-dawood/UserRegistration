import { MatTableDataSource } from '@angular/material/table';
import { LoadCvsResult } from 'src/app/services/cv.api.service';
import { CV } from 'src/app/types/cv.model';

export class CVDataSource extends MatTableDataSource<CV> {
  constructor() {
    super([]);
  }
  private _total = 0;
  private _loading = false;
  private _deleting: number[] = [];

  public isLoading(): boolean {
    return this._loading;
  }

  public total() {
    return this._total;
  }
  public length() {
    return this.data.length;
  }
  public hasNoData() {
    return this._total == 0 && !this._loading;
  }

  public add(data: LoadCvsResult) {
    this._total = data.total;
    this._loading = false;
    this.data = [...this.data, ...data.data];
  }

  public set(data: LoadCvsResult) {
    this._total = data.total;
    this._loading = false;
    this.data = data.data;
  }
  public beginRequest() {
    this._loading = true;
    this._updateChangeSubscription();
  }
  public error(error: any) {
    this._loading = false;
    this._updateChangeSubscription();
  }

  public setDeleting(id: number, value: boolean) {
    if (value && !this._deleting.includes(id)) {
      this._deleting.push(id);
      this._updateChangeSubscription();
    } else if (!value && this._deleting.includes(id)) {
      this._deleting = this._deleting.filter((x) => x != id);
      this._updateChangeSubscription();
    }
  }

  public isDeleting(id: number): boolean {
    return this._deleting.includes(id);
  }
}
