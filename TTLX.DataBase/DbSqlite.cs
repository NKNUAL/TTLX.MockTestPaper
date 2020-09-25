namespace TTLX.DataBase.Entity
{
    using SQLite.CodeFirst;
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.SQLite.EF6;
    using System.IO;
    using System.Linq;

    public class DbSqlite : DbContext
    {

        private DbSqlite(DbConnection con) : base(con, true) { }
        public static DbSqlite Instance
        {
            get
            {
                DbConnection sqliteCon = SQLiteProviderFactory.Instance.CreateConnection();
                sqliteCon.ConnectionString =
                    "Data Source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "record.db");
                return new DbSqlite(sqliteCon);
            }
        }
        public virtual DbSet<PaperQuestionRelation> PaperQuestionRelation { get; set; }
        public virtual DbSet<PaperRecord> PaperRecord { get; set; }
        public virtual DbSet<QuestionInfo> QuestionInfo { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new SqliteCreateDatabaseIfNotExists<DbSqlite>(modelBuilder));
        }
    }

}