using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ServiceStation.Models.Approval;
using ServiceStation.Models.Canvas;
using ServiceStation.Models.Common;
using ServiceStation.Models.DBConnect;
using ServiceStation.Models.Table.HRMS;
using ServiceStation.Models.Table.IT;
using ServiceStation.Models.Table.LAMP;

namespace ServiceStation.Controllers.Home
{
    [Authorize()]
    public class HomeController : Controller
    {
        private LAMP _LAMP;
        private HRMS _HRMS;
        private IT _IT;
        private CacheSettingController _Cache;
        private FunctionsController _callFunc;
        public HomeController(LAMP lamp, HRMS hrms, IT it, CacheSettingController cacheController, FunctionsController callfunction)
        {
            _LAMP = lamp;
            _HRMS = hrms;
            _IT = it;
            _Cache = cacheController;
            _callFunc = callfunction;
        }

        //[Authorize(Policy = "perGeneral")]
        [Authorize("Checked")]
        public IActionResult Index(Class @class, int? page,
            string v_serviceNo,
            string v_NameReq,
            string v_Dept,
            string v_serviceType,
            string v_DateRequestFrom,
            string v_DateRequestTo,
            string v_TargetDateFrom,
            string v_TargetDateTo,
            string v_Operator,
            string v_ApproveBy,
            string v_StatusService

            )
        // public IActionResult Index(Class @class)
        {
            int pageSize = 20; // จำนวนรายการที่จะแสดงต่อหน้า
            int pageNumber = (page ?? 1); // หน้าเริ่มต้นคือหน้า 1 ถ้าไม่มีการระบุ

            List<ViewAccEMPLOYEE> _ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.QUIT_CODE == null).OrderBy(x => x.DEPT_CODE).Distinct().ToList();
            SelectList formDept = new SelectList(_ViewAccEMPLOYEE.Where(x => x.QUIT_CODE == null).Select(s => s.DEPT_CODE).Distinct());
            ViewBag.vbformDept = formDept;

            //List<ViewsvsMastFlowApprove> _ViewsvsMastFlowApprove = _IT.svsMastFlowApprove.OrderBy(x => x.mfStep).Distinct().ToList();
            //SelectList formStatus = new SelectList(_ViewsvsMastFlowApprove.Select(s => s.mfSubject).Distinct());
            //ViewBag.vbformStatus = formStatus;

            var _ViewsvsMastFlowApprove = _IT.svsMastFlowApprove
                .OrderBy(x => x.mfStep)
                .GroupBy(x => new { x.mfStep, x.mfSubject })
                .Select(g => g.First())
                .ToList();

            var selectList = _ViewsvsMastFlowApprove.Select(x => new SelectListItem
            {
                Value = $"{x.mfStep}|{x.mfSubject}",  // ส่งสองค่าในรูปแบบ string
                Text = x.mfSubject
            }).ToList();

            ViewBag.vbformStatus = selectList;


            //List<ViewsvsMastFlowApprove> _ViewsvsMastFlowApprove1 = _IT.svsMastFlowApprove.OrderBy(x => x.mfStep).Distinct().ToList();
            //SelectList formStatus1 = new SelectList(_ViewsvsMastFlowApprove1.Select(s => s.mfStep + "|" + s.mfSubject).Distinct());
            //ViewBag.vbformStatus1 = formStatus1;



            List<ViewsvsMastServiceSub> _ViewsvsMastServiceSub = _IT.svsMastServiceSub.OrderBy(x => x.msSubNo).Distinct().ToList();
            SelectList formServiceSub = new SelectList(_ViewsvsMastServiceSub.Select(s => s.msSubNameEN).Distinct());
            ViewBag.vbformServiceSub = formServiceSub;

            List<ViewsvsServiceRequest> _ViewsvsServiceRequest = new List<ViewsvsServiceRequest>();
            @class._ListsvsServiceRequest = new List<ViewsvsServiceRequest>();
            @class._ListsvsServiceRequest = _IT.svsServiceRequest.OrderByDescending(x => x.srRequestDate).ThenByDescending(x => x.srIssueDateTime).ToList();

