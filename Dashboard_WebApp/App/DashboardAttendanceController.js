
var appDashboard = angular.module("appDashboard", ["ngDialog", "angularjs-dropdown-multiselect"]);

appDashboard.controller("DashboardAttendanceController", function ($scope, $rootScope, $http, $filter, $window, ngDialog) {

    $scope.companyname = "Bitopi Group (BG)";
    $scope.Message = "";
    $scope.CurrentDate = new Date();
    $scope.date = $filter("date")($scope.CurrentDate, "MM/dd/yyyy");
    $scope.example200settings = { scrollableHeight: "200px", Width: "100px", scrollable: true };
    $scope.example200X100settings = { scrollableHeight: "200px", Width: "100px", scrollable: true };
    $scope.multiFilterDLLAll = function () {
        $http.get("/DashboardAttendance/DashboardAllFilterDll").then(function (allFilters) {
            if (allFilters.status === 200) {
                $scope.Companiesddl = allFilters.data.Table;
                $scope.CompaniesObj = $scope.newCompany();

                //$scope.Divisionsddl = allFilters.data.Table1;
                //$scope.DivisionsObj = $scope.newDivision();

                //$scope.Unitsddl = allFilters.data.Table2;
                //$scope.UnitsObj = $scope.newUnit();

                $scope.Departmentsddl = allFilters.data.Table3;
                $scope.DepartmentsObj = $scope.newDepartment();

                //$scope.Sectionsddl = allFilters.data.Table4;
                //$scope.SectionsObj = $scope.newSection();

                //$scope.SubSectionsddl = allFilters.data.Table5;
                //$scope.SubSectionsObj = $scope.newSubSection();

                //$scope.Linesddl = allFilters.data.Table6;
                //$scope.LinesObj = $scope.newLine();
            }
        });
    };
    $scope.openingMultiFilter = function (employeeType) {
        $scope.CompanyCode = [];
        $scope.DepartmentCode = [];
        $scope.SectionCode = [];
        $scope.SubSectionCode = [];
        $scope.LineCode = [];
        $("#divLoading").show();

        var info = {
            EmployeeType: employeeType,
            CompanyCode: "all",
            DivisionCode: "01",
            UnitCode: "U1",
            DepartmentCode: "01",
            SectionCode: "01",
            SubSectionCode: "01",
            LineCode: "01",
            ActivityCode: "01",
            CurrentDate: $filter("date")($scope.CurrentDate, "MM/dd/yyyy") //"05/26/2017"
        };
        
        var config = { params: info, headers: { 'Accept': "application/json" } };
        $http.get("/DashboardAttendance/GetCompanyWise", config).then(function (company) {
            if (company.data.length > 0 && company.status === 200) {
                $scope.Companies = company.data;
                $scope.CompaniesAll = company.data;
                $("#divLoading").hide();
            }
            else {
                $scope.Companies = [];
                $scope.GpBudget = 0;
                $scope.GpOnroll = 0;
                $scope.GpPresent = 0;
                $scope.GpShortage = 0;
                $scope.GpExcess = 0;
                $scope.GpExcessCost = 0;
                $scope.GroupTotalUnallocated = 0;
                $("#divLoading").hide();
                $scope.Message = "Server connectivity problem!";
            }
        });
    }


    $scope.GpTotal = 0;
    $scope.GpPresent = 0;
    $scope.GpLeave = 0;
    $scope.GpAbsent = 0;
    $scope.GpLate = 0;

    $scope.setTotals = function (item) {
        if (item) {
            $scope.GpTotal += item.Onroll;
            $scope.GpPresent += item.Present;
            $scope.GpLeave += item.Leave;
            $scope.GpAbsent += item.Absent;
            $scope.GpLate += item.Late;
        }
    }

    ////////// Filtered Data Loaded ///////////////////

    $scope.newCompany = function () {
        var company = $scope.Companiesddl;
        var comObj = [];
        if (company.length > 0) {
            for (var i = 0; i < company.length; i++) {
                var com = {};
                com.id = company[i].CompanyCode;
                com.label = company[i].CompanyNames;
                comObj.push(com);
            }
        }
        return comObj;
    };
    var info;
    $scope.filtedByCompanyWithAll = function () {
        $("#divLoading").show();
        var companylist = $scope._getCompanylist();
        //var divisionlist = $scope._getDivisionlist();
        //var unitlist = $scope._getUnitlist();
        var department = $scope._getDepartmentList();
        //var section = $scope._getSectionList();
        //var subsection = $scope._getSubSectionList();
        //var line = $scope._getLineList();
        info = {
            EmployeeType: $scope.color.name,
            CompanyCode: companylist.toString(),
            DivisionCode: "01",
            UnitCode: "U1",
            DepartmentCode: department.toString(),
            SectionCode: "01",
            SubSectionCode: "01",
            LineCode: "01",
            ActivityCode: "01",
            CurrentDate: $filter("date")($scope.date, "MM/dd/yyyy")
        };
        info.ActivityCode = "01";
        
        var config = { params: info, headers: { 'Accept': "application/json" } };
        $http.get("/DashboardAttendance/GetCompanyWise", config).then(function (company) {
            if (company.data.length > 0 && company.status === 200) {
                $scope.Companies = company.data;
                $scope.CompaniesAll = company.data;
                $("#divLoading").hide();
            }
            else {
                $scope.Companies = [];
                $scope.GpBudget = 0;
                $scope.GpOnroll = 0;
                $scope.GpPresent = 0;
                $scope.GpShortage = 0;
                $scope.GpExcess = 0;
                $scope.GpExcessCost = 0;
                $scope.GroupTotalUnallocated = 0;
                $("#divLoading").hide();
            }
        });

        /////////////////////////////////
        var companyitems = [];
        var filteritem;

        //angular.forEach($scope.CompanyCode, function (value, key) {
        //    var companyitem = ($filter('filter')($scope.CompaniesAll, { CompanyID: value.id }));
        //    filteritem = $scope.FiltedByDivisionWithAll(companyitem, value.id);
        //    companyitems.push(companyitem[0]);

        //    companyitems[key].Divisions = [];
        //    companyitems[key].Divisions = filteritem;
        //});
        //$scope.Companies = companyitems;
    };
    $scope.openAttendanceEmployeeDetails = function (codes, activitycode) {
        $("#divLoading").show();
        $scope.EmpOnrollorPresent = activitycode;
        var infos = {
            CompanyCode: codes.Company,
            DivisionCode: codes.DivCode,
            UnitCode: codes.UnitCode,
            DepartmentCode: codes.DeptCode,
            SectionCode: codes.SecCode,
            SubSectionCode: codes.SSecCode,
            LineCode: codes.LineCode,
            CurrentDate: $filter("date")($scope.date, "MM/dd/yyyy"),
            EmployeeType: $scope.color.name,
            ActivityCode: activitycode
        };
        $scope.DtailsMessage = "";
        $scope.employees = [];
        
        var config = { params: infos, headers: { 'Accept': "application/json" } };
        $http.get("/DashboardAttendance/GetPresentEmployeeList", config).then(function (response) {
            if (response.data.length > 0 && response.status === 200) {
                $scope.employees = response.data;
                $("#divLoading").hide();
            } else {
                $scope.DtailsMessage = "Data not found";
                $("#divLoading").hide();
            }
        });
        ngDialog.open({ template: "PresentEmployeeList", controller: "DashboardAttendanceController", className: "ngdialog-theme-default", scope: $scope });

    };

    $scope.openAttendanceCharts = function (codes, activitycode) {
        
        ngDialog.open({ template: "Attendance_Details_Chart", controller: "DashboardAttendanceController", className: "ngdialog-theme-default", scope: $scope });
    };

    $scope._getCompanylist = function () {
        var arr = [];
        if ($scope.CompanyCode.length !== 0) {
            angular.forEach($scope.CompanyCode, function (value, key) {
                arr.push(value.id);
            });
        } else {
            angular.forEach($scope.CompaniesObj, function (value, key) {
                arr.push(value.id);
            });
        }
        return arr;
    };
    //$scope._getDivisionlist = function () {
    //    arr = [];
    //    if ($scope.DivisionCode.length !== 0) {
    //        angular.forEach($scope.DivisionCode, function (value, key) {
    //            arr.push(value.id);
    //        });
    //    } else {
    //        angular.forEach($scope.DivisionsObj, function (value, key) {
    //            arr.push(value.id);
    //        });
    //    }
    //    return arr;
    //};
    //$scope._getUnitlist = function () {
    //    arr = [];
    //    if ($scope.UnitCode.length !== 0) {
    //        angular.forEach($scope.UnitCode, function (value, key) {
    //            arr.push(value.id);
    //        });
    //    } else {
    //        angular.forEach($scope.UnitsObj, function (value, key) {
    //            arr.push(value.id);
    //        });
    //    }
    //    return arr;
    //};
    $scope._getDepartmentList = function () {
        var arr = [];
        if ($scope.DepartmentCode.length !== 0) {
            angular.forEach($scope.DepartmentCode, function (value, key) {
                arr.push(value.id);
            });
        } else {
            angular.forEach($scope.DepartmentsObj, function (value, key) {
                arr.push(value.id);
            });
        }
        return arr;
    };
    //$scope._getSectionList = function () {
    //    var arr = [];
    //    if ($scope.SectionCode.length !== 0) {
    //        angular.forEach($scope.SectionCode, function (value, key) {
    //            arr.push(value.id);
    //        });
    //    } else {
    //        angular.forEach($scope.SectionsObj, function (value, key) {
    //            arr.push(value.id);
    //        });
    //    }
    //    return arr;
    //};
    //$scope._getSubSectionList = function () {
    //    var arr = [];
    //    if ($scope.SubSectionCode.length !== 0) {
    //        angular.forEach($scope.SubSectionCode, function (value, key) {
    //            arr.push(value.id);
    //        });
    //    } else {
    //        angular.forEach($scope.SubSectionsObj, function (value, key) {
    //            arr.push(value.id);
    //        });
    //    }
    //    return arr;
    //};
    //$scope._getLineList = function () {
    //    var arr = [];
    //    if ($scope.LineCode.length !== 0) {
    //        angular.forEach($scope.LineCode, function (value, key) {
    //            arr.push(value.id);
    //        });
    //    } else {
    //        angular.forEach($scope.LinesObj, function (value, key) {
    //            arr.push(value.id);
    //        });
    //    }
    //    return arr;
    //};
    //$scope.newDivision = function () {
    //    var division = $scope.Divisionsddl;
    //    var divObj = [];
    //    if (division.length > 0) {
    //        for (var i = 0; i < division.length; i++) {
    //            var com = {};
    //            com.id = division[i].DivisionCode;
    //            com.label = division[i].DivisionName;
    //            divObj.push(com);
    //        }
    //    }
    //    return divObj;
    //};
    //$scope.newUnit = function () {
    //    var unit = $scope.Unitsddl;
    //    var unitObj = [];
    //    if (unit.length > 0) {
    //        for (var i = 0; i < unit.length; i++) {
    //            var com = {};
    //            com.id = unit[i].UnitCode;
    //            com.label = unit[i].UnitName;
    //            unitObj.push(com);
    //        }
    //    }
    //    return unitObj;
    //};
    $scope.newDepartment = function () {
        var unit = $scope.Departmentsddl;
        var unitObj = [];
        if (unit.length > 0) {
            for (var i = 0; i < unit.length; i++) {
                var com = {};
                com.id = unit[i].DepartmentCode;
                com.label = unit[i].DepartmentName;
                unitObj.push(com);
            }
        }
        return unitObj;
    };
    //$scope.newSection = function () {
    //    var section = $scope.Sectionsddl;
    //    var sectionObj = [];
    //    if (section.length > 0) {
    //        for (var i = 0; i < section.length; i++) {
    //            var com = {};
    //            com.id = section[i].SectionCode;
    //            com.label = section[i].SectionName;
    //            sectionObj.push(com);
    //        }
    //    }
    //    return sectionObj;
    //};
    //$scope.newSubSection = function () {
    //    var subSection = $scope.SubSectionsddl;
    //    var subSectionObj = [];
    //    if (subSection.length > 0) {
    //        for (var i = 0; i < subSection.length; i++) {
    //            var com = {};
    //            com.id = subSection[i].SubSectionCode;
    //            com.label = subSection[i].SubSectionName;
    //            subSectionObj.push(com);
    //        }
    //    }
    //    return subSectionObj;
    //};
    //$scope.newLine = function () {
    //    var line = $scope.Linesddl;
    //    var lineObj = [];
    //    if (line.length > 0) {
    //        for (var i = 0; i < line.length; i++) {
    //            var com = {};
    //            com.id = line[i].LineCode;
    //            com.label = line[i].LineName;
    //            lineObj.push(com);
    //        }
    //    }
    //    return lineObj;
    //};

    //$scope.FiltedByDivisionWithAll = function (companyitem, companyid) {
    //    var divisionitems = [];
    //    if ($scope.DivisionCode.length !== 0) {
    //        angular.forEach($scope.DivisionCode, function (value, key) {
    //            var divisionitem = ($filter('filter')($scope.DivisionAll, { CompanyID: companyid, DivisionCode: value.id }));
    //            if (divisionitem.length !== 0) {
    //                var filteritem = $scope.FiltedByUnitWithAll(divisionitem, value.id);
    //                divisionitems.push(divisionitem[0]);
    //            }
    //        });
    //        divisionitems[0].Units = [];
    //        divisionitems[0].Units = $scope.Units
    //        return divisionitems;
    //    }
    //    else {
    //        return companyitem[0].Divisions;
    //    }
    //}


    //$scope.FiltedByUnitWithAll = function (filteritem, divisioncode) {
    //    var unititems = [];
    //    angular.forEach($scope.UnitCode, function (value, key) {
    //        var id = value.id;
    //        var newid = parseInt(id.replace('U', ''));
    //        var unititem = ($filter('filter')($scope.UnitAll, { UnitId: newid, DivisionCode: divisioncode }));
    //        //var filteritem = $scope.FiltedByunitWithAll(item);
    //        unititems.push(unititem[0]);
    //    });
    //    $scope.Units = unititems;
    //}

    ////////// Filtered Data Loaded ///////////////////

    //openngDiologValidationUnit("all");
    //$scope._insertDivisions = function () {
    //    if ($scope.Companies.length > 0 && $scope.Divisions.length > 0) {
    //        for (var i = 0; i < $scope.Companies.length; i++) {
    //            var companycode = $scope.Companies[i].CompanyCode;

    //            var aDivisions = [];
    //            for (var j = 0; j < $scope.Divisions.length; j++) {
    //                if (companycode === $scope.Divisions[j].CompanyCode) {
    //                    var aDivision = {};
    //                    aDivision.CompanyCode = $scope.Divisions[j].CompanyCode;
    //                    aDivision.CompanyName = $scope.Divisions[j].CompanyName;
    //                    aDivision.DivisionCode = $scope.Divisions[j].DivisionCode;
    //                    aDivision.DivisionName = $scope.Divisions[j].DivisionName;
    //                    aDivision.Budget = $scope.Divisions[j].Budget;
    //                    aDivision.Onroll = $scope.Divisions[j].Onroll;
    //                    aDivision.Shortage = $scope.Divisions[j].Shortage;
    //                    aDivision.Excess = $scope.Divisions[j].Excess;

    //                    aDivision.UserDefine = $scope.Divisions[j].UserDefine;
    //                    aDivision.UdShortage = $scope.Divisions[j].UdShortage;
    //                    aDivision.UdExcess = $scope.Divisions[j].UdExcess;
    //                    aDivision.UdOnShortage = $scope.Divisions[j].UdOnShortage;
    //                    aDivision.UdOnExcess = $scope.Divisions[j].UdOnExcess;

    //                    $scope._insertUnits(companycode, $scope.Divisions[j].DivisionCode);
    //                    aDivision.Units = $scope.aCompanyUnits;
    //                    aDivisions.push(aDivision);
    //                }
    //            }
    //            $scope.Companies[i].Divisions = aDivisions;
    //        }
    //    }
    //};
    //$scope._insertUnits = function (companycode, divisionCode) {
    //    if ($scope.Units.length > 0) {
    //        var units = [];
    //        for (var i = 0; i < $scope.Units.length; i++) {
    //            if (companycode === $scope.Units[i].CompanyCode && $scope.Units[i].DivisionCode === divisionCode) {
    //                var aUnit = {};

    //                aUnit.CompanyCode = $scope.Units[i].CompanyCode;
    //                aUnit.CompanyName = $scope.Units[i].CompanyName;
    //                aUnit.DivisionCode = $scope.Units[i].DivisionCode;
    //                aUnit.UnitCode = $scope.Units[i].UnitCode;
    //                aUnit.UnitName = $scope.Units[i].UnitName;
    //                aUnit.Budget = $scope.Units[i].Budget;
    //                aUnit.Onroll = $scope.Units[i].Onroll;

    //                aUnit.ExcessCost = $scope.Units[i].ExcessCost;
    //                aUnit.Shortage = $scope.Units[i].Shortage;
    //                aUnit.Excess = $scope.Units[i].Excess;

    //                aUnit.UserDefine = $scope.Units[i].UserDefine;
    //                aUnit.UdExcessCost = $scope.Units[i].UdExcessCost;
    //                aUnit.UdShortage = $scope.Units[i].UdShortage;
    //                aUnit.UdExcess = $scope.Units[i].UdExcess;

    //                aUnit.UdOnExcessCost = $scope.Units[i].UdOnExcessCost;
    //                aUnit.UdOnShortage = $scope.Units[i].UdOnShortage;
    //                aUnit.UdOnExcess = $scope.Units[i].UdOnExcess;

    //                units.push(aUnit);
    //            }
    //        }
    //        $scope.aCompanyUnits = units;
    //    }
    //};
    //$scope._getshortage = function (shortage) {
    //    if (shortage > 0) {
    //        return shortage;
    //    } else {
    //        return 0;
    //    }
    //};
    //$scope._getexcess = function (shortage) {
    //    if (shortage < 0) {
    //        return Math.abs(shortage);
    //    } else {
    //        return 0;
    //    }
    //};
    //$scope._getcompanytotal = function (data) {
    //    var total = 0;
    //    if (data.length > 0) {
    //        for (var i = 0; i < data.length; i++) {
    //            total = total + data[i].UnitTotal;
    //        }
    //    }
    //    return total;
    //};
    //$scope._getGroupTotalBudget = function (allCompany) {
    //    var gtotal = 0;
    //    var gonroll = 0;
    //    var gpresent = 0;
    //    var guserdefine = 0;

    //    var gshortage = 0;
    //    var gexcess = 0;
    //    var gexcesscost = 0;

    //    var gudshortage = 0;
    //    var gudexcess = 0;
    //    var gudexcesscost = 0;

    //    var gudonshortage = 0;
    //    var gudonexcess = 0;
    //    var gudonexcesscost = 0;

    //    for (var i = 0; i < allCompany.length; i++) {

    //        var total = allCompany[i].Budget;
    //        gtotal = parseInt(gtotal) + parseInt(total);

    //        var onroll = allCompany[i].Onroll;
    //        gonroll = parseInt(gonroll) + parseInt(onroll);

    //        var present = allCompany[i].Present;
    //        gpresent = parseInt(gpresent) + parseInt(present);

    //        var userdefine = allCompany[i].UserDefine;
    //        guserdefine = parseInt(guserdefine) + parseInt(userdefine);

    //        var shortage = allCompany[i].Shortage;
    //        gshortage = parseInt(gshortage) + parseInt(shortage);

    //        var excess = allCompany[i].Excess;
    //        gexcess = parseInt(gexcess) + parseInt(excess);

    //        var excesscost = allCompany[i].ExcessCost;
    //        gexcesscost = parseInt(gexcesscost) + parseInt(excesscost);

    //        var udshortage = allCompany[i].UdShortage;
    //        gudshortage = parseInt(gudshortage) + parseInt(udshortage);

    //        var udexcess = allCompany[i].UdExcess;
    //        gudexcess = parseInt(gudexcess) + parseInt(udexcess);

    //        var udexcesscost = allCompany[i].UdExcessCost;
    //        gudexcesscost = parseInt(gudexcesscost) + parseInt(udexcesscost);

    //        var udonshortage = allCompany[i].UdOnShortage;
    //        gudonshortage = parseInt(gudonshortage) + parseInt(udonshortage);

    //        var udonexcess = allCompany[i].UdOnExcess;
    //        gudonexcess = parseInt(gudonexcess) + parseInt(udonexcess);

    //        var udonexcesscost = allCompany[i].UdOnExcessCost;
    //        gudonexcesscost = parseInt(gudonexcesscost) + parseInt(udonexcesscost);


    //    }
    //    $scope.GpBudget = gtotal;
    //    $scope.GpOnroll = gonroll;
    //    $scope.GpPresent = gpresent;
    //    $scope.GpUserDefine = guserdefine;

    //    $scope.GpShortage = gshortage;
    //    $scope.GpExcess = gexcess;
    //    $scope.GpExcessCost = gexcesscost;

    //    $scope.GpUdShortage = gudshortage;
    //    $scope.GpUdExcess = gudexcess;
    //    $scope.GpUdExcessCost = gudexcesscost;

    //    $scope.GpUdOnShortage = gudonshortage;
    //    $scope.GpUdOnExcess = gudonexcess;
    //    $scope.GpUdOnExcessCost = gudonexcesscost;

    //};
    //$scope._getGroupTotalOnroll = function (allCompany) {
    //    var gtotal = 0;
    //    for (var i = 0; i < allCompany.length; i++) {
    //        var total = allCompany[i].Actual;
    //        gtotal = parseInt(gtotal) + parseInt(total);
    //    }
    //    return gtotal;
    //};
    //$scope._getGroupTotalUserDefine = function (allCompany) {
    //    var gtotal = 0;
    //    for (var i = 0; i < allCompany.length; i++) {
    //        var total = allCompany[i].UserDefine;
    //        gtotal = parseInt(gtotal) + parseInt(total);
    //    }
    //    return gtotal;
    //};
    //$scope._getGroupTotalUnallocated = function (allCompany) {
    //    var gtotal = 0;
    //    for (var i = 0; i < allCompany.length; i++) {
    //        var total = allCompany[i].Unallocated;
    //        gtotal = parseInt(gtotal) + parseInt(total);
    //    }
    //    return gtotal;
    //};


    ////// hare Budget and Onroll Data Loading ///////////
    $scope.OpenngDiologValidation_Dept = function (event) {
        $("#divLoading").show();
        $scope.Message = "";
        $scope.departments = [];
        var obj = event.target.title;
        var aCompany = obj.split("_");
        $scope.depts = [{ CompanyCode: "-(" + aCompany[0] + ")" }, { CompanyName: aCompany[1] }];

        var obj2 = event.target.id;
        var aDivision = obj2.split("_");
        $scope.depts = [{ CompanyCode: "-(" + aCompany[0] + ")" }, { CompanyName: aCompany[1] }];

        var department = $scope._getDepartmentList();
        var comobj = {
            CompanyCode: aCompany[0],
            DivisionCode: aDivision[0],
            UnitCode: aDivision[1],
            DepartmentCode: department.toString(),
            EmployeeType: $scope.color.name
        };
        //comobj.CompanyCode = aCompany[0];
        //comobj.DivisionCode = aDivision[0];
        //comobj.UnitCode = aDivision[1];
        //comobj.EmployeeType = $scope.color.name;

        var config = { params: comobj, headers: { 'Accept': "application/json" } };
        $http.get("/DashboardMultiFilter/DashboardDepartment", config).then(function (dept) {

            if (dept.data.length > 0 && dept.status === 200) {
                $http.get("/Dashboard/DashboardSection", config).then(function (section) {
                    if (section.data.length > 0 && section.status === 200) {
                        $http.get("/Dashboard/DashboardSubSection", config).then(function (subsection) {
                            if (subsection.data.length > 0 && subsection.status === 200) {

                                var insertedSectiondata = $scope.InsertSection(dept.data, section.data, subsection.data, comobj);
                                $scope.departments = insertedSectiondata;

                                $scope.cols = Object.keys($scope.departments[0]);
                                $("#divLoading").hide();
                            } else {
                                $scope.Message = "Data not found";
                            }

                        });
                    } else {
                        $scope.Message = "Data not found";
                    }
                });
            } else {
                $scope.Message = "Data not found";
            }
        });
        ngDialog.open({ template: "Validation_DepartmentList", controller: "DashboardMultiFilterController", className: "ngdialog-theme-default", scope: $scope });
    };
    $scope.InsertSection = function (dept, section, ssection, comobj) {
        if (dept.length > 0) {
            for (var i = 0; i < dept.length; i++) {
                comobj.departmentcode = dept[i].DepartmentCode;
                var insertedSectiondata = [];
                for (var j = 0; j < section.length; j++) {

                    comobj.sectioncode = section[j].SectionCode;
                    if (comobj.departmentcode === section[j].DepartmentCode && comobj.sectioncode === section[j].SectionCode) {

                        insertedSectiondata.push(section[j]);
                        $scope.insertedSubSection(insertedSectiondata, ssection, comobj);
                    }
                }
                for (var k = 0; k < insertedSectiondata.length; k++) {
                    delete insertedSectiondata[k].SectionCode;
                    delete insertedSectiondata[k].DepartmentCode;
                    delete insertedSectiondata[k].UDefine;
                    if (insertedSectiondata[k].SubSections.length > 0) {
                        var sSection = insertedSectiondata[k].SubSections;
                        for (var l = 0; l < sSection.length; l++) {
                            delete sSection[l].DepartmentCode;
                            delete sSection[l].SectionCode;
                            delete sSection[l].SubSectionCode;
                            delete sSection[l].UDefine;
                        }
                    }
                }
                dept[i].Sections = insertedSectiondata;
                delete dept[i].DepartmentCode;
                delete dept[i].UDefine;
            }
        };
        return dept;
    };
    $scope.insertedSubSection = function (sectiondata, sSection, comobj) {
        if (sectiondata.length > 0 && sSection.length > 0) {

            for (var j = 0; j < sectiondata.length; j++) {
                comobj.sectioncode = sectiondata[j].SectionCode;
                var subSections = [];

                for (var i = 0; i < sSection.length; i++) {
                    comobj.subsectioncode = sSection[i].SubSectionCode;
                    if (comobj.departmentcode === sSection[i].DepartmentCode && comobj.sectioncode === sSection[i].SectionCode && comobj.subsectioncode === sSection[i].SubSectionCode) {
                        sSection[i].Action = [{
                            CompanyCode: comobj.CompanyCode, DivisionCode: comobj.DivisionCode, UnitCode: comobj.UnitCode, DepartmentCode: comobj.departmentcode,
                            SectionCode: comobj.sectioncode, SubSectionCode: comobj.subsectioncode, EmployeeType: comobj.EmployeeType
                        }];
                        subSections.push(sSection[i]);
                    }
                    sectiondata[j].SubSections = subSections;
                }
            }
        }
        return sectiondata;
    };
    ////// End of Budget and Onroll Data Loading ///////////

    ////// hare Budget and UserDefine Data Loading ///////////
    $scope.OpenngDiologValidation_UserDefine_Dept = function (event) {
        $("#divLoading").show();
        $scope.Message = "";
        $scope.departments = [];
        var obj = event.target.title;
        var aCompany = obj.split("_");
        $scope.depts = [{ CompanyCode: "-(" + aCompany[0] + ")" }, { CompanyName: aCompany[1] }];

        var obj2 = event.target.id;
        var aDivision = obj2.split("_");
        $scope.depts = [{ CompanyCode: "-(" + aCompany[0] + ")" }, { CompanyName: aCompany[1] }];

        var comobj = {};
        comobj.CompanyCode = aCompany[0];
        comobj.DivisionCode = aDivision[0];
        comobj.UnitCode = aDivision[1];
        comobj.EmployeeType = $scope.color.name;

        var config = { params: comobj, headers: { 'Accept': "application/json" } };
        $http.get("/Dashboard/DashboardDepartment", config).then(function (dept) {
            if (dept.data.length > 0 && dept.status === 200) {
                $http.get("/Dashboard/DashboardSection", config).then(function (section) {
                    if (section.data.length > 0 && section.status === 200) {
                        $http.get("/Dashboard/DashboardSubSection", config).then(function (subsection) {
                            if (subsection.data.length > 0 && subsection.status === 200) {

                                var insertedSectiondata = $scope.Insert_UserDefine_Section(dept.data, section.data, subsection.data, comobj);
                                $scope.departments = insertedSectiondata;

                                $scope.cols = Object.keys($scope.departments[0]);
                                $("#divLoading").hide();
                            } else {
                                $scope.Message = "Data not found";
                            }

                        });
                    } else {
                        $scope.Message = "Data not found";
                    }
                });
            } else {
                $scope.Message = "Data not found";
            }
        });
        ngDialog.open({ template: "Validation_DepartmentList", controller: "DashboardMultiFilterController", className: "ngdialog-theme-default", scope: $scope });
    };
    $scope.Insert_UserDefine_Section = function (dept, section, ssection, comobj) {
        if (dept.length > 0) {
            for (var i = 0; i < dept.length; i++) {
                comobj.departmentcode = dept[i].DepartmentCode;
                var insertedSectiondata = [];
                for (var j = 0; j < section.length; j++) {

                    comobj.sectioncode = section[j].SectionCode;
                    if (section[j].DepartmentCode === comobj.departmentcode && section[j].SectionCode === comobj.sectioncode) {

                        insertedSectiondata.push(section[j]);
                        $scope.Inserted_UserDefine_SubSection(insertedSectiondata, ssection, comobj);
                    }
                }
                for (var k = 0; k < insertedSectiondata.length; k++) {
                    delete insertedSectiondata[k].SectionCode;
                    delete insertedSectiondata[k].DepartmentCode;
                    delete insertedSectiondata[k].Onroll;
                    if (insertedSectiondata[k].SubSections.length > 0) {
                        var sSection = insertedSectiondata[k].SubSections;
                        for (var l = 0; l < sSection.length; l++) {
                            delete sSection[l].DepartmentCode;
                            delete sSection[l].SectionCode;
                            delete sSection[l].SubSectionCode;
                            delete sSection[l].Onroll;

                        }
                    }
                }
                dept[i].Sections = insertedSectiondata;
                delete dept[i].DepartmentCode;
                delete dept[i].Onroll;
            }
        };
        return dept;
    };
    $scope.Inserted_UserDefine_SubSection = function (sectiondata, sSection, comobj) {
        if (sectiondata.length > 0 && sSection.length > 0) {

            for (var j = 0; j < sectiondata.length; j++) {
                comobj.sectioncode = sectiondata[j].SectionCode;
                var subSections = [];

                for (var i = 0; i < sSection.length; i++) {
                    comobj.subsectioncode = sSection[i].SubSectionCode;
                    if (comobj.departmentcode === sSection[i].DepartmentCode && comobj.sectioncode === sSection[i].SectionCode && comobj.subsectioncode === sSection[i].SubSectionCode) {
                        sSection[i].Action = [{
                            CompanyCode: comobj.CompanyCode, DivisionCode: comobj.DivisionCode, UnitCode: comobj.UnitCode, DepartmentCode: comobj.departmentcode,
                            SectionCode: comobj.sectioncode, SubSectionCode: comobj.subsectioncode
                        }];
                        subSections.push(sSection[i]);
                    }
                    sectiondata[j].SubSections = subSections;
                }
            }
        }
        return sectiondata;
    };
    ////// End of Budget and UserDefine Data Loading ///////////


    ////// hare UserDefine and Onroll Data Loading ///////////
    $scope.OpenngDiologValidation_UserDefineOnroll_Dept = function (event) {
        $("#divLoading").show();
        $scope.Message = "";
        $scope.departments = [];
        var obj = event.target.title;
        var aCompany = obj.split("_");
        $scope.depts = [{ CompanyCode: "-(" + aCompany[0] + ")" }, { CompanyName: aCompany[1] }];

        var obj2 = event.target.id;
        var aDivision = obj2.split("_");
        $scope.depts = [{ CompanyCode: "-(" + aCompany[0] + ")" }, { CompanyName: aCompany[1] }];

        var comobj = {};
        comobj.CompanyCode = aCompany[0];
        comobj.DivisionCode = aDivision[0];
        comobj.UnitCode = aDivision[1];
        comobj.EmployeeType = $scope.color.name;

        var config = { params: comobj, headers: { 'Accept': "application/json" } };
        $http.get("/Dashboard/DashboardDepartmentUdOn", config).then(function (dept) {
            if (dept.data.length > 0 && dept.status === 200) {
                $http.get("/Dashboard/DashboardSectionUdOn", config).then(function (section) {
                    if (section.data.length > 0 && section.status === 200) {
                        $http.get("/Dashboard/DashboardSubSectionUdOn", config).then(function (subsection) {
                            if (subsection.data.length > 0 && subsection.status === 200) {

                                var insertedSectiondata = $scope.Insert_UserDefineOnroll_Section(dept.data, section.data, subsection.data, comobj);
                                $scope.departments = insertedSectiondata;

                                $scope.cols = Object.keys($scope.departments[0]);
                                $("#divLoading").hide();
                            } else {
                                $scope.Message = "Data not found";
                            }

                        });
                    } else {
                        $scope.Message = "Data not found";
                    }
                });
            } else {
                $scope.Message = "Data not found";
            }
        });
        ngDialog.open({ template: "Validation_DepartmentList", controller: "DashboardMultiFilterController", className: "ngdialog-theme-default", scope: $scope });
    };
    $scope.Insert_UserDefineOnroll_Section = function (dept, section, ssection, comobj) {
        if (dept.length > 0) {
            for (var i = 0; i < dept.length; i++) {
                comobj.departmentcode = dept[i].DepartmentCode;
                var insertedSectiondata = [];
                for (var j = 0; j < section.length; j++) {

                    comobj.sectioncode = section[j].SectionCode;
                    if (section[j].DepartmentCode === comobj.departmentcode && section[j].SectionCode === comobj.sectioncode) {

                        insertedSectiondata.push(section[j]);
                        $scope.inserted_UserDefineOnroll_SubSection(insertedSectiondata, ssection, comobj);
                    }
                }
                for (var k = 0; k < insertedSectiondata.length; k++) {
                    delete insertedSectiondata[k].SectionCode;
                    delete insertedSectiondata[k].DepartmentCode;
                    delete insertedSectiondata[k].Budget;
                    if (insertedSectiondata[k].SubSections.length > 0) {
                        var sSection = insertedSectiondata[k].SubSections;
                        for (var l = 0; l < sSection.length; l++) {
                            delete sSection[l].DepartmentCode;
                            delete sSection[l].SectionCode;
                            delete sSection[l].SubSectionCode;
                            delete sSection[l].Budget;
                        }
                    }
                }
                dept[i].Sections = insertedSectiondata;
                delete dept[i].DepartmentCode;
                delete dept[i].Budget;
            }
        };
        return dept;
    };
    $scope.inserted_UserDefineOnroll_SubSection = function (sectiondata, sSection, comobj) {
        if (sectiondata.length > 0 && sSection.length > 0) {

            for (var j = 0; j < sectiondata.length; j++) {
                comobj.sectioncode = sectiondata[j].SectionCode;
                var subSections = [];

                for (var i = 0; i < sSection.length; i++) {
                    comobj.subsectioncode = sSection[i].SubSectionCode;
                    if (comobj.departmentcode === sSection[i].DepartmentCode && comobj.sectioncode === sSection[i].SectionCode && comobj.ssectioncode === sSection[i].SSectionCode) {
                        sSection[i].Action = [{
                            CompanyCode: comobj.CompanyCode, DivisionCode: comobj.DivisionCode, UnitCode: comobj.UnitCode, DepartmentCode: comobj.departmentcode,
                            SectionCode: comobj.sectioncode, SubSectionCode: comobj.subsectioncode
                        }];
                        subSections.push(sSection[i]);
                    }
                    sectiondata[j].SubSections = subSections;
                }
            }
        }
        return sectiondata;
    };
    ////// End of UserDefine and Onroll Data Loading ///////////

    $scope.OpenngDiologValidation = function () {
        ngDialog.open({ template: "Validation_Id", controller: "DashboardMultiFilterController", className: "ngdialog-theme-default", scope: $scope });
    };

    $scope._openExcessList = function (event) {
        $scope.companycode = event.target.id;
        $scope.companyname = event.target.title;
        $rootScope.companycode = event.target.id;
        $rootScope.companyname = event.target.title;
        $scope.Excessemployees = [];
        $http.get("/Dashboard/DashboardExcessEmpList?companyCode=" + $scope.companycode).then(function (response) {
            if (response.data.length > 0 && response.status === 200) {
                $scope.Excessemployees = response.data;
            }
        });
        ngDialog.open({ template: "ExcessEmpList", controller: "DashboardMultiFilterController", className: "ngdialog-theme-default", scope: $scope });

    };
    $scope._openUnallocated = function (event) {
        $scope.companycode = event.target.id;
        $scope.companyname = event.target.title;
        $rootScope.companycode = event.target.id;
        $rootScope.companyname = event.target.title;
        $scope.employees = [];
        $http.get("/Dashboard/DashboardUnallocatedEmpList?companyCode=" + $scope.companycode).then(function (response) {
            if (response.data.length > 0 && response.status === 200) {
                $scope.employees = response.data;
            } else {
                $scope.UAMessage = "Data not found";
            }
        });
        ngDialog.open({ template: "UnallocatedEmpList_Table", controller: "DashboardMultiFilterController", className: "ngdialog-theme-default", scope: $scope });

    };
    $scope.ShowLineDetails = function (codes) {
        $scope.DtailsMessage = "";
        $scope.employees = [];
        //codes.EmployeeType = $scope.color.name;
        var config = { params: codes, headers: { 'Accept': "application/json" } };
        $http.get("/DashboardMultiFilter/DashboardAllocatedEmpList", config).then(function (response) {
            if (response.data.length > 0 && response.status === 200) {
                $scope.employees = response.data;
            } else {
                $scope.DtailsMessage = "Data not found";
            }
        });
        ngDialog.open({ template: "AllocatedEmpList", controller: "DashboardMultiFilterController", className: "ngdialog-theme-default", scope: $scope });

    };
    $scope.ShowLine_UserDefine_Details = function () {
        alert("Employee List is not in the database");
    };
    $scope.isArray = angular.isArray;
    $scope.toggleChildRow = function (event) {
        $("#target").toggle(function () {
            $("img#" + event.currentTarget.id).attr("src", "http://i.imgur.com/d4ICC.png");
            alert("First handler for .toggle() called.");
        }, function () {
            alert("Second handler for .toggle() called.");
        });



        $scope.id = 0;
        if (event.currentTarget.id !== 0 && $scope.expand === 1) {
            $("img#" + event.currentTarget.id).attr("src", "http://i.imgur.com/d4ICC.png");
            $scope.expand = 0;
        } else {
            $("img#" + event.currentTarget.id).attr("src", "http://i.imgur.com/SD7Dz.png");
            $scope.id = event.currentTarget.id;
            $scope.expand = 1;
        }
    };
    $scope.color = { name: "all" };
    $scope.ShowMP_DirectDetails = function (event) {
        if ($scope.color.name !== undefined) {
            $scope.empType = $scope.color.name;
            // openngDiologValidationUnit($scope.color.name);
        }
    };

    ///////////////// All Value Set ////////////////////

    $scope.Refresh = function () {
        $window.location.reload();
    }
    ///////////////// Show Details Render ////////////////////
});

