/**
 * Created by Admin on 03.03.2017.
 */
var ang = angular.module("studyModule", []);
ang.controller("menuCtrl", function ($scope) {
    $scope.menus = [
        {
            a: "/Home/Index",
            txt: "Главная"
        },
        {
            a: "/Home/Reception",
            txt: "Запись"
        },
        {
            a: "#",
            txt: "Пациентам"
        },
        {
            a: "/Home/OMC",
            txt: "ОМС"
        },
        {
            a: "/Home/Contact",
            txt: "Контакты"
        }
    ];
    $scope.menuDown = [{
            a: "/Home/DayHospital",
            txt: "Дневной стационар"
        },
        {
            a: "/Home/Question",
            txt: "Вопрос - Ответ"
        },
        {
            a: "/Home/InformationStaff",
            txt: "Сведенья о персонале"
        },
        {
            a: "/Home/Reviews",
            txt: "Отзывы"
        }];
 $scope.menuDownTwo = [{
            a: "/Home/DayHospital",
            txt: "Уставные документы"
        },
        {
            a: "/Home/Question",
            txt: "ОМС"
        },
        {
            a: "/Home/InformationStaff",
            txt: "Норм. документы"
        },
        {
            a: "/Home/Reviews",
            txt: "Наши партнеры"
        }];
    $scope.ShowMenu = function (id, bool) {
        if (id == 2) {
            if (bool) {
               // $("#patients").css({ display: "block" });
$("#patients").fadeIn(500);
            }
            else {
               // $("#patients").css({ display: "none" });
$("#patients").fadeOut(500);
            }
        }
 if (id == 3) {
            if (bool) {
            //    $("#oms").css({ display: "block" });
$("#oms").fadeIn(500);
            }
            else {
            //    $("#oms").css({ display: "none" });
$("#oms").fadeOut(500);
            }
        }
 if (id == 5) {
            if (bool) {
            //    $("#oms").css({ display: "block" });
$(".up-right-left-down").fadeIn(500);
            }
            else {
            //    $("#oms").css({ display: "none" });
$(".up-right-left-down").fadeOut(500);
            }
        }
    }
});
ang.controller("leftmenuCtrl", ['$scope', function ($scope) {
    $scope.menus = [
        {
            a: "/Home/Index",
            txt: "Поликлиническое отделение «Золотое сердце»"
        },
        {
            a: "/Home/Reception",
            txt: "Хоспис «Золотое сердце»"
        },
        {
            a: "#",
            txt: "Косметологя и платные услуги"
        },
        {
            a: "#",
            txt: "Вход для персонала"
        }
    ];

}]);
ang.controller("addDoctorCtrl", ['$scope', function ($scope) {
    $scope.birthday = "";
$scope.education= "";
$scope.certificate= "";
$scope.institutionworks= "";
$scope.phone= "";
$scope.email= "";
$scope.password= "";
$scope.password_repeat= "";
$scope.service = "0";
$scope.firstname= "";
$scope.name= "";
$scope.lastname= "";
$scope.position= "";
$scope.$watch('[birthday ,education,certificate,institutionworks,phone,email,password,password_repeat,service ,position,firstname,name,lastname]',function(newvalue)
{
newvalue.forEach(function(item)
{
if (item != "" && newvalue[0].length == 10 && newvalue[4].length == 16 && newvalue[3].length == 10 && newvalue[5].indexOf("@") != (-1) && newvalue[6] == newvalue[7] && newvalue[8] != 0)
{
$(".admin-add-doctor-fon button").eq(1).attr("disabled", false);
}
else
{
$(".admin-add-doctor-fon button").eq(1).attr("disabled", true);
}
});

});

}]);
ang.controller("holidalCtrl", ['$scope', function ($scope) {
$scope.from = "";
$scope.do = "";
 $scope.$watch('[from ,do]', function (newNames) {

        if (newNames[0] != "" && newNames[1] !="" && newNames[0].length == 10 && newNames[1].length == 10) {
            $(".doctor-holiday-fon button").attr("disabled", false).css({background:"#2980b9",color:"#fff"});
        }
        else {
            $(".doctor-holiday-fon button").attr("disabled", true).css({background:"none",color:"#2980b9"});
        }

    });
    

}]);
ang.controller('adressCtrl', ['$scope', function ($scope) {
    $scope.items = [{
        photo: "../img/photo.jpg",
        street: "проспект Комсомольскии 14/5",
        phone: "+7 (3462) 26-71-01",
        timejob: "Круглосуточно",
        web: "zshos.ru",
geo:"61.238337,73.437975"
    },
        {
            photo: "../img/photo.jpg",
            street: "проспект Комсомольскии 14/5",
            phone: "+7 (3462) 26-71-01",
            timejob: "Круглосуточно",
            web: "zshos.ru",
geo:"61.249985,73.440213"
        },
        {
            photo: "../img/photo.jpg",
            street: "проспект Комсомольскии 14/5",
            phone: "+7 (3462) 26-71-01",
            timejob: "Круглосуточно",
            web: "zshos.ru",
geo:"61.271060,73.373199"
        }];
    $scope.margin = function (id) {
        if (id == 1) {
            return "adress-item-margin";
        }
    };
}]);

