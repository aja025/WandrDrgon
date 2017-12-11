class DashboardService {
    constructor($resource) {
        this.dashboardResource = $resource("/api/Dashboard/:id");
    }

    getDashboard(id) {
        if (id == null) {
            return this.dashboardResource.get({ id: 0 });
        } else {
            return this.dashboardResource.get({ id: id });
        }
    }
}