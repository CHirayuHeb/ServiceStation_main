

$('.AddNew').click(function () {
    var rowCount = document.getElementById('tbNew').rows.length;
    var htmls = "";
    var rowi = rowCount - 2;
    htmls = "";
    htmls += "<tr align='Center' style='vertical-align: top;'>";
    htmls += "<td>";
    htmls += rowCount - 1;
    htmls += "</td>";
    htmls += "<td>";
    htmls += " <select autocomplete='off' required='required' style='width:100px;height: 24px;' class='cType' name='_ViewsvsRegisterUSB_New[" + rowi + "].nuType' >";
    htmls += "     <option value=''>กรุณาเลือก</option>";
    htmls += "     <option>Camera</option>          ";
    htmls += "     <option>Card Reader</option>     ";
    htmls += "     <option>External CD/DVD</option> ";
    htmls += "     <option>External Hard disk</option>";
    htmls += "     <option>SD Card</option>           ";
    htmls += "     <option>USB(Flash Drive)</option>  ";
    htmls += " </select> ";

    htmls += "</td>";
    htmls += "<td>";
    htmls += "<input type='text' name='_ViewsvsRegisterUSB_New[" + rowi + "].nuEquipment' value='' class='form - control' />";
    //htmls += "<input type='text' class='cEquipment'  value='' style='width: 80px'>";
    htmls += "</td>";
    htmls += "<td>";
    htmls += "<textarea id='w3review' name='_ViewsvsRegisterUSB_New[" + rowi + "].nuObjective'  class='cObject' value=''rows='3' cols='30' maxlength='100'></textarea>";
    htmls += "</td>";
    htmls += "<td>";
    htmls += "<input type='text' class='cUser' name='_ViewsvsRegisterUSB_New[" + rowi + "].nuUserIncharge' value='' style='width: 80px'>";
    htmls += "</td>";
    htmls += "<td>";
    htmls += "<input type='text' class='cIntercom' name='_ViewsvsRegisterUSB_New[" + rowi + "].nuIntercomNo'  value='' style='width: 50px'>";
    htmls += "</td>";
    htmls += "<td align='left'>";
    htmls += " <input name='files" + rowi + "' type='file' id='files" + rowi + " multiple='' class='button ' style='width:130px;'accept='image/*' >";
    htmls += "</td>";
    htmls += "<td>";
    htmls += "<input type='button' class='RemoveRow' value='&#x274C'>";
    htmls += "</td>";
    htmls += "<td>";
    htmls += "<input type='text' class='cHardwareID' name='_ViewsvsRegisterUSB_New[" + rowi + "].nuHardwareID' value='' style='width: 80px' readonly>";
    htmls += "</td>";
    htmls += "<td>";
    htmls += "<input type='text' class='cITCode' name='_ViewsvsRegisterUSB_New[" + rowi + "].nuITCode' value='' style='width: 80px' readonly>";
    htmls += "</td>";
    //htmls += "<td>";
    //htmls += "";
    //htmls += "</td>";
    htmls += "</tr>";
    $("#tbNew").append(htmls);
    //$("#tbNew").append('<tr><td>1</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td> <input type="button" class="RemoveRow" value="&#x274C"></td></tr>');
});

$('.Addcancel').click(function () {
    var rowCount = document.getElementById('tbCancel').rows.length;
    var htmls = "";
    var rowi = rowCount - 1;
    htmls = "";
    htmls += "<tr align='Center' style='vertical-align: top;' class='tbpurple'> ";
    htmls += "<td>";
    htmls += rowCount;
    htmls += "</td>";
    htmls += "<td  width='200px'>";
    htmls += " <select autocomplete='off' required='required' style='width:90%;height: 24px;' class='cuType' name='_ViewsvsRegisterUSB_Cancel[" + rowi + "].cuType' >";
    htmls += "     <option value=''>กรุณาเลือก</option>";
    htmls += "     <option>Camera</option>          ";
    htmls += "     <option>Card Reader</option>     ";
    htmls += "     <option>External CD/DVD</option> ";
    htmls += "     <option>External Hard disk</option>";
    htmls += "     <option>SD Card</option>           ";
    htmls += "     <option>USB(Flash Drive)</option>  ";
    htmls += " </select> ";
    htmls += "</td>";
    htmls += "<td width='200px'>";
    htmls += "<input type='text' name='_ViewsvsRegisterUSB_Cancel[" + rowi + "].cuUSBNo' value='' class='cuUSBNo' style='width: 90%'  />";
    htmls += "</td>";
    htmls += "<td align='left' >";
    htmls += "<input type='radio'  name='_ViewsvsRegisterUSB_Cancel[" + rowi + "].cuReason' value='Lost' /> สูญหาย <br/> ";
    htmls += "<input type='radio'  name='_ViewsvsRegisterUSB_Cancel[" + rowi + "].cuReason' value='broken' /> พัง,เสีย <br/>";
    htmls += " <input type='radio'  name='_ViewsvsRegisterUSB_Cancel[" + rowi + "].cuReason' value='Other' /> อื่นๆ ";
    htmls += "<input type='text' name='_ViewsvsRegisterUSB_Cancel[" + rowi + "].cuReason_other' value=''  class='cuReason_other'  style='width: 80%' maxlength='100'/>";
    htmls += "</td>";
    htmls += "<td>";
    htmls += "<input type='button' class='RemoveRow' value='&#x274C'>";
    htmls += "</td>";
    htmls += "</tr>";
    $("#tbCancel").append(htmls);

    //$("#tbCancel").append('<tr><td>1</td><td></td><td></td><td> <input type="radio"  value="loss" /> สูญหาย &emsp;&emsp;<input type = "radio" value = "broken" /> พัง, เสีย &emsp;&emsp;<input type="radio" value="broken" />อื่น ๆ &emsp;<input type="text" id="namedata2" name="input" /></td><td> <input type="button" class="RemoveRow" value="&#x274C"></td></tr>');
});
$('.RemovecancelRow').click(function () {
    $(this).closest('tr').remove();
});

