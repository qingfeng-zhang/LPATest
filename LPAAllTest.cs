using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LPATest
{
    class LPAAllTest : INotifyPropertyChanged
    {
        private int maxloglength = 8000;
        static public volatile bool bTerminate;//non optimise so that other thread can change this values


        Thread  Printer2Thread;
        Thread Printer4Thread;
        Thread Printer7Thread;

        public void stop()
        {
            bTerminate = true;
            Printer2Thread.Interrupt();
            Printer2Thread.Join();
            Printer4Thread.Join();
            Printer7Thread.Join();
        }
        public void startPrinterTest()
        {
            bTerminate = false;
            Printer2Thread = new Thread(new ParameterizedThreadStart(Printer2testTh));
            Printer2Thread.Start();
            Printer4Thread = new Thread(new ParameterizedThreadStart(Printer4testTh));
            Printer4Thread.Start();
            Printer7Thread = new Thread(new ParameterizedThreadStart(Printer7testTh));
            Printer7Thread.Start();
        }
        public void Printer2testTh(object msgobj)
        {
            String printerIP= "192.168.3.224";
            Logger log = LogManager.GetLogger("Test_Printer" + printerIP);

            printer2log = "IP=" + printerIP;
            log.Info("IP=" + printerIP);
            while (!bTerminate)
            {
                DateTime startTime = DateTime.Now;
                 
                Ping PingPrinter2 = new Ping();

                PingReply PR2 = PingPrinter2.Send(printerIP);

                DateTime endTime = DateTime.Now;
                if (PR2.Status == IPStatus.DestinationHostUnreachable || PR2.Status == IPStatus.TimedOut)
                {
                    printer2log = "ST2 Ping Failed,Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds;
                    log.Error("ST2 Ping Failed, Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds);
                }
                else
                {
                    printer2log = "ST2 Printer Ping OK!,  Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds;
                    log.Info("ST2 Printer Ping OK!,   Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds);
                }

                Thread.Sleep(30*1000);
            }

        }
        public void Printer4testTh(object msgobj)
        {
            String printerIP = "192.168.3.225";
            Logger log = LogManager.GetLogger("Test_Printer" + printerIP);

            printer4log = "IP=" + printerIP;
            log.Info("IP=" + printerIP);
            while (!bTerminate)
            {
                DateTime startTime = DateTime.Now;

                Ping PingPrinter2 = new Ping();

                PingReply PR2 = PingPrinter2.Send(printerIP);

                DateTime endTime = DateTime.Now;
                if (PR2.Status == IPStatus.DestinationHostUnreachable || PR2.Status == IPStatus.TimedOut)
                {
                    printer4log = "ST4 Ping Failed, Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds;
                    log.Error("ST4 Ping Failed, Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds);
                }
                else
                {
                    printer4log = "ST4 Printer Ping OK!,  Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds;
                    log.Info("ST4 Printer Ping OK!,   Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds);
                }

                Thread.Sleep(20 * 1000);
            }
        }
        public void Printer7testTh(object msgobj)
        {
            String printerIP = "192.168.3.226";
            Logger log = LogManager.GetLogger("Test_Printer" + printerIP);

            printer7log = "IP=" + printerIP;
            log.Info("IP=" + printerIP);
            while (!bTerminate)
            {
                DateTime startTime = DateTime.Now;

                Ping PingPrinter2 = new Ping();

                PingReply PR2 = PingPrinter2.Send(printerIP);

                DateTime endTime = DateTime.Now;
                if (PR2.Status == IPStatus.DestinationHostUnreachable || PR2.Status == IPStatus.TimedOut)
                {
                    printer7log = "ST7 Ping Failed, Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds;
                    log.Error("ST7 Ping Failed, Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds);
                }
                else
                {
                    printer7log = "ST7 Printer Ping OK!,   Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds;
                    log.Info("ST7 Printer Ping OK!,   Time used(ms): " + endTime.Subtract(startTime).TotalMilliseconds);
                }

                Thread.Sleep(30 * 1000);
            }
        }
        private string _printer2log = DateTime.Now.ToShortTimeString() + " ST2 log initialized";
        private string _printer2logPrevious = "0";
        public string printer2log
        {
            get { return _printer2log; }
            set
            {
                if (CheckIfNullorRepeated(value, _printer2logPrevious))
                {
                    _printer2logPrevious = value;
                    _printer2log = IndentToNextline(value, _printer2log);
                    OnPropertyChanged("printer2log");
                }   
            }
        }
        private string _printer4log = DateTime.Now.ToShortTimeString() + " ST4 log initialized";
        private string _printer4logPrevious = "0";
        public string printer4log
        {
            get { return _printer4log; }
            set
            {
                if (CheckIfNullorRepeated(value, _printer4logPrevious))
                {
                    _printer4logPrevious = value;
                    _printer4log = IndentToNextline(value, _printer4log);
                    OnPropertyChanged("printer4log");
                }
            }
        }
        private string _printer7log = DateTime.Now.ToShortTimeString() + " ST7 log initialized";
        private string _printer7logPrevious = "0";
        public string printer7log
        {
            get { return _printer7log; }
            set
            {
                if (CheckIfNullorRepeated(value, _printer7logPrevious))
                {
                    _printer7logPrevious = value;
                    _printer7log = IndentToNextline(value, _printer7log);
                    OnPropertyChanged("printer7log");
                }
            }
        }
        public string IndentToNextline(string NewValue, string OldValue)
        {
            OldValue = StringShortener(OldValue);
            return DateTime.Now.ToShortTimeString() + " " + NewValue + "\r\n" + OldValue;
        }
        public bool CheckIfNullorRepeated(string value, string previoussting)
        {
            if (value != "" && value != previoussting)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string StringShortener(string thestring)
        {
            if (thestring.Length > maxloglength)
            {
                thestring = thestring.Substring(0, maxloglength);
            }
            return thestring;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
