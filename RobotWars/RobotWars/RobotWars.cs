using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWars
{
    public class Robot //to create the robots with values and movement
    {
        public int x;
        public int y;
        public char direction;
        public string initialPosition;
        public string movement;
        public string finalPosition;

        
        public void turnLeft() //Robot will change it's direction 90 degrees towards left
        {
            if (direction == 'N')
            {
                direction = 'W';
            }
            else if (direction == 'W')
            {
                direction = 'S';
            }
            else if (direction == 'S')
            {
                direction = 'E';
            }
            else if (direction == 'E')
            {
                direction = 'N';
            }
            else
            {
                Console.WriteLine("Invalid initial direction. Direction set to North(N)");
                direction = 'N';
            }

        }

        public void turnRight() //Robot will change it's direction 90 degrees towards Right
        {
            if (direction == 'N')
            {
                direction = 'E';
            }
            else if (direction == 'E')
            {
                direction = 'S';
            }
            else if (direction == 'S')
            {
                direction = 'W';
            }
            else if (direction == 'W')
            {
                direction = 'N';
            }
            else
            {
                Console.WriteLine("Invalid initial direction. Direction set to North(N)");
                direction = 'N';
            }

        }

        public void moveNorth(int northBound) //to move north checking the Arena boundary limit
        {
            if(y < northBound)
            {
                y = y + 1;
            }
            else
            {
                Console.WriteLine("Arena Bounds Reached!");
            }
        }
        public void moveSouth() //to move south checking the Arena boundary limit
        {
            if (y > 0)
            {
                y = y - 1;
            }
            else
            {
                Console.WriteLine("Arena Bounds Reached!");
            }
        }
        public void moveEast(int eastBount) //to move east checking the Arena boundary limit
        {
            if (x < eastBount)
            {
                x = x + 1;
            }
            else
            {
                Console.WriteLine("Arena Bounds Reached!");
            }
        }
        public void moveWest() //to move west checking the Arena boundary limit
        {
            if (x > 0)
            {
                x = x - 1;
            }
            else
            {
                Console.WriteLine("Arena Bounds Reached!");
            }
        }


    }

    public class Arena //to create the arena
    {
        public string arenaSize;
        public int x;
        public int y;

        public void getArenaSize() // getting the arena size to be used later for boundary limits
        {
            arenaSize = Console.ReadLine();
            string[] values = arenaSize.Split(' ');
            x = Convert.ToInt32(values[0]);
            y = Convert.ToInt32(values[1]);

        }

    }
    public class War // to use arena and start a war between robots controlling their movements 
    {
        Arena newArena = new Arena();
        Robot robot1 = new Robot();
        Robot robot2 = new Robot();

        //Getting first line of user input for arena coordinates.
        public void getInputs()
        {
            newArena.getArenaSize();
            getRobotPosition(robot1);
            moveRobot(robot1);
            getRobotPosition(robot2);
            moveRobot(robot2);

        }

        public void getOutputs() //for printing out the final output
        {
            robot1.finalPosition = robot1.x + " " + robot1.y + " " + robot1.direction;
            robot2.finalPosition = robot2.x + " " + robot2.y + " " + robot2.direction;

            Console.WriteLine("Output : \n" +robot1.finalPosition + "\n" + robot2.finalPosition); 
        }

        public void getRobotPosition(Robot r)
        {
            
                r.initialPosition = Console.ReadLine(); // to get the initial position for the robot
                string[] values = r.initialPosition.Split(' '); //saving into an array to be updated later
                r.x = Convert.ToInt32(values[0]);
                r.y = Convert.ToInt32(values[1]);
                r.direction = Convert.ToChar(values[2]);
        }


        public void moveRobot(Robot r)
        {
            r.movement = Console.ReadLine(); // to read the movement instructions for the robot
            char[] moves = r.movement.ToCharArray();//adding every instruction in an array to move the robot accordingly
            foreach(char move in moves) // for every move, check the position and carry out the action 
            {
                if (move == 'L')
                {
                    r.turnLeft();
                }
                else if (move == 'R')
                {
                    r.turnRight();
                }
                else if (move == 'M')
                {
                    if (r.direction == 'E' )
                    {
                        r.moveEast(newArena.x);
                    }
                    else if (r.direction == 'W')
                    {
                        r.moveWest();
                    }
                    else if (r.direction == 'N')
                    {
                        r.moveNorth(newArena.y);
                    }
                    else if (r.direction == 'S')
                    {
                        r.moveSouth();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Direction");
                    }
                }

                else
                {
                    Console.WriteLine("Invalid Move!");
                }
            }
        }

    }
    class startWar
    {
        static void Main(string[] args)
        {
            War testWar = new War();
            testWar.getInputs();
            testWar.getOutputs();
            Console.ReadKey();

        }
    }
}