$('.AddNew').click(function () {
    var row = $(this).closest('tr').clone();
    row.find('input').val('');
    $(this).closest('tr').after(row);
    $('input[type="button"]', row).removeClass('AddNew')
        .addClass('RemoveRow').val('Remove item');
});

$('table').on('click', '.RemoveRow', function () {
    $(this).closest('tr').remove();
});


//page register
$('.RemoveRegis').click(function () {
    var rowCount = document.getElementById('tbUsb').rows.length;
    document.getElementById("tbUsb").deleteRow(rowCount - 1);
});


//F6
function btnaddF6(action) {
    let empcode = document.getElementById("txtF6empcode").value;
    let n_program = document.getElementById("ipF4pgm").value;
    // var action = '@Url.Action("SearchPersonal", "RequestForm")';
    var v_action = action + "?vEmpcode=" + empcode;
    var rowCount = document.getElementById('tbUsb').rows.length;
    var rowi = document.getElementById('tbUsb').rows.length - 2;
    var rdRow = Math.floor(Math.random() * 10);
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
        //type: "POST",
        //    url: '@Url.Action("SearchPersonal", "RequestForm")',
        //        data: "{vEmpcode:'" + v_emp + "'}",
        //            contentType: "application/json; charset=utf-8",
        //                dataType: "html",

        //action = action 
        $.ajax({
            type: "POST",
            url: action,
            data: { vEmpcode: empcode },
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
                        //htmls += "<td>" + item.emP_TNAME;
                        //htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysName' value='" + item.emP_TNAME + "'style='width: 90%;display:none'  />";
                        //htmls += "</td>"

                        htmls += "<td>" + item.emP_ENAME;
                        htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysName' value='" + item.emP_ENAME + "'style='width: 90%;display:none'  />";
                        htmls += "</td>"



                        htmls += "<td>" + item.lasT_ENAME;
                        htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysLastName' value='" + item.lasT_ENAME + "'style='width: 90%;display:none'  />";
                        //htmls += "<td>" + item.lasT_TNAME + "<input type='text' class='cLNAME' value='" + item.lasT_TNAME + "' style='width: 80px'></td>";
                        htmls += "</td>"


                        //htmls += "<td>" + item.lasT_TNAME;
                        //htmls += "<input type='text' name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysLastName' value='" + item.lasT_TNAME + "'style='width: 90%;display:none'  />";
                        ////htmls += "<td>" + item.lasT_TNAME + "<input type='text' class='cLNAME' value='" + item.lasT_TNAME + "' style='width: 80px'></td>";
                        //htmls += "</td>"


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


                        htmls += "<td align='center'> <input type='radio'  name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionEditor' value ='Editor'><label style ='font-size:8px;'> Yes</label><br /> <input type='radio'  name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionEditor' value ='No'><label style ='font-size:8px;'> No</label><br /></td>";
                        htmls += "<td align='center'> <input type='radio'  name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionRead'   value ='Read'><label style ='font-size:8px;'> Yes</label><br /><input type='radio'  name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionRead'   value ='No'><label style ='font-size:8px;'> No</label><br /></td>";
                        htmls += "<td align='center'> <input type='radio'  name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionDelete' value ='Delete'><label style ='font-size:8px;'> Yes</label><br /><input type='radio'  name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysPermissionDelete' value ='No'><label style ='font-size:8px;'> No</label><br /></td>";
                        
                        htmls += "<td align='center'> <textarea name='_ViewsvsSDE_SystemRegister[" + rowi + "].sysRemark' style = 'font-size: 12px; max-width: 100 % ; height: 80px;'></textarea> </td>";
                        //htmls += "<td align='Center'></td>";

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

//$('.datepicker').datepicker({
//    autoclose: true,
//    todayHighlight: true,
//    format: 'yyyy/mm/dd'

//});

//get date now start
function pad(d) {
    return (d < 10) ? '0' + d.toString() : d.toString();
}
var todaydate = new Date();
var day = pad(todaydate.getDate());
var month = pad(todaydate.getMonth() + 1);
var year = todaydate.getFullYear();
var datestring = year + "/" + month + "/" + day;

if (document.getElementById("i_SDate").value == "") {
    document.getElementById("i_SDate").value = datestring;
}





//document.getElementById("F4ubStatusReqNew").onclick();
//document.getElementById("F4ubStatusReqCancel").onclick();

//check F4 
//var v_Obstatus = @ViewBag.Obstatus;
//if (v_Obstatus == "Cancel") {
//   // document.getElementById("F4ubStatusReqCancel").onclick();
//    CheckF4NewCancel('Cancel');
//}
//else {
//   // document.getElementById("F4ubStatusReqNew").onclick();
//    CheckF4NewCancel('New');
//}




$('#i_EDate').change(function () {

    let v_SDate = document.getElementById("i_SDate").value;
    let v_EDate = document.getElementById("i_EDate").value;
    const myArray = v_EDate.split("/");
    if (myArray[2].length > 3) {
        v_EDate = myArray[2].toString() + "/" + myArray[0].toString() + "/" + myArray[1].toString();
    }
    console.log("tomorrow" + v_EDate);
    if (v_EDate < v_SDate) {
        console.log("tomorrow" + v_SDate);
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่ต้องการ มากกว่า วันที่ขอ",

        })
            .then((result) => {
                document.getElementById("i_EDate").value = "";

            });
    }
    else {
        document.getElementById("i_EDate").value = v_EDate;
    }
});



function CheckSV(status, type) {
    $(document).ready(function () {
        let checkStatus = status;
        console.log("checkStatusDis" + checkStatus);
        if (checkStatus == 'Other') {
            console.log("1 " + type);
            $('#txtSOther').removeAttr('disabled', 'disabled');
            document.getElementById("txtSOther").value = "";
        }
        else {
            console.log("else " + type);
            console.log("2 " + type);
            $('#txtSOther').attr("disabled", "disabled");
            document.getElementById("txtSOther").value = "";

        }

    });
}
function CheckCA(status, type) {
    $(document).ready(function () {
        let checkStatus = status;
        if (checkStatus == 'Other') {
            console.log("3  " + type);
            $('#txtCOther').removeAttr('disabled', 'disabled');
            document.getElementById("txtCOther").value = "";
        }
        else {
            console.log("4 " + type);
            $('#txtCOther').attr("disabled", "disabled");
            document.getElementById("txtCOther").value = "";
        }

    });
}

//for F3 BorrowSpare
function CheckF3Obj(status, type) {
    $(document).ready(function () {
        let checkStatus = status;
        if (checkStatus == 'Other') {
            console.log("3  " + type);
            $('#txtF3InputOther').removeAttr('disabled', 'disabled');
            document.getElementById("txtCOther").value = "";
        }
        else {
            console.log("4 " + type);
            $('#txtF3InputOther').attr("disabled", "disabled");
            document.getElementById("txtF3InputOther").value = "";
        }

    });
}




$("#btnfile").click(function () {
    $("#myModal2").modal("show");
});

$("#btnfilew").click(function () {
    $("#myModal3").modal("show");
});

function CheckDis(status) {
    $(document).ready(function () {
        var checkStatusDis = status;
        var step = $('#step').val();
        console.log("step ==> " + step);
        if (checkStatusDis == 'Disapprove' || checkStatusDis == 'Cancel') {
            $('#searchInputTO').attr("disabled", "disabled");
            //document.getElementById("searchInputTO").value = "";
            $('#EmailTo').removeAttr("name");
        }
        else {
            $('#searchInputTO').removeAttr('disabled', 'disabled');
            $('#EmailTo').removeAttr("name");
            if (step == 4) {
                console.log(step);
                document.getElementById("searchInputTO").value = $('#EmailTo').val();
            }

        }

    });
}
$('#txtF3ReturnStartDate').change(function () {
    let v_EDate = document.getElementById("txtF3ReturnStartDate").value;
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
    if (v_EDate < today1) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่ยืม มากกว่าหรือเท่ากับวันที่ปัจจุบัน",

        })
            .then((result) => {
                document.getElementById("txtF3ReturnStartDate").value = "";

            });
    }
});

