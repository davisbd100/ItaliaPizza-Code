using System;
using DatabaseConnection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseConnectionTest
{
    [TestClass]
    public class DatabaseConnectionTest
    {
        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void DbConnectionTest()
        {
            DbConnection dbConnection = new DbConnection();
        }
    }
}
