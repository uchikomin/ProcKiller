using System;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;

namespace ProcKiller
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// ソーター
        /// </summary>
        private ListViewColumnSorter sorter = new ListViewColumnSorter();

        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form読み込み時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(Object sender, EventArgs e)
        {
            // ソーターの設定
            listViewProcess.ListViewItemSorter = sorter;

            // 除外ボタンを無効化
            buttonRemove.Enabled = false;
        }

        /// <summary>
        /// メッセージを表示する
        /// </summary>
        /// <param name="msg">メッセージ</param>
        private void ShowMessage(string msg)
        {
            MessageBox.Show(this, msg);
        }

        // 参照
        // https://usagi.hatenablog.jp/entry/2018/12/02/182819

        /// <summary>
        /// リストビューのプロセス情報を更新します
        /// </summary>
        /// <param name="keyword">キーワード</param>
        private void UpdateListViewProcess(string keyword)
        {
            // リストビューの更新停止
            listViewProcess.BeginUpdate();

            // リストビューの項目をクリア
            listViewProcess.Items.Clear();

            // 初期化
            using (var s = new ManagementObjectSearcher())
            {
                // 検索クエリを設定
                s.Query.QueryString = "select * from win32_process";
                // 検索を実行して結果を取得
                using (var c = s.Get())
                {
                    // コレクションに対して繰り返し
                    foreach (var o in c)
                    {
                        //== Properties をすべて出力（時間がかかります）
                        //Debug.WriteLine("---------");
                        //foreach (var p in o.Properties)
                        //{
                        //    Debug.WriteLine("{0} : {1}", p.Name, p.Value);
                        //}

                        // プロセスID
                        var pid = o["ProcessId"].ToString();
                        // プロセス名
                        var name = o["Name"]?.ToString();
                        // コマンドライン
                        var line = o["CommandLine"]?.ToString();

                        // コマンドラインが空の場合
                        if (string.IsNullOrEmpty(line))
                        {
                            // スキップ
                            continue;
                        }
                        // コマンドラインにキーワードが含まれていない場合
                        if (!line.Contains(keyword))
                        {
                            // スキップ
                            continue;
                        }

                        // リストビュー項目を生成
                        var item = new ListViewItem();

                        item.Text = pid;
                        item.SubItems.Add(name);
                        item.SubItems.Add(line);

                        // リストビューに追加
                        listViewProcess.Items.Add(item);
                    }
                }
            }

            // リストビューの更新再開
            listViewProcess.EndUpdate();

            // プロセス停止ボタンの Enabled を変更する
            buttonKill.Enabled = listViewProcess.Items.Count > 0;
        }

        /// <summary>
        /// プロセスを検索する
        /// </summary>
        private void SearchProcess()
        {
            // キーワード
            string keyword = textBoxKeyword.Text;

            // キーワードが指定されていない場合
            if (keyword == string.Empty)
            {
                ShowMessage("キーワードを入力してください");
                return;
            }

            // リストビューを更新
            UpdateListViewProcess(keyword);
        }

        /// <summary>
        /// 検索ボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // プロセスを検索
            SearchProcess();
        }

        /// <summary>
        /// プロセス一覧のカラムをクリックしたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// プロセス停止ボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonKill_Click(object sender, EventArgs e)
        {
            // 確認フォームを生成
            var formConfirm = new FormConfirm();

            // 確認フォームがキャンセルされた場合
            if (formConfirm.ShowDialog() != DialogResult.OK)
            {
                // 何もしない
                return;
            }

            // 確認文字列がキーワードを異なる場合
            if (textBoxKeyword.Text != formConfirm.ConfirmText)
            {
                // 
                ShowMessage("キーワードが一致しません");
                // 処理を中止
                return;
            }

            // 
            for (int i = 0; i < listViewProcess.Items.Count; i++)
            {
                // リストビュー項目を取得
                ListViewItem item = listViewProcess.Items[i];

                // プロセスIDを取得
                int pid = int.Parse(item.Text);

                // プロセス情報を取得
                Process proc = Process.GetProcessById(pid);

                // プロセスが取得できなければ
                if (proc == null)
                {
                    // スキップ
                    continue;
                }

                try
                {
                    // プロセスを停止
                    proc.Kill();
                }
                catch (Exception ex)
                {
                    ShowMessage("プロセスの停止に失敗しました: " + ex.Message);
                    // 
                    break;
                }
            }

            // プロセスを再検索
            SearchProcess();
        }

        /// <summary>
        /// 選択が変更されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 一つでも選択されていれば、除外ボタンを押せるようにする
            buttonRemove.Enabled = listViewProcess.SelectedIndices.Count > 0;
        }

        /// <summary>
        /// 選択している項目を削除する
        /// </summary>
        private void RemoveSelectedItems()
        {
            // 描画停止
            listViewProcess.BeginUpdate();

            // 選択している項目に対して繰り返し
            foreach (ListViewItem item in listViewProcess.SelectedItems)
            {
                // リストから削除
                listViewProcess.Items.Remove(item);
            }

            // 描画再開
            listViewProcess.EndUpdate();
        }

        /// <summary>
        /// 除外ボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            // 選択している項目を削除する
            RemoveSelectedItems();
        }

        /// <summary>
        /// キーを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewProcess_KeyDown(object sender, KeyEventArgs e)
        {
            // 除外ボタンが押下可能で、DELETEキーを押した場合
            if (buttonRemove.Enabled && e.KeyCode == Keys.Delete)
            {
                // 選択している項目を削除する
                RemoveSelectedItems();
            }
        }
    }
}
