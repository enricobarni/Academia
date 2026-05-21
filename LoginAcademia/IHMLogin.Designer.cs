namespace LoginAcademia
{
    partial class IHMLogin
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
            this.pnLogin = new Guna.UI2.WinForms.Guna2Panel();
            this.lblErroSenha = new System.Windows.Forms.Label();
            this.lblErroUsuario = new System.Windows.Forms.Label();
            this.lbOuLogin = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnCriarConta = new Guna.UI2.WinForms.Guna2Button();
            this.btnEntrar = new Guna.UI2.WinForms.Guna2Button();
            this.txtSenha = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtUsuario = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbSenhaCadastro = new System.Windows.Forms.Label();
            this.lbUsuario = new System.Windows.Forms.Label();
            this.lbSubtitulo = new System.Windows.Forms.Label();
            this.lbBemVindo = new System.Windows.Forms.Label();
            this.imLogo = new System.Windows.Forms.PictureBox();
            this.pnLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnLogin
            // 
            this.pnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnLogin.BackColor = System.Drawing.Color.Transparent;
            this.pnLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.pnLogin.BorderRadius = 30;
            this.pnLogin.BorderThickness = 2;
            this.pnLogin.Controls.Add(this.lblErroSenha);
            this.pnLogin.Controls.Add(this.lblErroUsuario);
            this.pnLogin.Controls.Add(this.lbOuLogin);
            this.pnLogin.Controls.Add(this.btnCriarConta);
            this.pnLogin.Controls.Add(this.btnEntrar);
            this.pnLogin.Controls.Add(this.txtSenha);
            this.pnLogin.Controls.Add(this.txtUsuario);
            this.pnLogin.Controls.Add(this.lbSenhaCadastro);
            this.pnLogin.Controls.Add(this.lbUsuario);
            this.pnLogin.Controls.Add(this.lbSubtitulo);
            this.pnLogin.Controls.Add(this.lbBemVindo);
            this.pnLogin.Controls.Add(this.imLogo);
            this.pnLogin.Location = new System.Drawing.Point(711, 41);
            this.pnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnLogin.Name = "pnLogin";
            this.pnLogin.Size = new System.Drawing.Size(480, 601);
            this.pnLogin.TabIndex = 5;
            // 
            // lblErroSenha
            // 
            this.lblErroSenha.AutoSize = true;
            this.lblErroSenha.BackColor = System.Drawing.Color.Transparent;
            this.lblErroSenha.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErroSenha.ForeColor = System.Drawing.Color.Red;
            this.lblErroSenha.Location = new System.Drawing.Point(75, 348);
            this.lblErroSenha.Name = "lblErroSenha";
            this.lblErroSenha.Size = new System.Drawing.Size(91, 15);
            this.lblErroSenha.TabIndex = 52;
            this.lblErroSenha.Text = "Senha Inválida!";
            // 
            // lblErroUsuario
            // 
            this.lblErroUsuario.AutoSize = true;
            this.lblErroUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lblErroUsuario.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErroUsuario.ForeColor = System.Drawing.Color.Red;
            this.lblErroUsuario.Location = new System.Drawing.Point(75, 260);
            this.lblErroUsuario.Name = "lblErroUsuario";
            this.lblErroUsuario.Size = new System.Drawing.Size(100, 15);
            this.lblErroUsuario.TabIndex = 49;
            this.lblErroUsuario.Text = "Usuario Inválido!";
            // 
            // lbOuLogin
            // 
            this.lbOuLogin.AutoSize = false;
            this.lbOuLogin.BackColor = System.Drawing.Color.Transparent;
            this.lbOuLogin.Enabled = false;
            this.lbOuLogin.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOuLogin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbOuLogin.Location = new System.Drawing.Point(225, 498);
            this.lbOuLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbOuLogin.Name = "lbOuLogin";
            this.lbOuLogin.Size = new System.Drawing.Size(35, 25);
            this.lbOuLogin.TabIndex = 26;
            this.lbOuLogin.TabStop = false;
            this.lbOuLogin.Text = "ou";
            // 
            // btnCriarConta
            // 
            this.btnCriarConta.Animated = true;
            this.btnCriarConta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.btnCriarConta.BorderRadius = 5;
            this.btnCriarConta.BorderThickness = 2;
            this.btnCriarConta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCriarConta.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCriarConta.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCriarConta.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCriarConta.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCriarConta.FillColor = System.Drawing.Color.Transparent;
            this.btnCriarConta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCriarConta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.btnCriarConta.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCriarConta.Location = new System.Drawing.Point(40, 524);
            this.btnCriarConta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCriarConta.Name = "btnCriarConta";
            this.btnCriarConta.Size = new System.Drawing.Size(400, 46);
            this.btnCriarConta.TabIndex = 6;
            this.btnCriarConta.TabStop = false;
            this.btnCriarConta.Text = "CRIAR CONTA";
            this.btnCriarConta.Click += new System.EventHandler(this.btnCriarConta_Click);
            // 
            // btnEntrar
            // 
            this.btnEntrar.Animated = true;
            this.btnEntrar.BackColor = System.Drawing.Color.Transparent;
            this.btnEntrar.BorderRadius = 5;
            this.btnEntrar.BorderThickness = 2;
            this.btnEntrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEntrar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEntrar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEntrar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEntrar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.btnEntrar.FocusedColor = System.Drawing.Color.Transparent;
            this.btnEntrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEntrar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.btnEntrar.Location = new System.Drawing.Point(41, 453);
            this.btnEntrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(200)))));
            this.btnEntrar.ShadowDecoration.Enabled = true;
            this.btnEntrar.Size = new System.Drawing.Size(400, 46);
            this.btnEntrar.TabIndex = 5;
            this.btnEntrar.TabStop = false;
            this.btnEntrar.Text = "ENTRAR";
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // txtSenha
            // 
            this.txtSenha.BorderColor = System.Drawing.Color.Transparent;
            this.txtSenha.BorderRadius = 5;
            this.txtSenha.BorderThickness = 2;
            this.txtSenha.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSenha.DefaultText = "";
            this.txtSenha.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSenha.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSenha.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSenha.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSenha.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.txtSenha.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtSenha.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSenha.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtSenha.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtSenha.IconLeft = global::LoginAcademia.Properties.Resources.iconesSenha;
            this.txtSenha.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.txtSenha.IconLeftOffset = new System.Drawing.Point(3, 0);
            this.txtSenha.IconLeftSize = new System.Drawing.Size(15, 15);
            this.txtSenha.IconRight = global::LoginAcademia.Properties.Resources.iconesSenha2;
            this.txtSenha.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.txtSenha.IconRightOffset = new System.Drawing.Point(5, 0);
            this.txtSenha.IconRightSize = new System.Drawing.Size(20, 15);
            this.txtSenha.Location = new System.Drawing.Point(40, 311);
            this.txtSenha.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PlaceholderText = "Digite sua senha";
            this.txtSenha.SelectedText = "";
            this.txtSenha.Size = new System.Drawing.Size(400, 46);
            this.txtSenha.TabIndex = 1;
            this.txtSenha.UseSystemPasswordChar = true;
            this.txtSenha.IconRightClick += new System.EventHandler(this.txtSenha_IconRightClick);
            // 
            // txtUsuario
            // 
            this.txtUsuario.BorderColor = System.Drawing.Color.Transparent;
            this.txtUsuario.BorderRadius = 5;
            this.txtUsuario.BorderThickness = 2;
            this.txtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsuario.DefaultText = "";
            this.txtUsuario.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtUsuario.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtUsuario.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUsuario.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUsuario.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.txtUsuario.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUsuario.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtUsuario.IconLeft = global::LoginAcademia.Properties.Resources.iconesUsuario2;
            this.txtUsuario.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.txtUsuario.IconLeftSize = new System.Drawing.Size(15, 15);
            this.txtUsuario.Location = new System.Drawing.Point(40, 223);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.PlaceholderText = "Digite seu nome";
            this.txtUsuario.SelectedText = "";
            this.txtUsuario.Size = new System.Drawing.Size(400, 46);
            this.txtUsuario.TabIndex = 0;
            // 
            // lbSenhaCadastro
            // 
            this.lbSenhaCadastro.AutoSize = true;
            this.lbSenhaCadastro.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSenhaCadastro.ForeColor = System.Drawing.Color.Gray;
            this.lbSenhaCadastro.Location = new System.Drawing.Point(37, 282);
            this.lbSenhaCadastro.Name = "lbSenhaCadastro";
            this.lbSenhaCadastro.Size = new System.Drawing.Size(73, 25);
            this.lbSenhaCadastro.TabIndex = 20;
            this.lbSenhaCadastro.Text = "SENHA";
            // 
            // lbUsuario
            // 
            this.lbUsuario.AutoSize = true;
            this.lbUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lbUsuario.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsuario.ForeColor = System.Drawing.Color.Gray;
            this.lbUsuario.Location = new System.Drawing.Point(35, 194);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(93, 25);
            this.lbUsuario.TabIndex = 19;
            this.lbUsuario.Text = "USUÁRIO";
            // 
            // lbSubtitulo
            // 
            this.lbSubtitulo.AutoSize = true;
            this.lbSubtitulo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubtitulo.ForeColor = System.Drawing.Color.Gray;
            this.lbSubtitulo.Location = new System.Drawing.Point(103, 150);
            this.lbSubtitulo.Name = "lbSubtitulo";
            this.lbSubtitulo.Size = new System.Drawing.Size(253, 17);
            this.lbSubtitulo.TabIndex = 18;
            this.lbSubtitulo.Text = "Faça login para continuar sua evolução.";
            // 
            // lbBemVindo
            // 
            this.lbBemVindo.AutoSize = true;
            this.lbBemVindo.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBemVindo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbBemVindo.Location = new System.Drawing.Point(119, 100);
            this.lbBemVindo.Name = "lbBemVindo";
            this.lbBemVindo.Size = new System.Drawing.Size(237, 50);
            this.lbBemVindo.TabIndex = 17;
            this.lbBemVindo.Text = "BEM-VINDO";
            // 
            // imLogo
            // 
            this.imLogo.BackColor = System.Drawing.Color.Transparent;
            this.imLogo.Image = global::LoginAcademia.Properties.Resources.ChatGPT_Image_14_de_mai__de_2026__17_10_07;
            this.imLogo.Location = new System.Drawing.Point(168, 14);
            this.imLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imLogo.Name = "imLogo";
            this.imLogo.Size = new System.Drawing.Size(133, 100);
            this.imLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imLogo.TabIndex = 21;
            this.imLogo.TabStop = false;
            // 
            // IHMLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LoginAcademia.Properties.Resources.ChatGPT_Image_13_de_mai__de_2026__18_06_17;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1940, 1100);
            this.Controls.Add(this.pnLogin);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "IHMLogin";
            this.Text = "IHMLogin";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.IHMLogin_Load);
            this.pnLogin.ResumeLayout(false);
            this.pnLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnLogin;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbOuLogin;
        private Guna.UI2.WinForms.Guna2Button btnCriarConta;
        private Guna.UI2.WinForms.Guna2Button btnEntrar;
        private Guna.UI2.WinForms.Guna2TextBox txtSenha;
        private Guna.UI2.WinForms.Guna2TextBox txtUsuario;
        private System.Windows.Forms.Label lbSenhaCadastro;
        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.Label lbSubtitulo;
        private System.Windows.Forms.Label lbBemVindo;
        private System.Windows.Forms.PictureBox imLogo;
        private System.Windows.Forms.Label lblErroUsuario;
        private System.Windows.Forms.Label lblErroSenha;
    }
}