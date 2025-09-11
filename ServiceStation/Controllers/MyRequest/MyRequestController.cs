using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceStation.Models.Common;
using ServiceStation.Models.DBConnect;
using ServiceStation.Models.MyRequest;
using ServiceStation.Models.Table.HRMS;
using ServiceStation.Models.Table.IT;
using ServiceStation.Models.Table.LAMP;

namespace ServiceStation.Controllers.MyRequest
{
    public class MyRequestController : Controller
    {
        private LAMP _LAMP;
        private HRMS _HRMS;
        private IT _IT;
        private CacheSettingController _Cache;
        private FunctionsController _callFunc;
        public MyRequestController(LAMP lamp, HRMS hrms, IT it, CacheSettingController cacheController, FunctionsController callfunction)
        {
            _LAMP = lamp;
            _HRMS = hrms;
            _IT = it;
            _Cache = cacheController;
            _callFunc = callfunction;
        }

        public IActionResult Index(Class @classs)
        {
            string EmpCode = User.Claims.FirstOrDefault(s => s.Type == "EmpCode").Value?.ToString();
            List<ViewsvsMastFlowApprove> _ViewsvsMastFlowApprove = _IT.svsMastFlowApprove.OrderBy(x => x.mfStep).Distinct().ToList();
            SelectList formStatus = new SelectList(_ViewsvsMastFlowApprove.Select(s => s.mfSubject).Distinct());
            ViewBag.vbformStatus = formStatus;

            ViewsvsServiceRequest _ViewsvsServiceRequest = new ViewsvsServiceRequest();
            //@classs._ListsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srRequestBy == EmpCode).OrderByDescending(x => x.srRequestDate).ToList();
           // @classs._ListsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srRequestBy == EmpCode).OrderByDescending(x => x.srRequestDate).ToList();
            @classs._ListsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srRequestBy == EmpCode).OrderBy(x => x.srStep).ThenBy(x => x.srRequestDate).ToList();

