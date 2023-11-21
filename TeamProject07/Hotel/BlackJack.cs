using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Characters;

public enum Suit { Hearts, Diamonds, Clubs, Spades }
public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

namespace TeamProject07.Hotel
{
    internal class BlackJack
    {
        public void BlackJackMain()
        {
            BJPlayer player = new BJPlayer();
            Dealer dealer = new Dealer();
            Blackjack blackjack = new Blackjack();
            Deck deck = new Deck();
            deck.Shuffle();
            string input;
            //Card card = new Card();

            while (true)
            {
                if (blackjack.phase == 0)
                {
                    StartScene();
                    //dealer 2장 뽑기
                    dealer.DrawCardFromDeck(deck);
                    dealer.DrawCardFromDeck(deck);

                    //dealer 2장 뽑기
                    player.DrawCardFromDeck(deck);
                    player.DrawCardFromDeck(deck);

                    PrintGameState(player, dealer);

                    Console.Write("\nNext Move : ");
                    input = Console.ReadLine();
                    blackjack.NextMove(input, player, dealer, deck);
                }
                else if (blackjack.phase == 1)
                {
                    PrintGameState(player, dealer);
                    Console.Write("\nNext Move : ");
                    input = Console.ReadLine();
                    blackjack.NextMove(input, player, dealer, deck);

                }
                else if (blackjack.phase == 2)
                {
                    PrintGameState(player, dealer);
                    blackjack.WhoIsWin(player, dealer);
                    break;
                }

                //input = Console.ReadLine();
                Console.Clear();
            }
        }

        static void PrintGameState(BJPlayer player, Dealer dealer)
        {
            Console.WriteLine("\n||| BLACK JACK |||\n");
            Console.WriteLine("--Dealer--");
            dealer.Hand.PrintHandCards("dealer");
            Console.WriteLine("\n--BJPlayer--");
            player.Hand.PrintHandCards("player");
        }
        static void StartScene()
        {
            Console.Write("Press Any Key to Start ");
            Console.ReadLine();
            Console.Clear();
        }
    }
}

public class Card
{
    public Suit Suit { get; private set; }
    public Rank Rank { get; private set; }

    public Card(Suit s, Rank r)
    {
        Suit = s;
        Rank = r;
    }

    public int GetValue()
    {
        if ((int)Rank <= 10)
        {
            return (int)Rank;
        }
        else if ((int)Rank <= 13)
        {
            return 10;
        }
        else
        {
            return 11;
        }
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

// 덱을 표현하는 클래스
public class Deck
{
    private List<Card> cards;

    public Deck()
    {
        cards = new List<Card>();

        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card(s, r));
            }
        }

        Shuffle();
    }

    public void Shuffle()
    {
        Random rand = new Random();

        for (int i = 0; i < cards.Count; i++)
        {
            int j = rand.Next(i, cards.Count);
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

    public Card DrawCard()
    {
        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }


}

// 패를 표현하는 클래스
public class Hand
{
    private List<Card> cards;

    public Hand()
    {
        cards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public int GetTotalValue()
    {
        int total = 0;
        int aceCount = 0;

        foreach (Card card in cards)
        {
            if (card.Rank == Rank.Ace)
            {
                aceCount++;
            }
            total += card.GetValue();
        }

        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }

        return total;
    }
    public void PrintHandCards(string who)
    {
        foreach (Card PrintCard in cards)
        {
            Console.WriteLine($"{PrintCard.ToString()} ({PrintCard.GetValue()})");
        }
        Console.WriteLine("{0}'s total hand : {1}", who, GetTotalValue());
    }
}

// 플레이어를 표현하는 클래스
public class BJPlayer
{
    public Hand Hand { get; private set; }

    public BJPlayer()
    {
        Hand = new Hand();
    }

    public Card DrawCardFromDeck(Deck deck)
    {
        Card drawnCard = deck.DrawCard();
        Hand.AddCard(drawnCard);
        return drawnCard;
    }
}

// 여기부터는 학습자가 작성
// 딜러 클래스를 작성하고, 딜러의 행동 로직을 구현하세요.
public class Dealer : BJPlayer
{
    public bool NeedToDraw(Deck deck)
    {
        if (Hand.GetTotalValue() < 17)
        {
            Card drawnCard = deck.DrawCard();
            Hand.AddCard(drawnCard);
            return true;
        }
        return false;
    }
    // 코드를 여기에 작성하세요
}

// 블랙잭 게임을 구현하세요. 
public class Blackjack
{
    public int phase = 0;

    public bool WhoIsWin(BJPlayer player, BJPlayer dealer)
    {
        if (((player.Hand.GetTotalValue() > 21) && (dealer.Hand.GetTotalValue() > 21)) || (player.Hand.GetTotalValue() == dealer.Hand.GetTotalValue()))
        {
            Console.WriteLine("\nDRAW!");
            return true;
        }
        else if (dealer.Hand.GetTotalValue() > 21)
        {
            Console.WriteLine("\nPLAYER WIN!");
            return true;
        }
        else if (player.Hand.GetTotalValue() > 21)
        {
            Console.WriteLine("\nBUST!  DEALER WIN!");
            return true;
        }
        else
        {
            if ((21 - player.Hand.GetTotalValue()) < (21 - dealer.Hand.GetTotalValue()))
            {
                if (player.Hand.GetTotalValue() == 21)
                {
                    Console.WriteLine("\nBLACK JACK!!!!!");
                }
                else
                {
                    Console.WriteLine("\nPLAYER WIN!");
                }
                return true;
            }
            else if ((21 - player.Hand.GetTotalValue()) > (21 - dealer.Hand.GetTotalValue()))
            {
                Console.WriteLine("\nDEALER WIN!");
                return true;
            }
        }
        return false;
    }
    public void NextMove(string input, BJPlayer player, Dealer dealer, Deck deck)
    {
        if (input == "hit")
        {
            player.DrawCardFromDeck(deck);
            if (player.Hand.GetTotalValue() > 21)
            {
                phase = 2;
            }
            else
            {
                phase = 1;
            }
        }
        if (input == "stand")
        {
            while (dealer.NeedToDraw(deck) == true) { }
            phase = 2;
        }
    }
    // 코드를 여기에 작성하세요
}