$('#txtF3BorrowEndDate').change(function () {
    let S_SDate = document.getElementById("txtF3BorrowStratDate").value;
    let S_EDate = document.getElementById("txtF3BorrowEndDate").value;

    if (S_EDate < S_SDate) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่คืน ต้องมากกว่าหรือเท่ากับวันที่ยืม",

        })
            .then((result) => {
                document.getElementById("txtF3BorrowEndDate").value = "";

            });
    }
});

//borrow notebook 
$('#idBorrowStart').change(function () {
    let v_EDate = document.getElementById("idBorrowStart").value;
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
    if (v_EDate < today1) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่ยืม มากกว่าหรือเท่ากับวันที่ปัจจุบัน",

        })
            .then((result) => {
                document.getElementById("idBorrowStart").value = "";

            });
    }
});
$('#idBorrowEnd').change(function () {
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
    let S_SDate = document.getElementById("idBorrowStart").value;
    let S_EDate = document.getElementById("idBorrowEnd").value;

    if (S_EDate < S_SDate) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่คืน ต้องมากกว่าหรือเท่ากับวันที่ยืม",

        })
            .then((result) => {
                document.getElementById("idBorrowEnd").value = "";

            });
    } else if (S_EDate < today1) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่ยืม มากกว่าหรือเท่ากับวันที่ปัจจุบัน",

        })
            .then((result) => {
                document.getElementById("idBorrowEnd").value = "";

            });
    }
});



