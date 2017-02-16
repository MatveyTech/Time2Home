using System;
using System.Net;
using System.Threading;
using System.Xml;

namespace GMAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            String origin = "32.482023, 34.943980";
            String destination = "33.004898, 35.107219";
            //int departure_time = GetMiliseconds(18,0);
            string address = $"https://maps.googleapis.com/maps/api/directions/xml?origin={origin}&destination={destination}&departure_time=now&traffic_model=best_guess&mode=driving&key=AIzaSyAz0CuotC_swamnYuo18WX5wNXrW4If8ls";
            string text;
            WebClient client = new WebClient();
            while (true)
            {
                text = client.DownloadString(address);
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(text);
                XmlNodeList elemList = xDoc.GetElementsByTagName("duration_in_traffic");
                string st_output = elemList[0].ChildNodes[0].InnerText;
                int output;
                Int32.TryParse(st_output, out output);
                TimeSpan ts = TimeSpan.FromSeconds(output);

                string h = ts.Hours.ToString("00");
                string m = ts.Minutes.ToString("00");
                string s = ts.Seconds.ToString("00");
                Console.WriteLine($"{h}:{m}:{s}");

                Thread.Sleep(3000);
            }
        }

        private static int GetMiliseconds(int h, int m)
        {
            return 0;
            //TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            //return span.TotalSeconds;
        }
    }
}
