const urlDefault = location.href.split("ServiceStation")[0] + "\\ServiceStation";
const loader = document.getElementById("loading");
const loadingProcesser = document.getElementById("loadingProcess");
const ForwardModalID = "OTContent_step";
const ModalContentBase = "modal-new-content";
const ModalFooterBase = "FooterContent";
const NewOTRoadStyle = "color: black;font-family: 'LeelawaD Bold';";
const FooterID = "footer";
const apiSTPoint = "http://10.200.128.20/MVCPublish/ServiceStation/";



//async function apiPosting(dataModel){
//    let url = "Approval/ApproveSelected";
//    document.getElementById("bCounting").innerText = dataModel.Document.length;
//    return await console.log(dataModel.Document.length);
//    //fetch(url, {
//    //    method: "POST",
//    //    referrerPolicy: "strict-origin-when-cross-origin",
//    //    credentials: "same-origin",
//    //    headers: { 'Content-Type': 'application/json' },
//    //    body: JSON.stringify(dataModel)
//    //}).then(
//    //    function (response) {
//    //        return response.text();
//    //    }).then(function (cmd) {
//    //        //trans text to json
//    //        let fakeIndex = 0;
//    //        cmd = JSON.parse(cmd);
//    //        var itemCounting = setInterval(function () {
//    //            document.getElementById("bCounting").innerText = fakeIndex <= cmd.count ? ++fakeIndex : cmd.count;
//    //            if (fakeIndex >= cmd.count + 1) {
//    //                clearInterval(itemCounting);
//    //                Swal.fire({
//    //                    title: cmd.title,
//    //                    text: cmd.message,
//    //                    icon: cmd.icon
//    //                }).then(function () { GoSideMenu("Approval"); });;
//    //            }
//    //        }, 100);
//    //    }).catch(function (err) {
//    //        hideLoading();
//    //        alert('Something went wrong.', err);
//    //        return false;
//    //    });
//}

//async function postUntilEmpty(dataModel) {
//    var recentModel = { ...dataModel };
//    var splitModelPosting = {...dataModel};
//    splitModelPosting.Document = recentModel.Document.slice(0, 10);
//    recentModel.Document = recentModel.Document.slice(10);
//    console.log(splitModelPosting);
//    await apiPosting(splitModelPosting).then(() => document.getElementById("bCounting").innerText = recentModel.Document.length );
//    //console.log(recentModel);
//    //console.log(limitModel);


//    if (recentModel.Document.length)
//        postUntilEmpty(recentModel);

//}

//button tag
var home = document.querySelector("button.home");
var create = document.querySelector("button.create");
var requestform = document.querySelector("button.RequestForm");
var myRequest = document.querySelector("button.my-request");
var approval = document.querySelector("button.approval");
var administrator = document.querySelector("button.administrator");
var mywork = document.querySelector("button.work");
var signOut = document.querySelector("button.signOut");

//a tag
var ahome = document.querySelector("div.app a.home");
var acreate = document.querySelector("div.app a.create");
var arequestform = document.querySelector("div.app a.RequestForm");
var amyRequest = document.querySelector("div.app a.my-request");
var aapproval = document.querySelector("div.app a.approval");
var awork = document.querySelector("div.app a.work");
var aadministrator = document.querySelector("div.app a.administrator");
if (home != null)
    home.addEventListener("click", function () {
        GoSideMenu("Home");
    });
if (requestform != null)
    requestform.addEventListener("click", function () {
        GoSideMenu("RequestForm");
    });
if (create != null)
    create.addEventListener("click", function () {
        GoSideMenu("create");
    });
if (myRequest != null)
    myRequest.addEventListener("click", function () {
        GoSideMenu("MyRequest");
    });

if (mywork != null)
    mywork.addEventListener("click", function () {
        GoSideMenu("work");
    });
if (approval != null)
    approval.addEventListener("click", function () {
        GoSideMenu("Approval");
    });

if (administrator != null)
    administrator.addEventListener("click", function () {
        GoSideMenu("Administrator");
    });
if (signOut != null)
    signOut.addEventListener("click", function () {
        //this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //TempData.Clear();
        window.location.href = urlDefault + "\\Login\\SignOut\\";

        //window.Redirect("http://thsweb/mvcpublish/appcenter/landing");
        //Redirect("http://thsweb/mvcpublish/appcenter/landing");
    });

if (ahome != null)
    ahome.addEventListener("click", function () {
        GoSideMenu("Home");
        $("#AppLuncher").modal("hide");
    });
if (acreate != null)
    acreate.addEventListener("click", function () {
        GoSideMenu("create");
        $("#AppLuncher").modal("hide");
    });
if (arequestform != null)
    arequestform.addEventListener("click", function () {
        GoSideMenu("RequestForm");
        $("#AppLuncher").modal("hide");
    });
if (amyRequest != null)
    amyRequest.addEventListener("click", function () {
        GoSideMenu("MyRequest");
        $("#AppLuncher").modal("hide");
    });
if (aapproval != null)
    aapproval.addEventListener("click", function () {
        GoSideMenu("Approval");
        $("#AppLuncher").modal("hide");
    });
if (awork != null)
    aapproval.addEventListener("click", function () {
        GoSideMenu("work");
        $("#AppLuncher").modal("hide");
    });
if (aadministrator != null)
    aadministrator.addEventListener("click", function () {
        GoSideMenu("Administrator");
        $("#AppLuncher").modal("hide");
    });

//use 
function GoNewRequest(getID, getEvent, action, vForm, vTeam, vSubject, vSrNo) {
    let vId = getID;
    let url = "RequestForm?id=" + vId + "&vtype=" + getEvent + "&vForm=" + vForm + "&vTeam=" + vTeam + "&vSubject=" + vSubject + "&vSrNo=" + vSrNo;
    GoSideMenu(url);

}
function GoAccEmpdata(getID, getEvent, controller) {
    let vId = getID;

    //console.log("test" + vId);
    //'@Url.Action("Index", "RequestForm", new { id = vId,vtype = getEvent })'
    $.ajax({
        type: 'post',
        url: controller,
        data: { id: vId, vtype: getEvent },
        success: function (html) {
            var parser = new DOMParser();
            var doc = parser.parseFromString(html, "text/html");
            var ToContent = doc.getElementById("DisplayContent").outerHTML;
            var displayContent = document.getElementsByClassName("content-box").item(0);
            displayContent.innerHTML = ToContent;

        }
    });
}



function GoSideMenu(controller) {
    displayLoading();
    //console.time();
    var url = controller;
    fetch(url, {
        method: "POST",
        referrerPolicy: "strict-origin-when-cross-origin",
        credentials: "same-origin",


    }).then(function (response) {
        // When the page is loaded convert it to text
        return response.text()
    }).then(function (html) {
        // Initialize the DOM parser
        var parser = new DOMParser();

        // Parse the text
        var doc = parser.parseFromString(html, "text/html");

        var ToContent = doc.getElementById("DisplayContent").innerHTML;

        //get div Display
        var displayContent = document.getElementById("DisplayContent");



        //pointer side menu
        //if (controller.search("RequestForm") > -1) {
        //    controller = "RequestForm";
        //    url = controller;
        //}
        PositionY(controller);

        //text view controller to html
        displayContent.innerHTML = ToContent;

        //change url
        window.history.replaceState(controller, controller, url);
        hideLoading();
        //console.timeEnd();


    })
        .catch(function (err) {
            hideLoading();
            alert('Failed to fetch page: ', err);
            //window.location.href = urlDefault + "\\Login\\Index\\";
            window.location.href = urlDefault + "\\Login\\SignOut\\";

        });

}


function PositionY(menu) {
    if (menu.search("RequestForm") > -1) {
        menu = "RequestForm";
    }
    let PY = 0;
    let opacity;
    if (menu == "work") {
        menu = "Approval";
    }
    switch (menu) {
        case "Home":
            //LoadScript(window.location.protocol + "\\" + "js\\" + "Home\\Index.js", "Home");
            //LoadScript("js/Home/Hour.js", "EventHomeHour");
            //LoadScript("js\\" + "Home\\Search\\HourControl.js", "HourControl");
            //LoadScript("js/Home/Index.js", "Home");
            //LoadScript("lib\\" + "datepicker\\js\\bootstrap-datepicker.min.js", "datepicker");
            //LoadScript("lib\\" + "datepicker\\js\\bootstrap-datepicker.min.css", "datepicker");
            LoadScript("js/Create/Index.js", "Create");
            PY = "0px";
            opacity = "opacity-dot-3";
            break;
        case "create":
            LoadScript("js/Home/Hour.js", "EventHomeHour");
            LoadScript("js/Create/Index.js", "Create");
            LoadScript("js/Home/Index.js", "Home");
            LoadScript("js\\" + "Home\\Search\\HourControl.js", "HourControl");
            PY = "64px";
            opacity = "opacity-dot-3";
            break;
        case "RequestForm":
            //LoadScript("js/Home/Hour.js", "EventHomeHour");
            LoadScript("js/Create/Index.js", "Create");
            LoadScript("js/RequestForm/Index.js", "RequestForm");
            //LoadScript("lib\\" + "datepicker\\js\\bootstrap-datepicker.min.js", "datepicker");
            //LoadScript("lib\\" + "datepicker\\js\\bootstrap-datepicker.min.css", "datepicker");
            // LoadScript("js\\" + "Home\\Search\\HourControl.js", "HourControl");
            PY = "60px";
            opacity = "opacity-dot-3";
            break;
        case "MyRequest":

            LoadScript("js/Create/Index.js", "Create");
            //LoadScript("js/MyRequest/Index.js", "MyRequest");
            //LoadScript("js/New/EventMore.js", "EventMyRequestMore");
            PY = "124px";
            opacity = "opacity-dot-3";
            break;
        case "work":
            LoadScript("js/Home/Hour.js", "EventHomeHour");
            LoadScript("js\\" + "Home\\Search\\HourControl.js", "HourControl");
            PY = "244px";
            opacity = "opacity-dot-3";
            break;
        case "Approval":
            LoadScript("js/Create/Index.js", "Create");
            LoadScript("js\\Approval\\Index.js", "Approval");
            LoadScript("js\\New\\EventMore.js", "EventApprovalMore");
            PY = "188px";
            opacity = "opacity-dot-3";
            break;
        case "Administrator":
            PY = "248px";
            LoadScript("js\\Admin\\Index.js", "AdminSetting");
            opacity = "opacity-dot-3";
            break;
    }
    var Selector = document.getElementById("selector");
    var bg = document.getElementsByClassName("banner").item(0);
    var oldOpacity = Array.from(bg.classList).find(c => c.startsWith('opacity'));
    bg.classList.replace(oldOpacity, opacity);
    Selector.style.transform = "translate(0px, " + PY + ")";
}

function LoadScript(sourceFile, name) {

    var Time = Date.now();
    var oldScript = document.getElementById(name);
    var head = document.getElementsByTagName('head')[0];
    var script = document.createElement('script');
    script.src = sourceFile + "?t=" + Time;
    script.type = "text/javascript";
    script.id = name;

    if (oldScript != null) {
        oldScript.parentNode.removeChild(oldScript);
    }
    head.appendChild(script);
    return false;
}

function DisplayResult(url) {
    displayLoading();
    fetch(url, {
        method: "POST",
        referrerPolicy: "strict-origin-when-cross-origin",
        credentials: "same-origin",
    }).then(function (response) {
        // When the page is loaded convert it to text
        return response.text()
    }).then(function (html) {
        // Initialize the DOM parser
        var parser = new DOMParser();

        // Parse the text
        var doc = parser.parseFromString(html, "text/html");

        var ToContent = doc.getElementsByClassName("just-group").item(0).outerHTML;

        //get div Display
        var displayContent = document.getElementsByClassName("search-box").item(0);

        //text view controller to html
        displayContent.innerHTML = ToContent;

        ScriptAppendAndReplace(doc.getElementsByTagName("div").item(0).id);
        //LoadScript("js\\" + "New\\EventMore.js", "EventNewMore");
        hideLoading();
        //change url
        //window.history.replaceState(controller, controller, url);
        return new Promise(function (resolve) { $("#RequestControl").collapse("hide"); resolve("resolved"); });
    })
        .catch(function (err) {
            hideLoading();
            alert('Failed to fetch page: ', err);
        });
}

