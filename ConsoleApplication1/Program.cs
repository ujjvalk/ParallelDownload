using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (CheckForInternetConnection())
            {
                var list = new[] 
            { 
                "http://www.google.com",
                "https://www.facebook.com",
                "http://www.msn.com/",
                "https://www.yahoo.com/"
            };
                ServicePointManager.DefaultConnectionLimit = 200;
                Stopwatch t1 = new Stopwatch();
                var tasks = Parallel.ForEach(list,
                        s =>
                        {
                            using (var client = new WebClient())
                            {
                                t1.Start();
                                Console.WriteLine("starting to download {0} and Time is {1}.", s, t1.ElapsedMilliseconds);
                                string result = client.DownloadString((string)s);
                                Console.WriteLine("finished downloading {0} and Time is {1}.", s, t1.ElapsedMilliseconds);
                            }
                        });
            }
            else
            {
                Console.WriteLine("Internet Connection problem, check your internet connection.");
            }
            Console.ReadLine();
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }


    }
}
