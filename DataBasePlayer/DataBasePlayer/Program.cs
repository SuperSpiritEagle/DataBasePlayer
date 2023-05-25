using System;
using System.Collections.Generic;

namespace DataBasePlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddPlayer = "1";
            const string CommandDeletePlayer = "2";
            const string CommandShowPlayers = "3";
            const string CommandBannedPlayer = "4";
            const string CommandUnbanPlayer = "5";
            const string CommandExit = "6";

            DataBase dataBase = new DataBase();

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"\n   База данных\n" +
                    $"\n [{CommandAddPlayer}] Добавить игрока" +
                    $"\n [{CommandDeletePlayer}] Удалить игрока" +
                    $"\n [{CommandShowPlayers}] Показать игроков" +
                    $"\n [{CommandBannedPlayer}] Забанить игрока" +
                    $"\n [{CommandUnbanPlayer}] Разбанить игрока" +
                    $"\n [{CommandExit}] Выход");

                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case CommandAddPlayer:
                        dataBase.AddPlayer();
                        break;

                    case CommandDeletePlayer:
                        dataBase.DeletePlayer();
                        break;

                    case CommandShowPlayers:
                        dataBase.ShowPlayers();
                        break;

                    case CommandBannedPlayer:
                        dataBase.BanPlayer();
                        break;

                    case CommandUnbanPlayer:
                        dataBase.UnbanPlayer();
                        break;

                    case CommandExit:
                        Console.WriteLine("Программа закончила работу");
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Ошибка. Введены не коректные данные");
                        break;
                }
            }
        }
    }

    class Player
    {
        private string _nickname;

        public Player(string identifier, string nickname, bool status)
        {
            Identifier = identifier;
            _nickname = nickname;
            Status = status;
        }

        public string Identifier { get; private set; }
        public bool Status { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"ID = {Identifier} , Nick = {_nickname} , Status = {Status}");
        }

        public void Ban()
        {
            Status = false;
        }

        public void Unban()
        {
            Status = true;
        }
    }

    class DataBase
    {
        private List<Player> _players = new List<Player>();

        public void AddPlayer()
        {
            string identifier = CreateIdentifier();
            string userInput;

            Console.WriteLine("Введите Nick игрока");
            userInput = Console.ReadLine();

            Console.WriteLine("Введите статус игрока [true] = Unban [false] = Ban");
            bool.TryParse(Console.ReadLine(), out bool status);

            _players.Add(new Player(identifier, userInput, status));
        }

        public void ShowPlayers()
        {
            if (CheckLengh() > 0)

                for (int i = 0; i < _players.Count; i++)
                {
                    _players[i].ShowInfo();
                }
        }

        public void DeletePlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                _players.Remove(player);
                Console.WriteLine("Игрок удален");
            }
        }

        public void BanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Ban();
                Console.WriteLine("Ban");
            }
        }

        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Unban();
                Console.WriteLine("Unban");
            }
        }

        private bool TryGetPlayer(out Player player)
        {
            player = null;

            if (CheckLengh() > 0)
            {
                ShowPlayers();

                Console.WriteLine("Введите ID игрок");
                string userInput = Console.ReadLine();

                for (int i = 0; i < _players.Count; i++)
                {
                    if (_players[i].Identifier == userInput)
                    {
                        player = _players[i];
                        return true;
                    }
                }

                if (player == null)
                {
                    Console.WriteLine("Игрок не найден");
                }
            }

            return false;
        }

        private int CheckLengh()
        {
            if (_players.Count <= 0)
            {
                Console.WriteLine("База данных пуста");
            }

            return _players.Count;
        }

        private string CreateIdentifier()
        {
            var beginning = new DateTime(2016, 1, 1).Ticks;
            var currentTime = DateTime.Now.Ticks - beginning;
            var uniqueId = currentTime.ToString("x");

            return uniqueId;
        }
    }
}
