using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniDersProgrami.Models
{
    public class Bolum
    {
        public virtual int Id { get; set; }
        public virtual string Ad { get; set; }
        [ForeignKey("Fakulte")]
        public virtual int Fakulte { get; set; }
    }
    public class BolumMap:ClassMapping<Bolum>
    {
        public BolumMap()
        {
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.Ad, x => x.Column("Ad"));
            Property(x => x.Fakulte, x => x.Column("Fakulte"));
        }
    
    }

}