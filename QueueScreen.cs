using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WinFormsApp2
{
    public partial class QueueScreen : Form
    {
        private WebSocketClient _webSocketClient;
        private MainScreen mainscreen;
        public QueueScreen(int x, int y, WebSocketClient client, MainScreen mainscreen, Size size)
        {
            InitializeComponent();

            this.Size = size;
            this.mainscreen = mainscreen;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            this._webSocketClient = client;

            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 35;
            this.FormClosing += Form2_FormClosing;

            this.Resize += new EventHandler(Form2_Resize);
            this.VisibleChanged += findInterlocutorScreen_VisibleChanged;
            CenterLabel();

        }




        private void CenterLabel()
        {

            progressBar1.Location = new Point(
                (this.ClientSize.Width - progressBar1.Width) / 2,
                (this.ClientSize.Height - progressBar1.Height) / 2 + progressBar1.Height
            );

            findinglabel.Location = new Point(
                (this.ClientSize.Width - findinglabel.Width) / 2,
                (this.ClientSize.Height - findinglabel.Height) / 2 - progressBar1.Height
            );

        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            CenterLabel(); // Оновлюємо позицію Label при зміні розміру
        }


        

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainscreen.Location = new Point(this.Location.X, this.Location.Y);
            mainscreen.Size = this.Size;
            mainscreen.StartPosition = FormStartPosition.Manual;
            SubMainMenu message = new(); message.type = "subMainMenu"; message.subscription = true;
            StopFindingInterlocutor stop = new();
            stop.type = "stopFindingInterlocutor";
            this._webSocketClient.SendMessage(stop);
            this._webSocketClient.SendMessage(message);

            mainscreen.Show();
        }

        private void OnMessageReceived(string messageType, JsonElement json)
        {
            Invoke(new Action(() =>
            {

                switch (messageType)
                {
                    case "heartbeat":
                        Debug.Print("heartbeat send");

                        HeartBeetMessage message1 = new();
                        message1.type = "heartbeat";
                        _webSocketClient.SendMessage(message1);
                        Debug.Print("from 2 form");
                        break;
                    case "findInterlocutor":
                        this.Hide();
                        ChatScreen chatscreen = new ChatScreen(this.Location.X, this.Location.Y, this._webSocketClient, this.mainscreen, this.Size);
                        chatscreen.Show();
                        
                        break;
                }
            }));
        }



        private void findInterlocutorScreen_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                _webSocketClient.OnMessageReceived -= OnMessageReceived;
                Debug.Print("hide"); // Відписка від подій WebSocket
            }
            else
            {
                _webSocketClient.OnMessageReceived += OnMessageReceived;
                Debug.Print("show!"); // Повторна підписка на події WebSocket
            }
        }
    }
}
