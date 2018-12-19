using System;
using System.Text;
using ClassLibrary1;

namespace easy_Poker
{
    class Program
    {
		static bool	Read_Answer(string s) // Интерфейсная функция: задаёт вопрос и принимает ответы
		{						// принимает текст вопроса, 
			Console.WriteLine(s); // возвращает ответ типа bool: true/false
			while (true)
			{
				string str = Console.ReadLine();
				str = str.ToLower();
				if (str == "1" || str == "yes" || str == "да" || str == "y" || str == "д")
					return (true);
				if (str == "2" || str == "no" || str == "нет" || str == "n" || str == "н")
					return (false);
			}
		}
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.Unicode;
			while (Read_Answer("Хотите сыграть?\n1. Да\n2. Нет")) // Повторяем вопрос, пока пользователь не захочет перестать играть 
			{
				Deck d = new Deck(); // Создаём экземпляр класса колода
				d.Shuffle(); // Взбалтываем
				Hand h = new Hand(d.get_One(), d.get_One()); // Создаём экземпляр класса рука и сразу заполняем картами из созданной колоды
				h.Print(); // печатаем руку
				if (Read_Answer("Продолжим? ʕ•ᴥ•ʔ\n1. Да\n2. Нет")) // Здесь можно выйти и мы это и предлагаем
				{
					Field f = new Field(d.get_One(), d.get_One(), d.get_One()); // Создаём экземпляр класса игровое поле и сразу заполняем картами из созданной колоды
					f.Print(); // Печатаем поле
					h.Print(); // Печатаем руку
					Analizer a = new Analizer(h, f); // Отправляем на анализ руку и поле
					if (Read_Answer("Продолжим? ʕ•ᴥ•ʔ\n1. Да\n2. Нет")) // еще раз пытаемся прогнать игрока
					{
						f.AppendOne(d.get_One()); // Заполняем поле картой из колоды
						f.Print(); // Печатаем поле
						h.Print(); // Печатаем руку
						a = new Analizer(h, f);  // Отправляем на анализ руку и поле
						if (Read_Answer("Продолжим ? ʕ•ᴥ•ʔ\n1. Да\n2. Нет")) // Даём последний шанс сдаться
						{
							f.AppendOne(d.get_One()); //  Заполняем поле картой из колоды
							f.Print(); // Печатаем поле
							h.Print();  // Печатаем руку
							a = new Analizer(h, f); // Отправляем на анализ руку и поле
							Console.WriteLine("Игра закончена! (´• ω •`)");
						}
					}
				}
			}
        }
    }
}
