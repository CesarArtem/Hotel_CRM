using Hotel;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace HotelTest
{
    public class Tests
    {

        [Test]
        public void TestAuthorization()
        {
            TestClass test = new TestClass();
            
            Assert.AreEqual(true, test.Authorization("Admin", "123"));
        }

        [Test]
        public void TestAddEditUser()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditUserTest("Admin", "123", 1, 1));
        }

        [Test]
        public void TestAddEditNomer()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditNomerTest("12", 1));
        }

        [Test]
        public void TestAddEditBook()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditBookTest(DateTime.Now.AddDays(2), DateTime.Now.AddDays(6), 1, 1));
        }

        [Test]
        public void TestAddEditRoomType()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditRoomTypeTest("Комфорт"));
        }

        [Test]
        public void TestAddEditPriem()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditPriemTest(DateTime.Now.Date, 1, 674));
        }

        [Test]
        public void TestAddEditClient()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditClientTest("Иванов", "Иван", "Иванович", DateTime.Now.AddYears(-20), "4564", "721834"));
        }

        [Test]
        public void TestAddEditFood()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditFoodTest("Капуста", 1));
        }

        [Test]
        public void TestAddEditDish()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditDishTest("Капуста квашенная", "25"));
        }

        [Test]
        public void TestAddEditMenuType()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditMenuTypeTest("Мясное", DateTime.Now.Date, 2));
        }

        [Test]
        public void TestAddEditFridge()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditFridgeTest("Запасная морозилка"));
        }

        [Test]
        public void TestAddEditCleaning()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditCleaningTest(DateTime.Now.Date, 14, 8));
        }

        [Test]
        public void TestAddEditSotrudnik()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditSotrudnikTest("Иванов", "Иван", "Иванович", DateTime.Now.AddYears(-20)));
        }

        [Test]
        public void TestAddEditDoljnost()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditDoljnostTest("Бухгалтер", "37824"));
        }

        [Test]
        public void TestAddEditSklad()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditSkladTest("Запасной склад"));
        }

        [Test]
        public void TestAddEditEquipment()
        {
            TestClass test = new TestClass();

            Assert.AreEqual(true, test.AddEditEquipmentTest("Средство для мытья полов", 1));
        }
    }
}