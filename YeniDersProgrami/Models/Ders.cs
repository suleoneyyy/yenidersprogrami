using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniDersProgrami.Models
{
    public class Ders
    {
        public virtual int Id { get; set; }
        public virtual string Ad { get; set; }
        [ForeignKey("Bolum")]
        public virtual int Bolum { get; set; }
        [ForeignKey("Sinif")]
        public virtual int Sinif { get; set; }
    }
    public class DersMap:ClassMapping<Ders>
    {
        public DersMap()
        {
            Table("Ders");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.Ad, x => x.Column("Ad"));
            Property(x => x.Bolum, x => x.Column("Bolum"));
            Property(x => x.Sinif, x => x.Column("Sinif"));

        }
    }
}