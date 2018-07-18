using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniDersProgrami.Models
{
    public class DersProgrami
    {
        public virtual int Id { get; set; }
        [ForeignKey("Dersler")]
        public virtual int Ders { get; set; }
        [ForeignKey("Gunler")]
        public virtual int Gun { get; set; }
        [ForeignKey("BaslangicSaati")]
        public virtual int BaslangicSaati { get; set; }
        [ForeignKey("BitisSaati")]
        public virtual int BitisSaati { get; set; }

        public virtual DersProgrami clone()
        {
            DersProgrami nesne = new DersProgrami();
            nesne.Ders = this.Ders;
            nesne.Gun = this.Gun;
            nesne.BaslangicSaati = this.BaslangicSaati;
            nesne.BitisSaati = this.BitisSaati;

            return nesne;
        }
    }
    public class DersProgramiMap:ClassMapping<DersProgrami>
    {
        public DersProgramiMap()
        {
            Table("DersProgrami");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.Ders, x => x.Column("Ders"));
            Property(x => x.Gun, x => x.Column("Gun"));
            Property(x => x.BaslangicSaati, x => x.Column("Baslangic"));
            Property(x => x.BitisSaati, x => x.Column("Bitis"));
        }
    }
}