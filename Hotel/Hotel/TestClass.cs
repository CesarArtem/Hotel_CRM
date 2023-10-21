using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hotel
{
    public class TestClass
    {
        public bool Authorization(string login, string password)
        {
            int score = 0;
            if (login.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(login, @"[а-яА-Я]"))
            {
                score += 1;
            }
            if (login.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(login, @"[!-/]"))
            {
                score += 1;
            }
            if (password.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(password, @"[а-яА-Я]"))
            {
                score += 1;
            }
            if (password.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(password, @"[!-/]"))
            {
                score += 1;
            }
            if (score == 8)
                return true;
            else
                return false;
        }
        public bool AddEditUserTest (string login, string password, 
            int roleid, int sotrudnikid)
        {
            int score = 0;
            if (login.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(login, @"[а-яА-Я]"))
            {
                score += 1;
            }
            if (login.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(login, @"[!-/]"))
            {
                score += 1;
            }
            if (password.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(password, @"[а-яА-Я]"))
            {
                score += 1;
            }
            if (password.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(password, @"[!-/]"))
            {
                score += 1;
            }
            if (roleid > 0)
                score += 1;
            if (sotrudnikid > 0)
                score += 1;
            if (score == 10)
                return true;
            else
                return false;
        }

        public bool AddEditNomerTest(string number, int typeid)
        {
            int score = 0;
            int number2 = 0;
            try
            {
                number2 = Convert.ToInt32(number);
                score += 1;
            }
            catch { }
            if (number2.ToString().Length <= 4)
            {
                score += 1;
            }
            if (number.Length > 0)
            {
                score += 1;
            }
            if (typeid > 0)
                score += 1;
            if (number2 > 0)
                score += 1;
            if (score == 5)
                return true;
            else
                return false;
        }

        public bool AddEditBookTest(DateTime datestart, DateTime dateend, 
            int nomerid, int clientid)
        {
            int score = 0;
            if (datestart.Date >= DateTime.Now.Date.AddDays(-1))
            {
                score += 1;
            }
            if (dateend.Date > DateTime.Now.Date)
            {
                score += 1;
            }
            if (datestart.Date< dateend.Date)
                score += 1;
            if (nomerid > 0)
                score += 1;
            if (clientid > 0)
                score += 1;
            if (score == 5)
                return true;
            else
                return false;
        }

        public bool AddEditRoomTypeTest(string name)
        {
            int score = 0;
            if (name.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (name.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[!-/]"))
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[0-9]"))
            {
                score += 1;
            }
            if (score == 5)
                return true;
            else
                return false;
        }

        public bool AddEditPriemTest(DateTime date, int menudate, int nomer)
        {
            int score = 0;
            if (date.Date >= DateTime.Now.Date.AddDays(-1))
            {
                score += 1;
            }
            if (menudate > 0)
                score += 1;
            if (nomer > 0)
                score += 1;
            if (score == 3)
                return true;
            else
                return false;
        }

        public bool AddEditClientTest(string surname, string name, 
            string otchestvo, DateTime daterozhdeniya, string seriapas, string numberpas)
        {
            int score = 0;
            int seriapas2 = 0;
            int numberpas2 = 0;
            if (surname.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(surname, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (surname.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(surname, @"[!-/]"))
            {
                score += 1;
            }
            if (!Regex.IsMatch(surname, @"[0-9]"))
            {
                score += 1;
            }
            if (!surname.Contains(" "))
            {
                score += 1;
            }

            if (name.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (name.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[!-/]"))
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[0-9]"))
            {
                score += 1;
            }
            if (!name.Contains(" "))
            {
                score += 1;
            }

            if (otchestvo.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(otchestvo, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (!Regex.IsMatch(otchestvo, @"[!-/]"))
            {
                score += 1;
            }
            if (!Regex.IsMatch(otchestvo, @"[0-9]"))
            {
                score += 1;
            }
            if (!otchestvo.Contains(" "))
            {
                score += 1;
            }

            if (daterozhdeniya.Date <= DateTime.Now.Date.AddYears(-18))
            {
                score += 1;
            }

            try
            {
                seriapas2 = Convert.ToInt32(seriapas);
                score += 1;
            }
            catch { }
            if (seriapas2.ToString().Length == 4)
            {
                score += 1;
            }

            try
            {
                numberpas2 = Convert.ToInt32(numberpas);
                score += 1;
            }
            catch { }
            if (numberpas2.ToString().Length == 6)
            {
                score += 1;
            }
            if (score == 22)
                return true;
            else
                return false;
        }

        public bool AddEditFoodTest(string name, int fridgeid)
        {
            int score = 0;
            if (name.Length <= 30)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (name.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[!-/]"))
            {
                score += 1;
            }
            if (name.Length <= 15)
            {
                score += 1;
            }
            
            if (fridgeid > 0)
                score += 1;
            if (score == 6)
                return true;
            else
                return false;
        }

        public bool AddEditDishTest(string name, string weight)
        {
            int score = 0;
            int weight2 = 0;
            if (name.Length <= 30)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (name.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[!-/]"))
            {
                score += 1;
            }
            try
            {
                weight2 = Convert.ToInt32(weight);
                score += 1;
            }
            catch { }
            
            if (weight2 > 0)
                score += 1;
            if (score == 6)
                return true;
            else
                return false;
        }

        public bool AddEditMenuTypeTest(string name, DateTime date, int typeid)
        {
            int score = 0;
            if (name.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (name.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[!-/]"))
            {
                score += 1;
            }

            if (date.Date >= DateTime.Now.Date.AddDays(-1))
            {
                score += 1;
            }
           
            if (typeid > 0)
                score += 1;
            if (score == 6)
                return true;
            else
                return false;
        }

        public bool AddEditFridgeTest(string name)
        {
            int score = 0;
            if (name.Length <= 30)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (name.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[!-/]"))
            {
                score += 1;
            }
            if (score == 4)
                return true;
            else
                return false;
        }

        public bool AddEditCleaningTest(DateTime date, int sotrudnikid, int nomerid)
        {
            int score = 0;
            if (date.Date >= DateTime.Now.Date.AddDays(-1))
            {
                score += 1;
            }
            if (nomerid > 0)
                score += 1;
            if (sotrudnikid > 0)
                score += 1;
            if (score == 3)
                return true;
            else
                return false;
        }

        public bool AddEditSotrudnikTest(string surname, string name,
            string otchestvo, DateTime daterozhdeniya)
        {
            int score = 0;
            if (surname.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(surname, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (surname.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(surname, @"[!-/]"))
            {
                score += 1;
            }
            if (!Regex.IsMatch(surname, @"[0-9]"))
            {
                score += 1;
            }
            if (!surname.Contains(" "))
            {
                score += 1;
            }

            if (name.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (name.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[!-/]"))
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[0-9]"))
            {
                score += 1;
            }
            if (!name.Contains(" "))
            {
                score += 1;
            }

            if (otchestvo.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(otchestvo, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (!Regex.IsMatch(otchestvo, @"[!-/]"))
            {
                score += 1;
            }
            if (!Regex.IsMatch(otchestvo, @"[0-9]"))
            {
                score += 1;
            }
            if (!otchestvo.Contains(" "))
            {
                score += 1;
            }

            if (daterozhdeniya.Date <= DateTime.Now.Date.AddYears(-18))
                score += 1;

            if (score == 18)
                return true;
            else
                return false;
        }

        public bool AddEditDoljnostTest(string name, string salary)
        {
            int score = 0;
            decimal salary2=0;
            if (name.Length <= 15)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (name.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[!-/]"))
            {
                score += 1;
            }
            try
            {
                salary2 = Convert.ToDecimal(salary);
                score += 1;
            }
            catch { }
            if (salary2 >= 0)
                score += 1;
            if (score == 6)
                return true;
            else
                return false;
        }

        public bool AddEditSkladTest(string name)
        {
            int score = 0;
            if (name.Length <= 30)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (name.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[!-/]"))
            {
                score += 1;
            }
            if (score == 4)
                return true;
            else
                return false;
        }

        public bool AddEditEquipmentTest(string name, int skladid)
        {
            int score = 0;
            if (name.Length <= 30)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[a-zA-Z]"))
            {
                score += 1;
            }
            if (name.Length > 0)
            {
                score += 1;
            }
            if (!Regex.IsMatch(name, @"[!-/]"))
            {
                score += 1;
            }
            if (skladid > 0)
                score += 1;
            if (score == 5)
                return true;
            else
                return false;
        }
    }
}
