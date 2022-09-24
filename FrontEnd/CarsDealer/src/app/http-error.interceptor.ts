import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse} from '@angular/common/http';
import { EMPTY, Observable } from 'rxjs';
import {throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertService } from './_alert';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private alert: AlertService) {}
    intercept(request: HttpRequest<any>, next: HttpHandler) {
        return next.handle(request)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    const errorMessage = this.setError(error);
                    this.alert.error(errorMessage);
                    return throwError(error);
                })
            );
    }

    setError(error: HttpErrorResponse): string {
      let errorMessage = 'Unknown error occured';
      if(error.error instanceof ErrorEvent) {
          // Client side error
          errorMessage = error.error.message;
      } else {
          // server side error
          if (error.status != 0) {
              errorMessage = error.error.errorMessage;
          }
      }
      return errorMessage;
  }
}


