using System;
using System.Windows.Forms;

namespace TextProcessingApp
{
    public partial class TextProcessingForm : Form
    {
        public TextProcessingForm()
        {
            InitializeComponent();
            OpenTxtFile.Filter = Global.FILE_FILTERS;
            resetWorkSpace();
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            resetWorkSpace();
            if (OpenTxtFile.ShowDialog() == DialogResult.OK)
            {
                Controller.ParseFile(OpenTxtFile.FileName);
                Controller.ShowDictionary(DictionaryTable);
                FilePathLabel.Text = OpenTxtFile.FileName;
                SearchButton.Enabled = true;
                SearchTextBox.Enabled = true;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                RepetitionsTable.Rows.Clear();
                RepetitionsTable.Columns.Clear();
                RepetitionsTable.Refresh();
                Controller.ShowRepetitions(SearchTextBox.Text, RepetitionsTable);
            }
            catch (Exception ex)
            {
                SearchTextBox.Text = ex.Message;
            }
        }
        private void DictionaryTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DictionaryTable.CurrentCell.OwningColumn.HeaderText == Global.WORD_COLUMN)
                SearchTextBox.Text = DictionaryTable.CurrentCell.Value.ToString();
        }
        /// <summary>
        /// Set Windows Form to a default state
        /// </summary>
        private void resetWorkSpace()
        {
            SearchTextBox.Text = string.Empty;
            FilePathLabel.Text = string.Empty;
            SearchButton.Enabled = false;
            SearchTextBox.Enabled = false;
            SearchTextBox.Text = Global.TEXT_BOX_COPY_HINT;
            Controller.ResetTable(DictionaryTable);
            Controller.ResetTable(RepetitionsTable);
            Global.WordDictionary.Clear();
        }
    }
}