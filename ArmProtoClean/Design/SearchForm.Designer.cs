using System;
using System.Windows.Forms;

namespace ArmProtoClean.Design

{
    //partial class SearchForm
    //{
    //    /// <summary>
    //    /// Required designer variable.
    //    /// </summary>
    //    private System.ComponentModel.IContainer components = null;

    //    /// <summary>
    //    /// Clean up any resources being used.
    //    /// </summary>
    //    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing && (components != null))
    //        {
    //            components.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }

    //    #region Windows Form Designer generated code

    //    /// <summary>
    //    /// Required method for Designer support - do not modify
    //    /// the contents of this method with the code editor.
    //    /// </summary>
    //    private void InitializeComponent()
    //    {
    //        this.components = new System.ComponentModel.Container();
    //        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    //        this.Text = "SearchForm";
    //    }

    //    #endregion
    //}

    partial class SearchForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbTerritory = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbKindNpa = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAdoptionKindSubject = new System.Windows.Forms.ComboBox();
            this.cbAdoptionSubject = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFindAll = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCreateIndex = new System.Windows.Forms.Button();
            this.btnClearIndex = new System.Windows.Forms.Button();
            this.btnOptimizeIndx = new System.Windows.Forms.Button();
            this.chbLuceneSyntax = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpMin = new System.Windows.Forms.DateTimePicker();
            this.dtpMax = new System.Windows.Forms.DateTimePicker();
            this._bar = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCardAdoptionNumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(657, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Territory:";
            // 
            // cbTerritory
            // 
            this.cbTerritory.FormattingEnabled = true;
            this.cbTerritory.Items.AddRange(new object[] {
            "Все",
            "тестовая территория",
            "ru67.523.309",
            "Сургут"});
            this.cbTerritory.Location = new System.Drawing.Point(841, 79);
            this.cbTerritory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbTerritory.Name = "cbTerritory";
            this.cbTerritory.Size = new System.Drawing.Size(477, 24);
            this.cbTerritory.TabIndex = 1;
            this.cbTerritory.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(659, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "CardKind:";
            // 
            // cbKindNpa
            // 
            this.cbKindNpa.FormattingEnabled = true;
            this.cbKindNpa.Items.AddRange(new object[] {
            "Все",
            "конституция(устав)",
            "экспертное заключение",
            "постановление",
            "распоряжение",
            "решение",
            "УСТАВ МО",
            "акт органа прокуратуры"});
            this.cbKindNpa.Location = new System.Drawing.Point(841, 12);
            this.cbKindNpa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbKindNpa.Name = "cbKindNpa";
            this.cbKindNpa.Size = new System.Drawing.Size(477, 24);
            this.cbKindNpa.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(659, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "CardAdoptionKindSubject:";
            // 
            // cbAdoptionKindSubject
            // 
            this.cbAdoptionKindSubject.FormattingEnabled = true;
            this.cbAdoptionKindSubject.Items.AddRange(new object[] {
            "Все",
            "Представительный орган муниципального образования",
            "Местная администрация (исполнительно-распорядительный орган муниципального образо" +
                "вания)",
            "Сход граждан"});
            this.cbAdoptionKindSubject.Location = new System.Drawing.Point(841, 114);
            this.cbAdoptionKindSubject.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAdoptionKindSubject.Name = "cbAdoptionKindSubject";
            this.cbAdoptionKindSubject.Size = new System.Drawing.Size(477, 24);
            this.cbAdoptionKindSubject.TabIndex = 5;
            // 
            // cbAdoptionSubject
            // 
            this.cbAdoptionSubject.FormattingEnabled = true;
            this.cbAdoptionSubject.Items.AddRange(new object[] {
            "Все",
            "Тест Муниц",
            "Сход граждан Сургута",
            "Собрание депутатов Саратовского муниципального района",
            "Совет депутатов Никитинского сельского поселения Холм- Жирковского района Смоленс" +
                "кой области",
            "Совет депутатов Агибаловского сельского поселения Холм-Жирковского района Смоленс" +
                "кой области",
            "Администрация Никитинского сельского поселения Холм-Жирковского района Смоленской" +
                " области"});
            this.cbAdoptionSubject.Location = new System.Drawing.Point(841, 46);
            this.cbAdoptionSubject.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAdoptionSubject.Name = "cbAdoptionSubject";
            this.cbAdoptionSubject.Size = new System.Drawing.Size(477, 24);
            this.cbAdoptionSubject.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(657, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "CardAdoptionSubject:";
            // 
            // tbFindAll
            // 
            this.tbFindAll.Location = new System.Drawing.Point(173, 10);
            this.tbFindAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbFindAll.Multiline = true;
            this.tbFindAll.Name = "tbFindAll";
            this.tbFindAll.Size = new System.Drawing.Size(477, 66);
            this.tbFindAll.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 46);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Find All:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 185);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1305, 586);
            this.dataGridView1.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(21, 149);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(116, 28);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCreateIndex
            // 
            this.btnCreateIndex.Location = new System.Drawing.Point(145, 149);
            this.btnCreateIndex.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCreateIndex.Name = "btnCreateIndex";
            this.btnCreateIndex.Size = new System.Drawing.Size(116, 28);
            this.btnCreateIndex.TabIndex = 12;
            this.btnCreateIndex.Text = "Create Index";
            this.btnCreateIndex.UseVisualStyleBackColor = true;
            this.btnCreateIndex.Click += new System.EventHandler(this.btnCreateIndex_Click);
            // 
            // btnClearIndex
            // 
            this.btnClearIndex.Location = new System.Drawing.Point(393, 149);
            this.btnClearIndex.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearIndex.Name = "btnClearIndex";
            this.btnClearIndex.Size = new System.Drawing.Size(116, 28);
            this.btnClearIndex.TabIndex = 13;
            this.btnClearIndex.Text = "Clear Index";
            this.btnClearIndex.UseVisualStyleBackColor = true;
            this.btnClearIndex.Click += new System.EventHandler(this.btnClearIndex_Click);
            // 
            // btnOptimizeIndx
            // 
            this.btnOptimizeIndx.Location = new System.Drawing.Point(269, 149);
            this.btnOptimizeIndx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOptimizeIndx.Name = "btnOptimizeIndx";
            this.btnOptimizeIndx.Size = new System.Drawing.Size(116, 28);
            this.btnOptimizeIndx.TabIndex = 14;
            this.btnOptimizeIndx.Text = "Optimize Index";
            this.btnOptimizeIndx.UseVisualStyleBackColor = true;
            this.btnOptimizeIndx.Click += new System.EventHandler(this.btnOptimizeIndx_Click);
            // 
            // chbLuceneSyntax
            // 
            this.chbLuceneSyntax.AutoSize = true;
            this.chbLuceneSyntax.Location = new System.Drawing.Point(517, 154);
            this.chbLuceneSyntax.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chbLuceneSyntax.Name = "chbLuceneSyntax";
            this.chbLuceneSyntax.Size = new System.Drawing.Size(75, 21);
            this.chbLuceneSyntax.TabIndex = 17;
            this.chbLuceneSyntax.Text = "Строго";
            this.chbLuceneSyntax.UseVisualStyleBackColor = true;
            this.chbLuceneSyntax.CheckedChanged += new System.EventHandler(this.chbSearchStrong_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 124);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "AdoptionDate:";
            // 
            // dtpMin
            // 
            this.dtpMin.Location = new System.Drawing.Point(173, 117);
            this.dtpMin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpMin.Name = "dtpMin";
            this.dtpMin.Size = new System.Drawing.Size(240, 22);
            this.dtpMin.TabIndex = 19;
            this.dtpMin.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            // 
            // dtpMax
            // 
            this.dtpMax.Location = new System.Drawing.Point(423, 117);
            this.dtpMax.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpMax.Name = "dtpMax";
            this.dtpMax.Size = new System.Drawing.Size(227, 22);
            this.dtpMax.TabIndex = 20;
            this.dtpMax.Value = new System.DateTime(2014, 11, 20, 0, 0, 0, 0);
            // 
            // bar
            // 
            this._bar.Location = new System.Drawing.Point(657, 148);
            this._bar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._bar.Name = "_bar";
            this._bar.Size = new System.Drawing.Size(637, 28);
            this._bar.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 90);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "CardAdoptionNumber:";
            // 
            // tbCardAdoptionNumber
            // 
            this.tbCardAdoptionNumber.Location = new System.Drawing.Point(173, 86);
            this.tbCardAdoptionNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbCardAdoptionNumber.Name = "tbCardAdoptionNumber";
            this.tbCardAdoptionNumber.Size = new System.Drawing.Size(477, 22);
            this.tbCardAdoptionNumber.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1303, 155);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "0";
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 785);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbCardAdoptionNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this._bar);
            this.Controls.Add(this.dtpMax);
            this.Controls.Add(this.dtpMin);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chbLuceneSyntax);
            this.Controls.Add(this.btnOptimizeIndx);
            this.Controls.Add(this.btnClearIndex);
            this.Controls.Add(this.btnCreateIndex);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbFindAll);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbAdoptionSubject);
            this.Controls.Add(this.cbAdoptionKindSubject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbKindNpa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbTerritory);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTerritory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbKindNpa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbAdoptionKindSubject;
        private System.Windows.Forms.ComboBox cbAdoptionSubject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbFindAll;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCreateIndex;
        private System.Windows.Forms.Button btnClearIndex;
        private System.Windows.Forms.Button btnOptimizeIndx;
        private System.Windows.Forms.CheckBox chbLuceneSyntax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpMin;
        private System.Windows.Forms.DateTimePicker dtpMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCardAdoptionNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSearch;


    }
}

