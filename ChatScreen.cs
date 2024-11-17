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
using System.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
namespace WinFormsApp2
{
    public partial class ChatScreen : Form
    {
        private WebSocketClient _webSocketClient;
        MainScreen mainscreen;
        private bool InterlocutorLeaved = false;
        private int messageOffset = 10;
        private Image? selectedImage = null;
        private bool isPlaceholder = true;
        public ChatScreen(int x, int y, WebSocketClient client, MainScreen mainscreen, Size size)
        {
            InitializeComponent();
            this.Size = size;
            sendbutton.Anchor = AnchorStyles.Right;
            this.mainscreen = mainscreen;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            this._webSocketClient = client;

            this.FormClosing += Form3_FormClosing;
            this.Resize += new EventHandler(Form3_Resize);
            this.VisibleChanged += ChatScreen_VisibleChanged;


            textBox1.ForeColor = Color.Gray;
            isPlaceholder = true;
            textBox1.Text = "Повідомлення";

            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;

            chatPanel.Height = this.ClientSize.Height - buttompanel.Height - 100;
            chatPanel.Width = this.ClientSize.Width + 35;
            textBox1.Size = new Size(656 + this.Width - 816, 35);
            sendbutton.Location = new Point(710 + this.Width - 816, 14);
            photobutton.Location = new Point(666 + this.Width - 816, 12);


            //CenterLabel();
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (isPlaceholder)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
                isPlaceholder = false;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Повідомлення";
                textBox1.ForeColor = Color.Gray;
                isPlaceholder = true;
            }
        }






        private void AddTextMessage(string message, bool alignRight)
        {
            // Обчислюємо новий messageOffset
            UpdateMessageOffset();

            Label messageLabel = new Label
            {
                MaximumSize = new Size(chatPanel.Width / 2, 0),
                Text = message,
                AutoSize = true,
                Padding = new Padding(10),
                BackColor = alignRight ? Color.LightBlue : Color.LightGray,
                TextAlign = ContentAlignment.MiddleLeft
            };

            if (alignRight)
            {
                messageLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                messageLabel.Location = new Point(chatPanel.ClientSize.Width - messageLabel.PreferredWidth - 25, messageOffset);
            }
            else
            {
                messageLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                messageLabel.Location = new Point(10, messageOffset);
            }

            chatPanel.Controls.Add(messageLabel);
            messageOffset += messageLabel.Height + 10;

            ScrollToBottom();
        }
        private void UpdateMessageOffset()
        {
            messageOffset = 10; // Початкове значення
            foreach (Control control in chatPanel.Controls)
            {
                messageOffset = control.Location.Y + control.Height + 10;
            }
        }
        private void ScrollToBottom()
        {
            chatPanel.VerticalScroll.Value = chatPanel.VerticalScroll.Maximum;
            chatPanel.PerformLayout();
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

                    case "textmessage":
                        string jsonstr = json.GetRawText();
                        TextMessage? message = JsonConvert.DeserializeObject<TextMessage>(jsonstr);
                        AddTextMessage(message.text, false);
                        if (this.WindowState == FormWindowState.Minimized)
                        {
                            SystemSounds.Asterisk.Play();
                        };
                        break;

                    case "photomessage":
                        if (this.WindowState == FormWindowState.Minimized)
                        {
                            SystemSounds.Asterisk.Play();
                        };
                        Debug.Print("before json");
                        string jsonstr1 = json.GetRawText();
                        PhotoMessage? message2 = JsonConvert.DeserializeObject<PhotoMessage>(jsonstr1);
                        Debug.Print("after json");
                        Debug.Print($"{message2.photo}");
                        using (MemoryStream ms = new MemoryStream(message2.photo))
                        {
                            Image image = Image.FromStream(ms);

                            selectedImage = image;
                        }
                        AddPhotoMessage(selectedImage, false);

                        break;

                    case "roomDeletionNotice":
                        this.InterlocutorLeaved = !this.InterlocutorLeaved;
                        textBox1.Enabled = false;
                        MessageBox.Show("співрозмовник покинув чат", "Заголовок", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                }
            }));
        }

        private void Form3_Resize(object sender, EventArgs e)
        {

            //int newHeight = this.ClientSize.Height - buttompanel.Height - 100;
            //if (newHeight < 50)
            //{
            //    newHeight = this.Height - 150;
            //}

            chatPanel.Height = this.ClientSize.Height - buttompanel.Height - 100;
            chatPanel.Width = this.ClientSize.Width + 35;
            textBox1.Size = new Size(656 + this.Width - 816, 35);
            sendbutton.Location = new Point(710 + this.Width - 816, 14);
            photobutton.Location = new Point(666 + this.Width - 816, 12);
            //sendbutton.Location = new Point(sendbutton.Location.Y, sendbutton.Location.X);

            // Оновлюємо messageOffset
            UpdateMessageOffset();
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
            if (InterlocutorLeaved) { return; }
            if (selectedImage != null)
            {
                PhotoMessage messageph = new();
                messageph.type = "photomessage";
                Debug.Print($"сосі {ImageToByteArray(selectedImage)}");
                messageph.photo = ImageToByteArray(selectedImage);
                this._webSocketClient.SendMessage(messageph);
                AddPhotoMessage(selectedImage, true);

            }
            if (textBox1.Text == "Повідомлення" || textBox1.Text.Length == 0) { return; }

            AddTextMessage(textBox1.Text, true);
            TextMessage message = new();
            message.type = "textmessage";
            message.text = textBox1.Text;
            this._webSocketClient.SendMessage(message);
            textBox1.Clear();
            textBox1_Leave(sender, e);


        }
        public byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat); // Збереження зображення в потік у його вихідному форматі
                return ms.ToArray(); // Повертає масив байтів
            }
        }

        private void AddPhotoMessage(Image image, bool alignRight)
        {
            // Обчислюємо новий messageOffset
            UpdateMessageOffset();

            // Створюємо PictureBox для відображення зображення
            PictureBox pictureBox = new PictureBox
            {
                Image = image,
                Width = 300, // Фіксована ширина
                Height = 150, // Фіксована висота
                Padding = new Padding(5),
                SizeMode = PictureBoxSizeMode.Zoom, // Масштабування зображення
                BackColor = alignRight ? Color.LightBlue : Color.LightGray
            };

            if (alignRight)
            {
                pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                pictureBox.Location = new Point(chatPanel.ClientSize.Width - pictureBox.Width - 25, messageOffset);
            }
            else
            {
                pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                pictureBox.Location = new Point(10, messageOffset);
            }

            // Додаємо PictureBox на панель
            chatPanel.Controls.Add(pictureBox);

            // Оновлюємо messageOffset для наступного елемента
            messageOffset += pictureBox.Height + 10;

            // Прокрутка до низу
            ScrollToBottom();
            selectedImage = null;
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

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Виберіть фото";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImage = Image.FromFile(openFileDialog.FileName);


                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!isPlaceholder) { button1_Click(sender, e); textBox1_Enter(sender, e); }
                e.SuppressKeyPress = true;
            }

        }
    }


    public class CustomPanel : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x00200000; // WS_VSCROLL (постійний вертикальний скролбар)
                cp.Style &= ~0x00100000; // WS_HSCROLL (приховати горизонтальний скролбар)
                return cp;
            }
        }
    }
}
