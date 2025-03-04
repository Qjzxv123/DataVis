using System;
using System.Collections.Generic;

public class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Sound { get; set; }
    public bool IsEndangered { get; set; }

    // Constructor to initialize properties
    public Animal(string name, int age, string sound, bool isEndangered = false)
    {
        Name = name;
        Age = age;
        Sound = sound;
        IsEndangered = isEndangered;
    }

    // Virtual method to be overridden by derived classes
    public virtual void MakeSound()
    {
        Console.WriteLine(Sound);
    }

    // Feed the animal
    public void Feed()
    {
        Console.WriteLine($"{Name} has been fed!");
    }
}

// Mammal class inheriting from Animal
public class Mammal : Animal
{
    public string FurColor { get; set; }

    // Constructor calling the base class constructor
    public Mammal(string name, int age, string sound, string furColor, bool isEndangered = false)
        : base(name, age, sound, isEndangered)
    {
        FurColor = furColor;
    }

    // Overriding the MakeSound method for mammals
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says: {Sound}. It's a mammal.");
    }
}

// Bird class inheriting from Animal
public class Bird : Animal
{
    public bool CanFly { get; set; }

    // Constructor calling the base class constructor
    public Bird(string name, int age, string sound, bool canFly, bool isEndangered = false)
        : base(name, age, sound, isEndangered)
    {
        CanFly = canFly;
    }

    // Overriding the MakeSound method for birds
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says: {Sound}. It's a bird.");
    }
}

// Reptile class inheriting from Animal
public class Reptile : Animal
{
    public bool IsColdBlooded { get; set; }

    // Constructor calling the base class constructor
    public Reptile(string name, int age, string sound, bool isColdBlooded, bool isEndangered = false)
        : base(name, age, sound, isEndangered)
    {
        IsColdBlooded = isColdBlooded;
    }

    // Overriding the MakeSound method for reptiles
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says: {Sound}. It's a reptile.");
    }
}

// Zoo class to manage animals
public class Zoo
{
    private List<Animal> animals;

    public Zoo()
    {
        animals = new List<Animal>();
    }

    // Method to add an animal to the zoo
    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
    }

    // Method to display information about all animals
    public void DisplayAnimals()
    {
        foreach (var animal in animals)
        {
            Console.WriteLine($"Name: {animal.Name}, Age: {animal.Age}");
            if (animal is Mammal mammal)
            {
                Console.WriteLine($"Fur Color: {mammal.FurColor}");
            }
            else if (animal is Bird bird)
            {
                Console.WriteLine($"Can Fly: {bird.CanFly}");
            }
            else if (animal is Reptile reptile)
            {
                Console.WriteLine($"Cold-Blooded: {reptile.IsColdBlooded}");
            }
            Console.WriteLine($"Endangered: {animal.IsEndangered}");
            animal.MakeSound();
        }
    }
}

// Main method to test the simulation
public class Program
{
    public static void Main()
    {
        Zoo zoo = new Zoo();

        // Create some animals and add them to the zoo
        Mammal lion = new Mammal("Lion", 5, "Roar", "Golden", false);
        Bird eagle = new Bird("Eagle", 3, "Screech", true, false);
        Reptile snake = new Reptile("Snake", 2, "Hiss", true, true);

        zoo.AddAnimal(lion);
        zoo.AddAnimal(eagle);
        zoo.AddAnimal(snake);

        // Display the animals' information
        Console.WriteLine("Welcome to the Zoo!\n");
        zoo.DisplayAnimals();

        // Feed the animals
        Console.WriteLine("\nFeeding the animals:");
        lion.Feed();
        eagle.Feed();
        snake.Feed();
    }
}
