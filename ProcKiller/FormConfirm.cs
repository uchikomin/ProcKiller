using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcKiller
{
    /// <summary>
    /// 確認フォーム
    /// </summary>
    public partial class FormConfirm : Form
    {
        /// <summary>
        /// 確認文字列
        /// </summary>
        public string ConfirmText
        {
            get
            {
                // テキストボックスの内容を返す
                return textBoxConfirm.Text;
            }
        }


        public FormConfirm()
        {
            InitializeComponent();
        }
    }
}
