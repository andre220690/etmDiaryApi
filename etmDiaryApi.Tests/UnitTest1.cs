using etmDiaryApi.Controllers;
using etmDiaryApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace etmDiaryApi.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private DiaryController controller = new DiaryController(new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()));


        [TestMethod]
        public void TestTasksOnlyData()
        {
            string date1 = "01/01/2023";
            string date2 = "01/03/2023";

            var res = controller.GetTasks(date1, date2, null, null, null).Result.GetEnumerator();
            int count = 0;

            while (res.MoveNext())
            {
                count++;
            }

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void TestTasksUser()
        {
            string date1 = "01/01/2023";
            string date2 = "01/03/2023";
            int userCode = 111111;

            var res = controller.GetTasks(date1, date2, null, userCode, null).Result.GetEnumerator();
            int count = 0;

            while (res.MoveNext())
            {
                count++;
            }

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void TestTasksId()
        {
            int id = 1;
            var res = controller.GetTask(id).Result.ToString();
            Assert.IsTrue(string.Equals(res, "{ Id = 1, Start = 01.01.2023, End = 31.01.2023, Priority = 1, Theme = Освоение потенциала клиента, Partner = ООО Восход, User = Попов В.В., Description = Заведение новой продукции в ассортимент клиента, Condition = Назначено, Result =  }"));
        }


        [TestMethod]
        public void TestGetUser()
        {
            string line = "бар";

            var res = controller.GetUsers(line).Result.GetEnumerator();

            res.MoveNext();
            string user = res.Current.ToString();

            Assert.AreEqual(user, "{ UserName = Баранов А.А., Code = 222222 }");
        }

        [TestMethod]
        public void TestGetDepartment()
        {
            string line = "ОП";

            var res = controller.GetDepartments(line).Result.GetEnumerator();

            int count = 0;

            while (res.MoveNext())
            {
                count++;
            }

            Assert.IsTrue(count == 2);
        }

        [TestMethod]
        public void TestGetPartner()
        {
            string line = "ООО";
            var res = controller.GetPartners(line).Result.GetEnumerator();
            int count = 0;
            while (res.MoveNext())
            {
                count++;
            }
            Assert.IsTrue(count == 2);
        }

        [TestMethod]
        public void TestGetThemes()
        {
            var res = controller.GetThemes().Result.GetEnumerator();

            int count = 0;

            while (res.MoveNext())
            {
                count++;
            }

            Assert.IsTrue(count == 3);
        }

        [TestMethod]
        public void TestGetConditions()
        {
            var res = controller.GetConditions().Result.GetEnumerator();
            int count = 0;

            while (res.MoveNext())
            {
                count++;
            }
            Assert.IsTrue(count > 0);

        }

        [TestMethod]
        public void TestGetHistory()
        {
            var res = controller.GetHistory(1).Result.ToString().Split('|');
            Assert.IsTrue(res.Length > 2);
        }

        //[TestMethod]
        //public void TestPostSaveTask()
        //{

        //}

        //[TestMethod]
        //public void TestGetAddHistory()
        //{

        //}

        //[TestMethod]
        //public void TestGetCanban()
        //{

        //}

        //[TestMethod]
        //public void TestGetSticks()
        //{

        //}

        //[TestMethod]
        //public void TestGetSticksOnExpress()
        //{

        //}

        //[TestMethod]
        //public void TestPostAddSampleAndSticks()
        //{

        //}

        //[TestMethod]
        //public void TestPostAddStick()
        //{

        //}

        //[TestMethod]
        //public void TestGetStickRefrash()
        //{

        //}

        //[TestMethod]
        //public void TestGetReport()
        //{

        //}

        //[TestMethod]
        //public void TestGetFavorits()
        //{

        //}

        //[TestMethod]
        //public void TestGetAddFavoritTask()
        //{

        //}

        //[TestMethod]
        //public void TestGetAddFavoritStick()
        //{

        //}

    }
}