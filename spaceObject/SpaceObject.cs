using System;
using System.Collections.Generic;

namespace SpaceSim
{
    public class SpaceObject
    {
        public String name { get; }
        public double orbitalRadius { get; set; }
        public double orbitalPeriod { get; set; }
        public double objectRadius { get; set; }
        public double rotationalPeriod { get; set; }
        public double xpos { get; set; }
        public double ypos { get; set; }
        public List<SpaceObject> children { get; set; }
        public string color { get; set; }

        public SpaceObject(String name, double radius)
        {
            this.name = name;
            this.objectRadius = radius;
        }
        public virtual void Draw()
        {
            Console.WriteLine(name);
        }

        public void calcPos(int time)
        {
            if (!(this is Star))
            {
                double orbitalSpeed = (2 * Math.PI * orbitalRadius) / orbitalPeriod;
                xpos = orbitalRadius + (Math.Cos(time * orbitalSpeed * 3.1416 / 180) * orbitalRadius);
                ypos = orbitalRadius + (Math.Sin(time * orbitalSpeed * 3.1416 / 180) * orbitalRadius);
            }

        }

    }

    public class Star : SpaceObject
    {
        public Star(String name, double radius, int rotPeriod, String color) : base(name, radius)
        {
            objectRadius = radius;
            rotationalPeriod = rotPeriod;
            this.color = color;
            xpos = 200;
            ypos = 200;
            children = new List<SpaceObject>();
        }
        public override void Draw()
        {
            Console.Write("Star  : ");
            base.Draw();
        }
    }

    public class Planet : SpaceObject
    {
        public SpaceObject orbits { get; set; }
        
        public Planet(String name, int radius, int rotPeriod, String color, int orbRadius, int orbPeriod, SpaceObject orbiting) : base(name, radius)
        {
            orbits = orbiting;
            objectRadius = radius;
            orbitalRadius = orbRadius;
            this.color = color;
            rotationalPeriod = rotPeriod;
            orbitalPeriod = orbPeriod;
            xpos = orbRadius;
            ypos = 0;
            children = new List<SpaceObject>();
        }
        public override void Draw()
        {
            Console.Write("Planet: ");
            base.Draw();
        }
    }

    public class Moon : SpaceObject
    {
        public SpaceObject orbits { get; set; }
        public Moon(String name, int radius, int rotPeriod, String color, int orbRadius, int orbPeriod, SpaceObject orbiting) :
                    base(name, radius)
        {
            orbits = orbiting;
            objectRadius = radius;
            orbitalRadius = orbRadius;
            this.color = color;
            rotationalPeriod = rotPeriod;
            orbitalPeriod = orbPeriod;
            xpos = orbRadius;
            ypos = 0;
        }

        public override void Draw()
        {
            Console.Write("     Moon  : ");
            base.Draw();
        }
    }

    public class Asteroid : SpaceObject 
    {
        public Asteroid(String name, double radius) : base(name, radius) { }

        public override void Draw()
        {
            Console.Write("Asteroid  : ");
            base.Draw();
        }
    }

    public class Comet : SpaceObject
    {
        public Comet(String name, double radius) : base(name, radius) { }

        public override void Draw()
        {
            Console.Write("Comet  : ");
            base.Draw();
        }
    }

    public class AsteroidBelt : Asteroid
    {
        public SpaceObject orbits { get; set; }
        List<Asteroid> asteroids { get; set; }
        public AsteroidBelt(String name, double radius, Planet orbits, int size) : base(name, radius)
        {
            this.orbits = orbits;
            createBelt(size);
        }

        public void createBelt(int size) {
            for (int i = 0; i < size; i++) {
                
            }
        }

        public override void Draw()
        {
            Console.Write("AsteroidBelt  : ");
            base.Draw();
        }
    }

    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(String name, int radius, int rotPeriod, String color, int orbRadius, int orbPeriod, SpaceObject orbiting) :
                           base(name, radius, rotPeriod, color, orbRadius, orbPeriod, orbiting)
        {
            children = new List<SpaceObject>();
        }