function HomeSearch(url) {
    displayLoading();

    //effect from cbOTReqClick() need delay for new dateED changevalue
    let DateST = document.getElementById("dateOTStart");
    let DateED = document.getElementById("dateOTEnd");
    console.log(DateED);
    console.log(DateST);
    let jsonSearch = {};
    if (DateST != null)
        jsonSearch["start"] = DateST.value;
    if (DateED != null)
        jsonSearch["end"] = DateED.value;
    fetch(url, {
        method: "POST",
        referrerPolicy: "strict-origin-when-cross-origin",
        credentials: "same-origin",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(jsonSearch),
    }).then(function (response) {
        // When the page is loaded convert it to text
        return response.text()
    }).then(function (html) {
        // Initialize the DOM parser
        var parser = new DOMParser();
        console.log(html);
        // Parse the text
        var doc = parser.parseFromString(html, "text/html");
        var ToContent = doc.getElementsByClassName("just-group").item(0).outerHTML;

        //get div Display
        var displayContent = document.getElementsByClassName("search-box").item(1);

        //text view controller to html
        displayContent.innerHTML = ToContent;

        //ScriptAppendAndReplace(doc.getElementsByTagName("div").item(0).id);
        //LoadScript("js\\" + "New\\EventMore.js", "EventNewMore");
        hideLoading();
        //change url
        //window.history.replaceState(controller, controller, url);
    })
        .catch(function (err) {
            hideLoading();
            alert('Failed to fetch page: ', err);
        });
}

function ScriptAppendAndReplace(filename) {
    switch (filename) {
        case "Hour":
            LoadScript("js\\Home\\Search\\HourControl.js", "HourControl");
            break;
        case "Follow":
            LoadScript("js\\Home\\Search\\FollowControl.js", "FollowControl");
            break;
        case "Document":
            LoadScript("js\\Home\\Search\\DocumentControl.js", "DocumentControl");
            break;
        case "Graph":
            LoadScript("js\\Home\\Search\\GraphControl.js", "GraphControl");
            break;
        default:
            LoadScript("js\\New\\EventMore.js", "EventNewMore");
            break;
    }
    return;
}

function BtnActiive(ClassName) {
    var position;
    let oldActive;
    switch (ClassName) {
        case "hour": case "mytoday": case "FlowWaiting": case "FlowNewlate":
            position = 0;
            break;
        case "follow": case "myyesterday": case "FlowDone":
            position = 1;
            break;
        case "document": case "alltoday": case "FlowDisapproved":
            position = 2
            break;
        case "graph": case "allyesterday": case "DraftPage":
            position = 3
            break;
    }
    var buttonFilter = document.getElementsByClassName("item");
    for (var buttonAt = 0; buttonAt <= buttonFilter.length - 1; buttonAt++) {
        if (buttonAt == position) {
            oldActive = Array.from(buttonFilter.item(buttonAt).classList).find(c => c.startsWith('bg-'));
            buttonFilter.item(buttonAt).classList.replace(oldActive, "bg-active");
        } else {
            oldActive = Array.from(buttonFilter.item(buttonAt).classList).find(c => c.startsWith('bg-'));
            buttonFilter.item(buttonAt).classList.replace(oldActive, "bg-trans");
        }
    }
}

function resetStep(formID) {
    var form = document.getElementById(formID);
    var HisRoad = document.getElementsByClassName("istep");
    if (form.innerHTML.trim() != "") {
        form.innerHTML = "";
    }
    for (var item = 0; item <= HisRoad.length - 1; item++) {
        if (item == 0) { HisRoad.item(item).setAttribute("style", "display: block"); } else { HisRoad.item(item).setAttribute("style", "display: none"); }

    }
}

function Back(recentStep) {

    //History Link Road
    document.getElementsByClassName("istep").item(recentStep - 1).removeAttribute("style");
    document.getElementsByClassName("istep").item(recentStep - 2).setAttribute("style", NewOTRoadStyle);

    //content
    document.getElementById(ForwardModalID + recentStep).setAttribute("style", "display: none;");
    document.getElementById(ForwardModalID + (parseInt(recentStep) - 1)).removeAttribute("style");

    //footer
    document.getElementById(FooterID + recentStep).setAttribute("style", "display: none;");
    document.getElementById(FooterID + (parseInt(recentStep) - 1)).removeAttribute("style");
}

function createNextstep(nextStep) {
    var stepHasAlready = document.getElementById(ForwardModalID + nextStep);
    if (stepHasAlready == null) {
        let displayContent = document.getElementById(ModalContentBase);
        let displayFooter = document.getElementById(ModalFooterBase);
        let divContent = document.createElement("div");
        let divFooter = document.createElement("div");
        divContent.setAttribute("id", ForwardModalID + nextStep);
        displayContent.append(divContent);
        divFooter.setAttribute("id", FooterID + nextStep);
        displayFooter.append(divFooter);
    }
}

function GoToOTChoice(action, target) {
    var url = action;
    fetch(url, {
        method: "POST",
        referrerPolicy: "strict-origin-when-cross-origin",
        credentials: "same-origin",
    }).then(function (response) {
        // When the page is loaded convert it to text
        return response.text()
    }).then(function (html) {
        resetStep(ModalContentBase);

        var parser = new DOMParser();
        var doc = parser.parseFromString(html, "text/html");
        var ToContent = doc.getElementById(ForwardModalID + "1").outerHTML;
        var footer = doc.getElementById("footer1").outerHTML;
        var displayContent = document.getElementById(target);
        var displayFooter = document.getElementById(ModalFooterBase);
        var displayHisRoad = document.getElementsByClassName("istep").item(0);

        displayHisRoad.setAttribute("style", NewOTRoadStyle);
        displayContent.innerHTML = ToContent;
        displayFooter.innerHTML = footer;


        //set div step2
        createNextstep(2);
        LoadScript("js\\" + "New\\EventOTType.js", "EventOTType");
    })
        .catch(function (err) {
            alert('Failed to fetch page: ', err);
        });
}

function GoToOTMyData(action, target, value) {
    var url = action;
    var displayHisRoad = document.getElementsByClassName("istep");
    displayHisRoad.item(0).removeAttribute("style");
    displayHisRoad.item(1).setAttribute("style", NewOTRoadStyle);
    document.getElementById("DaySelected").innerText = value;

    //send param to controller

    if (document.getElementById(ForwardModalID + "2").innerHTML.trim() == "") {
        fetch(url, {
            method: "POST",
            referrerPolicy: "strict-origin-when-cross-origin",
            credentials: "same-origin",
        }).then(function (response) {
            // When the page is loaded convert it to text
            return response.text()
        }).then(function (html) {
            var parser = new DOMParser();
            var doc = parser.parseFromString(html, "text/html");
            doc.getElementById("OTType").value = value;
            var ToContent = doc.getElementById(ForwardModalID + "2").outerHTML;
            var footer = doc.getElementById(FooterID + "2").outerHTML;


            document.getElementById(ForwardModalID + "1").setAttribute("style", "display:none;");
            document.getElementById(FooterID + "1").setAttribute("style", "display:none;");

            var displayContent = document.getElementById(target);
            var displayFooter = document.getElementById(FooterID + "2");


            displayContent.innerHTML = ToContent;
            displayFooter.innerHTML = footer;


            //LoadScript(urlHost + "js\\" + "New\\Index.js", "NewItem");
            LoadScript("js\\New\\EventOTMyData.js", "EventOTMyData");
        });
    } else {
        document.getElementById("OTType").value = value;
        if (document.getElementById(ForwardModalID + "2").style.display === "none") {
            //content
            document.getElementById(ForwardModalID + "2").style.display = "block";
            document.getElementById(ForwardModalID + "1").style.display = "none";
            //footer
            document.getElementById(FooterID + "2").style.display = "block";
            document.getElementById(FooterID + "1").style.display = "none";
        }
    }
}

function GoToNextStep(nextStep, ToAction) {
    var stepHasAlready = document.getElementById(ForwardModalID + nextStep);
    var displayHisRoad = document.getElementsByClassName("istep");
    displayHisRoad.item(nextStep - 2).removeAttribute("style");
    displayHisRoad.item(nextStep - 1).setAttribute("style", NewOTRoadStyle);
    if (stepHasAlready.innerHTML.trim() == "") {
        var url = ToAction;
        var targetContent = ForwardModalID + nextStep;
        var targetFooter = "footer" + nextStep;
        var data = new URLSearchParams();
        fetch(url, {
            method: "POST",
            body: data,
            referrerPolicy: "strict-origin-when-cross-origin",
            credentials: "same-origin",
        }).then(function (response) {
            // When the page is loaded convert it to text
            return response.text()
        }).then(function (html) {
            var parser = new DOMParser();
            var doc = parser.parseFromString(html, "text/html");
            var ToContent = doc.getElementById(ForwardModalID + nextStep).outerHTML;
            var ToFooter = doc.getElementById(FooterID + nextStep).outerHTML;

            //hide old display
            document.getElementById(ForwardModalID + (parseInt(nextStep) - 1)).setAttribute("style", "display:none;");
            document.getElementById(FooterID + (parseInt(nextStep) - 1)).setAttribute("style", "display:none;");

            var displayContent = document.getElementById(targetContent);
            var displayFooter = document.getElementById(targetFooter);
            displayContent.innerHTML = ToContent;
            displayFooter.innerHTML = ToFooter;

            BringScriptToPage(nextStep);

            return false;
        });
    } else {
        if (document.getElementById(ForwardModalID + nextStep).style.display === "none") {
            //content
            document.getElementById(ForwardModalID + nextStep).style.display = "block";
            document.getElementById(ForwardModalID + (parseInt(nextStep) - 1)).style.display = "none";
            //footer
            document.getElementById(FooterID + nextStep).style.display = "block";
            document.getElementById(FooterID + (parseInt(nextStep) - 1)).style.display = "none";
        }
    }
}

function CheckedMyChildren(checkboxEle) {
    var childrenEle = document.getElementById(checkboxEle.value);
    var checkboxsInChildren = childrenEle.querySelectorAll("input[type=checkbox]");
    checkboxsInChildren.forEach(function (ele) {
        ele.checked = checkboxEle.checked;
    });
}

function cbOTReqClick() {
    let cbOTReq = document.getElementById("cbOTReq");
    var dateOTStart = document.getElementById("dateOTStart");
    let dateOTEnd = document.getElementById("dateOTEnd");
    dateOTStart.disabled = !cbOTReq.checked;
    dateOTEnd.disabled = !cbOTReq.checked;
    //dateOTStart.addEventListener("change", function () {
    //    dateOTEnd.setAttribute("min", dateOTStart.value);
    //    if (Date.parse(dateOTEnd.value) < Date.parse(dateOTStart.value))
    //        dateOTEnd.value = dateOTStart.value;
    //    dateOTEnd.disabled = false;
    //    HomeSearch("Home\\SearchFollow");
    //});
}

function ddlLineChange() {
    let ddlLine = document.getElementsByClassName("ddlLine").item(0);
    let ddlModel = document.getElementsByClassName("ddlModel").item(0);
    let url = "Functions/ModelsOfProdLine"
    let jsonProdLine = {};
    if (ddlLine != null)
        jsonProdLine["name"] = ddlLine.value;

    fetch(url, {
        method: "POST",
        referrerPolicy: "strict-origin-when-cross-origin",
        credentials: "same-origin",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(jsonProdLine),
    }).then(function (response) {
        // When the page is loaded convert it to text
        return response.text()
    }).then(function (json) {
        let str = "";
        json = JSON.parse(json);
        for (var index = 0; index <= json.length - 1; index++) {
            str += "<option value='" + json + "'> " + json + "</option>";
        }
        ddlModel.innerHTML = str;
    });
}

function draftOTDocument() {
    displayLoading();

    let formCrateNew = new FormData(document.getElementById("formCreateNew"));
    var param1 = new URLSearchParams(formCrateNew);
    param1.append("mrOTType", document.getElementById("OTType").value);
    let poiterWorker = document.querySelectorAll(".worker-newot-details");
    poiterWorker.forEach(function (div) {
        param1.append("NewWorkerList", JSON.stringify({
            "drEmpCode": div.getElementsByClassName("empcode").item(0).textContent,
            "drJobCode": div.getElementsByClassName("job").item(0).value
        }));
    });
    let poiterMailCC = document.querySelectorAll("label.cc");
    poiterMailCC.forEach(function (label) {
        param1.append("MailCCs", label.textContent,
        );
    });

    let url = "New\\DraftDocument";
    return fetch(url, {
        method: "POST",
        body: param1,
        referrerPolicy: "strict-origin-when-cross-origin",
        credentials: "same-origin",
    }).then(
        function (response) {
            return response.text();
        }).then(function (cmd) {
            hideLoading();

            //trans text to json
            cmd = JSON.parse(cmd);
            if (cmd.icon == "success") {
                if (document.getElementById("mrNoReq"))
                    document.getElementById("mrNoReq").value = cmd.req;
                return new Promise(function (resolve) { resolve("resolved"); });
            }
        }).catch(function (err) {
            hideLoading();
            alert('Something went wrong.', err);
            return false;
        });

}

