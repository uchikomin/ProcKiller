using System;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;

namespace ProcKiller
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// �\�[�^�[
        /// </summary>
        private ListViewColumnSorter sorter = new ListViewColumnSorter();

        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form�ǂݍ��ݎ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(Object sender, EventArgs e)
        {
            // �\�[�^�[�̐ݒ�
            listViewProcess.ListViewItemSorter = sorter;
        }

        // �Q��
        // https://usagi.hatenablog.jp/entry/2018/12/02/182819

        /// <summary>
        /// ���X�g�r���[�̃v���Z�X�����X�V���܂�
        /// </summary>
        /// <param name="keyword">�L�[���[�h</param>
        private void UpdateListViewProcess(string keyword)
        {
            // ���X�g�r���[�̍X�V��~
            listViewProcess.BeginUpdate();

            // ���X�g�r���[�̍��ڂ��N���A
            listViewProcess.Items.Clear();

            // ������
            using (var s = new ManagementObjectSearcher())
            {
                // �����N�G����ݒ�
                s.Query.QueryString = "select * from win32_process";
                // ���������s���Č��ʂ��擾
                using (var c = s.Get())
                {
                    // �R���N�V�����ɑ΂��ČJ��Ԃ�
                    foreach (var o in c)
                    {
                        //== Properties �����ׂďo�́i���Ԃ�������܂��j
                        //Debug.WriteLine("---------");
                        //foreach (var p in o.Properties)
                        //{
                        //    Debug.WriteLine("{0} : {1}", p.Name, p.Value);
                        //}

                        // �v���Z�XID
                        var pid = o["ProcessId"].ToString();
                        // �v���Z�X��
                        var name = o["Name"]?.ToString();
                        // �R�}���h���C��
                        var line = o["CommandLine"]?.ToString();

                        // �R�}���h���C������̏ꍇ
                        if (string.IsNullOrEmpty(line))
                        {
                            // �X�L�b�v
                            continue;
                        }
                        // �R�}���h���C���ɃL�[���[�h���܂܂�Ă��Ȃ��ꍇ
                        if (!line.Contains(keyword))
                        {
                            // �X�L�b�v
                            continue;
                        }

                        // ���X�g�r���[���ڂ𐶐�
                        var item = new ListViewItem();

                        item.Text = pid;
                        item.SubItems.Add(name);
                        item.SubItems.Add(line);

                        // ���X�g�r���[�ɒǉ�
                        listViewProcess.Items.Add(item);
                    }
                }
            }

            // ���X�g�r���[�̍X�V�ĊJ
            listViewProcess.EndUpdate();

            // �v���Z�X��~�{�^���� Enabled ��ύX����
            buttonKill.Enabled = listViewProcess.Items.Count > 0;
        }

        private void SearchProcess()
        {
            // �L�[���[�h
            string keyword = textBoxKeyword.Text;

            // �L�[���[�h���w�肳��Ă��Ȃ��ꍇ
            if (keyword == string.Empty)
            {
                MessageBox.Show("�L�[���[�h����͂��Ă�������");
                return;
            }

            // ���X�g�r���[���X�V
            UpdateListViewProcess(keyword);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // �v���Z�X������
            SearchProcess();
        }

        private void listViewProcess_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == sorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listViewProcess.Sort();
        }

        private void buttonKill_Click(object sender, EventArgs e)
        {
            // �m�F�t�H�[���𐶐�
            var formConfirm = new FormConfirm();

            // �m�F�t�H�[�����L�����Z�����ꂽ�ꍇ
            if (formConfirm.ShowDialog() != DialogResult.OK)
            {
                // �������Ȃ�
                return;
            }

            // �m�F�����񂪃L�[���[�h���قȂ�ꍇ
            if (textBoxKeyword.Text != formConfirm.ConfirmText)
            {
                // 
                MessageBox.Show("�L�[���[�h����v���܂���");
                // �����𒆎~
                return;
            }

            // 
            for (int i = 0; i < listViewProcess.Items.Count; i++)
            {
                // ���X�g�r���[���ڂ��擾
                ListViewItem item = listViewProcess.Items[i];

                // �v���Z�XID���擾
                int pid = int.Parse(item.Text);

                // �v���Z�X�����擾
                Process proc = Process.GetProcessById(pid);

                // �v���Z�X���擾�ł��Ȃ����
                if (proc == null)
                {
                    // �X�L�b�v
                    continue;
                }

                try
                {
                    // �v���Z�X���~
                    proc.Kill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("�v���Z�X�̒�~�Ɏ��s���܂���" + ex.Message);
                    // 
                    break;
                }
            }

            // �v���Z�X���Č���
            SearchProcess();
        }
    }
}
