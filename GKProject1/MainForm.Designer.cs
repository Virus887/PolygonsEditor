namespace GKProject1
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.StripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newBlueprintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DrawingArea = new System.Windows.Forms.PictureBox();
            this.LeftPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Draw_Polygon_Button = new System.Windows.Forms.Button();
            this.ClearAllButton = new System.Windows.Forms.Button();
            this.ExamplePolygonButton1 = new System.Windows.Forms.Button();
            this.BresenhamCheckBox = new System.Windows.Forms.CheckBox();
            this.ExamplePolygonButton2 = new System.Windows.Forms.Button();
            this.ExamplePolygonButton3 = new System.Windows.Forms.Button();
            this.Hint = new System.Windows.Forms.CheckBox();
            this.PointFMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PointFDeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EdgeMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EdgeRelationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConstantLengthMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HorizontalLineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VerticalLineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EdgeDeleteRelationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EdgeAddVerticleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EdgeDeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PolygonMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PolygonDeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.MainLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).BeginInit();
            this.LeftPanel.SuspendLayout();
            this.PointFMenuStrip.SuspendLayout();
            this.EdgeMenuStrip.SuspendLayout();
            this.PolygonMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1505, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // StripMenu
            // 
            this.StripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBlueprintToolStripMenuItem});
            this.StripMenu.Name = "StripMenu";
            this.StripMenu.Size = new System.Drawing.Size(37, 20);
            this.StripMenu.Text = "File";
            // 
            // newBlueprintToolStripMenuItem
            // 
            this.newBlueprintToolStripMenuItem.Name = "newBlueprintToolStripMenuItem";
            this.newBlueprintToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.newBlueprintToolStripMenuItem.Text = "New blueprint";
            this.newBlueprintToolStripMenuItem.Click += new System.EventHandler(this.newBlueprintToolStripMenuItem_Click);
            // 
            // MainLayoutPanel
            // 
            this.MainLayoutPanel.ColumnCount = 2;
            this.MainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.22924F));
            this.MainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.77077F));
            this.MainLayoutPanel.Controls.Add(this.DrawingArea, 1, 0);
            this.MainLayoutPanel.Controls.Add(this.LeftPanel, 0, 0);
            this.MainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.MainLayoutPanel.Name = "MainLayoutPanel";
            this.MainLayoutPanel.RowCount = 1;
            this.MainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainLayoutPanel.Size = new System.Drawing.Size(1505, 603);
            this.MainLayoutPanel.TabIndex = 1;
            // 
            // DrawingArea
            // 
            this.DrawingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawingArea.Location = new System.Drawing.Point(172, 3);
            this.DrawingArea.Name = "DrawingArea";
            this.DrawingArea.Size = new System.Drawing.Size(1330, 597);
            this.DrawingArea.TabIndex = 0;
            this.DrawingArea.TabStop = false;
            this.DrawingArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseClick);
            this.DrawingArea.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseDoubleClick);
            this.DrawingArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseDown);
            this.DrawingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseMove);
            this.DrawingArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseUp);
            // 
            // LeftPanel
            // 
            this.LeftPanel.ColumnCount = 1;
            this.LeftPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LeftPanel.Controls.Add(this.Draw_Polygon_Button, 0, 0);
            this.LeftPanel.Controls.Add(this.ClearAllButton, 0, 1);
            this.LeftPanel.Controls.Add(this.ExamplePolygonButton1, 0, 2);
            this.LeftPanel.Controls.Add(this.BresenhamCheckBox, 0, 5);
            this.LeftPanel.Controls.Add(this.ExamplePolygonButton2, 0, 3);
            this.LeftPanel.Controls.Add(this.ExamplePolygonButton3, 0, 4);
            this.LeftPanel.Controls.Add(this.Hint, 0, 6);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftPanel.Location = new System.Drawing.Point(3, 3);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Padding = new System.Windows.Forms.Padding(10);
            this.LeftPanel.RowCount = 7;
            this.LeftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LeftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LeftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.LeftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.LeftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.LeftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.LeftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.LeftPanel.Size = new System.Drawing.Size(163, 597);
            this.LeftPanel.TabIndex = 1;
            // 
            // Draw_Polygon_Button
            // 
            this.Draw_Polygon_Button.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Draw_Polygon_Button.Dock = System.Windows.Forms.DockStyle.Top;
            this.Draw_Polygon_Button.Location = new System.Drawing.Point(13, 13);
            this.Draw_Polygon_Button.Name = "Draw_Polygon_Button";
            this.Draw_Polygon_Button.Size = new System.Drawing.Size(137, 50);
            this.Draw_Polygon_Button.TabIndex = 0;
            this.Draw_Polygon_Button.Text = "New figure";
            this.Draw_Polygon_Button.UseVisualStyleBackColor = false;
            this.Draw_Polygon_Button.Click += new System.EventHandler(this.Draw_Polygon_Button_Click);
            // 
            // ClearAllButton
            // 
            this.ClearAllButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClearAllButton.Location = new System.Drawing.Point(13, 157);
            this.ClearAllButton.Name = "ClearAllButton";
            this.ClearAllButton.Size = new System.Drawing.Size(137, 50);
            this.ClearAllButton.TabIndex = 2;
            this.ClearAllButton.Text = "Clear all";
            this.ClearAllButton.UseVisualStyleBackColor = false;
            this.ClearAllButton.Click += new System.EventHandler(this.Clear_All_Click);
            // 
            // ExamplePolygonButton1
            // 
            this.ExamplePolygonButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExamplePolygonButton1.Location = new System.Drawing.Point(18, 306);
            this.ExamplePolygonButton1.Margin = new System.Windows.Forms.Padding(8);
            this.ExamplePolygonButton1.Name = "ExamplePolygonButton1";
            this.ExamplePolygonButton1.Padding = new System.Windows.Forms.Padding(5);
            this.ExamplePolygonButton1.Size = new System.Drawing.Size(127, 41);
            this.ExamplePolygonButton1.TabIndex = 1;
            this.ExamplePolygonButton1.Text = "Predefined polygon 1";
            this.ExamplePolygonButton1.UseVisualStyleBackColor = true;
            this.ExamplePolygonButton1.Click += new System.EventHandler(this.Example_Polygon1_Click);
            // 
            // BresenhamCheckBox
            // 
            this.BresenhamCheckBox.AutoSize = true;
            this.BresenhamCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BresenhamCheckBox.FlatAppearance.BorderSize = 5;
            this.BresenhamCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BresenhamCheckBox.Location = new System.Drawing.Point(13, 472);
            this.BresenhamCheckBox.Name = "BresenhamCheckBox";
            this.BresenhamCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.BresenhamCheckBox.Size = new System.Drawing.Size(130, 18);
            this.BresenhamCheckBox.TabIndex = 3;
            this.BresenhamCheckBox.Text = "Bresenham algorithm";
            this.BresenhamCheckBox.UseVisualStyleBackColor = true;
            this.BresenhamCheckBox.Click += new System.EventHandler(this.BresenhamCheckBox_Click);
            // 
            // ExamplePolygonButton2
            // 
            this.ExamplePolygonButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExamplePolygonButton2.Location = new System.Drawing.Point(18, 363);
            this.ExamplePolygonButton2.Margin = new System.Windows.Forms.Padding(8);
            this.ExamplePolygonButton2.Name = "ExamplePolygonButton2";
            this.ExamplePolygonButton2.Padding = new System.Windows.Forms.Padding(5);
            this.ExamplePolygonButton2.Size = new System.Drawing.Size(127, 41);
            this.ExamplePolygonButton2.TabIndex = 4;
            this.ExamplePolygonButton2.Text = "Predefined polygon 2";
            this.ExamplePolygonButton2.UseVisualStyleBackColor = true;
            this.ExamplePolygonButton2.Click += new System.EventHandler(this.ExamplePolygonButton2_Click);
            // 
            // ExamplePolygonButton3
            // 
            this.ExamplePolygonButton3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExamplePolygonButton3.Location = new System.Drawing.Point(18, 420);
            this.ExamplePolygonButton3.Margin = new System.Windows.Forms.Padding(8);
            this.ExamplePolygonButton3.Name = "ExamplePolygonButton3";
            this.ExamplePolygonButton3.Padding = new System.Windows.Forms.Padding(5);
            this.ExamplePolygonButton3.Size = new System.Drawing.Size(127, 41);
            this.ExamplePolygonButton3.TabIndex = 5;
            this.ExamplePolygonButton3.Text = "Predefined polygon 3";
            this.ExamplePolygonButton3.UseVisualStyleBackColor = true;
            this.ExamplePolygonButton3.Click += new System.EventHandler(this.ExamplePolygonButton3_Click);
            // 
            // Hint
            // 
            this.Hint.AutoSize = true;
            this.Hint.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Hint.Location = new System.Drawing.Point(13, 529);
            this.Hint.Name = "Hint";
            this.Hint.Size = new System.Drawing.Size(95, 17);
            this.Hint.TabIndex = 6;
            this.Hint.Text = "Relations hints";
            this.Hint.UseVisualStyleBackColor = true;
            // 
            // PointFMenuStrip
            // 
            this.PointFMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PointFDeleteMenuItem});
            this.PointFMenuStrip.Name = "PointFMenuStrip";
            this.PointFMenuStrip.Size = new System.Drawing.Size(149, 26);
            // 
            // PointFDeleteMenuItem
            // 
            this.PointFDeleteMenuItem.Image = global::GKProject1.Properties.Resources.DeletePoint;
            this.PointFDeleteMenuItem.Name = "PointFDeleteMenuItem";
            this.PointFDeleteMenuItem.Size = new System.Drawing.Size(148, 22);
            this.PointFDeleteMenuItem.Text = "Delete Verticle";
            this.PointFDeleteMenuItem.Click += new System.EventHandler(this.PointFDeleteMenuItem_Click);
            // 
            // EdgeMenuStrip
            // 
            this.EdgeMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EdgeRelationsMenuItem,
            this.EdgeAddVerticleMenuItem,
            this.EdgeDeleteMenuItem});
            this.EdgeMenuStrip.Name = "EdgeMenuStrip";
            this.EdgeMenuStrip.Size = new System.Drawing.Size(138, 70);
            // 
            // EdgeRelationsMenuItem
            // 
            this.EdgeRelationsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConstantLengthMenuItem,
            this.HorizontalLineMenuItem,
            this.VerticalLineMenuItem,
            this.EdgeDeleteRelationMenuItem});
            this.EdgeRelationsMenuItem.Image = global::GKProject1.Properties.Resources.Relations;
            this.EdgeRelationsMenuItem.Name = "EdgeRelationsMenuItem";
            this.EdgeRelationsMenuItem.Size = new System.Drawing.Size(137, 22);
            this.EdgeRelationsMenuItem.Text = "Relations";
            this.EdgeRelationsMenuItem.MouseEnter += new System.EventHandler(this.EdgeRelationsMenuItem_MouseEnter);
            // 
            // ConstantLengthMenuItem
            // 
            this.ConstantLengthMenuItem.Image = global::GKProject1.Properties.Resources.Length;
            this.ConstantLengthMenuItem.Name = "ConstantLengthMenuItem";
            this.ConstantLengthMenuItem.Size = new System.Drawing.Size(162, 22);
            this.ConstantLengthMenuItem.Text = "Constant Length";
            this.ConstantLengthMenuItem.Click += new System.EventHandler(this.ConstantLengthMenuItem_Click);
            // 
            // HorizontalLineMenuItem
            // 
            this.HorizontalLineMenuItem.Image = global::GKProject1.Properties.Resources.Horizontal;
            this.HorizontalLineMenuItem.Name = "HorizontalLineMenuItem";
            this.HorizontalLineMenuItem.Size = new System.Drawing.Size(162, 22);
            this.HorizontalLineMenuItem.Text = "Horizontal Line";
            this.HorizontalLineMenuItem.Click += new System.EventHandler(this.HorizontalLineMenuItem_Click);
            // 
            // VerticalLineMenuItem
            // 
            this.VerticalLineMenuItem.Image = global::GKProject1.Properties.Resources.Vertical;
            this.VerticalLineMenuItem.Name = "VerticalLineMenuItem";
            this.VerticalLineMenuItem.Size = new System.Drawing.Size(162, 22);
            this.VerticalLineMenuItem.Text = "Vertical Line";
            this.VerticalLineMenuItem.Click += new System.EventHandler(this.VerticalLineMenuItem_Click);
            // 
            // EdgeDeleteRelationMenuItem
            // 
            this.EdgeDeleteRelationMenuItem.Image = global::GKProject1.Properties.Resources.Delete;
            this.EdgeDeleteRelationMenuItem.Name = "EdgeDeleteRelationMenuItem";
            this.EdgeDeleteRelationMenuItem.Size = new System.Drawing.Size(162, 22);
            this.EdgeDeleteRelationMenuItem.Text = "Delete Relation";
            this.EdgeDeleteRelationMenuItem.Click += new System.EventHandler(this.EdgeDeleteRelationMenuItem_Click);
            // 
            // EdgeAddVerticleMenuItem
            // 
            this.EdgeAddVerticleMenuItem.Image = global::GKProject1.Properties.Resources.Point;
            this.EdgeAddVerticleMenuItem.Name = "EdgeAddVerticleMenuItem";
            this.EdgeAddVerticleMenuItem.Size = new System.Drawing.Size(137, 22);
            this.EdgeAddVerticleMenuItem.Text = "Add Verticle";
            this.EdgeAddVerticleMenuItem.Click += new System.EventHandler(this.EdgeAddVerticleMenuItem_Click);
            // 
            // EdgeDeleteMenuItem
            // 
            this.EdgeDeleteMenuItem.Image = global::GKProject1.Properties.Resources.Delete;
            this.EdgeDeleteMenuItem.Name = "EdgeDeleteMenuItem";
            this.EdgeDeleteMenuItem.Size = new System.Drawing.Size(137, 22);
            this.EdgeDeleteMenuItem.Text = "Delete Edge";
            this.EdgeDeleteMenuItem.Click += new System.EventHandler(this.EdgeDeleteMenuItem_Click);
            // 
            // PolygonMenuStrip
            // 
            this.PolygonMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PolygonDeleteMenuItem});
            this.PolygonMenuStrip.Name = "PolygonMenuStrip";
            this.PolygonMenuStrip.Size = new System.Drawing.Size(155, 26);
            // 
            // PolygonDeleteMenuItem
            // 
            this.PolygonDeleteMenuItem.Image = global::GKProject1.Properties.Resources.Delete;
            this.PolygonDeleteMenuItem.Name = "PolygonDeleteMenuItem";
            this.PolygonDeleteMenuItem.Size = new System.Drawing.Size(154, 22);
            this.PolygonDeleteMenuItem.Text = "Delete Polygon";
            this.PolygonDeleteMenuItem.Click += new System.EventHandler(this.PolygonDeleteMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1505, 627);
            this.Controls.Add(this.MainLayoutPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Polygons Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).EndInit();
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            this.PointFMenuStrip.ResumeLayout(false);
            this.EdgeMenuStrip.ResumeLayout(false);
            this.PolygonMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem StripMenu;
        private System.Windows.Forms.TableLayoutPanel MainLayoutPanel;
        private System.Windows.Forms.PictureBox DrawingArea;
        private System.Windows.Forms.TableLayoutPanel LeftPanel;
        private System.Windows.Forms.Button Draw_Polygon_Button;
        private System.Windows.Forms.Button ExamplePolygonButton1;
        private System.Windows.Forms.ToolStripMenuItem newBlueprintToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip PointFMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem PointFDeleteMenuItem;
        private System.Windows.Forms.ContextMenuStrip EdgeMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem EdgeDeleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EdgeRelationsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConstantLengthMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HorizontalLineMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VerticalLineMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EdgeAddVerticleMenuItem;
        private System.Windows.Forms.ContextMenuStrip PolygonMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem PolygonDeleteMenuItem;
        private System.Windows.Forms.Button ClearAllButton;
        private System.Windows.Forms.CheckBox BresenhamCheckBox;
        private System.Windows.Forms.ToolStripMenuItem EdgeDeleteRelationMenuItem;
        private System.Windows.Forms.Button ExamplePolygonButton2;
        private System.Windows.Forms.Button ExamplePolygonButton3;
        private System.Windows.Forms.CheckBox Hint;
    }
}