$('#txtF3BorrowStratDate').change(function () {
    let v_EDate = document.getElementById("txtF3BorrowStratDate").value;

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
    console.log(today1);

    if (v_EDate < today1) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่ยืม มากกว่าหรือเท่ากับวันที่ปัจจุบัน",

        })
            .then((result) => {
                document.getElementById("txtF3BorrowStratDate").value = "";
                document.getElementById("txtF3BorrowEndDate").value = "";
            });
    }
    else {
        //var v_action = @Url.Action("ListBorrowNotebook", "RequestForm");
        let mydata = $("#formRequest").serialize();
        $.ajax({
            type: 'post',
            url: '@Url.Action("ListBorrowNotebook", "RequestForm")',
            data: mydata,//{ getID: getID }, // mydata ,//
            success: function (data) {

                console.log("borrow");

                var htmls = "";
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
                var url = data.partial + mydata;
                $("#myModalBodyDiv4").load(url, function () {
                    $('#divlistNotebook').html(htmls);
                    $("#myModal4").modal("show");

                })
            }
        });






    }
});

$('#txtF5vpnStartDate').change(function () {
    let v_SDate = document.getElementById("txtF5vpnStartDate").value;
    let v_EDate = document.getElementById("txtF5vpnEndDate").value;

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
    console.log(today1);

    if (v_SDate < today1) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่เริ่มใช้ มากกว่าหรือเท่ากับวันที่ปัจจุบัน!!!",

        })
            .then((result) => {
                document.getElementById("txtF5vpnStartDate").value = "";
                document.getElementById("txtF5vpnEndDate").value = "";
            });
    }
    if (v_EDate != "") {
        if (v_SDate > v_EDate) {
            swal.fire({
                title: 'แจ้งเตือน',
                icon: 'warning',
                text: "กรุณาเลือกวันที่เริ่มใช้ มากกว่าหรือเท่ากับวันที่สิ้นสุด !!!",

            })
                .then((result) => {
                    document.getElementById("txtF5vpnStartDate").value = "";
                    document.getElementById("txtF5vpnEndDate").value = "";
                });
        }
    }
    
    //else {
    //    let start = new Date(v_EDate);
    //    let end = new Date(start);
    //    end.setMonth(end.getMonth() + 1);
    //    //console.log(end.toDateString("YYYY/MM/dd"));

    //    var today = new Date(end);
    //    var dd = today.getDate();
    //    var mm = today.getMonth() + 1;
    //    var yyyy = today.getFullYear();
    //    if (dd < 10) {
    //        dd = '0' + dd;
    //    }
    //    if (mm < 10) {
    //        mm = '0' + mm;
    //    }
    //    today = yyyy + '/' + mm + '/' + dd;
    //    console.log(today);
    //    document.getElementById("txtF5vpnStartDate").value = today;
    //}
});
$('#txtF5vpnEndDate').change(function () {
    let S_SDate = document.getElementById("txtF5vpnStartDate").value;
    let S_EDate = document.getElementById("txtF5vpnEndDate").value;

    let start = new Date(S_SDate);
    let end = new Date(start);
    end.setMonth(end.getMonth() + 1);
    //console.log(end.toDateString("YYYY/MM/dd"));

    var MaxCDare = new Date(end);
    var dd = MaxCDare.getDate();
    var mm = MaxCDare.getMonth() + 1;
    var yyyy = MaxCDare.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    MaxCDare = yyyy + '/' + mm + '/' + dd;
    console.log("MaxCDare" + MaxCDare);
   



    if (S_EDate < S_SDate) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือก วันที่สิ้นสุดต้อง มากกว่าหรือเท่ากับวันที่เริ่มใช้งาน!!!",

        })
            .then((result) => {
                document.getElementById("txtF5vpnEndDate").value = "";

            });
    }
    else if (S_EDate > MaxCDare) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือก วันที่สิ้นสุดต้องไม่เกิน 1 เดือนนับจากวันเริ่มต้น !!!",

        })
            .then((result) => {
                document.getElementById("txtF5vpnEndDate").value = "";

            });
    }
});

