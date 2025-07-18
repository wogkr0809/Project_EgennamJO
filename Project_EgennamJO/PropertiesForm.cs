using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using WeifenLuo.WinFormsUI.ThemeVS2015;
using Project_EgennamJO.Property;

namespace Project_EgennamJO
{
    public partial class PropertiesForm : DockContent
    {
        public enum PropertyType
        {
            Binary,
            Filter
        }
        public PropertiesForm()
        {
            InitializeComponent();

            LoadOptionControl(PropertyType.Filter);
            LoadOptionControl(PropertyType.Binary);
        }
        private void LoadOptionControl(PropertyType propType)
        {
            string tabName = propType.ToString();

            foreach(TabPage tabPage in tabPropControl.TabPages)
            {
                if (tabPage.Text == tabName)
                    return;

            }
            UserControl _inspProp = CreateUserControl(propType);
            if(_inspProp == null)
                return;

            TabPage newTab = new TabPage(tabName)
            {
                Dock = DockStyle.Fill
            };
            _inspProp.Dock = DockStyle.Fill;
            newTab.Controls.Add(_inspProp);
            tabPropControl.TabPages.Add(newTab);
            tabPropControl.SelectedTab = newTab; // 새 탭 선택

            _allTabs[tabName] = newTab;
        }
        private UserControl CreateUserControl(PropertyType propType)
        {
            UserControl curProp = null;
            switch (propType)
            {
                case PropertyType.Binary:
                    BinaryProp blobProp = new BinaryProp();
                    curProp = blobProp;
                    break;
                case PropertyType.Filter:
                    ImageFilterProp filterProp = new ImageFilterProp();
                    curProp = filterProp;
                    break;
                default:
                    MessageBox.Show("유효하지 않은 옵션입니다.");
                    return null;
            }
            return curProp;
        }
    }
}
