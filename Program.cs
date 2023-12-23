using System.Runtime.CompilerServices;

namespace Program
{
    class Vectors
    {
        private double magnitude;
        private double angle;
        private double modifier;
        public Vectors()
        {
            magnitude = 0.0;
            angle = 0.0;
            modifier = 0.0;
        }
        public double getMagnitude()
        {
            return magnitude;
        }
        public double getAngle()
        {
            return angle;
        }
        public double getModifier()
        {
            return modifier;
        }
        public void setMagnitude(double magnitude)
        {
            this.magnitude = magnitude;
        }
        public void setAngle(double angle)
        {
            this.angle = angle;
        }
        public void setModifier(double modifier)
        {
            this.modifier = modifier;
        }
        public double[] calcSubvectors()
        {
            double[] subvectors = [(double)(magnitude * Math.Cos(angle)), (double)(magnitude * Math.Sin(angle))];
            return subvectors;
        }
        public static double calcAngle(double Rx, double Ry)
        {
            double absolute = Math.Abs(Ry / Rx);
            double radians = Math.Atan(absolute);
            double theta = (radians * 180) / Math.PI;
            if (Rx > 0 && Ry > 0)
            {
                return theta;
            }
            else if (Rx < 0 && Ry > 0)
            {
                return 180 - theta;
            }
            else if (Rx < 0 && Ry < 0)
            {
                return theta + 180;
            }
            else
            {
                return 360 - theta;
            }
        }
        public static double calcMagnitude(double Rx, double Ry)
        {
            return Math.Sqrt(Rx * Rx + Ry * Ry);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            bool go = true;
            while (go)
            {
                int count = getNumVectors();
                double[] magnitude = new double[count];
                double[] magnitudeMod = new double[count];
                double[] angle = new double[count];

                Console.WriteLine("What unit of mesurement are you using? ");
                string unit = "" + Console.ReadLine();

                for (int vectorIdx = 0; vectorIdx < angle.Length; vectorIdx++)
                {
                    magnitude[vectorIdx] = getMagnitude(vectorIdx);
                    magnitudeMod[vectorIdx] = getMangnitudeMod(vectorIdx);
                    angle[vectorIdx] = getAngle(vectorIdx);
                }
                Vectors newVector = addVectors(createVectorObjects(magnitude, magnitudeMod, angle));
                Console.WriteLine("The new vector is " + Math.Round(newVector.getMagnitude(), 3) + unit + " @ " + Math.Round(newVector.getAngle(), 3) + "°");

                Console.WriteLine();
                Console.WriteLine("Calculate another? (y/N)");
                string choice = "" + Console.ReadLine();
                switch(choice.ToLower())
                {
                    case "y":
                        break;
                    case "n":
                        go = false;
                        break;
                    case "":
                        go = false;
                        break;
                    default:
                        go = false;
                        break;
                }
            }
        }
        static int parseInt(string s)
        {
            bool valid = false;
            while (!valid)
            {
                try
                {
                    return int.Parse(s);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to convert '{s}' to an integer vlaue.");
                    Console.WriteLine("Please enter and integer: ");
                    s = "" + Console.ReadLine();
                }
            }
            return 0;  
        }
        static double parseDouble(string s)
        {
            try
            {
                return Double.Parse(s);
            }
            catch(FormatException)
            {
                Console.WriteLine($"Unable to convert '{s}' to a double.");
                return -1;
            }
        }
        static int getNumVectors()
        {
            Console.WriteLine("How many vectors are you adding? ");
            return parseInt("" + Console.ReadLine());
        }
        static double getMagnitude(int idx)
        {
            Console.WriteLine("Magnitude of vector " + (idx+1) + ": ");
            return parseDouble("" + Console.ReadLine());
        }
        static double getMangnitudeMod(int idx)
        {
            Console.WriteLine("Magnitude multiplier for vector " + (idx+1) + ": ");
            return parseDouble("" + Console.ReadLine());
        }
        static double getAngle(int idx)
        {
            Console.WriteLine("Mathmatical angle of vector " + (idx+1) + ": ");
            bool isLessThan361 = false;
            while (!isLessThan361)
            {
                double angl = parseDouble("" + Console.ReadLine());
                if (angl > 360)
                {
                    Console.WriteLine("Enter a vlaue between 0 and 360.");
                }
                else
                {
                    isLessThan361 = true;
                    return (Math.PI * angl) / 180;
                }
            }
            return 0;
        }
        static Vectors[] createVectorObjects(double[] magnitude, double[] magnitudeMod, double[] angle)
        {
            Vectors[] vectors = new Vectors[magnitude.Length];
            for(int idx = 0; idx < vectors.Length; idx++)
            {
                vectors[idx] = new Vectors();
                vectors[idx].setMagnitude(magnitude[idx]);
                vectors[idx].setAngle(angle[idx]);
                vectors[idx].setModifier(magnitudeMod[idx]);
            }
            return vectors;
        }
        static Vectors addVectors(Vectors[] vectors) 
        {
            Vectors finalVector = new Vectors();
            double subXSum = 0;
            double subYSum = 0;

            for(int idx = 0;idx < vectors.Length; idx++)
            {
                double[] subVectors = vectors[idx].calcSubvectors();
                subXSum += subVectors[0];
                subYSum += subVectors[1];
            }

            double theta = Vectors.calcAngle(subXSum, subYSum);
            double newMagnitude = Vectors.calcMagnitude(subXSum, subYSum);

            finalVector.setMagnitude(newMagnitude);
            finalVector.setModifier(1);
            finalVector.setAngle(theta);

            return finalVector;
        }
    }
}