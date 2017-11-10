

var appDashboard = angular.module("appDashboard", ["ng-fusioncharts", "angularjs-dropdown-multiselect"]); //angularjs - dropdown - multiselect
appDashboard.controller("BusinessPlanLineKPIController", function ($scope, $http, $filter, excel, $timeout) {
    $scope.lineKpiData = [];
    $scope.CompanyCode = [];
    $http.get("/Businessplan/DashboardJsonData").then(function (businessplan) {
        if (businessplan.status === 200) {
            $scope.Companies = businessplan.data.Companies;
            $scope.example1data = $scope.newCompany();
        }
    });
    $scope.newCompany = function () {
        var company = $scope.Companies;
        var comObj = [];
        if (company.length > 0) {
            for (var i = 0; i < company.length; i++) {
                var com = {};
                com.id = company[i].CompanyCode;
                com.label = company[i].CompanyName;
                comObj.push(com);
            }
        }
        return comObj;
    };
    $scope.attrs = {

        "caption": "STD, PLAN, AND ACTUAL Comparison",
        //"subCaption": "Harry’ s SuperMart",
        //"numberprefix": "$",
        "plotgradientcolor": "",
        "bgcolor": "FFFFFF",
        "showalternatehgridcolor": "0",
        "divlinecolor": "CCCCCC",
        "showvalues": "0",
        "showcanvasborder": "0",
        "canvasborderalpha": "0",
        "canvasbordercolor": "CCCCCC",
        "canvasborderthickness": "1",
        "yaxismaxvalue": "30000",
        "captionpadding": "30",
        "linethickness": "3",
        "yaxisvaluespadding": "15",
        "legendshadow": "0",
        "legendborderalpha": "0",
        "palettecolors": "#00008b,#ffff00,#eff5ff,#33bdda,#e44a00,#6baa01,#583e78,#008ee4",
        "showborder": "0"
    };
    $scope.categories = [
        {
            "category": [
                {
                    "label": "MP"
                }, {
                    "label": "CM"
                }, {
                    "label": "EF"
                }, {
                    "label": "SAM"
                }, {
                    "label": "QTY"
                }
            ]
        }
    ];
    $scope.dataset = [
        {
            "seriesname": "STD",
            "data": [
                {
                    "value": "10000"
                }, {
                    "value": "11500"
                }, {
                    "value": "12500"
                }, {
                    "value": "15000"
                }, {
                    "value": "16000"
                }, {
                    "value": "17600"
                }
            ]
        },
        {
            "seriesname": "PLAN",
            "data": [
                {
                    "value": "22400"
                }, {
                    "value": "24800"
                }, {
                    "value": "21800"
                }, {
                    "value": "21800"
                }, {
                    "value": "24600"
                }, {
                    "value": "27600"
                }
            ]
        },
        {
            "seriesname": "ACTUAL",
            "data": [
                { "value": "10000" },
                { "value": "11500" },
                { "value": "12500" },
                { "value": "15000" },
                { "value": "16000" },
                { "value": "17600" }
            ]
        }
    ];

    $scope.ShawLineKPIChanrt = function (lineData) {
        $scope.checked = true;
        $(".content-area").fadeToggle("slow", "linear");
        if (lineData !== undefined) {
            var sdata = $scope.dataset[0].data;
            $scope.dataset[0].data = $scope._setLineDataSTD(lineData);
            $scope.dataset[1].data = $scope._setLineDataPLAN(lineData);
            $scope.dataset[2].data = $scope._setLineDataACTUAL(lineData);
        } else if ($scope.lineKpiData.length > 0) {
            $scope.dataset[0].data = $scope._setLineDataSTD($scope.lineKpiData[0]);
            $scope.dataset[1].data = $scope._setLineDataPLAN($scope.lineKpiData[0]);
            $scope.dataset[2].data = $scope._setLineDataACTUAL($scope.lineKpiData[0]);
            $(".content-area").fadeToggle("slow", "linear");
            $scope.checked = true;
        }
    };
    $scope._setLineDataSTD = function (lineData) {
        return [{ value: lineData.StdMp }, { value: lineData.StdCm }, { value: lineData.StdEf }, { value: lineData.StdSam }, { value: lineData.StdQty }];
        
    };
    $scope._setLineDataPLAN = function (lineData) {
        return [{ value: lineData.PlanMp }, { value: lineData.PlanCm }, { value: lineData.PlanEf }, { value: lineData.PlanSam }, { value: lineData.PlanQty }];

    };
    $scope._setLineDataACTUAL = function (lineData) {
        return [{ value: lineData.ActualMp }, { value: lineData.ActualCm }, { value: lineData.ActualEf }, { value: lineData.ActualSam }, { value: lineData.ActualQty }];

    };
    $scope.LoadLineKPIData = function () {
        $("#divLoading").show();
        var arr = [];
        angular.forEach($scope.CompanyCode, function (value, key) {
            arr.push(value.id);
        });
        var info = {};
        info.CompanyCode = arr.toString();
        info.FromDate = $filter("date")($scope.FromDate, "yyyy-MM-dd HH:mm:ss");
        info.ToDate = $filter("date")($scope.ToDate, "yyyy-MM-dd HH:mm:ss");
        var config = { params: info, headers: { 'Accept': "application/json" } };
        if (info.FromDate !== undefined && info.ToDate !== undefined) {
            $http.get("/BusinessPlan/GetLineKpiJonData", config).then(function (lineKpi) {
                if (lineKpi.data.length > 0 && lineKpi.status === 200) {
                    $("#divLoading").hide();
                    $scope.lineKpiData = lineKpi.data;
                }
            });
        } else {
            alert("Please Select From and To Date");
        }
    };

    //$scope.example1model = [];
    //$scope.example11data = [{ id: 1, label: "David" }, { id: 2, label: "Jhon" }, { id: 3, label: "Danny" }];
    //$scope.newCompanys = [];

   // app.controller("BusinessPlanLineKPIController", function (excel, $timeout) {
        $scope.exportToExcel = function (tableId) { // ex: '#my-table'
            $scope.exportHref = excel.tableToExcel(tableId, "sheet name");
            $timeout(function () { location.href = $scope.fileData.exportHref; }, 100); // trigger download
        };
    // });
    
});

app.factory("excel", function ($window) {
    var uri = "data:application/vnd.ms-excel;base64,",
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    return {
        tableToExcel: function (tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));
            return href;
        }
    };
});