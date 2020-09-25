using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TTLX.DataBase.Entity;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            using (DbSqlite db = DbSqlite.Instance)
            {
                var temp = db.PaperRecord.ToList();
            }

        }
    }
}
