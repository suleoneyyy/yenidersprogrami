using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YeniDersProgrami.Migrator
{
    [Migration(2)]
    public class _002_Time_Update : Migration
    {
        public override void Down()
        {
            Alter.Table("BaslangicSaati")
                .AlterColumn("Ad").AsInt32();

            Alter.Table("BaslangicSaati")
                .AlterColumn("Ad").AsInt32();
        }

        public override void Up()
        {
            Alter.Table("BaslangicSaati")
                .AlterColumn("Ad").AsString();

            Alter.Table("BitisSaati")
                .AlterColumn("Ad").AsString();
        }
    }
}