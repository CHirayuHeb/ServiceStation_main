using Microsoft.EntityFrameworkCore;
using ServiceStation.Models.Table.HRMS;
using ServiceStation.Models.Table.IT;
using ServiceStation.Models.Table.LAMP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceStation.Models.Common
{
    public class Class
    {
        public int PageNumber { get; set; }
        public ViewLogin _ViewLogin { get; set; }
        public Error _Error { get; set; }
        public string param { get; set; }
        public FileUpload FileUpload { get; set; }
        public string paramvrNo { get; set; }
        //HRMS
        public ViewAccEMPLOYEE _ViewAccEMPLOYEE { get; set; }
        public ViewAccPOSMAST _ViewAccPOSMAST { get; set; }
        

        //IT
        public List<ViewsvsMastServiceSub> _ListsvsMastServiceSub { get; set; }
        public List<ViewsvsMastServiceMain> _ListsvsMastServiceMain { get; set; }
        public List<ViewsvsServiceRequest> _ListsvsServiceRequest { get; set; }
        public ViewsvsServiceRequest _ViewsvsServiceRequest { get; set; }
  
     
        public ViewsvsMastFlowApprove _ViewsvsMastFlowApprove { get; set; }
        public ViewsvsHistoryApproved _ViewsvsHistoryApproved { get; set; }
        public List<ViewsvsHistoryApproved> _ListViewsvsHistoryApproved { get; set; }

        public ViewAttachment _ViewAttachment { get; set; }

        public ViewSearchData _ViewSearchData { get; set; }

        //my request
        public ViewSearchMyReq _ViewSearchMyReq { get; set; }
        
        public ViewProgramList _ViewProgramList { get; set; }

        //F5
        public ViewsvsVPN _ViewsvsVPN { get; set; }

        //F3
        public ViewsvsNotebookSpare _ViewsvsNotebookSpare { get; set; }
        public ViewsvsMastNotebookSpare _ViewsvsMastNotebookSpare { get; set; }
        public ViewsvsBorrowNotebookSpare _ViewsvsBorrowNotebookSpare { get; set; }
        public List<ViewlistBorrowSpareDate> _listViewlistBorrowSpareDate { get; set; }
        public ViewlistBorrowSpareDate _ViewlistBorrowSpareDate { get; set; }



        //F2
        public ViewsvsDataRestore _ViewsvsDataRestore { get; set; }
        public ViewsvsDataRestore_FileName _ViewsvsDataRestore_FileName { get; set; }

        //F1
        public ViewsvsGeneral _ViewsvsGeneral { get; set; }

        //F6
        //public List<ViewsvsSDE_SystemRegister> _ViewsvsSDE_SystemRegister { get; set; }
        //public DbSet<ViewsvsSDE_SystemRegister> _ViewsvsSDE_SystemRegister { get; set; }
        public List<ViewsvsSDE_SystemRegister> _ViewsvsSDE_SystemRegister { get; set; }
        public searchPrg _ViewsearchPrg { get; set; }


        //F7
        public ViewsvsITMS_SystemRegister _ViewsvsITMS_SystemRegister { get; set; }

        //F4
        public ViewsvsRegisterUSB _ViewsvsRegisterUSB { get; set; }
        public List<ViewsvsRegisterUSB_Cancel> _ViewsvsRegisterUSB_Cancel { get; set; }
        public List<ViewsvsRegisterUSB_New> _ViewsvsRegisterUSB_New { get; set; }
        public ViewsvsMastUSB _ViewsvsMastUSB { get; set; }


        //Search Data //Give
        public List<ViewDetail> dataNewRequest { get; set; }
        public List<ViewDetail> dataRequest { get; set; } //Give
        public ViewCodition _viewCondition { get; set; }

    }

    public class OTTimeStart
    {
        public string Time { get; set; }
    }
    public class OTTimeEnd
    {
        public string Time { get; set; }
    }
    public class OTModel
    {
        public string Name { get; set; }
    }
    public class OTProdLine
    {
        public string Name { get; set; }
    }
    public class OTReason
    {
        public string Code { get; set; }
        public string Caption { get; set; }
    }
    public class CCMail
    {
        public string email { get; set; }
    }

    public class req
    {
        public string no { get; set; }
    }
    public class searchbydate
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class CategoryWorkerList
    {
        public Guid Guid { get; set; }
        public byte EmpPic { get; set; }
        public string PriName { get; set; }
        public string EmpCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Job { get; set; }
        public string GRP_Code { get; set; }
    }

    public class workerImages
    {
        public string empcode { get; set; }
        public string image { get; set; }
    }
}
