using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_Book_GoFish
{
    class Player
    {

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
        }
        private Random random;
        private Deck cards;
        private TextBox textBoxOnForm;
        public Player(string name, Random random, TextBox textBoxOnForm)
        {
            // Конструктор класса Player инициализирует четыре закрытых поля, а затем
            // добавляет элементу управления TextBox строку ″Joe has just
            // joined the game″, используя имя закрытого поля. Не забудьте поставить
            // знак переноса в конец каждой строки, добавляемой в TextBox.
            this.name = name;
            this.random = random;
            this.cards = new Deck(new List<Card>());
            this.textBoxOnForm = textBoxOnForm;
            this.textBoxOnForm.Text += name + " has just joined the game\n";
        }

        public IEnumerable<Values> PullOutBooks()
        {
            List<Values> books = new List<Values>();
            for (int i = 1; i <= 13; i++)
            {
                Values value = (Values)i;
                int howMany = 0;
                for (int card = 0; card < cards.Count; card++)
                    if (cards.Peek(card).Value == value)
                        howMany++;
                if (howMany == 4)
                {
                    books.Add(value);
                    cards.PullOutValues(value);
                }
            }
            return books;
        }

        public Values GetRandomValue()
        {
            // Этот метод получает случайное значение, но из числа карт колоды!
            return (Values)random.Next(cards.Count - 1) + 1;
        }

        public Deck DoYouHaveAny(Values value)
        {
            // Соперник спрашивает о наличии у меня карты нужного достоинства
            // Используйте метод Deck.PullOutValues() для взятия карт. Добавьте в TextBox
            // строку ″Joe has 3 sixes″, используйте новый статический метод Card.Plural()
            Deck deckToReturn = cards.PullOutValues(value);
            textBoxOnForm.Text += name + " has " + deckToReturn.Count + " " + Card.Plural(value).ToString() + "\n";
            return deckToReturn;
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock)
        {
            // Это перегруженная версия AskForACard() — выберите случайную карту с помощью
            // метода GetRandomValue() и спросите о ней методом AskForACard()
            AskForACard(players, myIndex, stock, GetRandomValue());
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock, Values value)
        {
            // Спросите карту у соперников. Добавьте в TextBox текст: ″Joe asks if anyone has
            // a Queen″. В качестве параметра вам будет передана коллекция игроков
            // спросите (с помощью метода DoYouHaveAny()), есть ли у них карты
            // указанного достоинства. Переданные им карты добавьте в свой набор.
            // Следите за тем, сколько карт было добавлено. Если ни одной, вам нужно
            // взять карту из запаса (передается как параметр), в текстовое
            // поле нужно добавить строку TextBox: ″Joe had to draw from the stock″
            textBoxOnForm.Text += Name + " asks if anyone has a " + value.ToString() + "\n";
            int numberOfAddedCards = 0;
            for (int i = 0; i < players.Count; i++)
            {
                if(i != myIndex)
                {
                    Deck cardsFromAnotherPlayer = players[i].DoYouHaveAny(value);
                    numberOfAddedCards += cardsFromAnotherPlayer.Count;
                    for (int b = 0; b < cardsFromAnotherPlayer.Count; b++)
                    {
                        TakeCard(cardsFromAnotherPlayer.Peek(b));
                    }
                }
            }
            if(numberOfAddedCards == 0)
            {
                TakeCard(stock.Deal(random.Next(stock.Count)));
            }
        }

        // Перечень свойств и коротких методов, которые уже были написаны
        public int CardCount { get { return cards.Count; } }
        public void TakeCard(Card card) { cards.Add(card); }
        public IEnumerable<string> GetCardNames() { return cards.GetCardNames(); }
        public Card Peek(int cardNumber) { return cards.Peek(cardNumber); }
        public void SortHand() { cards.SortByValue(); }

    }
}
