using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp2
{
    public partial class ChatScreen : Form
    {
        private WebSocketClient _webSocketClient;
        MainScreen mainscreen;
        private bool InterlocutorLeaved = false;
        private int messageOffset = 10;
        public ChatScreen(int x, int y, WebSocketClient client, MainScreen mainscreen, Size size)
        {
            InitializeComponent();
            this.Size = size;
            this.mainscreen = mainscreen;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            this._webSocketClient = client;

            this.FormClosing += Form3_FormClosing;
            this.Resize += new EventHandler(Form3_Resize);
            this.VisibleChanged += ChatScreen_VisibleChanged;
            
            this.panel1.AutoScroll = true;




            CenterLabel();
        }



        private void AddMessage(string message, bool who)
        {
            // Створення нового повідомлення як Label
            Label messageLabel = new Label();
            messageLabel.Text = who ? $"me: {message}" : $"anonym: {message}"; 
            messageLabel.AutoSize = true;
            messageLabel.MaximumSize = new Size(panel1.Width - 20, 0); // Встановлює максимальну ширину для переносів
            messageLabel.Location = new Point(10, messageOffset);

            // Додаємо повідомлення до панелі
            panel1.Controls.Add(messageLabel);

            // Оновлюємо відстань для наступного повідомлення
            messageOffset += messageLabel.Height + 10;

            // Прокрутка до останнього повідомлення
            panel1.VerticalScroll.Value = panel1.VerticalScroll.Maximum;
            panel1.PerformLayout();
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
                        Debug.Print("from 3 form");
                        break;

                    case "message":
                        string jsonstr = json.GetRawText();
                        TextMessage? message = JsonConvert.DeserializeObject<TextMessage>(jsonstr);
                        AddMessage(message.text, false);
                        break;

                    case "roomDeletionNotice":
                        this.InterlocutorLeaved = !this.InterlocutorLeaved;
                        MessageBox.Show("співрозмовник покинув чат", "Заголовок", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                }
            }));
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            CenterLabel();
        }
        private void CenterLabel()
        {
            textBox1.Size = new Size(656 + this.Width - 816, 35);
            button1.Location = new Point(710 + this.Width - 816, 12);
            button2.Location = new Point(666 + this.Width - 816, 12);
            panel1.Size = new Size(800 + this.Width - 816, 360 + this.Height - 489);
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ChatScreen_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                _webSocketClient.OnMessageReceived -= OnMessageReceived;
                Debug.Print("hide"); // Відписка від подій WebSocket
            }
            else
            {
                _webSocketClient.OnMessageReceived += OnMessageReceived;
                InterlocutorLeaved = false; // баги тут
                Debug.Print("show!"); // Повторна підписка на події WebSocket
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AddMessage(textBox1.Text, true);
            TextMessage message = new();
            message.type = "message";
            message.text = textBox1.Text;
            if (!InterlocutorLeaved) { this._webSocketClient.SendMessage(message); }
            textBox1.Clear();

        }

        

        private void leavechatbutton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ви хочете покинути діалог?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var message = new LeaveDialog();
                message.type = "leaveDialog";
                this.Hide();
                mainscreen.Location = new Point(this.Location.X, this.Location.Y);
                mainscreen.Size = this.Size;
                mainscreen.StartPosition = FormStartPosition.Manual;
                SubMainMenu message1 = new(); message1.type = "subMainMenu"; message1.subscription = true;
                this._webSocketClient.SendMessage(message);
                this._webSocketClient.SendMessage(message1);
                mainscreen.Show();
            }
            
        }
    }
}