//Worker process
$('#txtwExpFinish').change(function () {
    let v_EDate = document.getElementById("txtwExpFinish").value;

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
    console.log(today1);

    if (v_EDate < today1) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่วันคาดว่างานเสร็จสิ้น มากกว่าหรือเท่ากับวันที่ปัจจุบัน",

        })
            .then((result) => {
                document.getElementById("txtwExpFinish").value = "";

            });
    }
    else {
        let start = new Date(v_EDate);
        let end = new Date(start);
        end.setMonth(end.getMonth() + 1);
        //console.log(end.toDateString("YYYY/MM/dd"));

        var today = new Date(end);
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        today = yyyy + '/' + mm + '/' + dd;
        console.log(today);
        //document.getElementById("txtF5vpnStartDate").value = today;
        document.getElementById("txtwExpFinish").value = today;
        
    }
});
$('#txtwFinishDate').change(function () {
    let v_EDate = document.getElementById("txtwFinishDate").value;

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
    console.log(today1);

    if (v_EDate < today1) {
        swal.fire({
            title: 'แจ้งเตือน',
            icon: 'warning',
            text: "กรุณาเลือกวันที่งานเสร็จสิ้น มากกว่าหรือเท่ากับวันที่ปัจจุบัน",

        })
            .then((result) => {
                document.getElementById("txtwFinishDate").value = "";

            });
    }
    else {
        let start = new Date(v_EDate);
        let end = new Date(start);
        end.setMonth(end.getMonth() + 1);
        //console.log(end.toDateString("YYYY/MM/dd"));

        var today = new Date(end);
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        today = yyyy + '/' + mm + '/' + dd;
        console.log(today);
        //document.getElementById("txtF5vpnStartDate").value = today;
    }
});

$("#btnSearch").click(function () {
    console.log("mmmmm");
    let v_emp = document.getElementById("txtF7ITMSitEmpcode").value;
    //'@Url.Action("SendMail_post", "RequestForm", new { vform = @ViewBag.vForm,vSR = @ViewBag.SRno})'
    ;
    var action = '@Url.Action("SearchPersonal", "RequestForm")';
    // var action = '/RequestForm/SearchPersonal?vEmpcode=' + v_emp;
    $.ajax({

        type: "POST",
        url: '@Url.Action("SearchPersonal", "RequestForm")',
        data: "{vEmpcode:'" + v_emp + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "html",




        //type: 'post',
        //url: '@Url.Action("SearchPersonal", "RequestForm")',
        //data: { vEmpcode: v_emp },// "",//{ getID: getID }, // mydata ,//
        success: function (data) {

            console.log("borrow");


            $.each(data._AccEMPLOYEE, function (i, item) {

                // document.getElementById("txtF7ITMSFname").value = item.emP_TNAME;
                //document.getElementById("txtF7ITMSLName").value = item.lasT_TNAME;
                //document.getElementById("txtF7ITMSDept").value = item.depT_CODE;

                document.getElementById("txtF7ITMSFname").innerHTML = item.emP_TNAME;
                document.getElementById("txtF7ITMSLName").innerHTML = item.lasT_TNAME;
                document.getElementById("txtF7ITMSDept").innerHTML = item.depT_CODE;
                document.getElementById("txtF7ITMSIntercom").innerHTML = item.intercomno;


            });


        }
    });
});
function btnSearchData(action) {
    console.log("btnSearchData");
    let v_emp = document.getElementById("txtF7ITMSitEmpcode").value;
    $.ajax({
        type: 'post',
        url: action,
        data: { vEmpcode: v_emp },// "",//{ getID: getID }, // mydata ,//
        success: function (data) {

            console.log("borrow");

            if (data._AccEMPLOYEE.length > 0) {
                $.each(data._AccEMPLOYEE, function (i, item) {

                    // document.getElementById("txtF7ITMSFname").value = item.emP_TNAME;
                    //document.getElementById("txtF7ITMSLName").value = item.lasT_TNAME;
                    //document.getElementById("txtF7ITMSDept").value = item.depT_CODE;

                    document.getElementById("txtF7ITMSFname").innerHTML = item.emP_TNAME;
                    document.getElementById("txtF7ITMSLName").innerHTML = item.lasT_TNAME;
                    document.getElementById("txtF7ITMSDept").innerHTML = item.depT_CODE;
                    document.getElementById("txtF7ITMSIntercom").innerHTML = item.intercomno;


                });
            } else {
                swal.fire({
                    title: 'แจ้งเตือน',
                    icon: 'warning',
                    text: "รหัสพนักงานไม่ถูกต้อง หรือ ไม่มีชื่อในระบบ !!!!!",

                })
                    .then((result) => {
                        document.getElementById("txtF7ITMSitEmpcode").value = "";
                        document.getElementById("txtF7ITMSFname").innerHTML = "";
                        document.getElementById("txtF7ITMSLName").innerHTML = "";
                        document.getElementById("txtF7ITMSDept").innerHTML = "";
                        document.getElementById("txtF7ITMSIntercom").innerHTML = "";
                    });
            }




        }
    });
}

