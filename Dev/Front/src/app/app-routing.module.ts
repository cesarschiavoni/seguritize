import { Routes } from '@angular/router';
import { RouterModule } from  '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuardService as AuthGuard } from './services/auth-guard.service';

const routes: Routes = [   
  { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) },
  { path: 'wallet', loadChildren: () => import('./wallets/wallets.module').then(m => m.WalletsModule) , canActivate: [AuthGuard] },
  { path: 'prici', loadChildren: () => import('./prici/prici.module').then(m => m.PriciModule) , canActivate: [AuthGuard] }, 
  { path: 'null', loadChildren: () => import('./home/home.module').then(m => m.HomeModule) },
  { path: '**', loadChildren: () => import('./home/home.module').then(m => m.HomeModule) },
  { path: '', loadChildren: () => import('./home/home.module').then(m => m.HomeModule) } 
]; 
  

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false })],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
