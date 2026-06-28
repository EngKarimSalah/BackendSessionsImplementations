using System;
using System.Collections.Generic;
using System.Text;

namespace FMS_Solution
{
    internal class TestingInhertance
    {
    }

    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public virtual void MakeSound()
        {
            Console.WriteLine("Some generic animal sound...");
        }

    }


    public class Dog : Animal
    {
        public string Breed { get; set; }

        public override void MakeSound()
        {
            Console.WriteLine("Woof!");
        }
    }

    public class Cat : Animal
    {


    }


    //////////////////////////////////////////////////////////

    public class BankAccount
    {
        public string accountNumber { get; set; }
        public string ownerName { get; set; }
        public double balance { get; set; }

        public double Deposit(double amount)
        {
            balance += amount;
            return balance;

        }


        //overloading => static polymorphism / compiletime
        public virtual double withdraw(double amount)
        {
            balance -= amount;
            return balance;

        }

        public double withdraw(double amount, double fees)
        {
            balance -= (amount + fees);
            return balance;

        }

    }

    public class SavingAccount : BankAccount
    {

    }

    public class CurrentAccount : BankAccount
    {
        //override => dynamic polymorphism / runtime 
        public override double withdraw(double amount)
        {
            balance -= (amount + 0.100);
            return balance;

        }

    }



    public class car
    {
        public string model { get; set; }
        protected int maxSpeed { get; set; }

        public car(int speed)
        {
            maxSpeed = speed;
        }

    }

    public class sedan : car
    {

        public string color { get; set; }

        public sedan(int x) : base(x)
        {

        }
    }
}
