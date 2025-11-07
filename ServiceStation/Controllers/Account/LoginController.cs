using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceStation.Models.Common;
using ServiceStation.Models.DBConnect;
using ServiceStation.Models.Table.HRMS;
using ServiceStation.Models.Table.IT;
using ServiceStation.Models.Table.LAMP;

namespace ServiceStation.Controllers.Account
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;




        private LAMP _LAMP;
        private HRMS _HRMS;
        private IT _IT;
        private CacheSettingController _Cache;
        public LoginController(LAMP lamp, HRMS hrms, CacheSettingController cacheController, IT it, IHttpClientFactory httpClientFactory)
        {
            _LAMP = lamp;
            _HRMS = hrms;
            _IT = it;
            _Cache = cacheController;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(string req, string token)
        {
            string remember = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            Class @class = new Class();

            string vSrNo = "";


            if (!string.IsNullOrWhiteSpace(token))
            {
                var response = await TokenVerify(token);
                if (!(response is OkObjectResult))
                    return response;
                string empcode = (response as OkObjectResult)?.Value as string;
                return await AutherizeAppcenter(empcode);
                //if (setClaim.Item1 == false)
                //    return Content(setClaim.Item2);
                //return RedirectToAction("Index", "Home");
            }



            //if (HttpContext.Request.Query.Count() > 0)
            //{
            //    vSrNo = HttpContext.Request.Query["vSrNo"];
            //    @class.paramvrNo = vSrNo;
            //    //TempData["vSrNo"] = vSrNo;
            //    //@class._ViewLogin.LastLogin = RequestEmp;
            //}


            if (HttpContext.Request.Query.Count() > 0)
            {
                vSrNo = HttpContext.Request.Query["vSrNo"];
                @class.paramvrNo = vSrNo;
            }
            if (remember != null)
            {
                return RedirectToAction("RememberMe", "Login", @class);
            }


            return View(@class);
        }

        [HttpPost]
        public async Task<IActionResult> Autherize(Class @class)
        {
            ViewLogin login = new ViewLogin();
            string sUsername = @class._ViewLogin.UserId.Trim();
            string sPassword = @class._ViewLogin.Password.Trim();
            //if (sUsername != GlobalVariable.AdminUserName && sPassword != GlobalVariable.AdminPassword){
            //    login = _LAMP.Login.FirstOrDefault(s => s.UserId == sUsername && s.Password == sPassword && s.Program == GlobalVariable.ProgramName);}
            //else{
            //    login.UserId = GlobalVariable.AdminUserName;
            //    login.Password = GlobalVariable.AdminPassword;
            //    login.Permission = GlobalVariable.AdminPermission;}

            ViewAccEMPLOYEE accData = new ViewAccEMPLOYEE();
            //accData = _HRMS.AccEMPLOYEE.FirstOrDefault(x => x.EMP_CODE == sUsername);
            accData = _HRMS.AccEMPLOYEE.FirstOrDefault(x => x.EMP_CODE == sUsername && x.EMP_ID.Substring(7, 6) == sPassword);
            var chk_email = _IT.rpEmails.Where(x => x.emEmpcode == sUsername).Select(x => x.emEmail_M365).FirstOrDefault();
            if (accData != null && chk_email != null)
            {
                string[] stat = await Task.Run(() => SetClaim(accData, sUsername, sPassword));
                if (stat[0] == "Ok")
                {
                    // @class.paramvrNo
                    if (@class.paramvrNo != null)
                    {
                        try
                        {
                            ViewsvsServiceRequest _svsServiceRequest = _IT.svsServiceRequest.Where(x => x.srNo == int.Parse(@class.paramvrNo)).FirstOrDefault();

                            if (_svsServiceRequest != null)
                            {
                                return RedirectToAction("Index", "RequestForm", new { id = _svsServiceRequest.srNo, vtype = "Edit", vForm = _svsServiceRequest.srFrom, vTeam = _svsServiceRequest.srType, vSubject = _svsServiceRequest.srSubject, vSrNo = _svsServiceRequest.srNo });
                            }
                            else
                            {
                                return RedirectToAction("Index", "FakePage");
                            }
                        }
                        catch (Exception ex)
                        {
                            return RedirectToAction("Index", "FakePage");
                        }

                        //return RedirectToAction("Index", "FakePage", new { vSrNo = TempData["vSrNo"] });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    //if (@class.param != null)
                    //return RedirectToAction("Index", "Approval", new { req = @class.param });

                }
                else
                {
                    @class._Error = new Error();
                    @class._Error.validation = stat[1];
                    return View("Index", @class);
                }
            }
            else
            {
                @class._Error = new Error();
                @class._Error.validation = "Username or Password invalid Or Username  must have Email M365";
                return View("Index", @class);
            }

        }



        public async Task<IActionResult> AutherizeAppcenter(string empcode)
        {
            Class @class = new Class();
            ViewLogin login = new ViewLogin();
            string sUsername = empcode; //@class._ViewLogin.UserId.Trim();

            ViewAccEMPLOYEE accData = new ViewAccEMPLOYEE();
            accData = _HRMS.AccEMPLOYEE.FirstOrDefault(x => x.EMP_CODE == sUsername);
            string[] stat = await Task.Run(() => SetClaim(accData, sUsername, ""));
            if (stat[0] == "Ok")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Content("Not access !!!!!");
            }



        }


        public async Task<IActionResult> RememberMe(Class @class)
        {
            try
            {
                ViewLogin login = new ViewLogin();
                ViewAccEMPLOYEE accData = new ViewAccEMPLOYEE();

                string sUsername = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                string sPassword = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password")?.Value;

                //if (sUsername != GlobalVariable.AdminUserName && sPassword != GlobalVariable.AdminPassword){
                //    login = _LAMP.Login.FirstOrDefault(s => s.UserId == sUsername && s.Password == sPassword && s.Program == GlobalVariable.ProgramName);}
                //else{
                //    login.UserId = GlobalVariable.AdminUserName;
                //    login.Password = GlobalVariable.AdminPassword;
                //    login.Permission = GlobalVariable.AdminPermission;}


                accData = _HRMS.AccEMPLOYEE.FirstOrDefault(x => x.EMP_CODE == sUsername);
                string[] stat = await Task.Run(() => SetClaim(accData, sUsername, sPassword));

                if (stat[0] == "Ok")
                {
                    if (@class.paramvrNo != null && @class.paramvrNo != "")
                    {
                        ViewsvsServiceRequest _svsServiceRequest = _IT.svsServiceRequest.Where(x => x.srNo == int.Parse(@class.paramvrNo)).FirstOrDefault();

                        if (_svsServiceRequest != null)
                        {
                            return RedirectToAction("Index", "RequestForm", new { id = _svsServiceRequest.srNo, vtype = "Edit", vForm = _svsServiceRequest.srFrom, vTeam = _svsServiceRequest.srType, vSubject = _svsServiceRequest.srSubject, vSrNo = _svsServiceRequest.srNo });
                        }
                        else
                        {
                            return RedirectToAction("Index", "FakePage");
                        }
                        //return RedirectToAction("Index", "RequestForm", new { req = @class.param });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }


                }

                await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Logout");
            }
        }

        public IActionResult Logout()
        {
            this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData.Clear();
            return RedirectToAction("Index", "Login");
            // return Redirect("http://thsweb/mvcpublish/appcenter/landing");
        }

        public async Task<string[]> SetClaim(ViewAccEMPLOYEE accdata, string vusername, string vpassword)
        {
            try
            {
                ViewAccEMPLOYEE acc = new ViewAccEMPLOYEE();
                string Email = "";


                _Cache.clearCacheAccEmployee();
                acc = await Task.Run(() => _Cache.cacheAccEmployee().FirstOrDefault(s => s.EMP_CODE == accdata.EMP_CODE));
                ViewrpEmail Emails = _Cache.cacheEmail().Where(w => w.emEmail_M365 != null && w.emEmpcode.Trim() == acc.EMP_CODE.Trim()).FirstOrDefault();
                if (Emails is null)
                {
                    Email = GlobalVariable.AdminEmail;
                }
                else
                {
                    Email = _Cache.cacheEmail().Where(w => w.emEmpcode.Trim() == acc.EMP_CODE.Trim()).FirstOrDefault().emEmail_M365.Trim();
                }


                acc.DIVI_CODE = await Task.Run(() => _Cache.cacheAccDIVIMast().Where(w => w.DIVI_CODE == acc.DIVI_CODE).FirstOrDefault().DIVI_NAME);
                acc.DEPT_CODE = await Task.Run(() => _Cache.cacheDEPTMast().Where(w => w.DEPT_CODE == acc.DEPT_CODE).FirstOrDefault().DEPT_NAME);
                acc.SEC_CODE = await Task.Run(() => _Cache.cacheSECMast().Where(w => w.SEC_CODE == acc.SEC_CODE).FirstOrDefault().SEC_NAME);
                acc.GRP_CODE = await Task.Run(() => _Cache.cacheGRPMast().Where(w => w.GRP_CODE == acc.GRP_CODE).FirstOrDefault().GRP_NAME);

                acc.LAST_TNAME = acc.LAST_TNAME is null ? "" : acc.LAST_TNAME;
                acc.NICKNAME = acc.EMP_ENAME.Substring(0, 1) + (acc.LAST_ENAME == "" ? "" : acc.LAST_ENAME.Substring(0, 1))?.ToString();



                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Country, "ServiceStation"));
                claims.Add(new Claim(ClaimTypes.Name, acc.EMP_CODE.ToString()));
                claims.Add(new Claim(ClaimTypes.Actor, acc.EMP_TNAME + " " + acc.LAST_TNAME));
                claims.Add(new Claim("UserId", acc.EMP_CODE.Trim().ToString()));
                claims.Add(new Claim("Password", vpassword.ToString()));
                claims.Add(new Claim("EmpCode", acc.EMP_CODE?.ToString()));
                // claims.Add(new Claim("Permission", login.Permission?.ToString()));
                // claims.Add(new Claim(ClaimTypes.Role, login.Permission?.ToString()));
                claims.Add(new Claim("Division", acc.DIVI_CODE.ToUpper()));
                claims.Add(new Claim("Department", acc.DEPT_CODE.ToUpper()));
                claims.Add(new Claim("DeptCode", acc.DEPT_CODE.ToUpper()));
                claims.Add(new Claim("Section", acc.SEC_CODE.ToUpper()));
                claims.Add(new Claim("Group", acc.GRP_CODE.ToUpper()));
                claims.Add(new Claim("Unit", acc.UNT_CODE.ToUpper()));
                claims.Add(new Claim("Position", acc.POS_CODE.ToUpper()));
                claims.Add(new Claim("ProgramName", GlobalVariable.ProgramName));
                claims.Add(new Claim("PriName", acc.PRI_THAI));
                claims.Add(new Claim("Name", acc.EMP_TNAME));
                claims.Add(new Claim("SurName", acc.LAST_TNAME));
                claims.Add(new Claim("NICKNAME", acc.NICKNAME.ToUpper()));
                claims.Add(new Claim("Email", Email));
                ClaimsIdentity identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                    , principal, new AuthenticationProperties()
                    {
                        IsPersistent = false,
                        AllowRefresh = true, // ✅ อนุญาตให้ session ถูกยืดอายุ
                        //ExpiresUtc = DateTime.UtcNow.AddMinutes(15)
                    }); //true is remember login

                string[] stat = { "Ok" };
                return stat;
            }
            catch (Exception ex)
            {
                string[] stat = { "Ng", ex.Message };
                return stat;
            }
        }

        public string FirstCharToUpper(string Text)
        {
            if (Text == null)
                return null;

            if (Text.Length > 0)
                return char.ToUpper(Text[0]) + Text.Substring(1);

            return Text.ToUpper();
        }

        public IActionResult SignOut()
        {
            this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _Cache.clearCacheAccEmployee();
            TempData.Clear();
            return RedirectToAction("Index", "Login");

            // return Redirect("http://thsweb/mvcpublish/appcenter/landing");
        }

        [HttpPost("tokenverify")]
        public async Task<ActionResult> TokenVerify(string token)
        {
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            var client = _httpClientFactory.CreateClient();
            var requestUrl = "http://thsweb/mvcpublish/api/account/token/jwtverify";

            try
            {
                var json = JsonConvert.SerializeObject(new { token = token });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(requestUrl, content);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    return Unauthorized();
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return BadRequest();
                if (response.StatusCode != HttpStatusCode.OK)
                    return BadRequest();

                string empcode = await response.Content.ReadAsStringAsync();

                return new OkObjectResult(empcode);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"API request error: {ex.Message}");
            }
        }
    }
}