function updateWorkerJob(Node) {
    let empcode = Node.parentNode.getElementsByClassName("empcode").item(0).innerHTML;
    let req = document.getElementById("mrNoReq").value;
    let jobselected = Node.value;
    let url = "New/UpdateWorkerJob?req=" + req + "&empcode=" + empcode + "&jobselected=" + jobselected;
    fetch(url, {
        method: "POST",
        referrerPolicy: "strict-origin-when-cross-origin",
        credentials: "same-origin",
    }).then(function (response) {
        return response.text();
    });
}

function updateWorkerAfterDelete(targetPaste, req) {
    let urlUpdateBasePage = "New\\WorkerList?req=" + req;
    fetch(urlUpdateBasePage).then(function (response) {
        return response.text();
    }).then(function (partialtext) {
        let parser = new DOMParser();
        let categoryhtml = parser.parseFromString(partialtext, "text/html");
        targetPaste.getElementsByClassName("workers-category").item(0).innerHTML = categoryhtml.getElementsByTagName("body").item(0).innerHTML;
    }).catch(function (err) {
        alert('Something went wrong.', err);
        return false;
    });
}

function ToXlsm(ele) {
    let value = ele.value;
    let url = "Functions\\ToXlsm?req=" + value;
    window.open(url, "_blank");
}

function LoadEmpPic(ele) {
    let empcode = ele.getElementsByClassName("empcode").item(0).innerHTML;
    let url = "Functions/LoadEmpPic?empcode=" + empcode;

    fetch(url).then(function (response) {
        return response.text();
    }).then(function (imgDataURL) {
        let containerImg = ele.getElementsByClassName("img").item(0);
        containerImg.innerHTML = "<img class='wx-100 border-rad' src='" + imgDataURL + "'>";
    });
}

async function ExportToXlsm(noInArray) {
    fetch("Functions/ToListXlsm", {
        method: "POST",
        referrerPolicy: "strict-origin-when-cross-origin",
        credentials: "same-origin",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(noInArray),
    }).then(function (response) {
        return response.text()
    }).then(function (xlsm) {
        location.href = "Functions/XlsxFromByte";
    });
}

function notEnter(e) { if (e.keyCode == 13) return false; }

//showing Loading
function displayLoading() {
    loader.style.display = "flex";
    //setTimeout(() => {
    //    loader.style.display = "none";
    //}, 300000);
}

//hiding Loading
function hideLoading() {
    loader.style.display = "none";
}

//showing Loading
function displayLoadingAndShowProcess(maxCount) {
    Swal.fire({
        html: "กำลังอัพเดทข้อมูล... <p><b id='bCounting'>0</b>" + "of <b>" + maxCount + "</b></p>",
        allowEscapeKey: false,
        allowOutsideClick: false,
        showConfirmButton: false,
        timerProgressBar: true,
    })
}

function displayExportingAndShowProcess() {
    Swal.fire({
        html: "กำลังสร้างไฟล์ Excel...",
        allowEscapeKey: false,
        allowOutsideClick: false,
        showConfirmButton: false,
        timerProgressBar: true,
    })
}

//hiding Loading
function hideLoadingAndShowProcess() {
    Swal.close();
    //loadingProcesser.style.display = "none";
}


