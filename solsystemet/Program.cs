using System;
using System.Collections.Generic;
using SpaceSim;

class Astronomy
{
    public static void Main()
    {
        allSpaceObjects planets = new allSpaceObjects();
        planets.allPlanets();
        planets.allMoons();
        List<SpaceObject> solarSystem = planets.objectList;

        foreach (SpaceObject obj in solarSystem) { 
            obj.Draw();
            if (obj.children.Count != 0) {
                Console.WriteLine(" Moons: ");
                foreach (SpaceObject child in obj.children)
                {
                    child.Draw();
                }
            }
        }

        Console.ReadLine();
    }
}