//F7
function CheckF7(status) {
    $(document).ready(function () {

        if (status == "Internet") {
            if (document.getElementById("txtF7itMInternet").checked == false) {
                $('#txtF7itObjectiveTemporaryUser').attr("disabled", "disabled");
                $('#txtF7itObjectiveGeneralInformation').attr("disabled", "disabled");
                $('#txtF7itObjectiveResearchfortheJob').attr("disabled", "disabled");
                $('#txtF7itObjectiveDirectConcernOnThejob').attr("disabled", "disabled");
                $('#txtF7itObjectiveOutsideCommunicationByE').attr("disabled", "disabled");
                $('#txtF7itObjectiveGeneralInformationT9').attr("disabled", "disabled");

                $('#txtF7itObjectiveTemporaryUser').prop('checked', false);
                $('#txtF7itObjectiveGeneralInformation').prop('checked', false);
                $('#txtF7itObjectiveResearchfortheJob').prop('checked', false);
                $('#txtF7itObjectiveDirectConcernOnThejob').prop('checked', false);
                $('#txtF7itObjectiveOutsideCommunicationByE').prop('checked', false);
                $('#txtF7itObjectiveGeneralInformationT9').prop('checked', false);


            } else {
                $('#txtF7itObjectiveTemporaryUser').removeAttr('disabled', 'disabled');
                $('#txtF7itObjectiveGeneralInformation').removeAttr('disabled', 'disabled');
                $('#txtF7itObjectiveResearchfortheJob').removeAttr('disabled', 'disabled');
                $('#txtF7itObjectiveDirectConcernOnThejob').removeAttr('disabled', 'disabled');
                $('#txtF7itObjectiveOutsideCommunicationByE').removeAttr('disabled', 'disabled');
                $('#txtF7itObjectiveGeneralInformationT9').removeAttr('disabled', 'disabled');

                $('#txtF7itObjectiveTemporaryUser').prop('checked', false);
                $('#txtF7itObjectiveGeneralInformation').prop('checked', false);
                $('#txtF7itObjectiveResearchfortheJob').prop('checked', false);
                $('#txtF7itObjectiveDirectConcernOnThejob').prop('checked', false);
                $('#txtF7itObjectiveOutsideCommunicationByE').prop('checked', false);
                $('#txtF7itObjectiveGeneralInformationT9').prop('checked', false);
            }
        }
        else if (status == "Mail") {
            if (document.getElementById("txtF7itMmail").checked == false) {
                $('#txtF7itMail_TypeLOTUSNOTES').attr("disabled", "disabled");
                $('#txtF7itMail_TypeOUTLOOK').attr("disabled", "disabled");

                $('#txtF7itMail_TypeLOTUSNOTES').prop('checked', false);
                $('#txtF7itMail_TypeOUTLOOK').prop('checked', false);


            } else {
                $('#txtF7itMail_TypeLOTUSNOTES').removeAttr('disabled', 'disabled');
                $('#txtF7itMail_TypeOUTLOOK').removeAttr('disabled', 'disabled');

                $('#txtF7itMail_TypeLOTUSNOTES').prop('checked', false);
                $('#txtF7itMail_TypeOUTLOOK').prop('checked', false);
            }
        }
        else if (status == "Pclan") {
            if (document.getElementById("txtF7itMPcLan").checked == false) {
                $('#txtF7itPcLan_TypeThai').attr("disabled", "disabled");
                $('#txtF7itPcLan_TypeJapan').attr("disabled", "disabled");

                $('#txtF7itPcLan_TypeThai').prop('checked', false);
                $('#txtF7itPcLan_TypeJapan').prop('checked', false);

            } else {
                $('#txtF7itPcLan_TypeThai').removeAttr('disabled', 'disabled');
                $('#txtF7itPcLan_TypeJapan').removeAttr('disabled', 'disabled');

                $('#txtF7itPcLan_TypeThai').prop('checked', false);
                $('#txtF7itPcLan_TypeJapan').prop('checked', false);

            }
        }

    });
}

