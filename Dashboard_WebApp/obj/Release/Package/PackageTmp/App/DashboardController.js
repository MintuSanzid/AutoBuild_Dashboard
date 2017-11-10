(function () {
    'use strict';
    var appDashboard = angular.module("appDashboard", ["ngDialog"]);
    appDashboard.controller("DashboardController", function ($scope, $rootScope, $http, $filter, $window, ngDialog) {

        $scope.companyname = "Bitopi Group (BG)";
        $scope.date = new Date();
        $scope.opening = function (employeeType) {
            $("#divLoading").show();
            var info = { EmployeeType: employeeType };
            var config = { params: info, headers: { 'Accept': "application/json" } };
            $http.get("/Dashboard/DashboardCompany", config).then(function (company) {
                if (company.data.length > 0 && company.status === 200) {
                    $scope.Companies = company.data;
                    $scope._getGroupTotalBudget(company.data);
                    $scope.GroupTotalUnallocated = $scope._getGroupTotalUnallocated(company.data);
                    $http.get("/Dashboard/DashboardDivision", config).then(function (division) {
                        if (division.data.length > 0 && division.status === 200) {
                            $scope.Divisions = division.data;

                            $http.get("/Dashboard/DashboardUnit", config).then(function (unit) {
                                if (unit.data.length > 0 && unit.status === 200) {
                                    $scope.Units = unit.data;
                                    $scope._insertDivisions();
                                }
                            });
                        }
                    });
                }
            });
            $http.get("/Dashboard/DashboardDivision", config).then(function (response) {
                if (response.data.length > 0 && response.status === 200) {
                    $scope._divisions = response.data;
                }
            });
            $http.get("/Dashboard/DashboardUnit", config).then(function (response) {
                if (response.data.length > 0 && response.status === 200) {
                    $scope._units = response.data;
                    $scope.companytotal = $scope._getcompanytotal(response.data);
                    $scope.companyUnallocated = response.data[0].Unallocated;
                    $scope.companybudgeted = $scope.companytotal + response.data[0].Unallocated;
                    $("#divLoading").hide();
                }
            });
        }
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
                            aDivision.Onroll = $scope.Divisions[j].Onroll;
                            aDivision.Shortage = $scope.Divisions[j].Shortage;
                            aDivision.Excess = $scope.Divisions[j].Excess;
                            aDivision.Present = $scope.Divisions[j].Present;
                            aDivision.Absent = $scope.Divisions[j].Absent;

                            aDivision.UserDefine = $scope.Divisions[j].UserDefine;
                            aDivision.UdShortage = $scope.Divisions[j].UdShortage;
                            aDivision.UdExcess = $scope.Divisions[j].UdExcess;
                            aDivision.UdOnShortage = $scope.Divisions[j].UdOnShortage;
                            aDivision.UdOnExcess = $scope.Divisions[j].UdOnExcess;

                            $scope._insertUnits(companycode, $scope.Divisions[j].DivisionCode);
                            aDivision.Units = $scope.aCompanyUnits;
                            aDivisions.push(aDivision);
                        }
                    }
                    $scope.Companies[i].Divisions = aDivisions;
                }
            }
        };
        $scope._insertUnits = function (companycode, divisionCode) {
            if ($scope.Units.length > 0) {
                var units = [];
                for (var i = 0; i < $scope.Units.length; i++) {
                    if (companycode === $scope.Units[i].CompanyCode && $scope.Units[i].DivisionCode === divisionCode) {
                        var aUnit = {};

                        aUnit.CompanyCode = $scope.Units[i].CompanyCode;
                        aUnit.CompanyName = $scope.Units[i].CompanyName;
                        aUnit.DivisionCode = $scope.Units[i].DivisionCode;
                        aUnit.UnitCode = $scope.Units[i].UnitCode;
                        aUnit.UnitName = $scope.Units[i].UnitName;
                        aUnit.Budget = $scope.Units[i].Budget;
                        aUnit.Onroll = $scope.Units[i].Onroll;

                        aUnit.ExcessCost = $scope.Units[i].ExcessCost;
                        aUnit.Shortage = $scope.Units[i].Shortage;
                        aUnit.Excess = $scope.Units[i].Excess;
                        aUnit.Present = $scope.Units[i].Present;
                        aUnit.Absent = $scope.Units[i].Absent;

                        aUnit.UserDefine = $scope.Units[i].UserDefine;
                        aUnit.UdExcessCost = $scope.Units[i].UdExcessCost;
                        aUnit.UdShortage = $scope.Units[i].UdShortage;
                        aUnit.UdExcess = $scope.Units[i].UdExcess;

                        aUnit.UdOnExcessCost = $scope.Units[i].UdOnExcessCost;
                        aUnit.UdOnShortage = $scope.Units[i].UdOnShortage;
                        aUnit.UdOnExcess = $scope.Units[i].UdOnExcess;

                        units.push(aUnit);
                    }
                }
                $scope.aCompanyUnits = units;
            }
        };
        $scope._getshortage = function (shortage) {
            if (shortage > 0) {
                return shortage;
            } else {
                return 0;
            }
        };
        $scope._getexcess = function (shortage) {
            if (shortage < 0) {
                return Math.abs(shortage);
            } else {
                return 0;
            }
        };
        $scope._getcompanytotal = function (data) {
            var total = 0;
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    total = total + data[i].UnitTotal;
                }
            }
            return total;
        };
        $scope._getGroupTotalBudget = function (allCompany) {
            var gtotal = 0;
            var gonroll = 0;
            var guserdefine = 0;

            var gshortage = 0;
            var gexcess = 0;
            var gexcesscost = 0;

            var gpresent = 0;
            var gabsent = 0;

            var gudshortage = 0;
            var gudexcess = 0;
            var gudexcesscost = 0;

            var gudonshortage = 0;
            var gudonexcess = 0;
            var gudonexcesscost = 0;

            for (var i = 0; i < allCompany.length; i++) {

                var total = allCompany[i].Budget;
                gtotal = parseInt(gtotal) + parseInt(total);

                var onroll = allCompany[i].Onroll;
                gonroll = parseInt(gonroll) + parseInt(onroll);

                var userdefine = allCompany[i].UserDefine;
                guserdefine = parseInt(guserdefine) + parseInt(userdefine);

                var shortage = allCompany[i].Shortage;
                gshortage = parseInt(gshortage) + parseInt(shortage);

                var excess = allCompany[i].Excess;
                gexcess = parseInt(gexcess) + parseInt(excess);

                var excesscost = allCompany[i].ExcessCost;
                gexcesscost = parseInt(gexcesscost) + parseInt(excesscost);

                var present = allCompany[i].Present;
                gpresent = parseInt(gpresent) + parseInt(present);

                var absent = allCompany[i].Absent;
                gabsent = parseInt(gabsent) + parseInt(absent);

                var udshortage = allCompany[i].UdShortage;
                gudshortage = parseInt(gudshortage) + parseInt(udshortage);

                var udexcess = allCompany[i].UdExcess;
                gudexcess = parseInt(gudexcess) + parseInt(udexcess);

                var udexcesscost = allCompany[i].UdExcessCost;
                gudexcesscost = parseInt(gudexcesscost) + parseInt(udexcesscost);

                var udonshortage = allCompany[i].UdOnShortage;
                gudonshortage = parseInt(gudonshortage) + parseInt(udonshortage);

                var udonexcess = allCompany[i].UdOnExcess;
                gudonexcess = parseInt(gudonexcess) + parseInt(udonexcess);

                var udonexcesscost = allCompany[i].UdOnExcessCost;
                gudonexcesscost = parseInt(gudonexcesscost) + parseInt(udonexcesscost);


            }
            $scope.GpBudget = gtotal;
            $scope.GpOnroll = gonroll;
            $scope.GpUserDefine = guserdefine;

            $scope.GpShortage = gshortage;
            $scope.GpExcess = gexcess;
            $scope.GpExcessCost = gexcesscost;

            $scope.GpPresent = gpresent;
            $scope.GpAbsent = gabsent;

            $scope.GpUdShortage = gudshortage;
            $scope.GpUdExcess = gudexcess;
            $scope.GpUdExcessCost = gudexcesscost;

            $scope.GpUdOnShortage = gudonshortage;
            $scope.GpUdOnExcess = gudonexcess;
            $scope.GpUdOnExcessCost = gudonexcesscost;

        };
        $scope._getGroupTotalOnroll = function (allCompany) {
            var gtotal = 0;
            for (var i = 0; i < allCompany.length; i++) {
                var total = allCompany[i].Actual;
                gtotal = parseInt(gtotal) + parseInt(total);
            }
            return gtotal;
        };
        $scope._getGroupTotalUserDefine = function (allCompany) {
            var gtotal = 0;
            for (var i = 0; i < allCompany.length; i++) {
                var total = allCompany[i].UserDefine;
                gtotal = parseInt(gtotal) + parseInt(total);
            }
            return gtotal;
        };
        $scope._getGroupTotalUnallocated = function (allCompany) {
            var gtotal = 0;
            for (var i = 0; i < allCompany.length; i++) {
                var total = allCompany[i].Unallocated;
                gtotal = parseInt(gtotal) + parseInt(total);
            }
            return gtotal;
        };


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
            $http.get("/Dashboard/DashboardDepartment", config).then(function (dept) {
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
            ngDialog.open({ template: "Validation_DepartmentList", controller: "DashboardController", className: "ngdialog-theme-default", scope: $scope });
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
                            sSection[i].Action = [
                                {
                                    CompanyCode: comobj.CompanyCode,
                                    DivisionCode: comobj.DivisionCode,
                                    UnitCode: comobj.UnitCode,
                                    DepartmentCode: comobj.departmentcode,
                                    SectionCode: comobj.sectioncode,
                                    SubSectionCode: comobj.subsectioncode
                                }
                            ];
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
            ngDialog.open({ template: "Validation_DepartmentList", controller: "DashboardController", className: "ngdialog-theme-default", scope: $scope });
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
                            sSection[i].Action = [
                                {
                                    CompanyCode: comobj.CompanyCode,
                                    DivisionCode: comobj.DivisionCode,
                                    UnitCode: comobj.UnitCode,
                                    DepartmentCode: comobj.departmentcode,
                                    SectionCode: comobj.sectioncode,
                                    SubSectionCode: comobj.subsectioncode
                                }
                            ];
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
            ngDialog.open({ template: "Validation_DepartmentList", controller: "DashboardController", className: "ngdialog-theme-default", scope: $scope });
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
                            sSection[i].Action = [
                                {
                                    CompanyCode: comobj.CompanyCode,
                                    DivisionCode: comobj.DivisionCode,
                                    UnitCode: comobj.UnitCode,
                                    DepartmentCode: comobj.departmentcode,
                                    SectionCode: comobj.sectioncode,
                                    SubSectionCode: comobj.subsectioncode
                                }
                            ];
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
            ngDialog.open({ template: "Validation_Id", controller: "DashboardController", className: "ngdialog-theme-default", scope: $scope });
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
            ngDialog.open({ template: "ExcessEmpList", controller: "DashboardController", className: "ngdialog-theme-default", scope: $scope });

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
            ngDialog.open({ template: "UnallocatedEmpList_Table", controller: "DashboardController", className: "ngdialog-theme-default", scope: $scope });

        };
        $scope.ShowLineDetails = function (codes) {
            $scope.BudgetTotal = 0;
            $scope.UDefineTotal = 0;
            $scope.OnrollTotal = 0;
            $scope.ShortTotal = 0;
            $scope.ExcessTotal = 0;

            $scope.BudgetActivityTotal = 0;
            $scope.UDefineActivityTotal = 0;
            $scope.OnrollActivityTotal = 0;
            $scope.ShortActivityTotal = 0;
            $scope.ExcessActivityTotal = 0;

            $scope.BudgetLineTotal = 0;
            $scope.UDefineLineTotal = 0;
            $scope.OnrollLineTotal = 0;
            $scope.ShortLineTotal = 0;
            $scope.ExcessLineTotal = 0;

            $scope.DtailsMessage = "";
            $scope.employees = [];
            $scope.designations = [];
            $scope.designationsofActivity = [];
            $scope.lineSummary = [];
            codes.EmployeeType = $scope.empType;
            var config = { params: codes, headers: { 'Accept': "application/json" } };
            $http.get("/Dashboard/DashboardAllocatedEmpList", config).then(function (response) {
                if (response.data.Table.length > 0 && response.status === 200) {
                    $scope.employees = response.data.Table;
                    $scope.designations = response.data.Table1;
                    $scope.designationsofActivity = response.data.Table2;
                    $scope.lineSummary = response.data.Table3;
                } else {
                    $scope.DtailsMessage = "Data not found";
                }
            });
            ngDialog.open({ template: "AllocatedEmpList", controller: "DashboardController", className: "ngdialog-theme-default", scope: $scope });

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
        $scope.preselected = function () {
            $scope.color = { name: "all" };
            $scope.empType = "all";
        }
        $scope.ShowMP_DirectDetails = function (name) {
            if (name !== undefined) {
                $scope.empType = name;
                $scope.opening(name);
            }
        };

        ///////////////// All Value Set ////////////////////
        $scope.setTotals = function (item) {
            if (item) {
                $scope.BudgetTotal += item.Budget;
                $scope.UDefineTotal += item.UDefine;
                $scope.OnrollTotal += item.Onroll;
                $scope.ShortTotal += item.Short;
                $scope.ExcessTotal += item.Excess;
                // $scope.SSecShort=
            }
        }
        $scope.setActivityTotals = function (item) {
            if (item) {
                $scope.BudgetActivityTotal += item.Budget;
                $scope.UDefineActivityTotal += item.UDefine;
                $scope.OnrollActivityTotal += item.Onroll;
                $scope.ShortActivityTotal += item.Short;
                $scope.ExcessActivityTotal += item.Excess;
                // $scope.SSecShort=
            }
        }
        $scope.setLineTotals = function (line) {
            if (line) {
                $scope.BudgetLineTotal += line.Budget;
                $scope.UDefineLineTotal += line.UDefine;
                $scope.OnrollLineTotal += line.Onroll;
                $scope.ShortLineTotal += line.Short;
                $scope.ExcessLineTotal += line.Excess;
                // $scope.SSecShort=
            }
        }
        ///////////////// Pages Render ////////////////////
    });
}());