            if (pageNumber > 1)
            {
                @class._ViewSearchData = _ViewSearchData(v_serviceNo, v_NameReq, v_Dept, v_serviceType, v_DateRequestFrom, v_DateRequestTo, v_TargetDateFrom, v_TargetDateTo, v_Operator, v_ApproveBy, v_StatusService);
            }



            if (@class._ViewSearchData != null)
            {
                //@class._ListsvsServiceRequest = _IT.svsServiceRequest.OrderByDescending(x => x.srRequestDate).ThenByDescending(x => x.srIssueDateTime).ToList();
                if (@class._ViewSearchData.v_serviceNo != null && @class._ViewSearchData.v_serviceNo != "")
                {
                    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srServiceNo.Contains(@class._ViewSearchData.v_serviceNo.ToUpper())).ToList();
                }
                if (@class._ViewSearchData.v_NameReq != null && @class._ViewSearchData.v_NameReq != "")
                {
                    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srRequestBy.Contains(@class._ViewSearchData.v_NameReq)).ToList();
                }
                if (@class._ViewSearchData.v_Dept != null && @class._ViewSearchData.v_Dept != "")
                {
                    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srDeptCode == @class._ViewSearchData.v_Dept).ToList();
                }
                if (@class._ViewSearchData.v_serviceType != null && @class._ViewSearchData.v_serviceType != "")
                {
                    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srSubject.Contains(@class._ViewSearchData.v_serviceType)).ToList();
                }

                //date resuest
                if (@class._ViewSearchData.v_DateRequestFrom != null && @class._ViewSearchData.v_DateRequestFrom != "")
                {
                    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => DateTime.Parse(x.srRequestDate) >= DateTime.Parse(@class._ViewSearchData.v_DateRequestFrom)).ToList();
                }
                if (@class._ViewSearchData.v_DateRequestTo != null && @class._ViewSearchData.v_DateRequestTo != "")
                {
                    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => DateTime.Parse(x.srRequestDate) <= DateTime.Parse(@class._ViewSearchData.v_DateRequestTo)).ToList();
                }

                //date target
                if (@class._ViewSearchData.v_TargetDateFrom != null && @class._ViewSearchData.v_TargetDateFrom != "")
                {
                    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => DateTime.Parse(x.srDesiredDate) >= DateTime.Parse(@class._ViewSearchData.v_TargetDateFrom)).ToList();
                }
                if (@class._ViewSearchData.v_TargetDateTo != null && @class._ViewSearchData.v_TargetDateTo != "")
                {
                    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => DateTime.Parse(x.srDesiredDate) <= DateTime.Parse(@class._ViewSearchData.v_TargetDateTo)).ToList();
                }


                if (@class._ViewSearchData.v_Operator != null && @class._ViewSearchData.v_Operator != "")
                {
                    //@class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srOperatorEmpcode == @class._ViewSearchData.v_Operator).ToList();
                    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srOperatorEmpcode.Contains(@class._ViewSearchData.v_Operator)).ToList();
                }
                if (@class._ViewSearchData.v_ApproveBy != null && @class._ViewSearchData.v_ApproveBy != "")
                {
                    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srApproveEmpcode.Contains(@class._ViewSearchData.v_ApproveBy)).ToList();
                }
                //if (@class._ViewSearchData.v_StatusService != null && @class._ViewSearchData.v_StatusService != "")
                //{
                //    @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srStatus.Contains(@class._ViewSearchData.v_StatusService)).ToList();

                //}
                if (@class._ViewSearchData.v_StatusService != null && @class._ViewSearchData.v_StatusService != "")
                {
                    var parts = @class._ViewSearchData.v_StatusService.Split('|');
                    string mfStep = parts[0];       // "001"
                    string mfSubject = parts[1];    // "รอตรวจสอบ"

                    if (mfSubject.Contains("Cancel") || mfSubject.Contains("Transfer"))
                    {
                        //@class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srStep == 0).ToList();

                        @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srStatus.Contains(mfSubject)).ToList();

                    }
                    else
                    {
                        @class._ListsvsServiceRequest = @class._ListsvsServiceRequest.Where(x => x.srStep == int.Parse(mfStep)).ToList();

                    }


                }



                //hard code for life 02/05/2025
                ViewBag.v_serviceNo = @class._ViewSearchData.v_serviceNo;
                ViewBag.v_NameReq = @class._ViewSearchData.v_NameReq;
                ViewBag.v_Dept = @class._ViewSearchData.v_Dept;
                ViewBag.v_serviceType = @class._ViewSearchData.v_serviceType;
                ViewBag.v_DateRequestFrom = @class._ViewSearchData.v_DateRequestFrom;
                ViewBag.v_DateRequestTo = @class._ViewSearchData.v_DateRequestTo;
                ViewBag.v_TargetDateFrom = @class._ViewSearchData.v_TargetDateFrom;
                ViewBag.v_TargetDateTo = @class._ViewSearchData.v_TargetDateTo;
                ViewBag.v_Operator = @class._ViewSearchData.v_Operator;
                ViewBag.v_ApproveBy = @class._ViewSearchData.v_ApproveBy;
                ViewBag.v_StatusService = @class._ViewSearchData.v_StatusService;





            }


            ViewBag.Listcount = @class._ListsvsServiceRequest.Count();
            var pagedList = @class._ListsvsServiceRequest
           .Skip((pageNumber - 1) * pageSize) // ข้ามหน้าที่แล้ว
           .Take(pageSize) // เลือกข้อมูลในหน้า
           .ToList();

            // คำนวณจำนวนหน้าทั้งหมด
            ViewBag.CurrentPage = pageNumber;
            //ViewBag.TotalPages = Math.Ceiling((double)@class._ListceMastSubMakerRequest.Count() / pageSize);
            ViewBag.TotalPages = (int)Math.Ceiling((double)@class._ListsvsServiceRequest.Count() / pageSize);
            // คำนวณหน้าใกล้เคียงที่จะแสดง (เช่น 3 หน้า)
            int startPage = Math.Max(1, pageNumber - 1); // คำนวณหน้าเริ่มต้น (แสดงหน้า 1 ถึง 3)
            int endPage = Math.Min(ViewBag.TotalPages, pageNumber + 1); // คำนวณหน้าสิ้นสุด



            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;






            @class._ListsvsServiceRequest = pagedList; // ส่งข้อมูลที่แบ่งหน้าแล้วไปยัง View
            return View("Index", @class);
        }

        public ViewSearchData _ViewSearchData(
             string v_serviceNo,
            string v_NameReq,
            string v_Dept,
            string v_serviceType,
            string v_DateRequestFrom,
            string v_DateRequestTo,
            string v_TargetDateFrom,
            string v_TargetDateTo,
            string v_Operator,
            string v_ApproveBy,
            string v_StatusService

            )
        {
            return new ViewSearchData
            {
                v_serviceNo = v_serviceNo,
                v_NameReq = v_NameReq,
                v_Dept = v_Dept,
                v_serviceType = v_serviceType,
                v_DateRequestFrom = v_DateRequestFrom,
                v_DateRequestTo = v_DateRequestTo,
                v_TargetDateFrom = v_TargetDateFrom,
                v_TargetDateTo = v_TargetDateTo,
                v_Operator = v_Operator,
                v_ApproveBy = v_ApproveBy,
                v_StatusService = v_StatusService
            };
        }



        [HttpPost]
        public ActionResult SearchDataT(Class @class)
        {
            List<ViewAccEMPLOYEE> _ViewAccEMPLOYEE = _HRMS.AccEMPLOYEE.Where(x => x.QUIT_CODE == null).OrderBy(x => x.DEPT_CODE).Distinct().ToList();
            SelectList formDept = new SelectList(_ViewAccEMPLOYEE.Where(x => x.QUIT_CODE == null).Select(s => s.DEPT_CODE).Distinct());
            ViewBag.vbformDept = formDept;

            List<ViewsvsMastFlowApprove> _ViewsvsMastFlowApprove = _IT.svsMastFlowApprove.OrderBy(x => x.mfStep).Distinct().ToList();
            SelectList formStatus = new SelectList(_ViewsvsMastFlowApprove.Select(s => s.mfSubject).Distinct());
            ViewBag.vbformStatus = formStatus;


            @class._ListsvsServiceRequest = _IT.svsServiceRequest.ToList();

            //  var list_v_data = v_data.ToList();
            ViewBag.SearchData = @class._ListsvsServiceRequest;
            return View("Index", @class);
            //return PartialView("_PartialSearch",@class);
        }




    }
}