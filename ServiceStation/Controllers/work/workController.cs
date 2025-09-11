using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceStation.Models.Common;
using ServiceStation.Models.DBConnect;
using ServiceStation.Models.Table.IT;

namespace ServiceStation.Controllers.Mywork
{
    public class workController : Controller
    {
        private LAMP _LAMP;
        private HRMS _HRMS;
        private IT _IT;
        private CacheSettingController _Cache;
        private FunctionsController _callFunc;
        public workController(LAMP lamp, HRMS hrms, IT it, CacheSettingController cacheController, FunctionsController callfunction)
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

            @classs._ListsvsServiceRequest = _IT.svsServiceRequest.Where(x => x.srApproveEmpcode == EmpCode && x.srStep==3).ToList();

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
                    int v_step = 0;
                    if (@classs._ViewSearchMyReq.v_Status == "Waiting for approval CS Up of Dept.") //1
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

            }


            return View(@classs);
        }
    }
}