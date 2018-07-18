using YeniDersProgrami;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YeniDersProgrami.Models;
using YeniDersProgrami.ViewModels;
using System.Web.UI.WebControls;

namespace YeniDersProgrami.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var fakulteler = Database.Session.Query<Fakulte>().ToList();
            var bolumler = Database.Session.Query<Bolum>().ToList();
            var siniflar = Database.Session.Query<Sinif>().ToList();

            Calendar cl = new Calendar();

            return View(new Anasayfa
            {
                Fakulteler = fakulteler,
                Bolumler = bolumler,
                Siniflar = siniflar
            });
        }

        [HttpPost]
        public ActionResult Index(Anasayfa form)
        {
            List<Ders> dersler = Database.Session.Query<Ders>().Where(x => x.Bolum == form.bolum && x.Sinif == form.sinif).ToList();
            var derslerId = new List<int>(); dersler.ForEach(x => derslerId.Add(x.Id));

            List<DersProgrami> programlar = Database.Session.Query<DersProgrami>().Where(x => derslerId.Contains(x.Ders)).ToList();

            var program = programlar.OrderBy(x => x.Gun).ThenBy(x => x.BaslangicSaati).ToList();
                
            return View("Goster", new DersTablosu().getList(program));
        }
        
        public ActionResult Goster()
        {
            List<DersTablosu> list = new List<DersTablosu>();
            return View(list);
        }

        [HttpPost]
        public ActionResult GetDDL(string Id)
        {
            List<SelectListItem> Bolumler = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Id))
            {
                int fakulteId = Convert.ToInt32(Id);
                var bolum = Database.Session.Query<Bolum>().Where(x => x.Fakulte == fakulteId).ToList();
                bolum.ForEach(x =>
                {
                    Bolumler.Add(new SelectListItem { Text = x.Ad, Value = x.Id.ToString() });
                });
            }
            return Json(Bolumler, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTimeDDL(string Gun, string Bolum, string Sinif)
        {
            List<SelectListItem> Baslangic = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(Gun) && !string.IsNullOrEmpty(Bolum) && !string.IsNullOrEmpty(Sinif))
            {
                int bolumId = Int32.Parse(Bolum); int gunId = Int32.Parse(Gun); int sinifId = Int32.Parse(Sinif);

                List<Ders> dersler = Database.Session.Query<Ders>().Where(x => x.Bolum == bolumId && x.Sinif == sinifId).ToList();
                var derslerId = new List<int>(); dersler.ForEach(x => derslerId.Add(x.Id));

                List<DersProgrami> programlar = Database.Session.Query<DersProgrami>().Where(x => derslerId.Contains(x.Ders) && x.Gun == gunId).ToList();
                List<int> silinecekBitis = new List<int>();
                programlar.ForEach(x => silinecekBitis.Add(x.BaslangicSaati));
                silinecekBitis.Distinct().ToList();

                var baslangicSaatleri = Database.Session.Query<BaslangicSaati>().ToList();

                baslangicSaatleri.ForEach(x =>
                {
                    if (!silinecekBitis.Contains(x.Id))
                    {
                        Baslangic.Add(new SelectListItem { Text = x.Ad, Value = x.Id.ToString() });
                    }
                });
            }
            return Json(Baslangic, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEndTimeDDL(string Gun, string Bolum, string Sinif)
        {
            List<SelectListItem> Bitis = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(Gun) && !string.IsNullOrEmpty(Bolum) && !string.IsNullOrEmpty(Sinif))
            {
                int bolumId = Int32.Parse(Bolum); int gunId = Int32.Parse(Gun); int sinifId = Int32.Parse(Sinif);

                List<Ders> dersler = Database.Session.Query<Ders>().Where(x => x.Bolum == bolumId && x.Sinif == sinifId).ToList();
                var derslerId = new List<int>(); dersler.ForEach(x => derslerId.Add(x.Id));

                List<DersProgrami> programlar = Database.Session.Query<DersProgrami>().Where(x => derslerId.Contains(x.Ders) && x.Gun == gunId).ToList();
                List<int> silinecekBitis = new List<int>();
                programlar.ForEach(x => silinecekBitis.Add(x.BitisSaati));
                silinecekBitis.Distinct().ToList();

                var bitisSaatleri = Database.Session.Query<BitisSaati>().ToList();

                bitisSaatleri.ForEach(x =>
                {
                    if (!silinecekBitis.Contains(x.Id))
                    {
                        Bitis.Add(new SelectListItem { Text = x.Ad, Value = x.Id.ToString() });
                    }
                });
            }
            return Json(Bitis, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View(new Admin());
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            if (admin.Name != "Admin" || admin.Password != "Admin")
                ModelState.AddModelError("Bilgiler Hatalıdır", "Kullanıcı Adı ya da Sifre hatalıdır");
            if (!ModelState.IsValid)
            {
                return View(admin);
            }

            FormsAuthentication.SetAuthCookie(admin.Name, true);


            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Add()
        {
            var fakulteler = Database.Session.Query<Fakulte>().ToList();
            var bolumler = Database.Session.Query<Bolum>().ToList();
            var siniflar = Database.Session.Query<Sinif>().ToList();
            var gunler = Database.Session.Query<Gunler>().ToList();
            var baslangic = Database.Session.Query<BaslangicSaati>().ToList();
            var bitis = Database.Session.Query<BitisSaati>().ToList();
            return View(new DersEkle {
                Fakulteler = fakulteler,
                Bolumler = bolumler,
                Siniflar = siniflar,
                Gunler = gunler,
                BaslangicSaati = baslangic,
                BitisSaati = bitis
            });
        }

        public ActionResult Delete(int id)
        {
            var program = Database.Session.Load<DersProgrami>(id);

            if (program == null)
                return HttpNotFound();

            Database.Session.Delete(program);
            Database.Session.Flush();
            return RedirectToAction("index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(DersEkle dersEkle)
        {

            if (!ModelState.IsValid)
            {
                return View(dersEkle);
            }


            Ders ders = new Ders
            {
                Ad = dersEkle.DersAdi,
                Bolum = dersEkle.bolum,
                Sinif = dersEkle.sinif
            };
            using (var txn = Database.Session.BeginTransaction())
            {
                Database.Session.SaveOrUpdate(ders);
                txn.Commit();
            }

            int baslangic = Int32.Parse(Database.Session.Query<BaslangicSaati>().Where(x => x.Id == dersEkle.baslangic).FirstOrDefault().Ad.Split(':')[0]);
            int bitis = Int32.Parse(Database.Session.Query<BitisSaati>().Where(x => x.Id == dersEkle.bitis).FirstOrDefault().Ad.Split(':')[0]);

            DersProgrami program = new DersProgrami
            {
                Ders = ders.Id,
                Gun = dersEkle.gun,
                BaslangicSaati = dersEkle.baslangic              
            };
            string baslangiString = "";

            while(baslangic < bitis)
            {
                DersProgrami yeniProgram = program.clone();
                string bitisString = Convert.ToString(baslangic + 1) + ":20"; if (bitisString.Length < 5) bitisString = "0" + bitisString;
                int bitisSaati = Database.Session.Query<BitisSaati>().Where(x => x.Ad == bitisString).FirstOrDefault().Id;
                yeniProgram.BitisSaati = bitisSaati;

                Database.Session.Save(yeniProgram);
                Database.Session.Flush();

                baslangiString = Convert.ToString(++baslangic) + ":30"; if (baslangiString.Length < 5) baslangiString = "0" + baslangiString;
                if (baslangic == 12)
                    continue;
                program.BaslangicSaati = Database.Session.Query<BaslangicSaati>().Where(x => x.Ad == baslangiString).FirstOrDefault().Id;
                Database.Session.Evict(yeniProgram);
            } 

            return RedirectToAction("Index");
        }


    }
}