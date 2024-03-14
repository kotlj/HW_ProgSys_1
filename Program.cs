using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskStack
{
    class Task1
    {
        public enum MessageBoxResult : uint
        {
            Ok = 1,
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern MessageBoxResult MessageBox(IntPtr hwnd, String text, String caption, uint type);
        public static void task1()
        {
            MessageBox(IntPtr.Zero,
                "Next messageBoxes will show person information(All coincidences are coincidental)",
                "Zero",
                0);
            MessageBox(IntPtr.Zero, "Name: RandomName.Html", "NameIndex=0.2", 0);
            MessageBox(IntPtr.Zero, "SurName: Error:EmptyPath", "Surname.txt=\"D:\\\"", 0);
            MessageBox(IntPtr.Zero, "PersonalInformation: Not Found", "Id/Name/Surname/PersonalData", 0);
        }
    }

    class Task2
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, String lParam);

        public static IntPtr FindWindow(string caption)
        {
            return FindWindow(String.Empty, caption);
        }

        const uint WM_SETTEXT = 0x000C;
        const uint WM_CLOSE = 0x0010;
        const uint WM_DESTROY = 0x0002;


        public static void task2()
        {
            Console.WriteLine("Enter window name:\t");
            string wName = Console.ReadLine();
            Console.WriteLine("Enter window class(Good luck, need win spy for this...)");
            string wClass = Console.ReadLine();
            IntPtr hwnd = FindWindow("WindowsForms10.Window.8.app.0.141b42a_r8_ad1", wName);
            if (hwnd != IntPtr.Zero)
            {
                char choise = '0';
                Console.WriteLine("\n1 - change name\n2 - close window\n3 - destoy window\nelse - end without changes");
                choise = (Console.ReadLine())[0];
                if (choise == '1')
                {
                    Console.WriteLine("Enter new text");
                    string userText = Console.ReadLine();
                    IntPtr text = Marshal.StringToCoTaskMemUni(userText);
                    SendMessage(hwnd, WM_SETTEXT, IntPtr.Zero, userText);
                }
                else if (choise == '2')
                {
                    SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
                else if (choise == '3')
                {
                    SendMessage(hwnd, WM_DESTROY, IntPtr.Zero, IntPtr.Zero);
                }
                else
                {
                    Console.WriteLine("End without changes");
                }
            }
            else
            {
                Console.WriteLine("Error: Window not found");
            }
        }

    }

    class Task3
    {
        [DllImport("user32.dll")]
        public static extern bool MessageBeep(UInt64 uType);

        const UInt64 MB_ICONERROR = 0x00000010L;
        const UInt64 MB_ICONQUESTION = 0x00000020L;
        public static void task3()
        {
            MessageBeep(MB_ICONERROR);
            Thread.Sleep(1000);
            MessageBeep(MB_ICONERROR);
            Thread.Sleep(1000);
            MessageBeep(MB_ICONERROR);
            Thread.Sleep(1000);
            MessageBeep(MB_ICONQUESTION);
            Thread.Sleep(1000);
            MessageBeep(MB_ICONERROR);
        }

    }
}

namespace HW_ProgSys_1
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            char choise = '1';
            while(choise != '0')
            {
                Console.WriteLine("Enter the:\n1 - task1\n2 - task2\n3 - task3\n0 - exit");
                choise = (Console.ReadLine())[0];
                if (choise == '1')
                {
                    TaskStack.Task1.task1();
                }
                else if (choise == '2')
                {
                    TaskStack.Task2.task2();
                }
                else if (choise == '3')
                {
                    TaskStack.Task3.task3();
                }
                else if (choise == '0')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Unknown option");
                }
            }
        }
    }
}
