var timesheetListComponent = new Vue({
    el: '#timesheets-list-component',
    data() {
        return {
            timesheets: [],
            isViewReady: false
        };
    },
    methods: {
        refreshData: function () {
            var self = this;
            this.isViewReady = false;

            axios.get('/api/timesheet/')
                .then(function (response) {
                    self.timesheets = response.data;
                    self.isViewReady = true;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData();
    }
});