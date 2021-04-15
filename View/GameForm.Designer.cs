
namespace Snake.View
{
    partial class GameForm
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
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this._menuFileNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this._menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this._menuGameEasy = new System.Windows.Forms.ToolStripMenuItem();
            this._menuGameNormal = new System.Windows.Forms.ToolStripMenuItem();
            this._menuGameHard = new System.Windows.Forms.ToolStripMenuItem();
            this._menuFilePause = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuFile,
            this._menuSettings});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(758, 24);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "Menü";
            // 
            // _menuFile
            // 
            this._menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuFileNewGame,
            this._menuFileExit,
            this._menuFilePause});
            this._menuFile.Name = "_menuFile";
            this._menuFile.Size = new System.Drawing.Size(37, 20);
            this._menuFile.Text = "Fájl";
            // 
            // _menuFileNewGame
            // 
            this._menuFileNewGame.Name = "_menuFileNewGame";
            this._menuFileNewGame.Size = new System.Drawing.Size(113, 22);
            this._menuFileNewGame.Text = "Új játék";
            this._menuFileNewGame.Click += new System.EventHandler(this.MenuFileNewGame_Click);
            // 
            // _menuFileExit
            // 
            this._menuFileExit.Name = "_menuFileExit";
            this._menuFileExit.Size = new System.Drawing.Size(113, 22);
            this._menuFileExit.Text = "Kilépés";
            this._menuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);
            // 
            // _menuSettings
            // 
            this._menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuGameEasy,
            this._menuGameNormal,
            this._menuGameHard});
            this._menuSettings.Name = "_menuSettings";
            this._menuSettings.Size = new System.Drawing.Size(75, 20);
            this._menuSettings.Text = "Beállítások";
            // 
            // _menuGameEasy
            // 
            this._menuGameEasy.Checked = true;
            this._menuGameEasy.CheckState = System.Windows.Forms.CheckState.Checked;
            this._menuGameEasy.Name = "_menuGameEasy";
            this._menuGameEasy.Size = new System.Drawing.Size(108, 22);
            this._menuGameEasy.Text = "Szint 1";
            this._menuGameEasy.Click += new System.EventHandler(this.MenuGameEasy_Click);
            // 
            // _menuGameNormal
            // 
            this._menuGameNormal.Name = "_menuGameNormal";
            this._menuGameNormal.Size = new System.Drawing.Size(108, 22);
            this._menuGameNormal.Text = "Szint 2";
            this._menuGameNormal.Click += new System.EventHandler(this.MenuGameMedium_Click);
            // 
            // _menuGameHard
            // 
            this._menuGameHard.Name = "_menuGameHard";
            this._menuGameHard.Size = new System.Drawing.Size(108, 22);
            this._menuGameHard.Text = "Szint 3";
            this._menuGameHard.Click += new System.EventHandler(this.MenuGameHard_Click);
            // 
            // _menuFilePause
            // 
            this._menuFilePause.Name = "_menuFilePause";
            this._menuFilePause.Size = new System.Drawing.Size(113, 22);
            this._menuFilePause.Text = "Szünet";
            this._menuFilePause.Click += new System.EventHandler(this.MenuFilePause_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 844);
            this.Controls.Add(this._menuStrip);
            this.MainMenuStrip = this._menuStrip;
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ChangeDirection);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _menuFile;
        private System.Windows.Forms.ToolStripMenuItem _menuSettings;
        private System.Windows.Forms.ToolStripMenuItem _menuFileNewGame;
        private System.Windows.Forms.ToolStripMenuItem _menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem _menuGameEasy;
        private System.Windows.Forms.ToolStripMenuItem _menuGameHard;
        private System.Windows.Forms.ToolStripMenuItem _menuGameNormal;
        private System.Windows.Forms.ToolStripMenuItem _menuFilePause;
    }
}