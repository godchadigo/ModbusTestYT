using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Machin1();
            Machin2();
            Machin3();
        }

        private void Machin1()
        {
            //機台身分初始化
            var plc = new HslCommunication.ModBus.ModbusTcpNet("127.0.0.1" , 502);
            //機台連線
            plc.ConnectServer();
            //啟動線程
            Task.Run(async () => {
                //無窮迴圈
                while (true)
                {
                    //讀取數據
                    var result = plc.ReadInt16("0", 1);
                    //把數據顯示到控制台面板
                    Console.WriteLine("機台1數據 : " + result.Content[0]);
                    //委派
                    this.BeginInvoke(new Action(delegate
                    {
                        //把數據結果顯示到Label標籤上
                        this.label1.Text = result.Content[0].ToString();
                    }));
                    //延遲0.1s
                    await Task.Delay(100);
                }
            });            

        }

        private void Machin2()
        {
            var plc = new HslCommunication.ModBus.ModbusTcpNet("127.0.0.1", 503);
            plc.ConnectServer();
            Task.Run(async () => {
                while (true)
                {
                    var result = plc.ReadInt16("0", 1);
                    Console.WriteLine("機台2數據 : " + result.Content[0]);
                    this.BeginInvoke(new Action(delegate
                    {
                        this.label2.Text = result.Content[0].ToString();
                    }));
                    await Task.Delay(100);
                }
            });

        }
        private void Machin3()
        {
            var plc = new HslCommunication.ModBus.ModbusTcpNet("127.0.0.1", 504);
            plc.ConnectServer();
            Task.Run(async () => {
                while (true)
                {
                    var result = plc.ReadInt16("0", 1);
                    Console.WriteLine("機台3數據 : " + result.Content[0]);
                    this.BeginInvoke(new Action(delegate
                    {
                        this.label3.Text = result.Content[0].ToString();
                    }));
                    await Task.Delay(100);
                }
            });

        }
    }
}
