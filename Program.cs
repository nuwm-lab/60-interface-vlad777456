using System;

namespace PracticantWorkerApp
{
    // ---------------- Базовий клас "Практикант" ----------------
    class Practicant
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string University { get; set; }

        /// <summary>
        /// Задання даних практиканта через консольний ввід.
        /// </summary>
        public virtual void SetData()
        {
            Console.Write("Введіть прізвище практиканта: ");
            LastName = Console.ReadLine();

            Console.Write("Введіть ім'я практиканта: ");
            FirstName = Console.ReadLine();

            Console.Write("Введіть назву ВУЗу: ");
            University = Console.ReadLine();
        }

        /// <summary>
        /// Перевірка, чи є прізвище симетричним (паліндромом).
        /// </summary>
        public virtual bool IsLastNameSymmetric()
        {
            if (string.IsNullOrWhiteSpace(LastName)) return false;

            string lower = LastName.ToLower();
            char[] reversed = lower.ToCharArray();
            Array.Reverse(reversed);

            return lower == new string(reversed);
        }

        /// <summary>
        /// Вивід інформації про практиканта.
        /// </summary>
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

        /// <summary>
        /// Перевизначений метод для задання даних працівника.
        /// Викликає базовий метод для ПІБ, а також додає специфічні поля.
        /// </summary>
        public override void SetData()
        {
            base.SetData(); // виклик базового методу для задання прізвища, імені та ВУЗу

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
                    if (hireDate <= DateTime.Now)
                    {
                        HireDate = hireDate;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("❌ Дата найму не може бути у майбутньому.");
                    }
                }
                else
                {
                    Console.WriteLine("❌ Невірний формат дати. Спробуйте ще раз.");
                }
            }
        }

        /// <summary>
        /// Обчислення стажу роботи у роках.
        /// </summary>
        public int GetExperience()
        {
            DateTime now = DateTime.Now;
            int years = now.Year - HireDate.Year;

            // Якщо ще не пройшов день народження стажу у цьому році – відняти 1
            if (now.Month < HireDate.Month || (now.Month == HireDate.Month && now.Day < HireDate.Day))
                years--;

            return years >= 0 ? years : 0;
        }

        /// <summary>
        /// Вивід інформації про працівника.
        /// </summary>
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
