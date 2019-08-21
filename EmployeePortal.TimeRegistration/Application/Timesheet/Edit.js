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
            this.getTimeSheet(id);
        },
        getTimeSheet: function (timesheetid) {
            var self = this;
            this.isViewReady = false;
            axios.get('/api/timesheet/' + timesheetid)
                .then(function (response) {
                    self.timesheet = response.data;
                    self.isViewReady = true;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        },
        addHourline: function () {
            var hl = { id: 0, timesheetid: 1, dayofmonth: 1, hours: 0, description: "" }
            this.timesheet.hourLines.push(hl);
        },
        saveHourlines: function () {
            var self = this;
            axios.post('/api/timesheet/', this.timesheet)
                .then(function (response) {
                    self.timesheet = response.data;
                    self.refreshData(document.getElementById("timesheetid").value);
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        },
        deleteHourline: function (hourlineid) {
            var self = this;
            axios.delete('/api/hourline/' + hourlineid)
                .then(function (response) {
                    self.refreshData(document.getElementById("timesheetid").value);
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    mounted: function () {
        this.refreshData(document.getElementById("timesheetid").value);
    }
});