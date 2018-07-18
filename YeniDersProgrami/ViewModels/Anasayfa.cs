using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YeniDersProgrami.Models;

namespace YeniDersProgrami.ViewModels
{
    public class Anasayfa
    {
        public List<Fakulte> Fakulteler { get; set; }
        public List<Bolum> Bolumler { get; set; }
        public List<Sinif> Siniflar { get; set; }

        [Required]
        public int fakulte { get; set; }
        [Required]
        public int bolum { get; set; }
        [Required]
        public int sinif { get; set; }


    }

    public class DersEkle
    {
        public List<Fakulte> Fakulteler { get; set; }
        public List<Bolum> Bolumler { get; set; }
        public List<Sinif> Siniflar { get; set; }
        public List<Gunler> Gunler { get; set; }
        public List<BaslangicSaati> BaslangicSaati { get; set; }
        public List<BitisSaati> BitisSaati { get; set; }
        public string DersAdi { get; set; }

        public int fakulte { get; set; }
        public int bolum { get; set; }
        public int sinif { get; set; }
        public int gun { get; set; }
        public int baslangic { get; set; }
        public int bitis { get; set; }
    }

    public class DersTablosu
    {
        public int Id { get; set; }
        public string DersAdi { get; set; }
        public string Gun { get; set; }
        public string Baslangic { get; set; }
        public string Bitis { get; set; }

        public List<DersTablosu> getList(List<DersProgrami> dersler)
        {
            var results = new List<DersTablosu>();
            foreach (var ders in dersler)
            {
                var item = new DersTablosu();
                item.Id = ders.Id;
                item.DersAdi = Database.Session.Query<Ders>().Where(x => x.Id == ders.Ders).First().Ad;
                item.Gun = Database.Session.Query<Gunler>().Where(x => x.Id == ders.Gun).First().Ad;
                item.Baslangic = Database.Session.Query<BaslangicSaati>().Where(x => x.Id == ders.BaslangicSaati).First().Ad;
                item.Bitis = Database.Session.Query<BitisSaati>().Where(x => x.Id == ders.BitisSaati).First().Ad;

                results.Add(item);
            }
            return results;
        }
    }
}