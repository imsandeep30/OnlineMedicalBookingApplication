import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Register } from './register/register';
import { provideHttpClient } from '@angular/common/http';
@Component({
  selector: 'app-root',
  imports: [Register, RouterOutlet],
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
})
export class App {
  title = 'MediPick';
}
