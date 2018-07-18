﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YeniDersProgrami.Models
{
    public class Gunler
    {
        public virtual int Id { get; set; }
        public virtual string Ad { get; set; }
    }
    public class GunlerMap : ClassMapping<Gunler>
    {
        public GunlerMap()
        {
            Table("Gunler");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.Ad, x => x.Column("Ad"));
        }
    }
}