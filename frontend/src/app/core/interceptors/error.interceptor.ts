import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { NotificationService } from '../services/notification.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    const notificationService = inject(NotificationService);

    return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
            let errorMessage = 'Bir hata oluştu';

            if (error.error instanceof ErrorEvent) {
                // Client-side error
                errorMessage = `Hata: ${error.error.message}`;
            } else {
                // Server-side error
                switch (error.status) {
                    case 400:
                        errorMessage = 'Geçersiz istek. Lütfen bilgileri kontrol edin.';
                        break;
                    case 401:
                        errorMessage = 'Yetkilendirme hatası.';
                        break;
                    case 403:
                        errorMessage = 'Bu işlem için yetkiniz yok.';
                        break;
                    case 404:
                        errorMessage = 'İstenen kaynak bulunamadı.';
                        break;
                    case 500:
                        errorMessage = 'Sunucu hatası. Lütfen daha sonra tekrar deneyin.';
                        break;
                    default:
                        errorMessage = `Sunucu hatası: ${error.status}`;
                }

                if (error.error?.message) {
                    errorMessage += ` - ${error.error.message}`;
                }
            }

            notificationService.error(errorMessage);
            return throwError(() => error);
        })
    );
};
