/**
 * Created by Admin on 09.03.2017.
 */
var check_days = 0;
var doctor = {time:"",doctorname:"",fls:"",phone:"",service:"",day:"",month:"",years:new Date().getFullYear(),id:"",birthday:""
};
$(document).ready(function () {
$(".mask").click(function()
{
 $(".sing").css({display: "none"});
});
    var height = document.documentElement.clientHeight;
    $(".service-item-img").click(function () {

        if ($(this).find("img").attr("src") === "../img/minus.png") {
            $(this).find("img").attr("src", "../img/plus.png");
            $(this).parent().find(".service-item-info").slideToggle("slow");
            $(this).parent().find(".service-item-info-two").slideToggle("slow");
        }
        else {
            $(this).find("img").attr("src", "../img/minus.png");
            $(this).parent().find(".service-item-info").slideToggle("slow");
            $(this).parent().find(".service-item-info-two").slideToggle("slow");
        }
    });
    $("#month_appo").change(
        function () {
            var days_in_april = 0;
            if (new Date().getMonth() === 11) {
                if (parseInt($(this).val()) !== 11)
                {
                    days_in_april = 32 - new Date(new Date().getFullYear() + 1, $(this).val(), 32).getDate();
                }
                else
                {
                    days_in_april = 32 - new Date(new Date().getFullYear(), $(this).val(), 32).getDate();
                }
            }
            else
            {
                days_in_april = 32 - new Date(new Date().getFullYear(), $(this).val(), 32).getDate();
            }
            $("#day_appo").html("<option selected disabled>День</option>");

            var i;
            if (new Date().getMonth() === parseInt($(this).val()))
            {
                for (i = new Date().getDate() ; i <= days_in_april; i++) {
                    $("#day_appo").append("<option >" + i + "</option>");
                }

            }
            else
            {
                
                for (i = 1 ; i <= new Date().getDate() ; i++) {
                    $("#day_appo").append("<option >" + i + "</option>");
                }
            }
           
            
        }
    );
    $("#years").change(function()
    {
        check_days = 32 - new Date($(this).val(), 0, 32).getDate();
        $("#month").val("0");
        $("#day").val("1");
    });
    $("#month").change(function()
    {
        check_days = 32 - new Date( $("#years").val(), $(this).val(), 32).getDate();
        $("#day").html("<option selected disabled>День</option>");
        for (var i=0;i<check_days;i++)
        {
            $("#day").append("<option >" + i+ "</option>");
        }
    });
   /* $("body").on("click",".hospital-item",function()
    {
        $(".hospital").css({display:"none"});
       $(".hospital-fon-user-fon").css({display:"block"});
        $(".hospital-fon-user-appo").css({display:"block"});
    });*/
    $(".up-right-left").mouseover(function() {
        $(".up-right-left-down").css({display:"block"});
    }).mouseout(function() {
        $(".up-right-left-down").css({display:"none"});
    });
        $('input[name="dataAdmin"]').mask("00.00.0000");
    if ($("body").height() < height) {
        $("footer").css({ marginTop: (height - $("body").height()) + "px" });
    }
  $(".hospital-fon-user-right input").keydown(function()
    {
        console.log($(this).attr("start-width"));
if ($(this).attr("start-width") != undefined || $(this).attr("start-width") != null)
{
    if (($(this).val().length * parseInt($(this).attr("step"))) > parseInt($(this).attr("start-width")))
    {
        $(this).css({width: ($(this).val().length * parseInt($(this).attr("step"))) + "px"});
    }
    else
    {
        $(this).css({width: parseInt($(this).attr("start-width")) + "px"});
    }
}

    });
var phoneC = $(".ls-center-info-down input").eq(0).val();
    var emailC = $(".ls-center-info-down input").eq(1).val();
    $(".ls-center-info-up-item input").keyup(function () {

        if ($(".ls-center-info-down input").eq(0).val() != phoneC || emailC != $(".ls-center-info-down input").eq(1).val()) {
            $(".ls-center-change button").attr("disabled", false);
        }
        else {
            if ($(".ls-center-info-down input").eq(0).val() == phoneC && emailC == $(".ls-center-info-down input").eq(1).val()) {
                $(".ls-center-change button").attr("disabled", false);
            } else {
                $(".ls-center-change button").attr("disabled", true);
            }
        }

        if ($(".ls-center-info-up-item input").eq(0).val() != "" && $(".ls-center-info-up-item input").eq(1).val() == $(".ls-center-info-up-item input").eq(2).val() && $(".ls-center-info-up-item input").eq(1).val().length >= 6)
        {
            $(".ls-center-change button").attr("disabled",false);
        }
        else
        {
            if ($(".ls-center-info-up-item input").eq(0).val() == "" && $(".ls-center-info-up-item input").eq(1).val() == "" && $(".ls-center-info-up-item input").eq(2).val() == "")
            {
                $(".ls-center-change button").attr("disabled",false);
            }
            else
            {
                $(".ls-center-change button").attr("disabled",true);
            }
        }

    });
    $(".ls-center-change button").click(function () {
        var Data = {
            OldPass:"",
            NewPass:"",
            ConfirmPass:"",
            Phone:"",
            Email:""
        };
        if ($(".ls-center-info-up-item input").eq(0).val() != "" && $(".ls-center-info-up-item input").eq(1).val() == $(".ls-center-info-up-item input").eq(2).val())
        {
            Data.OldPass = $(".ls-center-info-up-item input").eq(0).val();
            Data.NewPass = $(".ls-center-info-up-item input").eq(1).val();
            Data.ConfirmPass = $(".ls-center-info-up-item input").eq(2).val();
        }
        else
        {
            if ($(".ls-center-info-up-item input").eq(0).val() == "" && $(".ls-center-info-up-item input").eq(1).val() == "" && $(".ls-center-info-up-item input").eq(2).val() == "")
            {
                Data.OldPass = "";
                Data.NewPass = "";
                Data.ConfirmPass = "";
            }
        }

        if ($(".ls-center-info-down input").eq(0).val() != phoneC || emailC != $(".ls-center-info-down input").eq(1).val())
        {
            Data.Phone = $(".ls-center-info-down input").eq(0).val();
            Data.Email = $(".ls-center-info-down input").eq(1).val();
        }
        else
        {
            if ($(".ls-center-info-down input").eq(0).val() == phoneC && emailC == $(".ls-center-info-down input").eq(1).val()) {
                Data.Phone = "";
                Data.Email = "";
            }
            else {
                Data.Phone = $(".ls-center-info-down input").eq(0).val();
                Data.Email = $(".ls-center-info-down input").eq(1).val();
            }
        }

        $.ajax({
            url: "/Account/EditInfo",
            type: "POST",
            data: Data,
            success: function(data)
            {
                $(".ls-center-change span").text(data);
            },
            error: function ()
            {

            }
        });
    });

HoverAnimation($(".up-left-right"),"menu");
 $('.main-right').scrolly();
    new WOW().init();
});
function SetHoliday ()
{
 $.ajax({
            url: "/Administrator/Leave",
            type: "POST",
            data: {id:$("#docId").val(),
Begin:$(".doctor-holiday-fon input").eq(0).val(),
End:$(".doctor-holiday-fon input").eq(1).val()},
            success: function(data)
            {
location.reload();

            },
            error: function ()
            {

            }
        });
}
function TogglePassword(obj)
{
    if ( $(obj).parent().find("input").attr("type") == "password")
    {
       $(obj).css({background:"url(../img/close_password.svg) no-repeat",backgroundSize:"contain",backgroundPositionY:"7px"});
        $(obj).parent().find("input").attr("type","text");
    }
    else
    {
        $(obj).css({background:"url(../img/open_password.svg) no-repeat",backgroundSize:"contain"});
        $(obj).parent().find("input").attr("type","password");
    }

}
function ShowHoliday()
{
$(".doctor-holiday,.mask").css({display:"block"});
$(".mask").click(function ()
{
$(".doctor-holiday,.mask").css({display:"none"});
   $(".blur-fon").css({ filter: "", background: "" });
});
 $(".blur-fon").css({ filter: "blur(5px)", background: "rgba(255,255,255,0.55)" });

}
function ClosedHoliday()
{
$(".doctor-holiday,.mask").css({display:"none"});
   $(".blur-fon").css({ filter: "", background: "" });
}
function Goto(str)
{
window.location.href = str;
}
function ShowMenuLeft() {
    $(".main-left").css({ display: "block" });
    $(".main-left").stop(1, 1).animate({ left: "0px" }, 500,function ()
{
 $(".mask").css({ display: "block" });
});
    $(".blur-fon").css({ filter: "blur(5px)", background: "rgba(255,255,255,0.55)" });
}
function ClosedMenuLeft() {
    $(".main-left").stop(1, 1).animate({ left: "-400px" }, 500, function () {
        $(".main-left,.mask").css({ display: "none" });
        $(".blur-fon").css({ filter: "", background: "" });
    });


}
 function HoverAnimation(obg,who)
{

     $(obg).mouseover(function() {
        if (who == "menu")
        {
            $(this).find(".up-left-right-item").eq(0).stop(1,1).animate({marginBottom:"10px"},300);
            $(this).find(".up-left-right-item").eq(1).stop(1,1).animate({marginBottom:"10px"},300);
        }
    }).mouseout(function() {
        if (who == "menu") {
            $(this).find(".up-left-right-item").eq(0).stop(1, 1).animate({marginBottom: "5px"}, 300);
            $(this).find(".up-left-right-item").eq(1).stop(1, 1).animate({marginBottom: "5px"}, 300);
        }
    });
}
function SendDoctor()
{
  
    console.log("SendDoctor");
    $.ajax({
        url: "/Home/Reception", method: "POST",
        data: {
            Name: doctor.fls,
            Phone: doctor.phone,
            ServiceId: 0,
            Day: doctor.day,
            Mounth: doctor.month,
            Birthday: doctor.birthday,
            Time: doctor.time,
            DocId: doctor.id
        }, success: function () {
            $(".appo-centr-sms").css({ display: "block" });
        },
        error: function () {
            console.log(error);
        }
    });
            
}
function SelectTime(obj) {
    if (window.location.pathname === "/Administrator" || window.location.pathname === "/Administrator/Index")
    {
        AdminDoctor(obj);
        return;
    }
    if (  $(obj).hasClass("appo-select"))
{
doctor.time = "";
$("#Docs button").removeClass("appo-select");
 $(".appo-h-down button").attr("disabled", true).css({color:"#2980b9",background:"none"}) ;
  $(".appo-centr-down-down").css({ display: "none" });
return;
}
    
        $("#Docs button").removeClass("appo-select");
    
   
    $(obj).addClass("appo-select");
    doctor.time = $(obj).text();
    doctor.id = $(obj).parents(".appo-centr-down").attr("doctor-id");
    doctor.doctorname = $(obj).parents(".appo-centr-down").attr("doctor-name");
    if (window.location.pathname.indexOf("/Home/Doctor/") !== (-1)) {
        $(".appo-centr-down-down").html("<p>Ваши Ф.И.О: " + doctor.fls + ", Контактный номер: " + doctor.phone + "</p>" +
                 "<p>Врач: " + doctor.doctorname + ", Дата: " + doctor.day + "." + (parseInt(doctor.month) + 1) + "." + doctor.years + ", Время: " + doctor.time + "</p>");
        if (doctor.fls.split(" ").length  >= 2 && doctor.phone !== "" && doctor.day !== null && doctor.month !== null) {
            $(".appo-centr-down-down").css({ display: "block" });
            $(".appo-h-down button").attr("disabled", false).css({color:"#fff",background:"#2980b9"}) ;
        }
        else {
            $(".appo-h-down button").attr("disabled", true).css({color:"#2980b9",background:"none"}) ;
            $(".appo-centr-down-down").css({ display: "none" });

        }
    }
    else
    {
        $(".appo-centr-down-down").html("<p>Ваши Ф.И.О: " + doctor.fls + ", Контактный номер: " + doctor.phone + "</p>" +
                 "<p>Услуга: " + $("select[name = 'service'] option").eq(doctor.service).text() + ", Врач: " + doctor.doctorname + ", Дата: " + doctor.day + "." + (parseInt(doctor.month) + 1) + "." + doctor.years + ", Время: " + doctor.time + "</p>");

        if (doctor.fls.split(" ").length >= 2 && doctor.service !== null && doctor.phone !== "" && doctor.day !== null && doctor.month !== null) {
            $(".appo-centr-down-down").css({ display: "block" });
            $(".appo-h-down button").attr("disabled", false).css({color:"#fff",background:"#2980b9"}) ;
        }
        else {
            $(".appo-h-down button").attr("disabled", true).css({color:"#2980b9",background:"none"}) ;
            $(".appo-centr-down-down").css({ display: "none" });

        }
    }
   
    
};
function AdminDoctor(obj)
{
    var id = $(obj).parents(".appo-centr-down").attr("doctor-id");
    var date = $("#dataAdmin").val();
    if (date === null || date === "") {
        date = $("#dataAdmin").attr("placeholder");
    }
    var time = $(obj).val();
    console.log(id + " " + date + " " + time);

    if ($(obj).hasClass("appo-closed")) {
        $(obj).removeClass("appo-closed");
    }
    else {
        $(obj).addClass("appo-closed");
    }
    $.ajax({
        url: "/Administrator/DeleteTime",
        method: "POST",
        data: {
            id: id,
            Date: date,
            Time: time
        }
    });
}
function ClosedSing() {
    $(".sing").css({display: "none"});
  //  $(".blur-fon").css({filter: "", background: ""});

}
function Sing() {
    $(".sing").css({display: "block"});

   // $(".blur-fon").css({filter: "blur(5px)", background: "rgba(255,255,255,0.55)"});
}
function ClosedCheckInfo() {
    $(".check-info").css({display: "none"});
    $(".blur-fon").css({filter: "", background: ""});
}
function Check() {
    $(".check-info").css({display: "block"});
    $(".blur-fon").css({ filter: "blur(5px)", background: "rgba(255,255,255,0.55)" });
}
function ShowMessage()
{
 $(".go-message,.mask").css({display: "block"});
$(".mask").click(function()
{
$(".go-message,.mask").css({display: "none"});
 $(".blur-fon").css({filter: "", background: ""});

});
    $(".blur-fon").css({ filter: "blur(5px)", background: "rgba(255,255,255,0.55)" });
}
function GoIssues() {
    $(".go-issues,.mask").css({display: "block"});
$(".mask").click(function()
{
$(".go-issues,.mask").css({display: "none"});
 $(".blur-fon").css({filter: "", background: ""});

});
    $(".blur-fon").css({ filter: "blur(5px)", background: "rgba(255,255,255,0.55)" });
    $(".go-issues-fon h3").text("задать вопрос");
    $(".go-issues-last button").text("задать вопрос");
}
function SetMessage()
{
 $.ajax({
            url: "/Administrator/MessageForStaff",
            type: "POST",
            data: {Text:$(".go-message-item textarea").val()},
            success: function(data)
            {
location.reload();
            },
            error: function ()
            {

            }
        });
}
function ClosedMessage()
{
 $(".go-message,.mask").css({display: "none"});
    $(".blur-fon").css({filter: "", background: ""});
}
function ClosedIssuesInfo ()
{
 $(".go-issues-info,.mask").css({display: "none"});
    $(".blur-fon").css({filter: "", background: ""});
}
function ClosedIssues() {
 $(".go-issues,.mask").css({display: "none"});
    $(".blur-fon").css({filter: "", background: ""});
}
function ContuneQestion(id) {
    $("input[name='QuestionId']").val(id);
    $(".go-issues-fon h3").text("Продолжить вопрос");
    $(".go-issues").css({display: "block"});
    $(".blur-fon").css({ filter: "blur(5px)", background: "rgba(255,255,255,0.55)" });
    $(".go-issues-last button").text("Ответить");
}