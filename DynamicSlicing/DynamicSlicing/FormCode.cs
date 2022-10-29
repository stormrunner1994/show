using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicSlicing
{
    public partial class FormCode : Form
    {
		public FormCode(bool testen)
		{
			InitializeComponent();
			this.testen = testen;
			this.isShown = false;
			this.beispielcodes = new List<string>();
			this.datentypen = new List<string>();
			datentypen.Add("int");
			datentypen.Add("Integer");
			datentypen.Add("double");
			datentypen.Add("Double");
			datentypen.Add("float");
			datentypen.Add("Float");
			LadeBeispielCodes();
		}

		private List<string> datentypen;

		public bool isShown { get; set; }

		private List<string> beispielcodes;
		public string code { get; set; }
		private string fehler = "";
		private bool testen;

		private void LadeBeispielCodes()
		{
			string beispielcode1 = "begin" +
			"\n\tz = 0;" +
			"\n\tt = b;" +
			"\n\tif (x > b) then" +
			"\n\t\tbegin" +
			"\n\t\tx = x - b;" +
			"\n\t\tend" +
			"\n\tfi" +
			"\n\twhile (x <= t) do" +
			"\n\t\tbegin" +
			"\n\t\tt = t - x;" +
			"\n\t\tz = t + 1;" +
			"\n\t\tend" +
			"\n\tod" +
			"\n\twrite(z);" +
			"\nend";

			string beispielcode2 =
			"class Public" +
			"\n{" +
			"\n\tpublic static void main()" +
			"\n\t{" +
			"\n\t\tint z = 3;" +
			"\n\t\tint x = (z + y);" +
			"\n\t\tint y = 0;" +
			"\n\t\tint b = 0;" +
			"\n\t\tint a = 1;" +
			"\n\t\ty = (y + x);" +
			"\n\t\tx = (a - b);" +
			"\n\t\tb = (z + a);" +
			"\n\t\tz = y;" +
			"\n\t}" +
			"\n}";

			string beispielcode3 =
			"class Public1 {" +
			"\n\tpublic static void main() {" +
			"\n\t\tint z  = 3;" +
			"\n\t\tint x=(z+y);" +
			"\n\t\tint y = 0;" +
			"\n\t\tint b = 0;" +
			"\n\t\tint a = 1;" +
			"\n\t\ty = ( y + x );" +
			"\n\t\tx = (a-b);" +
			"\n\t\ty = (z+a);" +
			"\n\t}" +
			"\n}";

			string beispielcode4 =
			"class Test2 {" +
			"\n\tpublic static void main() {" +
			"\n\t\tint x = 1;" +
			"\n\t\tint y = 2;" +
			"\n\t\tint t = 0;" +
			"\n\t\twhile (y > 0)" +
			"\n\t\t{" +
			"\n\t\t\tz = (4 + x);" +
			"\n\t\t\ty = t;" +
			"\n\t\t\twhile (y < 0)" +
			"\n\t\t\t{" +
			"\n\t\t\t\ty = y;" +
			"\n\t\t\t}" +
			"\n\t\t}" +
			"\n\t\tz = z + y;" +
			"\n\t}" +
			"\n}";

			string beispielcode5 =
			"class Test7" +
			"\n{" +
			"\n\tpublic static void main()" +
			"\n\t{" +
			"\n\t\tint x = 1;" +
			"\n\t\tint y = 2;" +
			"\n\t\tint z = 0;" +
			"\n\t\tfor (y = 1; y >= 0; y--)" +
			"\n\t\t{" +
			"\n\t\t\tj = (4 + x);" +
			"\n\t\t\tif (y > 0)" +
			"\n\t\t\t{" +
			"\n\t\t\t\ty = 1;" +
			"\n\t\t\t}" +
			"\n\t\t\telse" +
			"\n\t\t\t{" +
			"\n\t\t\t\ty = y + z;" +
			"\n\t\t\t}" +
			"\n\t\t\ti = x;" +
			"\n\t\t}" +
			"\n\t\tz = (z + y);" +
			"\n\t}" +
			"\n}";

			string beispielcode6 =
			"int x = 3;" +
			"\nint y = 6;" +
			"\nfor (int a = x; a < y; a++)" +
			"\n{" +
			"\n\tx = x + 2;" +
			"\n}" +
			"\nwrite(x);" +
			"\nwrite(y);";

			beispielcodes.Add(beispielcode1);
			beispielcodes.Add(beispielcode2);
			beispielcodes.Add(beispielcode3);
			beispielcodes.Add(beispielcode4);
			beispielcodes.Add(beispielcode5);
			beispielcodes.Add(beispielcode6);
		}

		private void FormCode_Load(object sender, EventArgs e)
        {
			isShown = true;
			comboBoxexamples.SelectedIndex = 0;

			// Testmodus
			if (testen)
			{
				comboBoxexamples.SelectedIndex = 6;
				/*
				StreamReader sr = new StreamReader(@"2018.txt");
				richTextBoxcode.Text = sr.ReadToEnd();
				sr.Close();*/
				buttonformatecode_Click(sender, e);
				buttonchoosecode_Click(sender, e);
			}
		}

		public string GetBeispielCode(int index)
		{
			return beispielcodes[index];
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxexamples.SelectedIndex == -1) return;
			else if (comboBoxexamples.SelectedIndex == 0) richTextBoxcode.Text = "";
			else
				richTextBoxcode.Text = beispielcodes[comboBoxexamples.SelectedIndex-1];
		}

		private void buttonchoosecode_Click(object sender, EventArgs e)
		{
			this.code = richTextBoxcode.Text;
			isShown = false;
			this.Close();
		}

		private int zähleTabs(string zeile)
		{
			int count = 0;
			while (zeile.Length > 0 && zeile.First() == '\t')
			{
				zeile = zeile.Remove(0, 1);
				count++;
			}
			return count;
		}

		private void AddTabs(ref string zeile, int tabs)
		{
			for (int a = 0; a < tabs; a++)
				zeile = "\t" + zeile;
		}
		
		private string CodeFormatieren(string code)
		{
			int tabs = 0;
			string formatiert = "";
			try
			{
				// Ziel ist, "{", leere Zeilen zu ignorieren und nur Tabs zu erlauben
				string[] zeilen = code.Split('\n');
				foreach (string zeile in zeilen)
				{
					string trim = zeile.Trim();
					if (trim == "") continue;
					else if (trim == "{")
					{
						tabs++; continue;
					}
					else if (trim == "}")
					{
						tabs--; continue;
					}
					else if (zähleTabs(zeile) > tabs) tabs++;
					else if (zähleTabs(zeile) < tabs) tabs--;
					else if (zeile.Contains('{'))
					{
						trim = zeile.Trim().Remove(zeile.Trim().Length - 1, 1).Trim();
						AddTabs(ref trim, tabs);
						Helper.Append(ref formatiert, trim, "\n");
						tabs++;
						continue;
					}
					else if (zeile.Contains('}'))
					{
						trim = zeile.Trim().Remove(zeile.Trim().Length - 1, 1).Trim();
						AddTabs(ref trim, tabs);
						Helper.Append(ref formatiert, trim, "\n");
						tabs--; 
						continue;
					}

					// Datentypen entfernen
					foreach (string typ in datentypen)
					{
						if (trim.IndexOf(typ) == 0)
							trim = trim.Remove(0, typ.Length+1);
					}

					AddTabs(ref trim, tabs);
					Helper.Append(ref formatiert, trim, "\n");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			if (tabs < 0) MessageBox.Show("Der Code hat fehlerhafte Einrückungen!");


			return formatiert;
		}

		private void FührendeTabsEntfernen(ref List<string> zeilen)
		{
			int tabs = zähleTabs(zeilen.First());
			// jetzt noch unnötige, beginnde Tabs entfernen
			for (int a = 0; a < zeilen.Count; a++)
			{
				for (int b = 0; b < tabs; b++)
					zeilen[a] = zeilen[a].Remove(0, 1);
			}
		}

		private void FormCode_FormClosing(object sender, FormClosingEventArgs e)
		{
			isShown = false;
		}

		private void buttonformatecode_Click(object sender, EventArgs e)
		{
			richTextBoxcode.Text = CodeFormatieren(richTextBoxcode.Text);
			if (fehler == "" && richTextBoxcode.Text != "")
			{
				buttonchoosecode.Enabled = true;
				buttonformatecode.Enabled = false;
			}
			else
			{
				buttonchoosecode.Enabled = false;
				buttonformatecode.Enabled = true;
			}
		}

		private void buttondatei_Click(object sender, EventArgs e)
		{
			try
			{
				openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(
					System.Reflection.Assembly.GetExecutingAssembly().Location);
				openFileDialog1.FileName = "";
				openFileDialog1.ShowDialog();

				StreamReader sr = new StreamReader(openFileDialog1.FileName);
				richTextBoxcode.Text = sr.ReadToEnd();
				sr.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void FormCode_Shown(object sender, EventArgs e)
		{
			// Reset
			richTextBoxcode.Text = code;
			buttonchoosecode.Enabled = false;
			buttonformatecode.Enabled = true;
		}

		private void richTextBoxcode_TextChanged(object sender, EventArgs e)
		{
			if (buttonformatecode.Enabled == false)
			{
				buttonformatecode.Enabled = true;
				buttonchoosecode.Enabled = false;
			}
		}

    }
}
