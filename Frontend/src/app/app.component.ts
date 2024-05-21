import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';

@Component({
  selector: 'app-root',
  standalone: true,
  template: `
    <div class="container">
      <div class="header">
        <img src="assets/logo2.png" alt="Logo" class="logo" />
      </div>
      <router-outlet></router-outlet>
    </div>
  `,
  styleUrl: './app.component.css',
  imports : [HomeComponent, RouterModule]
})
export class AppComponent {
  title = 'PostOffice';
  constructor(private router: Router) {}
  onViewShipments() {
    this.router.navigate(['/shipments']);
  }
}