//F4
function CheckF4NewCancel(status) {
    $(document).ready(function () {
        var d_new = document.getElementById("f4New");
        var d_cancel = document.getElementById("f4Cancel");

        if (status == 'New') {
            if (d_new.style.display === "none") {
                d_new.style.display = "block";
                d_cancel.style.display = "none";
            } else {
                d_cancel.style.display = "none";
            }
        } else {
            if (d_cancel.style.display === "none") {
                d_cancel.style.display = "block";
                d_new.style.display = "none";
            } else {
                d_new.style.display = "none";
            }
        }


    });
}
//F1 Revise & New Program
function CheckF1NewRevise(status) {
    $(document).ready(function () {
        var d_new = document.getElementById("f4New");
        var d_cancel = document.getElementById("ipF1pgm");
        var d_pgm = document.getElementById("ipF1pgm");

        if (status == 'New') {
            d_pgm.style.display = "none";
          
            //if (d_new.style.display === "none") {
            //    d_new.style.display = "block";
            //    d_cancel.style.display = "none";
            //} else {
            //    d_cancel.style.display = "none";
            //}
        } else {
            d_pgm.style.display = "block";
            //if (d_cancel.style.display === "none") {
            //    d_cancel.style.display = "block";
            //    d_new.style.display = "none";
            //} else {
            //    d_new.style.display = "none";
            //}
        }


    });
}




//$('#txtPListDateStart').change(function () {

//    let vCDateStart = document.getElementById("txtPListDateStart").value;  //date start
//    var table = document.getElementById("tbListNotebookSp");

//    let mydata = $("#formRequest").serialize();
//    //// var action = '@Url.Action("SearchPersonal", "RequestForm")';
//    //var v_action = action + "?vEmpcode=" + empcode;
//    var action = '/RequestForm/ListBorrowNotebook?dateS=' + vCDateStart;// '@Url.Action("SearchPersonal", "RequestForm")'
//   // action = '@Url.Action("ListBorrowNotebook", "RequestForm")' + '?dateS=' + vCDateStart;
//    //'@Url.Action("ListBorrowNotebook", "RequestForm")''@Url.Action("ListBorrowNotebook", "RequestForm")',
//    $.ajax({
//        type: 'post',
//        url: '@Url.Action("ListBorrowNotebook", "RequestForm")',
//        data: { dateS: vCDateStart }, // mydata ,//
//        success: function (data) {
//            table.innerHTML = "";
//            //console.log("borrow");
//            //var htmls = "";
//            let v_row = 0;

