using System;
using System.Linq;

namespace CarRaceGame
{

    class Program
    {
        static void Main(string[] args)
        {
            Vehicle[] vehicles = new Vehicle[5];

            vehicles[0] = new Vehicle();
            vehicles[0].Color = "Red";
            vehicles[0].Number = 2;
            vehicles[0].Driver = "Joe";

            //Another way to initialize the object:
            vehicles[1] = new Vehicle
            {
                Color = "Red",
                Number = 5,
                Driver = "Kali"
            };

            vehicles[2] = new Vehicle
            {
                Color = "Blue",
                Number = 7,
                Driver = "Jenna"
            };

            vehicles[3] = new Vehicle
            {
                Color = "Yellow",
                Number = 8,
                Driver = "Amy"
            };

            vehicles[4] = new Vehicle
            {
                Color = "Silver",
                Number = 13,
                Driver = "Jim"
            };


            int lapNumber = 0;
            while (lapNumber < 13 && vehicles.Any(x => !x.Broken))
            {
                foreach (var vehicle in vehicles)
                {
                    if (!vehicle.Broken)
                    {
                        int choice = PromptUserForNumber(string.Format("{0}, would you like to 1.take a lap, or 2.pit ?", vehicle.Driver));
                        if (choice == 2)
                        {
                            vehicle.Pit();
                        }
                        else
                        {
                            vehicle.TakeALap();
                        }
                    }
                }
                vehicles = vehicles.OrderBy(x => x.Broken).ThenBy(x => x.TotalTime).ToArray();

                lapNumber++;
                Console.WriteLine("Standings after lap {0}:", lapNumber + 1);
                for (int i = 0; i < vehicles.Length; i++)
                {
                    if (!vehicles[i].Broken)
                    {
                        Console.WriteLine("{0}:\t{1} in the {2}\t{3}\t\t{4}\t{5}", i + 1, vehicles[i].Driver, (vehicles[i].Color + " car").PadRight(25, ' '), vehicles[i].TotalTime, vehicles[i].FuelPercentage, vehicles[i].TirePercentage);
                    }
                    else
                    {
                        Console.WriteLine("{0} broke down on lap {1}", vehicles[i].Driver, vehicles[i].LapsTaken);
                    }
                }

            }


            Console.WriteLine("#{0} Wins it! Congratulations, {1} in the {2} car!", vehicles[0].Number, vehicles[0].Driver, vehicles[0].Color);
            Console.ReadLine();


        }


        private static int PromptUserForNumber(string promptString = "Enter a number")
        {
            int num;
            string input = "";
            while (!int.TryParse(input, out num))
            {
                Console.WriteLine(promptString);
                input = Console.ReadLine();
            }

            return num;
        }
    }

    public class Vehicle
    {
        public Vehicle()
        {
            TotalTime = new TimeSpan();
            FuelPercentage = 100;
            TirePercentage = 100;
        }

        private static Random _r = new Random();

        public string Color { get; set; }
        public int Number { get; set; }
        public string Driver { get; set; }

        public bool Broken { get; private set; }

        public TimeSpan TotalTime { get; private set; }

        public decimal FuelPercentage { get; private set; }

        public decimal TirePercentage { get; private set; }

        public int LapsTaken { get; private set; }

        public void TakeALap()
        {
            if (!Broken)
            {
                FuelPercentage -= _r.Next(10, 20);
                TirePercentage -= _r.Next(5, 10);
                LapsTaken += 1;
                TotalTime += new TimeSpan(0, 0, _r.Next(45, 70));


                if (_r.Next(0, 50) == 1)
                {
                    Broken = true;
                }

                if (FuelPercentage <= 0)
                {
                    Broken = true;
                }
                if (TirePercentage <= 0)
                {
                    Broken = true;
                }
            }
        }

        public void Pit()
        {
            TotalTime += new TimeSpan(0, 0, _r.Next(15, 20));
            FuelPercentage = 100;
            if (TirePercentage < 20)
            {
                TotalTime += new TimeSpan(0, 0, _r.Next(20, 30));
                TirePercentage = 100;
            }
            TakeALap();
        }


    }
}
