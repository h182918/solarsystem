using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpaceSim;

namespace wpfSolarSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void delg(int time);
        private System.Windows.Threading.DispatcherTimer t;
        private allSpaceObjects allObjects;
        List<SpaceObject> list;
        int time = 0; 
        public event delg moveIt;
        public MainWindow()
        {
            InitializeComponent();
            allObjects = new allSpaceObjects();
            //allObjects.allPlanets();
            allObjects.allMoons();
            list = allObjects.objectList;
            t = new System.Windows.Threading.DispatcherTimer(); 
            t.Interval = new TimeSpan(2000000); 
            t.Tick += t_Tick; 
            t.Start();
            subscribePlanets();


        }
        void t_Tick(object sender, EventArgs e) { moveIt(time++); }

        void subscribePlanets() {
            moveIt += makePlanet;
            for (int i = 0; i < list.Count(); i++) {
                moveIt += list[i].calcPos;
            }
        }

        void makePlanet(int time) {
            ClearCanvasInfo();
            List<SpaceObject> test = list;
            for (int i = 0; i < list.Count(); i++)
            {
                Ellipse ellipse = makeSpaceObject(list[i].objectRadius, list[i].color);
                double currentx = list[i].xpos / 10000;
                double currenty = list[i].ypos / 10000;
                Canvas.SetLeft(ellipse, currentx);
                Canvas.SetTop(ellipse,currenty);
                if (list[i].children.Count != 0)
                {
                    foreach (SpaceObject child in list[i].children)
                    {
                        Ellipse childEllipse = makeSpaceObject(child.objectRadius, child.color);
                        Canvas.SetLeft(childEllipse, currentx + (child.xpos / 10000));
                        Canvas.SetTop(childEllipse, currenty + (child.ypos / 10000));
                        myCanvas.Children.Add(childEllipse);
                    }
                }
                myCanvas.Children.Add(ellipse);
            }
        }

        public Ellipse makeSpaceObject(double objectRadius, String color)
        {
            Ellipse ellipse = new Ellipse();
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = Color.FromArgb(0, 255, 255, 1);
            ellipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = Brushes.Black;
            ellipse.Width = objectRadius;
            ellipse.Height = objectRadius;

            return ellipse;
        }

        private void ClearCanvasInfo()
        {
            for (int i = myCanvas.Children.Count - 1; i >= 0; i += -1)
            {
                UIElement Child = myCanvas.Children[i];
                if (Child is Ellipse)
                    myCanvas.Children.Remove(Child);
            }
        }
    }
}