function sendMail(getID, action) {
    //const formdata = new FormData(document.forms.item(0)).serialize();
    let vEdate = document.getElementById("i_EDate").value;
    let vform = document.getElementById("txtvForm").value;
    let msg = "";
    let model = new Array();
    var NewrList = {};   //F4

    if (vform == "F1") { //genaral
        let vtxtgDes = document.getElementById("txtgDes").value;
        let vtxtgkosu = document.getElementById("txtgkosu").value;

        //new & revise program SDE
        let vtxtSUbject = document.getElementById("txtSUbject").value;
        //F1TypeNew
        //F1TypeRevise
        if (vtxtgDes == "") {
            msg = "กรุณากรอกรายละเอียด !!!";
        }
        else if (vtxtgkosu == "0") {
            msg = "กรุณากรอก Kosu !!!";
        }

        //check new & revise program SDE
        if (vtxtSUbject.search("Revise") > -1) {
            if (document.getElementById("F1TypeNew").checked == false
                && document.getElementById("F1TypeRevise").checked == false) {
                msg = "ประเภทการร้องขอ";
            }
            else if (document.getElementById("F1TypeRevise").checked == true) {
                //let n_program = document.getElementById("ipF1pgm").value;
                if (document.getElementById("ipF1pgm").value == "") {
                    msg = "กรุณาเลือกโปรแกรมที่ต้องการแก้ไข";
                }

            }


        }

    }
    else if (vform == "F2") { //data restore
        if (document.getElementById("txtF2DateRestore").value == "") {
            msg = "กรุณากรอกวันที่กู้ข้อมูลคืน !!!";
        } else if (document.getElementById("txtF2SystemCase1").checked == false && document.getElementById("txtF2SystemCase2").checked == false) {
            msg = "กรุณาเลือกสาเหตุที่ขอกู้ข้อมูล !!!";
        }
        else if (document.getElementById("SystemPCLan").checked == false &&
            document.getElementById("SystemServer").checked == false &&
            document.getElementById("SystemDB").checked == false &&
            document.getElementById("SystemOther").checked == false) {
            msg = "กรุณาเลือก อยู่ในระบบ !!!";
        }
        else if (document.getElementById("txtF2GroupUser").value == "") {
            msg = "กรุณากรอก Path ที่ต้องการนำไปไฟล์เก็บไว้ !!!";

        }
        else if (document.getElementById("txtF2KeepFile").value == "") {
            msg = "กรุณากรอก ชื่อไฟล์ ที่หาย!!!";
        }

    }
    else if (vform == "F3")//F3 Borrow notebook
    {
        if (document.getElementById("txtF3WaitOrderNew").checked == false &&
            document.getElementById("txtF3ExternalTraining").checked == false &&
            document.getElementById("txtF3MeetAbroad").checked == false &&
            document.getElementById("txtF3MeetCustomer").checked == false &&
            document.getElementById("txtF3Other").checked == false) {
            msg = "กรุณาเลือก วัตถุประสงค์ !!!";
        }
        else if (document.getElementById("txtF3Description").value == "") {
            msg = "กรุณากรอก รายละเอียด(เพิ่มเติม)!!!";
        }
        else if (document.getElementById("txtF3BorrowStratDate").value == "") {
            msg = "กรุณากรอก Start(วันที่ยืม)!!!";
        }
        else if (document.getElementById("txtF3BorrowEndDate").value == "") {
            msg = "กรุณากรอก End Date(วันที่คืน)!!!";
        }
    }
    else if (vform == "F4") {

        if (document.getElementById("F4ubStatusReqNew").checked == false &&
            document.getElementById("F4ubStatusReqCancel").checked == false) {
            msg = "กรุณาเลือก Objective Request !!!!";

        }
        //else {
        //    if (document.getElementById("F4ubStatusReqNew").checked) {

        //    }
        //    else if (document.getElementById("F4ubStatusReqCancel").checked) { }
        //}



        //var v_obj = $('input[name="F4ubStatusReq"]:checked').val();
        //console.log("v_objective" + v_obj);
        //if (v_obj == "New") {
        //    //cEquipment
        //                                                           //set last result ----- (0)


        //    var table = document.getElementById("tbNew");
        //    let v_tr = table.querySelectorAll('tr');
        //    var column_count = table.rows[1].cells.length;
        //    var row = table.rows[1];
        //    v_tr.forEach(function (trHtm) {

        //        if (trHtm.querySelector(".cEquipment") != null) {
        //            console.log(trHtm.querySelector(".cEquipment").value);
        //            model.push({
        //                "nuNewNo": 0,
        //                "nuNo": 0,
        //                "nuType": trHtm.querySelector(".cType").value,
        //                "nuEquipment": trHtm.querySelector(".cEquipment").value,
        //                "nuObjective": trHtm.querySelector(".cObject").value,
        //                "nuCodeIncharge": trHtm.querySelector(".cUser").value,
        //                "nuUserIncharge": trHtm.querySelector(".cUser").value,
        //                "nuIntercomNo": trHtm.querySelector(".cIntercom").value,
        //                "nuImage": trHtm.querySelector(".cType").value,
        //                "nuHardwareID": trHtm.querySelector(".cEquipment").value,
        //                "nuITCode": trHtm.querySelector(".cObject").value,
        //                "nuIssueBy": trHtm.querySelector(".cObject").value,
        //                "nuUpdateBy": trHtm.querySelector(".cObject").value,
        //            });
        //        }


        //    });
        //    NewrList["_ViewsvsRegisterUSB_New"] = model;
        //    console.log(NewrList)

        //    //if (column_count > 0) {
        //    //    for (var index = 0; index < column_count; index++) {
        //    //        let v_eq = row.cells[index].querySelectorAll('.cEquipment');

        //    //        console.log("v_eq" + v_eq);
        //    //        //Or marks[index] = document.getElementsByName('inputcell' + index)[0].value;
        //    //    }
        //    //}
        //}
        //else if (v_obj == "Cancel") {

        //}


    }
    else if (vform == "F5") {//vpn

        if (document.getElementById("txtF5vpnPCName").value == "") {
            msg = "กรุณากรอก หมายเลขเครื่อง !!!";
        }
        else if (document.getElementById("txtF5vpnStatusUse1").checked == false &&
            document.getElementById("txtF5vpnStatusUse2").checked == false) {
            msg = "กรุณาเลือกการใช้งาน เคยใช้งาน หรือ ไม่เคยใช้งาน !!!";
        }
        else if (document.getElementById("txtF5vpnEmpCode").value == "") {
            msg = "กรุณากรอก รหัสพนักงาน !!!";
        }
        else if (document.getElementById("txtF5vpnEmpCode").value.length > 6) {
            msg = "กรุณากรอก รหัสพนักงานไม่เกิน 6 หลัก !!!";
        }
        else if (document.getElementById("txtF5vpnWork").value == "") {
            msg = "กรุณากรอก งานที่ได้รับมอบหมาย !!!";
        }
        //else if (document.getElementById("txtF5vpnRemark").value == "") {
        //    msg = "กรุณากรอก หมายเหตุ!!!";
        //}
        else if (document.getElementById("txtF5vpnStartDate").value == "") {
            msg = "กรุณากรอก วันที่เริ่มใช้ VPN!!!";
        }
        else if (document.getElementById("txtF5vpnEndDate").value == "") {
            msg = "กรุณากรอก วันที่สิ้นสุด VPN!!!";
        }

    }
    else if (vform == "F6")//SDE User Register Application
    {
        //let a = document.getElementById("_ViewsvsSDE_SystemRegister[0].sysPermissionEditor").checked;
        //console.log("a" + a);
        //let b = document.getElementById("_ViewsvsSDE_SystemRegister[0].sysPermissionRead").checked;
        //console.log("b" + b);

        //public int sysNo { get; set; } ViewsvsSDE_SystemRegister
        //public string sysEmpCode { get; set; }
        //public string sysProgramName { get; set; }
        //public string sysName { get; set; }
        //public string sysLastName { get; set; }
        //public string sysDeptCode { get; set; }
        //public string sysSectCode { get; set; }
        //public string sysIntercomNo { get; set; }
        //public string sysObject { get; set; }
        //public string sysPermissionEditor { get; set; }
        //public string sysPermissionRead { get; set; }
        //public string sysPermissionDelete { get; set; }
        //public string sysIssueBy { get; set; }
        //public string sysUpdateBy { get; set; } tbUsb

        //var SDEList = {};
        //let model = new Array();

        //var refTab = document.getElementById("tbUsb")
        //var ttl;
        //// Loop through all rows and columns of the table and popup alert with the value
        //// /content of each cell.
        //for (var i = 2; row = refTab.rows[i]; i++) {
        //    row = refTab.rows[i];
        //    for (var j = 0; col = row.cells[j]; j++) {
        //        // let chckApprove = refTab.querySelectorAll(".cObject1");    
        //        //console.log(col.firstChild.nodeValue);
        //        //console.log("chckApprove" + chckApprove);
        //        if (col.firstChild.nodeValue != null) {
        //            model.push(col.firstChild.nodeValue);
        //        }
        //    }
        //}

        //workerList["ViewsvsSDE_SystemRegister"] = model;

    }
    else if (vform == "F7")//ITMS System register
    {
        if (document.getElementById("txtF7txtF7itObjectiveNew").checked == false &&
            document.getElementById("txtF7txtF7itObjectiveChange").checked == false &&
            document.getElementById("txtF7txtF7itObjectiveCancel").checked == false) {
            msg = "กรุณาเลือกวัตถุประสงค์ !!!";
        }
        else if (document.getElementById("txtF7ITMSitEmpcode").value == "") {
            msg = "กรุณากรอกรหัสพนักงาน  !!!";
        }
        //else if(document.getElementById("txtF7ITMSFname").value == "") {
        //    msg = "กรุณากดปุ่ม ค้นหาข้อมูล เพื่อแสดงข้อมูลพนักงาน!!!";
        //}

        if (document.getElementById("txtF7itMPcLan").checked == true) {
            if (document.getElementById("txtF7itPcLan_TypeThai").checked == false &&
                document.getElementById("txtF7itPcLan_TypeJapan").checked == false) {
                msg = "กรุณาเลือกประเภทขอใช้งานระบบ PC-Lan!!!";
            }
            else {
                if (document.getElementById("txtF7itPcLan_TypeThai").checked == true) {
                    $('#txtF7itPcLan_TypeThai').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itPcLan_TypeJapan").checked == true) {
                    $('#txtF7itPcLan_TypeJapan').removeAttr('disabled', 'disabled');
                }
            }
        }
        if (document.getElementById("txtF7itMmail").checked == true) {
            if (document.getElementById("txtF7itMail_TypeLOTUSNOTES").checked == false &&
                document.getElementById("txtF7itMail_TypeOUTLOOK").checked == false) {
                msg = "กรุณาเลือกประเภทขอใช้งานระบบ Mail!!!";
            }
            else {
                if (document.getElementById("txtF7itMail_TypeLOTUSNOTES").checked == true) {
                    $('#txtF7itMail_TypeLOTUSNOTES').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itMail_TypeOUTLOOK").checked == true) {
                    $('#txtF7itMail_TypeOUTLOOK').removeAttr('disabled', 'disabled');
                }
            }
        }
        if (document.getElementById("txtF7itMInternet").checked == true) {
            if (document.getElementById("txtF7itObjectiveTemporaryUser").checked == false &&
                document.getElementById("txtF7itObjectiveGeneralInformation").checked == false &&
                document.getElementById("txtF7itObjectiveResearchfortheJob").checked == false &&
                document.getElementById("txtF7itObjectiveDirectConcernOnThejob").checked == false &&
                document.getElementById("txtF7itObjectiveOutsideCommunicationByE").checked == false &&
                document.getElementById("txtF7itObjectiveGeneralInformationT9").checked == false) {
                msg = "กรุณาเลือกประเภทขอใช้งาน Internet !!!";
            }
            else {
                if (document.getElementById("txtF7itObjectiveTemporaryUser").checked == true) {
                    $('#txtF7itObjectiveTemporaryUser').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveGeneralInformation").checked == true) {
                    $('#txtF7itObjectiveGeneralInformation').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveResearchfortheJob").checked == true) {
                    $('#txtF7itObjectiveResearchfortheJob').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveDirectConcernOnThejob").checked == true) {
                    $('#txtF7itObjectiveDirectConcernOnThejob').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveOutsideCommunicationByE").checked == true) {
                    $('#txtF7itObjectiveOutsideCommunicationByE').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveGeneralInformationT9").checked == true) {
                    $('#txtF7itObjectiveGeneralInformationT9').removeAttr('disabled', 'disabled');
                }
            }



        }
    }


    if (msg != "") {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: msg,
        })
            .then((result) => { });
    }
    else if (vEdate != null && vEdate != "") {

        let mydata = $("#formRequest").serialize();

        //let mydata = new FormData(document.forms.item(0)).serialize();

        //let formData = document.forms.namedItem("formRequest");
        //let viewModel = new FormData(formData);
        //$.each(formData, function (index, input) {
        //    viewModel.append(input.name, input.value);
        //});

        $.ajax({
            type: 'post',
            url: action,
            data: mydata,//mydata,//{ getID: getID }, // mydata ,//
            //contentType: "application/json; charset=utf-8",
            //processData: false,
            //contentType: false,
            success: function (data) {

                console.log("fsendMail");
                //$("#myModal1").modal("show");
                var htmls = "";
                //if (data.status == "hasHistory") {
                //    htmls = " <div class='panel panel-default property'>"
                //    // console.log(data.listHistory.length);
                //    $.each(data.listHistory, function (i, item) {
                //        //console.log('test' + item.htTo); console.log(data.listHistory[0].htTo);
                //        console.log("OK")
                //        htmls += "     <div class='panel-heading panel-heading-custom property' tabindex = '0' >"
                //        htmls += "         <h4 class='panel-title faq-title-range collapsed' data-bs-toggle='collapse' data-bs-target='#Ans" + item.htStep + "' aria-expanded='false' aria-controls='collapseExample'>"
                //        htmls += "             <label style='font-size: 13px;'>Step : " + (item.htStep + 1) + "</label> <label class='lbV'></label>"
                //        if (item.htStep == 0) {
                //            htmls += "<label  style='color:red'>Disapprove</label>"
                //            htmls += "<label  style='color:red'>" + item.htStatus + "</label>"
                //        }
                //        else if (item.htStep == 1) {

                //            htmls += " <label  style='color:mediumpurple'>Waiting for approval of Dept.</label>"
                //            htmls += "<label  style='color:mediumpurple'>" + item.htStatus + "</label>"
                //        } else if (item.htStep == 2) {

                //            htmls += " <label  style='color:orange'>Waiting for approval by CS of IS</label>"
                //            htmls += "<label  style='color:orange'>" + item.htStatus + "</label>"
                //        }
                //        else if (item.htStep == 3) {

                //            htmls += " <label  style='color:blue'>Process</label>"
                //            htmls += "<label  style='color:blue'>" + item.htStatus + "</label>"
                //        }
                //        else if (item.htStep == 4) {

                //            htmls += " <label  style='color:yellow'>Waiting for accept by CS of IS</label>"
                //            htmls += "<label  style='color:yellow'>" + item.htStatus + "</label>"
                //        }
                //        else if (item.htStep == 5) {

                //            htmls += " <label  style='color:Green'>Finish</label>"
                //            htmls += "<label  style='color:Green'>" + item.htStatus + "</label>"
                //        }


                //        htmls += "         </h4>"
                //        htmls += "     </div >"
                //        htmls += "     <div class='panel-collapse collapse' style = 'overflow: auto;' id = 'Ans" + item.htStep + "' > "

                //        htmls += "         <div class='panel-body'>"
                //        htmls += "             <div style='font-size: x-small; clear: both; width: 100%; tetx-align: left; font-weight: bold;'>"
                //        htmls += "                 <label> " + item.htDate + " :: " + item.htTime + " น.</label>"

                //        htmls += "             </div>"
                //        htmls += "             <div style='font-size: x-small; float: left; width: 20%; tetx-align: left;'>"
                //        htmls += "                 <label>FROM : </label></br>"
                //        htmls += "                 <label>" + item.htFrom + "</label > "
                //        htmls += "             </div>"
                //        htmls += "             <div style='font-size: x-small; float: left; width: 20%; tetx-align: left;'>"
                //        htmls += "                 <label>TO : </label></br>"
                //        htmls += "                 <label>" + item.htTo + "</label>"
                //        htmls += "             </div>"
                //        htmls += "             <div style='font-size: x-small; float: left; width: 20%; tetx-align: left;'>"
                //        if (item.htCC == null) { item.htCC = "" }
                //        else { item.htCC = item.htCC }
                //        htmls += "                 <label>CC : </label>"
                //        htmls += "                 <label>" + item.htCC + "</label>"
                //        htmls += "             </div>"
                //        htmls += "             <div style='font-size: x-small; float: right; width: 20%; tetx-align: left;'>"
                //        if (item.htRemark == null) { item.htRemark = "" }
                //        else { item.htRemark = item.htRemark }
                //        htmls += "                 <label>Remark : </label>"
                //        htmls += "                 <label>" + item.htRemark + "</label>"
                //        htmls += "             </div>"
                //        htmls += "             <div style='font-size: x-small; float: right; width: 20%; tetx-align: left;'>"
                //        htmls += "                 <label>Status : </label>"
                //        if (item.htStatus == null) { item.htStatus = "" }
                //        else {
                //            item.htStatus = item.htStatus
                //            if (item.htStatus == "Finished") {
                //                htmls += "                 <label><span style='color: green;'>" + item.htStatus + "</span></label>"
                //            } else {
                //                htmls += "                 <label><span style='color: darkkhaki;'>" + item.htStatus + "</span></label>"
                //            }
                //        }

                //        htmls += "             </div>"
                //        htmls += "         </div>"
                //        htmls += "     </div>"

                //    });
                //    htmls += "</div>"
                //}
                var url = data.partial + mydata + "&vform=" + vform;


                console.log("url" + url);
                $("#myModalBodyDiv1").load(url, function () {
                   // $('#divHistory').html(htmls);
                    $("#myModal1").modal("show");
                })
            }
        });
    } else {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่ต้องการ !!!",

        })
            .then((result) => {
                document.getElementById("i_EDate").value = "";

            });

    }


}
function sendmailF3Inform(getID, action) {
    console.log("sssssss");
    let vmsg = "";
    if (document.getElementById("txtF3MastNB").value == "") {
        vmsg = "กรุณากรอกชื่อเครื่อง  !!!";
    }
    else if (document.getElementById("txtF3ReturnStartDate").value == "") {
        vmsg = "กรุณากรอก Start Date(วันที่ยืม)  !!!";
    }
    else if (document.getElementById("txtF3ReturnEndDate").value == "") {
        vmsg = "กรุณากรอก End Date(วันที่คืน)  !!!";
    }


    //txtF3MastNB
    //txtF3ReturnStartDate
    //txtF3ReturnEndDate
    if (vmsg != "") {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: vmsg,

        })
            .then((result) => {


            });
    }
    else {
        let vform = document.getElementById("txtvForm").value;
        let mydata = $("#formRequest").serialize();
        $.ajax({
            type: 'post',
            url: action,
            data: mydata,//{ getID: getID }, // mydata ,//
            success: function (data) {

                //document.getElementById("txtStatus").value == document.getElementById("txtwsrStatus").value;

                console.log("sendMail worker");
                //$("#myModal1").modal("show");
                var htmls = "";
                if (data.status == "hasHistory") {
                    htmls = ""

                }
                var url = data.partial + mydata + "&vform=" + vform;
                // //var url = data.partial ;//+ "?class=" + mydata;
                //// $("#myModalBodyDiv1").html("");
                // //var url = String($(url).data(mydata));
                $("#myModalBodyDiv1").load(url, function () {
                    //  $('#divHistory').html(htmls);
                    $("#myModal1").modal("show");

                })






            }
        });
    }
}
function sendMailWorker1(getID, action) {
    //const formdata = new FormData(document.forms.item(0)).serialize();
    //document.getElementById('searchInputTO').value = "";
    let vstep = document.getElementById("txtMsrStep").value;

    let formData = document.forms.namedItem("formData");
    let viewModel = new FormData(formData);

    $.each(formData, function (index, input) {
        viewModel.append(input.name, input.value);
    });


    let mydata = $("#formRequest").serialize();
    //let vEdate = document.getElementById("i_EDate").value;
    let vform = document.getElementById("txtvForm").value;
    if (vform == "F7")//ITMS System register
    {
        if (document.getElementById("txtF7txtF7itObjectiveNew").checked == false &&
            document.getElementById("txtF7txtF7itObjectiveChange").checked == false &&
            document.getElementById("txtF7txtF7itObjectiveCancel").checked == false) {
            msg = "กรุณาเลือกวัตถุประสงค์ !!!";
        }
        else if (document.getElementById("txtF7ITMSitEmpcode").value == "") {
            msg = "กรุณากรอกรหัสพนักงาน !!!";
        }

        if (document.getElementById("txtF7itMPcLan").checked == true) {
            if (document.getElementById("txtF7itPcLan_TypeThai").checked == false &&
                document.getElementById("txtF7itPcLan_TypeJapan").checked == false) {
                msg = "กรุณาเลือกประเภทขอใช้งานระบบ PC-Lan!!!";
            }
            else {
                if (document.getElementById("txtF7itPcLan_TypeThai").checked == true) {
                    $('#txtF7itPcLan_TypeThai').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itPcLan_TypeJapan").checked == true) {
                    $('#txtF7itPcLan_TypeJapan').removeAttr('disabled', 'disabled');
                }
            }
        }
        if (document.getElementById("txtF7itMmail").checked == true) {
            if (document.getElementById("txtF7itMail_TypeLOTUSNOTES").checked == false &&
                document.getElementById("txtF7itMail_TypeOUTLOOK").checked == false) {
                msg = "กรุณาเลือกประเภทขอใช้งานระบบ Mail!!!";
            }
            else {
                if (document.getElementById("txtF7itMail_TypeLOTUSNOTES").checked == true) {
                    $('#txtF7itMail_TypeLOTUSNOTES').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itMail_TypeOUTLOOK").checked == true) {
                    $('#txtF7itMail_TypeOUTLOOK').removeAttr('disabled', 'disabled');
                }
            }
        }
        if (document.getElementById("txtF7itMInternet").checked == true) {
            if (document.getElementById("txtF7itObjectiveTemporaryUser").checked == false &&
                document.getElementById("txtF7itObjectiveGeneralInformation").checked == false &&
                document.getElementById("txtF7itObjectiveResearchfortheJob").checked == false &&
                document.getElementById("txtF7itObjectiveDirectConcernOnThejob").checked == false &&
                document.getElementById("txtF7itObjectiveOutsideCommunicationByE").checked == false &&
                document.getElementById("txtF7itObjectiveGeneralInformationT9").checked == false) {
                msg = "กรุณาเลือกประเภทขอใช้งาน Internet !!!";
            }
            else {
                if (document.getElementById("txtF7itObjectiveTemporaryUser").checked == true) {
                    $('#txtF7itObjectiveTemporaryUser').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveGeneralInformation").checked == true) {
                    $('#txtF7itObjectiveGeneralInformation').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveResearchfortheJob").checked == true) {
                    $('#txtF7itObjectiveResearchfortheJob').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveDirectConcernOnThejob").checked == true) {
                    $('#txtF7itObjectiveDirectConcernOnThejob').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveOutsideCommunicationByE").checked == true) {
                    $('#txtF7itObjectiveOutsideCommunicationByE').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveGeneralInformationT9").checked == true) {
                    $('#txtF7itObjectiveGeneralInformationT9').removeAttr('disabled', 'disabled');
                }
            }



        }
    }



    $.ajax({
        type: "POST",
        url: action,
        data: viewModel,
        processData: false,
        contentType: false,
        beforeSend: function () {
            swal.fire({
                html: '<h5>Loading...</h5>',
                showConfirmButton: false,
                onRender: function () {
                    // there will only ever be one sweet alert open.
                    //$('.swal2-content').prepend(sweet_loader);
                }
            });
        },
        success: function (response) {
            $("#ResultSendMail").html(response); // เอา HTML Partial View มาใส่ใน Div
            $("#myModal1").modal("show");
        },
        error: function () {
            alert("Error!!");
        }
    });


}


