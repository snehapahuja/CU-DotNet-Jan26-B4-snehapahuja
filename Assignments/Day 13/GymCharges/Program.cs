namespace GymCharges
{
    internal class Program
    {
        static double CalculateGymBill(bool treadMill, bool weight, bool zumba)
        {
            double amount = 1000.00;
            amount += treadMill ? 300.00 : 0.00;
            amount += weight ? 500.00 : 0.00;
            amount += zumba ? 250.00 : 0.00;

            if (amount == 1000.00)
            {
                Console.WriteLine("No Service Opted, and you are fined");
                amount += 500;
            }
            amount += amount * 0.05;

            return (amount);
        }
            
            static void Main(string[] args)
            {
                bool treadMill = bool.Parse(Console.ReadLine());
                double total = CalculateGymBill(false, false, false);
                Console.WriteLine($"Bill: {total:f2}");
                

            }
        }
    }
