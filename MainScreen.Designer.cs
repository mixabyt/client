namespace WinFormsApp2
{
    partial class MainScreen
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
            useronlinelabel = new Label();
            SuspendLayout();
            // 
            // useronlinelabel
            // 
            useronlinelabel.AutoSize = true;
            useronlinelabel.Location = new Point(646, 57);
            useronlinelabel.Name = "useronlinelabel";
            useronlinelabel.Size = new Size(73, 15);
            useronlinelabel.TabIndex = 0;
            useronlinelabel.Text = "users online:";
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(useronlinelabel);
            MinimumSize = new Size(816, 489);
            Name = "MainScreen";
            Text = "Screen";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label useronlinelabel;
    }
}