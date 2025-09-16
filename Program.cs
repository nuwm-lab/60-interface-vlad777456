using System;

namespace PracticantWorkerApp
{
    // Базовий клас Практикант
    class Practicant
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string University { get; set; }

        // Метод для задання даних
        public virtual void SetData(string lastName, string firstName, string university)
        {
            LastName = lastName;
            FirstName = firstName;
            University = university;
        }

        // Метод для перевірки, чи є прізвище симетричним (паліндромом)
        public virtual bool IsLastNameSymmetric()
        {
            string lower = LastName.ToLower();
            char[] reversed = lower.ToCharArray();
            Array.Reverse(reversed);
            return lower == new string(reversed);
        }

        // Вивід інформації
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Практикант: {FirstName} {LastName}, ВУЗ: {University}");
        }
    }

    // Похідний клас Працівник фірми
    class Worker : Practicant
    {
        public DateTime HireDate { get; set; }
        public string GraduatedUniversity { get; set; }
        public string Position { get; set; }

        // Перевантажений метод для задання даних
        public void SetData(string lastName, string firstName, string graduatedUniversity, string position, DateTime hireDate)
        {
            LastName = lastName;
            FirstName = firstName;
            GraduatedUniversity = graduatedUniversity;
            Position = position;
            HireDate = hireDate;
        }

        // Метод для обчислення стажу роботи
        public int GetExperience()
        {
            DateTime now = DateTime.Now;
            int years = now.Year - HireDate.Year;
            if (now.Month < HireDate.Month || (now.Month == HireDate.Month && now.Day < HireDate.Day))
                years--;
            return years;
        }

        // Вивід інформації
        public override void DisplayInfo()
        {
            Console.WriteLine($"Працівник: {FirstName} {LastName}, Посада: {Position}, ВУЗ: {GraduatedUniversity}, Дата прийому: {HireDate.ToShortDateString()}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення об'єкта Практикант
            Practicant pract = new Practicant();
            pract.SetData("Anna", "Petrova", "Національний університет");
            pract.DisplayInfo();
            Console.WriteLine($"Прізвище симетричне: {pract.IsLastNameSymmetric()}\n");

            // Створення об'єкта Працівник
            Worker worker = new Worker();
            worker.SetData("Oleh", "Ivanenko", "Київський політехнічний інститут", "Програміст", new DateTime(2018, 5, 10));
            worker.DisplayInfo();
            Console.WriteLine($"Стаж роботи: {worker.GetExperience()} років");
            Console.WriteLine($"Прізвище симетричне: {worker.IsLastNameSymmetric()}");
        }
    }
}
