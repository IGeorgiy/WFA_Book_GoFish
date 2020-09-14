using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_Book_GoFish
{
    class Game
    {

        private List<Player> players;
        private Dictionary<Values, Player> books;
        private Deck stock;
        private TextBox textBoxOnForm;

        public Game(string playerName, IEnumerable<string> opponentNames, TextBox textBoxOnForm)
        {
            Random random = new Random();
            this.textBoxOnForm = textBoxOnForm;
            players = new List<Player>();
            players.Add(new Player(playerName, random, textBoxOnForm));
            foreach (string player in opponentNames)
            {
                players.Add(new Player(player, random, textBoxOnForm));
            }
            books = new Dictionary<Values, Player>();
            stock = new Deck();
            Deal();
            players[0].SortHand();
        }
        private void Deal()
        {
            // Именно здесь начинается игра.
            // Тасуется колода, раздается по пять карт каждому игроку, затем с помощью
            // цикла foreach вызывается метод PullOutBooks() для каждого игрока.
            foreach(Player player in players)
            {
                for(int i = 0; i < 5; i++)
                {
                    Random random = new Random();
                    player.TakeCard(stock.Deal(random.Next(stock.Count - 1)));
                }

            }
        }
        public bool PlayOneRound(int selectedPlayerCard)
        {
            // Сыграйте один раз. Параметром является выбранная игроком карта из имеющихся на руках
            // Вызовите метод AskForACard() для каждого из игроков, начиная с человека
            // с нулевым индексом. Затем вызовите метод PullOutBooks() —
            // если он вернет значение true, значит, у игрока кончились
            // карты. Закончив со всеми игроками, отсортируйте карты
            // человека (чтобы список в форме выглядел красиво). Проверьте, не закончились
            // ли карты в запасе. В случае положительного результата очистите поле TextBox
            // и выведите фразу ″The stock is out of cards. Game over!″.

        }
        public bool PullOutBooks(Player player)
        {
            // Игроки выкладывают взятки. Метод возвращает значение true, если карты
            // у игрока закончились. Каждая взятка добавляется в словарь Books.
        }
        public string DescribeBooks()
        {
            // Этот метод возвращает длинную строку с описанием взяток каждого игрока,
            // взяв за основу содержание словаря Books: ″Joe has a book of sixes.
            // (перенос строки) Ed has a book of Aces.″
        }
        public string GetWinnerName()
        {
            // Этот метод вызывается в конце игры. Он использует собственный словарь
            // (Dictionary<string, int> winners) для отслеживания количества взяток
            // каждого игрока. Сначала цикл foreach (Values value in books.Keys)
            // заполняет словарь winners информацией о взятках. Затем
            // словарь просматривается на предмет поиска максимального
            // количества взяток. Напоследок словарь просматривается еще один раз, чтобы
            // сформировать список победителей в виде строки (″Joe and Ed″). Если победитель
            // один, возвращается строка ″Ed with 3 books″. В противном
            // случае возвращается строка ″A tie between Joe and Bob with 2 books.″
        }
        // Пара коротких методов, которые были написаны раньше:
        public IEnumerable<string> GetPlayerCardNames()
        {
            return players[0].GetCardNames();
        }
        public string DescribePlayerHands()
        {
            string description = "";
            for (int i = 0; i < players.Count; i++)
            {
                description += players[i].Name + " has " + players[i].CardCount;
                if (players[i].CardCount == 1)
                    description += " card." + Environment.NewLine;
                else
                    description += " cards." + Environment.NewLine;
            }
            description += "The stock has " + stock.Count + " cards left.";
            return description;
        }
    }
}