function sendMailWorker(getID, action) {
    //const formdata = new FormData(document.forms.item(0)).serialize();
    //document.getElementById('searchInputTO').value = "";
    let vstep = document.getElementById("txtMsrStep").value;


    //let vEdate = document.getElementById("i_EDate").value;
    let vform = document.getElementById("txtvForm").value;
    if (vform == "F7")//ITMS System register
    {
        if (document.getElementById("txtF7txtF7itObjectiveNew").checked == false &&
            document.getElementById("txtF7txtF7itObjectiveChange").checked == false &&
            document.getElementById("txtF7txtF7itObjectiveCancel").checked == false) {
            msg = "กรุณาเลือกวัตถุประสงค์ !!!";
        }
        else if (document.getElementById("txtF7ITMSitEmpcode").value == "") {
            msg = "กรุณากรอกรหัสพนักงาน !!!";
        }

        if (document.getElementById("txtF7itMPcLan").checked == true) {
            if (document.getElementById("txtF7itPcLan_TypeThai").checked == false &&
                document.getElementById("txtF7itPcLan_TypeJapan").checked == false) {
                msg = "กรุณาเลือกประเภทขอใช้งานระบบ PC-Lan!!!";
            }
            else {
                if (document.getElementById("txtF7itPcLan_TypeThai").checked == true) {
                    $('#txtF7itPcLan_TypeThai').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itPcLan_TypeJapan").checked == true) {
                    $('#txtF7itPcLan_TypeJapan').removeAttr('disabled', 'disabled');
                }
            }
        }
        if (document.getElementById("txtF7itMmail").checked == true) {
            if (document.getElementById("txtF7itMail_TypeLOTUSNOTES").checked == false &&
                document.getElementById("txtF7itMail_TypeOUTLOOK").checked == false) {
                msg = "กรุณาเลือกประเภทขอใช้งานระบบ Mail!!!";
            }
            else {
                if (document.getElementById("txtF7itMail_TypeLOTUSNOTES").checked == true) {
                    $('#txtF7itMail_TypeLOTUSNOTES').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itMail_TypeOUTLOOK").checked == true) {
                    $('#txtF7itMail_TypeOUTLOOK').removeAttr('disabled', 'disabled');
                }
            }
        }
        if (document.getElementById("txtF7itMInternet").checked == true) {
            if (document.getElementById("txtF7itObjectiveTemporaryUser").checked == false &&
                document.getElementById("txtF7itObjectiveGeneralInformation").checked == false &&
                document.getElementById("txtF7itObjectiveResearchfortheJob").checked == false &&
                document.getElementById("txtF7itObjectiveDirectConcernOnThejob").checked == false &&
                document.getElementById("txtF7itObjectiveOutsideCommunicationByE").checked == false &&
                document.getElementById("txtF7itObjectiveGeneralInformationT9").checked == false) {
                msg = "กรุณาเลือกประเภทขอใช้งาน Internet !!!";
            }
            else {
                if (document.getElementById("txtF7itObjectiveTemporaryUser").checked == true) {
                    $('#txtF7itObjectiveTemporaryUser').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveGeneralInformation").checked == true) {
                    $('#txtF7itObjectiveGeneralInformation').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveResearchfortheJob").checked == true) {
                    $('#txtF7itObjectiveResearchfortheJob').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveDirectConcernOnThejob").checked == true) {
                    $('#txtF7itObjectiveDirectConcernOnThejob').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveOutsideCommunicationByE").checked == true) {
                    $('#txtF7itObjectiveOutsideCommunicationByE').removeAttr('disabled', 'disabled');
                }
                if (document.getElementById("txtF7itObjectiveGeneralInformationT9").checked == true) {
                    $('#txtF7itObjectiveGeneralInformationT9').removeAttr('disabled', 'disabled');
                }
            }



        }
    }
    let vmsg = "";

    //if (document.getElementById("txtwsrStatus").value == "") {
    //    vmsg = "กรุณากรอกข้อมูล สถานะการปฏิบัติงาน ให้ครบถ้วน  !!!";
    //}
    //else if (document.getElementById("txtwExpFinish").value == "") {
    //    vmsg = "กรุณากรอกข้อมูล วันคาดว่างานเสร็จสิ้น ให้ครบถ้วน  !!!";
    //}
    //else if (document.getElementById("txtwFinishDate").value == "") {
    //    vmsg = "กรุณากรอกข้อมูล วันที่งานเสร็จสิ้น ให้ครบถ้วน !!!";
    //}
    //else if (document.getElementById("txtwTotalWorkTime").value == "") {
    //    vmsg = "กรุณากรอกข้อมูล เวลาการทำงานทั้งหมด ให้ครบถ้วน !!!";
    //}
    //else if (document.getElementById("txtwSolveProblem").value == "") {
    //    vmsg = "กรุณากรอกข้อมูล วิธีการแก้ไข ให้ครบถ้วน !!!";
    //}

    //operator 07/05/2025 11:28
    if (document.getElementById("txtwsrStatus").value.includes("Process")) {
        vmsg = "กรุณากรอก อัพเดทสถานะการทำงาน !!!";
    }
    else if (document.getElementById("txtwExpFinish").value == "") {
        vmsg = "กรุณากรอก วันคาดว่างานเสร็จสิ้น !!!";
    }
    else if (document.getElementById("txtwFinishDate").value == "") {
        vmsg = "กรุณากรอก วันที่งานเสร็จสิ้น  !!!";
    }
    else if (document.getElementById("txtwTotalWorkTime").value == "") {
        vmsg = "กรุณากรอก เวลาการทำงานทั้งหมด(ชั่วโมง)  !!!";
    }
    if (vmsg != "") {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: vmsg,

        })
            .then((result) => {


            });
    } else {
        let mydata = $("#formRequest").serialize();


        $.ajax({
            type: 'post',
            url: action,
            data: mydata,//{ getID: getID }, // mydata ,//
            success: function (data) {

                console.log("sendMail worker");
                //$("#myModal1").modal("show");
                var htmls = "";
                if (data.status == "hasHistory") {
                    htmls = " <div class='panel panel-default property'>"
                    // console.log(data.listHistory.length);
                    $.each(data.listHistory, function (i, item) {
                        //console.log('test' + item.htTo); console.log(data.listHistory[0].htTo);
                        console.log("OK")
                        htmls += "     <div class='panel-heading panel-heading-custom property' tabindex = '0' >"
                        htmls += "         <h4 class='panel-title faq-title-range collapsed' data-bs-toggle='collapse' data-bs-target='#Ans" + item.htStep + "' aria-expanded='false' aria-controls='collapseExample'>"
                        htmls += "             <label style='font-size: 13px;'>Step " + item.htStep + "</label> <label class='lbV'></label>"
                        if (item.htStep == 0) {

                            htmls += " <label  style='color:red'>Disapprove</label>"
                        }
                        else if (item.htStep == 1) {

                            htmls += " <label  style='color:mediumpurple'>Waiting for approval of Dept.</label>"
                        } else if (item.htStep == 2) {

                            htmls += " <label  style='color:orange'>Waiting for approval by CS of IS</label>"
                        }
                        else if (item.htStep == 3) {

                            htmls += " <label  style='color:blue'>Process</label>"
                        }
                        else if (item.htStep == 4) {

                            htmls += " <label  style='color:yellow'>Waiting for accept by CS of IS</label>"
                        }
                        else if (item.htStep == 5) {

                            htmls += " <label  style='color:Green'>Finish</label>"
                        }

                        htmls += "         </h4>"
                        htmls += "     </div >"
                        htmls += "     <div class='panel-collapse collapse' style = 'overflow: auto;' id = 'Ans" + item.htStep + "' > "

                        htmls += "         <div class='panel-body'>"
                        htmls += "             <div style='font-size: x-small; clear: both; width: 100%; tetx-align: left; font-weight: bold;'>"
                        htmls += "                 <label> " + item.htDate + " :: " + item.htTime + " น.</label>"
                        htmls += "             </div>"
                        htmls += "             <div style='font-size: x-small; float: left; width: 20%; tetx-align: left;'>"
                        htmls += "                 <label>FROM : </label></br>"
                        htmls += "                 <label>" + item.htFrom + "</label > "
                        htmls += "             </div>"
                        htmls += "             <div style='font-size: x-small; float: left; width: 20%; tetx-align: left;'>"
                        htmls += "                 <label>TO : </label></br>"
                        htmls += "                 <label>" + item.htTo + "</label>"
                        htmls += "             </div>"
                        htmls += "             <div style='font-size: x-small; float: left; width: 20%; tetx-align: left;'>"
                        if (item.htCC == null) { item.htCC = "" }
                        else { item.htCC = item.htCC }
                        htmls += "                 <label>CC : </label>"
                        htmls += "                 <label>" + item.htCC + "</label>"
                        htmls += "             </div>"
                        htmls += "             <div style='font-size: x-small; float: right; width: 20%; tetx-align: left;'>"
                        if (item.htRemark == null) { item.htRemark = "" }
                        else { item.htRemark = item.htRemark }
                        htmls += "                 <label>Remark : </label>"
                        htmls += "                 <label>" + item.htRemark + "</label>"
                        htmls += "             </div>"
                        htmls += "             <div style='font-size: x-small; float: right; width: 20%; tetx-align: left;'>"
                        htmls += "                 <label>Status : </label>"
                        if (item.htStatus == null) { item.htStatus = "" }
                        else {
                            item.htStatus = item.htStatus
                            if (item.htStatus == "Finished") {
                                htmls += "                 <label><span style='color: green;'>" + item.htStatus + "</span></label>"
                            } else {
                                htmls += "                 <label><span style='color: darkkhaki;'>" + item.htStatus + "</span></label>"
                            }
                        }

                        htmls += "             </div>"
                        htmls += "         </div>"
                        htmls += "     </div>"

                    });
                    htmls += "</div>"
                }
                // var url = data.partial + mydata + "&vform=" + vform;
                var url = data.partial + mydata;
                // //var url = data.partial ;//+ "?class=" + mydata;
                //// $("#myModalBodyDiv1").html("");
                // //var url = String($(url).data(mydata));
                $("#myModalBodyDiv1").load(url, function () {
                    document.getElementById("searchInputTO").value = data.nameIssue;
                    $('#divHistory').html(htmls);
                    $("#myModal1").modal("show");

                })




            }
        });
    }
    //else if (vEdate != null && vEdate != "") {


}


