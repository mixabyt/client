namespace WinFormsApp2
{
    partial class ChatScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            sendbutton = new Button();
            chatPanel = new CustomPanel();
            buttompanel = new Panel();
            photobutton = new Button();
            toppanel = new Panel();
            label1 = new Label();
            leavechatbutton = new Button();
            buttompanel.SuspendLayout();
            toppanel.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(18, 12);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(647, 33);
            textBox1.TabIndex = 7;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // sendbutton
            // 
            sendbutton.BackColor = Color.FromArgb(42, 120, 245);
            sendbutton.FlatStyle = FlatStyle.Flat;
            sendbutton.ForeColor = Color.White;
            sendbutton.Location = new Point(709, 14);
            sendbutton.Margin = new Padding(4, 3, 4, 3);
            sendbutton.Name = "sendbutton";
            sendbutton.Size = new Size(78, 31);
            sendbutton.TabIndex = 1;
            sendbutton.Text = "send";
            sendbutton.UseVisualStyleBackColor = true;
            sendbutton.Click += button1_Click;
            // 
            // chatPanel
            // 
            chatPanel.AutoScroll = true;
            chatPanel.BackColor = Color.White;
            chatPanel.Location = new Point(0, 50);
            chatPanel.Margin = new Padding(4, 3, 4, 3);
            chatPanel.Name = "chatPanel";
            chatPanel.Padding = new Padding(4, 3, 4, 10);
            chatPanel.Size = new Size(834, 308);
            chatPanel.TabIndex = 2;
            // 
            // buttompanel
            // 
            buttompanel.BackColor = Color.FromArgb(42, 120, 245);
            buttompanel.Controls.Add(photobutton);
            buttompanel.Controls.Add(textBox1);
            buttompanel.Controls.Add(sendbutton);
            buttompanel.Dock = DockStyle.Bottom;
            buttompanel.Location = new Point(0, 395);
            buttompanel.Margin = new Padding(4, 3, 4, 3);
            buttompanel.Name = "buttompanel";
            buttompanel.Padding = new Padding(18, 12, 18, 12);
            buttompanel.Size = new Size(800, 55);
            buttompanel.TabIndex = 1;
            // 
            // photobutton
            // 
            photobutton.Location = new Point(663, 12);
            photobutton.Name = "photobutton";
            photobutton.Size = new Size(49, 35);
            photobutton.TabIndex = 3;
            photobutton.Text = "button2";
            photobutton.UseVisualStyleBackColor = true;
            photobutton.Click += button2_Click;
            // 
            // toppanel
            // 
            toppanel.BackColor = Color.FromArgb(42, 120, 245);
            toppanel.Controls.Add(label1);
            toppanel.Controls.Add(leavechatbutton);
            toppanel.Dock = DockStyle.Top;
            toppanel.Location = new Point(0, 0);
            toppanel.Margin = new Padding(4, 3, 4, 3);
            toppanel.Name = "toppanel";
            toppanel.Padding = new Padding(18, 17, 18, 17);
            toppanel.Size = new Size(800, 44);
            toppanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(390, 3);
            label1.Name = "label1";
            label1.Size = new Size(68, 35);
            label1.TabIndex = 1;
            label1.Text = "Чат";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // leavechatbutton
            // 
            leavechatbutton.Location = new Point(0, 0);
            leavechatbutton.Name = "leavechatbutton";
            leavechatbutton.Size = new Size(40, 44);
            leavechatbutton.TabIndex = 0;
            leavechatbutton.Text = "<-";
            leavechatbutton.UseVisualStyleBackColor = true;
            leavechatbutton.Click += leavechatbutton_Click;
            // 
            // ChatScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(toppanel);
            Controls.Add(buttompanel);
            Controls.Add(chatPanel);
            MinimumSize = new Size(816, 489);
            Name = "ChatScreen";
            Text = "ChatScreen";
            buttompanel.ResumeLayout(false);
            buttompanel.PerformLayout();
            toppanel.ResumeLayout(false);
            toppanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBox1;
        private Button sendbutton;
        private Panel buttompanel;
        private Button photobutton;
        private Panel toppanel;
        private Label label1;
        private Button leavechatbutton;
        private CustomPanel chatPanel;
    }
}