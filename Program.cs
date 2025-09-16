using System;

namespace PracticantWorkerApp
{
    // ---------------- Базовий клас "Практикант" ----------------
    class Practicant
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string University { get; set; }

        // Метод для задання даних вручну
        public virtual void SetData()
        {
            Console.Write("Введіть прізвище практиканта: ");
            LastName = Console.ReadLine();

            Console.Write("Введіть ім'я практиканта: ");
            FirstName = Console.ReadLine();

            Console.Write("Введіть назву ВУЗу: ");
            University = Console.ReadLine();
        }

        // Метод для перевірки, чи є прізвище симетричним (паліндром)
        public virtual bool IsLastNameSymmetric()
        {
            if (string.IsNullOrWhiteSpace(LastName)) return false;

            string lower = LastName.ToLower();
            char[] reversed = lower.ToCharArray();
            Array.Reverse(reversed);

            return lower == new string(reversed);
        }

        // Метод для виводу інформації
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"\nПрактикант: {FirstName} {LastName}");
            Console.WriteLine($"ВУЗ: {University}");
        }
    }

    // ---------------- Похідний клас "Працівник фірми" ----------------
    class Worker : Practicant
    {
        public DateTime HireDate { get; set; }
        public string GraduatedUniversity { get; set; }
        public string Position { get; set; }

        // Перевантажений метод для задання даних вручну
        public override void SetData()
        {
            Console.Write("Введіть прізвище працівника: ");
            LastName = Console.ReadLine();

            Console.Write("Введіть ім'я працівника: ");
            FirstName = Console.ReadLine();

            Console.Write("Введіть навчальний заклад, який закінчив: ");
            GraduatedUniversity = Console.ReadLine();

            Console.Write("Введіть посаду: ");
            Position = Console.ReadLine();

            // Обробка виключень при введенні дати
            while (true)
            {
                Console.Write("Введіть дату прийому на роботу (рррр-мм-дд): ");
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out DateTime hireDate))
                {
                    HireDate = hireDate;
                    break;
                }
                else
                {
                    Console.WriteLine("❌ Невірний формат дати. Спробуйте ще раз.");
                }
            }
        }

        // Метод для обчислення стажу роботи у роках
        public int GetExperience()
        {
            DateTime now = DateTime.Now;
            int years = now.Year - HireDate.Year;

            if (now.Month < HireDate.Month || (now.Month == HireDate.Month && now.Day < HireDate.Day))
                years--;

            return years >= 0 ? years : 0;
        }

        // Метод для виводу інформації
        public override void DisplayInfo()
        {
            Console.WriteLine($"\nПрацівник: {FirstName} {LastName}");
            Console.WriteLine($"Посада: {Position}");
            Console.WriteLine($"ВУЗ (закінчений): {GraduatedUniversity}");
            Console.WriteLine($"Дата прийому: {HireDate.ToShortDateString()}");
            Console.WriteLine($"Стаж роботи: {GetExperience()} років");
        }
    }

    // ---------------- Головна програма ----------------
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Створення практиканта
                Practicant pract = new Practicant();
                pract.SetData();
                pract.DisplayInfo();
                Console.WriteLine($"Прізвище симетричне: {pract.IsLastNameSymmetric()}\n");

                // Створення працівника
                Worker worker = new Worker();
                worker.SetData();
                worker.DisplayInfo();
                Console.WriteLine($"Прізвище симетричне: {worker.IsLastNameSymmetric()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Сталася помилка: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}
