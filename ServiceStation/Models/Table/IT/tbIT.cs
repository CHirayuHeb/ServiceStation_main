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

        public string mmEmpCodecc { get; set; }
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

        [NotMapped]
        public string useByemp { get; set; }
        [NotMapped]
        public string useByNickname { get; set; }
        [NotMapped]
        public string useByIntercom { get; set; }

        public string srSubjectOther { get; set; } //07/10/2026


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

    public class RegisterUSBNewViewModel
    {

        public int nuNewNo { get; set; }
        public int nuNo { get; set; }
        public string nuType { get; set; }
        public string nuEquipment { get; set; }
        public string nuObjective { get; set; }
        public string nuCodeIncharge { get; set; }
        public string nuUserIncharge { get; set; }
        public string nuIntercomNo { get; set; }
        //public string nuImage { get; set; }
        public IFormFile nuImage { get; set; }
        public string nuHardwareID { get; set; }
        public string nuITCode { get; set; }
        public string nuIssueBy { get; set; }
        public string nuUpdateBy { get; set; }
    }

    //[Table("svsServiceProject")]
    public class ViewsvsServiceProject
    {
        [Key]
        public int prNo { get; set; }
        public string prProjectName { get; set; }
        public bool prPType_Automation { get; set; }
        public bool prPType_AI { get; set; }
        public bool prPType_IoT { get; set; }
        public bool prPType_SAP { get; set; }
        public bool prPType_PLEX { get; set; }
        public bool prPType_Web { get; set; }
        public bool prPType_PowerPlatform { get; set; }
        public bool prPType_Dashboard { get; set; }
        public bool prPType_Other { get; set; }
        public string prPType_OtherText { get; set; }
        public bool prObj_ReduceManual { get; set; }
        public bool prObj_ReduceTime { get; set; }
        public bool prObj_ReduceCost { get; set; }
        public bool prObj_ReduceError { get; set; }
        public bool prObj_Efficiency { get; set; }
        public bool prObj_Compliance { get; set; }
        public bool prObj_DataReport { get; set; }
        public string prKPI { get; set; }
        public string prEffect { get; set; }
        public double? prBenefit_HoursPerMonth { get; set; }   // ลดเวลา (ชม./เดือน)
        public double? prBenefit_CostPerYear { get; set; }     // ลดต้นทุน (บาท/ปี)
        public double? prBenefit_ErrorPct { get; set; }        // ลด Error (%)

        public string prBackground { get; set; }

        public int? prCur_JobsPerMonth { get; set; }
        public double? prCur_HoursPerMonth { get; set; }
        public int? prCur_Users { get; set; }
        public int? prCur_ErrorPerMonth { get; set; }

        public string prImpactIfNot { get; set; }

        public bool prAtt_ProcessFlow { get; set; }
        public bool prAtt_SampleData { get; set; }
        public bool prAtt_Screenshot { get; set; }
        public bool prAtt_Report { get; set; }
        public bool prAtt_Other { get; set; }
        [StringLength(500)]
        public string prAtt_OtherText { get; set; }
        // ===== 4. System, Data & User Information =====
        public bool prSys_SAP { get; set; }
        public bool prSys_PLEX { get; set; }
        public bool prSys_PowerApps { get; set; }
        public bool prSys_PowerAutomate { get; set; }
        public bool prSys_PowerBI { get; set; }
        public bool prSys_SharePoint { get; set; }
        public bool prSys_M365 { get; set; }
        public bool prSys_AI { get; set; }
        public bool prSys_IoT { get; set; }
        public bool prSys_Excel { get; set; }
        public bool prSys_Other { get; set; }
        [StringLength(500)]
        public string prSys_OtherText { get; set; }
        public double? prCost_OneTime { get; set; }            // ครั้งเดียว (บาท)
        public double? prCost_PerYear { get; set; }            // ต่อเนื่อง (บาท/ปี)
        public bool prCostItem_Dev { get; set; }
        public bool prCostItem_Equip { get; set; }
        public bool prCostItem_License { get; set; }
        public bool prCostItem_Maint { get; set; }
        public bool prCostItem_Other { get; set; }
        public string prCostItem_OtherText { get; set; }
        // ===== Audit (format: yyyy/MM/dd HH:mm:ss) =====
        public string prCreatedBy { get; set; }
        public string prCreatedDate { get; set; }
        public string prUpdatedBy { get; set; }
        public string prUpdatedDate { get; set; }
    }


    [Table("svsps_project_request")]
    public class Viewsvsps_project_request
    {
        [Key]
        public long id { get; set; }                                   // bigint

        [StringLength(20)]
        public string request_no { get; set; }                         // varchar(20)

        [StringLength(300)]
        public string project_name { get; set; }                       // nvarchar(300)

        // ---- 1. Request Information ----
        [StringLength(1000)]
        public string project_types { get; set; }                      // varchar(1000)  CSV ของ code จาก PROJECT_TYPE
        [StringLength(500)]
        public string pt_other_text { get; set; }                      // nvarchar(500)

        // ---- 2. Objective & Expected Benefit ----
        [StringLength(1000)]
        public string objectives { get; set; }                         // varchar(1000)  CSV ของ code จาก OBJECTIVE
        public string expected_kpi { get; set; }                       // nvarchar(max)
        public string business_effect { get; set; }                    // nvarchar(max)

        public decimal? ben_kosu_hours_per_month { get; set; }         // decimal
        public decimal? ben_cost_saving_thb_per_year { get; set; }     // decimal
        public decimal? ben_error_reduction_pct { get; set; }          // decimal(5,?)

        // ---- 3. Business Background ----
        public string current_process_problem { get; set; }            // nvarchar(max)
        public int? cur_txn_per_month { get; set; }                    // int
        public decimal? cur_hours_per_month { get; set; }              // decimal
        public int? cur_users { get; set; }                            // int
        public int? cur_errors_per_month { get; set; }                 // int
        public string impact_if_not_done { get; set; }                 // nvarchar(max)

        [StringLength(1000)]
        public string attachment_types { get; set; }                   // varchar(1000)  CSV ของ code จาก ATTACHMENT_TYPE
        [StringLength(500)]
        public string att_other_text { get; set; }                     // nvarchar(500)
        [StringLength(1000)]
        public string attachment_folder_path { get; set; }             // nvarchar(1000)

        // ---- 4. System, Data & User ----
        [StringLength(1000)]
        public string related_systems { get; set; }                    // varchar(1000)  CSV ของ code จาก RELATED_SYSTEM
        [StringLength(500)]
        public string sys_other_text { get; set; }                     // nvarchar(500)

        public decimal? cost_one_time_thb { get; set; }                // decimal
        public decimal? cost_recurring_thb_per_year { get; set; }      // decimal
        [StringLength(1000)]
        public string cost_categories { get; set; }                    // varchar(1000)  CSV ของ code จาก COST_CATEGORY
        [StringLength(500)]
        public string cost_other_text { get; set; }                    // nvarchar(500)

        // ---- Workflow ----
        public int? status_item_id { get; set; }                       // int
        [StringLength(200)]
        public string assigned_to { get; set; }                        // nvarchar(200)
        public string reviewer_comment { get; set; }                   // nvarchar(max)
        
    }
    [Table("svsps_lookup_item")]
    public class Viewsvsps_lookup_item
    {
        [Key]
        public int id { get; set; }

        public int lookup_category_id { get; set; }
        public string code { get; set; }
        public string name_th { get; set; }
        public string name_en { get; set; }

        public int? parent_item_id { get; set; }

        public bool allow_other_text { get; set; }
        public int sort_order { get; set; }
        public bool is_active { get; set; }

        public DateTime created_at { get; set; }
        public long created_by { get; set; }

        public DateTime? updated_at { get; set; }
        public long? updated_by { get; set; }

        public bool is_deleted { get; set; }

        //[Timestamp]
        //public byte[] row_version { get; set; }
    }
    [Table("svsps_lookup_category")]
    public class Viewsvsps_lookup_category
    {
        [Key]
        public int id { get; set; }

        public string code { get; set; }
        public string name_th { get; set; }
        public string name_en { get; set; }

        public string selection_mode { get; set; }

        public bool is_required { get; set; }
        public bool is_active { get; set; }

        public DateTime created_at { get; set; }
        public long created_by { get; set; }

        public DateTime? updated_at { get; set; }
        public long? updated_by { get; set; }

        public bool is_deleted { get; set; }

        //[Timestamp]
        //public byte[] row_version { get; set; }
    }


 

    public class LookupGroupViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string SelectionMode { get; set; }
        public bool IsRequired { get; set; }

        public List<LookupItemViewModel> Items { get; set; }
            = new List<LookupItemViewModel>();
    }

    public class LookupItemViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string NameTH { get; set; }
        public string NameEN { get; set; }
        public bool AllowOtherText { get; set; }

        public bool IsSelected { get; set; }
    }

}
