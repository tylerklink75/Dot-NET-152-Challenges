﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Challenge_4v2
{
    internal class ProgramUI
    {
        readonly BadgeRepository _badgeRepo = new BadgeRepository();
        List<Door> _allDoors;
        List<Door> _tempDoors;
        Dictionary<int, List<Door>> _badges;
        bool isRunning;
        internal void Run()
        {
            _badges = _badgeRepo.GetAllBadges();
            _allDoors = _badgeRepo.GetAllDoors();
            SeedDoors();
            SeedBadges();

            isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                PrintMainMenu();
                ParseUserResponse(Console.ReadLine());
            }
            Console.ReadLine();
        }

        private void ParseUserResponse(string userResponse)
        {
            switch (userResponse)
            {
                case "1":
                    AddNewBadge();
                    break;
                case "2":
                    //PrintEditBadgeMenu();
                    break;
                case "3":
                    PrintBadges();
                    break;
                case "4":
                    CreateNewDoor();
                    break;
                case "5":
                    //EditExistingDoor();
                    break;
                case "6":
                    RemoveExistingDoor();
                    break;
                case "7":
                    isRunning = false;
                    break;
                default:
                    PrintSassyMessage(userResponse);
                    break;
            }
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        private void RemoveExistingDoor()
        {
            PrintAllDoors();
            Console.Write("Enter the number of the door you wish to remove: ");
            int removeDoorNumber = int.Parse(Console.ReadLine());
            var doorToRemove = _allDoors.Find(door => door.DoorID == removeDoorNumber);
            _badgeRepo.RemoveDoor(doorToRemove);
        }

        private void PrintSassyMessage(string userInput)
        {
            Thread.Sleep(1275);
            Console.Write("What ");
            Thread.Sleep(1225);
            Console.Write("were ");
            Thread.Sleep(1225);
            Console.Write("you ");
            Thread.Sleep(1225);
            Console.WriteLine("thinking?!?");
            Thread.Sleep(1225);
            Console.WriteLine($"\"{userInput}\" is not a valid choice. Try again...");
            Thread.Sleep(2425);
        }

        private void PrintBadges()
        {
            Console.WriteLine($"Badge No.\t\tDoors");
            foreach(var badge in _badges)
                Console.WriteLine($"{badge.Key}\t\t\t{ConvertDoorListToString(badge.Value)}");
        }

        private string ConvertDoorListToString(List<Door> value)
        {
            string doorNames = "";

            foreach(var door in value)
                doorNames += $"{door.DoorName}, ";
            return doorNames.TrimEnd(new char[]{',',' '});
        }

        private void AddNewBadge()
        {
            _tempDoors = new List<Door>();
            bool addMoreDoors;
            do
            {
                Console.WriteLine($"Would you like to:\n" +
                    $"1. Add an existing Door?\n" +
                    $"2. Add a new door?\n");
                int.TryParse(Console.ReadLine(), out int menuResult);
                switch (menuResult)
                {
                    case 1:
                        PrintAllDoors();
                        Console.Write("Enter the number of the door you wish to add to the new badge:");
                        var desiredDoorIndex = int.Parse(Console.ReadLine()) - 1;
                        _tempDoors.Add(_allDoors[desiredDoorIndex]);
                        break;
                    case 2:
                        _tempDoors.Add(CreateNewDoor());
                        break;
                }
                Console.WriteLine("Would you like to add more doors to the badge? y/n");
                addMoreDoors = GetBooleanResponse();
            } while (addMoreDoors);

            var newBadge = new Badge(_badges.Count + 1, _tempDoors);
            _badgeRepo.AddBadgeToDictionary(newBadge);
        }

        private bool GetBooleanResponse()
        {
            var response = Console.ReadLine().ToLower();
            return response.Contains("y");
        }

        private Door CreateNewDoor()
        {
            Door newDoor;
            Console.WriteLine($"Enter the name of the new door:");
            var doorName = Console.ReadLine();

            if (!_allDoors.Exists(door => door.DoorName == doorName))
            {
                newDoor = new Door()
                {
                    DoorName = doorName
                };
                _badgeRepo.AddNewDoorToAllDoors(newDoor);
                return newDoor;
            }
            else
                return _allDoors.Find(door => door.DoorName == doorName);
        }

        private void PrintAllDoors()
        {
            //var i = 1;
            foreach (var door in _allDoors)
            {
                Console.WriteLine($"{door.DoorID}. {door}");
                //i++;
            }
        }

        private void PrintMainMenu()
        {
            Console.WriteLine($"What would you like to do?" +
                $"\n\n1. Add a badge" +
                $"\n2. Edit a badge" +
                $"\n3. List all badges" +
                $"\n4. Add a Door" +
                $"\n5. Edit a Door" +
                $"\n6. Remove a Door" +
                $"\n7. Exit program");
        }

        private void SeedBadges()
        {
            _badgeRepo.AddBadgeToDictionary(new Badge(_badges.Count + 1, new List<Door>() { _allDoors[0], _allDoors[2] }));
            _badgeRepo.AddBadgeToDictionary(new Badge(_badges.Count + 1, new List<Door>() { _allDoors[2], _allDoors[3] }));
            _badgeRepo.AddBadgeToDictionary(new Badge(_badges.Count + 1, new List<Door>() { _allDoors[4] }));
        }

        private void SeedDoors()
        {
            _badgeRepo.AddNewDoorToAllDoors(new Door(_allDoors.Count + 1, "A1"));
            _badgeRepo.AddNewDoorToAllDoors(new Door(_allDoors.Count + 1, "A2"));
            _badgeRepo.AddNewDoorToAllDoors(new Door(_allDoors.Count + 1, "A3"));
            _badgeRepo.AddNewDoorToAllDoors(new Door(_allDoors.Count + 1, "B1"));
            _badgeRepo.AddNewDoorToAllDoors(new Door(_allDoors.Count + 1, "C1"));
        }
    }
}