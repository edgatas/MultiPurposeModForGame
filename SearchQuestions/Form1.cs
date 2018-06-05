using System;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

using InSimDotNet;

namespace SearchQuestions
{
    public partial class resultsBox : Form
    {
        //inSimWork _inSim;
        CruiseEngine _inSim;
        string filePath;
        string[] allLines;

        public resultsBox()
        {
            AllocConsole();
            InitializeComponent();

            

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Run2();
                Thread.Sleep(1000);
            }).Start();

            //_inSim = new inSimWork();
            //_inSim.SendWelcomeMessage();
            //ReadAnswers();

            //new Thread(() =>
            //{
            //    while (true)
            //    {
            //        Thread.CurrentThread.IsBackground = true;
            //        Run();
            //        Thread.Sleep(1000);
            //    }
            //}).Start();
        }

        private void Run2()
        {
            //bool portOpen = false;
            //while (!portOpen)
            //{
            try
            {
                _inSim = new CruiseEngine();
                //portOpen = true;
                infoText.Text = @"Connected to LFS";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                infoText.Text = @"Port is closed. Please open Port 29999 in LFS with command /insim 29999";
                //portOpen = false;
                Thread.Sleep(1000);
            }
            //}
        }

        private void GetFile()
        {
            //var openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            //int size = -1;

            //DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            //if (result == DialogResult.OK) // Test result.
            //{
                //filePath = openFileDialog1.FileName;
            filePath = "C:\\Users\\edgar\\Desktop\\Viktorina.txt";
            pickFile.BackColor = System.Drawing.Color.Green;
            //}
            searchBox.Clear();
        }

        private void putTextToBox(string[] text)
        {
            int numberOfLines = text.Length;

            StringBuilder questions = new StringBuilder();
            for (int i = 0; i < numberOfLines; i++)
            {
                questions.Append("\r\n" + text[i]);
            }

            allQuestions.Text = questions.ToString();
        }

        private void ReadFile()
        {
            try
            {
                allLines = File.ReadAllLines(filePath);
            }
            catch(IOException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ReadAnswers()
        {
            GetFile();
            ReadFile();
            putTextToBox(allLines);
        }

        private void pickFile_Click(object sender, EventArgs e)
        {
            GetFile();
            ReadFile();
            putTextToBox(allLines);
            searchBox.Focus();
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(int hwnd);

        private void Run()
        {
            String normalQuestion = "";
            String[] chat = _inSim.GetChat();
            for (int i = 99; i > 0; i--)
            {
                if (chat[i] != null)
                {
                    if (chat[i].StartsWith("Klausimas ("))
                    {
                        String[] newQuestion2 = chat[i].Split(':');
                        normalQuestion = newQuestion2[1].TrimStart(' ');
                        break;
                    }
                    else
                    {
                        _inSim.ClearLine(i);
                    }
                }
            }

            //String[] newQuestion = line.Split(':');
            //String normalQuestion = newQuestion[1].TrimStart(' ');

            normalQuestion = normalQuestion.Replace((char)731, 'ž');
            normalQuestion = normalQuestion.Replace((char)711, 'Ž');
            normalQuestion = normalQuestion.Replace((char)248, 'ų');
            normalQuestion = normalQuestion.Replace((char)154, 'š');
            normalQuestion = normalQuestion.Replace((char)138, 'Š');
            normalQuestion = normalQuestion.Replace((char)224, 'ą');
            normalQuestion = normalQuestion.Replace((char)235, 'ė');
            normalQuestion = normalQuestion.Replace((char)240, 'š');
            normalQuestion = normalQuestion.Replace((char)254, 'ž');
            normalQuestion = normalQuestion.Replace((char)225, 'į');
            normalQuestion = normalQuestion.Replace((char)363, 'ū');
            normalQuestion = normalQuestion.Replace((char)353, 'š');

            //int number1 = 1;

            //Console.WriteLine("Odd Character: " + normalQuestion[number1] + normalQuestion[number1 + 1]);
            //Console.WriteLine("Odd Character number: " + (int)normalQuestion[number1]);
            //Console.WriteLine("Odd Character: " + "ą");
            //Console.WriteLine("Odd Character number: " + (int)'ą');
            searchBox.Text = normalQuestion;
        }

        private void testing_Click(object sender, EventArgs e)
        {
            Run();
        }


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        private void searchBox_TextChanged_1(object sender, EventArgs e)
        {
            String searchText = searchBox.Text;
            if (searchText.Length < 2)
            {

            }
            else
            {
                StringBuilder result = new StringBuilder();
                bool firstRun = true;
                int resultCount = 0;
                String firstResult = "None? True";

                int numberOfLines = allLines.Length;
                for (int i = 0; i < numberOfLines; i++)
                {
                    char[] q1 = searchText.ToCharArray();
                    char[] q2 = allLines[i].ToCharArray();
                    bool check = true;

                    int comparinsonLength;

                    if (q1.Length < q2.Length)
                    {
                        comparinsonLength = q1.Length;
                    }
                    else
                    {
                        comparinsonLength = q2.Length;
                    }

                    for (int j = 0; j < comparinsonLength; j++)
                    {
                        if (q1[j].CompareTo(q2[j]) != 0)
                        {
                            check = false;
                        }
                    }

                    if (check)
                    {
                        if (firstRun)
                        {
                            result.Append(allLines[i]);
                            firstRun = false;
                            firstResult = allLines[i];
                            resultCount++;
                        }
                        else
                        {
                            result.Append("\r\n" + allLines[i]);
                        }
                    }
                }

                allQuestions.Text = result.ToString();

                String[] sep = firstResult.Split('?');

                if (resultCount == 1)
                {
                    //_inSim.SendLocalMessage("Answer found");
                    answer.Text = "!q" + sep[1];
                    String end = answer.Text;
                    //end = end.Replace((char)731, 'ž');
                    //end = end.Replace((char)711, 'Ž');
                    //end = end.Replace((char)248, 'ų');
                    //end = end.Replace((char)154, 'š');
                    //end = end.Replace((char)138, 'Š');
                    //end = end.Replace((char)224, 'ą');
                    //end = end.Replace((char)235, 'ė');
                    //end = end.Replace((char)240, 'š');
                    //end = end.Replace((char)254, 'ž');
                    //end = end.Replace((char)225, 'į');
                    //end = end.Replace((char)363, 'ū');
                    //end = end.Replace((char)353, 'š');
                    _inSim.SendAnswer(end);
                    _inSim.ClearHistory();
                }
                else
                {
                    //_inSim.SendLocalMessage("Answer not found");
                }
            }
        }
    }
}
