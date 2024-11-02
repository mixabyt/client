using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class MainScreen : Form
    {

        private WebSocketClient _webSocketClient;
        private Label myLabel;
        private Button findbutton;
        public MainScreen()
        {
            InitializeComponent();


            _webSocketClient = new WebSocketClient("ws://16.171.206.175:8080/ws");
           

            // Підключення до WebSocket
            ConnectToWebSocket();

            myLabel = new Label();
            myLabel.Text = "Anonymous chat";
            myLabel.Font = new Font(myLabel.Font.FontFamily, 32);
            myLabel.AutoSize = true;

            
            this.Controls.Add(myLabel);
            this.Resize += new EventHandler(Form1_Resize);


           

            findbutton = new Button();
            findbutton.Text = "Знайти співрозмовника";
            findbutton.Name = "myButtonfindbutton";
            findbutton.Size = new Size(200, 50);
            findbutton.Font = new Font(myLabel.Font.FontFamily, 10, FontStyle.Bold);
            findbutton.BackColor = Color.FromArgb(42, 120, 245);
            findbutton.ForeColor = Color.White;
            findbutton.FlatStyle = FlatStyle.Flat;
            findbutton.FlatAppearance.BorderSize = 0;
            findbutton.Click += findbutton_Click;
            this.Controls.Add(findbutton);
            //findbutton.Font = new Font(myLabel.Font.FontFamily, 30);
            CenterLabel();
            this.VisibleChanged += MainScreen_VisibleChanged;


        }
        private void CenterLabel()
        {
            // Центруємо Label по середині форми
            myLabel.Location = new Point(
                (this.ClientSize.Width - myLabel.Width) / 2,
                (this.ClientSize.Height - myLabel.Height) / 4 + myLabel.Height/2
            );

            
            findbutton.Location = new Point(
                (this.ClientSize.Width - findbutton.Width) / 2,
                (this.ClientSize.Height - findbutton.Height) / 2 + findbutton.Height
            );
            useronlinelabel.Location = new Point(
                (this.ClientSize.Width - useronlinelabel.Width) / 2,
                (this.ClientSize.Height - useronlinelabel.Height) / 2 + 2*findbutton.Height - useronlinelabel.Height
            );



        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterLabel(); // Оновлюємо позицію Label при зміні розміру
        }





        public async void ConnectToWebSocket()
        {
            await _webSocketClient.Connect();
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
                        Debug.Print("from 1 form");
                        break;

                    case "subMainMenu":
                        Debug.Print("subMainMenu");
                        var jsonstr1 = json.GetRawText();
                        UpdateCountUser? message2 = JsonConvert.DeserializeObject<UpdateCountUser>(jsonstr1);
                        useronlinelabel.Text = $"users online: {message2.count}";
                        break;
                }
            }));
        }

        

        private void findbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            QueueScreen waitingscreen = new QueueScreen(this.Location.X, this.Location.Y, this._webSocketClient, this, this.Size);
            FindInterlocutor message = new(); message.type = "findInterlocutor";
            SubMainMenu message1 = new(); message1.type = "subMainMenu"; message1.subscription = false;
            this._webSocketClient.SendMessage(message);
            this._webSocketClient.SendMessage(message1);
            waitingscreen.Show();
            Debug.Print("tap!");
        }

        private void MainScreen_VisibleChanged(object sender, EventArgs e)
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

