﻿namespace Lab3
{
    partial class Form1
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
            this.View = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.SuspendLayout();
            // 
            // View
            // 
            this.View.AccumBits = ((byte)(0));
            this.View.AutoCheckErrors = false;
            this.View.AutoFinish = false;
            this.View.AutoMakeCurrent = true;
            this.View.AutoSwapBuffers = true;
            this.View.BackColor = System.Drawing.Color.Black;
            this.View.ColorBits = ((byte)(32));
            this.View.DepthBits = ((byte)(16));
            this.View.Location = new System.Drawing.Point(-1, 0);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(804, 451);
            this.View.StencilBits = ((byte)(0));
            this.View.TabIndex = 0;
            this.View.Load += new System.EventHandler(this.View_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.View);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }

        private Tao.Platform.Windows.SimpleOpenGlControl View;

        #endregion
    }
}