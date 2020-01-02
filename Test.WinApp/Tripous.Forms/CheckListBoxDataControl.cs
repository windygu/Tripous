﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tripous.Forms
{

    /// <summary>
    /// CheckedListBox <see cref="DataControl"/>
    /// </summary>
    public partial class CheckListBoxDataControl : DataControl, IDataBindLookUp
    {

        /* constructor */
        /// <summary>
        /// Constructor
        /// </summary>
        public CheckListBoxDataControl()
        {
            InitializeComponent();

            this.Control = ListControl;
        }


        /* public */
        /// <summary>
        /// Data bind method
        /// </summary>
        public override Binding Bind()
        {
            string BindPropertyName = "SelectedItem";
            if (ListSource != null && !string.IsNullOrWhiteSpace(DisplayMember) && !string.IsNullOrWhiteSpace(ValueMember))
            {
                BindPropertyName = "SelectedValue";
            }

            return Ui.Bind(Control, BindPropertyName, DataSource, DataField);
        }
        /* properties */
        /// <summary>
        /// The name of the field to display.
        /// <para>NOTE: For ComboBox and ListBox only.</para>
        /// </summary>
        [DefaultValue(""), Localizable(false), Description("The name of the field to display.")]
        public string DisplayMember
        {
            get { return this.ListControl.DisplayMember; }
            set { this.ListControl.DisplayMember = value; }
        }
        /// <summary>
        /// The name of the field to be used as the actual control value.
        /// <para>NOTE: For ComboBox and ListBox only.</para>
        /// </summary>
        [DefaultValue(""), Localizable(false), Description("The name of the field to be used as the actual control value.")]
        public string ValueMember
        {
            get { return this.ListControl.ValueMember; }
            set { this.ListControl.ValueMember = value; }
        }
        /// <summary>
        /// The list source name of the Control.
        /// <para>NOTE: For ComboBox and ListBox only.</para>
        /// </summary>
        [DefaultValue(""), Localizable(false), Description("The list source name of the Control. ")]
        public string ListSourceName { get; set; }
        /// <summary>
        /// The list source of the Control.
        /// <para>NOTE: For ComboBox and ListBox only.</para>
        /// </summary>
        [DefaultValue(null), Browsable(false), Description("The list source of the Control. "), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object ListSource
        {
            get { return this.ListControl.DataSource; }
            set { this.ListControl.DataSource = value; }
        }
    }
}
