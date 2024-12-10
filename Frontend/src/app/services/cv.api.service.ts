import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CV } from '../types/cv.model';
import { GenericResult } from '../types/generic-result.model';
import { PersonalInformation } from '../types/personal-information.model';
import { ExperienceInformation } from '../types/experience-information.model';

@Injectable({
  providedIn: 'root',
})
export class CvApiService {
  constructor(private http: HttpClient) {}

  LoadCvs(
    skip: number,
    pageSize: number,
    query?: string
  ): Observable<LoadCvsResult> {
    return this.http
      .get<GenericResult<CV[]>>('/api/cv/LoadCvs', {
        params: {
          Skip: skip,
          PageSize: pageSize,
          Query: query ?? '',
        },
      })
      .pipe(
        map((result) => {
          return {
            data: result.data,
            total: result.total,
          };
        })
      );
  }

  LoadCv(id: number): Observable<CV> {
    return this.http
      .get<GenericResult<CV>>(`/api/cv/GetCv/${id}`, {})
      .pipe(map((result) => result.data));
  }

  AddCv(body: CV): Observable<CV> {
    return this.http
      .post<GenericResult<CV>>('/api/cv/add', body)
      .pipe(map((result) => result.data));
  }

  DeleteCv(id: number): Observable<Object> {
    return this.http.delete(`/api/cv/delete/${id}`);
  }

  UpdateCv(body: CV): Observable<CV> {
    return this.http
      .post<GenericResult<CV>>('/api/cv/update', body)
      .pipe(map((result) => result.data));
  }

  UpdatePersonalInformation(
    cvId: number,
    body: PersonalInformation
  ): Observable<PersonalInformation> {
    return this.http
      .post<GenericResult<PersonalInformation>>(
        `/api/cv/UpdatePersonalInformation/${cvId}`,
        body
      )
      .pipe(map((result) => result.data));
  }

  UpdateExperienceInformation(
    cvId: number,
    body: ExperienceInformation[]
  ): Observable<ExperienceInformation> {
    return this.http
      .post<GenericResult<ExperienceInformation>>(
        `/api/cv/UpdateExperienceInformation/${cvId}`,
        body
      )
      .pipe(map((result) => result.data));
  }
}

export class LoadCvsResult {
  data: CV[];
  total: number;
}
