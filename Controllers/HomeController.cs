using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GoldenHeart.Models;
using Microsoft.Owin.Security;

namespace GoldenHeart.Controllers
{
    public class HomeController : Controller
    {
        // Работа с Owin авторизацией
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        // Инициализация входа в Базу Данных
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // Авторизация
        //-------------------------------------------------------------------------
        [HttpPost]
        public ActionResult Login(LogDoc LogDoc)
        {
            if (!ModelState.IsValid) return Content("Проверьте правильность введёных данных");
            var Doc = new User();
            var hash = Hash.GetHashString(LogDoc.Pass);

            if (LogDoc.Email != null && LogDoc.Pass != null)
                Doc = db.Docs.FirstOrDefault(u => u.Email == LogDoc.Email && u.HashPassword == hash);
            else if (LogDoc.Phone != null && LogDoc.Pass != null)
                Doc = db.Docs.FirstOrDefault(u => u.Phone == LogDoc.Phone && u.HashPassword == hash);
            else
                return Content("Неправильный логин или пароль.");

            if (Doc != null)
            {
                var claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, Doc.Id.ToString(), ClaimValueTypes.String));
                claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, Doc.FirstName + " " + Doc.Name.First() + ". " + Doc.LastName.First() + ".", ClaimValueTypes.String));
                claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "OWIN Provider", ClaimValueTypes.String));
                if (Doc.Role != null)
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, Doc.Role.Name, ClaimValueTypes.String));

                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);

                return Content("1");
            }
            else
                return Content("Неправильный логин или пароль. Возможно у вас выбрана другая раскладка клавиатуры или нажата клавиша \"Caps Lock\".");
        }

        // Главная
        //-------------------------------------------------------------------------
        public ActionResult Index()
        {
            ViewBag.page = "Main";
            ViewBag.wow = "1";
            return View();
        }

        // Запись на приём
        //-------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Reception()
        {
            return View(db.Services.ToList());
        }

        // Запись на приём
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task Reception(ReceptionUser Recep)
        {
            if (ModelState.IsValid)
            {
                var year = DateTime.Now.Year;
                ++Recep.Mounth;
                var time = Recep.Time.Split(':');
                var birthday = Recep.Birthday.Split('.');
                year = (Recep.Mounth == 1 && DateTime.Now.Month == 12) ? ++year : year;
                var doc = await db.Docs.FindAsync(Recep.DocId);
                var dateReception = new DateTime(year, Recep.Mounth, Recep.Day, int.Parse(time[0]), int.Parse(time[1]), 0);
                var birthdayModel = new DateTime(int.Parse(birthday[2]), int.Parse(birthday[1]), int.Parse(birthday[0]));

                if (doc.DocReceptions.FirstOrDefault(r => r.DateReception == dateReception) == null)
                {
                    var patient = new Patient() {
                        Name = Recep.Name,
                        Phone = Recep.Phone,
                        Birthday = birthdayModel
                    };

                    var existPatient = await db.Patients.FirstOrDefaultAsync(p => p.Name == patient.Name && p.Phone == patient.Phone && p.Birthday == patient.Birthday);

                    if (existPatient == null)
                        db.Patients.Add(patient);
            
                    db.Receptions.Add(new Reception()
                    {
                        PatientName = Recep.Name,
                        Phone = Recep.Phone,
                        Birthday = birthdayModel,
                        DateReception = dateReception,
                        Doc = doc
                    });
                    await db.SaveChangesAsync();
                }
            }
        }

        // Вывод докторов
        //-------------------------------------------------------------------------
        [HttpPost]
        public ActionResult _Doc(int docId, int serviceId, int day, int mounth, string Date, string Type)
        {
            if (string.IsNullOrEmpty(Date))
            {
                var year = DateTime.Now.Year;
                ++mounth;
                year = (mounth == 1 && DateTime.Now.Month == 12) ? ++year : year;
                ViewBag.dateReception = new DateTime(year, mounth, day);
            }
            else
            {
                var date = Date.Split('.');
                ViewBag.dateReception = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
            }
            
            var docs = new List<User>();

            if (serviceId != 0)
                docs = db.Docs.Where(d => d.Service.Id == serviceId && d.Role.Name != "Admin").ToList();
            else if (docId != 0)
                docs = db.Docs.Where(d => d.Id == docId && d.Role.Name != "Admin").ToList();
            else
                docs = db.Docs.Where(d => d.Role.Name != "Admin").ToList();
            
            if (Type == "Admin")
                ViewBag.editAdmin = 1;

            return PartialView(docs);
        }

        // Дневной стационар
        //-------------------------------------------------------------------------
        public ActionResult DayHospital()
        {
            return View();
        }

        // Сведения о персонале
        //-------------------------------------------------------------------------
        public ActionResult InformationStaff ()
        {
            return View(db.Docs.Where(d => d.Role.Name != "Admin").ToList());
        }

        // Отзывы
        //-------------------------------------------------------------------------
        public ActionResult Reviews()
        {
            return View();
        }

        // Вопросы
        //-------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Question()
        {
            // View Count Questions
            var VCQ = 5;
            var Questions = db.Questions.Where(q => q.Reply != null).OrderByDescending(q => q.DateCreate).Include("Reply").ToList();

            ViewBag.allQuestionsCount = Questions.Count;
            ViewBag.viewQuestionsCount = VCQ;

            if (Questions.Count >= VCQ)
                Questions.RemoveRange(VCQ, Questions.Count - VCQ);

            return View(Questions);
        }

        // Частичное представление вопроса
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> _Question(int viewQuestionsCount, int VCQ, string Search)
        {
            var Questions = new List<Question>();

            if (Search == null)
                Questions = await db.Questions.Where(q => q.Reply != null).OrderByDescending(q => q.DateCreate).Include("Reply").ToListAsync();
            else
                Questions = await db.Questions.Where(q => q.Reply != null && (q.Name.Contains(Search) || q.Text.Contains(Search) || q.Reply.NameDoc.Contains(Search) || q.Reply.Text.Contains(Search))).OrderByDescending(q => q.DateCreate).Include("Reply").ToListAsync();

            ViewBag.allQuestionsCount = Questions.Count;
            ViewBag.viewQuestionsCount = viewQuestionsCount + VCQ;

            if (Questions.Count > viewQuestionsCount)
            {
                Questions.RemoveRange(0, viewQuestionsCount);
                Questions = Questions.GetRange(0, (Questions.Count > VCQ) ? VCQ : Questions.Count);
                return PartialView(Questions);
            }
            return Content(null);
        }

        // Добавление вопроса
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task Question([Bind(Include = "Name,Email,Text")] Question Question)
        {
            if (ModelState.IsValid)
            {
                var id = User.Identity.GetUserId<int>();
                Question.DateCreate = DateTime.Now;
                db.Questions.Add(Question);
                await db.SaveChangesAsync();
            }
        }

        // ОМС
        //-------------------------------------------------------------------------
        public ActionResult OMC()
        {
            return View();
        }

        // Контакты
        //-------------------------------------------------------------------------
        public ActionResult Contact()
        {
            return View();
        }

        // Личный кабинет врача
        //-------------------------------------------------------------------------
        public ActionResult Doctor(int? id)
        {
            var doc = db.Docs.Find(id);
            ViewBag.newCountQuestion = db.Questions.Count(q => q.Reply == null);
            ViewBag.newCountInfo = 3;
            ViewBag.dateReception = DateTime.Now.Date;

            return View(doc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}