function OpenfileUsbNew(getID, action) {
    $.ajax({
        type: 'post',
        url: action,
        data: "",//mydata,//{ getID: getID }, // mydata ,//
        //contentType: "application/json; charset=utf-8",
        success: function (data) {

            //console.log("fsendMail");
            ////$("#myModal1").modal("show");
            //var htmls = "";
            //if (data.status == "hasHistory") {
            //    htmls = " <div class='panel panel-default property'>"
            //    // console.log(data.listHistory.length);
            //    $.each(data.listHistory, function (i, item) {
            //        //console.log('test' + item.htTo); console.log(data.listHistory[0].htTo);
            //        console.log("OK")
            //        htmls += "     <div class='panel-heading panel-heading-custom property' tabindex = '0' >"
            //        htmls += "         <h4 class='panel-title faq-title-range collapsed' data-bs-toggle='collapse' data-bs-target='#Ans" + item.htStep + "' aria-expanded='false' aria-controls='collapseExample'>"
            //        htmls += "             <label style='font-size: 13px;'>Step " + item.htStep + "</label> <label class='lbV'></label>"
            //        htmls += "         </h4>"
            //        htmls += "     </div >"
            //        htmls += "     <div class='panel-collapse collapse' style = 'overflow: auto;' id = 'Ans" + item.htStep + "' > "

            //        htmls += "         <div class='panel-body'>"
            //        htmls += "             <div style='font-size: x-small; clear: both; width: 100%; tetx-align: left; font-weight: bold;'>"
            //        htmls += "                 <label> " + item.htDate + " :: " + item.htTime + " น.</label>"
            //        htmls += "             </div>"
            //        htmls += "             <div style='font-size: x-small; float: left; width: 20%; tetx-align: left;'>"
            //        htmls += "                 <label>FROM : </label></br>"
            //        htmls += "                 <label>" + item.htFrom + "</label > "
            //        htmls += "             </div>"
            //        htmls += "             <div style='font-size: x-small; float: left; width: 20%; tetx-align: left;'>"
            //        htmls += "                 <label>TO : </label></br>"
            //        htmls += "                 <label>" + item.htTo + "</label>"
            //        htmls += "             </div>"
            //        htmls += "             <div style='font-size: x-small; float: left; width: 20%; tetx-align: left;'>"
            //        if (item.htCC == null) { item.htCC = "" }
            //        else { item.htCC = item.htCC }
            //        htmls += "                 <label>CC : </label>"
            //        htmls += "                 <label>" + item.htCC + "</label>"
            //        htmls += "             </div>"
            //        htmls += "             <div style='font-size: x-small; float: right; width: 20%; tetx-align: left;'>"
            //        if (item.htRemark == null) { item.htRemark = "" }
            //        else { item.htRemark = item.htRemark }
            //        htmls += "                 <label>Remark : </label>"
            //        htmls += "                 <label>" + item.htRemark + "</label>"
            //        htmls += "             </div>"
            //        htmls += "             <div style='font-size: x-small; float: right; width: 20%; tetx-align: left;'>"
            //        htmls += "                 <label>Status : </label>"
            //        if (item.htStatus == null) { item.htStatus = "" }
            //        else {
            //            item.htStatus = item.htStatus
            //            if (item.htStatus == "Finished") {
            //                htmls += "                 <label><span style='color: green;'>" + item.htStatus + "</span></label>"
            //            } else {
            //                htmls += "                 <label><span style='color: darkkhaki;'>" + item.htStatus + "</span></label>"
            //            }
            //        }

            //        htmls += "             </div>"
            //        htmls += "         </div>"
            //        htmls += "     </div>"

            //    });
            //    htmls += "</div>"
            //}
            //var url = data.partial + mydata + "&vform=" + vform;
            //$("#myModalBodyDiv4").load(url, function () {
            //$('#divHistory').html(htmls);
            $("#myModal4").modal("show");

            //})
        }
    });
}


function SearchData(action) {
    let mydata = $("#formSearch").serialize();
    $.ajax({
        type: 'post',
        url: action,
        data: mydata,
        success: function (html) {

            //console.log(html);
            var parser = new DOMParser();
            var doc = parser.parseFromString(html, "text/html");
            //console.log(doc.getElementById("FormNameRequest_1"));
            var ToContent = doc.getElementById("formSearch").outerHTML;
            var displayContent = document.getElementsByClassName("result-container1 ").item(0);
            displayContent.innerHTML = ToContent;



        }
    });
}
function DeleteData(id, action) {
    Swal.fire({
        title: "Are you sure?",
        text: "Are you sure delete?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes",
        cancelButtonText: "No"
    }).then((result) => {
        if (result['isConfirmed']) {
            $.ajax({
                type: 'post',
                url: action,
                data: { id: id },
                success: function (res) {
                    swal.fire({
                        title: 'แจ้งเตือน',
                        icon: res.res,
                        text: res.res,
                    })
                        .then((result) => {
                            GoSideMenu("Home");

                        });



                }
            });
        } else {
            //console.log('Cancel');
            return false;
        }
    });
}

function DeleteFileUser(id, vname, action) {
    var getID = document.getElementById("txtMIssueID").value; //txtMIssueID
    var getEvent = "Edit";
    var vaction = "";//
    var vForm = document.getElementById("txtMsrFrom").value;//txtMsrFrom
    var vTeam = document.getElementById("txtMsrType").value;//txtMsrType
    //var vSubject = document.getElementById("txtMsrSubject").value;// txtMsrSubject
    var vSubject = document.getElementById("txtSUbject").value;// txtSUbject

    var vSrNo = document.getElementById("txtSRno").value;// txtSRno

    //action, vForm, vTeam, vSubject, vSrNo


    Swal.fire({
        title: "Are you sure?",
        text: "Are you sure delete File?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes",
        cancelButtonText: "No"
    }).then((result) => {
        if (result['isConfirmed']) {
            $.ajax({
                type: 'post',
                url: action,
                data: { id: id, vname: vname },
                success: function (res) {
                    swal.fire({
                        title: 'แจ้งเตือน',
                        icon: res.res,
                        text: res.res,
                    })
                        .then((result) => {
                            //GoSideMenu("Home");
                            GoNewRequest(getID, getEvent, vaction, vForm, vTeam, vSubject, vSrNo)
                        });



                }
            });
        } else {
            //console.log('Cancel');
            return false;
        }
    });
}



function sendmailsubmit(action) {
    //let vform = document.getElementById("txtvFSM").value;
    let vform = document.getElementById("txtvForm").value;
    let vstep = document.getElementById("txtMsrStep").value;
    let vmsg = "";


    //let mydata = $("#formRequest").serialize() + "&vform=" + vform;
    let mydata = $("#formRequest").serialize(); //F6
    let formData = document.forms.namedItem("formData");
    let viewModel = new FormData(formData);

    //$.each(formData, function (index, input) {
    //    viewModel.append(input.name, input.value);
    //});
    //console.log($('input[name="files1"]'));
    //console.log($('input[name="files2"]'));
    if (vform == "F7") {

        $('#txtF7itPcLan_TypeThai').removeAttr('disabled', 'disabled');
        $('#txtF7itPcLan_TypeJapan').removeAttr('disabled', 'disabled');

        $('#txtF7itMail_TypeLOTUSNOTES').removeAttr('disabled', 'disabled');
        $('#txtF7itMail_TypeOUTLOOK').removeAttr('disabled', 'disabled');

        $('#txtF7itObjectiveTemporaryUser').removeAttr('disabled', 'disabled');
        $('#txtF7itObjectiveGeneralInformation').removeAttr('disabled', 'disabled');
        $('#txtF7itObjectiveResearchfortheJob').removeAttr('disabled', 'disabled');
        $('#txtF7itObjectiveDirectConcernOnThejob').removeAttr('disabled', 'disabled');
        $('#txtF7itObjectiveOutsideCommunicationByE').removeAttr('disabled', 'disabled');
        $('#txtF7itObjectiveGeneralInformationT9').removeAttr('disabled', 'disabled');

    }

    $.each(formData, function (index, input) {
        viewModel.append(input.name, input.value);
    });

    //if (vstep > 2 && document.getElementById("txtwsrStatus").value != "Cancel") { Transfer
    if (vstep > 2 && document.getElementById("txtwsrStatus").value != "Transfer" && document.getElementById("txtwsrStatus").value != "Cancel") {
        if (document.getElementById("txtwsrStatus").value == "") {
            vmsg = "กรุณากรอกข้อมูล สถานะการปฏิบัติงาน ให้ครบถ้วน  !!!";
        }
        else if (document.getElementById("txtwExpFinish").value == "") {
            vmsg = "กรุณากรอกข้อมูล วันคาดว่างานเสร็จสิ้น ให้ครบถ้วน  !!!";
        }
        else if (document.getElementById("txtwFinishDate").value == "") {
            vmsg = "กรุณากรอกข้อมูล วันที่งานเสร็จสิ้น ให้ครบถ้วน !!!";
        }
        else if (document.getElementById("txtwTotalWorkTime").value == "") {
            vmsg = "กรุณากรอกข้อมูล เวลาการทำงานทั้งหมด ให้ครบถ้วน !!!";
        }
        else if (document.getElementById("txtwSolveProblem").value == "") {
            vmsg = "กรุณากรอกข้อมูล วิธีการแก้ไข ให้ครบถ้วน !!!";
        }

        else if (vform == "F3" && vstep > 2) {
            if (document.getElementById("txtF3MastNB").value == "") {
                vmsg = "กรุณากรอกข้อมูลชื่อเครื่อง (Operator) ให้ครบถ้วน !!!";
            }
            else if (document.getElementById("txtF3ReturnStartDate").value == "") {
                vmsg = "กรุณากรอกข้อมูล Start Date(วันที่ยืม)(Operator) ให้ครบถ้วน !!!";
            }
            else if (document.getElementById("txtF3ReturnEndDate").value == "") {
                vmsg = "กรุณากรอกข้อมูล End Date(วันที่คืน)(Operator) ให้ครบถ้วน !!!";
            }
        }
    }


    if (vmsg != "") {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: vmsg,

        })
            .then((result) => {
            });
    } else {
        $.ajax({
            type: "POST",
            url: action,
            data: viewModel,
            processData: false,
            contentType: false,
            beforeSend: function () {
                swal.fire({
                    html: '<h5>Loading...</h5>',
                    showConfirmButton: false,
                    onRender: function () {
                        // there will only ever be one sweet alert open.
                        //$('.swal2-content').prepend(sweet_loader);
                    }
                });
            },
            success: async function (config) {
                // alert(config.c1);
                if (config.c1 == "S" || config.c1 == "D") {
                    $("#loaderDiv").hide();
                    await $("#myModal1").modal("hide");
                    swal.fire({
                        title: 'SUCCESS',
                        icon: 'success',
                        text: "Send Mail Already",
                    }).then((result) => {
                        if (result.isConfirmed) {
                            console.log("config.c3" + config.c3);
                            GoSideMenu("Home");
                        }
                    });
                }
                else if (config.c1 == "E") {
                    //$("#loaderDiv").hide();
                    //await $("#myModal1").modal("hide");
                    Swal.fire({
                        icon: 'error',
                        title: 'ERROR',
                        text: config.c2,
                    })
                        .then((result) => {
                            $("#myModal1").modal("show");
                        });

                }
                else if (config.c1 == "P") {
                    //$("#loaderDiv").hide();
                    //await $("#myModal1").modal("hide");
                    await $("#myModal1").modal("hide");
                    Swal.fire({
                        icon: 'warning',
                        title: 'warning',
                        text: config.c2,
                    })
                        .then((result) => {

                            //$("#myModal1").modal("show");
                        });

                }

            }
        });
    }











}




