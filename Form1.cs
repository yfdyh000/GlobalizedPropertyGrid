using System;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;

namespace GlobalizedPropertyGrid
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Person person;
		private string[] supportedLanguages = null;

		internal System.Windows.Forms.PropertyGrid PropertyGrid1;
		private System.Windows.Forms.ComboBox cbLang;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Build an array of supported languages
			supportedLanguages = new string[2];
			supportedLanguages[0] = "en";
			supportedLanguages[1] = "de";

			// Instantiate test class and set some data
			person = new Person();
			person.FirstName = "Max";
			person.LastName = "Headroom";
			person.Age = 42;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PropertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.cbLang = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// PropertyGrid1
			// 
			this.PropertyGrid1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.PropertyGrid1.CommandsVisibleIfAvailable = true;
			this.PropertyGrid1.LargeButtons = false;
			this.PropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.PropertyGrid1.Location = new System.Drawing.Point(0, 40);
			this.PropertyGrid1.Name = "PropertyGrid1";
			this.PropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.PropertyGrid1.Size = new System.Drawing.Size(376, 200);
			this.PropertyGrid1.TabIndex = 1;
			this.PropertyGrid1.Text = "PropertyGrid1";
			this.PropertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.PropertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// cbLang
			// 
			this.cbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLang.Location = new System.Drawing.Point(8, 8);
			this.cbLang.Name = "cbLang";
			this.cbLang.Size = new System.Drawing.Size(121, 21);
			this.cbLang.TabIndex = 3;
			this.cbLang.SelectedIndexChanged += new System.EventHandler(this.cbLang_SelectedIndexChanged);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(376, 237);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cbLang,
																		  this.PropertyGrid1});
			this.Name = "Form1";
			this.Text = "Globalized Property Grid Demo";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// Setup combo box with languages available
			cbLang.Items.Insert(0,(new CultureInfo(supportedLanguages[0])).DisplayName);
			cbLang.Items.Insert(1,(new CultureInfo(supportedLanguages[1])).DisplayName);

			// Preselect the first one
			cbLang.SelectedIndex = 0;

			// Assign person to property grid
			PropertyGrid1.SelectedObject = person;
		}

		private void cbLang_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// get the language selected from combo box
			int lang = cbLang.SelectedIndex;
			if( lang == -1 )
				return;

			// Set selected language as the current one
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(supportedLanguages[lang]);

			PropertyGrid1.Refresh();
		}
	}
}