ang.controller('issuesCtrl', ['$scope', function ($scope) {
    $scope.name= "";
  $scope.email= "";
  $scope.text= "";
    $scope.$watch('[name,email,text]', function (newNames) {

        if (newNames[0] != "" && newNames[1].indexOf("@") != (-1) && newNames[2] != "") {
            $(".go-issues-last button").attr("disabled", false).css({background:"#2980b9",color:"#fff"});
        }
        else {
            $(".go-issues-last button").attr("disabled", true).css({background:"none",color:"#2980b9"});
        }

    });
$scope.AddQuestion = function ()
{
$(".go-issues").css({display:"none"});
$(".go-issues-info").css({display:"block"});
$(".go-issues-info-fon span").text($("#QuestionName").val());
$(".mask").click(function()
{
$(".go-issues-info,.mask").css({display: "none"});
 $(".blur-fon").css({filter: "", background: ""});

});
};
}]);
ang.controller('messageCtrl', ['$scope', function ($scope) {
  $scope.message = "";
$scope.$watch('message',function (newvalue)
{
if (newvalue != "")
{
 $(".go-message-last button").attr("disabled", false).css({background:"#2980b9",color:"#fff"});
        }
        else {
            $(".go-message-last button").attr("disabled", true).css({background:"none",color:"#2980b9"});
        }
});
}]);
ang.controller('hospisCtrl', ['$scope', function ($scope) {
    $scope.items = [{ img: "../img/main/1.png", txt: "г. Сургут,ул. Аэрофлотская 8" },
        { img: "../img/main/2.png", txt: "Круглосуточно" },
        { img: "../img/main/3.png", txt: "8(3462) 378-345" },
        { img: "../img/main/4.png", txt: "Zshos.ru" }];
}]);
ang.controller('hospitalCtrl', ['$scope', function ($scope) {
    $scope.items = [{ img: "../img/main/1.png", txt: "г. Сургут, пр-т Комсомольский 14/5" },
        { img: "../img/main/2.png", txt: "Пн-Пт: с 9.00-20.00  Сб-Вс: Выходной" },
        { img: "../img/main/3.png", txt: "8(3462) 378-345" },
        { img: "../img/main/4.png", txt: "www.Zsmed.ruv" }];
}]);
ang.controller('dbCtrl', ['$scope', function ($scope) {
    $scope.up = ["Фамилия Имя Отчество", "Дата рождения", "Контактный номер"];
}]);
ang.controller('lsCtrl', ['$scope','$http', function ($scope,$http) {
    $scope.itemsDoc = [{ text: "Личная информация", a: "/Home/Doctor/" }, { text: "Записи", a: "/Account/Reception" }, { text: "Вопросы", a: "/Account/Questions" ,count:0}, { text: "Информация для персонала", a: "/Account/Info",count:0 }, { text: "Выйти", a: "/Account/Logout" }];
    $scope.itemsUser = ["Личная информация", "история записей", "ваши вопросы"];

    function Counts() {
        $http.post('/Account/CountInfoQuest', JSON.stringify({

        }))
            .then(function (data, status, headers, config) {

                var arra = data.data.split(";");
                $scope.itemsDoc[2].count = arra[0];
                $scope.itemsDoc[3].count = arra[1];
                console.log(data.data);

            }, function (error) {
                console.log(error);
            });
    }
    Counts();
    setInterval(function () { Counts() },5000);
    $scope.update = function (id) {
        if (location.pathname.indexOf("/Home/Doctor") != (-1))
        {
            if (id == 0) {
                return "ls-active";
            }
        }
        if (location.pathname.indexOf("/Account/Reception") != (-1))
        {
            if (id == 1) {
                return "ls-active";
            }
        }
        if (location.pathname.indexOf("/Account/Questions") != (-1))
        {
            if (id == 2) {
                return "ls-active";
            }
        }
        if (location.pathname.indexOf("/Account/Info") != (-1))
        {
            if (id == 3) {
                return "ls-active";
            }
        }

    };
    $scope.updateline = function (object) {
        $(".ls-fon-item").removeClass("ls-active");
        $(object.currentTarget).addClass("ls-active");

    };
}]);
ang.controller('lsadminCtrl', ['$scope', function ($scope) {
    $scope.itemsAdmin = [{ text: "Время приема", a: "/Administrator" }, { text: "Персонал", a: "/Administrator/Staff" }, { text: "База пациентов", a: "/Administrator/Patients" }, { text: "Сообщения для персонала", a: "/Administrator/MessageForStaff" }, { text: "Выйти", a: "/Account/Logout" }];
    $scope.update = function (id) {
     
        if (location.pathname.indexOf("/Administrator/Staff") != (-1))
        {
            if (id == 1) {
                return "ls-active";
            }
return "";
        }
        if (location.pathname.indexOf("/Administrator/Patients") != (-1))
        {
            if (id == 2) {
                return "ls-active";
            }
return "";
        }
        if (location.pathname.indexOf("/Administrator/MessageForStaff") != (-1))
        {
            if (id == 3) {
                return "ls-active";
            }
return "";
        }
  if (location.pathname.indexOf("/Administrator") != (-1))
        {
            if (id == 0) {
                return "ls-active";
            }
return "";
        }

    };
    $scope.updateline = function (object) {
        $(".ls-fon-item").removeClass("ls-active");
        $(object.currentTarget).addClass("ls-active");

    };
}]);

