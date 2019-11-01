var HomeController = function () {
    this.initialize = function () {
        initDateRangePicker();
        loadData();
        loadUserData();
    }



    function loadData(from, to) {

        $.ajax({
            type: "GET",
            url: "/Admin/Home/GetRevenue",
            data: {
                fromDate: from,
                toDate: to
            },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                initChart(response);

                tedu.stopLoading();

            },
            error: function (status) {
                tedu.notify('Có lỗi xảy ra', 'error');
                tedu.stopLoading();
            }
        });
    }

    function loadUserData(from, to) {
        $.ajax({
            type: "GET",
            url: "/Admin/Home/GetNewUser",
            data: {
                fromDate: from,
                toDate: to
            },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                initNewUserChart(response);

                tedu.stopLoading();

            },
            error: function (status) {
                tedu.notify('Có lỗi xảy ra khi tải dữ liệu người dùng', 'error');
                tedu.stopLoading();
            }
        });
    }


    function initChart(data) {
        var arrRevenue = [];

        $.each(data, function (i, item) {
            arrRevenue.push([new Date(item.Date).getTime(), item.Revenue]);
        });
        var chart_plot_02_settings = {
            grid: {
                show: true,
                aboveData: true,
                color: "#3f3f3f",
                //labelMargin: 10,
                axisMargin: 0,
                borderWidth: 0,
                borderColor: null,
                //minBorderMargin: 5,
                clickable: true,
                hoverable: true,
                autoHighlight: true
            },
            series: {
                lines: {
                    show: true,
                    fill: true,
                    lineWidth: 2,
                    steps: false
                },
                points: {
                    show: true,
                    radius: 2.5,
                    symbol: "circle",
                    lineWidth: 3.0
                }
            },
            shadowSize: 0,
            tooltip: true,
            yaxis: {
                min: 0
            },
            xaxis: {
                mode: 'time',
                tickDecimals: 0,
                minTickSize: [1, 'day'],
                timeformat: '%d/%m/%y',
            }
        };

        $("<div id='tooltip'></div>").css({
            position: "absolute",
            display: "none",
            border: "1px solid #fdd",
            padding: "2px",
            "background-color": "#fee",
            opacity: 0.80
        }).appendTo("body");

        $("#chart_plot_02").bind("plothover", function (event, pos, item) {

            if (!pos.x || !pos.y) {
                return;
            }

            if (item) {
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var date = moment(x).format('DD/MM');
                $("#tooltip").html(item.series.label + " ngày " + date + ": " + y)
                    .css({ top: item.pageY + 5, left: item.pageX + 5 })
                    .fadeIn(200);
            } else {
                $("#tooltip").hide();
            }
        });

        if ($("#chart_plot_02").length > 0) {

            if (arrRevenue.length > 0) {
                $.plot($("#chart_plot_02"),
                    [{
                        label: "Doanh thu",
                        data: arrRevenue,
                        lines: {
                            fillColor: "rgba(150, 202, 89, 0.12)"
                        },
                        points: {
                            fillColor: "#fff"
                        }
                    }], chart_plot_02_settings);
            }

            else {
                document.getElementById("chart_plot_02").innerHTML = "Không có dữ liệu hóa đơn!!";
            }
        }
    }
    function initNewUserChart(data) {
        var arrNewUser = [];

        $.each(data, function (i, item) {
            arrNewUser.push([new Date(item.Date).getTime(), item.TotalNewUser]);
        });
        var chart_plot_02_settings = {
            grid: {
                show: true,
                aboveData: true,
                color: "#3f3f3f",
                //labelMargin: 10,
                axisMargin: 0,
                borderWidth: 0,
                borderColor: null,
                //minBorderMargin: 5,
                clickable: true,
                hoverable: true,
                autoHighlight: true
            },
            series: {
                lines: {
                    show: true,
                    fill: true,
                    lineWidth: 2,
                    steps: false
                },
                points: {
                    show: true,
                    radius: 2.5,
                    symbol: "circle",
                    lineWidth: 3.0
                }
            },
            shadowSize: 0,
            tooltip: true,
            yaxis: {
                min: 0
            },
            xaxis: {
                mode: 'time',
                tickDecimals: 0,
                minTickSize: [1, 'day'],
                timeformat: '%d/%m/%y',
            }
        };

        $("<div id='tooltipUser'></div>").css({
            position: "absolute",
            display: "none",
            border: "1px solid #fdd",
            padding: "2px",
            "background-color": "#fee",
            opacity: 0.80
        }).appendTo("body");

        $("#chart_user").bind("plothover", function (event, pos, item) {

            if (!pos.x || !pos.y) {
                return;
            }

            if (item) {
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var date = moment(x).format('DD/MM');
                $("#tooltipUser").html(item.series.label + " ngày " + date + ": " + y)
                    .css({ top: item.pageY + 5, left: item.pageX + 5 })
                    .fadeIn(200);
            } else {
                $("#tooltipUser").hide();
            }
        });

        if ($("#chart_user").length > 0) {

            if (arrNewUser.length > 0) {
                $.plot($("#chart_user"),
                    [{
                        label: "Người dùng mới",
                        data: arrNewUser,
                        lines: {
                            fillColor: "rgba(150, 202, 89, 0.12)"
                        },
                        points: {
                            fillColor: "#fff"
                        }
                    }], chart_plot_02_settings);

            }
            else {
                document.getElementById("chart_user").innerHTML = "Không có người dùng mới!!";
            }
        }
    }

    function initDateRangePicker() {

        if (typeof ($.fn.daterangepicker) === 'undefined') { return; }
        console.log('init_daterangepicker');

        var cb = function (start, end, label) {
            console.log(start.toISOString(), end.toISOString(), label);
            $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        };

        var optionSet1 = {
            startDate: moment().subtract(29, 'days'),
            endDate: moment(),
            minDate: '01/01/2012',
            maxDate: moment().format('MM/DD/YYYY'),
            dateLimit: {
                days: 60
            },
            showDropdowns: true,
            showWeekNumbers: true,
            timePicker: false,
            timePickerIncrement: 1,
            timePicker12Hour: true,
            ranges: {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            },
            opens: 'left',
            buttonClasses: ['btn btn-default'],
            applyClass: 'btn-small btn-primary',
            cancelClass: 'btn-small',
            format: 'MM/DD/YYYY',
            separator: ' to ',
            locale: {
                applyLabel: 'Submit',
                cancelLabel: 'Clear',
                fromLabel: 'From',
                toLabel: 'To',
                customRangeLabel: 'Custom',
                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                firstDay: 1
            }
        };

        $('#reportrange span').html(moment().subtract(29, 'days').format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));
        $('#reportrange').daterangepicker(optionSet1, cb);
        $('#reportrange').on('show.daterangepicker', function () {
            console.log("show event fired");
        });
        $('#reportrange').on('hide.daterangepicker', function () {
            console.log("hide event fired");
        });
        $('#reportrange').on('apply.daterangepicker', function (ev, picker) {
            console.log("apply event fired, start/end dates are " + picker.startDate.format('MMMM D, YYYY') + " to " + picker.endDate.format('MMMM D, YYYY'));
            loadData(picker.startDate.format("MM/DD/YYYY"), picker.endDate.format('MM/DD/YYYY'));


        });
        $('#reportrange').on('cancel.daterangepicker', function (ev, picker) {
            console.log("cancel event fired");
        });
        $('#options1').click(function () {
            $('#reportrange').data('daterangepicker').setOptions(optionSet1, cb);
        });
        $('#options2').click(function () {
            $('#reportrange').data('daterangepicker').setOptions(optionSet2, cb);
        });
        $('#destroy').click(function () {
            $('#reportrange').data('daterangepicker').remove();
        });

    }
}