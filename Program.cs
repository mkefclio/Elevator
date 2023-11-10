using System;
using System.Collections.Generic;
using System.Linq;

class Elevator
{
    private int totalFloors;
    private int capacity;
    private int currentFloor;
    private List<int> upQueue;
    private List<int> downQueue;
    private List<int> intermediateStops;

    public Elevator(int totalFloors, int capacity)
    {
        this.totalFloors = totalFloors;
        this.capacity = capacity;
        currentFloor = 0;
        upQueue = new List<int>();
        downQueue = new List<int>();
        intermediateStops = new List<int>();
    }

    public void RequestElevator(int destination)
    {
        if (destination == currentFloor)
        {
            Console.WriteLine($"Person on floor {destination} is already on the same floor.");
            return;
        }

        if (destination > currentFloor)
        {
            upQueue.Add(destination);
            upQueue.Sort();
        }
        else
        {
            downQueue.Add(destination);
            downQueue.Sort((a, b) => b.CompareTo(a));
        }

        ProcessQueue();
    }

    private void ProcessQueue()
    {
        while (upQueue.Count > 0 || downQueue.Count > 0)
        {
            if (currentFloor < totalFloors && upQueue.Count > 0)
            {
                MoveUp();
            }
            else if (currentFloor > 0 && downQueue.Count > 0)
            {
                MoveDown();
            }
            else
            {
                Console.WriteLine("Elevator is waiting at the current floor.");
                break;
            }
        }
    }

    private void MoveUp()
    {
        currentFloor++;
        intermediateStops.Add(currentFloor);
        Console.WriteLine($"Elevator moves up to floor {currentFloor}");
        upQueue.Remove(currentFloor);

        if (intermediateStops.Count == capacity || upQueue.Count == 0)
        {
            intermediateStops.Clear();
            ProcessQueue();
        }
    }

    private void MoveDown()
    {
        currentFloor--;
        intermediateStops.Add(currentFloor);
        Console.WriteLine($"Elevator moves down to floor {currentFloor}");
        downQueue.Remove(currentFloor);

        if (intermediateStops.Count == capacity || downQueue.Count == 0)
        {
            intermediateStops.Clear();
            ProcessQueue();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int totalFloors = 5;  // Αντικαταστήστε τον αριθμό των ορόφων όπως απαιτείται
        int capacity = 3;     // Αντικαταστήστε τη χωρητικότητα του ανελκυστήρα όπως απαιτείται

        Elevator elevator = new Elevator(totalFloors, capacity);

        // Προσομοιωμένα αιτήματα επιβίβασης από τους ορόφους
        elevator.RequestElevator(2);
        elevator.RequestElevator(3);
        elevator.RequestElevator(1);
        elevator.RequestElevator(4);
        elevator.RequestElevator(0);

        // Προσομοιώστε περισσότερα αιτήματα επιβίβασης κατά την ανάπτυξη του προγράμματος

        Console.WriteLine("Elevator has finished all requests.");
    }
}
