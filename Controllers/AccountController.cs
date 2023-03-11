using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GoldenHeart.Models;
using Microsoft.Owin.Security;

namespace GoldenHeart.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // Работа с Owin авторизацией
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        // Подключение к базе данных
        private ApplicationDbContext db = new ApplicationDbContext();

        // Выход
        //-------------------------------------------------------------------------
        public ActionResult Logout()
        {
            // Удаляем клеймы
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // Запись на прием
        //-------------------------------------------------------------------------
        public ActionResult Reception()
        {
            var id = User.Identity.GetUserId<int>();
            DateTime dateRec = DateTime.Now.AddDays(-1);
            // Вытаскиваем только актуальные записи
            var rec = db.Receptions.Where(r => r.Doc.Id == id && dateRec <= r.DateReception).OrderBy(r => r.DateReception).ToList();

            return View(rec);
        }

        // Вопросы
        //-------------------------------------------------------------------------
        public ActionResult Questions()
        {
            return View(db.Questions.Where(q => q.Reply == null).ToList());
        }

        // Информация для персонала
        //-------------------------------------------------------------------------
        public ActionResult Info()
        {
            // Информация для персонала висит 2 недели
            var dateInfo = DateTime.Now.AddDays(-14);
            var info = db.MessagesStaff.Where(m => m.DateCreate > dateInfo).OrderByDescending(m => m.DateCreate).ToList();

            return View(info);
        }

        // Показать телефонный номер
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task ViewPhone(int id)
        {
            // Разрешаем показывать телефонный номер пациентам
            var doc = await db.Docs.FindAsync(id);
            if (doc != null) doc.VisiblePhone = !doc.VisiblePhone;
            await db.SaveChangesAsync();
        }

        // Показать почтовый адресс
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task ViewEmail(int id)
        {
            // Разрешаем показывать почтовый адресс пациентам
            var doc = await db.Docs.FindAsync(id);
            if (doc != null) doc.VisibleEmail = !doc.VisibleEmail;
            await db.SaveChangesAsync();
        }

        // Скрытие вопроса
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task HiddenQuestion(int id)
        {
            var docId = User.Identity.GetUserId<int>();
            var question = await db.Questions.FindAsync(id);
            var doc = await db.Docs.FindAsync(docId);

            question?.HiddenDocs.Add(doc);
            await db.SaveChangesAsync();
        }

        // Количество новых вопросов и информации
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task<string> CountInfoQuest()
        {
            var id = User.Identity.GetUserId<int>();
            var date = DateTime.Now.AddDays(-14);
            // Возвращаем количество неотвеченных вопросов и количество новой информации от админа
            var count = $"{await db.Questions.CountAsync(q => q.Reply == null && q.HiddenDocs.FirstOrDefault(d => d.Id == id) == null)};{await db.MessagesStaff.CountAsync(m => m.DateCreate > date)}";
            return count;
        }

        // Добавление ответа
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task Reply([Bind(Include = "Text,Id")] Reply Reply)
        {
            var docId = User.Identity.GetUserId<int>();
            var doc = await db.Docs.FindAsync(docId);
            var question = await db.Questions.FindAsync(Reply.Id);

            Reply.NameDoc = doc.Position + ", " + doc.FirstName + " " + doc.Name + " " + doc.LastName;
            Reply.DateCreate = DateTime.Now;
            db.Replies.Add(Reply);

            await db.SaveChangesAsync();

            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();

            msg.Subject = "Золотое Сердце. Ответ на вопрос.";

            msg.Body = $"<h2>{question.Name}, на Ваш вопрос был получен ответ.</h2>"
                     + "<h4>Ваш вопрос:</h4>"
                     + $"{question.Text}"
                     + $"<h4>Ответ дал - {Reply.NameDoc}:</h4>"
                     + $"{Reply.Text}";

            msg.From = new MailAddress("info@unk.im", "Золотое Сердце");
            msg.To.Add(question.Email);
            msg.IsBodyHtml = true;
            client.Host = "smtp.yandex.ru";
            NetworkCredential basicauthenticationinfo = new NetworkCredential("info@unk.im", "348191Zz");
            client.Port = 25;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicauthenticationinfo;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(msg);
        }

        // Изменение пароля, почты и телефона
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task<string> EditInfo(string OldPass, string NewPass, string ConfirmPass, string Phone, string Email)
        {
            var docId = User.Identity.GetUserId<int>();
            var doc = await db.Docs.FindAsync(docId);
            var returnString = "";
            
            // Мыло
            if (Email != null && Email != "" && Email != doc.Email)
            {
                if (Email.IndexOf('@') > -1 && Email.IndexOf('.') > -1)
                {
                    var emailConf = await db.Docs.FirstOrDefaultAsync(d => d.Email == Email);
                    if (emailConf == null)
                    {
                        doc.Email = Email;
                        returnString += "Почтовый адресс успешно изменен! ";
                    }
                }
                else
                    returnString += "Введите действующий почтовый адресс! ";
            }

            // Телефон
            if (Phone != null && Phone != "" && Phone != doc.Phone)
            {
                if (Phone.Length == 11)
                {
                    var phoneConf = await db.Docs.FirstOrDefaultAsync(d => d.Phone == Phone);
                    if (phoneConf == null)
                    {
                        doc.Phone = Phone;
                        returnString += "Телефонный номер успешно изменен! ";
                    }
                }
                else
                    returnString += "Проверьте правильность ввода телефонного номера, например - \"89123211212\"! ";
            }

            // Пасс
            if (OldPass != null && NewPass != null && ConfirmPass != null && OldPass != "" && NewPass != "" && ConfirmPass != "")
            {
                if (NewPass == ConfirmPass)
                {
                    OldPass = Hash.GetHashString(OldPass);
                    if (doc.HashPassword == OldPass)
                    {
                        NewPass = Hash.GetHashString(NewPass);
                        doc.HashPassword = NewPass;
                        returnString += "Пароль успешно изменен! ";
                    }
                    else
                        returnString += "Введенный Вами пароль не соответствует нынешнему! ";
                }
                else
                    returnString += "Новый пароль и пароль подтверждения отличаются! ";
            }
            
            await db.SaveChangesAsync();
            
            // Возвращаем ответ
            return returnString;
        }

        // Дисконектим базу
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