using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WinFormsApp2
{
    partial class QueueScreen
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
            progressBar1 = new ProgressBar();
            backbutton = new Button();
            findinglabel = new Label();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(384, 220);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(100, 23);
            progressBar1.TabIndex = 0;
            // 
            // backbutton
            // 
            backbutton.BackColor = Color.FromArgb(42, 120, 245);
            backbutton.FlatAppearance.BorderSize = 0;
            backbutton.FlatStyle = FlatStyle.Flat;
            backbutton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            backbutton.ForeColor = Color.White;
            backbutton.Location = new Point(0, 0);
            backbutton.Name = "backbutton";
            backbutton.Size = new Size(98, 32);
            backbutton.TabIndex = 1;
            backbutton.Text = "<-";
            backbutton.UseVisualStyleBackColor = false;
            backbutton.Click += button1_Click;
            // 
            // findinglabel
            // 
            findinglabel.AutoSize = true;
            findinglabel.Location = new Point(406, 156);
            findinglabel.Name = "findinglabel";
            findinglabel.Size = new Size(150, 15);
            findinglabel.TabIndex = 2;
            findinglabel.Text = "Шукаємо співрозмовника";
            // 
            // QueueUsers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(findinglabel);
            Controls.Add(backbutton);
            Controls.Add(progressBar1);
            MinimumSize = new Size(816, 489);
            Name = "QueueUsers";
            Text = "QueueUsers";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar1;
        private Button backbutton;
        private Label findinglabel;
    }
}