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
            const string CommandShowPlayer = "3";
            const string CommandStatusPlayer = "4";
            const string CommandExit = "5";

            DataBase dataBase = new DataBase();

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"\n   База данных\n" +
                    $"\n [{CommandAddPlayer}] Добавить игрока" +
                    $"\n [{CommandDeletePlayer}] Удалить игрока" +
                    $"\n [{CommandShowPlayer}] Показать игрока" +
                    $"\n [{CommandStatusPlayer}] Статус игрока" +
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

                    case CommandShowPlayer:
                        dataBase.ShowPlayers();
                        break;

                    case CommandStatusPlayer:
                        dataBase.StatusPlayer();
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
        public string Identifier { get; private set; }
        public string Nickname { get; private set; }
        public bool Status { get; private set; }

        public Player(string identifier, string nik, bool status)
        {
            Identifier = identifier;
            Nickname = nik;
            Status = status;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ID = {Identifier} , Nik = {Nickname} , Status = {Status}");
        }

        public bool SetStatus(bool status)
        {
            return Status = status;
        }
    }

    class DataBase
    {
        private List<Player> _players = new List<Player>();

        public void AddPlayer()
        {
            string identifier = CreateIdentifier();
            string userInput;

            Console.WriteLine("Введите Nik игрока");
            userInput = Console.ReadLine();

            Console.WriteLine("Введите статус игрока [true] = not banned [false] = banned");
            bool.TryParse(Console.ReadLine(), out bool status);

            _players.Add(new Player(identifier, userInput, status));
        }

        public void ShowPlayers()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].ShowInfo();
            }
        }

        public void DeletePlayer()
        {
            ShowPlayers();

            Console.WriteLine("Введите ID игрока");
            string userInput = Console.ReadLine();

            for (int i = 0; i < _players.Count; i++)
            {
                if (userInput == _players[i].Identifier)
                {
                    _players.Remove(_players[i]);
                    Console.WriteLine("Игрок удален");
                }
                else
                {
                    Console.WriteLine("Игрок не найден");
                }
            }
        }

        public void StatusPlayer()
        {
            ShowPlayers();

            Console.WriteLine("Введите ID игрок");
            string userInput = Console.ReadLine();

            for (int i = 0; i < _players.Count; i++)
            {
                if (userInput == _players[i].Identifier)
                {
                    Console.WriteLine("Введите статус игрока true или false");

                    bool.TryParse(Console.ReadLine(), out bool status);

                    if (status == true)
                    {
                        Console.WriteLine("Игрок разбанен");
                    }
                    else
                    {
                        Console.WriteLine("Игоко забанен");
                    }

                    _players[i].SetStatus(status);
                }
            }
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