//            if (data.status == "listdata") {
//                $.each(data.listdata, function (i, item) {
//                    var row = table.insertRow(v_row);
//                    for (let i = 0; i <= data.countDay; i++) {
//                        var cell1 = row.insertCell(i);
//                        if (i == 0) {
//                            cell1.innerHTML = item.v_bnPCName;
//                        }
//                        else if (i == 1) { cell1.innerHTML = i.toString(); if (item.v_1 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 2) { cell1.innerHTML = i.toString(); if (item.v_2 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 3) { cell1.innerHTML = i.toString(); if (item.v_3 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 4) { cell1.innerHTML = i.toString(); if (item.v_4 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 5) { cell1.innerHTML = i.toString(); if (item.v_5 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 6) { cell1.innerHTML = i.toString(); if (item.v_6 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 7) { cell1.innerHTML = i.toString(); if (item.v_7 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 8) { cell1.innerHTML = i.toString(); if (item.v_8 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 9) { cell1.innerHTML = i.toString(); if (item.v_9 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 10) { cell1.innerHTML = i.toString(); if (item.v_10 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 11) { cell1.innerHTML = i.toString(); if (item.v_11 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 12) { cell1.innerHTML = i.toString(); if (item.v_12 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 13) { cell1.innerHTML = i.toString(); if (item.v_13 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 14) { cell1.innerHTML = i.toString(); if (item.v_14 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 15) { cell1.innerHTML = i.toString(); if (item.v_15 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 16) { cell1.innerHTML = i.toString(); if (item.v_16 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 17) { cell1.innerHTML = i.toString(); if (item.v_17 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 18) { cell1.innerHTML = i.toString(); if (item.v_18 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 19) { cell1.innerHTML = i.toString(); if (item.v_19 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 20) { cell1.innerHTML = i.toString(); if (item.v_20 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 21) { cell1.innerHTML = i.toString(); if (item.v_21 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 22) { cell1.innerHTML = i.toString(); if (item.v_22 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 23) { cell1.innerHTML = i.toString(); if (item.v_23 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 24) { cell1.innerHTML = i.toString(); if (item.v_24 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 25) { cell1.innerHTML = i.toString(); if (item.v_25 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 26) { cell1.innerHTML = i.toString(); if (item.v_26 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 27) { cell1.innerHTML = i.toString(); if (item.v_27 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 28) { cell1.innerHTML = i.toString(); if (item.v_28 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 29) { cell1.innerHTML = i.toString(); if (item.v_29 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 30) { cell1.innerHTML = i.toString(); if (item.v_20 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                        else if (i == 31) { cell1.innerHTML = i.toString(); if (item.v_31 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
//                    }
//                });


//            }

//        }
//    });
//});

function getmonth(sel, action) {
    console.log(sel.value);
    var table = document.getElementById("tbListNotebookSp");

    let mydata = $("#formRequest").serialize();
    // var action = '/RequestForm/ListBorrowNotebook?dateS=' + sel.value;
    action = action + '?dateS=' + sel.value;

    $.ajax({
        type: 'post',
        url: action,
        data: mydata,//{ getID: getID }, // mydata ,//
        success: function (data) {
            table.innerHTML = "";
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
                        else if (i == 30) { cell1.innerHTML = i.toString(); if (item.v_20 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                        else if (i == 31) { cell1.innerHTML = i.toString(); if (item.v_31 != null) { cell1.style.backgroundColor = "yellow"; } else { cell1.style.backgroundColor = "white"; } }
                        //if (i <= item.bnEndDate) {
                        //    cell1.innerHTML = i.toString();
                        //}

                        ////if (item.v_st >= i && i <= item.v_ed ) {
                        ////    cell1.innerHTML = i.toString();
                        ////}
                        //else {
                        //cell1.innerHTML = i.toString();
                        //}

                        //}
                    }
                    //    event.push({
                    //        id: item.v_id,
                    //        title: item.v_bnPCName,
                    //        start: item.v_st,
                    //        end: item.v_ed,
                    //    });







                    //var row = table.insertRow(v_row);
                    //for (let i = 0; i <= 30; i++) {
                    //    var cell1 = row.insertCell(i);
                    //    if (i == 0) {
                    //        cell1.innerHTML = item.mnPCName;
                    //    } else {
                    //        if (i<=item.bnEndDate) {
                    //            cell1.innerHTML = i.toString();
                    //        }

                    //        //if (item.v_st >= i && i <= item.v_ed ) {
                    //        //    cell1.innerHTML = i.toString();
                    //        //}
                    //        else {
                    //            cell1.innerHTML = "ww";
                    //        }

                    //    }
                    //}


                    ////item.bnPCName ;
                    //for (let i = 0; i <= s_M1 + n_M2 + 1; i++) {
                    //var cell1 = row.insertCell(0);
                    //var cell2 = row.insertCell(1);

                    //cell1.innerHTML = item.v_bnPCName;
                    //cell2.innerHTML = item.v_bnStatus;

                    //cell4.innerHTML = item.v_ed;
                    //    if (i == 0) {
                    // cell1.innerHTML = item.bnPCName + "start" + item.bnStratDate + " End" + item.bnEndDate;//i; //n_M1;
                    //    } else {
                    //        if (i == (2 + cSDate - n_M2)) {
                    //            n_M1 = 1;
                    //        } else {
                    //            n_M1 = n_M1;
                    //        }
                    //        //cell1.innerHTML = n_M1;//i; //n_M1;
                    //        cell1.innerHTML = ""; //"<span style='color:red'>0</span>";//i; //n_M1;
                    //        cell1.style.backgroundColor = "yellow";
                    //        n_M1 = n_M1 + 1;


                    //    }
                    //}

                    // v_row += 1;
                });


            }

        }
    });

}
