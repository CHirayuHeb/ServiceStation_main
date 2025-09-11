using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceStation.Models.DBConnect;
using ServiceStation.Models.Table.IT;

namespace ServiceStation.Controllers.Account
{
    public class FakePageController : Controller
    {
        private LAMP _LAMP;
        private HRMS _HRMS;
        private IT _IT;
        private CacheSettingController _Cache;
        public FakePageController(LAMP lamp, HRMS hrms, CacheSettingController cacheController, IT it)
        {
            _LAMP = lamp;
            _HRMS = hrms;
            _IT = it;
            _Cache = cacheController;
        }
        public IActionResult Index(int vSrNo)
        {
            //ViewsvsServiceRequest _svsServiceRequest = _IT.svsServiceRequest.Where(x => x.srNo == vSrNo).FirstOrDefault();
            ////onclick = "GoNewRequest('@item.srRequestBy','Edit','@Url.Action("Index", "RequestForm")','@item.srFrom','@item.srType','@item.srSubject','@item.srNo')"
            //// let url = "RequestForm?id=" + vId + "&vtype=" + getEvent + "&vForm=" + vForm + "&vTeam=" + vTeam + "&vSubject=" + vSubject + "&vSrNo=" + vSrNo;
            //ViewBag.reqBy = _svsServiceRequest.srRequestBy;
            //ViewBag.form = _svsServiceRequest.srFrom;
            //ViewBag.type = _svsServiceRequest.srType;
            //ViewBag.Subject = _svsServiceRequest.srSubject;
            //ViewBag.srNo = _svsServiceRequest.srNo;

            //ViewBag.rep_empcode = rep_empcode;
            //ViewBag.rep_issueDate = rep_issueDate;
            return View();
        }
    }
}