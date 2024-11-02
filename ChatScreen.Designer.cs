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
            button1 = new Button();
            panel1 = new Panel();
            buttonpanel = new Panel();
            button2 = new Button();
            toppanel = new Panel();
            label1 = new Label();
            leavechatbutton = new Button();
            buttonpanel.SuspendLayout();
            toppanel.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBox1.Location = new Point(12, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(648, 35);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(42, 120, 245);
            button1.FlatAppearance.BorderSize = 0;
            button1.Location = new Point(710, 12);
            button1.Name = "button1";
            button1.Size = new Size(78, 35);
            button1.TabIndex = 2;
            button1.Text = "send";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Location = new Point(0, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 360);
            panel1.TabIndex = 3;
            // 
            // buttonpanel
            // 
            buttonpanel.BackColor = Color.FromArgb(42, 120, 245);
            buttonpanel.Controls.Add(button2);
            buttonpanel.Controls.Add(textBox1);
            buttonpanel.Controls.Add(button1);
            buttonpanel.Dock = DockStyle.Bottom;
            buttonpanel.Location = new Point(0, 395);
            buttonpanel.Name = "buttonpanel";
            buttonpanel.Size = new Size(800, 55);
            buttonpanel.TabIndex = 4;
            // 
            // button2
            // 
            button2.Location = new Point(666, 12);
            button2.Name = "button2";
            button2.Size = new Size(49, 35);
            button2.TabIndex = 3;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // toppanel
            // 
            toppanel.BackColor = Color.FromArgb(42, 120, 245);
            toppanel.Controls.Add(label1);
            toppanel.Controls.Add(leavechatbutton);
            toppanel.Dock = DockStyle.Top;
            toppanel.Location = new Point(0, 0);
            toppanel.Name = "toppanel";
            toppanel.Size = new Size(800, 44);
            toppanel.TabIndex = 5;
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
            leavechatbutton.Size = new Size(96, 44);
            leavechatbutton.TabIndex = 0;
            leavechatbutton.Text = "<-";
            leavechatbutton.UseVisualStyleBackColor = true;
            leavechatbutton.Click += leavechatbutton_Click;
            // 
            // ChatScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toppanel);
            Controls.Add(buttonpanel);
            Controls.Add(panel1);
            MinimumSize = new Size(816, 489);
            Name = "ChatScreen";
            Text = "ChatScreen";
            buttonpanel.ResumeLayout(false);
            buttonpanel.PerformLayout();
            toppanel.ResumeLayout(false);
            toppanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private Panel panel1;
        private Panel buttonpanel;
        private Button button2;
        private Panel toppanel;
        private Label label1;
        private Button leavechatbutton;
    }
}