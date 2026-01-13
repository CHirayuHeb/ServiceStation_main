using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using ServiceStation.Models.Common;
using ServiceStation.Models.DBConnect;
using ServiceStation.Models.MyRequest;
using ServiceStation.Models.Table.HRMS;
using ServiceStation.Models.Table.IT;
using ServiceStation.Models.Table.LAMP;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mail;

namespace ServiceStation.Controllers.RequestForm
{
    public class RequestFormController : Controller
    {
        private LAMP _LAMP;
        private HRMS _HRMS;
        private IT _IT;
        private CacheSettingController _Cache;
        private FunctionsController _callFunc;
        public string path = @"\\thsweb\\ServiceStation\\";

        [BindProperty]
        public FileUpload fileUpload { get; set; }

        //public string path = @"\\thsweb\\MAINTENANCE_MOLD\\";
        public RequestFormController(LAMP lamp, HRMS hrms, IT it, CacheSettingController cacheController, FunctionsController callfunction)
        {
            _LAMP = lamp;
            _HRMS = hrms;
            _IT = it;
            _Cache = cacheController;
            _callFunc = callfunction;
        }

        //[HttpPost]
        [Authorize("Checked")]
        public ActionResult Index(string id, string vtype, string vForm, string vTeam, string vSubject, string vSrNo)
        {

            string _UserId = User.Claims.FirstOrDefault(s => s.Type == "UserId")?.Value;

            ViewBag.vTeam = vTeam;
            ViewBag.vForm = vForm;
            ViewBag.vSubject = vSubject;
            Class @class = new Class();
            //if (vtype == "New")
            //{ }
            //string vid = "015142";

            //list in page
            //ViewBag.listAttachmentCOunt = "0";
            //ViewBag.listAttachmentCOuntWorker = "0";

            @class._ListViewsvsHistoryApproved = new List<ViewsvsHistoryApproved>();
            @class._ViewsvsGeneral = new ViewsvsGeneral();


            @class._ViewsvsRegisterUSB = new ViewsvsRegisterUSB();
            @class._ViewsvsRegisterUSB_New = new List<ViewsvsRegisterUSB_New>();
            @class._ViewsvsRegisterUSB_Cancel = new List<ViewsvsRegisterUSB_Cancel>();
            if (vForm == "F4")
            {
                //ViewsvsMastUSB
                List<ViewsvsMastUSB> _ViewsvsMastUSB = _IT.svsMastUSB.ToList();
                SelectList formMastUSB = new SelectList(_ViewsvsMastUSB.Select(s => s.muUSBName).Distinct());
                ViewBag.formMastUSB = formMastUSB;
            }
            else if (vForm == "F6" || vForm == "F1")
            {

                List<ViewProgramList> _ViewProgramList = _IT.ProgramList.Where(x => x.PdStatus == "USE" && x.pdseccode == "SDE" && x.pdWorking == null).OrderBy(x => x.PdPgm).ToList();
                SelectList formPgm = new SelectList(_ViewProgramList.Select(s => s.PdPgm).Distinct());
                ViewBag.formPgm = formPgm;


                //พนักงานภายในบริษัททั้งหมด  /  กำหนดผู้ใช้งาน  (พร้อมแนบรายชื่อผู้ใช้งาน)
                List<string> _listTypeUser = new List<string>{
                                                "พนักงานภายในบริษัททั้งหมด",
                                                "กำหนดผู้ใช้งาน (พร้อมแนบรายชื่อผู้ใช้งาน)"};
                SelectList _listofTypeUser = new SelectList(_listTypeUser);
                ViewBag.listTypeUser = _listofTypeUser;


                //string a = "";
                //try
                //{

                //}
                //catch (Exception e)
                //{
                //    a = e.Message;
                //}

                // var a = _IT.ProgramList.Where(x => x.PdStatus == "USE").FirstOrDefault();

                //List<ViewProgramList> _ViewProgramList = _IT.ProgramList.Where(x => x.PdStatus == "USE").ToList();
                //SelectList formPgm = new SelectList(_ViewProgramList.Select(s => s.PdPgm).Distinct());
                //ViewBag.formPgm = formPgm;
            }




            ViewAccEMPLOYEE vAcc = new ViewAccEMPLOYEE();
            if (vSrNo == null)
            {
                if (id != null)
                {
                    @class._ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == id).FirstOrDefault();
                    @class._ViewsvsServiceRequest = new ViewsvsServiceRequest();
                    // @class._ViewsvsServiceRequest.srNo = ;
                    @class._ViewsvsServiceRequest.srServiceNo = "";
                    @class._ViewsvsServiceRequest.srRequestBy = @class._ViewAccEMPLOYEE.EMP_CODE;
                    @class._ViewsvsServiceRequest.srRequestName = @class._ViewAccEMPLOYEE.NICKNAME;
                    @class._ViewsvsServiceRequest.srIntercom = @class._ViewAccEMPLOYEE.INTERCOMNO;
                    @class._ViewsvsServiceRequest.srSecCode = @class._ViewAccEMPLOYEE.SEC_CODE;
                    @class._ViewsvsServiceRequest.srDeptCode = @class._ViewAccEMPLOYEE.DEPT_CODE;
                    @class._ViewsvsServiceRequest.srRequestDate = DateTime.Now.ToString("yyyy/MM/dd");
                    //_svsServiceRequest.srDesiredDate = @class._ViewAccEMPLOYEE.DEPT_CODE;
                    @class._ViewsvsServiceRequest.srType = vTeam;
                    @class._ViewsvsServiceRequest.srSubject = vSubject;
                    @class._ViewsvsServiceRequest.srFrom = vForm;
                    // @class._ViewsvsServiceRequest.srKosu = 12;

                    // @class._ViewsvsGeneral = new ViewsvsGeneral();
                    // @class._ViewsvsGeneral.gnDescription = "test";

                    //@class._ViewsvsDataRestore = new ViewsvsDataRestore();
                    //@class._ViewsvsDataRestore.drSys_PCLan = 1;
                }
            }
            else
            {
                @class._ViewsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srNo == int.Parse(vSrNo)).FirstOrDefault();
                @class._ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == @class._ViewsvsServiceRequest.srRequestBy).FirstOrDefault();

                //F1
                if (vForm == "F1")
                {
                    @class._ViewsvsGeneral = _IT.svsGeneral.Where(x => x.gnNo == int.Parse(vSrNo)).FirstOrDefault();
                    ViewBag.gnType = @class._ViewsvsGeneral.gnType;


                }

                //F2
                if (vForm == "F2")
                {
                    @class._ViewsvsDataRestore = _IT.svsDataRestore.Where(x => x.drNo == int.Parse(vSrNo)).FirstOrDefault();

                }
                //F3
                if (vForm == "F3")
                {
                    List<ViewsvsMastNotebookSpare> _ViewsvsMastNotebookSpare = _IT.svsMastNotebookSpare.OrderBy(x => x.mnPCName).ToList();
                    SelectList formMastNotebook = new SelectList(_ViewsvsMastNotebookSpare.Select(s => s.mnPCName).Distinct());
                    ViewBag.formMastNotebook = formMastNotebook;

                    @class._ViewsvsNotebookSpare = _IT.svsNotebookSpare.Where(x => x.nsNo == int.Parse(vSrNo)).FirstOrDefault();

                }


                //USB
                //@class._ViewsvsRegisterUSB = new ViewsvsRegisterUSB();
                if (vForm == "F4")
                {

                    @class._ViewsvsRegisterUSB = _IT.svsRegisterUSB.Where(x => x.ubNo == int.Parse(vSrNo)).FirstOrDefault();

                    @class._ViewsvsRegisterUSB_Cancel = _IT.svsRegisterUSB_Cancel.Where(x => x.cuNo == int.Parse(vSrNo)).ToList();

                    @class._ViewsvsRegisterUSB_New = _IT.svsRegisterUSB_New.Where(x => x.nuNo == int.Parse(vSrNo)).ToList();
                    ViewBag.Obstatus = @class._ViewsvsRegisterUSB.ubStatusReq;

                }

                //F5 VPN
                if (vForm == "F5")
                {
                    @class._ViewsvsVPN = _IT.svsVPN.Where(x => x.vpnNo == int.Parse(vSrNo)).FirstOrDefault();
                }

                //F6 User Register Application
                if (vForm == "F6")
                {

                    @class._ViewsvsSDE_SystemRegister = _IT.svsSDE_SystemRegister.Where(x => x.sysNo == int.Parse(vSrNo)).ToList();


                }

