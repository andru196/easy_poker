using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
	public class Card : IComparable // Класс для экземпляра карт
	{
		private static string[] indexator =  {"2", "3", "4", "5", "6", "7", "8", "9", "X", "J", "Q", "K", "A"};
		public string suit;
		public int power;
		public string suit_ico;
		public string value;
		private System.ConsoleColor color;
		public Card(string a, string b) //	Конструктор класса. 
		{                               //	 Принимает две строки:
			this.suit = a;			//		 Первая задаёт рубашку
			this.value = b;				//	Вторая значение карты
			power = Array.IndexOf(indexator, b);
			switch (a)
			{
				case ("D"):         // diamond-бубна
					this.suit_ico = "♦";
					this.color = ConsoleColor.Blue;
					break;
				case ("S"):         // spade-пика
					this.suit_ico = "♠";
					this.color = ConsoleColor.Gray;
					break;
				case ("C"):         // club-трефа
					this.suit_ico = "♣";
					this.color = ConsoleColor.Green;
					break;
				case ("H"):         // heart-черва
					this.suit_ico = "♥";
					this.color = ConsoleColor.Red;
					break;
			}
		}
		public void Print_Str(int i) // Метод печатающий i-ую часть карты. Толко часть, потому что карта занимает несколько строк, при том что на одной страке может быть несколько карт
		{//								Принимает число i, обозначающее какую строку карты печатать, от 1 до 4
			Console.ForegroundColor = this.color; // Ничего не возвращает, только печатает, например 4 вызова подряд:
			switch (i)                              // ___ 
			{                                       // |♠| 
				case 1:                             // |X| 
					Console.Write(" ___ ");         // ___ 
					break;
				case 2:
					Console.Write($" |{this.suit_ico}| ");
					break;
				case 3:
					Console.Write($" |{this.value}| ");
					break;
				case 4:
					Console.Write(" ___ ");
					break;
				default:
					break;
			}
			Console.ResetColor();
		} 
		public override string ToString() //Метод представляющий экземпляр класса в строке
		{                               // ничего не принимает
			return ($"{value}{this.suit}"); // Возвращает строку со значением и рубашкой карты, например: AH, или 2S
		} 
		public int CompareTo(object o) // Метод для сравнения карты с другой картой
		{							// Принимает на вход экземпляр карты
			Card c = o as Card;		// Возвращает число: -1, 0, 1
			if (c != null)
				return this.power.CompareTo(c.power);
			else
				throw new Exception("Невозможно сравнить два объекта");
		} 
		public int CompareTo(Card p) // Метод для сравнения карты с другой картой
		{                               // Принимает на вход экземпляр карты
			return this.power.CompareTo(p.power);  // Возвращает число: -1, 0, 1
		}
	}
	public class Deck // Класс колоды
	{
		private Card[] deck = new Card[52]; // Массив карт
		public Card[] Deckk // Свойство реализующее частичный доступ к колоде
		{
			get
				{
				return (this.deck);
				}
			set
				{
				Console.WriteLine("ERROR");
				}
			}
		public Deck()  // Конструктор колоды
		{           // заполняет экземпляр картами в строго определённом порядке
			string	s; // на вход ничего не принимает
			string	v; // НИчего не возвращает
			for (int i = 0; i < 13; i++)
			{
				switch (i)
				{
					case 0:
						v = "A";
						break;
					case 1:
						v = "X";
						break;
					case 10:
						v = "J";
						break;
					case 11:
						v = "Q";
						break;
					case 12:
						v = "K";
						break;
					default:
						v = i.ToString();
						break;
				}
				for(int j = 0; j < 4; j++)
				{
					
					switch (j)
					{
						case 0:
							s = "D";
							break;
						case 1:
							s = "H";
							break;
						case 2:
							s = "S";
							break;
						case 3:
							s = "C";
							break;
						default:
							s = "";
							break;
					}
					deck[(i * 4) + j] = new Card(s, v);
				}
			}
		} 
		public void Shuffle() // Метод для перемешивания колоды не принимает и не возвращает ничего
		{					// Ничего не принимает
			var random = new Random(DateTime.Now.Millisecond); // Ничего не возвращает
			deck = deck.OrderBy(x => random.Next()).ToArray(); // Изменяет экземпляр к которому вызван
			Array.Reverse(deck);
		} 
		public Card get_One()  // Метод возвращающий последнюю карту колоды, заранее удаляя ее оттуда
		{					// Принимает ничего
			Card c = new Card(deck[deck.Length - 1].suit, deck[deck.Length - 1].value); // Возвращает последнюю карту из колоды
			Array.Resize(ref deck, deck.Length - 1);
			return c;
		}
		public override string ToString() // Метод позволяющий получить строковое представление всей колоды
		{								// Ничего не принимает
			string str = "";			// Возвращает строку, со всеми картами в колоде, краткий пример: "AS 4S 3H"
			foreach (Card el in deck)
			{
				str = String.Concat(str, " ", el.ToString());
			}
			return (str);
		}
	}
	public class Hand // Класс руки
	{
		public Card[] cards = new Card[2]; 
		public virtual void Print() // Метод печатающий содержимое руки (2 карты)
		{							// Ничего не принимает
			for (int i = 0; i < 4; i++)	// Ничего не возвращает, только печатает
			{
				cards[0].Print_Str(i + 1);
				cards[1].Print_Str(i + 1);
				Console.Write("\n");
			}
			Console.WriteLine("\n");
		}
		public override string ToString() // Метод возвращающий строку с содержимым руки (в краткой форме)
		{									// Ничего не принимает
			return (String.Concat(cards[0].ToString(), " ", cards[1].ToString())); // Возвращает строку с кратким содержимым руки, например: "AH JS"
		} 
		public Hand(Card c1, Card c2) // Конструктор руки.  и заполняет ими внутренний массив карт
		{                           // Принимает 2 экземпляра карт
			this.cards[0] = c1;		//Ничего не возвращает
			this.cards[1] = c2;
		} 
	} 
	public class Field : Hand // Класс игрового поля/стола
	{
		public int Length; // Количество карт на столе в данный момент
		private new Card[] cards = new Card[5]; // Массив карт
		public Card[] Cards // Свойство дающее частичный доступ к списку карт
		{
			get
			{
				return (cards);
			}

		}
		public void AppendOne(Card car) // Метод для добавления карты на стол. Принимает экземпляр карты, занося его во внутренний массив, ничего не возвращат.
		{								// Принимает экземпляр карты
			if (this.Length == 3)		// ничего не возвращает
			{
				cards[3] = car;
				this.Length++;
			}
			else if (this.Length == 4)
			{
				cards[4] = car;
				this.Length++;
			}
		}
		public override void Print() // метод печатающий все карты на столе
		{								// Ничего не возвращает
			for (int i = 1; i <= 4; i++)// Ничего не принимает
			{
				for (int j = 0; j < this.Length; j++)
					cards[j].Print_Str(i);
				Console.Write("\n");
			}
			Console.WriteLine("");
		}
		public Field(Card c1, Card c2, Card c3) // Конструктор игрового поля
			: base(c1, c2)                      // принимает 3 экземпляра карт
		{										// Возвращает ничего
			cards[0] = c1;
			cards[1] = c2;
			cards[2] = c3;
			Length = 3;
		}
	}
	public class Analizer // Класс анализирующий текущий состояние игрока
	{
		private Card[] cards = new Card[7];
		int D; // Счетчики для разных мастей
		int H; 
		int C;
		int S;
		int Length; // количество карт в игре
		public Analizer(Hand h, Field f) // Конструктор:
		{									
			h.cards.CopyTo(this.cards, 0);//  принимает экземпляр руки и игрового поля
			f.Cards.CopyTo(this.cards, 2);// Добавляет их карты в свой массив
			this.Length = 2 + f.Length;	// Ничего не возвращает
			D = H = C = S = 0;
			for (int i = 0; i < this.Length; i++)
			{
				switch (this.cards[i].suit)
				{
					case "S":
						S++;
						break;
					case "C":
						C++;
						break;
					case "H":
						H++;
						break;
					case "D":
						D++;
						break;
				}
			}
			Console.WriteLine($"Бубн: {D}\nПик: {S}\nТреф: {C}\nЧерв:{H}");
			Array.Sort(this.cards);
			string str;
			string str1;
			if (((str, str1) = Straight_Flush_Search()).Item1 == (null))
			{
				if ((str = Four_of_Kind_Search()) == null)
				{
					if (((str, str1) = Full_House_Search()).Item1 == (null))
					{
						if ((str = Flush_Search()) == null)
						{
							if ((str = Street_Search()) == null)
							{
								if ((str = Three_Search()) == null)
								{
									if (((str, str1) = Two_Pairs_Search()).Item1 == null)
									{
										if ((str = Pair_Search()) == null)
											Console.WriteLine($"Старшая карта: {cards[6].ToString()}");
										else Console.WriteLine($"Пара из {str}");
									}
									else Console.WriteLine($"Две пары из {str} и {str1}");
								}
								else Console.WriteLine($"Cет из {str}");
							}
							else Console.WriteLine($"Стрит, старшая карта {str}");
						}
						else Console.WriteLine($"Флеш из {str}");
					}
					else Console.WriteLine($"Фулл хаус из тройки {str} и пары: {str1}");
				}
				else Console.WriteLine($"Карэ из {str}");
			}
			else Console.WriteLine($"Стрит флеш, старшая карта {str}{str1}!!!");
		}
		string Pair_Search() // Метод для поиска комбинации "Пара"
		{					// Принимает ничего
			int i = this.cards.Length; // Возвращает  возвращает null если поиск не удачен или значение карты, участвующией в комбинации, например если есть пара вольтов: "J"
			for (int j = i - 1; j > 7 - this.Length; j--)
			{
				if (cards[j].value == cards[j - 1].value)
				{
					if (j > 1 && cards[j - 1]?.value == cards[j - 2]?.value)
					{
						if (j > 2 && cards[j - 2]?.value == cards[j - 3]?.value)
							j -= 2;
						else
							j -= 1;
					}
					else
						return (cards[j].value);
				}
			}
			return (null);
		} 
		string Pair_Search(string s) // Метод для поиска комбинации "Пара", после обнаружения одной пары
		{                               // возвращает null если поиск не удачен или значение карты, участвующией в комбинации, принимает значение карты, для последующего игнорирования уже найденной пары, например если у нас пара королев: "Q"
			int i = this.cards.Length; // Принимае значение карты, пара которой уже обнаружена и должна быть пропущена, например "A"
			for (int j = i - 1; j > 7 - Length; j--)
			{
				if (cards[j].value == s)
				{
					j -= 1;
					continue;
				}
				if (cards[j].value == cards[j - 1].value)
				{
					if (j > 1 && cards[j - 1].value == cards[j - 2]?.value)
					{
						if (j > 2 && cards[j - 2].value == cards[j - 3]?.value)
							j -= 3;
						else
							j -= 2;
					}
					else
						return (cards[j].value);
				}
			}
			return (null);
		}	
		private  (string, string) Two_Pairs_Search()	// Метод для поиска комбинации "Две пары", 
		{												//Ничего не принимает
			string s;                                   //возвращает (null,null) если поиск не удачен или значения карт, участвующих в комбинации, например если найдено две пары 4 и 2: ("4", "2")
			string s2;
			if ((s = Pair_Search()) != null)
				if ((s2 = Pair_Search(s)) != null)
				{
					var result = (s, s2);
					return result;
				}
			return (null, null);
		}
		(string, string) Full_House_Search()	// Метод для поиска комбинации "Фулл-Хаус"
		{										//Ничего не принимает
			string s1;                          //Возвращает (null,null) если поиск не удачен или значения карт, участвующих в комбинации, первая соответствует тройке, вторая паре
			string s2;							// Например найден фулл хаус из сета тузов и пары двоек: ("A", "2")
			if ((s1 = Three_Search()) != null)
				if ((s2 = Pair_Search()) != null)
					return (s1, s2);
				else if ((s2 = Three_Search(s1)) != null)
					return (s1, s2);
			return (null, null);
		}
		string Three_Search() // Метод для поиска комбинации "Сет", 
		{                       //Принимает ничего
			int i = this.cards.Length; //Возвращает null если поиск не удачен или значение карты, участвующией в комбинации, например найден сет из королей: "K"
			for (int j = i - 1; j > 7 - Length; j--)
			{
				if (cards[j].value == cards[j - 1].value)
				{
					if (j > 1 && cards[j - 1]?.value == cards[j - 2]?.value)
					{
						if (j > 2 && cards[j - 2]?.value == cards[j - 3]?.value)
							j -= 2;
						else
							return (cards[j].value);
					}
					j -= 1;
				}
			}
			return (null);
		}
		string Three_Search(string s)   // Метод для поиска комбинации "Сет" и пропуска найденного сета
		{                               //Принимает значение первого сета, например: "A"
			int i = this.cards.Length;  //Возвращает null если поиск не удачен или значение карты, участвующией в комбинации, например  найден сет из королей: "K"
			for (int j = i - 1; j > 7 - Length; j--)
			{
				if (cards[j].value == s)
					continue;
				if (cards[j].value == cards[j - 1].value)
				{
					if (j > 1 && cards[j - 1]?.value == cards[j - 2]?.value)
					{
						if (j > 2 && cards[j - 2]?.value == cards[j - 3]?.value)
							j -= 2;
						else
							return (cards[j].value);
					}
					j -= 1;
				}
			}
			return (null);
		}
		string Four_of_Kind_Search()  // Метод для поиска комбинации "Карэ"
		{								//Ничего не возвращает
			int i = this.cards.Length;  //Возвращает возвращает null если поиск не удачен или значение карты, участвующией в комбинации
			for (int j = i - 1; j > 7 - Length; j--) // Например если найдено карэ из двоек: "2"
			{
				if (cards[j].value == cards[j - 1].value)
				{
					if (j > 1 && cards[j - 1].value == cards[j - 2]?.value)
					{
						if (cards[j - 2].value == cards[j - 3]?.value)
							return (cards[j].value);
					}
					else
						j -= 1;

				}
			}
			return (null);
		}
		string Street_Search()	// Метод для поиска комбинации "Стрит"
		{						//Ничего не принимает
			int i = 0;          //Возвращает null если поиск не удачен или значение старшей! карты, участвующией в комбинации
			int j = this.cards.Length - 1; //Напимер у нас есть следующий стрит: 62345, тогда вернется срока "6"
			string str = null;				
			while (j != 0 && i < 5)
			{
				
				if (j >= 3)
				{
					str = cards[j].value;
					i = 1;
					for (int k = 0; k < 4; k++)
					{
						if (j - k > 7 - Length && cards[j - k].power == cards[j - k - 1]?.power)
						{
							if (j - k > 7 - Length + 1 && cards[j - k].power == cards[j - k - 2].power)
							{
								if (j - k > 7 - Length + 2 && cards[j - k].power == cards[j - k - 3].power)
									j--;
								j--;
							}
							j--;
						}
						if (j - k > 7 - Length && cards[j - k].power != (cards[j - k - 1].power + 1))
							break;
						if (j - k > 7 - Length && cards[j - k].power == (cards[j - k - 1].power + 1))
							i++;
					}
				} 
				j--;
			}
			if (i == 4 && str == "5" && cards[cards.Length - 1].value == "A")
				i++;
			return ( i == 5 ? str : null);
		}
		string Flush_Search()	// Метод для поиска комбинации "Флеш"
		{						//Ничего не принимает
			if (D > 4)          //Возвращает null если поиск не удачен или рубашку карт, участвующих в комбинации
				return "D";		//Например имеется флеш из черв(H), тогда вернется строка "H"
			else if (H > 4)
				return "H";
			else if (C > 4)
				return "C";
			else if (S > 4)
				return "C";
			else
				return (null);

		}
		(string, string) Straight_Flush_Search() // Метод для поиска комбинации "Стрит-Флеш"
		{										//Ничего не принимает
			string s1;                          //Возвращает (null,null) если поиск не удачен или значение и масть старшей карты участвующей в комбинации.
			string s2;									//Например имеется стрит-флеш из следующих треф 87654, тогда мы получим кортеж строк: ("C", "8")
			if ((s1 = Flush_Search()) != null)
			{
				if ((s2 = Street_Search()) != null)
				{
					int l = Length - 1;
					while (cards[l].value != s2)
						l--;
					
					for (int k = 0; k < 3; k++)
					{
						int i = l - k;
						int j = 1;
						while (i != 0)
						{
							if (cards[i].power == cards[i - 1].power + 1 && cards[i].suit == s1 && s1 == cards[i - 1].suit)
								j++;
							i--;
						}
						if (j == 4 && cards[this.Length - 1].value == "A")
						{
							if (cards[this.Length - 1].suit == s1)
								j++;
							else if (cards[this.Length - 2].value == "A" && cards[this.Length - 2].suit == s1)
								j++;
							else if (cards[this.Length - 3].value == "A" && cards[this.Length - 3].suit == s1)
								j++;
						}
						if (j > 4)
							return (s1, s2);
					}
				}
			}
			return (null, null);
		}
	}
}