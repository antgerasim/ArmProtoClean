using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArmProtoClean.Helpers;
using ArmProtoClean.Lucene;
using ArmProtoClean.Model;
using ArmProtoClean.Sql;

namespace ArmProtoClean.Design
{

    public partial class SearchForm : Form
    {
        private ProgressBar _bar;

        public SearchForm()
        {
            InitializeComponent();
            InitializeAddComponents();
        }

        private Dictionary<string, object> ControlDict
        {
            //create dictionary with key=control.name and value=control.value. 
            //The values to be stored in CardCriterion field (CriteriaMaker(criteriaList)
            get
            {
                var controlDict = new Dictionary<string, object>
                {
                    {cbTerritory.Name, cbTerritory.Text},
                    {cbKindNpa.Name, cbKindNpa.Text},
                    {cbAdoptionKindSubject.Name, cbAdoptionKindSubject.Text},
                    {cbAdoptionSubject.Name, cbAdoptionSubject.Text},
                    {tbFindAll.Name, tbFindAll.Text},
                    {tbCardAdoptionNumber.Name, tbCardAdoptionNumber.Text},
                    //{tbCardEdition.Name, tbCardEdition.Text},
                    {dtpMin.Name, dtpMin.Value},
                    {dtpMax.Name, dtpMax.Value}
                };
                return controlDict;
            }
        }

        private static void InitializeAddComponents()
        {
            Loginform loginform = new Loginform {TopMost = true};
            loginform.Show();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbTerritory.SelectedIndex = 0;
            cbKindNpa.SelectedIndex = 0;
            cbAdoptionKindSubject.SelectedIndex = 0;
            cbAdoptionSubject.SelectedIndex = 0;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CriteriaManager criteriaManager = new CriteriaManager(ControlDict);

            List<KeyValuePair<string, object>> searchCriterion = criteriaManager.GetSearchCriterion();

            dataGridView1.DataSource = new SearchResults(searchCriterion, chbLuceneSyntax.Checked).GetSearchResults();
        }

        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            try
            {
                CardCriteriaRepository cardCriteriaRepository = new CardCriteriaRepository();
                cardCriteriaRepository.ProgressEvent +=
                    new EventHandler<ProgressBarEventArgs>(CardCriteriaRepository_ProgressEventHandler);
                SqlManager.ProgressEvent += SqlManager_ProgressEvent;

                SqlManager.CheckRowCount();
                SqlManager.CreateIndex();

                MessageBox.Show("Search index was created successfully");
            }
            catch (Exception ex)
            {
                #region Old

                //string exMsg = null;
                //string innerExMsg = null;
                //if (!string.IsNullOrEmpty(ex.Message))
                //{
                //    exMsg = ex.Message + "@@".Replace("@", Environment.NewLine);
                //}
                //try
                //{
                //    if (!string.IsNullOrEmpty(ex.InnerException.Message))
                //    {
                //        innerExMsg = ex.InnerException.Message + "@@".Replace("@", Environment.NewLine);
                //    }
                //}
                //catch
                //{

                //}

                #endregion

                MessageBox.Show("An error occoured, the application will be closed.");
                //MessageBox.Show("Sorry, but must specify Database Credentials to index your DB. This application will be closed.");
                Close();
                Application.Exit();
            }

        }

        private void SqlManager_ProgressEvent(object sender, ProgressBarEventArgs e)
        {
            _bar.Maximum = e.Maximum;
            _bar.Value += e.Progress;
        }

        private void CardCriteriaRepository_ProgressEventHandler(object sender, ProgressBarEventArgs e)
        {
            _bar.Maximum = e.Maximum;
            _bar.Value += e.Progress;
        }

        private void btnClearIndex_Click(object sender, EventArgs e)
        {
            if (LuceneIndex.ClearLuceneIndex())
                MessageBox.Show("Search index was cleared successfully!");
            else
                MessageBox.Show("Index is locked and cannot be cleared, try again later or clear manually!");
        }

        private void btnOptimizeIndx_Click(object sender, EventArgs e)
        {
            //Optimze method is deprecated since 4.0. Don use it!
            LuceneIndex.Optimize();
        }

        private void chbSearchStrong_CheckedChanged(object sender, EventArgs e)
        {
            tbFindAll.Text = chbLuceneSyntax.Checked && true ? "+" : string.Empty;
        }

        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e) { Application.Exit(); }

    }

}