            if (@classs._ViewSearchMyReq != null)
            {
               
                if (@classs._ViewSearchMyReq.v_srNo != null)
                {
                    @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srServiceNo.Contains(@classs._ViewSearchMyReq.v_srNo)).ToList();
                }
                if (@classs._ViewSearchMyReq.v_Title != null)
                {
                    @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srSubject.Contains(@classs._ViewSearchMyReq.v_Title)).ToList();
                }
                if (@classs._ViewSearchMyReq.v_Status != null)
                {

                    if (@classs._ViewSearchMyReq.v_Status != null && @classs._ViewSearchMyReq.v_Status != "")
                    {
                        if (@classs._ViewSearchMyReq.v_Status.Contains("Cancel"))
                        {
                            @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srStep == 0).ToList();
                        }
                        else if (@classs._ViewSearchMyReq.v_Status.Contains("Waiting for approval CS Up of Dept."))
                        {
                            @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srStep == 1).ToList();
                        }
                        else if (@classs._ViewSearchMyReq.v_Status.Contains("Waiting for approval by CS of IS"))
                        {
                            @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srStep == 2).ToList();
                        }
                        else if (@classs._ViewSearchMyReq.v_Status.Contains("In Process") || @classs._ViewSearchMyReq.v_Status.Contains("Transfer")
                            || @classs._ViewSearchMyReq.v_Status.Contains("Done"))
                        {
                            @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srStep == 3).ToList();
                        }
                        else if (@classs._ViewSearchMyReq.v_Status.Contains("Waiting for accept by CS of IS"))
                        {
                            @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srStep == 4).ToList();
                        }
                        else if (@classs._ViewSearchMyReq.v_Status.Contains("Finish"))
                        {
                            @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srStep == 5).ToList();
                        }
                        //1 - Waiting for approval of Dept.
                        //2 - Waiting for approval by CS of IS
                        //3   0   In Process
                        //3   1   Transfer
                        //3   2   Done
                        //3   3   Cancel
                        //4 - Waiting for accept by CS of IS
                        //5 - Finish
                        // @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srStatus == @class._ViewSearchData.v_StatusService).ToList();
                    }


                    
                }
                if (@classs._ViewSearchMyReq.v_DateReq != null)
                {
                    @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srRequestDate == @classs._ViewSearchMyReq.v_DateReq).ToList();
                }
                if (@classs._ViewSearchMyReq.v_DateTarget != null)
                {
                    @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srDesiredDate == @classs._ViewSearchMyReq.v_DateTarget).ToList();
                }

            }


            return View(@classs);
        }
       

        [HttpPost]
        public ActionResult SearchData(Class @classs)
        {
            string EmpCode = User.Claims.FirstOrDefault(s => s.Type == "EmpCode").Value?.ToString();
            List<ViewsvsMastFlowApprove> _ViewsvsMastFlowApprove = _IT.svsMastFlowApprove.OrderBy(x => x.mfStep).Distinct().ToList();
            SelectList formStatus = new SelectList(_ViewsvsMastFlowApprove.Select(s => s.mfSubject).Distinct());
            ViewBag.vbformStatus = formStatus;


            //@classs._ListsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srRequestBy == EmpCode).OrderByDescending(x => x.srRequestDate).ToList();
            @classs._ListsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srRequestBy == EmpCode).OrderBy(x=>x.srStep).ThenBy(x => x.srRequestDate).ToList();
            if (@classs._ViewSearchMyReq.v_srNo != null)
            {
                @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srServiceNo.Contains(@classs._ViewSearchMyReq.v_srNo)).ToList();
            }
            if (@classs._ViewSearchMyReq.v_Title != null)
            {
                @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srSubject.Contains(@classs._ViewSearchMyReq.v_Title)).ToList();
            }
            if (@classs._ViewSearchMyReq.v_Status != null)
            {
                int v_step = 0;
                if(@classs._ViewSearchMyReq.v_Status == "Waiting for approval of Dept.") //1
                {
                    v_step = 1;
                }
                else if (@classs._ViewSearchMyReq.v_Status == "Waiting for approval by CS of IS")//2
                {
                    v_step = 2;
                }
                else if (@classs._ViewSearchMyReq.v_Status == "In Process" || @classs._ViewSearchMyReq.v_Status == "Transfer")//3
                {
                    v_step = 3;
                }
                else if (@classs._ViewSearchMyReq.v_Status == "Waiting for accept by CS of IS")//2
                {
                    v_step = 4;
                }
                else if (@classs._ViewSearchMyReq.v_Status == "Finish")//2
                {
                    v_step = 5;
                }
                @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srStep == v_step).ToList();
            }
            if (@classs._ViewSearchMyReq.v_DateReq != null)
            {
                @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srRequestDate == @classs._ViewSearchMyReq.v_DateReq).ToList();
            }
            if (@classs._ViewSearchMyReq.v_DateTarget != null)
            {
                @classs._ListsvsServiceRequest = @classs._ListsvsServiceRequest.Where(x => x.srDesiredDate == @classs._ViewSearchMyReq.v_DateTarget).ToList();
            }



            //return RedirectToAction("Index", @classs);

            return View("Index", @classs);
        }


            public IActionResult DisplayWaiting()
        {
            string EmpCode = User.Claims.FirstOrDefault(s => s.Type == "EmpCode").Value?.ToString();
            WebClient request = new WebClient();
            string imgPath = GlobalVariable.imgPath;
            MultiDocMast docMast = new MultiDocMast();
            docMast.docList = new List<MultiDocDetails>();
            docMast.mastFlow = _Cache.cacheMastFlowApprove().ToList();
            docMast.mastJobs = _Cache.cacheMastJob().ToList();
            foreach (ViewMastRequestOT items in _Cache.cacheMastRequestOT().Where(w => w.mrEmpReq == EmpCode && !w.mrStatus.StartsWith(GlobalVariable.StatusFinishedST) && w.mrStep != 0)
                                                                            .OrderByDescending(o => DateTime.Parse(o.mrDateReq.Substring(o.mrDateReq.Length - 4) + "/"
                                                                                                  + o.mrDateReq.Substring(3, 2) + "/"
                                                                                                  + o.mrDateReq.Substring(0, 2)))
                                                                            .ThenByDescending(o => o.mrNoReq.Substring(o.mrNoReq.Length - 2)).ToList())
            {
                MultiDocDetails docDetails = new MultiDocDetails();
                docDetails.workerImages = new List<workerImages>();
                List<ViewDetailRequestOT> workerLists = _Cache.cacheDetailRequestOT().Where(w => w.drNoReq == items.mrNoReq).ToList();

                docDetails.requestOT = items;
                docDetails.workerList = workerLists;
                if (workerLists != null)
                    foreach (string workerEmpcode in workerLists.Select(s => s.drEmpCode))
                    {
                        imgPath = GlobalVariable.imgPath + "/" + workerEmpcode + ".jpg";
                        request.Credentials = new NetworkCredential(GlobalVariable.UFTP, GlobalVariable.PFTP);

                        try
                        {
                            //byte[] imgFile = request.DownloadData(imgPath);
                            //string file64String = Convert.ToBase64String(imgFile);
                            //string imgDataURL = string.Format("data:image/jpg;base64,{0}", file64String);
                            workerImages workerImage = new workerImages()
                            {
                                empcode = workerEmpcode,
                                //image = imgDataURL,
                            };
                            docDetails.workerImages.Add(workerImage);
                        }
                        catch (WebException wex)
                        { Console.Write(wex.ToString()); }
                    };

                docDetails.stepHistory = _Cache.cacheHistoryApproved().Where(w => w.htNoReq == items.mrNoReq).ToList();
                foreach (var oldFrom in docDetails.stepHistory)
                {
                    //lenght empcode 
                    if (oldFrom.htFrom.Length <= 6)
                    {
                        ViewAccEMPLOYEE profile = _Cache.cacheAccEmployee().Where(w => w.EMP_CODE == oldFrom.htFrom).FirstOrDefault();
                        oldFrom.htFrom = "คุณ" + profile.EMP_TNAME + " " + profile.LAST_TNAME;
                    }
                }
                docMast.docList.Add(docDetails);
            }
            return PartialView("_DisplayWaiting", docMast);
        }
        public IActionResult DisplayDone()
        {
            string EmpCode = User.Claims.FirstOrDefault(s => s.Type == "EmpCode").Value?.ToString();
            WebClient request = new WebClient();
            string imgPath = GlobalVariable.imgPath;
            MultiDocMast docMast = new MultiDocMast();
            docMast.docList = new List<MultiDocDetails>();
            docMast.mastFlow = _Cache.cacheMastFlowApprove().ToList();
            docMast.mastJobs = _Cache.cacheMastJob().ToList();
            foreach (ViewMastRequestOT items in _Cache.cacheMastRequestOT().Where(w => w.mrEmpReq == EmpCode && w.mrStatus.StartsWith(GlobalVariable.StatusFinishedST))
                                                                            .OrderByDescending(o => DateTime.Parse(o.mrDateReq.Substring(o.mrDateReq.Length - 4) + "/"
                                                                                                  + o.mrDateReq.Substring(3, 2) + "/"
                                                                                                  + o.mrDateReq.Substring(0, 2)))
                                                                            .ThenByDescending(o => o.mrNoReq.Substring(o.mrNoReq.Length - 2)).ToList())
            {
                MultiDocDetails docDetails = new MultiDocDetails();
                docDetails.workerImages = new List<workerImages>();
                List<ViewDetailRequestOT> workerLists = _Cache.cacheDetailRequestOT().Where(w => w.drNoReq == items.mrNoReq).ToList();

                docDetails.requestOT = items;
                docDetails.workerList = workerLists;
                if (workerLists != null)
                    foreach (string workerEmpcode in workerLists.Select(s => s.drEmpCode))
                    {
                        imgPath = GlobalVariable.imgPath + "/" + workerEmpcode + ".jpg";
                        request.Credentials = new NetworkCredential(GlobalVariable.UFTP, GlobalVariable.PFTP);

                        try
                        {
                            //byte[] imgFile = request.DownloadData(imgPath);
                            //string file64String = Convert.ToBase64String(imgFile);
                            //string imgDataURL = string.Format("data:image/jpg;base64,{0}", file64String);
                            workerImages workerImage = new workerImages()
                            {
                                empcode = workerEmpcode,
                                //image = imgDataURL,
                            };
                            docDetails.workerImages.Add(workerImage);
                        }
                        catch (WebException wex)
                        { Console.Write(wex.ToString()); }
                    };

                docDetails.stepHistory = _Cache.cacheHistoryApproved().Where(w => w.htNoReq == items.mrNoReq).ToList();
                foreach (var oldFrom in docDetails.stepHistory)
                {
                    //lenght empcode 
                    if (oldFrom.htFrom.Length <= 6)
                    {
                        ViewAccEMPLOYEE profile = _Cache.cacheAccEmployee().Where(w => w.EMP_CODE == oldFrom.htFrom).FirstOrDefault();
                        oldFrom.htFrom = "คุณ" + profile.EMP_TNAME + " " + profile.LAST_TNAME;
                    }
                }
                docMast.docList.Add(docDetails);
            }

            return PartialView("_DisplayDone", docMast);
        }
        public IActionResult DisplayDisapproved()
        {
            string EmpCode = User.Claims.FirstOrDefault(s => s.Type == "EmpCode").Value?.ToString();
            WebClient request = new WebClient();
            string imgPath = GlobalVariable.imgPath;
            MultiDocMast docMast = new MultiDocMast();
            docMast.docList = new List<MultiDocDetails>();
            docMast.mastFlow = _Cache.cacheMastFlowApprove().ToList();
            docMast.mastJobs = _Cache.cacheMastJob().ToList();
            foreach (ViewMastRequestOT items in _Cache.cacheMastRequestOT().Where(w => w.mrEmpReq == EmpCode && w.mrStatus.StartsWith(GlobalVariable.StatusRejected))
                                                                            .OrderByDescending(o => DateTime.Parse(o.mrDateReq.Substring(o.mrDateReq.Length - 4) + "/"
                                                                                                  + o.mrDateReq.Substring(3, 2) + "/"
                                                                                                  + o.mrDateReq.Substring(0, 2)))
                                                                            .ThenByDescending(o => o.mrNoReq.Substring(o.mrNoReq.Length - 2)).ToList())
            {
                MultiDocDetails docDetails = new MultiDocDetails();
                docDetails.workerImages = new List<workerImages>();
                List<ViewDetailRequestOT> workerLists = _Cache.cacheDetailRequestOT().Where(w => w.drNoReq == items.mrNoReq).ToList();

                docDetails.requestOT = items;
                docDetails.workerList = workerLists;
                if (workerLists != null)
                    foreach (string workerEmpcode in workerLists.Select(s => s.drEmpCode))
                    {
                        imgPath = GlobalVariable.imgPath + "/" + workerEmpcode + ".jpg";
                        request.Credentials = new NetworkCredential(GlobalVariable.UFTP, GlobalVariable.PFTP);

                        try
                        {
                            //byte[] imgFile = request.DownloadData(imgPath);
                            //string file64String = Convert.ToBase64String(imgFile);
                            //string imgDataURL = string.Format("data:image/jpg;base64,{0}", file64String);
                            workerImages workerImage = new workerImages()
                            {
                                empcode = workerEmpcode,
                                //image = imgDataURL,
                            };
                            docDetails.workerImages.Add(workerImage);
                        }
                        catch (WebException wex)
                        { Console.Write(wex.ToString()); }
                    };

                docDetails.stepHistory = _Cache.cacheHistoryApproved().Where(w => w.htNoReq == items.mrNoReq).ToList();
                foreach (var oldFrom in docDetails.stepHistory)
                {
                    //lenght empcode 
                    if (oldFrom.htFrom.Length <= 6)
                    {
                        ViewAccEMPLOYEE profile = _Cache.cacheAccEmployee().Where(w => w.EMP_CODE == oldFrom.htFrom).FirstOrDefault();
                        oldFrom.htFrom = "คุณ" + profile.EMP_TNAME + " " + profile.LAST_TNAME;
                    }
                }
                docMast.docList.Add(docDetails);
            }
            return PartialView("_DisplayDisapproved", docMast);
        }

        public IActionResult DisplayDraft()
        {
            string EmpCode = User.Claims.FirstOrDefault(s => s.Type == "EmpCode").Value?.ToString();
            WebClient request = new WebClient();
            string imgPath = GlobalVariable.imgPath;
            MultiDocMast docMast = new MultiDocMast();
            docMast.docList = new List<MultiDocDetails>();
            docMast.mastFlow = _Cache.cacheMastFlowApprove().ToList();
            docMast.mastJobs = _Cache.cacheMastJob().ToList();
            foreach (ViewMastRequestOT items in _Cache.cacheMastRequestOT().Where(w => w.mrEmpReq == EmpCode && w.mrStep == 0)
                                                                            .OrderByDescending(o => DateTime.Parse(o.mrDateReq.Substring(o.mrDateReq.Length - 4) + "/"
                                                                                                  + o.mrDateReq.Substring(3, 2) + "/"
                                                                                                  + o.mrDateReq.Substring(0, 2)))
                                                                            .ThenByDescending(o => o.mrNoReq.Substring(o.mrNoReq.Length - 2)).ToList())
            {
                MultiDocDetails docDetails = new MultiDocDetails();
                docDetails.workerImages = new List<workerImages>();
                List<ViewDetailRequestOT> workerLists = _Cache.cacheDetailRequestOT().Where(w => w.drNoReq == items.mrNoReq).ToList();

                docDetails.requestOT = items;
                docDetails.workerList = workerLists;
                if (workerLists != null)
                    foreach (string workerEmpcode in workerLists.Select(s => s.drEmpCode))
                    {
                        imgPath = GlobalVariable.imgPath + "/" + workerEmpcode + ".jpg";
                        request.Credentials = new NetworkCredential(GlobalVariable.UFTP, GlobalVariable.PFTP);

                        try
                        {
                            //byte[] imgFile = request.DownloadData(imgPath);
                            //string file64String = Convert.ToBase64String(imgFile);
                            //string imgDataURL = string.Format("data:image/jpg;base64,{0}", file64String);
                            workerImages workerImage = new workerImages()
                            {
                                empcode = workerEmpcode,
                                //image = imgDataURL,
                            };
                            docDetails.workerImages.Add(workerImage);
                        }
                        catch (WebException wex)
                        { Console.Write(wex.ToString()); }
                    };

                docDetails.stepHistory = _Cache.cacheHistoryApproved().Where(w => w.htNoReq == items.mrNoReq).ToList();
                foreach (var oldFrom in docDetails.stepHistory)
                {
                    //lenght empcode 
                    if (oldFrom.htFrom.Length <= 6)
                    {
                        ViewAccEMPLOYEE profile = _Cache.cacheAccEmployee().Where(w => w.EMP_CODE == oldFrom.htFrom).FirstOrDefault();
                        oldFrom.htFrom = "คุณ" + profile.EMP_TNAME + " " + profile.LAST_TNAME;
                    }
                }
                docMast.docList.Add(docDetails);
            }
            return PartialView("_DisplayDraft", docMast);
        }
    }
}