ang.controller('checkCtrl', ['$scope', function ($scope) {
    $scope.first_name = "";
    $scope.last_name = "";
    $scope.second_name = "";
    $scope.sex = "M";
    $scope.day = "День";
    $scope.month = "Месяц";
    $scope.years = "Год";
    $scope.phone = "";
    $scope.email = "";
    $scope.password = "";
    $scope.password_repeat = "";
    $scope.$watch('[first_name,last_name,second_name,sex,day,month,years,phone,email,password,password_repeat]', function (newNames) {
        newNames.forEach(function (item, i, arr) {
            if (item != "" && newNames[4] != "День" && newNames[5] != "Месяц" && newNames[6] != "Год" && newNames[9] == newNames[10] && newNames[9].length >= 6) {
                $(".check-btn button").attr("disabled", false);
            }
            else {
                $(".check-btn button").attr("disabled", true);
            }
        });
    });
    $scope.Select = function (object, sel) {
        $(".check-fon-item-last div").removeClass("sex-active");
        $(object.currentTarget).addClass("sex-active");

        $scope.sex = sel;
        if (sel == "M") {
            $("input[name='Gender']").val(true);
        }
        else {
            $("input[name='Gender']").val(false);
        }

    };

}]);

ang.controller('appoCtrl', ['$scope','$http', function ($scope,$http) {
    $scope.first_name = "";
    $scope.last_name = "";
    $scope.second_name = "";
 
 
 
 /*   $(".appo-centr-time-down button,.appo-centr-time button").click(function () {
        if ($(this).hasClass("appo-closed") == false) {
            $(".appo-centr-time-down button,.appo-centr-time button").removeClass("appo-select");
            $(this).addClass("appo-select");
            $scope.time = $(this).text();
        }
    });*/
    $("select[name = 'month']").change(function () {
        doctor.day = null;
        doctor.month = $(this).val();
        if (new Date().getMonth() == 11 && $(this).val() == "0") {
            doctor.years++;
        }
        $(".appo-h-down button").attr("disabled", true);
        $(".appo-centr-down-down").css({ display: "none" });
    });

    $("input[name = 'name'], select[name = 'service'], input[name = 'phoneAppo'],input[name = 'birthday']").change(function () {
        doctor.fls = $("input[name = 'name']").val();
        doctor.service = $("select[name = 'service']").val();
        doctor.phone = $("input[name = 'phoneAppo']").val();
        doctor.birthday = $("input[name = 'birthday']").val();
        if (window.location.pathname.indexOf("/Home/Doctor/") != (-1)) {
            doctor.service = 0;
            $(".appo-centr-down-down").html("<p>Ваши Ф.И.О: " + doctor.fls + ", Контактный номер: " + doctor.phone + "</p>" +
                  "<p>Врач: " + doctor.doctorname + ", Дата: " + doctor.day + "." + (parseInt(doctor.month) + 1) + "." + doctor.years + ", Время: " + doctor.time + "</p>");
        }
        else
        {
            $(".appo-centr-down-down").html("<p>Ваши Ф.И.О: " + doctor.fls + ", Контактный номер: " + doctor.phone + "</p>" +
                  "<p>Услуга: " + $("select[name = 'service'] option").eq(doctor.service).text() + ", Врач: " + doctor.doctorname + ", Дата: " + doctor.day + "." + (parseInt(doctor.month) + 1) + "." + doctor.years + ", Время: " + doctor.time + "</p>");
        }
        if (doctor.fls.split(" ").length >= 2 && doctor.service != null && doctor.phone != "" && doctor.day != null && doctor.month != null && doctor.birthday != "" && doctor.time != "")
        {
            $(".appo-centr-down-down").css({ display: "block" });
            $(".appo-h-down button").attr("disabled", false).css({color:"#fff",background:"#2980b9"}) ;
        }
        else
        {
            $(".appo-h-down button").attr("disabled", true).css({color:"#2980b9",background:"none"}) ;
            $(".appo-centr-down-down").css({ display: "none" });

        }
      

    });
  
    $("select[name = 'service'], select[name = 'day']").change(function () {
        doctor.day = $("select[name = 'day']").val();
        if (window.location.pathname.indexOf("/Home/Doctor/") != (-1)) {
            if ($("select[name = 'day']").val() != null && $("select[name = 'month']").val() != null) {
                $(".appo-h-down button").attr("disabled", true);
                $(".appo-centr-down-down").css({ display: "none" });
                $http.post('/Home/_Doc', JSON.stringify({
                    serviceId: 0,
                    day: $("#day_appo").val(),
                    mounth: $("#month_appo").val(),
                    docId: $(".appo-centr-down").attr("doctor-id")
                }))
                    .then(function (data, status, headers, config) {
                        if (data.data != "0") {
                            $("#Docs").html(data.data);
                        }
                        else {
                            console.log(data.data);
                        }
                    }, function (error) {
                        console.log(error);
                    });

            }
            return;
        }
        if ($("select[name = 'service']").val() != null && $("select[name = 'day']").val() != null && $("select[name = 'month']").val() != null) {
            $(".appo-h-down button").attr("disabled", true);
            $(".appo-centr-down-down").css({ display: "none" });
            $http.post('/Home/_Doc', JSON.stringify({
                serviceId: $("#jojoba").val(),
                day: $("#day_appo").val(),
                mounth: $("#month_appo").val(),
                docId: 0
            }))
                .then(function (data, status, headers, config) {
                    if (data.data != "0") {
                        $("#Docs").html(data.data);
                    }
                }, function (error) {
                    console.log(error);
                });
        }
    });
 
    $scope.SendReception = function () {
        $http.post('/Home/Reception', JSON.stringify({
            Name: doctor.fls,
            Phone: doctor.phone,
            ServiceId: doctor.service,
            Day: doctor.day,
            Mounth: doctor.month,
            Birthday: doctor.birthday,
            Time: doctor.time,
            DocId: doctor.id
        }))
               .then(function (data, status, headers, config) {
                   $(".appo-centr-sms").css({display:"block"});
                 
               }, function (error) {
                   console.log(error);
               });
    };
   
    $scope.Select = function (object, sel) {
        $(".check-fon-item-last div").removeClass("sex-active");
        $(object.currentTarget).addClass("sex-active");

        $scope.sex = sel;
        if (sel == "M") {
            $("input[name='Gender']").val(true);
        }
        else {
            $("input[name='Gender']").val(false);
        }

    };

}]);
ang.controller('singCtrl', ['$scope','$http', function ($scope,$http) {

    $scope.email = "";
    $scope.password = "";
    $scope.phone = "";
$scope.block = true;
    $scope.$watch('[email,password]', function (newNames) {
      
            if (newNames[0].indexOf("@") == (-1)) {
                $scope.phone = newNames[0];
                $scope.email = "";
            }
            else {
                $scope.phone = "";
                $scope.email = newNames[0];
            }
            if (newNames[0] != "" && newNames[1].length >= 6) {

                $(".sing-down button").attr("disabled", false);
$scope.block = false;
            }
            else {
                $(".sing-down button").attr("disabled", true);
$scope.block = true;
            }
      
    });
$scope.EnterSing = function (obj)
{
console.log(obj);
if (obj.keyCode == 13 && $scope.block == false)
{
$scope.sing();
}

};
    $scope.Select = function (object, sel) {
        $(".check-fon-item-last div").removeClass("sex-active");
        $(object.currentTarget).addClass("sex-active");

        $scope.sex = sel;
    };
    $scope.sing = function () {
        $http.post('/Home/Login', JSON.stringify({
            Email: $scope.email,
            Phone: $scope.phone,
            Pass: $scope.password
        }))
            .then(function (data, status, headers, config) {
                if (data.data != "1")
                {
                    $(".sing-down span").text(data.data);
                }
                else
                {
                    location.reload();
                }

            }, function (error) {
                console.log(error);
            });
    };

}]);
ang.controller('serviceCtrl', ['$scope', function ($scope) {
    $scope.items = [{ photo: "../img/photo.jpg", txt: "Узи <br> сердца", btn: "записаться" },
        { photo: "../img/photo.jpg", txt: "Узи <br> сердца", btn: "записаться" },
        { photo: "../img/photo.jpg", txt: "Узи <br> сердца", btn: "записаться" },
        { photo: "../img/photo.jpg", txt: "Узи <br> сердца", btn: "записаться" }];
    $scope.services = [{ name: "Амбулаторно поликлинические услуги", photo: "../img/minus.png" },
        { name: "Видеоконсультация", photo: "../img/plus.png" },
        { name: "Лабораторные услуги", photo: "../img/plus.png" },
        { name: "Услуги процедурного кабинета", photo: "../img/plus.png" },
        { name: "Услуги УЗИ и функциональной диагностики", photo: "../img/plus.png" }];
    $scope.itemservice = [{ name: "сама тослатя баба в мире", price: "600р" },
        { name: "сама тослатя баба в мире", price: "600р" },
        { name: "сама тослатя баба в мире", price: "600р" }];
    $scope.first = function (id) {
        if (id == 0) {
            return "service-item-info-two";
        }
        else {
            return "service-item-info";
        }
    };

}]);
ang.directive('compileHtml', function ($compile) {
    return function (scope, element, attrs) {
        scope.$watch(
            function (scope) {
                return scope.$eval(attrs.compileHtml);
            },
            function (value) {
                element.html(value);
                $compile(element.contents())(scope);
            }
        );
    };
});
ang.controller('aboutCtrl', ['$scope', function ($scope) {
    $scope.items = [{ txt: "запись <br> на прием", img: "../img/about/edit.png" },
        { txt: "узнать <br> про лечение <br> по омс", img: "../img/about/pill.png" },
        { txt: "посмотреть <br> услуги <br> и цены", img: "../img/about/price.png" }];
    $scope.First = function (id) {
        if (id == 0) {
            return "about-action-item-first";
        }
        if (id == 1) {
            return "about-action-margin";
        }
    };

}]);
ang.controller('footerCtrl', ['$scope', function ($scope) {
    $scope.one = ["Главная", "Поликлиника 1", "Поликлиника 2", "Поликлиника 3"];
    $scope.two = ["Вопрос-ответ", "Актуальное", "Архив"];
    $scope.three = ["Услуги и цены", "Подробно об услугах", "Стоимость"];
    $scope.right = ["Карточка предприятия", "Уставные документы", "Пациентам", "Контактная информация", "Политика конфиденциональности", "Карта сайта"];
}]);