angular.module('CurrencyExchange.controllers', ['chart.js'])
.controller('mainPageController', function ($scope, $http) {

    $scope.initialize = function () {
        $scope.currentPage = 'currency-exchange-rate';
        $scope.currencylist = [];
        $scope.selectedCur = '';
        $scope.chartCur = '';
        $scope.currencyRateResult = null;
        $scope.conversionPair = {
            FirstRate: {},
            SecondRate: {}
        };
        $scope.chartData = null;

        $http.get('/api/Exchange/GetCurrencies').
        then(function (response) {
            if (response && response.data) {
                $scope.currencylist = response.data;
            }
        });
    };

    $scope.convert = function () {
        $http.post('/api/Exchange/ConvertCurrencies', $scope.conversionPair).
        then(function (response) {
            if (response && response.data) {
                $scope.conversionPair = response.data;
            }
        });
    };

    $scope.getRate = function () {
        $http.get('/api/Exchange/GetCurrencyRate?currencyCode=' + $scope.selectedCur).
        then(function (response) {
            if (response && response.data) {
                $scope.currencyRateResult = {
                    Currency: $scope.selectedCur,
                    Rate: response.data.Rate
                };
            }
        });
    };

    $scope.displayChart = function () {
        $http.get('/api/Exchange/GetChartData?currencyCode=' + $scope.chartCur).
        then(function (response) {
            if (response && response.data) {
                $scope.chartData = {
                    labels: [],
                    data: [],
                    series: ['USD'],
                    datasetOverride: [{ yAxisID: 'y-axis-1' }],
                    options: {
                        scales: {
                            yAxes: [
                              {
                                  id: 'y-axis-1',
                                  type: 'linear',
                                  display: true,
                                  position: 'left'
                              }
                            ]
                        }
                    },
                    colors: [{
                        fillColor: 'rgba(76, 175, 80, 0.8)',
                        strokeColor: 'rgba(76, 175, 80, 0.8)',
                        highlightFill: 'rgba(76, 175, 80, 0.8)',
                        highlightStroke: 'rgba(76, 175, 80, 0.8)'
                    }]
                };

                angular.forEach(response.data, function (d) {
                    if (d) {
                        var date = new Date(d.Date);
                        $scope.chartData.labels.push(date.toDateString());
                        $scope.chartData.data.push(d.Rate.toFixed(4));
                    }
                });
            }
        });
    };

    $scope.firstValueChanged = function () {
        $scope.conversionPair.SecondRate.Value = null;
    };

    $scope.secondValueChanged = function () {
        $scope.conversionPair.FirstRate.Value = null;
    };

    $scope.changePage = function (page) {
        $scope.currentPage = page;
    };

    $scope.initialize();
});