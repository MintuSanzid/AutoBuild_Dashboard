using System;
using System.Collections.Generic;

namespace DashboardHR.Models.Models
{
    public class BpLineKpiDbObjModel 
    {
        // EeveryDay MonthName   WeekNo YearNo  FpFlag    CompanyName UnitID  UnitName TotalLine   LineName 
        // StdId StdMp StdWh StdCm  StdAvgE StdSam StdQty 
        // PlanId  OrderQty PlannedQty  ProductionQty ProductionSAM   CM 
        // RefId   ActualSAM CM  MP ActualWRPerMan ActualQty 
        // CmStdBgtVsPlan  CmPlanVsActual CmStdVsActual  
        // EStdBgtVsPlan  EPlanVsActual EStdVsActual
        // MpStdBgtVsPlan MpPlanVsActual MpStdVsActual 

        public string EeveryDay { get; set; }
        public string MonthName { get; set; }
        public string WeekNo { get; set; }
        public string YearNo { get; set; }
        public string FpFlag { get; set; }
        public string CompanyName { get; set; } 
        public string UnitName { get; set; } 
        public string LineName { get; set; }

        public string StdId { get; set; } 
        public string StdMp { get; set; }
        public string StdWh { get; set; }
        public string StdCm { get; set; }
        public string StdAvgE { get; set; }
        public string StdSam { get; set; } 
        public string StdQty { get; set; } 

        public string PlanId { get; set; }
        public string PlanMp { get; set; }
        public string PlanWh { get; set; }
        public string PlanCm { get; set; }
        public string PlanAvgE { get; set; }
        public string PlanSam { get; set; }
        public string PlanQty { get; set; } 

        public string ActualId { get; set; }
        public string ActualMp { get; set; }
        public string ActualWh { get; set; }
        public string ActualCm { get; set; }
        public string ActualAvgE { get; set; }
        public string ActualSam { get; set; }
        public string ActualQty { get; set; }

        public string CmStdBgtVsPlan { get; set; }
        public string CmPlanVsActual { get; set; }
        public string CmStdVsActual { get; set; }

        public string EStdBgtVsPlan { get; set; } 
        public string EPlanVsActual { get; set; }
        public string EStdVsActual { get; set; }

        public string MpStdBgtVsPlan { get; set; }  
        public string MpPlanVsActual { get; set; }
        public string MpStdVsActual { get; set; } 
    }

}
