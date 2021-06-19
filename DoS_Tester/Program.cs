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
            for (int i = 0; i < 100; i++)
            {
                Start(RunPage);
            }
            
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        public static Thread Start(Action action)
        {
            Thread thread = new Thread(() => { action(); });
            thread.Start();
            return thread;
        }

        public static void RunPage()
        {
            IWebDriver driver = new ChromeDriver("C:\\Users\\joshua.whitfield\\source\\repos\\DoS_Tester");
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                driver.Navigate().GoToUrl("http://localhost:51498/Reports/DueDiary/DueDiary.aspx");
                while (true)
                {
                    try
                    {
                        driver.FindElement(By.Id("MainContent_btnRefreshGV")).Click();
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