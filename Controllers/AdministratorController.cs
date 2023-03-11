using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GoldenHeart.Models;

namespace GoldenHeart.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // Время персонала
        //-------------------------------------------------------------------------
        public ActionResult Index()
        {
            ViewBag.dateReception = DateTime.Now;
            ViewBag.editAdmin = 1;
            return View(db.Docs.Where(d => d.Role.Name != "Admin").ToList());
        }

        // Персонал
        //-------------------------------------------------------------------------
        public ActionResult Staff()
        {
            return View(db.Docs.ToList());
        }

        // Частичное представление персонала
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> _Staff(string search)
        {
            // заглушка на налл
            if (search == null) return PartialView(await db.Docs.ToListAsync());

            // ищим докторов с информацией удовлетворяющей поиску
            var docs = await db.Docs.Where(d => d.Birthday.Contains(search) ||                      // Дата рождения
                                                d.Certificate.Contains(search) ||                   // Сертификат
                                                d.Education.Contains(search) ||                     // Образование
                                                d.Email.Contains(search) ||                         // Email
                                                d.FirstName.Contains(search) ||                     // Фамилия
                                                d.InstitutionWorks.Contains(search) ||              // Начало работы
                                                d.LastName.Contains(search) ||                      // Отчество
                                                d.Name.Contains(search) ||                          // Имя
                                                d.Phone.Contains(search) ||                         // Телефон
                                                d.Position.Contains(search)).ToListAsync();         // Должность

            // возвращаем частичное представление
            return docs.Count > 0 ? PartialView(docs) : PartialView(null);
        }

        // Добавление врача
        //-------------------------------------------------------------------------
        [HttpGet]
        public ActionResult CreateDoc()
        {
            ViewBag.Services = db.Services.ToList();
            return View();
        }

        // Добавление врача
        //-------------------------------------------------------------------------
        [HttpPost]
        public ActionResult CreateDoc(CreateDoc CreateDoc, HttpPostedFileBase ava)
        {
            // Проверяем на валидность
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Avatar", "Проверьте правильность введённых данных!");
                return View(CreateDoc);
            }

            // Проверяем на уникальность почты и телефона
            var email = db.Docs.FirstOrDefault(u => u.Email == CreateDoc.Email);
            var phone = db.Docs.FirstOrDefault(u => u.Phone == CreateDoc.Phone);
            var imgName = "add-doctor.png";

            if (email != null)
            {
                ModelState.AddModelError("Email", "Врач с таким почтовым адресом уже зарегистрирован на нашем сайте!");
                return View(CreateDoc);
            }

            if (phone != null)
            {
                ModelState.AddModelError("Phone", "Врач с таким номером телефона уже зарегистрирован на нашем сайте!");
                return View(CreateDoc);
            }

            if (CreateDoc.Pass != CreateDoc.PassConfirm)
            {
                ModelState.AddModelError("Pass", "Поля \"Пароль\" и \"Подтверждение пароля\" не совпадают!");
                return View(CreateDoc);
            }

            // Добавление аватарки
            if (ava != null)
            {
                // Получаем имя файла
                imgName = Path.GetFileName(ava.FileName);
                // если это картинка
                if (ava.ContentType.IndexOf("image") > -1)
                {
                    // Проверяем существование имени
                    imgName = InspectionPathImg(imgName, "~/img/avatars/");
                    // Сохраняем
                    ava.SaveAs(Server.MapPath("~/img/avatars/" + imgName));
                }
            }

            // Создаём врача
            var doc = new User()
            {
                Avatar = imgName,
                FirstName = CreateDoc.FirstName,
                Name = CreateDoc.Name,
                LastName = CreateDoc.LastName,
                Email = CreateDoc.Email,
                VisibleEmail = true,
                Phone = CreateDoc.Phone,
                VisiblePhone = true,
                Birthday = CreateDoc.Birthday,
                Certificate = CreateDoc.Certificate,
                Education = CreateDoc.Education,
                InstitutionWorks = CreateDoc.InstitutionWorks,
                Position = CreateDoc.Position,
                BeginLeave = DateTime.Now.AddDays(-1),
                EndLeave = DateTime.Now.AddDays(-1),
                HashPassword = Hash.GetHashString(CreateDoc.Pass),
                Role = db.Roles.FirstOrDefault(r => r.Name == "Doc"),
                Service = (CreateDoc.ServiceId != 0)
                    ? db.Services.FirstOrDefault(s => s.Id == CreateDoc.ServiceId)
                    : null
            };

            db.Docs.Add(doc);
            db.SaveChanges();

            return RedirectToAction("Staff", "Administrator");
        }

        // Добавление врача
        //-------------------------------------------------------------------------
        [HttpGet]
        public ActionResult EditDoc(int id)
        {
            var doc = db.Docs.Find(id);
            ViewBag.Services = db.Services.ToList();
            return View(doc);
        }

        // Добавление врача
        //-------------------------------------------------------------------------
        [HttpPost]
        public ActionResult EditDoc(EditDoc editDoc, HttpPostedFileBase ava)
        {
            var doc = db.Docs.Find(editDoc.Id);
            // Список сервисов
            ViewBag.Services = db.Services.ToList();

            // Не валидная
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Avatar", "Проверьте правильность введённых данных!");
                return View(doc);
            }

            // Проверяет существует-ли email или телефон
            var email = db.Docs.FirstOrDefault(u => u.Email == editDoc.Email);
            var phone = db.Docs.FirstOrDefault(u => u.Phone == editDoc.Phone);

            if (email != null && doc.Email != editDoc.Email)
            {
                ModelState.AddModelError("Email", "Врач с таким почтовым адресом уже зарегистрирован на нашем сайте!");
                return View(doc);
            }

            if (phone != null && doc.Phone != editDoc.Phone)
            {
                ModelState.AddModelError("Phone", "Врач с таким номером телефона уже зарегистрирован на нашем сайте!");
                return View(doc);
            }

            if (editDoc.Pass != editDoc.PassConfirm && (editDoc.Pass != null || editDoc.PassConfirm != null))
            {
                ModelState.AddModelError("HashPassword", "Поля \"Пароль\" и \"Подтверждение пароля\" не совпадают!");
                return View(doc);
            }

            // Изменение доктора
            if (doc != null)
            {
                doc.FirstName = editDoc.FirstName;
                doc.LastName = editDoc.LastName;
                doc.Name = editDoc.Name;
                doc.Education = editDoc.Education;
                doc.Certificate = editDoc.Certificate;
                doc.Position = editDoc.Position;
                doc.InstitutionWorks = editDoc.InstitutionWorks;
                doc.Phone = editDoc.Phone;
                doc.Email = editDoc.Email;
                doc.Birthday = editDoc.Birthday;

                if (editDoc.Pass != null && editDoc.PassConfirm != null && editDoc.Pass != "" &&
                    editDoc.PassConfirm != "" && editDoc.Pass == editDoc.PassConfirm)
                    doc.HashPassword = Hash.GetHashString(editDoc.Pass);

                doc.Service = (editDoc.ServiceId != 0)
                    ? db.Services.FirstOrDefault(s => s.Id == editDoc.ServiceId)
                    : null;

                // Добавление аватарки
                var imgName = Path.GetFileName(ava?.FileName);
                if (ava?.ContentType.IndexOf("image") > -1)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/img/avatars/" + doc.Avatar)) &&
                        doc.Avatar != "add-doctor.png")
                        System.IO.File.Delete(Server.MapPath("~/img/avatars/" + doc.Avatar));
                    imgName = InspectionPathImg(imgName, "~/img/avatars/");
                    ava.SaveAs(Server.MapPath("~/img/avatars/" + imgName));
                    doc.Avatar = imgName;
                }

                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("Avatar", "Проверьте правильность введённых данных!");
                return View(doc);
            }
            
            return RedirectToAction("Doctor", "Home", new { id = doc.Id });
        }

        // Пациенты
        //-------------------------------------------------------------------------
        public ActionResult Patients()
        {
            return View(db.Patients.ToList());
        }

        // Частичное представление пациентов
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> _Patients(string search)
        {
            // налл
            if (search == null) return PartialView(await db.Patients.ToListAsync());

            // Поиск по пациентам
            var patients = await db.Patients.Where(p => p.Name.Contains(search) || p.Phone.Contains(search)).OrderBy(p => p.Name).ToListAsync();

            return patients.Count > 0 ? PartialView(patients) : PartialView(null);
        }

        // Сообщения для персонала
        //-------------------------------------------------------------------------
        [HttpGet]
        public ActionResult MessageForStaff()
        {
            return View(db.MessagesStaff.OrderByDescending(m => m.DateCreate).ToList());
        }

        // Сообщения для персонала
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task MessageForStaff([Bind(Include = "Text")] MessageStaff MessageStaff)
        {
            MessageStaff.DateCreate = DateTime.Now;
            db.MessagesStaff.Add(MessageStaff);
            await db.SaveChangesAsync();
        }

        // Больничный
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task Hospital(int id)
        {
            var doc = await db.Docs.FindAsync(id);
            doc.Hospital = !doc.Hospital;
            await db.SaveChangesAsync();
        }

        // Отпуск
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task Leave(int id, string Begin, string End)
        {
            var doc = await db.Docs.FindAsync(id);
            var arr = Begin.Split('.');
            var begin = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));
            arr = End.Split('.');
            var end = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));

            if (doc != null)
            {
                doc.BeginLeave = begin;
                doc.EndLeave = end;
            }

            await db.SaveChangesAsync();
        }

        // Удаление времени для записи
        //-------------------------------------------------------------------------
        [HttpPost]
        public async Task DeleteTime(int id, string Date, string Time)
        {
            var doc = await db.Docs.FindAsync(id);
            var date = Date.Split('.');
            var time = Time.Split(':');
            var dateDel = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), int.Parse(time[0]), int.Parse(time[1]), 0);
            var confirmDateDel = await db.DeleteTime.FirstOrDefaultAsync(d => d.Time == dateDel);
            if (confirmDateDel == null)
            {
                db.DeleteTime.Add(new DeleteTime()
                {
                    Docs = doc,
                    Time = dateDel
                });
            }
            else
                db.DeleteTime.Remove(confirmDateDel);
            
            await db.SaveChangesAsync();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Функция проверки совпадения имени картинки
        //-------------------------------------------------------------------------
        public string InspectionPathImg(string fileNameImg, string pathDirectory)                                              // имя картинки, директория
        {
            if (fileNameImg != null && pathDirectory != null)                                                                   // значения присутствуют
            {
                var fileFlag = false;                                                                                          // флаг для выхода из цикла
                while (!fileFlag)
                {
                    if (System.IO.File.Exists(Server.MapPath(pathDirectory + fileNameImg)))                                     // проверяем наличие картинки
                    {                                                                                                           // если есть
                        var newFileName = "g";                                                                               // добавляем в начало названия картинки "g"
                        newFileName += fileNameImg;                                                                             //
                        fileNameImg = newFileName;                                                                              //
                    }
                    else                                                                                                        // иначе
                        fileFlag = true;                                                                                        // иначе флаг = true
                }
                return fileNameImg;                                                                                             // возвращаем имя файла
            }
            else                                                                                                              // иначе null
                return null;
        }// end inspectionPathImg
    }
}