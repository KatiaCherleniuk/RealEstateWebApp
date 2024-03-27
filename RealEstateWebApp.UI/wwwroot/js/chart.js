(function (scope) {
    scope.Chart = scope.Chart || {}
    scope.Chart.func1 = _func1;
    scope.Chart.func2 = _func2;
    scope.Chart.func3 = _func3;

    function groupDataByTitle(data) {
        var groupedData = {};
        data.forEach(function (item) {
            var dateOnly = item.periodStart.split('T')[0];

            if (!groupedData[item.title]) {
                groupedData[item.title] = {
                    Title: item.title,
                    Data: []
                };
            }
            groupedData[item.title].Data.push([dateOnly, item.averagePrice]); // Використовуємо тільки дату без часу
        });
        return Object.values(groupedData);
    }
    function _func1(info) {
        if (!info || info.length === 0) {
            console.error('Data is empty or undefined.');
            return;
        }

        var data = groupDataByTitle(info);

        Highcharts.chart('container', {
            chart: {
                type: 'areaspline'
            },
            title: {
                text: ''
            },
            xAxis: {
                type: 'category',
                categories: data[0].Data.map(function (point) {
                    return point[0]; // Використовуємо дати з першої серії
                }),
                labels: {
                    formatter: function () {
                        return Highcharts.dateFormat('%Y-%m-%d', Date.parse(this.value));
                    }
                },
                title: {
                    text: 'Period'
                }
            },
            yAxis: {
                title: {
                    text: 'Average Price'
                }
            },
            series: data.map(function (item) {
                return {
                    name: item.Title,
                    data: item.Data
                };
            })
        });
    }




    function _func2(data, registeredLoc) {
        Highcharts.chart('container2', {
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            credits: {
                enabled: false
            },
            xAxis: {
                categories: data.map(i => {
                    const date = new Date(i.registrationDate);
                    const day = date.getDate().toString().padStart(2, '0');
                    const month = (date.getMonth() + 1).toString().padStart(2, '0'); 
                    return `${day}.${month}`; 
                })
            },
            yAxis: {
                title: {
                    enabled: false
                },
                min: 0
            }, legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: `${registeredLoc} - {point.y}`
            },
            colors: [
                "#31afe6",
                "#3fc2e0",
                "#4bd6da",
                "#57e9d4",
                "#63fcd0",
                "#6ffffb",
                "#7effd9",
                "#8dfeba",
                "#9dfc9d",
                "#adfa80",
                "#bcf864",
                "#cbf648",
                "#dafa4c",
                "#eaf250",
                "#f9f854",
                "#fbef5c",
                "#fcea64",
                "#fde46c",
                "#ffdf74",
                "#ffd87c"
            ],


            series: [{
                colorByPoint: true,
                data: data.map(i => i.userCount)
            }],
            legend: {
                enabled: false
            }
        });
    }

    function _func3(data) {
        let colors = ["#2499E3", "#005CA0", "#002761", "#568EFF", "#5287DC", "#7573CD", "#00B7EB", "#00D0D8"]
        var formattedData = data.map(function (record) {
            return {
                name: record.title, 
                y: record.recordCount 
            };
        });

        Highcharts.chart('container3', {
            chart: {
                type: 'pie'
            },
            credits: {
                enabled: false
            },
            legend: {
                enabled: true,
                align: 'left',
                verticalAlign: 'top',
                layout: 'vertical'
            },
            plotOptions: {
                pie: {
                    innerSize: '50%',
                    dataLabels: {
                        enabled: false
                    },
                }
            },
            title: {
                text: ''
            },
            colors: colors,
            series: [{
                name: 'Кількість записів',
                data: formattedData, // Використовуємо трансформовані дані для побудови діаграми pie
                showInLegend: true
            }]
        });
    }
})(window)