                if (vForm == "F7")
                {
                    @class._ViewsvsITMS_SystemRegister = _IT.svsITMS_SystemRegister.Where(x => x.itNo == int.Parse(vSrNo)).FirstOrDefault();

                    if (@class._ViewsvsITMS_SystemRegister != null)
                    {
                        ViewAccEMPLOYEE _ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == @class._ViewsvsITMS_SystemRegister.itEmpcode.Trim()).FirstOrDefault();
                        // @class._ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == @class._ViewsvsITMS_SystemRegister.itEmpcode).FirstOrDefault();
                        if (_ViewAccEMPLOYEE != null)
                        {
                            ViewBag.Name = _ViewAccEMPLOYEE.EMP_TNAME;
                            ViewBag.lname = _ViewAccEMPLOYEE.LAST_TNAME;
                            ViewBag.Dep = _ViewAccEMPLOYEE.DEPT_CODE;
                            ViewBag.Intercom = _ViewAccEMPLOYEE.INTERCOMNO;
                        }



                    }





                }

                if (@class._ViewsvsServiceRequest.srStep > 2)
                {
                    var _EmailCS = _IT.svsHistoryApproved.Where(x => x.htSrNo == @class._ViewsvsServiceRequest.srNo.ToString() && x.htStep == 3).Select(x => x.htFrom).FirstOrDefault();
                    var _empcs = _IT.rpEmails.Where(x => x.emEmail_M365 == _EmailCS).Select(x => x.emEmpcode).FirstOrDefault();

                    if (_empcs == _UserId && @class._ViewsvsServiceRequest.srStep < 4)
                    {
                        List<ViewsvsMastFlowApprove> _listStatus = _IT.svsMastFlowApprove.Where(x => x.mfStep == 3).OrderBy(x => x.mfDept).Distinct().ToList();
                        SelectList formfStatus = new SelectList(_listStatus.Select(s => s.mfSubject).Distinct());
                        ViewBag.vbformfStatus = formfStatus;
                    }
                    else
                    {
                        List<ViewsvsMastFlowApprove> _listStatus = _IT.svsMastFlowApprove.Where(x => x.mfStep == 3 && int.Parse(x.mfDept) != 1).OrderBy(x => x.mfDept).Distinct().ToList();
                        SelectList formfStatus = new SelectList(_listStatus.Select(s => s.mfSubject).Distinct());
                        ViewBag.vbformfStatus = formfStatus;
                    }


                    ViewBag._empcs = _empcs;
                }





                List<ViewAttachment> _ViewAttachment = _IT.Attachment.Where(x => x.fnNo == vSrNo.ToString() && x.fnType != "Worker" && x.fnProgram == "ServiceStation").ToList();
                ViewBag.listAttachment = _ViewAttachment.ToList();
                ViewBag.listAttachmentCOunt = _ViewAttachment.Count();

                List<ViewAttachment> _ViewAttachmentWorker = _IT.Attachment.Where(x => x.fnNo == vSrNo.ToString() && x.fnType == "Worker" && x.fnProgram == "ServiceStation").ToList();
                ViewBag.listAttachmentWorker = _ViewAttachmentWorker.ToList();
                ViewBag.listAttachmentCOuntWorker = _ViewAttachmentWorker.Count();


                //add history 27/11/2024 14:53
                List<ViewsvsHistoryApproved> _listHistory = new List<ViewsvsHistoryApproved>();
                _listHistory = _IT.svsHistoryApproved.Where(x => x.htSrNo == vSrNo).ToList();
                _listHistory = _listHistory.OrderBy(x => x.htStep).ToList();
                ViewBag._listHistory = _listHistory.ToList();



                @class._ListViewsvsHistoryApproved = _IT.svsHistoryApproved.Where(x => x.htSrNo == vSrNo).ToList();


            }

            //return RedirectToAction("Index", "RequestForm", new { @class = @class });
            return View(@class);

        }


        public string fcheck(string vForm)
        {
            Class @class = new Class();
            if (vForm == "F4")
            {
                @class._ViewsvsRegisterUSB = _IT.svsRegisterUSB.Where(x => x.ubNo == 85).FirstOrDefault();
                ViewBag.Obstatus = @class._ViewsvsRegisterUSB.ubStatusReq;
                return ViewBag.Obstatus;

            }
            return "";
        }


        public ActionResult LoadAccData(string id, string vtype)
        {
            Class @class = new Class();
            ViewAccEMPLOYEE vAcc = new ViewAccEMPLOYEE();
            if (id != null)
            {
                @class._ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == id).FirstOrDefault();

            }
            //return Json("");
            return View("Index", @class);
            // return Json(new { c1 = @class });
        }




        [HttpPost]
        public JsonResult History_Inform(Class @classs, string vform)//string getID)
        {
            //Class @class ,
            string partialUrl = "";
            string v_status = "";
            if (@classs._ViewsvsServiceRequest.srStatus != null)
            {
                v_status = @classs._ViewsvsServiceRequest.srStatus;
                //partialUrl = Url.Action("SendMailWorker", "RequestForm");
            }
            else
            {
                // partialUrl = Url.Action("SendMail", "RequestForm", new { @class = @classs });
            }
            List<ViewsvsHistoryApproved> _listHistory = new List<ViewsvsHistoryApproved>();

            partialUrl = Url.Action("SendMail_Inform", "RequestForm", new { @class = @classs });
            //partialUrl = Url.Action("SendMail", "RequestForm");
            //string partialUrl = Url.Action("SendMail", "RequestForm");
            if (@classs._ViewsvsServiceRequest != null)
            {
                String hisId = @classs._ViewsvsServiceRequest.srNo.ToString();
                //ViewcpNewRequest recentStep = _HRMS.cpNewRequest.Find(hisId, getIssue);
                // ViewsvsServiceRequest _ViewsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srNo == int.Parse(hisId)).FirstOrDefault();
                _listHistory = _IT.svsHistoryApproved.Where(x => x.htSrNo == hisId).ToList();
                _listHistory = _listHistory.OrderBy(x => x.htStep).ToList();
                return Json(new { status = "hasHistory", listHistory = _listHistory, partial = partialUrl });
            }

            return Json(new { status = "empty", listHistory = _listHistory, partial = partialUrl });
        }
        public ActionResult SendMail_Inform(Class @classs, string vform)
        {
            int docno = @classs._ViewsvsServiceRequest.srNo;
            var vSR = @classs._ViewsvsServiceRequest.srServiceNo != null ? @classs._ViewsvsServiceRequest.srServiceNo : "wait";


            // var vSR = @classs._ViewsvsServiceRequest.srServiceNo != null ? @classs._ViewsvsServiceRequest.srServiceNo : "wait";
            @classs._ViewsvsHistoryApproved = new ViewsvsHistoryApproved();
            @classs._ViewsvsHistoryApproved.htTo = "";

            @classs._ViewsvsHistoryApproved.htStatus = "Approve";

            string vIssue = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var v_emailFrom = _IT.rpEmails.Where(x => x.emEmpcode == vIssue).Select(p => p.emName_M365).FirstOrDefault(); //chg to m365
            @classs._ViewsvsHistoryApproved.htFrom = v_emailFrom;


            //var v_Status = _IT.svsServiceRequest.Where(x => x.srNo == docno ).Select(x => x.srs).FirstOrDefault();
            // int v_Step = _IT.svsServiceRequest.Where(x => x.srNo == docno).Select(x => x.srStep).FirstOrDefault();

            // var v_From = _IT.svsHistoryApproved
            //.Where(x => x.htSrNo == docno.ToString() && x.htStep == v_Step)
            //.GroupBy(x => x.htSrNo == docno.ToString() && x.htStep == v_Step)
            //.Select(x => x.OrderByDescending(t => t.htNo)
            //.First());
            // var v_Issue = _IT.rpEmails.Where(x => x.emEmpcode == vIssue).Select(x => x.emName_M365).FirstOrDefault();
            // if (v_From.Count() == 0)
            // {
            //     @classs._ViewsvsHistoryApproved.htFrom = v_Issue;
            // }
            // else
            // {
            //     var vhtfrom = v_From.Select(x => x.htTo).First();
            //     @classs._ViewsvsHistoryApproved.htFrom = _IT.rpEmails.Where(x => x.emEmail_M365 == vhtfrom).Select(x => x.emName_M365).FirstOrDefault();

            //     if (@classs._ViewsvsServiceRequest.srStep == 1)
            //     {
            //         ViewsvsMastServiceMain _svsData = new ViewsvsMastServiceMain();
            //         _svsData = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).FirstOrDefault();
            //         //var vNicnameCSIT = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).Select(x => x.mmResponsible).FirstOrDefault();
            //         var vEMPCsIT = _HRMS.AccEMPLOYEE.Where(x => x.NICKNAME == _svsData.mmResponsible && x.EMP_CODE == _svsData.mmEmpCode).Select(x => x.EMP_CODE).FirstOrDefault();
            //         @classs._ViewsvsHistoryApproved.htTo = _IT.rpEmails.Where(x => x.emEmpcode == vEMPCsIT).Select(x => x.emName_M365).FirstOrDefault(); ;

            //     }

            //     else if (@classs._ViewsvsServiceRequest.srStep == 2)
            //     {
            //         var v_srNo = _IT.svsServiceRequest.Where(x => x.srNo == docno && x.srServiceNo != "wait").Select(s => s.srServiceNo).FirstOrDefault();
            //         if (v_srNo != null)
            //         {
            //             vSR = v_srNo;
            //         }
            //         else
            //         {
            //             //var max = v_srNo.
            //             var max0 = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait").FirstOrDefault();
            //             //var max1 = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait") != null ? _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait" && x.srServiceNo != null).OrderByDescending(p => p.srServiceNo.Substring(4, 5)).Select(p => p.srServiceNo.Substring(4, 5)).First() : null;
            //             //var max = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait" && x.srServiceNo !=null).OrderByDescending(p => p.srServiceNo.Substring(4, 5)).Select(p => p.srServiceNo.Substring(4, 5)).First();

            //             //var max = _IT.svsServiceRequest.Where(x=>x.srServiceNo != "wait").Select(p => p.srServiceNo.Substring(4, 5)).FirstOrDefault(); //_IT.svsServiceRequest.Where(x => x != null && x.srServiceNo != "wait").Select(p => p.srServiceNo.Substring(2, 4)).FirstOrDefault();
            //             if (max0 != null)
            //             {
            //                 var max = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait" && x.srServiceNo != null).OrderByDescending(p => p.srServiceNo.Substring(4, 5)).Select(p => p.srServiceNo.Substring(4, 5)).First();
            //                 vSR = "SR" + DateTime.Now.ToString("yy") + String.Format("{0:D5}", (int.Parse(max) + 1));
            //             }
            //             else
            //             {
            //                 vSR = "SR" + DateTime.Now.ToString("yy") + "00001";
            //             }

            //         }

            //         @classs._ViewsvsServiceRequest.srServiceNo = vSR;

            //     }
            //     //else if (vform == "F3" && @classs._ViewsvsServiceRequest.srStep == 3)
            //     //{

            //     //}
            //     else if (@classs._ViewsvsServiceRequest.srStep == 3 && (@classs._ViewsvsServiceRequest.srStatus.Contains("Done") || @classs._ViewsvsServiceRequest.srStatus.Contains("Cancel")))
            //     {

            //         //var vNicnameIssue = _IT.svsServiceRequest.Where(x => x.srNo == docno).Select(x => x.srRequestName).FirstOrDefault();
            //         ////var vNicnameIssue = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).Select(x => x.mmResponsible).FirstOrDefault();
            //         //var vEMPissue = _HRMS.AccEMPLOYEE.Where(x => x.NICKNAME == vNicnameIssue && x.QUIT_CODE == null).Select(x => x.EMP_CODE).FirstOrDefault();
            //         //@classs._ViewsvsHistoryApproved.htTo = _IT.rpEmails.Where(x => x.emEmpcode == vEMPissue).Select(x => x.emName_M365).FirstOrDefault();


            //     }


            // }
            // //}
            var vNicnameIssue = _IT.svsServiceRequest.Where(x => x.srNo == docno).Select(x => x.srRequestName).FirstOrDefault();
            //var vNicnameIssue = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).Select(x => x.mmResponsible).FirstOrDefault();
            var vEMPissue = _HRMS.AccEMPLOYEE.Where(x => x.NICKNAME == vNicnameIssue && x.QUIT_CODE == null).Select(x => x.EMP_CODE).FirstOrDefault();
            @classs._ViewsvsHistoryApproved.htTo = _IT.rpEmails.Where(x => x.emEmpcode == vEMPissue).Select(x => x.emName_M365).FirstOrDefault();




            @classs._ViewsvsHistoryApproved.htStatus = "Approve";
            ViewBag.vWststus = @classs._ViewsvsServiceRequest.srStatus;
            ViewBag.vForm = vform != null ? vform : @classs._ViewsvsServiceRequest.srFrom;
            ViewBag.SRno = vSR;
            ViewBag.vDate = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
            return PartialView("SendMail_inform", @classs);
        }


        public JsonResult SendMail_post_Inform(Class @class, List<IFormFile> files, string vform, string vSR, List<IFormFile> files0, List<IFormFile> files1, List<IFormFile> files2, List<IFormFile> files3, List<IFormFile> files4, List<IFormFile> files5, List<IFormFile> files6, List<IFormFile> files7, List<IFormFile> files8, List<IFormFile> files9, List<IFormFile> files10)
        {
            int[] getSrNo;

            string[] chkPermis;



            string vCCemail = "";
            string config = "S";
            string msg = "Send Mail already ";
            int i_Step = @class._ViewsvsServiceRequest.srStep;
            // files._items.count

            chkPermis = chkPermission(@class);
            if (chkPermis[0] == "Yes")
            {
                if (@class._ViewsvsHistoryApproved.htTo != null)
                {
                    config = "S";

                }
                else
                {
                    config = "E";
                    msg = "Please input e-mail.";
                }
            }
            else //No
            {
                config = "P";
                msg = chkPermis[1];
            }




            if (config == "S")
            {
                string UpdateBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

                int vsrNo = @class._ViewsvsServiceRequest.srNo;
                string vmsg = "";
                using (var dbContextTransaction = _IT.Database.BeginTransaction())
                {
                    try
                    {
                        ViewsvsNotebookSpare _ViewsvsNotebookSpare = _IT.svsNotebookSpare.Where(x => x.nsNo == vsrNo).FirstOrDefault();

                        //update

                        _ViewsvsNotebookSpare.nsNo = vsrNo;
                        _ViewsvsNotebookSpare.nsObjective = @class._ViewsvsNotebookSpare.nsObjective;
                        _ViewsvsNotebookSpare.nsDescription = @class._ViewsvsNotebookSpare.nsDescription;
                        _ViewsvsNotebookSpare.nsObjective_Other = @class._ViewsvsNotebookSpare.nsObjective_Other;
                        _ViewsvsNotebookSpare.nsBorrowStratDate = @class._ViewsvsNotebookSpare.nsBorrowStratDate;
                        _ViewsvsNotebookSpare.nsBorrowEndDate = @class._ViewsvsNotebookSpare.nsBorrowEndDate;
                        _ViewsvsNotebookSpare.nsComputerName = @class._ViewsvsNotebookSpare.nsComputerName;
                        _ViewsvsNotebookSpare.nsReturnStartDate = @class._ViewsvsNotebookSpare.nsReturnStartDate;
                        _ViewsvsNotebookSpare.nsReturnEndDate = @class._ViewsvsNotebookSpare.nsReturnEndDate;
                        // _ViewsvsNotebookSpare.nsIssueBy = @class._ViewsvsNotebookSpare.nsIssueBy;
                        _ViewsvsNotebookSpare.nsUpdateBy = UpdateBy;
                        _IT.svsNotebookSpare.Update(_ViewsvsNotebookSpare);
                        _IT.SaveChanges();


                        ViewsvsBorrowNotebookSpare _svsBorrowNotebookSpare = _IT.svsBorrowNotebookSpare.Where(x => x.bnPCName == @class._ViewsvsNotebookSpare.nsComputerName && x.bnStratDate == @class._ViewsvsNotebookSpare.nsReturnStartDate && x.bnEndDate == @class._ViewsvsNotebookSpare.nsReturnEndDate && x.bnStatus == "Y").FirstOrDefault();
                        if (_svsBorrowNotebookSpare != null)
                        {
                            _svsBorrowNotebookSpare.bnPCName = @class._ViewsvsNotebookSpare.nsComputerName;
                            _svsBorrowNotebookSpare.bnStratDate = @class._ViewsvsNotebookSpare.nsReturnStartDate;
                            _svsBorrowNotebookSpare.bnEndDate = @class._ViewsvsNotebookSpare.nsReturnEndDate;
                            _svsBorrowNotebookSpare.bnStatus = "Y";
                            _IT.svsBorrowNotebookSpare.Update(_svsBorrowNotebookSpare);
                            _IT.SaveChanges();
                        }
                        else
                        {

                            ViewsvsBorrowNotebookSpare _ViewsvsBorrowNotebookSpare = new ViewsvsBorrowNotebookSpare();
                            _ViewsvsBorrowNotebookSpare.bnPCName = @class._ViewsvsNotebookSpare.nsComputerName;
                            _ViewsvsBorrowNotebookSpare.bnStratDate = @class._ViewsvsNotebookSpare.nsReturnStartDate;
                            _ViewsvsBorrowNotebookSpare.bnEndDate = @class._ViewsvsNotebookSpare.nsReturnEndDate;
                            _ViewsvsBorrowNotebookSpare.bnStatus = "Y";
                            _IT.svsBorrowNotebookSpare.Add(_ViewsvsBorrowNotebookSpare);
                            _IT.SaveChanges();
                        }





                        vmsg = "Insert success";
                        dbContextTransaction.Commit();

                    }
                    catch (Exception e)
                    {
                        vmsg = "Insert Fail !!!!!" + e.Message;
                        dbContextTransaction.Rollback();
                    }
                }







                //var v_subject = "";

                var email = new MimeMessage();


                ViewrpEmail fromEmailFrom = _IT.rpEmails.Where(w => w.emName_M365 == @class._ViewsvsHistoryApproved.htFrom).FirstOrDefault();
                ViewrpEmail fromEmailTO = _IT.rpEmails.Where(w => w.emName_M365 == @class._ViewsvsHistoryApproved.htTo).FirstOrDefault();

                MailboxAddress FromMailFrom = new MailboxAddress(fromEmailFrom.emName_M365, fromEmailFrom.emEmail_M365);
                MailboxAddress FromMailTO = new MailboxAddress(fromEmailTO.emName_M365, fromEmailTO.emEmail_M365);
                email.Subject = "Service Station Request==>  ส่งแจ้งรายละเอียด Notebook Spare​ "; /*( " + _ViewlrBuiltDrawing.bdDocumentType + " ) " + _ViewlrHistoryApprove.htStatus*/;
                //email.From.Add(MailboxAddress.Parse(_ViewlrHistoryApprove.htFrom));
                email.From.Add(FromMailFrom);
                email.To.Add(FromMailTO);

                if (@class._ViewsvsHistoryApproved.htCC != null)
                {
                    string[] splitCC = @class._ViewsvsHistoryApproved.htCC.Split(',');
                    foreach (var i in splitCC)
                    {
                        if (i != " " & i != "")
                        {
                            string ccEmail = _IT.rpEmails.Where(w => w.emName_M365 == i).Select(s => s.emEmail_M365).FirstOrDefault().ToString();
                            email.Cc.Add(MailboxAddress.Parse(ccEmail));
                            vCCemail = ccEmail + ",";
                        }
                    }
                }
                //insert into HistoryApproved


                //var varifyUrl = "http://thsweb/MVCPublish/MoldMaintenance/Home/Login?mode=edit&DocumentNo=" + getDocNO[1] + "&MoldNo=" + @class.ViewRequest.v_moldNo_Name + "&UserID=" + v_ApproveBy + "&Plant=" + vplant + "&Date=" + @class.ViewRequest.v_month;
                //"RequestForm?id=" + vId + "&vtype=" + getEvent + "&vForm=" + vForm + "&vTeam=" + vTeam + "&vSubject=" + vSubject + "&vSrNo=" + vSrNo;
                // onclick="GoNewRequest('@item.srRequestBy','Edit','@Url.Action("Index", "RequestForm")','@item.srFrom','@item.srType','@item.srSubject','@item.srNo')"
                var varifyUrl = "http://thsweb/MVCPublish/ServiceStation/Login/index?vSrNo=" + @class._ViewsvsServiceRequest.srNo;
                var bodyBuilder = new BodyBuilder();
                //var image = bodyBuilder.LinkedResources.Add(@"E:\01_My Document\02_Project\_2023\1. PartTransferUnbalance\PartTransferUnbalance\wwwroot\images\btn\OK.png");
                string vIssue = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                string vIssueName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor)?.Value;
                string EmailBody = $"<div>" +
                    $"<B>Service Station </B> <br>" +
                    $"<B>Service No : </B> " + @class._ViewsvsServiceRequest.srServiceNo + "<br>" +
                    $"<B>Request By : </B> " + vIssue + " : " + vIssueName + "<br>" +
                    $"<B>Subject : </B> ส่งแจ้งรายละเอียด Notebook Spare​ <br>" +
                    $"<B>PC name Spare : </B>" + @class._ViewsvsNotebookSpare.nsComputerName + "<br>" +
                    $"<B> วันที่ยืม : </B> -->" + @class._ViewsvsNotebookSpare.nsReturnStartDate + "<br> " +
                    $"<B> วันที่คืน : </B> -->" + @class._ViewsvsNotebookSpare.nsReturnEndDate + "<br> " +
                    $"<B> หมายเหตุ : </B> -->" + @class._ViewsvsHistoryApproved.htRemark + "<br> " +
                    $"คลิ๊กลิงค์เพื่อเปิดเอกสาร <a href='" + varifyUrl + "'>More Detail" +
                    //$"<img src = 'http://thsweb/MVCPublish/LR_Service_Request/images/btn/mail1.png' alt = 'HTML tutorial' style = 'width: 42px; height: 42px;'>" +
                    $"</a>" +
                    $"</div>";

                // bodyBuilder.Attachments.Add(@"E:\01_My Document\02_Project\_2023\1. PartTransferUnbalance\PartTransferUnbalance\dev_rfc.log");

                bodyBuilder.HtmlBody = string.Format(EmailBody);
                email.Body = bodyBuilder.ToMessageBody();

                // send email
                //var smtp = new SmtpClient();
                ////smtp.Connect("mail.csloxinfo.com");
                //smtp.Connect("203.146.237.138");
                ////smtp.Connect("10.200.128.12");
                //smtp.Send(email);
                //smtp.Disconnect(true);


                var senderEmail = new MailAddress(fromEmailFrom.emEmail_M365, fromEmailFrom.emName_M365);
                var receiverEmail = new MailAddress(fromEmailTO.emEmail_M365, fromEmailTO.emName_M365);


                System.Net.Mime.ContentType mimeTypeS = new System.Net.Mime.ContentType("text/html");
                AlternateView alternate = AlternateView.CreateAlternateViewFromString(EmailBody, mimeTypeS);
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.csloxinfo.com");
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                using (MailMessage mess = new MailMessage(senderEmail, receiverEmail))
                {
                    mess.Subject = "Service Station Request==>  ส่งแจ้งรายละเอียด Notebook Spare​ ";
                    //add CC
                    if (@class._ViewsvsHistoryApproved.htCC != null)
                    {
                        string[] splitCC = @class._ViewsvsHistoryApproved.htCC.Split(',');
                        foreach (var i in splitCC)
                        {
                            if (i != " " & i != "")
                            {
                                string ccEmail = _IT.rpEmails.Where(w => w.emName_M365 == i).Select(s => s.emEmail_M365).FirstOrDefault().ToString();
                                //email.Cc.Add(MailboxAddress.Parse(ccEmail));
                                //vCCemail = ccEmail + ",";
                                mess.CC.Add(ccEmail);
                            }
                        }
                    }



                    mess.AlternateViews.Add(alternate);
                    smtp.Send(mess);
                }




                return Json(new { c1 = config, c2 = msg, c3 = "" });
            }
            else if (config == "P")
            {
                config = "P";
                msg = msg;
                return Json(new { c1 = config, c2 = msg });
            }
            else
            {
                config = "E";
                msg = msg;
                return Json(new { c1 = config, c2 = msg });
            }


            //getSForm

        }



        [HttpPost]
        public JsonResult History(Class @classs, string vform)//string getID)
        {
            //Class @class ,
            string partialUrl = "";
            string v_status = "";
            if (@classs._ViewsvsServiceRequest.srStatus != null)
            {
                v_status = @classs._ViewsvsServiceRequest.srStatus;
                //partialUrl = Url.Action("SendMailWorker", "RequestForm");
            }
            else
            {
                // partialUrl = Url.Action("SendMail", "RequestForm", new { @class = @classs });
            }
            List<ViewsvsHistoryApproved> _listHistory = new List<ViewsvsHistoryApproved>();

            partialUrl = Url.Action("SendMail", "RequestForm", new { @class = @classs });
            //partialUrl = Url.Action("SendMail", "RequestForm");
            //string partialUrl = Url.Action("SendMail", "RequestForm");
            if (@classs._ViewsvsServiceRequest != null)
            {
                String hisId = @classs._ViewsvsServiceRequest.srNo.ToString();
                //ViewcpNewRequest recentStep = _HRMS.cpNewRequest.Find(hisId, getIssue);
                // ViewsvsServiceRequest _ViewsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srNo == int.Parse(hisId)).FirstOrDefault();
                _listHistory = _IT.svsHistoryApproved.Where(x => x.htSrNo == hisId).ToList();
                _listHistory = _listHistory.OrderBy(x => x.htStep).ToList();

                string NameIssue = "";
                if ((@classs._ViewsvsServiceRequest.srStep == 4) && (@classs._ViewsvsServiceRequest.srStatus.Contains("Done") || @classs._ViewsvsServiceRequest.srStatus.Contains("Cancel")))
                {

                    var empIssue = _IT.svsServiceRequest.Where(x => x.srNo == @classs._ViewsvsServiceRequest.srNo).Select(x => x.srRequestBy).FirstOrDefault();
                    //var vNicnameIssue = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).Select(x => x.mmResponsible).FirstOrDefault();
                    var vEMPissue = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == empIssue && x.QUIT_CODE == null).Select(x => x.EMP_CODE).FirstOrDefault();
                    NameIssue = _IT.rpEmails.Where(x => x.emEmpcode == empIssue).Select(x => x.emName_M365).FirstOrDefault();


                }
                else if (@classs._ViewsvsServiceRequest.srStep == 3 && @classs._ViewsvsServiceRequest.srStatus != "Transfer")
                {


                    var _EmailCS = _IT.svsHistoryApproved.Where(x => x.htSrNo == @classs._ViewsvsServiceRequest.srNo.ToString() && x.htStep == 3).Select(x => x.htFrom).FirstOrDefault();
                    var _empcs = _IT.rpEmails.Where(x => x.emEmail_M365 == _EmailCS).Select(x => x.emEmpcode).FirstOrDefault();
                    NameIssue = _IT.rpEmails.Where(x => x.emEmpcode == _empcs).Select(x => x.emName_M365).FirstOrDefault();
                }
                else if (@classs._ViewsvsServiceRequest.srStep == 1)
                {
                    ViewsvsMastServiceMain _svsData = new ViewsvsMastServiceMain();
                    _svsData = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).FirstOrDefault();
                    //var vNicnameCSIT = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).Select(x => x.mmResponsible).FirstOrDefault();
                    var vEMPCsIT = _HRMS.AccEMPLOYEE.Where(x => x.NICKNAME == _svsData.mmResponsible && x.EMP_CODE == _svsData.mmEmpCode).Select(x => x.EMP_CODE).FirstOrDefault();
                    NameIssue = _IT.rpEmails.Where(x => x.emEmpcode == vEMPCsIT).Select(x => x.emName_M365).FirstOrDefault(); ;

                }





                return Json(new { status = "hasHistory", listHistory = _listHistory, partial = partialUrl, NameIssue = NameIssue });
            }

            return Json(new { status = "empty", listHistory = _listHistory, partial = partialUrl });
        }



        [Authorize("Checked")]
        //[HttpPost]
        public ActionResult SendMail(Class @classs, string vform)
        {
            int docno = @classs._ViewsvsServiceRequest.srNo;
            var vSR = @classs._ViewsvsServiceRequest.srServiceNo != null ? @classs._ViewsvsServiceRequest.srServiceNo : "wait";
            //F3 step working spare
            //if (vform == "F3" && @classs._ViewsvsServiceRequest.srStep ==3)
            //{
            //    var v_empOperator = _IT.svsServiceRequest.Where(x => x.srNo == docno).Select(x => x.srApproveEmpcode).FirstOrDefault();
            //    var v_emailFrom1 = _IT.rpEmails.Where(x => x.emEmpcode == v_empOperator).Select(p => p.emName_M365).FirstOrDefault();
            //    @classs._ViewsvsHistoryApproved.htFrom = v_emailFrom1;

            //    var vNicnameIssue = _IT.svsServiceRequest.Where(x => x.srNo == docno).Select(x => x.srRequestName).FirstOrDefault();
            //    //var vNicnameIssue = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).Select(x => x.mmResponsible).FirstOrDefault();
            //    var vEMPissue = _HRMS.AccEMPLOYEE.Where(x => x.NICKNAME == vNicnameIssue && x.QUIT_CODE == null).Select(x => x.EMP_CODE).FirstOrDefault();
            //    @classs._ViewsvsHistoryApproved.htTo = _IT.rpEmails.Where(x => x.emEmpcode == vEMPissue).Select(x => x.emName_M365).FirstOrDefault();

            //}
            //else
            //{

            // var vSR = @classs._ViewsvsServiceRequest.srServiceNo != null ? @classs._ViewsvsServiceRequest.srServiceNo : "wait";
            @classs._ViewsvsHistoryApproved = new ViewsvsHistoryApproved();
            @classs._ViewsvsHistoryApproved.htTo = "";

            @classs._ViewsvsHistoryApproved.htStatus = "Approve";

            string vIssue = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;


            string _UserId = User.Claims.FirstOrDefault(s => s.Type == "UserId")?.Value;
            @classs._ViewsvsHistoryApproved.htFrom = _IT.rpEmails.Where(x => x.emEmpcode == _UserId).Select(x => x.emName_M365).FirstOrDefault(); ;


            //var v_emailFrom = _IT.rpEmails.Where(x => x.emEmpcode == vIssue).Select(p => p.emName_M365).FirstOrDefault(); //chg to m365
            //@classs._ViewsvsHistoryApproved.htFrom = v_emailFrom;


            //var v_Status = _IT.svsServiceRequest.Where(x => x.srNo == docno ).Select(x => x.srs).FirstOrDefault();
            int v_Step = _IT.svsServiceRequest.Where(x => x.srNo == docno).Select(x => x.srStep).FirstOrDefault();

            var v_From = _IT.svsHistoryApproved
           .Where(x => x.htSrNo == docno.ToString() && x.htStep == v_Step)
           .GroupBy(x => x.htSrNo == docno.ToString() && x.htStep == v_Step)
           .Select(x => x.OrderByDescending(t => t.htNo)
           .First());
            var v_Issue = _IT.rpEmails.Where(x => x.emEmpcode == vIssue).Select(x => x.emName_M365).FirstOrDefault();
            if (v_From.Count() == 0)
            {
                @classs._ViewsvsHistoryApproved.htFrom = v_Issue;
            }
            else
            {
                //var vhtfrom = v_From.Select(x => x.htTo).First();
                //@classs._ViewsvsHistoryApproved.htFrom = _IT.rpEmails.Where(x => x.emEmail_M365 == vhtfrom).Select(x => x.emName_M365).FirstOrDefault();

                if (@classs._ViewsvsServiceRequest.srStep == 1)
                {
                    ViewsvsMastServiceMain _svsData = new ViewsvsMastServiceMain();
                    _svsData = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).FirstOrDefault();
                    //var vNicnameCSIT = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).Select(x => x.mmResponsible).FirstOrDefault();
                    var vEMPCsIT = _HRMS.AccEMPLOYEE.Where(x => x.NICKNAME == _svsData.mmResponsible && x.EMP_CODE == _svsData.mmEmpCode).Select(x => x.EMP_CODE).FirstOrDefault();
                    @classs._ViewsvsHistoryApproved.htTo = _IT.rpEmails.Where(x => x.emEmpcode == vEMPCsIT).Select(x => x.emName_M365).FirstOrDefault(); ;

                }

                else if (@classs._ViewsvsServiceRequest.srStep == 2)
                {
                    var v_srNo = _IT.svsServiceRequest.Where(x => x.srNo == docno && x.srServiceNo != "wait").Select(s => s.srServiceNo).FirstOrDefault();
                    if (v_srNo != null)
                    {
                        vSR = v_srNo;
                    }
                    else
                    {
                        //var max = v_srNo.
                        var max0 = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait").FirstOrDefault();
                        //var max1 = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait") != null ? _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait" && x.srServiceNo != null).OrderByDescending(p => p.srServiceNo.Substring(4, 5)).Select(p => p.srServiceNo.Substring(4, 5)).First() : null;
                        //var max = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait" && x.srServiceNo !=null).OrderByDescending(p => p.srServiceNo.Substring(4, 5)).Select(p => p.srServiceNo.Substring(4, 5)).First();

                        //var max = _IT.svsServiceRequest.Where(x=>x.srServiceNo != "wait").Select(p => p.srServiceNo.Substring(4, 5)).FirstOrDefault(); //_IT.svsServiceRequest.Where(x => x != null && x.srServiceNo != "wait").Select(p => p.srServiceNo.Substring(2, 4)).FirstOrDefault();
                        if (max0 != null)
                        {
                            var max = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait" && x.srServiceNo != null).OrderByDescending(p => p.srServiceNo.Substring(4, 5)).Select(p => p.srServiceNo.Substring(4, 5)).First();
                            vSR = "SR" + DateTime.Now.ToString("yy") + String.Format("{0:D5}", (int.Parse(max) + 1));
                        }
                        else
                        {
                            vSR = "SR" + DateTime.Now.ToString("yy") + "00001";
                        }

                    }

                    @classs._ViewsvsServiceRequest.srServiceNo = vSR;

                }
                //else if (vform == "F3" && @classs._ViewsvsServiceRequest.srStep == 3)
                //{

                //}
                else if (@classs._ViewsvsServiceRequest.srStep == 3 && (@classs._ViewsvsServiceRequest.srStatus.Contains("Done") || @classs._ViewsvsServiceRequest.srStatus.Contains("Cancel")))
                {

                    var vNicnameIssue = _IT.svsServiceRequest.Where(x => x.srNo == docno).Select(x => x.srRequestName).FirstOrDefault();
                    //var vNicnameIssue = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).Select(x => x.mmResponsible).FirstOrDefault();
                    var vEMPissue = _HRMS.AccEMPLOYEE.Where(x => x.NICKNAME == vNicnameIssue && x.QUIT_CODE == null).Select(x => x.EMP_CODE).FirstOrDefault();
                    @classs._ViewsvsHistoryApproved.htTo = _IT.rpEmails.Where(x => x.emEmpcode == vEMPissue).Select(x => x.emName_M365).FirstOrDefault();


                }
                else if (@classs._ViewsvsServiceRequest.srStep == 3 && @classs._ViewsvsServiceRequest.srStatus.Contains("Transfer"))
                {
                    @classs._ViewsvsHistoryApproved.htTo = "";
                }


            }
            //}



            @classs._ViewsvsHistoryApproved.htStatus = "Approve";
            ViewBag.vWststus = @classs._ViewsvsServiceRequest.srStatus;
            ViewBag.vForm = vform != null ? vform : @classs._ViewsvsServiceRequest.srFrom;
            ViewBag.SRno = vSR;
            ViewBag.vDate = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
            return PartialView("SendMail", @classs);
        }

        //[HttpPost]
        public ActionResult SendMailWorker(Class @classs, string vform)
        {

            var vSR = @classs._ViewsvsServiceRequest.srServiceNo != null ? @classs._ViewsvsServiceRequest.srServiceNo : "wait";
            @classs._ViewsvsHistoryApproved = new ViewsvsHistoryApproved();
            @classs._ViewsvsHistoryApproved.htTo = "";

            @classs._ViewsvsHistoryApproved.htStatus = "Approve";

            string vIssue = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var v_emailFrom = _IT.rpEmails.Where(x => x.emEmpcode == vIssue).Select(p => p.emName_M365).FirstOrDefault(); //chg to m365
            @classs._ViewsvsHistoryApproved.htFrom = v_emailFrom;

            int docno = @classs._ViewsvsServiceRequest.srNo;
            //var v_Status = _IT.svsServiceRequest.Where(x => x.srNo == docno ).Select(x => x.srs).FirstOrDefault();
            int v_Step = _IT.svsServiceRequest.Where(x => x.srNo == docno).Select(x => x.srStep).FirstOrDefault();

            var v_From = _IT.svsHistoryApproved
           .Where(x => x.htSrNo == docno.ToString() && x.htStep == v_Step)
           .GroupBy(x => x.htSrNo == docno.ToString() && x.htStep == v_Step)
           .Select(x => x.OrderByDescending(t => t.htNo)
           .First());
            var v_Issue = _IT.rpEmails.Where(x => x.emEmpcode == vIssue).Select(x => x.emName_M365).FirstOrDefault();
            if (v_From.Count() == 0)
            {
                @classs._ViewsvsHistoryApproved.htFrom = v_Issue;
            }
            else
            {
                var vhtfrom = v_From.Select(x => x.htTo).First();
                @classs._ViewsvsHistoryApproved.htFrom = _IT.rpEmails.Where(x => x.emEmail_M365 == vhtfrom).Select(x => x.emName_M365).FirstOrDefault();

                if (@classs._ViewsvsServiceRequest.srStep == 1)
                {
                    ViewsvsMastServiceMain _svsData = new ViewsvsMastServiceMain();
                    _svsData = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).FirstOrDefault();
                    //var vNicnameCSIT = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).Select(x => x.mmResponsible).FirstOrDefault();
                    var vEMPCsIT = _HRMS.AccEMPLOYEE.Where(x => x.NICKNAME == _svsData.mmResponsible && x.EMP_CODE == _svsData.mmEmpCode).Select(x => x.EMP_CODE).FirstOrDefault();
                    @classs._ViewsvsHistoryApproved.htTo = _IT.rpEmails.Where(x => x.emEmpcode == vEMPCsIT).Select(x => x.emName_M365).FirstOrDefault(); ;

                }

                else if (@classs._ViewsvsServiceRequest.srStep == 2)
                {
                    var v_srNo = _IT.svsServiceRequest.Where(x => x.srNo == docno && x.srServiceNo != "wait").Select(s => s.srServiceNo).FirstOrDefault();
                    if (v_srNo != null)
                    {
                        vSR = v_srNo;
                    }
                    else
                    {
                        //var max = v_srNo.
                        var max0 = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait").FirstOrDefault();
                        //var max1 = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait") != null ? _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait" && x.srServiceNo != null).OrderByDescending(p => p.srServiceNo.Substring(4, 5)).Select(p => p.srServiceNo.Substring(4, 5)).First() : null;
                        //var max = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait" && x.srServiceNo !=null).OrderByDescending(p => p.srServiceNo.Substring(4, 5)).Select(p => p.srServiceNo.Substring(4, 5)).First();

                        //var max = _IT.svsServiceRequest.Where(x=>x.srServiceNo != "wait").Select(p => p.srServiceNo.Substring(4, 5)).FirstOrDefault(); //_IT.svsServiceRequest.Where(x => x != null && x.srServiceNo != "wait").Select(p => p.srServiceNo.Substring(2, 4)).FirstOrDefault();
                        if (max0 != null)
                        {
                            var max = _IT.svsServiceRequest.Where(x => x.srServiceNo != "wait" && x.srServiceNo != null).OrderByDescending(p => p.srServiceNo.Substring(4, 5)).Select(p => p.srServiceNo.Substring(4, 5)).First();
                            vSR = "SR" + DateTime.Now.ToString("yy") + String.Format("{0:D5}", (int.Parse(max) + 1));
                        }
                        else
                        {
                            vSR = "SR" + DateTime.Now.ToString("yy") + "00001";
                        }

                    }

                    @classs._ViewsvsServiceRequest.srServiceNo = vSR;

                }
                else if (@classs._ViewsvsServiceRequest.srStep == 3 && (@classs._ViewsvsServiceRequest.srStatus.Contains("Done") || @classs._ViewsvsServiceRequest.srStatus.Contains("Cancel")))
                {

                    var vNicnameIssue = _IT.svsServiceRequest.Where(x => x.srNo == docno).Select(x => x.srRequestName).FirstOrDefault();
                    //var vNicnameIssue = _IT.svsMastServiceMain.Where(x => x.mmSecCode == @classs._ViewsvsServiceRequest.srType).Select(x => x.mmResponsible).FirstOrDefault();
                    var vEMPissue = _HRMS.AccEMPLOYEE.Where(x => x.NICKNAME == vNicnameIssue && x.QUIT_CODE == null).Select(x => x.EMP_CODE).FirstOrDefault();
                    @classs._ViewsvsHistoryApproved.htTo = _IT.rpEmails.Where(x => x.emEmpcode == vEMPissue).Select(x => x.emName_M365).FirstOrDefault();


                }


            }



            //if (@classs._ViewsvsServiceRequest.srServiceNo is null)
            //{
            //    var v_emailFrom = _IT.rpEmails.Where(x => x.emEmpcode == @classs._ViewsvsServiceRequest.srRequestBy.ToString()).Select(p => p.emName_M365).FirstOrDefault(); //chg to m365
            //    @classs._ViewsvsHistoryApproved.htFrom = v_emailFrom;
            //}

            @classs._ViewsvsHistoryApproved.htStatus = "Approve";
            ViewBag.vWststus = @classs._ViewsvsServiceRequest.srStatus;
            ViewBag.vForm = vform;
            ViewBag.SRno = vSR;
            ViewBag.vDate = DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
            return PartialView("SendMail_Woker", @classs);
        }
        public ActionResult Search(string term)
        {
            {
                return Json(_IT.rpEmails.Where(p => p.emName_M365.Contains(term)).Select(p => p.emEmail_M365 + "|" + p.emName_M365).ToList());
                //return Json(_IT.rpEmails.Where(p => p.emEmail.Contains(term) || p.emEmail_M365.Contains(term)).Select(p => p.emEmail_M365).ToList());
            }
        }
        public JsonResult SendMail_post(Class @class, List<IFormFile> files, List<IFormFile> filesw, string vform, string vSR, List<IFormFile> files0, List<IFormFile> files1, List<IFormFile> files2, List<IFormFile> files3, List<IFormFile> files4, List<IFormFile> files5, List<IFormFile> files6, List<IFormFile> files7, List<IFormFile> files8, List<IFormFile> files9, List<IFormFile> files10)
        {
            int[] getSrNo;
            string getSForm;
            string getWForm;
            string[] fsavefileUser;
            string[] fsavefileWorker;
            string[] chkPermis;

            //for test
            string[] fsavefile0;
            string[] fsavefile1;
            string[] fsavefile2;
            string[] fsavefile3;
            string[] fsavefile4;
            string[] fsavefile5;
            string[] fsavefile6;
            string[] fsavefile7;
            string[] fsavefile8;
            string[] fsavefile9;
            string fsavefile10 = "";


            string vCCemail = "";
            string config = "S";
            string msg = "Send Mail already ";
            int i_Step = @class._ViewsvsServiceRequest.srStep;
            // files._items.count
            string vSrSubject = @class._ViewsvsServiceRequest.srSubject;

            chkPermis = chkPermission(@class);
            if (chkPermis[0] == "Yes")
            {
                if (@class._ViewsvsHistoryApproved.htTo != null || (@class._ViewsvsHistoryApproved.htTo == null && @class._ViewsvsHistoryApproved.htStatus == "Disapprove") || (@class._ViewsvsHistoryApproved.htTo == null && @class._ViewsvsHistoryApproved.htStatus == "Cancel"))
                {
                    if (@class._ViewsvsHistoryApproved.htStatus == "Approve")
                    {
                        i_Step = i_Step + 1;
                        config = "S";

                    }
                    else if (@class._ViewsvsHistoryApproved.htStatus == "Disapprove" || @class._ViewsvsHistoryApproved.htStatus == "Cancel")
                    {
                        i_Step = 0;
                        var v_Issue = _IT.rpEmails.Where(x => x.emEmpcode == @class._ViewsvsServiceRequest.srRequestBy).Select(x => x.emName_M365).First(); // m365
                        @class._ViewsvsHistoryApproved.htTo = v_Issue;
                        config = "S";

                    }
                    else
                    {
                        config = "E";
                        msg = "Please input Status";
                    }

                }
                else
                {
                    config = "E";
                    msg = "Please input e-mail.";
                }
            }
            else //No
            {
                config = "P";
                msg = chkPermis[1];
            }

            //check vpn step 1 cs up 13 / 01 / 2024 if ((vform == "F5" || vSrSubject.Contains("Print Color") || vSrSubject.Contains("Admin Access Request")) && i_Step == 1)
            if ((vform == "F5" || vSrSubject.Contains("Print Color") || vSrSubject.Contains("Admin Access Request")) && i_Step == 1)
            {
                //check emp positon
                try
                {
                    string v_POS_HCM_CODE = "";
                    string v_empcsup = _IT.rpEmails.Where(w => w.emName_M365 == @class._ViewsvsHistoryApproved.htTo).Select(x => x.emEmpcode).FirstOrDefault();

                    string _Divicheck = User.Claims.FirstOrDefault(s => s.Type == "Division")?.Value;
                    if (_Divicheck.Contains("SL"))
                    {
                        //check case SL ddm
                        v_POS_HCM_CODE = _HRMS.AccPOSMAST.Where(x => x.POS_CODE == "DDM").Select(x => x.POS_HCM_CODE).FirstOrDefault();
                    }
                    else
                    {
                        //normal case
                        v_POS_HCM_CODE = _HRMS.AccPOSMAST.Where(x => x.POS_CODE == "DM").Select(x => x.POS_HCM_CODE).FirstOrDefault();
                    }


                    ViewAccEMPLOYEE _ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == v_empcsup).FirstOrDefault();
                    List<ViewAccPOSMAST> _ViewAccPOSMAST = _HRMS.AccPOSMAST.Where(x => int.Parse(x.POS_HCM_CODE) <= int.Parse(v_POS_HCM_CODE)).ToList();
                    string v_chk = _ViewAccPOSMAST.Where(x => x.POS_CODE == _ViewAccEMPLOYEE.POS_CODE).Select(x => x.POS_CODE).FirstOrDefault();

                    if (v_chk == null || v_chk == "")
                    {
                        config = "W";
                        msg = "Please send approval to DM Up of Dept.!!!";
                    }

                }
                catch (Exception ex)
                {
                    config = "E";
                    msg = "Please check email send to !!!!";
                }

            }
            //check emp positon CS up 07/02/2025  chirayu add
            else if (i_Step == 1)
            {
                try
                {
                    string v_POS_HCM_CODE = "";
                    string v_empcsup = _IT.rpEmails.Where(w => w.emName_M365 == @class._ViewsvsHistoryApproved.htTo).Select(x => x.emEmpcode).FirstOrDefault();
                    string _Divicheck = User.Claims.FirstOrDefault(s => s.Type == "Division")?.Value;
                    v_POS_HCM_CODE = _HRMS.AccPOSMAST.Where(x => x.POS_CODE == "GL").Select(x => x.POS_HCM_CODE).FirstOrDefault();
                    ViewAccEMPLOYEE _ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == v_empcsup).FirstOrDefault();
                    List<ViewAccPOSMAST> _ViewAccPOSMAST = _HRMS.AccPOSMAST.Where(x => int.Parse(x.POS_HCM_CODE) < int.Parse(v_POS_HCM_CODE)).ToList();
                    string v_chk = _ViewAccPOSMAST.Where(x => x.POS_CODE == _ViewAccEMPLOYEE.POS_CODE).Select(x => x.POS_CODE).FirstOrDefault();

                    if (v_chk == null || v_chk == "")
                    {
                        config = "E";
                        msg = "Please send approval to CS Up of Dept.!!!";
                    }

                }
                catch (Exception ex)
                {
                    config = "E";
                    msg = "Please check email send to !!!!";
                }
            }



            if (config == "S")
            {
                //check status Transfer
                if (@class._ViewsvsServiceRequest.srStatus != null)
                {
                    if (@class._ViewsvsServiceRequest.srStatus == "Transfer")
                    {
                        i_Step = i_Step - 1;
                    }
                }

                @class._ViewsvsServiceRequest.srServiceNo = vSR;
                getSrNo = Save(@class, i_Step);  //save main
                getSForm = SaveForm(@class, vform, getSrNo[0]); //save form

                if (@class._ViewsvsServiceRequest.srStatus != null && @class._ViewsvsServiceRequest.srStatus != "") //save form worker
                {
                    getWForm = SaveFormWorker(@class, vform, getSrNo[0]);
                }

                fsavefileUser = save_file(@class, files, getSrNo[0], ""); // save file
                fsavefileWorker = save_file(@class, filesw, getSrNo[0], "Worker"); // save file



                //subject check
                var v_subject = "";
                if (@class._ViewsvsServiceRequest.srStatus != null)
                {
                    if (@class._ViewsvsServiceRequest.srStatus == "Transfer")
                    {
                        v_subject = _IT.svsMastFlowApprove.Where(x => x.mfStep == i_Step && x.mfDept == "1").Select(x => x.mfSubject).FirstOrDefault();
                    }
                    else if (@class._ViewsvsServiceRequest.srStatus == "Cancel")
                    {
                        v_subject = _IT.svsMastFlowApprove.Where(x => x.mfStep == 3 && x.mfDept == "3").Select(x => x.mfSubject).FirstOrDefault();
                    }
                    else
                    {
                        v_subject = i_Step == 0 ? "Disapprove" : _IT.svsMastFlowApprove.Where(x => x.mfStep == i_Step).Select(x => x.mfSubject).FirstOrDefault();
                    }
                }
                else
                {
                    v_subject = i_Step == 0 ? "Disapprove" : _IT.svsMastFlowApprove.Where(x => x.mfStep == i_Step).Select(x => x.mfSubject).FirstOrDefault();

                }

                var email = new MimeMessage();
                ViewrpEmail fromEmailFrom = _IT.rpEmails.Where(w => w.emName_M365 == @class._ViewsvsHistoryApproved.htFrom).FirstOrDefault();
                ViewrpEmail fromEmailTO = _IT.rpEmails.Where(w => w.emName_M365 == @class._ViewsvsHistoryApproved.htTo).FirstOrDefault();

                MailboxAddress FromMailFrom = new MailboxAddress(fromEmailFrom.emName_M365, fromEmailFrom.emEmail_M365);
                MailboxAddress FromMailTO = new MailboxAddress(fromEmailTO.emName_M365, fromEmailTO.emEmail_M365);
                email.Subject = "Service Station Request==> " + v_subject; /*( " + _ViewlrBuiltDrawing.bdDocumentType + " ) " + _ViewlrHistoryApprove.htStatus*/;
                //email.From.Add(MailboxAddress.Parse(_ViewlrHistoryApprove.htFrom));
                email.From.Add(FromMailFrom);
                email.To.Add(FromMailTO);


                if (@class._ViewsvsHistoryApproved.htCC != null)
                {
                    ViewrpEmail fromEmailCC = new ViewrpEmail();
                    string[] splitCC = @class._ViewsvsHistoryApproved.htCC.Split(',');
                    foreach (var i in splitCC)
                    {
                        if (i != " " & i != "")
                        {
                            var v_cc = "";
                            try
                            {
                                fromEmailCC = _IT.rpEmails.Where(w => w.emName_M365 == i).FirstOrDefault();
                                MailboxAddress FromMailcc = new MailboxAddress(fromEmailCC.emName_M365, fromEmailCC.emEmail_M365);
                                email.Cc.Add(FromMailcc);
                                vCCemail += fromEmailCC.emEmail_M365.ToString() + ",";
                            }
                            catch (Exception e)
                            {
                                v_cc = e.Message;
                            }



                        }
                    }
                }



                //try
                //{
                ViewsvsHistoryApproved _svsHistoryApproved = new ViewsvsHistoryApproved();
                _svsHistoryApproved.htSrNo = getSrNo[0].ToString();
                _svsHistoryApproved.htStep = i_Step;
                _svsHistoryApproved.htStatus = @class._ViewsvsHistoryApproved.htStatus;
                _svsHistoryApproved.htFrom = fromEmailFrom.emEmail_M365;
                _svsHistoryApproved.htTo = fromEmailTO.emEmail_M365;
                _svsHistoryApproved.htCC = vCCemail;
                _svsHistoryApproved.htDate = DateTime.Now.ToString("yyyy/MM/dd");
                _svsHistoryApproved.htTime = DateTime.Now.ToString("HH:mm:ss");
                _svsHistoryApproved.htRemark = @class._ViewsvsHistoryApproved.htRemark;
                _svsHistoryApproved.htCCDept = "";

                _IT.svsHistoryApproved.AddAsync(_svsHistoryApproved);
                _IT.SaveChanges();




                if (vform == "F4")
                {
                    //remove
                    var _DataAttachment = _IT.Attachment.Where(c => c.fnNo == getSrNo[0].ToString());
                    _IT.Attachment.RemoveRange(_DataAttachment);
                    _IT.SaveChanges();

                    if (files0.Count > 0)
                    {
                        fsavefile0 = save_file_F4(@class, getSrNo[0], files0, 1);
                        fsavefile10 = fsavefile10 + "F0" + fsavefile0[0];
                    }
                    if (files1.Count > 0)
                    {
                        fsavefile1 = save_file_F4(@class, getSrNo[0], files1, 2);
                        fsavefile10 = fsavefile10 + "F1" + fsavefile1[0];
                    }
                    if (files2.Count > 0)
                    {
                        fsavefile2 = save_file_F4(@class, getSrNo[0], files2, 3);
                        fsavefile10 = fsavefile10 + "F2" + fsavefile2[0];
                    }
                    if (files3.Count > 0)
                    {
                        fsavefile3 = save_file_F4(@class, getSrNo[0], files3, 4);
                        fsavefile10 = fsavefile10 + "F3" + fsavefile3[0];
                    }
                    if (files4.Count > 0)
                    {
                        fsavefile4 = save_file_F4(@class, getSrNo[0], files4, 5);
                        fsavefile10 = fsavefile10 + "F4" + fsavefile4[0];
                    }
                    if (files5.Count > 0)
                    {
                        fsavefile5 = save_file_F4(@class, getSrNo[0], files5, 6);
                        fsavefile10 = fsavefile10 + "F5" + fsavefile5[0];
                    }
                    if (files6.Count > 0) { fsavefile6 = save_file_F4(@class, getSrNo[0], files6, 7); }
                    if (files7.Count > 0) { fsavefile7 = save_file_F4(@class, getSrNo[0], files7, 8); }
                    if (files8.Count > 0) { fsavefile8 = save_file_F4(@class, getSrNo[0], files8, 9); }
                    if (files9.Count > 0) { fsavefile9 = save_file_F4(@class, getSrNo[0], files9, 10); }
                    //if (files10.Count > 0) { fsavefile10 = save_file_F4(@class, getSrNo[0], files10, 11); }

                    //fsavefile10 = "F1" + fsavefile0[0] + "F2" + fsavefile1 + "F3" + fsavefile2 + "F4" + fsavefile3 + "F5" + fsavefile4;

                    // fsavefile = save_fileF4(@class, getSrNo[0], files0, files1, files2, files3, files4, files5, files6, files7, files8, files9, files10); // save file
                }






                // v_subject = i_Step == 0 ? "Disapprove" : _IT.svsMastFlowApprove.Where(x => x.mfStep == i_Step).Select(x => x.mfSubject).FirstOrDefault();









                var varifyUrl = "http://thsweb/MVCPublish/ServiceStation/Login/index?vSrNo=" + getSrNo[0].ToString();
                var bodyBuilder = new BodyBuilder();
                //var image = bodyBuilder.LinkedResources.Add(@"E:\01_My Document\02_Project\_2023\1. PartTransferUnbalance\PartTransferUnbalance\wwwroot\images\btn\OK.png");
                string vIssue = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                string vIssueName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor)?.Value;
                string EmailBody = $"<div>" +
                    $"<B>Service Station </B> <br>" +
                    $"<B>Service No : </B> " + @class._ViewsvsServiceRequest.srServiceNo + "<br>" +
                    $"<B>Request By : </B> " + @class._ViewsvsServiceRequest.srRequestBy + " : " + @class._ViewsvsServiceRequest.srRequestName + "<br>" +
                    $"<B>Subject : </B>" + @class._ViewsvsServiceRequest.srSubject + "<br>" +
                    $"<B>Status : </B> " + v_subject + "<br> " +
                    $"<B> หมายเหตุ : </B> " + @class._ViewsvsHistoryApproved.htRemark + "<br> " +
                    $"คลิ๊กลิงค์เพื่อเปิดเอกสาร <a href='" + varifyUrl + "'>More Detail" +
                    //$"<img src = 'http://thsweb/MVCPublish/LR_Service_Request/images/btn/mail1.png' alt = 'HTML tutorial' style = 'width: 42px; height: 42px;'>" +
                    $"</a>" +
                    $"</div>";

                // bodyBuilder.Attachments.Add(@"E:\01_My Document\02_Project\_2023\1. PartTransferUnbalance\PartTransferUnbalance\dev_rfc.log");

                bodyBuilder.HtmlBody = string.Format(EmailBody);
                email.Body = bodyBuilder.ToMessageBody();

                // send email
                //var smtp = new SmtpClient();
                ////smtp.Connect("mail.csloxinfo.com");
                //smtp.Connect("203.146.237.138");
                ////smtp.Connect("10.200.128.12");s
                //smtp.Send(email);
                //smtp.Disconnect(true);

                var senderEmail = new MailAddress(fromEmailFrom.emEmail_M365, fromEmailFrom.emName_M365);
                var receiverEmail = new MailAddress(fromEmailTO.emEmail_M365, fromEmailTO.emName_M365);


                System.Net.Mime.ContentType mimeTypeS = new System.Net.Mime.ContentType("text/html");
                AlternateView alternate = AlternateView.CreateAlternateViewFromString(EmailBody, mimeTypeS);
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.csloxinfo.com");
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


                using (MailMessage mess = new MailMessage(senderEmail, receiverEmail))
                {
                    mess.Subject = "Service Station Request==> " + v_subject;
                    //add CC
                    if (@class._ViewsvsHistoryApproved.htCC != null)
                    {
                        ViewrpEmail fromEmailCC = new ViewrpEmail();
                        string[] splitCC = @class._ViewsvsHistoryApproved.htCC.Split(',');
                        foreach (var i in splitCC)
                        {
                            if (i != " " & i != "")
                            {
                                var v_cc = "";
                                try
                                {
                                    fromEmailCC = _IT.rpEmails.Where(w => w.emName_M365 == i).FirstOrDefault();
                                    //MailboxAddress FromMailcc = new MailboxAddress(fromEmailCC.emName_M365, fromEmailCC.emEmail_M365);
                                    //email.Cc.Add(FromMailcc);
                                    //vCCemail += fromEmailCC.emEmail_M365.ToString() + ",";
                                    mess.CC.Add(fromEmailCC.emEmail_M365);
                                }
                                catch (Exception e)
                                {
                                    v_cc = e.Message;
                                }

                            }
                        }
                    }

                    mess.AlternateViews.Add(alternate);
                    smtp.Send(mess);
                }





                //insert into HistoryApproved

                ////try
                ////{
                //ViewsvsHistoryApproved _svsHistoryApproved = new ViewsvsHistoryApproved();
                //_svsHistoryApproved.htSrNo = getSrNo[0].ToString();
                //_svsHistoryApproved.htStep = i_Step;
                //_svsHistoryApproved.htStatus = @class._ViewsvsHistoryApproved.htStatus;
                //_svsHistoryApproved.htFrom = fromEmailFrom.emEmail_M365;
                //_svsHistoryApproved.htTo = fromEmailTO.emEmail_M365;
                //_svsHistoryApproved.htCC = vCCemail;
                //_svsHistoryApproved.htDate = DateTime.Now.ToString("yyyy/MM/dd");
                //_svsHistoryApproved.htTime = DateTime.Now.ToString("HH:mm:ss");
                //_svsHistoryApproved.htRemark = @class._ViewsvsHistoryApproved.htRemark;
                //_svsHistoryApproved.htCCDept = "";

                //_IT.svsHistoryApproved.AddAsync(_svsHistoryApproved);
                //_IT.SaveChanges();


                //int a = _IT.svsHistoryApproved.Where(x => x.htStep == i_Step && x.htNo == int.Parse(getSrNo[0].ToString())).Count();
                ////}
                ////catch (Exception e)
                ////{
                ////    msg = "fail" + e.Message;
                ////}









                return Json(new { c1 = config, c2 = msg, c3 = fsavefile10 });
            }
            else if (config == "P")
            {
                config = "P";
                msg = msg;
                return Json(new { c1 = config, c2 = msg });
            }
            else
            {
                config = "E";
                msg = msg;
                return Json(new { c1 = config, c2 = msg });
            }


            //getSForm

        }


        public JsonResult SavePageForm(Class @class, List<IFormFile> files, List<IFormFile> filesw, string vform, List<IFormFile> files0, List<IFormFile> files1, List<IFormFile> files2, List<IFormFile> files3, List<IFormFile> files4, List<IFormFile> files5, List<IFormFile> files6, List<IFormFile> files7, List<IFormFile> files8, List<IFormFile> files9, List<IFormFile> files10)
        {


            int[] getSrNo;
            string getSForm;
            string getWForm;
            string[] fsavefileUser;
            string[] fsavefileWoker;
            string[] chkPermis;

            //for test
            string[] fsavefile0;
            string[] fsavefile1;
            string[] fsavefile2;
            string[] fsavefile3;
            string[] fsavefile4;
            string[] fsavefile5;
            string[] fsavefile6;
            string[] fsavefile7;
            string[] fsavefile8;
            string[] fsavefile9;
            string fsavefile10 = "";

            string config = "S";
            string msg = "Save page Sucess ";
            int i_Step = @class._ViewsvsServiceRequest.srStep;
            // files._items.count


            string _UserId = User.Claims.FirstOrDefault(s => s.Type == "UserId")?.Value;
            var chkData = _IT.svsServiceRequest.Where(x => x.srNo == @class._ViewsvsServiceRequest.srNo).FirstOrDefault();
            string message_per = "";
            string status_per = "";
            string _empcs = "";

            if (@class._ViewsvsServiceRequest.srStep > 2) //check cs IS
            {
                var _EmailCS = _IT.svsHistoryApproved.Where(x => x.htSrNo == @class._ViewsvsServiceRequest.srNo.ToString() && x.htStep == 3).Select(x => x.htFrom).FirstOrDefault();
                _empcs = _IT.rpEmails.Where(x => x.emEmail_M365 == _EmailCS).Select(x => x.emEmpcode).FirstOrDefault();

            }

            if (_UserId == chkData.srApproveEmpcode || _UserId == _empcs || (_UserId == @class._ViewsvsServiceRequest.srRequestBy && @class._ViewsvsServiceRequest.srStep == 3))
            {
                config = "S";
            }
            else
            {
                config = "P";
                msg = "You don't have permission to access";
            }

            //chkPermis = chkPermission(@class);



            //if (chkPermis[0] == "Yes") { config = "S"; }
            //else { config = "P"; msg = chkPermis[1]; }

            if (config == "S")
            {



                //check status Transfer

                @class._ViewsvsServiceRequest.srServiceNo = @class._ViewsvsServiceRequest.srServiceNo;
                getSrNo = Save(@class, i_Step);  //save main
                getSForm = SaveForm(@class, vform, getSrNo[0]); //save form

                if (@class._ViewsvsServiceRequest.srStatus != null && @class._ViewsvsServiceRequest.srStatus != "") //save form worker
                {
                    getWForm = SaveFormWorker(@class, vform, getSrNo[0]);
                }
                fsavefileUser = save_file(@class, files, getSrNo[0], ""); // save file user
                fsavefileWoker = save_file(@class, filesw, getSrNo[0], "Worker"); // save file worker
                if (vform == "F4")
                {
                    //remove
                    var _DataAttachment = _IT.Attachment.Where(c => c.fnNo == getSrNo[0].ToString());
                    _IT.Attachment.RemoveRange(_DataAttachment);
                    _IT.SaveChanges();

                    if (files0.Count > 0)
                    {
                        fsavefile0 = save_file_F4(@class, getSrNo[0], files0, 1);
                        fsavefile10 = fsavefile10 + "F0" + fsavefile0[0];
                    }
                    if (files1.Count > 0)
                    {
                        fsavefile1 = save_file_F4(@class, getSrNo[0], files1, 2);
                        fsavefile10 = fsavefile10 + "F1" + fsavefile1[0];
                    }
                    if (files2.Count > 0)
                    {
                        fsavefile2 = save_file_F4(@class, getSrNo[0], files2, 3);
                        fsavefile10 = fsavefile10 + "F2" + fsavefile2[0];
                    }
                    if (files3.Count > 0)
                    {
                        fsavefile3 = save_file_F4(@class, getSrNo[0], files3, 4);
                        fsavefile10 = fsavefile10 + "F3" + fsavefile3[0];
                    }
                    if (files4.Count > 0)
                    {
                        fsavefile4 = save_file_F4(@class, getSrNo[0], files4, 5);
                        fsavefile10 = fsavefile10 + "F4" + fsavefile4[0];
                    }
                    if (files5.Count > 0)
                    {
                        fsavefile5 = save_file_F4(@class, getSrNo[0], files5, 6);
                        fsavefile10 = fsavefile10 + "F5" + fsavefile5[0];
                    }
                    if (files6.Count > 0) { fsavefile6 = save_file_F4(@class, getSrNo[0], files6, 7); }
                    if (files7.Count > 0) { fsavefile7 = save_file_F4(@class, getSrNo[0], files7, 8); }
                    if (files8.Count > 0) { fsavefile8 = save_file_F4(@class, getSrNo[0], files8, 9); }
                    if (files9.Count > 0) { fsavefile9 = save_file_F4(@class, getSrNo[0], files9, 10); }
                    //if (files10.Count > 0) { fsavefile10 = save_file_F4(@class, getSrNo[0], files10, 11); }

                    //fsavefile10 = "F1" + fsavefile0[0] + "F2" + fsavefile1 + "F3" + fsavefile2 + "F4" + fsavefile3 + "F5" + fsavefile4;

                    // fsavefile = save_fileF4(@class, getSrNo[0], files0, files1, files2, files3, files4, files5, files6, files7, files8, files9, files10); // save file
                }

                return Json(new { c1 = config, c2 = msg, c3 = fsavefile10 });


            }
            else if (config == "P")
            {
                config = "P";
                msg = msg;
                return Json(new { c1 = config, c2 = msg });
            }
            else
            {
                config = "E";
                msg = msg;
                return Json(new { c1 = config, c2 = msg });
            }

            /////////////////

        }






        //check permission
        public string[] chkPermission(Class @class)
        {
            string _UserId = User.Claims.FirstOrDefault(s => s.Type == "UserId")?.Value;
            var chkData = _IT.svsServiceRequest.Where(x => x.srNo == @class._ViewsvsServiceRequest.srNo).FirstOrDefault();
            string message_per = "";
            string status_per = "";
            string _empcs = "";

            if (@class._ViewsvsServiceRequest.srStep > 2) //check cs IS
            {
                var _EmailCS = _IT.svsHistoryApproved.Where(x => x.htSrNo == @class._ViewsvsServiceRequest.srNo.ToString() && x.htStep == 3).Select(x => x.htFrom).FirstOrDefault();
                _empcs = _IT.rpEmails.Where(x => x.emEmail_M365 == _EmailCS).Select(x => x.emEmpcode).FirstOrDefault();

            }


            try
            {
                if (chkData != null)
                {
                    //check operator //check create user
                    if (_UserId == chkData.srApproveEmpcode || _UserId == _empcs)
                    // if ((_UserId == chkData.srApproveEmpcode) || (_UserId == chkData.srRequestBy))
                    {
                        status_per = "Yes";
                        message_per = "You have permission ";
                    }
                    else
                    {
                        status_per = "No";
                        message_per = "You don't have permission to access";
                    }


                }
                else
                {
                    status_per = "Yes";
                    message_per = "You have permission ";
                }
                string[] returnvar = { status_per, message_per };
                return returnvar;
            }
            catch (Exception e)
            {
                string[] returnvar = { "No", "something went wrong !!!" };
                return returnvar;
            }



        }


        public int[] Save(Class @class, int vstep)
        {
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string UpdateBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            ViewrpEmail vEmailTO = new ViewrpEmail();
            var v_approveBy = "";
            var v_EmpApproveBy = "";
            int v_srNo = @class._ViewsvsServiceRequest.srNo;
            int v_step = vstep;// @class._ViewsvsServiceRequest.srStep;
            //string v_status = @class._ViewsvsHistoryApproved.htStatus == "Cancel" ? @class._ViewsvsHistoryApproved.htStatus : _IT.svsMastFlowApprove.Where(x => x.mfStep == v_step).Select(x => x.mfSubject).FirstOrDefault();
            string v_status = _IT.svsMastFlowApprove.Where(x => x.mfStep == v_step).Select(x => x.mfSubject).FirstOrDefault();



            ViewsvsServiceRequest _ViewsvsServiceRequest = new ViewsvsServiceRequest();
            using (var dbContextTransaction = _IT.Database.BeginTransaction())
            {
                try
                {
                    _ViewsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srNo == v_srNo).FirstOrDefault();

                    if (@class._ViewsvsHistoryApproved != null) //normal case
                    {
                        vEmailTO = _IT.rpEmails.Where(w => w.emName_M365 == @class._ViewsvsHistoryApproved.htTo).FirstOrDefault();
                        var fnameApprove = vEmailTO.emName_M365.Split(" ")[1].ToString();
                        v_approveBy = _HRMS.AccEMPLOYEE.Where(x => x.EMP_ENAME == fnameApprove && x.QUIT_CODE == null).Select(x => x.NICKNAME).FirstOrDefault();
                        v_EmpApproveBy = vEmailTO.emEmpcode.ToString();
                    }
                    else //case save
                    {
                        v_approveBy = _ViewsvsServiceRequest.srApproveName;
                        v_EmpApproveBy = _ViewsvsServiceRequest.srApproveEmpcode;
                    }


                    //new
                    if (_ViewsvsServiceRequest == null)
                    {
                        @class._ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == @class._ViewsvsServiceRequest.srRequestBy).FirstOrDefault();

                        ViewsvsServiceRequest _svsServiceRequest = new ViewsvsServiceRequest();
                        //_svsServiceRequest.srNo = v_srNo; //key
                        _svsServiceRequest.srServiceNo = "wait"; //service No
                        _svsServiceRequest.srRequestBy = @class._ViewsvsServiceRequest.srRequestBy;
                        _svsServiceRequest.srRequestName = @class._ViewAccEMPLOYEE.NICKNAME;
                        _svsServiceRequest.srIntercom = @class._ViewsvsServiceRequest.srIntercom;
                        _svsServiceRequest.srSecCode = @class._ViewsvsServiceRequest.srSecCode;
                        _svsServiceRequest.srDeptCode = @class._ViewsvsServiceRequest.srDeptCode;
                        _svsServiceRequest.srRequestDate = @class._ViewsvsServiceRequest.srRequestDate;
                        _svsServiceRequest.srDesiredDate = @class._ViewsvsServiceRequest.srDesiredDate;
                        _svsServiceRequest.srType = @class._ViewsvsServiceRequest.srType;
                        _svsServiceRequest.srSubject = @class._ViewsvsServiceRequest.srSubject;
                        _svsServiceRequest.srKosu = @class._ViewsvsServiceRequest.srKosu;
                        //_svsServiceRequest.srServiceNo = @class._ViewsvsServiceRequest.srServiceNo;
                        _svsServiceRequest.srIssueDateTime = IssueBy;
                        _svsServiceRequest.srApproveEmpcode = v_EmpApproveBy; // vEmailTO != null ? vEmailTO.emEmpcode : "";
                        _svsServiceRequest.srApproveName = v_approveBy;
                        _svsServiceRequest.srFrom = @class._ViewsvsServiceRequest.srFrom;
                        _svsServiceRequest.srFlow = "01";
                        _svsServiceRequest.srStep = vstep == 0 ? vstep : 1;
                        _svsServiceRequest.srStatus = v_status;

                        //chirayu add operator 06/01/2024
                        _svsServiceRequest.srOperatorEmpcode = "";
                        _svsServiceRequest.srOperatorName = "";


                        _IT.svsServiceRequest.Add(_svsServiceRequest);
                        _IT.SaveChanges();

                        v_srNo = _svsServiceRequest.srNo;
                        v_step = 1;
                    }
                    //update
                    else

                    {
                        @class._ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == @class._ViewsvsServiceRequest.srRequestBy).FirstOrDefault();


                        _ViewsvsServiceRequest.srServiceNo = @class._ViewsvsServiceRequest.srServiceNo;
                        _ViewsvsServiceRequest.srRequestBy = @class._ViewsvsServiceRequest.srRequestBy;
                        _ViewsvsServiceRequest.srRequestName = @class._ViewAccEMPLOYEE.NICKNAME;
                        _ViewsvsServiceRequest.srIntercom = @class._ViewsvsServiceRequest.srIntercom;
                        _ViewsvsServiceRequest.srSecCode = @class._ViewsvsServiceRequest.srSecCode;
                        _ViewsvsServiceRequest.srDeptCode = @class._ViewsvsServiceRequest.srDeptCode;
                        _ViewsvsServiceRequest.srRequestDate = @class._ViewsvsServiceRequest.srRequestDate;
                        _ViewsvsServiceRequest.srDesiredDate = @class._ViewsvsServiceRequest.srDesiredDate;
                        _ViewsvsServiceRequest.srType = @class._ViewsvsServiceRequest.srType;
                        _ViewsvsServiceRequest.srSubject = @class._ViewsvsServiceRequest.srSubject.Trim();
                        _ViewsvsServiceRequest.srKosu = @class._ViewsvsServiceRequest.srKosu;
                        //_ViewsvsServiceRequest.srIssueDateTime = IssueBy;
                        _ViewsvsServiceRequest.srApproveEmpcode = v_EmpApproveBy;//vEmailTO != null ? vEmailTO.emEmpcode : "";
                        _ViewsvsServiceRequest.srApproveName = v_approveBy;
                        _ViewsvsServiceRequest.srFrom = @class._ViewsvsServiceRequest.srFrom;
                        //_ViewsvsServiceRequest.srServiceNo = @class._ViewsvsServiceRequest.srServiceNo;
                        _ViewsvsServiceRequest.srFlow = "01";
                        _ViewsvsServiceRequest.srStep = vstep;          //v_step == 4 ? v_step : v_step + 1;


                        //chirayu add operator 06/01/2024
                        if (vstep == 3)
                        {
                            _ViewsvsServiceRequest.srOperatorEmpcode = v_EmpApproveBy;
                            _ViewsvsServiceRequest.srOperatorName = v_approveBy;
                        }


                        _ViewsvsServiceRequest.srStatus = vstep == 0 ? "Cancel" : v_status;
                        _IT.svsServiceRequest.Update(_ViewsvsServiceRequest);
                        _IT.SaveChanges();

                        v_step = _ViewsvsServiceRequest.srStep;
                    }

                    dbContextTransaction.Commit();

                }
                catch (Exception e)
                {

                    dbContextTransaction.Rollback();
                }
            }

            int[] returnVal = { v_srNo, v_step };
            return returnVal;
        }
        public string SaveFormWorker(Class @class, string vform, int vsrNo)
        {
            string getWForm = "";
            using (var dbContextTransaction = _IT.Database.BeginTransaction())
            {
                try
                {
                    ViewsvsServiceRequest _ViewsvsServiceRequest = new ViewsvsServiceRequest();
                    _ViewsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srNo == vsrNo).FirstOrDefault();
                    _ViewsvsServiceRequest.srStatus = @class._ViewsvsServiceRequest.srStatus; //get status
                    _ViewsvsServiceRequest.srExpFinishDate = @class._ViewsvsServiceRequest.srExpFinishDate;
                    _ViewsvsServiceRequest.srFinishDate = @class._ViewsvsServiceRequest.srFinishDate;
                    _ViewsvsServiceRequest.srTotalWorkTime = @class._ViewsvsServiceRequest.srTotalWorkTime;
                    _ViewsvsServiceRequest.srSolveProblem = @class._ViewsvsServiceRequest.srSolveProblem;

                    _IT.svsServiceRequest.Update(_ViewsvsServiceRequest);
                    _IT.SaveChanges();
                    dbContextTransaction.Commit();
                    getWForm = "success";

                }
                catch (Exception e)
                {
                    getWForm = e.Message;
                    dbContextTransaction.Rollback();
                }
            }

            return getWForm;


        }
        public string SaveForm(Class @class, string vform, int vsrNo)
        {
            string getSForm = "";
            if (vform == "F1")
            {
                getSForm = SaveGenaral(@class, vsrNo);
            }
            else if (vform == "F2")
            {
                getSForm = SaveDataRestore(@class, vsrNo);
            }
            else if (vform == "F3")
            {
                getSForm = SaveBorrowNotebookSpare(@class, vsrNo);
            }
            else if (vform == "F4")
            {
                if (@class._ViewsvsRegisterUSB.ubStatusReq == "New")
                {
                    getSForm = SaveRegisterUSBNew(@class, vsrNo);
                }
                else
                {
                    getSForm = SaveRegisterUSBCancel(@class, vsrNo);
                }

            }
            else if (vform == "F5")
            {

                getSForm = SaveVPNbyOTP(@class, vsrNo);
            }
            else if (vform == "F6")
            {
                getSForm = SaveUserRegisApp(@class, vsrNo);

            }
            else if (vform == "F7")
            {
                getSForm = SaveSystemRegister(@class, vsrNo);
            }
            return getSForm;


        }
        public string SaveGenaral(Class @class, int vsrNo) //F1 Genaral
        {
            string vmsg = "";
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string UpdateBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            ViewsvsGeneral _ViewsvsGeneral = new ViewsvsGeneral();

            using (var dbContextTransaction = _IT.Database.BeginTransaction())
            {
                try
                {
                    _ViewsvsGeneral = _IT.svsGeneral.Where(x => x.gnNo == vsrNo).FirstOrDefault();
                    //new
                    if (_ViewsvsGeneral == null)
                    {
                        ViewsvsGeneral _svsGeneral = new ViewsvsGeneral();
                        _svsGeneral.gnNo = vsrNo;
                        _svsGeneral.gnDescription = @class._ViewsvsGeneral.gnDescription;
                        _svsGeneral.gnIssueBy = IssueBy;

                        //chirayu add 07/02/2025
                        _svsGeneral.gnUpdateBy = UpdateBy;

                        _svsGeneral.gnProgramName = @class._ViewsvsGeneral.gnProgramName;
                        _svsGeneral.gnType = @class._ViewsvsGeneral.gnType;

                        _svsGeneral.gnProgramUser = @class._ViewsvsGeneral.gnProgramUser; //add SR2502616

                        _IT.svsGeneral.Add(_svsGeneral);
                        _IT.SaveChanges();
                    }
                    //update
                    else
                    {

                        //_ViewsvsGeneral.gnNo = vsrNo;
                        _ViewsvsGeneral.gnDescription = @class._ViewsvsGeneral.gnDescription;
                        _ViewsvsGeneral.gnIssueBy = IssueBy;
                        _ViewsvsGeneral.gnUpdateBy = UpdateBy;

                        //chirayu add 07/02/2025
                        _ViewsvsGeneral.gnType = @class._ViewsvsGeneral.gnType;
                        _ViewsvsGeneral.gnProgramName = @class._ViewsvsGeneral.gnProgramName;

                        _ViewsvsGeneral.gnProgramUser = @class._ViewsvsGeneral.gnProgramUser; //add SR2502616

                        _IT.svsGeneral.Update(_ViewsvsGeneral);
                        _IT.SaveChanges();

                    }
                    vmsg = "Insert success";
                    dbContextTransaction.Commit();

                }
                catch (Exception e)
                {
                    vmsg = "Insert Fail !!!!!";
                    dbContextTransaction.Rollback();
                }
            }


            string returnVal = vmsg;
            return returnVal;
        }
        public string SaveDataRestore(Class @class, int vsrNo) //F2 Data Restore
        {
            string vmsg = "";
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string UpdateBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            ViewsvsDataRestore _ViewsvsDataRestore = new ViewsvsDataRestore();

            using (var dbContextTransaction = _IT.Database.BeginTransaction())
            {
                try
                {
                    _ViewsvsDataRestore = _IT.svsDataRestore.Where(x => x.drNo == vsrNo).FirstOrDefault();
                    //new
                    if (_ViewsvsDataRestore == null)
                    {
                        ViewsvsDataRestore _svsDataRestore = new ViewsvsDataRestore();
                        _svsDataRestore.drNo = vsrNo;
                        _svsDataRestore.drDateRestore = @class._ViewsvsDataRestore.drDateRestore;
                        _svsDataRestore.drCause = @class._ViewsvsDataRestore.drCause;
                        _svsDataRestore.drCause_RemarkOther = @class._ViewsvsDataRestore.drCause_RemarkOther;
                        _svsDataRestore.drSystem = @class._ViewsvsDataRestore.drSystem;
                        _svsDataRestore.drSys_RemarkOther = @class._ViewsvsDataRestore.drSys_RemarkOther;
                        _svsDataRestore.drKeepFile = @class._ViewsvsDataRestore.drKeepFile; //ชื่อไฟล์
                        _svsDataRestore.drGroupUser = @class._ViewsvsDataRestore.drGroupUser; //ห้องที่เก็บ
                        _svsDataRestore.drIssueBy = IssueBy;
                        _svsDataRestore.drUpdateBy = UpdateBy;
                        _IT.svsDataRestore.Add(_svsDataRestore);
                        _IT.SaveChanges();
                    }
                    //update
                    else
                    {
                        _ViewsvsDataRestore.drNo = vsrNo;
                        _ViewsvsDataRestore.drDateRestore = @class._ViewsvsDataRestore.drDateRestore;
                        _ViewsvsDataRestore.drCause = @class._ViewsvsDataRestore.drCause;
                        _ViewsvsDataRestore.drCause_RemarkOther = @class._ViewsvsDataRestore.drCause_RemarkOther;
                        _ViewsvsDataRestore.drSystem = @class._ViewsvsDataRestore.drSystem;
                        _ViewsvsDataRestore.drSys_RemarkOther = @class._ViewsvsDataRestore.drSys_RemarkOther;
                        _ViewsvsDataRestore.drKeepFile = @class._ViewsvsDataRestore.drKeepFile; //ชื่อไฟล์
                        _ViewsvsDataRestore.drGroupUser = @class._ViewsvsDataRestore.drGroupUser; //ห้องที่เก็บ
                        _ViewsvsDataRestore.drUpdateBy = UpdateBy;


                        _IT.svsDataRestore.Update(_ViewsvsDataRestore);
                        _IT.SaveChanges();

                    }
                    vmsg = "Insert success";
                    dbContextTransaction.Commit();

                }
                catch (Exception e)
                {
                    vmsg = "Insert Fail !!!!!";
                    dbContextTransaction.Rollback();
                }
            }


            string returnVal = vmsg;
            return returnVal;
        }
        public string SaveBorrowNotebookSpare(Class @class, int vsrNo) //F3 Borrow Notebook Spare
        {
            string vmsg = "";
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string UpdateBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            ViewsvsNotebookSpare _ViewsvsNotebookSpare = new ViewsvsNotebookSpare();
            ViewsvsBorrowNotebookSpare _ViewsvsBorrowNotebookSpare = new ViewsvsBorrowNotebookSpare();

            using (var dbContextTransaction = _IT.Database.BeginTransaction())
            {
                try
                {
                    _ViewsvsNotebookSpare = _IT.svsNotebookSpare.Where(x => x.nsNo == vsrNo).FirstOrDefault();
                    //new
                    if (_ViewsvsNotebookSpare == null)
                    {
                        ViewsvsNotebookSpare _svsNotebookSpare = new ViewsvsNotebookSpare();
                        _svsNotebookSpare.nsNo = vsrNo;
                        _svsNotebookSpare.nsObjective = @class._ViewsvsNotebookSpare.nsObjective;
                        _svsNotebookSpare.nsDescription = @class._ViewsvsNotebookSpare.nsDescription;
                        _svsNotebookSpare.nsObjective_Other = @class._ViewsvsNotebookSpare.nsObjective_Other;
                        _svsNotebookSpare.nsBorrowStratDate = @class._ViewsvsNotebookSpare.nsBorrowStratDate;
                        _svsNotebookSpare.nsBorrowEndDate = @class._ViewsvsNotebookSpare.nsBorrowEndDate;
                        _svsNotebookSpare.nsComputerName = @class._ViewsvsNotebookSpare.nsComputerName;
                        _svsNotebookSpare.nsReturnStartDate = @class._ViewsvsNotebookSpare.nsReturnStartDate;
                        _svsNotebookSpare.nsReturnEndDate = @class._ViewsvsNotebookSpare.nsReturnEndDate;
                        _svsNotebookSpare.nsIssueBy = IssueBy;// @class._ViewsvsNotebookSpare.nsIssueBy;
                        _svsNotebookSpare.nsUpdateBy = UpdateBy; // @class._ViewsvsNotebookSpare.nsUpdateBy;
                        _IT.svsNotebookSpare.Add(_svsNotebookSpare);
                        _IT.SaveChanges();




                    }
                    //update
                    else
                    {
                        _ViewsvsNotebookSpare.nsNo = vsrNo;
                        _ViewsvsNotebookSpare.nsObjective = @class._ViewsvsNotebookSpare.nsObjective;
                        _ViewsvsNotebookSpare.nsDescription = @class._ViewsvsNotebookSpare.nsDescription;
                        _ViewsvsNotebookSpare.nsObjective_Other = @class._ViewsvsNotebookSpare.nsObjective_Other;
                        _ViewsvsNotebookSpare.nsBorrowStratDate = @class._ViewsvsNotebookSpare.nsBorrowStratDate;
                        _ViewsvsNotebookSpare.nsBorrowEndDate = @class._ViewsvsNotebookSpare.nsBorrowEndDate;
                        _ViewsvsNotebookSpare.nsComputerName = @class._ViewsvsNotebookSpare.nsComputerName;
                        _ViewsvsNotebookSpare.nsReturnStartDate = @class._ViewsvsNotebookSpare.nsReturnStartDate;
                        _ViewsvsNotebookSpare.nsReturnEndDate = @class._ViewsvsNotebookSpare.nsReturnEndDate;
                        // _ViewsvsNotebookSpare.nsIssueBy = @class._ViewsvsNotebookSpare.nsIssueBy;
                        _ViewsvsNotebookSpare.nsUpdateBy = UpdateBy;
                        _IT.svsNotebookSpare.Update(_ViewsvsNotebookSpare);
                        _IT.SaveChanges();

                    }

                    if (@class._ViewsvsServiceRequest.srStep > 2)
                    {
                        ViewsvsBorrowNotebookSpare _svsBorrowNotebookSpare = _IT.svsBorrowNotebookSpare.Where(x => x.bnPCName == @class._ViewsvsNotebookSpare.nsComputerName && x.bnStratDate == @class._ViewsvsNotebookSpare.nsReturnStartDate && x.bnEndDate == @class._ViewsvsNotebookSpare.nsReturnEndDate && x.bnStatus == "Y").FirstOrDefault();
                        if (_svsBorrowNotebookSpare != null)
                        {
                            _svsBorrowNotebookSpare.bnPCName = @class._ViewsvsNotebookSpare.nsComputerName;
                            _svsBorrowNotebookSpare.bnStratDate = @class._ViewsvsNotebookSpare.nsReturnStartDate;
                            _svsBorrowNotebookSpare.bnEndDate = @class._ViewsvsNotebookSpare.nsReturnEndDate;
                            _svsBorrowNotebookSpare.bnStatus = "Y";
                            _IT.svsBorrowNotebookSpare.Update(_svsBorrowNotebookSpare);
                            _IT.SaveChanges();
                        }
                        else
                        {
                            ViewsvsBorrowNotebookSpare _VsvsBorrowNotebookSpare = new ViewsvsBorrowNotebookSpare();
                            _VsvsBorrowNotebookSpare.bnPCName = @class._ViewsvsNotebookSpare.nsComputerName;
                            _VsvsBorrowNotebookSpare.bnStratDate = @class._ViewsvsNotebookSpare.nsReturnStartDate;
                            _VsvsBorrowNotebookSpare.bnEndDate = @class._ViewsvsNotebookSpare.nsReturnEndDate;
                            _VsvsBorrowNotebookSpare.bnStatus = "Y";
                            _IT.svsBorrowNotebookSpare.Add(_VsvsBorrowNotebookSpare);
                            _IT.SaveChanges();
                        }

                    }



                    vmsg = "Insert success";
                    dbContextTransaction.Commit();

                }
                catch (Exception e)
                {
                    vmsg = "Insert Fail !!!!!" + e.Message;
                    dbContextTransaction.Rollback();
                }
            }


            string returnVal = vmsg;
            return returnVal;
        }
        public string SaveRegisterUSBNew(Class @class, int vsrNo) //F4 Register USB VPN by OTP
        {
            string vmsg = "";
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string UpdateBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            ViewsvsRegisterUSB _ViewsvsRegisterUSB = new ViewsvsRegisterUSB();
            if (@class._ViewsvsRegisterUSB_New.Count > 0)
            {

                using (var dbContextTransaction = _IT.Database.BeginTransaction())
                {
                    try
                    {
                        _ViewsvsRegisterUSB = _IT.svsRegisterUSB.Where(x => x.ubNo == vsrNo).FirstOrDefault();
                        if (_ViewsvsRegisterUSB == null)//new
                        {
                            //tb 1
                            ViewsvsRegisterUSB _svsRegisterUSB = new ViewsvsRegisterUSB();
                            _svsRegisterUSB.ubNo = vsrNo;
                            _svsRegisterUSB.ubStatusReq = @class._ViewsvsRegisterUSB.ubStatusReq;
                            _IT.svsRegisterUSB.Add(_svsRegisterUSB);
                            _IT.SaveChanges();

                            //tb2
                            for (int i = 0; i < @class._ViewsvsRegisterUSB_New.Count; i++)
                            {
                                ViewsvsRegisterUSB_New _svsRegisterUSB_New = new ViewsvsRegisterUSB_New();
                                _svsRegisterUSB_New.nuNewNo = i + 1;
                                _svsRegisterUSB_New.nuNo = vsrNo;
                                _svsRegisterUSB_New.nuType = @class._ViewsvsRegisterUSB_New[i].nuType;
                                _svsRegisterUSB_New.nuEquipment = @class._ViewsvsRegisterUSB_New[i].nuEquipment;
                                _svsRegisterUSB_New.nuObjective = @class._ViewsvsRegisterUSB_New[i].nuObjective;
                                _svsRegisterUSB_New.nuCodeIncharge = @class._ViewsvsRegisterUSB_New[i].nuCodeIncharge;
                                _svsRegisterUSB_New.nuUserIncharge = @class._ViewsvsRegisterUSB_New[i].nuUserIncharge;
                                _svsRegisterUSB_New.nuIntercomNo = @class._ViewsvsRegisterUSB_New[i].nuIntercomNo;
                                _svsRegisterUSB_New.nuImage = @class._ViewsvsRegisterUSB_New[i].nuImage;
                                _svsRegisterUSB_New.nuHardwareID = @class._ViewsvsRegisterUSB_New[i].nuHardwareID;
                                _svsRegisterUSB_New.nuITCode = @class._ViewsvsRegisterUSB_New[i].nuITCode;
                                _svsRegisterUSB_New.nuIssueBy = IssueBy;
                                _svsRegisterUSB_New.nuUpdateBy = UpdateBy;
                                _IT.svsRegisterUSB_New.AddAsync(_svsRegisterUSB_New);

                            }
                            _IT.SaveChanges();
                            vmsg = "Insert success";
                            dbContextTransaction.Commit();

                        }
                        else //update
                        {
                            //tb1
                            _ViewsvsRegisterUSB.ubNo = vsrNo;
                            _ViewsvsRegisterUSB.ubStatusReq = @class._ViewsvsRegisterUSB.ubStatusReq;
                            _IT.svsRegisterUSB.Update(_ViewsvsRegisterUSB);
                            _IT.SaveChanges();

                            //tb2
                            //remove
                            var _DataNew = _IT.svsRegisterUSB_New.Where(c => c.nuNo == vsrNo);
                            _IT.svsRegisterUSB_New.RemoveRange(_DataNew);
                            _IT.SaveChanges();
                            for (int i = 0; i < @class._ViewsvsRegisterUSB_New.Count; i++)
                            {
                                ViewsvsRegisterUSB_New _svsRegisterUSB_New = new ViewsvsRegisterUSB_New();
                                _svsRegisterUSB_New.nuNewNo = i + 1;
                                _svsRegisterUSB_New.nuNo = vsrNo;
                                _svsRegisterUSB_New.nuType = @class._ViewsvsRegisterUSB_New[i].nuType;
                                _svsRegisterUSB_New.nuEquipment = @class._ViewsvsRegisterUSB_New[i].nuEquipment;
                                _svsRegisterUSB_New.nuObjective = @class._ViewsvsRegisterUSB_New[i].nuObjective;
                                _svsRegisterUSB_New.nuCodeIncharge = @class._ViewsvsRegisterUSB_New[i].nuCodeIncharge;
                                _svsRegisterUSB_New.nuUserIncharge = @class._ViewsvsRegisterUSB_New[i].nuUserIncharge;
                                _svsRegisterUSB_New.nuIntercomNo = @class._ViewsvsRegisterUSB_New[i].nuIntercomNo;
                                _svsRegisterUSB_New.nuImage = @class._ViewsvsRegisterUSB_New[i].nuImage;
                                _svsRegisterUSB_New.nuHardwareID = @class._ViewsvsRegisterUSB_New[i].nuHardwareID;
                                _svsRegisterUSB_New.nuITCode = @class._ViewsvsRegisterUSB_New[i].nuITCode;
                                _svsRegisterUSB_New.nuIssueBy = IssueBy;
                                _svsRegisterUSB_New.nuUpdateBy = UpdateBy;
                                _IT.svsRegisterUSB_New.AddAsync(_svsRegisterUSB_New);

                            }
                            _IT.SaveChanges();
                            //for (int i = 0; i < @class._ViewsvsRegisterUSB_Cancel.Count; i++)
                            //{
                            //    ViewsvsRegisterUSB_Cancel _svsRegisterUSB_Cancel = _IT.svsRegisterUSB_Cancel.Where(x => x.cuCancelNo == @class._ViewsvsRegisterUSB_Cancel[i].cuCancelNo && x.cuNo == @class._ViewsvsRegisterUSB_Cancel[i].cuNo).FirstOrDefault();
                            //    if (_svsRegisterUSB_Cancel != null)
                            //    {
                            //        _svsRegisterUSB_Cancel.cuCancelNo = @class._ViewsvsRegisterUSB_Cancel[i].cuCancelNo;
                            //        _svsRegisterUSB_Cancel.cuNo = @class._ViewsvsRegisterUSB_Cancel[i].cuNo;
                            //        _svsRegisterUSB_Cancel.cuType = @class._ViewsvsRegisterUSB_Cancel[i].cuType;
                            //        _svsRegisterUSB_Cancel.cuUSBNo = @class._ViewsvsRegisterUSB_Cancel[i].cuUSBNo;
                            //        _svsRegisterUSB_Cancel.cuReason = @class._ViewsvsRegisterUSB_Cancel[i].cuReason;
                            //        _svsRegisterUSB_Cancel.cuReason_other = @class._ViewsvsRegisterUSB_Cancel[i].cuReason_other;
                            //        //_svsRegisterUSB_Cancel.cuIssueBy = IssueBy;
                            //        _svsRegisterUSB_Cancel.cuUpdateBy = UpdateBy;
                            //        _IT.svsRegisterUSB_Cancel.Update(_svsRegisterUSB_Cancel);
                            //        _IT.SaveChanges();
                            //    }

                            //}
                            vmsg = "Update success";
                            dbContextTransaction.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        vmsg = "Insert Fail !!!!!" + e.Message;
                        dbContextTransaction.Rollback();
                    }
                }
            }

            string returnVal = vmsg;
            return returnVal;
        }
        public string SaveRegisterUSBCancel(Class @class, int vsrNo) //F4 Register USB VPN by OTP
        {
            string vmsg = "";
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string UpdateBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            ViewsvsRegisterUSB _ViewsvsRegisterUSB = new ViewsvsRegisterUSB();
            if (@class._ViewsvsRegisterUSB_Cancel.Count > 0)
            {

                using (var dbContextTransaction = _IT.Database.BeginTransaction())
                {
                    try
                    {
                        _ViewsvsRegisterUSB = _IT.svsRegisterUSB.Where(x => x.ubNo == vsrNo).FirstOrDefault();
                        if (_ViewsvsRegisterUSB == null)//new
                        {
                            //tb 1
                            ViewsvsRegisterUSB _svsRegisterUSB = new ViewsvsRegisterUSB();
                            _svsRegisterUSB.ubNo = vsrNo;
                            _svsRegisterUSB.ubStatusReq = @class._ViewsvsRegisterUSB.ubStatusReq;
                            _IT.svsRegisterUSB.Add(_svsRegisterUSB);
                            _IT.SaveChanges();

                            //tb2

                            for (int i = 0; i < @class._ViewsvsRegisterUSB_Cancel.Count; i++)
                            {
                                ViewsvsRegisterUSB_Cancel _svsRegisterUSB_Cancel = new ViewsvsRegisterUSB_Cancel();
                                _svsRegisterUSB_Cancel.cuCancelNo = i + 1;
                                _svsRegisterUSB_Cancel.cuNo = vsrNo;
                                _svsRegisterUSB_Cancel.cuType = @class._ViewsvsRegisterUSB_Cancel[i].cuType;
                                _svsRegisterUSB_Cancel.cuUSBNo = @class._ViewsvsRegisterUSB_Cancel[i].cuUSBNo;
                                _svsRegisterUSB_Cancel.cuReason = @class._ViewsvsRegisterUSB_Cancel[i].cuReason;
                                _svsRegisterUSB_Cancel.cuReason_other = @class._ViewsvsRegisterUSB_Cancel[i].cuReason_other;
                                _svsRegisterUSB_Cancel.cuIssueBy = IssueBy;
                                _svsRegisterUSB_Cancel.cuUpdateBy = UpdateBy;
                                _IT.svsRegisterUSB_Cancel.Add(_svsRegisterUSB_Cancel);
                                _IT.SaveChanges();
                            }
                            vmsg = "Insert success";
                            dbContextTransaction.Commit();

                        }
                        else //update
                        {
                            //tb1
                            _ViewsvsRegisterUSB.ubNo = vsrNo;
                            _ViewsvsRegisterUSB.ubStatusReq = @class._ViewsvsRegisterUSB.ubStatusReq;
                            _IT.svsRegisterUSB.Update(_ViewsvsRegisterUSB);
                            _IT.SaveChanges();

                            //tb2
                            //remove
                            var _Datacancel = _IT.svsRegisterUSB_Cancel.Where(c => c.cuNo == vsrNo);
                            _IT.svsRegisterUSB_Cancel.RemoveRange(_Datacancel);
                            _IT.SaveChanges();
                            for (int i = 0; i < @class._ViewsvsRegisterUSB_Cancel.Count; i++)
                            {
                                ViewsvsRegisterUSB_Cancel _svsRegisterUSB_Cancel = new ViewsvsRegisterUSB_Cancel();
                                _svsRegisterUSB_Cancel.cuCancelNo = i + 1;
                                _svsRegisterUSB_Cancel.cuNo = vsrNo;
                                _svsRegisterUSB_Cancel.cuType = @class._ViewsvsRegisterUSB_Cancel[i].cuType;
                                _svsRegisterUSB_Cancel.cuUSBNo = @class._ViewsvsRegisterUSB_Cancel[i].cuUSBNo;
                                _svsRegisterUSB_Cancel.cuReason = @class._ViewsvsRegisterUSB_Cancel[i].cuReason;
                                _svsRegisterUSB_Cancel.cuReason_other = @class._ViewsvsRegisterUSB_Cancel[i].cuReason_other;
                                _svsRegisterUSB_Cancel.cuIssueBy = IssueBy;
                                _svsRegisterUSB_Cancel.cuUpdateBy = UpdateBy;
                                _IT.svsRegisterUSB_Cancel.Add(_svsRegisterUSB_Cancel);
                                _IT.SaveChanges();
                            }

                            //for (int i = 0; i < @class._ViewsvsRegisterUSB_Cancel.Count; i++)
                            //{
                            //    ViewsvsRegisterUSB_Cancel _svsRegisterUSB_Cancel = _IT.svsRegisterUSB_Cancel.Where(x => x.cuCancelNo == @class._ViewsvsRegisterUSB_Cancel[i].cuCancelNo && x.cuNo == @class._ViewsvsRegisterUSB_Cancel[i].cuNo).FirstOrDefault();
                            //    if (_svsRegisterUSB_Cancel != null)
                            //    {
                            //        _svsRegisterUSB_Cancel.cuCancelNo = @class._ViewsvsRegisterUSB_Cancel[i].cuCancelNo;
                            //        _svsRegisterUSB_Cancel.cuNo = @class._ViewsvsRegisterUSB_Cancel[i].cuNo;
                            //        _svsRegisterUSB_Cancel.cuType = @class._ViewsvsRegisterUSB_Cancel[i].cuType;
                            //        _svsRegisterUSB_Cancel.cuUSBNo = @class._ViewsvsRegisterUSB_Cancel[i].cuUSBNo;
                            //        _svsRegisterUSB_Cancel.cuReason = @class._ViewsvsRegisterUSB_Cancel[i].cuReason;
                            //        _svsRegisterUSB_Cancel.cuReason_other = @class._ViewsvsRegisterUSB_Cancel[i].cuReason_other;
                            //        //_svsRegisterUSB_Cancel.cuIssueBy = IssueBy;
                            //        _svsRegisterUSB_Cancel.cuUpdateBy = UpdateBy;
                            //        _IT.svsRegisterUSB_Cancel.Update(_svsRegisterUSB_Cancel);
                            //        _IT.SaveChanges();
                            //    }

                            //}
                            vmsg = "Update success";
                            dbContextTransaction.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        vmsg = "Insert Fail !!!!!" + e.Message;
                        dbContextTransaction.Rollback();
                    }
                }
            }

            string returnVal = vmsg;
            return returnVal;
        }
        public string SaveVPNbyOTP(Class @class, int vsrNo) //F5 VPN by OTP
        {
            string vmsg = "";
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string UpdateBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            ViewsvsVPN _ViewsvsVPN = new ViewsvsVPN();

            using (var dbContextTransaction = _IT.Database.BeginTransaction())
            {
                try
                {
                    _ViewsvsVPN = _IT.svsVPN.Where(x => x.vpnNo == vsrNo).FirstOrDefault();
                    //new
                    if (_ViewsvsVPN == null)
                    {
                        ViewsvsVPN _svsVPN = new ViewsvsVPN();

                        _svsVPN.vpnNo = vsrNo;
                        _svsVPN.vpnPCName = @class._ViewsvsVPN.vpnPCName;
                        _svsVPN.vpnStatusUse = @class._ViewsvsVPN.vpnStatusUse;
                        _svsVPN.vpnEmpCode = @class._ViewsvsVPN.vpnEmpCode;
                        _svsVPN.vpnWork = @class._ViewsvsVPN.vpnWork;
                        _svsVPN.vpnRemark = @class._ViewsvsVPN.vpnRemark;
                        _svsVPN.vpnStartDate = @class._ViewsvsVPN.vpnStartDate;
                        _svsVPN.vpnEndDate = @class._ViewsvsVPN.vpnEndDate;
                        _IT.svsVPN.Add(_svsVPN);
                        _IT.SaveChanges();

                    }
                    //update
                    else
                    {
                        _ViewsvsVPN.vpnNo = vsrNo;
                        _ViewsvsVPN.vpnPCName = @class._ViewsvsVPN.vpnPCName;
                        _ViewsvsVPN.vpnStatusUse = @class._ViewsvsVPN.vpnStatusUse;
                        _ViewsvsVPN.vpnEmpCode = @class._ViewsvsVPN.vpnEmpCode;
                        _ViewsvsVPN.vpnWork = @class._ViewsvsVPN.vpnWork;
                        _ViewsvsVPN.vpnRemark = @class._ViewsvsVPN.vpnRemark;
                        _ViewsvsVPN.vpnStartDate = @class._ViewsvsVPN.vpnStartDate;
                        _ViewsvsVPN.vpnEndDate = @class._ViewsvsVPN.vpnEndDate;
                        _IT.svsVPN.Update(_ViewsvsVPN);
                        _IT.SaveChanges();

                    }
                    vmsg = "Insert success";
                    dbContextTransaction.Commit();

                }
                catch (Exception e)
                {
                    vmsg = "Insert Fail !!!!!";
                    dbContextTransaction.Rollback();
                }
            }


            string returnVal = vmsg;
            return returnVal;
        }
        public string SaveUserRegisApp(Class @class, int vsrNo) //F6 User Register Application
        {
            string vmsg = "";
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string UpdateBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            ViewsvsSDE_SystemRegister _ViewsvsSDE_SystemRegister = new ViewsvsSDE_SystemRegister();

            using (var dbContextTransaction = _IT.Database.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < @class._ViewsvsSDE_SystemRegister.Count; i++)
                    {
                        _ViewsvsSDE_SystemRegister = _IT.svsSDE_SystemRegister.Where(x => x.sysNo == vsrNo
                                                                                     && x.sysEmpCode == @class._ViewsvsSDE_SystemRegister[i].sysEmpCode
                                                                                     && x.sysProgramName == @class._ViewsvsSDE_SystemRegister[i].sysProgramName
                                                                                    ).FirstOrDefault();
                        if (_ViewsvsSDE_SystemRegister == null)
                        {
                            ViewsvsSDE_SystemRegister _svsSDE_SystemRegister = new ViewsvsSDE_SystemRegister();

                            _svsSDE_SystemRegister.sysNo = vsrNo;
                            _svsSDE_SystemRegister.sysEmpCode = @class._ViewsvsSDE_SystemRegister[i].sysEmpCode;
                            _svsSDE_SystemRegister.sysProgramName = @class._ViewsvsSDE_SystemRegister[i].sysProgramName;
                            _svsSDE_SystemRegister.sysName = @class._ViewsvsSDE_SystemRegister[i].sysName;
                            _svsSDE_SystemRegister.sysLastName = @class._ViewsvsSDE_SystemRegister[i].sysLastName;
                            _svsSDE_SystemRegister.sysDeptCode = @class._ViewsvsSDE_SystemRegister[i].sysDeptCode;
                            _svsSDE_SystemRegister.sysSectCode = @class._ViewsvsSDE_SystemRegister[i].sysSectCode;
                            _svsSDE_SystemRegister.sysIntercomNo = @class._ViewsvsSDE_SystemRegister[i].sysIntercomNo;
                            _svsSDE_SystemRegister.sysObject = @class._ViewsvsSDE_SystemRegister[i].sysObject;
                            _svsSDE_SystemRegister.sysPermissionEditor = @class._ViewsvsSDE_SystemRegister[i].sysPermissionEditor;
                            _svsSDE_SystemRegister.sysPermissionRead = @class._ViewsvsSDE_SystemRegister[i].sysPermissionRead;
                            _svsSDE_SystemRegister.sysPermissionDelete = @class._ViewsvsSDE_SystemRegister[i].sysPermissionDelete;
                            _svsSDE_SystemRegister.sysRemark = @class._ViewsvsSDE_SystemRegister[i].sysRemark; //add new 21/11/2024
                            _svsSDE_SystemRegister.sysIssueBy = IssueBy;
                            _svsSDE_SystemRegister.sysUpdateBy = UpdateBy;

                            _IT.svsSDE_SystemRegister.AddAsync(_svsSDE_SystemRegister);
                            _IT.SaveChanges();

                        }
                        //update
                        else
                        {
                            _ViewsvsSDE_SystemRegister.sysNo = vsrNo;
                            _ViewsvsSDE_SystemRegister.sysEmpCode = @class._ViewsvsSDE_SystemRegister[i].sysEmpCode;
                            _ViewsvsSDE_SystemRegister.sysProgramName = @class._ViewsvsSDE_SystemRegister[i].sysProgramName;
                            _ViewsvsSDE_SystemRegister.sysName = @class._ViewsvsSDE_SystemRegister[i].sysName;
                            _ViewsvsSDE_SystemRegister.sysLastName = @class._ViewsvsSDE_SystemRegister[i].sysLastName;
                            _ViewsvsSDE_SystemRegister.sysDeptCode = @class._ViewsvsSDE_SystemRegister[i].sysDeptCode;
                            _ViewsvsSDE_SystemRegister.sysSectCode = @class._ViewsvsSDE_SystemRegister[i].sysSectCode;
                            _ViewsvsSDE_SystemRegister.sysIntercomNo = @class._ViewsvsSDE_SystemRegister[i].sysIntercomNo;
                            _ViewsvsSDE_SystemRegister.sysObject = @class._ViewsvsSDE_SystemRegister[i].sysObject;
                            _ViewsvsSDE_SystemRegister.sysPermissionEditor = @class._ViewsvsSDE_SystemRegister[i].sysPermissionEditor;
                            _ViewsvsSDE_SystemRegister.sysPermissionRead = @class._ViewsvsSDE_SystemRegister[i].sysPermissionRead;
                            _ViewsvsSDE_SystemRegister.sysPermissionDelete = @class._ViewsvsSDE_SystemRegister[i].sysPermissionDelete;
                            _ViewsvsSDE_SystemRegister.sysRemark = @class._ViewsvsSDE_SystemRegister[i].sysRemark; //add new 21/11/2024
                            _ViewsvsSDE_SystemRegister.sysUpdateBy = UpdateBy;
                            _IT.svsSDE_SystemRegister.Update(_ViewsvsSDE_SystemRegister);
                            _IT.SaveChanges();

                        }



                    }

                    vmsg = "Insert success";
                    dbContextTransaction.Commit();
                    // k.sysNo, k.sysEmpCode, k.sysProgramName 


                }
                catch (Exception e)
                {
                    vmsg = "Insert Fail !!!!!" + e.Message;
                    dbContextTransaction.Rollback();
                }
            }


            string returnVal = vmsg;
            return returnVal;
        }
        public string SaveSystemRegister(Class @class, int vsrNo) //F7 System Register
        {
            string vmsg = "";
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string UpdateBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            ViewsvsITMS_SystemRegister _ViewsvsITMS_SystemRegister = new ViewsvsITMS_SystemRegister();

            using (var dbContextTransaction = _IT.Database.BeginTransaction())
            {
                try
                {
                    _ViewsvsITMS_SystemRegister = _IT.svsITMS_SystemRegister.Where(x => x.itNo == vsrNo).FirstOrDefault();
                    //new
                    if (_ViewsvsITMS_SystemRegister == null)
                    {
                        ViewsvsITMS_SystemRegister _svsITMS_SystemRegister = new ViewsvsITMS_SystemRegister();
                        _svsITMS_SystemRegister.itNo = vsrNo;
                        _svsITMS_SystemRegister.itEmpcode = @class._ViewsvsITMS_SystemRegister.itEmpcode;
                        _svsITMS_SystemRegister.itObjective = @class._ViewsvsITMS_SystemRegister.itObjective;
                        _svsITMS_SystemRegister.itPcLan = @class._ViewsvsITMS_SystemRegister.itPcLan;
                        _svsITMS_SystemRegister.itMail = @class._ViewsvsITMS_SystemRegister.itMail;
                        _svsITMS_SystemRegister.itInternet = @class._ViewsvsITMS_SystemRegister.itInternet;
                        _svsITMS_SystemRegister.itPcLan_Type = @class._ViewsvsITMS_SystemRegister.itPcLan_Type;
                        _svsITMS_SystemRegister.itMail_Type = @class._ViewsvsITMS_SystemRegister.itMail_Type;
                        _svsITMS_SystemRegister.itInternet_choice1 = @class._ViewsvsITMS_SystemRegister.itInternet_choice1;
                        _svsITMS_SystemRegister.itInternet_choice2 = @class._ViewsvsITMS_SystemRegister.itInternet_choice2;
                        _svsITMS_SystemRegister.itInternet_choice3 = @class._ViewsvsITMS_SystemRegister.itInternet_choice3;
                        _svsITMS_SystemRegister.itInternet_choice4 = @class._ViewsvsITMS_SystemRegister.itInternet_choice4;
                        _svsITMS_SystemRegister.itInternet_choice5 = @class._ViewsvsITMS_SystemRegister.itInternet_choice5;
                        _svsITMS_SystemRegister.itInternet_choice6 = @class._ViewsvsITMS_SystemRegister.itInternet_choice6;





                        //_svsVPN.vpnNo = vsrNo;
                        //_svsVPN.vpnPCName = @class._ViewsvsVPN.vpnPCName;
                        //_svsVPN.vpnStatusUse = @class._ViewsvsVPN.vpnStatusUse;
                        //_svsVPN.vpnEmpCode = @class._ViewsvsVPN.vpnEmpCode;
                        //_svsVPN.vpnWork = @class._ViewsvsVPN.vpnWork;
                        //_svsVPN.vpnRemark = @class._ViewsvsVPN.vpnRemark;
                        //_svsVPN.vpnStartDate = @class._ViewsvsVPN.vpnStartDate;
                        //_svsVPN.vpnEndDate = @class._ViewsvsVPN.vpnEndDate;
                        _IT.svsITMS_SystemRegister.Add(_svsITMS_SystemRegister);
                        _IT.SaveChanges();

                    }
                    //update
                    else
                    {
                        _ViewsvsITMS_SystemRegister.itNo = vsrNo;
                        _ViewsvsITMS_SystemRegister.itEmpcode = @class._ViewsvsITMS_SystemRegister.itEmpcode;
                        _ViewsvsITMS_SystemRegister.itObjective = @class._ViewsvsITMS_SystemRegister.itObjective;
                        _ViewsvsITMS_SystemRegister.itPcLan = @class._ViewsvsITMS_SystemRegister.itPcLan;
                        _ViewsvsITMS_SystemRegister.itMail = @class._ViewsvsITMS_SystemRegister.itMail;
                        _ViewsvsITMS_SystemRegister.itInternet = @class._ViewsvsITMS_SystemRegister.itInternet;
                        _ViewsvsITMS_SystemRegister.itPcLan_Type = @class._ViewsvsITMS_SystemRegister.itPcLan_Type;
                        _ViewsvsITMS_SystemRegister.itMail_Type = @class._ViewsvsITMS_SystemRegister.itMail_Type;
                        _ViewsvsITMS_SystemRegister.itInternet_choice1 = @class._ViewsvsITMS_SystemRegister.itInternet_choice1;
                        _ViewsvsITMS_SystemRegister.itInternet_choice2 = @class._ViewsvsITMS_SystemRegister.itInternet_choice2;
                        _ViewsvsITMS_SystemRegister.itInternet_choice3 = @class._ViewsvsITMS_SystemRegister.itInternet_choice3;
                        _ViewsvsITMS_SystemRegister.itInternet_choice4 = @class._ViewsvsITMS_SystemRegister.itInternet_choice4;
                        _ViewsvsITMS_SystemRegister.itInternet_choice5 = @class._ViewsvsITMS_SystemRegister.itInternet_choice5;
                        _ViewsvsITMS_SystemRegister.itInternet_choice6 = @class._ViewsvsITMS_SystemRegister.itInternet_choice6;
                        _IT.svsITMS_SystemRegister.Update(_ViewsvsITMS_SystemRegister);
                        _IT.SaveChanges();

                    }
                    vmsg = "Insert success";
                    dbContextTransaction.Commit();

                }
                catch (Exception e)
                {
                    vmsg = "Insert Fail !!!!!";
                    dbContextTransaction.Rollback();
                }
            }


            string returnVal = vmsg;
            return returnVal;
        }



        public ActionResult DeteleDataFile(string id, string vname)
        {
            try
            {
                //var find = _IT.Attachment(X => X.)

                ViewAttachment find = _IT.Attachment.Where(x => x.fnNo == id && x.fnFilename == vname && x.fnProgram == "ServiceStation").FirstOrDefault();
                var delete = _IT.Attachment.Remove(find);


                _IT.SaveChanges();
            }
            catch
            {
                return Json(new { res = "error" });

            }
            return Json(new { res = "success" });


            //return Json(_IT.rpEmails.Where(p => p.emEmail.Contains(term) || p.emEmail_M365.Contains(term)).Select(p => p.emEmail_M365).ToList());

        }



        public ActionResult DeteleData(int id)
        {
            try
            {
                ViewsvsServiceRequest find = _IT.svsServiceRequest.Find(id);
                var delete = _IT.svsServiceRequest.Remove(find);


                _IT.SaveChanges();
            }
            catch
            {
                return Json(new { res = "error" });

            }
            return Json(new { res = "success" });


            //return Json(_IT.rpEmails.Where(p => p.emEmail.Contains(term) || p.emEmail_M365.Contains(term)).Select(p => p.emEmail_M365).ToList());

        }

        public string[] save_file_F4(Class @class, int vSno, List<IFormFile> files0, int v_row)
        {
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string v_name = "";
            string fileName = "";
            string v_error = "";
            string v_fnType = @class._ViewsvsServiceRequest.srType;
            string IssueDate = DateTime.Now.ToString("yyyyMMdd HHmmss");

            try
            {

                using (var dbContextTransaction = _IT.Database.BeginTransaction())
                {
                    try
                    {
                        //remove
                        if (files0 is null)
                        {
                            v_name = "";
                        }
                        else
                        {
                            for (int i = 0; i < files0.Count; i++)
                            {

                                fileName = IssueDate + "-" + files0[i].FileName;// + "-" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss").ToString();//System.IO.Path.GetExtension(file.FileName).ToLower();
                                string filePath = path + fileName;
                                var fileLocation = new FileInfo(filePath);
                                //filePaths.Add(filePath);
                                if (!Directory.Exists(filePath))
                                {
                                    using (var stream = new FileStream(filePath, FileMode.Create))
                                    {
                                        files0[i].CopyTo(stream);
                                    }
                                }

                                ViewsvsRegisterUSB_New _svsRegisterUSB_New = new ViewsvsRegisterUSB_New();
                                _svsRegisterUSB_New = _IT.svsRegisterUSB_New.Where(x => x.nuNo == vSno && x.nuNewNo == v_row).FirstOrDefault();
                                if (_svsRegisterUSB_New != null)
                                {
                                    _svsRegisterUSB_New.nuImage = fileName;
                                    _IT.svsRegisterUSB_New.Update(_svsRegisterUSB_New);
                                    //  _IT.SaveChanges();


                                }

                                ViewAttachment _viewAttachment = new ViewAttachment();
                                _viewAttachment.fnNo = vSno.ToString();
                                _viewAttachment.fnPath = filePath;
                                _viewAttachment.fnFilename = fileName;
                                _viewAttachment.fnIssueBy = IssueBy;
                                _viewAttachment.fnUpdateBy = IssueBy;
                                _viewAttachment.fnType = v_row.ToString();
                                _viewAttachment.fnProgram = "ServiceStation";
                                _IT.Attachment.AddAsync(_viewAttachment);
                                _IT.SaveChanges();
                                dbContextTransaction.Commit();


                            }

                            //_IT.SaveChanges();
                            // dbContextTransaction.Commit();

                            //dbContextTransaction.Dispose();
                            //_IT.Dispose();
                            //detach first instance from change tracker
                            v_name = fileName;
                        }

                        // _IT.SaveChanges();

                        // dbContextTransaction.Commit();

                    }
                    catch (Exception e)
                    {
                        v_error = e.Message;
                        v_name = e.Message;
                        dbContextTransaction.Rollback();
                        //dbContextTransaction.Dispose();
                    }
                    finally
                    {
                        if (dbContextTransaction != null)
                        {
                            dbContextTransaction.Dispose();
                        }


                    }
                }


            }
            catch (Exception e)
            {
                v_error = e.Message;
                v_name = e.Message;
            }

            string[] returnVal = { v_name };
            //string[] returnVal = { "1", "" };
            return returnVal;
        }


        public string[] save_file(Class @class, List<IFormFile> file, int vSno, string v_type)
        {
            string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " - " + HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string v_name = "";
            string fileName = "";
            string v_error = "";
            string v_fnType = v_type != "" ? v_type : @class._ViewsvsServiceRequest.srType;
            string IssueDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            //if (@class._ViewsvsServiceRequest.srStatus != null)
            //{
            //    v_fnType = "Woker";
            //    //if (@class._ViewsvsServiceRequest.srStatus.Contains("Done"))
            //    //{
            //    //    v_fnType = "Woker";
            //    //}
            //}





            try
            {
                if (file is null)
                {
                    v_name = "";
                }
                else
                {
                    for (int i = 0; i < file.Count; i++)
                    {

                        fileName = IssueDate + "-" + file[i].FileName;//System.IO.Path.GetExtension(file.FileName).ToLower();
                        string filePath = path + fileName;
                        var fileLocation = new FileInfo(filePath);
                        //filePaths.Add(filePath);
                        if (!Directory.Exists(filePath))
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file[i].CopyTo(stream);
                            }
                        }

                        using (var dbContextTransaction = _IT.Database.BeginTransaction())
                        {
                            try
                            {

                                ViewAttachment _viewAttachment = new ViewAttachment();
                                _viewAttachment.fnNo = vSno.ToString();
                                _viewAttachment.fnPath = filePath;
                                _viewAttachment.fnFilename = fileName;
                                _viewAttachment.fnIssueBy = IssueBy;
                                _viewAttachment.fnUpdateBy = IssueBy;
                                _viewAttachment.fnType = v_fnType;
                                _viewAttachment.fnProgram = "ServiceStation";
                                _IT.Attachment.AddAsync(_viewAttachment);
                                _IT.SaveChanges();
                                dbContextTransaction.Commit();

                            }
                            catch (Exception e)
                            {
                                v_error = e.Message;
                                dbContextTransaction.Rollback();
                            }
                        }



                        v_name = fileName;
                    }

                }
            }
            catch (Exception e)
            {
                v_name = "";
            }


            string[] returnVal = { v_name };
            //string[] returnVal = { "1", "" };
            return returnVal;
        }

        public FileResult openFile(string pathFile)
        {


            string locationfile = path + "/" + pathFile;
            // string locationfile = @"//thsweb//MAINTENANCE_MOLD/denso_requestment.txt";
            string extension = Path.GetExtension(locationfile);
            byte[] fileByte = System.IO.File.ReadAllBytes(locationfile);


            return File(fileByte, "application/octet-stream", locationfile);

        }


        [HttpPost]
        public JsonResult ListBorrowNotebook(Class @classs, string dateS)//string getID)
        {
            //Class @class ,
            string partialUrl = Url.Action("ShowBorrowNotebook", "RequestForm", new { @class = @classs, dateS });

            List<ViewsvsBorrowNotebookSpare> listBorrow = new List<ViewsvsBorrowNotebookSpare>();
            List<ViewsvsMastNotebookSpare> listMaster = new List<ViewsvsMastNotebookSpare>();
            List<ViewlistBorrowSpareDate> listdata = new List<ViewlistBorrowSpareDate>();
            // List<ViewlistBorrowSpareDate> listdata = new List<ViewlistBorrowSpareDate>();
            //string IssueBy = DateTime.Now.ToString("yyyyMMdd HH:mm:ss")
            int cdays = 0;
            listMaster = _IT.svsMastNotebookSpare.ToList();
            dateS = dateS != null ? dateS : DateTime.Now.ToString("yyyy/MM/dd");
            if (dateS != null)
            {
                cdays = DateTime.DaysInMonth(int.Parse(dateS.Split("/")[0]), int.Parse(dateS.Split("/")[1]));
                listBorrow = _IT.svsBorrowNotebookSpare.Where(x => x.bnStatus == "Y" && x.bnStratDate.Substring(0, 7) == dateS).ToList();
                //listBorrow = _IT.svsBorrowNotebookSpare.ToList();

                listMaster = _IT.svsMastNotebookSpare.ToList();
                //add master
                for (int i = 0; i < listMaster.Count; i++)
                {
                    listdata.Add(new ViewlistBorrowSpareDate
                    {
                        v_bnPCName = listMaster[i].mnPCName,
                        //v_bnStatus = listBorrow[i].bnStatus.ToString(),

                    });
                }

                //add event
                if (listdata.Count > 0)
                {
                    for (int i = 0; i < listBorrow.Count; i++)
                    {
                        int v_st = int.Parse(listBorrow[i].bnStratDate.ToString().Substring(8, 2));//2024/08/02
                        int v_ed = int.Parse(listBorrow[i].bnEndDate.ToString().Substring(8, 2));
                        if (listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).Count() > 0)
                        {
                            //for loop 1 to 31
                            //int j = i + 1;
                            for (int k = 1; k <= cdays; k++)
                            {
                                if (k == 1) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_1 = "1"; } }
                                if (k == 2) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_2 = "1"; } }
                                if (k == 3) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_3 = "1"; } }
                                if (k == 4) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_4 = "1"; } }
                                if (k == 5) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_5 = "1"; } }
                                if (k == 6) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_6 = "1"; } }
                                if (k == 7) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_7 = "1"; } }
                                if (k == 8) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_8 = "1"; } }
                                if (k == 9) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_9 = "1"; } }
                                if (k == 10) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_10 = "1"; } }
                                if (k == 11) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_11 = "1"; } }
                                if (k == 12) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_12 = "1"; } }
                                if (k == 13) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_13 = "1"; } }
                                if (k == 14) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_14 = "1"; } }
                                if (k == 15) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_15 = "1"; } }
                                if (k == 16) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_16 = "1"; } }
                                if (k == 17) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_17 = "1"; } }
                                if (k == 18) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_18 = "1"; } }
                                if (k == 19) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_19 = "1"; } }
                                if (k == 20) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_20 = "1"; } }
                                if (k == 21) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_21 = "1"; } }
                                if (k == 22) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_22 = "1"; } }
                                if (k == 23) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_23 = "1"; } }
                                if (k == 24) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_24 = "1"; } }
                                if (k == 25) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_25 = "1"; } }
                                if (k == 26) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_26 = "1"; } }
                                if (k == 27) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_27 = "1"; } }
                                if (k == 28) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_28 = "1"; } }
                                if (k == 29) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_29 = "1"; } }
                                if (k == 30) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_30 = "1"; } }
                                if (k == 31) { if (k >= v_st && k <= v_ed) { listdata.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).FirstOrDefault().v_31 = "1"; } }
                            }


                        }
                    }
                }

            }
            //else
            //{
            //    listBorrow = _IT.svsBorrowNotebookSpare.ToList();

            //}
            listdata.OrderBy(x => x.v_bnPCName);
            return Json(new { status = "listdata", listdata, partial = partialUrl, countDay = cdays });
            //}

            //return Json(new { status = "empty", partial = partialUrl });
        }
        public ActionResult ShowBorrowNotebook(Class @classs, string dateS)
        {
            // List<ViewsvsBorrowNotebookSpare> listBorrow = _IT.svsBorrowNotebookSpare.Where(x => x.bnStatus == "Y" && DateTime.Parse(x.bnStratDate) <= DateTime.Parse(dateS) && DateTime.Parse(x.bnEndDate) >= DateTime.Parse(dateS)).ToList();
            //for (int i = 0; i < listBorrow.Count; i++)
            //{
            //if (@classs._listViewlistBorrowSpareDate.Count > 0)
            //{
            //    @classs._listViewlistBorrowSpareDate = @classs._listViewlistBorrowSpareDate.Where(x => x.v_bnPCName == listBorrow[i].bnPCName).ToList();


            //}
            //else
            //{
            //@classs._listViewlistBorrowSpareDate.Add(new ViewlistBorrowSpareDate
            //{
            //    v_bnPCName = listBorrow[i].bnPCName,
            //    bnStatus = listBorrow[i].bnStatus,
            //    v_1​ = "",
            //    v_2 = "",
            //    v_3 = "",
            //    v_4​ = "",
            //    v_5 = "",
            //    v_6 = "",
            //    v_7​ = "",
            //    v_8 = "",
            //    v_9 = "",
            //    v_10​ = "",
            //    v_11 = "",
            //    v_12 = "",
            //    v_13 = "",
            //    v_14 = "",
            //    v_15 = "",
            //    v_16​ = "",
            //    v_17 = "",
            //    v_18 = "",
            //    v_19 = "",
            //    v_20 = "",
            //    v_21 = "",
            //    v_22 = "",
            //    v_23 = "",
            //    v_24 = "",
            //    v_25 = "",
            //    v_26 = "",
            //    v_27 = "",
            //    v_28 = "",
            //    v_29 = "",
            //    v_30 = "",
            //    v_31 = "",
            //});
            //}


            //}

            return PartialView("_PartialListBorrowNotebook", @classs);
        }

        //public ActionResult ShowCalBorrow(Class @classs)
        //{
        //    return PartialView("_PartialListBorrowNotebook", @classs);
        //}
        [HttpPost]
        public JsonResult SearchPersonal(string vEmpcode)//string getID)
        {
            Class @class = new Class();

            @class._ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == vEmpcode && x.QUIT_CODE == null).FirstOrDefault();

            List<ViewAccEMPLOYEE> _AccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.EMP_CODE == vEmpcode && x.QUIT_CODE == null).ToList();
            return Json(new { _AccEMPLOYEE });


            //return Json(new { @class });


        }


    }


}