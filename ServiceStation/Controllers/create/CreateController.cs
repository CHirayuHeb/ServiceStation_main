using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceStation.Models.Common;
using ServiceStation.Models.DBConnect;
using ServiceStation.Models.MyRequest;
using ServiceStation.Models.Table.HRMS;
using ServiceStation.Models.Table.LAMP;
using ServiceStation.Models.Table.IT;


namespace ServiceStation.Controllers.create
{
    public class CreateController : Controller
    {
        private LAMP _LAMP;
        private HRMS _HRMS;
        private IT _IT;
        private CacheSettingController _Cache;
        private FunctionsController _callFunc;
        public CreateController(LAMP lamp, HRMS hrms, IT it, CacheSettingController cacheController, FunctionsController callfunction)
        {
            _LAMP = lamp;
            _HRMS = hrms;
            _IT = it;
            _Cache = cacheController;
            _callFunc = callfunction;
        }

        [Authorize("Checked")]
        public IActionResult Index()
        {
            Class @class = new Class();
            string EmpCode = User.Claims.FirstOrDefault(s => s.Type == "EmpCode").Value?.ToString();

            @class._ListsvsMastServiceMain = _IT.svsMastServiceMain.ToList();
            @class._ListsvsMastServiceSub = _IT.svsMastServiceSub.ToList();

            return View(@class);
        }


     
    }
}