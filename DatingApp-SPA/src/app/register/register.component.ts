import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();

  model: any = {};

  constructor(private authServise: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authServise.register(this.model).subscribe(() => {
      console.log('register succesful');
    }, error => {
      console.log(error);
    });
  }

  cancel() {
    this.cancelRegister.emit();
  }
}
