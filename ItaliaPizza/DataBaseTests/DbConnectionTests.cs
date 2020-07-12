using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseConnection;

namespace DataBase.Tests
{
    [TestClass()]
    public class DbConnectionTests
    {
        [TestMethod()]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void DbConnectionTest()
        {
            DbConnection dbConnection = new DbConnection();
        }
    }
}