using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        class row
        {
            public double time;
            public double acceleration;
            public double velocity;
            public double altitude;
        }
        List<row> table = new List<row>();

        public Form1()
        {
            InitializeComponent();
        }

        private void calculateCurrent()
        {
            for (int i=1; i < table.Count; i++)
            {
                double dQ = table[i].acceleration - table[i - 1].acceleration;
                double dt = table[i].time - table[i - 1].time;
                table[i].velocity = dQ / dt;
            }
        }
        private void calculateDCurrent()
        {
            for (int i = 2; i < table.Count; i++)
            {
                double dI = table[i].acceleration - table[i - 1].acceleration;
                double dt = table[i].time - table[i - 1].time;
                table[i].velocity = dI / dt;
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "csv files|*.csv";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        string line = sr.ReadLine();
                        while (!sr.EndOfStream)
                        {
                            table.Add(new row());
                            string[] r = sr.ReadLine().Split(',');
                            table.Last().time = double.Parse(r[0]);
                            table.Last().altitude = double.Parse(r[1]);
                        }
                    }
                    calculateCurrent();
                    calculateDCurrent();
                }
                catch (IOException)
                {
                    MessageBox.Show(openFileDialog1.FileName + " failed to open.");
                }
                catch (FormatException)
                {
                    MessageBox.Show(openFileDialog1.FileName + " is not in the required format");
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show(openFileDialog1.FileName + " is not in the required format");
                }
                catch (DivideByZeroException)
                {
                    MessageBox.Show(openFileDialog1.FileName + "has rows that have the same time");
                }
               
                
                
                
                }
            }

        private void currentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;


        }

        private void chart1_Click(object sender, EventArgs e)
        {
           
        }
    }
    }

