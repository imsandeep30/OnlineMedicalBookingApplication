import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { Router, ActivatedRouteSnapshot } from '@angular/router';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const router = inject(Router);
  const token = localStorage.getItem('jwt');
  const userRole = localStorage.getItem('role');  // get stored role

  if (!token) {
    // Not logged in
    return router.createUrlTree(['/login']);
  }

  // Get required roles from route data
  const requiredRoles: string[] = route.data['roles'];

  if (requiredRoles && requiredRoles.length > 0) {
    if (!userRole || !requiredRoles.includes(userRole)) {
      // Role not authorized for this route
      // Redirect to "Access Denied" page or homepage
      return router.createUrlTree(['/access-denied']);
    }
  }

  // Authorized
  return true;
};