function sendmailsubmitF4(action) {
    let vform = document.getElementById("txtvFSM").value;

    let vstep = document.getElementById("txtMsrStep").value;
    let vmsg = "";


    //let mydata = $("#formRequest").serialize() + "&vform=" + vform;
    let mydata = $("#formRequest").serialize(); //F6
    let formData = document.forms.namedItem("formData");
    let viewModel = new FormData(formData);

    //$.each(formData, function (index, input) {
    //    viewModel.append(input.name, input.value);
    //});
    //console.log($('input[name="files1"]'));
    //console.log($('input[name="files2"]'));
    if (vform == "F7") {

        $('#txtF7itPcLan_TypeThai').removeAttr('disabled', 'disabled');
        $('#txtF7itPcLan_TypeJapan').removeAttr('disabled', 'disabled');

        $('#txtF7itMail_TypeLOTUSNOTES').removeAttr('disabled', 'disabled');
        $('#txtF7itMail_TypeOUTLOOK').removeAttr('disabled', 'disabled');

        $('#txtF7itObjectiveTemporaryUser').removeAttr('disabled', 'disabled');
        $('#txtF7itObjectiveGeneralInformation').removeAttr('disabled', 'disabled');
        $('#txtF7itObjectiveResearchfortheJob').removeAttr('disabled', 'disabled');
        $('#txtF7itObjectiveDirectConcernOnThejob').removeAttr('disabled', 'disabled');
        $('#txtF7itObjectiveOutsideCommunicationByE').removeAttr('disabled', 'disabled');
        $('#txtF7itObjectiveGeneralInformationT9').removeAttr('disabled', 'disabled');

    }

    $.each(formData, function (index, input) {
        viewModel.append(input.name, input.value);
    });

    //if (vstep > 2) {
    //    if (document.getElementById("txtwsrStatus").value == "") {
    //        vmsg = "กรุณากรอกข้อมูล สถานะการปฏิบัติงาน ให้ครบถ้วน  !!!";
    //    }
    //    else if (document.getElementById("txtwExpFinish").value == "") {
    //        vmsg = "กรุณากรอกข้อมูล วันคาดว่างานเสร็จสิ้น ให้ครบถ้วน  !!!";
    //    }
    //    else if (document.getElementById("txtwFinishDate").value == "") {
    //        vmsg = "กรุณากรอกข้อมูล วันที่งานเสร็จสิ้น ให้ครบถ้วน !!!";
    //    }
    //    else if (document.getElementById("txtwTotalWorkTime").value == "") {
    //        vmsg = "กรุณากรอกข้อมูล เวลาการทำงานทั้งหมด ให้ครบถ้วน !!!";
    //    }
    //    else if (document.getElementById("txtwSolveProblem").value == "") {
    //        vmsg = "กรุณากรอกข้อมูล วิธีการแก้ไข ให้ครบถ้วน !!!";
    //    }
    //}


    if (vmsg != "") {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: vmsg,

        })
            .then((result) => {


            });
    } else {
        $.ajax({
            type: "POST",
            url: action,
            data: viewModel,
            processData: false,
            contentType: false,
            beforeSend: function () {
                swal.fire({
                    html: '<h5>Loading...</h5>',
                    showConfirmButton: false,
                    onRender: function () {
                        // there will only ever be one sweet alert open.
                        //$('.swal2-content').prepend(sweet_loader);
                    }
                });
            },
            success: async function (config) {
                // alert(config.c1);
                if (config.c1 == "S" || config.c1 == "D") {
                    $("#loaderDiv").hide();
                    await $("#myModal1").modal("hide");
                    swal.fire({
                        title: 'SUCCESS',
                        icon: 'success',
                        text: "Send Mail Already",
                    }).then((result) => {
                        if (result.isConfirmed) {
                            console.log("config.c3" + config.c3);
                            GoSideMenu("Home");
                        }
                    });
                }
                else if (config.c1 == "E") {
                    //$("#loaderDiv").hide();
                    //await $("#myModal1").modal("hide");
                    Swal.fire({
                        icon: 'error',
                        title: 'ERROR',
                        text: config.c2,
                    })
                        .then((result) => {
                            $("#myModal1").modal("show");
                        });

                }
                else if (config.c1 == "P") {
                    //$("#loaderDiv").hide();
                    //await $("#myModal1").modal("hide");
                    await $("#myModal1").modal("hide");
                    Swal.fire({
                        icon: 'warning',
                        title: 'warning',
                        text: config.c2,
                    })
                        .then((result) => {

                            //$("#myModal1").modal("show");
                        });

                }

            }
        });
    }











}


function savesubmit(action) {


    var getID = document.getElementById("txtMIssueID").value; //txtMIssueID
    var getEvent = "Edit";
    var vaction = "";//
    var vForm = document.getElementById("txtMsrFrom").value;//txtMsrFrom
    var vTeam = document.getElementById("txtMsrType").value;//txtMsrType
    //var vSubject = document.getElementById("txtMsrSubject").value;// txtMsrSubject
    var vSubject = document.getElementById("txtSUbject").value;// txtMsrSubject
    var vSrNo = document.getElementById("txtSRno").value;// txtSRno



    let vform = document.getElementById("txtvForm").value;

    let msg = "";

    let mydata = $("#formRequest").serialize(); //F6
    let formData = document.forms.namedItem("formData");
    let viewModel = new FormData(formData);

    $.each(formData, function (index, input) {
        viewModel.append(input.name, input.value);
    });



    $.ajax({
        type: "POST",
        url: action,
        data: viewModel,
        processData: false,
        contentType: false,
        beforeSend: function () {
            swal.fire({
                html: '<h5>Loading...</h5>',
                showConfirmButton: false,
                onRender: function () {
                    // there will only ever be one sweet alert open.
                    //$('.swal2-content').prepend(sweet_loader);
                }
            });
        },
        success: async function (config) {
            // alert(config.c1);
            if (config.c1 == "S" || config.c1 == "D") {
                $("#loaderDiv").hide();
                await $("#myModal1").modal("hide");
                swal.fire({
                    title: 'SUCCESS',
                    icon: 'success',
                    text: "Save data Already",
                }).then((result) => {
                    if (result.isConfirmed) {
                        console.log("config.c3" + config.c3);
                        // GoSideMenu("Home");

                        GoNewRequest(getID, getEvent, vaction, vForm, vTeam, vSubject, vSrNo)
                    }
                });
            }
            else if (config.c1 == "E") {
                //$("#loaderDiv").hide();
                //await $("#myModal1").modal("hide");
                Swal.fire({
                    icon: 'error',
                    title: 'ERROR',
                    text: config.c2,
                })
                    .then((result) => {
                        $("#myModal1").modal("show");
                    });

            }
            else if (config.c1 == "P") {
                //$("#loaderDiv").hide();
                //await $("#myModal1").modal("hide");
                await $("#myModal1").modal("hide");
                Swal.fire({
                    icon: 'warning',
                    title: 'warning',
                    text: config.c2,
                })
                    .then((result) => {

                        //$("#myModal1").modal("show");
                    });

            }

        }
    });




}


