import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root',
})
export class AlertifyService {
  constructor() {}

  confirm(msg: string, okCallBack: () => any) {
    alertify.confirm(msg, (e: any) => {
      if (e) {
        okCallBack();
      }
      else {}
    });
  }

  succes(msg: string) {
    alertify.success(msg);
  }

  error(msg: string) {
    alertify.error(msg);
  }

  warning(msg: string) {
    alertify.warning(msg);
  }

  message(msg: string) {
    alertify.message(msg);
  }
}
