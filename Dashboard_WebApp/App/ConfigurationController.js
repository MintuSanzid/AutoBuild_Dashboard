
var app = angular.module("app", ["ngDialog"]);

app.controller("ConfigurationController", function ($scope, $rootScope, $http, $filter, $window, ngDialog) {

    $scope.companyname = "Bitopi Group (BG)";

    function openngDiologValidationUnit(employeeType) {
        $("#divLoading").show();
        $scope.empType = "all";
        var info = { EmployeeType: employeeType };
        var config = { params: info, headers: { 'Accept': "application/json" } };

        $http.get("/Configuration/DashboardCompany", config).then(function (company) {
            if (company.data.length > 0 && company.status === 200) {
                $scope.Companies = company.data;
                $("#divLoading").hide();
                $scope._getGroupTotalBudget(company.data);
                $scope.GroupTotalUnallocated = $scope._getGroupTotalUnallocated(company.data);
                $http.get("/Configuration/DashboardDivisionJsonData", config).then(function (division) {
                    if (division.data.length > 0 && division.status === 200) {
                        $scope.Divisions = division.data;

                        $http.get("/Configuration/DashboardUnitJsonData", config).then(function (unit) {
                            if (unit.data.length > 0 && unit.status === 200) {
                                $scope.Units = unit.data;
                                
                                $scope._insertDivisions();
                                $("#divLoading").hide();
                            }
                        });
                    }
                });
            }
        });

        $http.get("/Configuration/DashboardDivisionJsonData", config).then(function (response) {
            if (response.data.length > 0 && response.status === 200) {
                $scope._divisions = response.data;
            }
        });

        $http.get("/Configuration/DashboardUnitJsonData", config).then(function (response) {
            if (response.data.length > 0 && response.status === 200) {
                $scope._units = response.data;
                $scope.companytotal = $scope._getcompanytotal(response.data);
                $scope.companyUnallocated = response.data[0].Unallocated;
                $scope.companybudgeted = $scope.companytotal + response.data[0].Unallocated;
            }
        });

    }
    openngDiologValidationUnit("all");
    $scope._insertDivisions = function () {
        if ($scope.Companies.length > 0 && $scope.Divisions.length > 0) {
            for (var i = 0; i < $scope.Companies.length; i++) {
                var companycode = $scope.Companies[i].CompanyCode;

                var aDivisions = [];
                for (var j = 0; j < $scope.Divisions.length; j++) {
                    if (companycode === $scope.Divisions[j].CompanyCode) {
                        var aDivision = {};
                        aDivision.CompanyCode = $scope.Divisions[j].CompanyCode;
                        aDivision.CompanyName = $scope.Divisions[j].CompanyName;
                        aDivision.DivisionCode = $scope.Divisions[j].DivisionCode;
                        aDivision.DivisionName = $scope.Divisions[j].DivisionName;
                        aDivision.Budget = $scope.Divisions[j].Budget;
                        aDivision.Actual = $scope.Divisions[j].Actual;
                        aDivision.UserDefine = $scope.Divisions[j].UserDefine; 
                        aDivision.Shortage = $scope.Divisions[j].Shortage;
                        aDivision.Excess = $scope.Divisions[j].Excess;

                        $scope._insertUnits(companycode, $scope.Divisions[j].DivisionCode);
                        aDivision.Units = $scope.aCompanyUnits;
                        aDivisions.push(aDivision);
                    }
                }
                $scope.Companies[i].Divisions = aDivisions;
            }
        }
    }
    $scope._insertUnits = function (companycode, divisionCode) {
        if ($scope.Units.length > 0) {
            var units = [];
            for (var i = 0; i < $scope.Units.length; i++) {
                if (companycode === $scope.Units[i].CompanyCode && $scope.Units[i].DivCode === divisionCode) {
                    var aUnit = {};

                    aUnit.CompanyCode = $scope.Units[i].CompanyCode;
                    aUnit.CompanyName = $scope.Units[i].CompanyName;
                    aUnit.DivCode = $scope.Units[i].DivCode;
                    aUnit.UnitCode = $scope.Units[i].UnitCode;
                    aUnit.UnitName = $scope.Units[i].UnitName;
                    aUnit.Budget = $scope.Units[i].Budget;
                    aUnit.Actual = $scope.Units[i].Actual;
                    aUnit.UserDefine = $scope.Units[i].UserDefine;
                    aUnit.ExcessCost = $scope.Units[i].ExcessCost;
                    aUnit.Shortage = $scope.Units[i].Shortage;
                    aUnit.Excess = $scope.Units[i].Excess;
                    units.push(aUnit);
                }
            }
            $scope.aCompanyUnits = units;
        }
    }

    $scope._getshortage = function (shortage) {
        if (shortage > 0) {
            return shortage;
        } else {
            return 0;
        }
    }
    $scope._getexcess = function (shortage) {
        if (shortage < 0) {
            return Math.abs(shortage);
        } else {
            return 0;
        }
    }
    $scope._getcompanytotal = function (data) {
        var total = 0;
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                total = total + data[i].UnitTotal;
            }
        }
        return total;
    }
    $scope._getGroupTotalBudget = function (allCompany) {
        var gtotal = 0;
        var gactual = 0;
        var guserdefine = 0;
        var gshortage = 0;
        var gexcess = 0;
        var gexcesscost = 0;

        for (var i = 0; i < allCompany.length; i++) {

            var total = allCompany[i].Budget;
            gtotal = parseInt(gtotal) + parseInt(total);

            var actual = allCompany[i].Actual;
            gactual = parseInt(gactual) + parseInt(actual);

            var userdefine = allCompany[i].UserDefine; 
            guserdefine = parseInt(guserdefine) + parseInt(userdefine);

            var shortage = allCompany[i].Shartage;
            gshortage = parseInt(gshortage) + parseInt(shortage);

            var excess = allCompany[i].Excess;
            gexcess = parseInt(gexcess) + parseInt(excess);

            var excesscost = allCompany[i].ExcessCost;
            gexcesscost = parseInt(gexcesscost) + parseInt(excesscost);
        }
        $scope.GpBudget = gtotal;
        $scope.GpActual = gactual;
        $scope.GpUserDefine = guserdefine;
        $scope.GpShortage = gshortage;
        $scope.GpExcess = gexcess;
        $scope.GpExcessCost = gexcesscost;
    }
    $scope._getGroupTotalActual = function (allCompany) {
        var gtotal = 0;
        for (var i = 0; i < allCompany.length; i++) {
            var total = allCompany[i].Actual;
            gtotal = parseInt(gtotal) + parseInt(total);
        }
        return gtotal;
    }
    $scope._getGroupTotalUnallocated = function (allCompany) {
        var gtotal = 0;
        for (var i = 0; i < allCompany.length; i++) {
            var total = allCompany[i].Unallocated;
            gtotal = parseInt(gtotal) + parseInt(total);
        }
        return gtotal;
    }

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

        var comobj = {};
        comobj.CompanyCode = aCompany[0];
        comobj.DivisionCode = aDivision[0];
        comobj.UnitCode = aDivision[1];
        comobj.EmployeeType = $scope.color.name;

        var config = { params: comobj, headers: { 'Accept': "application/json" } };
        $http.get("/Configuration/DashboardHrfDepartmentJson", config).then(function (dept) {
            if (dept.data.length > 0 && dept.status === 200) {
                $http.get("/Configuration/DashboardHRFSectionJson", config).then(function (section) {
                    if (section.data.length > 0 && section.status === 200) {
                        $http.get("/Configuration/DashboardHrfSubSectionJson", config).then(function (subsection) {
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
        ngDialog.open({ template: "Validation_DepartmentList", controller: "ConfigurationController", className: "ngdialog-theme-default", scope: $scope });
    };
    $scope.InsertSection = function (dept, section, ssection, comobj) {
        if (dept.length > 0) {
            for (var i = 0; i < dept.length; i++) {
                comobj.deptId = dept[i].DeptId;
                var insertedSectiondata = [];
                for (var j = 0; j < section.length; j++) {

                    comobj.sectionId = section[j].SectionId;
                    if (section[j].DeptId === comobj.deptId && section[j].SectionId === comobj.sectionId) {

                        insertedSectiondata.push(section[j]);
                        $scope.insertedSubSection(insertedSectiondata, ssection, comobj);
                    }
                }
                for (var k = 0; k < insertedSectiondata.length; k++) {
                    delete insertedSectiondata[k].SectionId;
                    delete insertedSectiondata[k].DeptId;
                    if (insertedSectiondata[k].SubSections.length > 0) {
                        var sSection = insertedSectiondata[k].SubSections;
                        for (var l = 0; l < sSection.length; l++) {
                            delete sSection[l].DeptId;
                            delete sSection[l].SectionId;
                            delete sSection[l].SSectionId;
                        }
                    }
                }
                dept[i].Sections = insertedSectiondata;
                delete dept[i].DeptId;
            }
        };
        return dept;
    };
    $scope.insertedSubSection = function (sectiondata, sSection, comobj) {
        if (sectiondata.length > 0 && sSection.length > 0) {

            for (var j = 0; j < sectiondata.length; j++) {
                comobj.sectionId = sectiondata[j].SectionId;
                var subSections = [];

                for (var i = 0; i < sSection.length; i++) {
                    comobj.ssectionId = sSection[i].SSectionId;
                    if (sSection[i].DeptId === comobj.deptId && sSection[i].SectionId === comobj.sectionId && sSection[i].SSectionId === comobj.ssectionId) {

                        //delete sSection[i].SSectionId;
                        sSection[i].Action = [{
                            CompanyCode: comobj.CompanyCode, DivisionCode: comobj.DivisionCode, UnitCode: comobj.UnitCode, DepartmentId: comobj.deptId,
                            SectionId: comobj.sectionId, SubSectionId: comobj.ssectionId
                        }];
                        subSections.push(sSection[i]);
                        //$scope.insertedLine(insertedSectiondata, ssection, deptId, companyCode);
                    }
                    sectiondata[j].SubSections = subSections;
                    //delete sectiondata[j].SectionId;
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
        $http.get("/Configuration/DashboardHrfDepartmentJson", config).then(function (dept) {
            if (dept.data.length > 0 && dept.status === 200) {
                $http.get("/Configuration/DashboardHRFSectionJson", config).then(function (section) {
                    if (section.data.length > 0 && section.status === 200) {
                        $http.get("/Configuration/DashboardHrfSubSectionJson", config).then(function (subsection) {
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
        ngDialog.open({ template: "Validation_DepartmentList", controller: "ConfigurationController", className: "ngdialog-theme-default", scope: $scope });
    };
    $scope.Insert_UserDefine_Section = function (dept, section, ssection, comobj) {
        if (dept.length > 0) {
            for (var i = 0; i < dept.length; i++) {
                comobj.deptId = dept[i].DeptId;
                var insertedSectiondata = [];
                for (var j = 0; j < section.length; j++) {

                    comobj.sectionId = section[j].SectionId;
                    if (section[j].DeptId === comobj.deptId && section[j].SectionId === comobj.sectionId) {

                        insertedSectiondata.push(section[j]);
                        $scope.inserted_UserDefine_SubSection(insertedSectiondata, ssection, comobj);
                    }
                }
                for (var k = 0; k < insertedSectiondata.length; k++) {
                    delete insertedSectiondata[k].SectionId;
                    delete insertedSectiondata[k].DeptId;
                    delete insertedSectiondata[k].Onroll;
                    if (insertedSectiondata[k].SubSections.length > 0) {
                        var sSection = insertedSectiondata[k].SubSections;
                        for (var l = 0; l < sSection.length; l++) {
                            delete sSection[l].DeptId;
                            delete sSection[l].SectionId;
                            delete sSection[l].SSectionId;
                            delete sSection[l].Onroll;

                        }
                    }
                }
                dept[i].Sections = insertedSectiondata;
                delete dept[i].DeptId;
                delete dept[i].Actual;
            }
        };
        return dept;
    };
    $scope.inserted_UserDefine_SubSection = function (sectiondata, sSection, comobj) {
        if (sectiondata.length > 0 && sSection.length > 0) {

            for (var j = 0; j < sectiondata.length; j++) {
                comobj.sectionId = sectiondata[j].SectionId;
                var subSections = [];

                for (var i = 0; i < sSection.length; i++) {
                    comobj.ssectionId = sSection[i].SSectionId;
                    if (sSection[i].DeptId === comobj.deptId && sSection[i].SectionId === comobj.sectionId && sSection[i].SSectionId === comobj.ssectionId) {

                        //delete sSection[i].SSectionId;
                        sSection[i].Action = [{
                            CompanyCode: comobj.CompanyCode, DivisionCode: comobj.DivisionCode, UnitCode: comobj.UnitCode, DepartmentId: comobj.deptId,
                            SectionId: comobj.sectionId, SubSectionId: comobj.ssectionId
                        }];
                        subSections.push(sSection[i]);
                        //$scope.insertedLine(insertedSectiondata, ssection, deptId, companyCode);
                    }
                    sectiondata[j].SubSections = subSections;
                    //delete sectiondata[j].SectionId;
                }
            }
        }
        return sectiondata;
    };
    ////// End of Budget and UserDefine Data Loading ///////////


    ////// hare Onroll and UserDefine Data Loading ///////////
    $scope.OpenngDiologValidation_OnrollUserDefine_Dept = function (event) {
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
        $http.get("/Configuration/DashboardHrfDepartmentJson", config).then(function (dept) {
            if (dept.data.length > 0 && dept.status === 200) {
                $http.get("/Configuration/DashboardHRFSectionJson", config).then(function (section) {
                    if (section.data.length > 0 && section.status === 200) {
                        $http.get("/Configuration/DashboardHrfSubSectionJson", config).then(function (subsection) {
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
        ngDialog.open({ template: "Validation_DepartmentList", controller: "ConfigurationController", className: "ngdialog-theme-default", scope: $scope });
    };
    $scope.Insert_OnrollUserDefine_Section = function (dept, section, ssection, comobj) {
        if (dept.length > 0) {
            for (var i = 0; i < dept.length; i++) {
                comobj.deptId = dept[i].DeptId;
                var insertedSectiondata = [];
                for (var j = 0; j < section.length; j++) {

                    comobj.sectionId = section[j].SectionId;
                    if (section[j].DeptId === comobj.deptId && section[j].SectionId === comobj.sectionId) {

                        insertedSectiondata.push(section[j]);
                        $scope.insertedSubSection(insertedSectiondata, ssection, comobj);
                    }
                }
                for (var k = 0; k < insertedSectiondata.length; k++) {
                    delete insertedSectiondata[k].SectionId;
                    delete insertedSectiondata[k].DeptId;
                    if (insertedSectiondata[k].SubSections.length > 0) {
                        var sSection = insertedSectiondata[k].SubSections;
                        for (var l = 0; l < sSection.length; l++) {
                            delete sSection[l].DeptId;
                            delete sSection[l].SectionId;
                            delete sSection[l].SSectionId;
                        }
                    }
                }
                dept[i].Sections = insertedSectiondata;
                delete dept[i].DeptId;
            }
        };
        return dept;
    };
    $scope.inserted_OnrollUserDefine_SubSection = function (sectiondata, sSection, comobj) {
        if (sectiondata.length > 0 && sSection.length > 0) {

            for (var j = 0; j < sectiondata.length; j++) {
                comobj.sectionId = sectiondata[j].SectionId;
                var subSections = [];

                for (var i = 0; i < sSection.length; i++) {
                    comobj.ssectionId = sSection[i].SSectionId;
                    if (sSection[i].DeptId === comobj.deptId && sSection[i].SectionId === comobj.sectionId && sSection[i].SSectionId === comobj.ssectionId) {

                        //delete sSection[i].SSectionId;
                        sSection[i].Action = [{
                            CompanyCode: comobj.CompanyCode, DivisionCode: comobj.DivisionCode, UnitCode: comobj.UnitCode, DepartmentId: comobj.deptId,
                            SectionId: comobj.sectionId, SubSectionId: comobj.ssectionId
                        }];
                        subSections.push(sSection[i]);
                        //$scope.insertedLine(insertedSectiondata, ssection, deptId, companyCode);
                    }
                    sectiondata[j].SubSections = subSections;
                    //delete sectiondata[j].SectionId;
                }
            }
        }
        return sectiondata;
    };
    ////// End of Onroll and UserDefine Data Loading ///////////

    $scope.OpenngDiologValidation = function () {
        ngDialog.open({ template: "Validation_Id", controller: "ConfigurationController", className: "ngdialog-theme-default", scope: $scope });
    };

    $scope._openExcessList = function (event) {
        $scope.companycode = event.target.id;
        $scope.companyname = event.target.title;
        $rootScope.companycode = event.target.id;
        $rootScope.companyname = event.target.title;
        $scope.Excessemployees = [];
        $http.get("/Configuration/DashboardExcessEmpList?companyCode=" + $scope.companycode).then(function (response) {
            if (response.data.length > 0 && response.status === 200) {
                $scope.Excessemployees = response.data;
            }
        });
        ngDialog.open({ template: "ExcessEmpList", controller: "ConfigurationController", className: "ngdialog-theme-default", scope: $scope });

    };
    $scope._openUnallocated = function (event) {
        $scope.companycode = event.target.id;
        $scope.companyname = event.target.title;
        $rootScope.companycode = event.target.id;
        $rootScope.companyname = event.target.title;
        $scope.employees = [];
        $http.get("/Configuration/DashboardUnallocatedEmpList?companyCode=" + $scope.companycode).then(function (response) {
            if (response.data.length > 0 && response.status === 200) {
                $scope.employees = response.data;
            } else {
                $scope.UAMessage = "Data not found";
            }
        });
        ngDialog.open({ template: "UnallocatedEmpList_Table", controller: "ConfigurationController", className: "ngdialog-theme-default", scope: $scope });

    };
    $scope.ShowLineDetails = function (codes) {
        $scope.DtailsMessage = "";
        $scope.employees = [];
        codes.EmployeeType = $scope.empType;
        var config = { params: codes, headers: { 'Accept': "application/json" } };
        $http.get("/Configuration/DashboardAllocatedEmpList", config).then(function (response) {
            if (response.data.length > 0 && response.status === 200) {
                $scope.employees = response.data;
            } else {
                $scope.DtailsMessage = "Data not found";
            }
        });
        ngDialog.open({ template: "AllocatedEmpList", controller: "ConfigurationController", className: "ngdialog-theme-default", scope: $scope });

    }
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
    }

    $scope.color = { name: "all" };
    $scope.ShowMP_DirectDetails = function (event) {
        if ($scope.color.name !== undefined) {
            $scope.empType = $scope.color.name;
            openngDiologValidationUnit($scope.color.name);
        }
    };

    ///////////////// All Value Set ////////////////////


    ///////////////// Pages Render ////////////////////


    //In controller
    $scope.exportAction = function () {
        alert("hi");
        switch ($scope.export_action) {
            case "pdf": $scope.$broadcast("export-pdf", {});
                break;
            case "excel": $scope.$broadcast("export-excel", {});
                break;
            case "doc": $scope.$broadcast("export-doc", {});
                break;
            default: console.log("no event caught");
        }
    }
});

