import { RouterModule, Routes } from "@angular/router";
import { AdminGuard } from "src/app/services/admin.guard";
import { AuthGuardService } from "src/app/services/auth-guard.service";
import { AdminComponent } from "./admin/admin.component";

const routes: Routes =[
    {
        path: 'admin', component: AdminComponent, canActivate: [AuthGuardService, AdminGuard]
    },
]

export const AdminRoutingModule = RouterModule.forChild(routes);