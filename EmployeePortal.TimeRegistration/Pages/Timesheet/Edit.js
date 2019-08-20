var timesheetFormComponent = new Vue({
    el: '#timesheets-form-component',
    data() {
        return {
            timesheet: null,
            isViewReady: false
        };
    },
    methods: {
        refreshData: function (id) {
            var self = this;
            this.isViewReady = false;

            axios.get('/api/timesheet/' + id)
                .then(function (response) {
                    self.timesheet = response.data;
                    self.isViewReady = true;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData(1);
    }
});