        public override void Draw()
        {
            Console.Write("DwarfPlanet  : ");
            base.Draw();
        }
    }

    public class allSpaceObjects
    {
        public List<SpaceObject> objectList = new List<SpaceObject>();
        

        public allSpaceObjects() {
            objectList.Add(new Star("Sun", 50, 0, "YELLOW"));
            objectList.Add(new Planet("Earth", 15, 2, "BLUE", 149600, 365, objectList[0]));
        }

        public void allPlanets() {
            objectList.Add(new Planet("Mercury", 3, 2, "ORANGE", 57910, 87, objectList[0]));
            objectList.Add(new Planet("Venus", 3, 2, "BROWN", 108200, 224, objectList[0]));
            objectList.Add(new Planet("Earth", 3, 2, "BLUE", 149600, 365, objectList[0]));
            objectList.Add(new Planet("Mars", 4, 24, "RED", 227940, 686, objectList[0]));
            objectList.Add(new Planet("Jupiter", 10, 500, "BEIGE", 778330, 4332, objectList[0]));
            objectList.Add(new Planet("Saturn", 10, 500, "BROWN", 1429400, 10759, objectList[0]));
            objectList.Add(new Planet("Uranus", 10, 500, "GREEN", 2870990, 30685, objectList[0]));
            objectList.Add(new Planet("Neptune", 10, 500, "BLUE", 4504300, 60190, objectList[0]));
            objectList.Add(new DwarfPlanet("Pluto", 10, 500, "GRAY", 5913520, 90550, objectList[0]));
        }

        public void allMoons()
        {
            for (int i = 1; i < objectList.Count; i++) {
                SpaceObject current = objectList[i];
                switch (current.name)
                {
                    case "Earth":
                        current.children.Add(new Moon("Moon", 10, 0, "GRAY", 384, 27, current));
                        break;
                    case "Mars":
                        current.children.Add(new Moon("Phobos", 1, 0, "GRAY", 9, 1, current));
                        current.children.Add(new Moon("Deimos", 1, 0, "GRAY", 23, 46023, current));
                        break;
                    case "Jupiter":
                        current.children.Add(new Moon("Metis", 1, 0, "GRAY", 128, 1, current));
                        current.children.Add(new Moon("Adrastea", 1, 0, "GRAY", 129, 1, current));
                        current.children.Add(new Moon("Amalthea", 1, 0, "GRAY", 181, 1, current));
                        current.children.Add(new Moon("Thebe", 1, 0, "GRAY", 222, 1, current));
                        current.children.Add(new Moon("Io", 1, 0, "GRAY", 422, 28126, current));
                        current.children.Add(new Moon("Europa", 1, 0, "GRAY", 671, 20149, current));
                        current.children.Add(new Moon("Ganymede", 1, 0, "GRAY", 1070, 42186, current));
                        current.children.Add(new Moon("Callisto", 1, 0, "GRAY", 1883, 16, current));
                        current.children.Add(new Moon("Leda", 1, 0, "GRAY", 11094, 238, current));
                        current.children.Add(new Moon("Himalia", 1, 0, "GRAY", 11480, 250, current));
                        current.children.Add(new Moon("Lysithea", 1, 0, "GRAY", 11720, 259, current));
                        current.children.Add(new Moon("Elara", 1, 0, "GRAY", 11737, 259, current));
                        current.children.Add(new Moon("Ananke", 1, 0, "GRAY", 21200, -631, current));
                        current.children.Add(new Moon("Carme", 1, 0, "GRAY", 22600, -692, current));
                        current.children.Add(new Moon("Pasiphae", 1, 0, "GRAY", 23500, -735, current));
                        current.children.Add(new Moon("Sinope", 1, 0, "GRAY", 23700, -758, current));
                        break;
                    case "Saturn":
                        current.children.Add(new Moon("Pan", 1, 0, "GRAY", 134, 1, current));
                        current.children.Add(new Moon("Atlas", 1, 0, "GRAY", 138, 1, current));
                        current.children.Add(new Moon("Prometheus", 1, 0, "GRAY", 139, 1, current));
                        current.children.Add(new Moon("Pandora", 1, 0, "GRAY", 142, 1, current));
                        current.children.Add(new Moon("Epimetheus", 1, 0, "GRAY", 151, 1, current));
                        current.children.Add(new Moon("Janus", 1, 0, "GRAY", 151, 1, current));
                        current.children.Add(new Moon("Mimas", 1, 0, "GRAY", 186, 1, current));
                        current.children.Add(new Moon("Enceladus", 1, 0, "GRAY", 238, 13516, current));
                        current.children.Add(new Moon("Moon", 1, 0, "GRAY", 295, 32509, current));
                        current.children.Add(new Moon("Telesto", 1, 0, "GRAY", 295, 32509, current));
                        current.children.Add(new Moon("Calypso", 1, 0, "GRAY", 295, 32509, current));
                        current.children.Add(new Moon("Dione", 1, 0, "GRAY", 377, 27061, current));
                        current.children.Add(new Moon("Helene", 1, 0, "GRAY", 377, 27061, current));
                        current.children.Add(new Moon("Rhea", 1, 0, "GRAY", 527, 19085, current));
                        current.children.Add(new Moon("Titan", 1, 0, "GRAY", 1222, 15, current));
                        current.children.Add(new Moon("Hyperion", 1, 0, "GRAY", 1481, 21, current));
                        current.children.Add(new Moon("Iapetus", 1, 0, "GRAY", 3561, 79, current));
                        current.children.Add(new Moon("Phoebe", 1, 0, "GRAY", 12952, -550, current));
                        break;
                    case "Uranus":
                        current.children.Add(new Moon("Cordelia", 1, 0, "GRAY", 50, 1, current));
                        current.children.Add(new Moon("Ophelia", 1, 0, "GRAY", 54, 1, current));
                        current.children.Add(new Moon("Bianca", 1, 0, "GRAY", 59, 1, current));
                        current.children.Add(new Moon("Cressida", 1, 0, "GRAY", 62, 1, current));
                        current.children.Add(new Moon("Desdemona", 1, 0, "GRAY", 63, 1, current));
                        current.children.Add(new Moon("Juliet", 1, 0, "GRAY", 64, 1, current));
                        current.children.Add(new Moon("Portia", 1, 0, "GRAY", 66, 1, current));
                        current.children.Add(new Moon("Rosalind", 1, 0, "GRAY", 70, 1, current));
                        current.children.Add(new Moon("Belinda", 1, 0, "GRAY", 75, 1, current));
                        current.children.Add(new Moon("Puck", 1, 0, "GRAY", 86, 1, current));
                        current.children.Add(new Moon("Miranda", 1, 0, "GRAY", 130, 14977, current));
                        current.children.Add(new Moon("Ariel", 1, 0, "GRAY", 191, 19025, current));
                        current.children.Add(new Moon("Umbriel", 1, 0, "GRAY", 266, 41730, current));
                        current.children.Add(new Moon("Titania", 1, 0, "GRAY", 436, 26146, current));
                        current.children.Add(new Moon("Oberon", 1, 0, "GRAY", 583, 13, current));
                        current.children.Add(new Moon("Caliban", 1, 0, "GRAY", 7169, -580, current));
                        current.children.Add(new Moon("Stephano", 1, 0, "GRAY", 7948, -674, current));
                        current.children.Add(new Moon("Sycorax", 1, 0, "GRAY", 12213, -1289, current));
                        current.children.Add(new Moon("Prospero", 1, 0, "GRAY", 16568, -2019, current));
                        current.children.Add(new Moon("Setebos", 1, 0, "GRAY", 17681, -2239, current));
                        break;
                    case "Neptune":
                        current.children.Add(new Moon("Naiad", 1, 0, "GRAY", 48, 1, current));
                        current.children.Add(new Moon("Thalassa", 1, 0, "GRAY", 50, 1, current));
                        current.children.Add(new Moon("Despina", 1, 0, "GRAY", 53, 1, current));
                        current.children.Add(new Moon("Galatea", 1, 0, "GRAY", 62, 1, current));
                        current.children.Add(new Moon("Larissa", 1, 0, "GRAY", 74, 1, current));
                        current.children.Add(new Moon("Triton", 1, 0, "GRAY", 355, -5, current));
                        current.children.Add(new Moon("Nereid", 1, 0, "GRAY", 5513, 360, current));
                        break;
                    case "Pluto":
                        current.children.Add(new Moon("Charon", 1, 0, "GRAY", 20, 14397, current));
                        current.children.Add(new Moon("Nix", 1, 0, "GRAY", 49, 24, current));
                        current.children.Add(new Moon("Nereid", 1, 0, "GRAY", 65, 38, current));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
