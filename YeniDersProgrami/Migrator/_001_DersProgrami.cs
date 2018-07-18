using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace YeniDersProgrami.Migrator
{
    [Migration(1)]
    public class _001_DersProgrami :Migration
    {
        public override void Down()
        {
            Delete.Table("DersProgrami");
            Delete.Table("Ders");
            Delete.Table("Sinif");
            Delete.Table("Gunler");
            Delete.Table("Bolum");
            Delete.Table("Fakulte");
            Delete.Table("BaslangicSaati");
            Delete.Table("BitisSaati");
        }
        public override void Up()
        {
            Create.Table("Sinif")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Ad").AsString();
            Create.Table("Gunler")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Ad").AsString();
            Create.Table("Fakulte")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Ad").AsString();
            Create.Table("Bolum")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Ad").AsString()
                .WithColumn("Fakulte").AsInt32().ForeignKey("Fakulte", "Id").OnDelete(System.Data.Rule.Cascade);
            Create.Table("BaslangicSaati")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Ad").AsInt32();
            Create.Table("BitisSaati")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Ad").AsInt32();
            Create.Table("Ders")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Ad").AsString()
                .WithColumn("Bolum").AsInt32().ForeignKey("Bolum", "Id").OnDelete(System.Data.Rule.Cascade)
                .WithColumn("Sinif").AsInt32().ForeignKey("Sinif", "Id").OnDelete(System.Data.Rule.Cascade);
            Create.Table("DersProgrami")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Ders").AsInt32().ForeignKey("Ders", "Id").OnDelete(System.Data.Rule.Cascade)
                .WithColumn("Gun").AsInt32().ForeignKey("Gunler", "Id").OnDelete(System.Data.Rule.Cascade)
                .WithColumn("Baslangic").AsInt32().ForeignKey("BaslangicSaati", "Id").OnDelete(System.Data.Rule.Cascade)
                .WithColumn("Bitis").AsInt32().ForeignKey("BitisSaati", "Id").OnDelete(System.Data.Rule.Cascade);

        }
    }
}