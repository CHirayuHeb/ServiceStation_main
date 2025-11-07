using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceStation.Models.Table.IT
{
    [Table("rpEmail")]
    public class ViewrpEmail
    {
        [Key]
        public string emEmpcode { get; set; }
        public string emEmail { get; set; }
        public string emDeptCode { get; set; }
        public string emEmail_M365 { get; set; }
        public string emDept_M365 { get; set; }
        public string emName_M365 { get; set; }
    }

    [Table("svsMastServiceMain")]
    public class ViewsvsMastServiceMain
    {
        [Key]
        public int mmNo { get; set; }
        public string mmSecCode { get; set; }
        public string mmDecription { get; set; }
        public string mmResponsible { get; set; }
        public string mmEmpCode { get; set; }
        public string mmIssueBy { get; set; }
        public string mmUpdateBy { get; set; }
    }
    [Table("svsMastServiceSub")]
    public class ViewsvsMastServiceSub
    {
        [Key]
        public string msSecCode { get; set; }
        public string msSubNo { get; set; }
        public string msSubNameEN { get; set; }
        public string msSubNameTH { get; set; }
        public string msKosu { get; set; }
        public string msFrom { get; set; }
        public string msIssueBy { get; set; }
        public string msUpdateBy { get; set; }

    }


    //svsServiceRequest
    [Table("svsServiceRequest")]
    public class ViewsvsServiceRequest
    {
        [Key]
        public int srNo { get; set; }
        public string srServiceNo { get; set; }
        public string srRequestBy { get; set; }
        public string srRequestName { get; set; }
        public string srIntercom { get; set; }
        public string srSecCode { get; set; }
        public string srDeptCode { get; set; }
        public string srRequestDate { get; set; }
        public string srDesiredDate { get; set; }
        public string srType { get; set; }
        public string srSubject { get; set; }
        public double srKosu { get; set; }
        public string srIssueDateTime { get; set; }
        //public string srIssueDateTime1 { get; set; } //add 26/11/2024
        public string srApproveEmpcode { get; set; }
        public string srApproveName { get; set; }
        public string srFlow { get; set; }
        public int srStep { get; set; }
        public string srFrom { get; set; }

        //worker 
        public string srStatus { get; set; }
        public string srExpFinishDate { get; set; }
        public string srFinishDate { get; set; }
        public double? srTotalWorkTime { get; set; }
        //public double? srTotalWorkTime { get; set; }
        public string srSolveProblem { get; set; }


        //chirayu add 06/01/2025 name operator 
        public string srOperatorEmpcode { get; set; }
        public string srOperatorName { get; set; }

    }
    //svsGeneral
    [Table("svsGeneral")]
    public class ViewsvsGeneral
    {
        [Key]
        public int? gnNo { get; set; }
        public string gnDescription { get; set; }
        public string gnIssueBy { get; set; }
        public string gnUpdateBy { get; set; }

        public string gnType { get; set; } //chirayu add 06/02/2025
        public string gnProgramName { get; set; }  //chirayu add 06/02/2025

        public string gnProgramUser { get; set; } //chirayu add 05/11/2025
    }
    //   Attachment
    //   svsDataRestore
    //svsDataRestore_FileName
    [Table("Attachment")]
    public class ViewAttachment
    {
        [Key]
        public string fnNo { get; set; }
        public string fnPath { get; set; }
        public string fnFilename { get; set; }
        public string fnIssueBy { get; set; }
        public string fnUpdateBy { get; set; }
        public string fnType { get; set; }
        public string fnProgram { get; set; }

    }

    //svsDataRestore
    [Table("svsDataRestore")]
    public class ViewsvsDataRestore
    {
        [Key]
        public int drNo { get; set; }
        public string drDateRestore { get; set; }
        public string drCause { get; set; }
        public string drCause_RemarkOther { get; set; }
        public string drSystem { get; set; }
        public string drSys_RemarkOther { get; set; }
        public string drKeepFile { get; set; }
        public string drGroupUser { get; set; }
        public string drIssueBy { get; set; }
        public string drUpdateBy { get; set; }


    }
    //    svsDataRestore_FileName
    [Table("svsDataRestore_FileName")]
    public class ViewsvsDataRestore_FileName
    {
        [Key]
        public int rfNo { get; set; }
        public int rfFileNo { get; set; }
        public string rfFileName { get; set; }
        public string rfRemark { get; set; }


    }

    [Table("svsHistoryApproved")]
    public class ViewsvsHistoryApproved
    {
        [Key]
        public int htNo { get; set; }
        public string htSrNo { get; set; }
        public int htStep { get; set; }
        public string htStatus { get; set; }
        public string htFrom { get; set; }
        public string htTo { get; set; }
        public string htCC { get; set; }
        public string htDate { get; set; }
        public string htTime { get; set; }
        public string htRemark { get; set; }
        public string htCCDept { get; set; }


    }
    [Table("svsMastFlowApprove")]
    public class ViewsvsMastFlowApprove
    {
        [Key]
        public string mfFlowNo { get; set; }
        public int mfStep { get; set; }
        public string mfDept { get; set; }
        public string mfSubject { get; set; }
        public string mfFlowName { get; set; }
        public string mfPermission { get; set; }
        public string mfCC { get; set; }
        public string mfIssueBy { get; set; }
        public string mfUpdateBy { get; set; }



    }

    //svsNotebookSpare
    [Table("svsNotebookSpare")]
    public class ViewsvsNotebookSpare
    {
        [Key]
        public int nsNo { get; set; }
        public string nsObjective { get; set; }
        public string nsDescription { get; set; }
        public string nsObjective_Other { get; set; }
        public string nsBorrowStratDate { get; set; }
        public string nsBorrowEndDate { get; set; }
        public string nsComputerName { get; set; }
        public string nsReturnStartDate { get; set; }
        public string nsReturnEndDate { get; set; }
        public string nsIssueBy { get; set; }
        public string nsUpdateBy { get; set; }

    }
    //svsMastNotebookSpare
    [Table("svsMastNotebookSpare")]
    public class ViewsvsMastNotebookSpare
    {
        [Key]
        public string mnPCName { get; set; }
        public string mnIssueBy { get; set; }
        public string mnUpdateBy { get; set; }

    }
    //svsBorrowNotebookSpare
    [Table("svsBorrowNotebookSpare")]
    public class ViewsvsBorrowNotebookSpare
    {
        [Key]
        public string bnPCName { get; set; }
        public string bnStratDate { get; set; }
        public string bnEndDate { get; set; }
        public string bnStatus { get; set; }
    }




    [Table("svsVPN")]
    public class ViewsvsVPN
    {
        [Key]
        public int vpnNo { get; set; }
        public string vpnPCName { get; set; }
        public string vpnStatusUse { get; set; }
        public string vpnEmpCode { get; set; }
        public string vpnWork { get; set; }
        public string vpnRemark { get; set; }
        public string vpnStartDate { get; set; }
        public string vpnEndDate { get; set; }

    }


    [Table("svsSDE_SystemRegister")]
    public class ViewsvsSDE_SystemRegister
    {
        [Key]
        public int sysNo { get; set; }
        public string sysEmpCode { get; set; }
        public string sysProgramName { get; set; }
        public string sysName { get; set; }
        public string sysLastName { get; set; }
        public string sysDeptCode { get; set; }
        public string sysSectCode { get; set; }
        public string sysIntercomNo { get; set; }
        public string sysObject { get; set; }
        public string sysPermissionEditor { get; set; }
        public string sysPermissionRead { get; set; }
        public string sysPermissionDelete { get; set; }
        public string sysIssueBy { get; set; }
        public string sysUpdateBy { get; set; }
        public string sysRemark { get; set; } //add new column 21/11/2024


    }
    [Table("svsITMS_SystemRegister")]
    public class ViewsvsITMS_SystemRegister
    {
        [Key]
        public int itNo { get; set; }
        public string itEmpcode { get; set; }
        public string itObjective { get; set; }
        public bool itPcLan { get; set; }
        //public bool IsChecked { get; set; }
        public bool itMail { get; set; }
        public bool itInternet { get; set; }
        public string itPcLan_Type { get; set; }
        public string itMail_Type { get; set; }
        public bool itInternet_choice1 { get; set; }
        public bool itInternet_choice2 { get; set; }
        public bool itInternet_choice3 { get; set; }
        public bool itInternet_choice4 { get; set; }
        public bool itInternet_choice5 { get; set; }
        public bool itInternet_choice6 { get; set; }

    }


    [Table("svsRegisterUSB")]
    public class ViewsvsRegisterUSB
    {
        [Key]
        public int ubNo { get; set; }

        public string ubStatusReq { get; set; }

    }

    [Table("svsRegisterUSB_Cancel")]
    public class ViewsvsRegisterUSB_Cancel
    {
        [Key]
        public int cuCancelNo { get; set; }
        public int cuNo { get; set; }
        public string cuType { get; set; }
        public string cuUSBNo { get; set; }
        //public bool IsChecked { get; set; }
        public string cuReason { get; set; }
        public string cuReason_other { get; set; }
        public string cuIssueBy { get; set; }
        public string cuUpdateBy { get; set; }
    }
    [Table("svsRegisterUSB_New")]
    public class ViewsvsRegisterUSB_New
    {
        [Key]
        public int nuNewNo { get; set; }
        public int nuNo { get; set; }
        public string nuType { get; set; }
        public string nuEquipment { get; set; }
        public string nuObjective { get; set; }
        //public bool IsChecked { get; set; }
        public string nuCodeIncharge { get; set; }
        public string nuUserIncharge { get; set; }
        public string nuIntercomNo { get; set; }
        public string nuImage { get; set; }
        public string nuHardwareID { get; set; }
        public string nuITCode { get; set; }
        public string nuIssueBy { get; set; }
        public string nuUpdateBy { get; set; }
    }

    [Table("svsMastUSB")]
    public class ViewsvsMastUSB
    {
        [Key]
        public string muUSBName { get; set; }

        public string muIssueBy { get; set; }
        public string muUpdateBy { get; set; }
    }
    [Table("ProgramList")]
    public class ViewProgramList
    {
        [Key]
        public decimal PdNo { get; set; }
        public string PdPath { get; set; }
        public string PdPgm { get; set; }
        public string PdDsn { get; set; }
        public string PdDept { get; set; }
        public string PdDesc { get; set; }
        public string PdDeveloper { get; set; }
        public string PdReleasedate { get; set; }
        public string PdSize { get; set; }
        public string PdStatus { get; set; }
        public string pdWorking { get; set; }
        public string pdseccode { get; set; }


    }

    public class FileUpload
    {
        // public List<IFormFile> Files { get; set; }
        public List<IFormFile> FormFiles { get; set; }

    }

    public class ViewSearchData
    {
        public string v_serviceNo { get; set; }
        public string v_NameReq { get; set; }
        public string v_Dept { get; set; }
        public string v_serviceType { get; set; }
        public string v_DateRequestFrom { get; set; }
        public string v_DateRequestTo { get; set; }
        public string v_TargetDateFrom { get; set; }
        public string v_TargetDateTo { get; set; }
        public string v_Operator { get; set; }
        public string v_ApproveBy { get; set; }
        public string v_StatusService { get; set; }


    }

    public class ViewSearchMyReq
    {
        public string v_srNo { get; set; }
        public string v_Title​ { get; set; }
        public string v_Status​ { get; set; }
        public string v_DateReq { get; set; }
        public string v_DateTarget​ { get; set; }
    }

    public class searchPrg
    {
        public string program { get; set; }
    }

    public class ViewlistBorrowSpareDate
    {
        //public int v_id { get; set; }
        public string v_bnPCName { get; set; }
        public string v_bnStatus { get; set; }
        public string v_st { get; set; }
        public string v_ed { get; set; }
        public string v_1​ { get; set; }
        public string v_2 { get; set; }
        public string v_3 { get; set; }
        public string v_4​ { get; set; }
        public string v_5 { get; set; }
        public string v_6 { get; set; }
        public string v_7​ { get; set; }
        public string v_8 { get; set; }
        public string v_9 { get; set; }
        public string v_10​ { get; set; }
        public string v_11 { get; set; }
        public string v_12 { get; set; }
        public string v_13 { get; set; }
        public string v_14 { get; set; }
        public string v_15 { get; set; }
        public string v_16​ { get; set; }
        public string v_17 { get; set; }
        public string v_18 { get; set; }
        public string v_19 { get; set; }
        public string v_20 { get; set; }
        public string v_21 { get; set; }
        public string v_22 { get; set; }
        public string v_23 { get; set; }
        public string v_24 { get; set; }
        public string v_25 { get; set; }
        public string v_26 { get; set; }
        public string v_27 { get; set; }
        public string v_28 { get; set; }
        public string v_29 { get; set; }
        public string v_30 { get; set; }
        public string v_31 { get; set; }
    }

    public class ViewCodition   //Give
    {
        public string ServiceNo { get; set; }
        public string NameReq { get; set; }
        public string Dept { get; set; }
        public string ServiceType { get; set; }
        public string DateRequestFrom { get; set; }
        public string DateRequestTo { get; set; }
        public string TargetDateFrom { get; set; }
        public string TargetDateTo { get; set; }
        public string Operator { get; set; }
        public string ApproveBy { get; set; }
        public string StatusService { get; set; }
        public int page { get; set; }
        public int qrycount { get; set; }
    }

    public class ViewDetail  //Give
    {
        public int No { get; set; }
        public string ServiceNo { get; set; }
        public string RequestBy { get; set; }
        public string NameReq { get; set; }
        public string Intercom { get; set; }
        public string Sec { get; set; }
        public string Dept { get; set; }
        public string DateRequest { get; set; }
        public string Operator { get; set; }
        public string DateRequestFrom { get; set; }
        public string DateRequestTo { get; set; }
        public string TargetDateFrom { get; set; }
        public string TargetDateTo { get; set; }
        public string StatusService { get; set; }
        public string ApproveBy { get; set; }
        public string ApproveName { get; set; }
        public string Subject { get; set; }
        public int Step { get; set; }
        public string DesiredDate { get; set; }
        public string From { get; set; }
        public string Type { get; set; }
        public string Flow { get; set; }

    }

    [Table("ProgramList")]
    public class ViewPrograms
    {
        [Key]
        [Column("PdNo")]
        public decimal id { get; set; }
        [Column("PdPgm")]
        public string program_name { get; set; }
    }

}