function getListNotebook(action) {
    //console.log("aaaaaaaa");
    let mydata = $("#formRequest").serialize();
    $.ajax({
        type: 'post',
        url: action,
        data: mydata,//{ getID: getID }, // mydata ,//
        success: function (data) {

            console.log("borrow");

            var htmls = "";
            var url = data.partial;
            $("#myModalBodyDiv4").load(url, function () {
                // $('#divlistNotebook').html(htmls);
                $("#myModal4").modal("show");

            })
        }
    });
}
function getval(action) {

    var table = document.getElementById("tbListNotebookSp");
    table.innerHTML = "";
    let vCDateStart = document.getElementById("txtPListDateStart").value;
    let mydata = $("#formRequest").serialize();
    console.log(vCDateStart);
    // var action = '/RequestForm/ListBorrowNotebook?dateS=' + sel.value;
    //action = action;// + '?dateS=' + vCDateStart;

    if (document.getElementById("txtPListDateStart").value == "") {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: "warning",
            text: "กรุณาเลือกเดือนที่ต้องการ !!",
        })
            .then((result) => {


            });
    } else {
        $.ajax({
            type: 'post',
            url: action,
            data: { dateS: vCDateStart },//mydata,//{ getID: getID }, // mydata ,//
            success: function (data) {

                //console.log("borrow");
                //var htmls = "";
                let v_row = 0;

                if (data.status == "listdata") {
                    $.each(data.listdata, function (i, item) {
                        var row = table.insertRow(v_row);
                        for (let i = 0; i <= data.countDay; i++) {
                            var cell1 = row.insertCell(i);
                            if (i == 0) {
                                cell1.innerHTML = item.v_bnPCName;
                            }
                            else if (i == 1) { cell1.innerHTML = i.toString(); if (item.v_1 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 2) { cell1.innerHTML = i.toString(); if (item.v_2 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 3) { cell1.innerHTML = i.toString(); if (item.v_3 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 4) { cell1.innerHTML = i.toString(); if (item.v_4 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 5) { cell1.innerHTML = i.toString(); if (item.v_5 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 6) { cell1.innerHTML = i.toString(); if (item.v_6 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 7) { cell1.innerHTML = i.toString(); if (item.v_7 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 8) { cell1.innerHTML = i.toString(); if (item.v_8 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 9) { cell1.innerHTML = i.toString(); if (item.v_9 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 10) { cell1.innerHTML = i.toString(); if (item.v_10 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 11) { cell1.innerHTML = i.toString(); if (item.v_11 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 12) { cell1.innerHTML = i.toString(); if (item.v_12 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 13) { cell1.innerHTML = i.toString(); if (item.v_13 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 14) { cell1.innerHTML = i.toString(); if (item.v_14 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 15) { cell1.innerHTML = i.toString(); if (item.v_15 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 16) { cell1.innerHTML = i.toString(); if (item.v_16 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 17) { cell1.innerHTML = i.toString(); if (item.v_17 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 18) { cell1.innerHTML = i.toString(); if (item.v_18 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 19) { cell1.innerHTML = i.toString(); if (item.v_19 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 20) { cell1.innerHTML = i.toString(); if (item.v_20 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 21) { cell1.innerHTML = i.toString(); if (item.v_21 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 22) { cell1.innerHTML = i.toString(); if (item.v_22 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 23) { cell1.innerHTML = i.toString(); if (item.v_23 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 24) { cell1.innerHTML = i.toString(); if (item.v_24 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 25) { cell1.innerHTML = i.toString(); if (item.v_25 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 26) { cell1.innerHTML = i.toString(); if (item.v_26 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 27) { cell1.innerHTML = i.toString(); if (item.v_27 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 28) { cell1.innerHTML = i.toString(); if (item.v_28 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 29) { cell1.innerHTML = i.toString(); if (item.v_29 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 30) { cell1.innerHTML = i.toString(); if (item.v_30 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                            else if (i == 31) { cell1.innerHTML = i.toString(); if (item.v_31 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }

                        }

                    });


                }

            }
        });
    }




}

function btnChooseDate() {
    var today1 = new Date();
    var dd1 = today1.getDate();
    var mm1 = today1.getMonth() + 1;
    var yyyy1 = today1.getFullYear();
    if (dd1 < 10) {
        dd1 = '0' + dd1;
    }
    if (mm1 < 10) {
        mm1 = '0' + mm1;
    }
    today1 = yyyy1 + '/' + mm1 + '/' + dd1;

    var vBorrowST = document.getElementById("idBorrowStart").value;
    var vBorrowEnd = document.getElementById("idBorrowEnd").value;


    if (vBorrowST == "" || vBorrowEnd == "") {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: "warning",
            text: "กรุณาเลือกวันที่ยืม หรือวันที่คืน ที่ต้องการ !!",
        })
            .then((result) => {

            });
    } else {
        if (document.getElementById("idBorrowEnd").value < document.getElementById("idBorrowStart").value) {
            swal.fire({
                title: 'แจ้งเตือน',
                icon: "warning",
                text: "วันที่คืน ต้องมากกว่าหรือเท่ากับ วันที่ยืม!!",
            })
                .then((result) => {
                    document.getElementById("idBorrowEnd").value = "";
                    document.getElementById("idBorrowStart").value = "";
                    document.getElementById("txtF3BorrowStratDate").value = "";
                    document.getElementById("txtF3BorrowEndDate").value = "";
                });
        }
        else {
            document.getElementById("txtF3BorrowStratDate").value = vBorrowST;
            document.getElementById("txtF3BorrowEndDate").value = vBorrowEnd;
            $("#myModal4").modal("hide");
        }

    }

    //txtF3BorrowStratDate
    //txtF3BorrowEndDate

}

function getval2(sel) {
    console.log(sel.value);
    var table = document.getElementById("tbListNotebookSp");

    // document.getElementById("tbListNotebookSp").remove(); 
    var Table = document.getElementById("tbListNotebookSp");
    Table.innerHTML = "";
    let vCDateStart = document.getElementById("txtPListDateStart").value;  //date start

    const myArray = vCDateStart.split("/");

    const getDaysInMonth = date => new Date(date.getFullYear(), date.getMonth(), 0).getDate();
    let cSDate = getDaysInMonth(new Date(myArray[0], myArray[1]));

    // var row = table.insertRow(0);
    let n_M1 = parseInt(myArray[2]);
    let n_M2 = parseInt(myArray[2]);
    let s_M1 = cSDate - myArray[2];


    var row = table.insertRow(0);
    for (let i = 0; i <= s_M1 + n_M2 + 1; i++) {
        var cell1 = row.insertCell(i);
        if (i == 0) {
            cell1.innerHTML = "Name PC";//i; //n_M1;
        } else {

            if (i == (2 + cSDate - n_M2)) {
                n_M1 = 1;
            } else {
                n_M1 = n_M1;
            }
            cell1.innerHTML = n_M1;//i; //n_M1;
            n_M1 = n_M1 + 1;
        }
    }
    let mydata = $("#formRequest").serialize();
    var action = '/RequestForm/ListBorrowNotebook?dateS=' + vCDateStart;
    $.ajax({
        type: 'post',
        url: action,
        data: mydata,//{ getID: getID }, // mydata ,//
        success: function (data) {

            console.log("borrow");
            var htmls = "";
            let v_row = 1;

            if (data.status == "listBorrow") {
                $.each(data.listBorrow, function (i, item) {
                    var row = table.insertRow(v_row);
                    //item.bnPCName ;
                    for (let i = 0; i <= s_M1 + n_M2 + 1; i++) {
                        var cell1 = row.insertCell(i);
                        if (i == 0) {
                            cell1.innerHTML = item.bnPCName + "start" + item.bnStratDate + " End" + item.bnEndDate;//i; //n_M1;
                        } else {
                            if (i == (2 + cSDate - n_M2)) {
                                n_M1 = 1;
                            } else {
                                n_M1 = n_M1;
                            }
                            //cell1.innerHTML = n_M1;//i; //n_M1;
                            cell1.innerHTML = ""; //"<span style='color:red'>0</span>";//i; //n_M1;
                            cell1.style.backgroundColor = "yellow";
                            n_M1 = n_M1 + 1;


                        }
                    }

                    v_row += 1;
                });


            }

        }
    });

}

function borrowChooseDate() {
    document.getElementById("txtF3BorrowStratDate").value = document.getElementById("txtPListDateStart").value;
    document.getElementById("txtF3BorrowEndDate").value = document.getElementById("txtPListDateEnd").value;

    $("#myModal4").modal("hide");
    //$("#myModal4").hide();
}

function addF6User(action) {
    let empcode = document.getElementById("txtF6empcode").value;
    let n_program = document.getElementById("ipF4pgm").value;
    // var action = '@Url.Action("SearchPersonal", "RequestForm")';
    var v_action = action + "?vEmpcode=" + empcode;
    var rowCount = document.getElementById('tbUsb').rows.length;
    var rowi = document.getElementById('tbUsb').rows.length - 2;
    var htmls = "";

    if (empcode == "") {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: "warning",
            text: "กรุณากรอกรหัสนักงานเพื่อเพิ่มข้อมูล !!",
        })
            .then((result) => {


            });
    }
    else if (n_program == "") {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: "warning",
            text: "กรุณาเลือกโปรแกรมที่ต้องการ !!",
        })
            .then((result) => {


            });
    }
    else {
        $.ajax({
            type: "POST",
            url: v_action,
            data: "",
            success: function (data) {
                console.log("scearh");
                if (data._AccEMPLOYEE.length > 0) {
                    $.each(data._AccEMPLOYEE, function (i, item) {


                        htmls = "";
                        htmls += "<tr>";
                        htmls += "<td>";
                        htmls += rowCount - 1;
                        htmls += "</td>";
                        htmls += "<td>" + empcode;
                        htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysEmpCode' value='" + empcode + "'style='width: 90%;display:none'  />";
                        htmls += "</td>"

                        // htmls += "<td>" + empcode + " <input type='text' class='cEmcode'  value='" + empcode + "' style='width: 80px'></td>";
                        htmls += "<td>" + item.emP_TNAME;
                        htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysName' value='" + item.emP_TNAME + "'style='width: 90%;display:none'  />";
                        htmls += "</td>"


                        htmls += "<td>" + item.lasT_TNAME;
                        htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysLastName' value='" + item.lasT_TNAME + "'style='width: 90%;display:none'  />";
                        //htmls += "<td>" + item.lasT_TNAME + "<input type='text' class='cLNAME' value='" + item.lasT_TNAME + "' style='width: 80px'></td>";
                        htmls += "</td>"


                        htmls += "<td>" + item.depT_CODE + "/" + item.seC_CODE;
                        htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysDeptCode' value='" + item.depT_CODE + "'style='width: 90%;display:none'  />"
                        htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysSectCode' value='" + item.seC_CODE + "'style='width: 90%;display:none'  />"
                        htmls += "</td>"

                        //htmls += "<td>" + item.depT_CODE + "/" + item.seC_CODE + "<input type='text' class='cDep' value='" + item.depT_CODE + "'  style='width: 50px'> <input type = 'text' class='cSec' value='" + item.seC_CODE + "'  style='width: 50px'></td>";
                        htmls += "<td>" + item.intercomno;
                        htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysIntercomNo' value='" + item.intercomno + "'style='width: 90%;display:none'  />"
                        htmls += "</td>"

                        htmls += "<td>" + n_program;
                        htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysProgramName' value='" + n_program + "'style='width: 90%;display:none'  />"
                        htmls += "</td>"



                        //htmls += "<td>" + item.intercomno + "<input type='text'  value='" + item.intercomno + "'  style='width: 50px'></td>";
                        htmls += "<td align='Center'> <input type='radio'   name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysObject' value ='New'></td>";
                        htmls += "<td align='Center'> <input type='radio'   name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysObject' value ='Change'></td>";
                        htmls += "<td align='Center'> <input type='radio'   name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysObject' value ='Cancel'></td>";


                        htmls += "<td align='Center'><input type='checkbox'  name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionEditor' id='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionEditor'  value='true'></td>";
                        htmls += "<td align='Center'><input type='checkbox'  name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionRead'   id='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionRead'  value='true'></td>";
                        htmls += "<td align='Center'><input type='checkbox'  name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionDelete' id='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionDelete' value='true'>";
                        //htmls += "Html.CheckBoxFor(model => model._ViewsvsSDE_SystemRegister[i].sysPermissionDelete, new { @id = 'txtF6PermissionDelete' })";
                        htmls += "</td> ";

                        htmls += "<td align='Center'><input type='button' class='RemoveRow' value='&#x274C'></td>";
                        htmls += "</tr>";
                        $("#tbUsb").append(htmls);
                        document.getElementById("txtF6empcode").value = "";
                        //$("#tbUsb").append('<tr><td>1</td><td>' + empcode + '</td><td>' + item.emP_TNAME + '</td><td>' + item.lasT_TNAME + '</td><td>' + item.depT_CODE + '</td><td>' + item.intercomno + '</td><td></td><td></td><td></td><td></td><td></td><td></td><td> <input type="button" class="RemoveRow" value="&#x274C"></td></tr>');
                    });


                } else {
                    swal.fire({
                        title: 'แจ้งเตือน',
                        icon: "warning",
                        text: "กรุณากรอกรหัสนักงานให้ถูกต้อง!!",
                    })
                        .then((result) => {
                            document.getElementById("txtF6empcode").value = "";

                        });
                }


            }
        });

    }

}
$("#btnLoadData").click(async function () {
    let homeAction = "Home\\DisplayRequest";
    let objFilter = {
        ServiceNo: $('#id_No').val(),
        NameReq: $('#id_Name').val(),
        Dept: $('#ipCodeDept').val(),
        ServiceType: $('#id_Type').val(),
        DateRequestFrom: $('#id_DataRequestFrom').val(),
        DateRequestTo: $('#id_DataRequestTo').val(),
        TargetDateFrom: $('#id_TargetFrom').val(),
        TargetDateTo: $('#id_TargetTo').val(),
        Operator: $('#id_Operator').val(),
        ApproveBy: $('#id_ApproveBy').val(),
        StatusService: $('#ipStatus').val(),
    };

    console.log(objFilter);

    $.ajax({
        url: homeAction,
        type: 'POST',
        data: { objFilter },
    }).then(function (partial) {
        let parser = new DOMParser();
        let doc = parser.parseFromString(partial, "text/html");
        let tb = document.getElementById("divtable");
        let n_tb = doc.getElementById("divResult").outerHTML;

        //datatable after search cdn js
        ////$(document).ready(function () {
        ////    tableIDDatatable();
        ////});

        tb.innerHTML = n_tb;

    }).catch((error) => console.log(error));

});



//capture
function capture(vSRNo) {

    //var vSRNo = vSRNo + '.png';
    //const form = document.getElementById('formRequest');
    //// ✅ แก้ให้ textarea แสดงข้อความในภาพ
    //const textareas = form.querySelectorAll('textarea');
    //textareas.forEach(textarea => {
    //    const text = textarea.value;
    //    const span = document.createElement('div');
    //    span.textContent = text;
    //    span.style.cssText = getComputedStyle(textarea).cssText;
    //    span.style.whiteSpace = 'pre-wrap'; // รักษาบรรทัด
    //    span.style.border = '1px solid #ccc';
    //    span.style.padding = '5px';
    //    span.style.minHeight = textarea.offsetHeight + 'px';
    //    span.style.width = textarea.offsetWidth + 'px';

    //    // ซ่อนตัวจริง แล้วใส่แทนที่ชั่วคราว
    //    textarea.style.display = 'none';
    //    textarea.parentNode.insertBefore(span, textarea);
    //    textarea.dataset._tempSpan = 'true'; // tag ไว้ลบตอนจบ
    //});

    //// 📸 Capture
    //html2canvas(form).then(function (canvas) {
    //    const link = document.createElement('a');
    //    link.download = vSRNo;
    //    link.href = canvas.toDataURL();
    //    link.click();

    //    // 🔁 cleanup: เอา span ออก แล้วโชว์ textarea กลับ
    //    textareas.forEach(textarea => {
    //        const span = textarea.previousSibling;
    //        if (textarea.dataset._tempSpan === 'true' && span) {
    //            span.remove();
    //            textarea.style.display = '';
    //        }


    //    });
    //});


    var vSRNo = vSRNo + '.png';
    const form = document.getElementById('formRequest');

    // 🔧 เก็บ div ที่สร้างแทน textarea ไว้ใน array
    const replacements = [];

    const textareas = form.querySelectorAll('textarea');
    textareas.forEach(textarea => {
        const text = textarea.value;
        const span = document.createElement('div');

        // ตั้งค่า style ให้เหมือน textarea
        const style = getComputedStyle(textarea);
        span.textContent = text;
        span.style.fontSize = style.fontSize;
        span.style.fontFamily = style.fontFamily;
        span.style.color = style.color;
        span.style.backgroundColor = style.backgroundColor;
        span.style.border = '1px solid #ccc';
        span.style.padding = '5px';
        span.style.minHeight = textarea.offsetHeight + 'px';
        span.style.width = textarea.offsetWidth + 'px';
        span.style.whiteSpace = 'pre-wrap';

        // ซ่อน textarea แล้วแทรก span
        textarea.style.display = 'none';
        textarea.parentNode.insertBefore(span, textarea);

        // 👉 เก็บไว้ลบทีหลัง
        replacements.push({ span, textarea });
    });

    // 📸 Capture
    html2canvas(form).then(function (canvas) {
        const link = document.createElement('a');
        link.download = vSRNo;
        link.href = canvas.toDataURL();
        link.click();

        // ✅ cleanup และตรวจสอบจริง
        replacements.forEach(({ span, textarea }, i) => {
            if (span && span.parentNode) {
                span.parentNode.removeChild(span);
                console.log(`✅ Removed span ${i}`);
            } else {
                console.warn(`❌ Span ${i} not found in DOM`);
            }
            textarea.style.display = '';
        });
    });

}


