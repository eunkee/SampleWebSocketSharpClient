using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using WebSocketSharp;

namespace SampleWebSocketSharpClient
{
    public partial class Form1 : Form
    {
        private WebSocket ws = null;

        public Form1()
        {
            InitializeComponent();
            ComboBoxEventType.SelectedIndex = 0;
        }

        #region method 
        private void SetWebSocket(string address)
        {
            ws = new WebSocket(address);

            ws.OnOpen += (sender, e) =>
            {
                Console.WriteLine("OnOpen");
                //Console.WriteLine("ReadyState = " + ws.ReadyState.ToString());
            };

            ws.OnClose += (sender, e) =>
            {
                Console.WriteLine("OnClose");
            };

            ws.OnMessage += (sender, e) =>
            {
                Console.WriteLine(e.Data);
            };
            ws.Connect();
        }
        #endregion method 

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            string address = TextBoxAddress.Text;
            if (address.Length > 0)
            {
                SetWebSocket(address);
            }
        }

        private void ButtonDisconnect_Click(object sender, EventArgs e)
        {
            if(ws != null)
            {
                if (ws.ReadyState != WebSocketState.Closed)
                {
                    ws.Close();
                }
            }
        }

        private void ButtonSubscribe_Click(object sender, EventArgs e)
        {
            if (ws.ReadyState == WebSocketState.Open)
            {
                string data = TextBoxCallData.Text;
                string eventType = ComboBoxEventType.Text;
                
                if (data.Length > 0 && eventType.Length > 0)
                {
                    try
                    {
                        ModelWebsocketCall we = new ModelWebsocketCall
                        {
                            Event = eventType,
                            Data = data
                        };

                        string json = JsonConvert.SerializeObject(we);
                        ws.Send(json);
                    }
                    catch { }
                }
                else
                {
                    MessageBox.Show("Not Input data");
                }
            }
            else
            {
                MessageBox.Show("Not Connected.");
            }
        }
    }
}
