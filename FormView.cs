///===============================================================================================
///Object Name          : FormView
///Object Type		    : Class
///Purpose			    : To provide component as FormView

///Change History
///------------------------------------------------------------------------------------------------
///    Date         	Modified by	    Remarks  
///------------------------------------------------------------------------------------------------
///    09/26/2016	    Santosh	 	    Initial Version
///=============================================================================================== 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSG.CommonBusinessEntity;
using SSG.CommonBusinessEntity.Base;
using System.Web.UI.HtmlControls;

namespace SSG.CustomControls
{
    [ToolboxData("<{0}:FormView runat=server></{0}:FormView>")]
    public class FormView : WebControl, INamingContainer
    {
        #region Control Events

        public delegate void CreateFieldEventHandler(object sender, CreateFieldEventArgs e);
        public event CreateFieldEventHandler CreateField;

        public delegate void AutoPostBackEventHandler(object sender, AutoPostBackEventArgs e);
        public event AutoPostBackEventHandler AutoPostBackEvent;

        #endregion

        #region Private Variables

        private object _dataSource;
        private object _layoutDataSource;
        private List<FormViewListItemData> _listitem_dataSource;
        private TableItemStyle _ItemStyle;
        private TableItemStyle _altItemStyle;
        private TableItemStyle _headerStyle;
        private TableItemStyle _footerStyle;
        private bool _useDataSource = false;
        private bool _isColAdded = false;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or Sets the HeaderText
        /// </summary>
        public string HeaderText
        {
            get
            {
                if (ViewState["HeaderText"] != null)
                {
                    return ViewState["HeaderText"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["HeaderText"] = value; }
        }

        /// <summary>
        /// Gets or Sets the FooterText
        /// </summary>
        public string FooterText
        {
            get
            {
                if (ViewState["FooterText"] != null)
                {
                    return ViewState["FooterText"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["FooterText"] = value; }
        }

        ///// <summary>
        ///// Gets or Sets the AllowEdit
        ///// </summary>
        //public bool AllowEdit
        //{
        //    get
        //    {
        //        if (ViewState["AllowEdit"] != null)
        //        {
        //            return bool.Parse(ViewState["AllowEdit"].ToString());
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    set
        //    {
        //        ViewState["AllowEdit"] = value;
        //        if (value == false)
        //            FormMode = FormViewMode.View;
        //        else
        //            FormMode = FormViewMode.Edit;
        //    }
        //}

        /// <summary>
        /// Gets or Sets the EnableFields
        /// </summary>
        public bool EnableFields
        {
            get
            {
                if (ViewState["EnableFields"] != null)
                {
                    return bool.Parse(ViewState["EnableFields"].ToString());
                }
                else
                {
                    return true;
                }
            }
            set
            {
                ViewState["EnableFields"] = value;
            }
        }

        /// <summary>
        /// Gets or Sets the CssHeader
        /// </summary>
        public string CssHeader
        {
            get
            {
                if (ViewState["CssHeader"] != null)
                {
                    return ViewState["CssHeader"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssHeader"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssError
        /// </summary>
        public string CssError
        {
            get
            {
                if (ViewState["CssError"] != null)
                {
                    return ViewState["CssError"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }

            set { ViewState["CssError"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssFooter
        /// </summary>
        public string CssFooter
        {
            get
            {
                if (ViewState["CssFooter"] != null)
                {
                    return ViewState["CssFooter"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssFooter"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssCellCaption
        /// </summary>
        public string CssCellCaption
        {
            get
            {
                if (ViewState["CssCellCaption"] != null)
                {
                    return ViewState["CssCellCaption"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssCellCaption"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssFillerCaption
        /// </summary>
        public string CssFillerCaption
        {
            get
            {
                if ((ViewState["CssFillerCaption"] != null))
                {
                    return ViewState["CssFillerCaption"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssFillerCaption"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssCellValue
        /// </summary>
        public string CssCellValue
        {
            get
            {
                if (ViewState["CssCellValue"] != null)
                {
                    return ViewState["CssCellValue"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssCellValue"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssLabel
        /// </summary>
        public string CssLabel
        {
            get
            {
                if (ViewState["CssLabel"] != null)
                {
                    return ViewState["CssLabel"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssLabel"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssTextBox
        /// </summary>
        public string CssTextBox
        {
            get
            {
                if (ViewState["CssTextBox"] != null)
                {
                    return ViewState["CssTextBox"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssTextBox"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssDropDown
        /// </summary>
        public string CssDropDown
        {
            get
            {
                if (ViewState["CssDropDown"] != null)
                {
                    return ViewState["CssDropDown"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssDropDown"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssCheckBox
        /// </summary>
        public string CssCheckBox
        {
            get
            {
                if (ViewState["CssCheckBox"] != null)
                {
                    return ViewState["CssCheckBox"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssCheckBox"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssRadioButton
        /// </summary>
        public string CssRadioButton
        {
            get
            {
                if (ViewState["CssRadioButton"] != null)
                {
                    return ViewState["CssRadioButton"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssRadioButton"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssCheckBoxList
        /// </summary>
        public string CssCheckBoxList
        {
            get
            {
                if (ViewState["CssCheckBoxList"] != null)
                {
                    return ViewState["CssCheckBoxList"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssCheckBoxList"] = value; }
        }

        /// <summary>
        /// Gets or Sets the CssRadioButtonList
        /// </summary>
        public string CssRadioButtonList
        {
            get
            {
                if (ViewState["CssRadioButtonList"] != null)
                {
                    return ViewState["CssRadioButtonList"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["CssRadioButtonList"] = value; }
        }

        /// <summary>
        /// Gets or Sets the MandatorySpanStyle
        /// </summary>
        public string MandatorySpanStyle
        {
            get
            {
                if (ViewState["MandatorySpanStyle"] != null)
                {
                    return ViewState["MandatorySpanStyle"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["MandatorySpanStyle"] = value; }
        }

        /// <summary>
        /// Gets or Sets the ColumnCount
        /// </summary>
        public int ColumnCount
        {
            get { return Convert.ToInt32(ViewState["ColumnCount"]); }
            set { ViewState["ColumnCount"] = value; }
        }

        /// <summary>
        /// Gets or Sets the TotalVisibleFields
        /// </summary>
        public int TotalVisibleFields
        {
            get { return Convert.ToInt32(ViewState["TotalVisibleFields"]); }
            set { ViewState["TotalVisibleFields"] = value; }
        }

        /// <summary>
        /// Gets or Sets the TotalVisibleFillerFields
        /// </summary>
        public int TotalVisibleFillerFields
        {
            get { return Convert.ToInt32(ViewState["TotalVisibleFillerFields"]); }
            set { ViewState["TotalVisibleFillerFields"] = value; }
        }

        /// <summary>
        /// Gets or Sets the FormMode
        /// </summary>
        public FormViewMode FormMode
        {
            get
            {
                if (ViewState["FormMode"] == null)
                {
                    return FormViewMode.Edit;
                }
                return (FormViewMode)Enum.Parse(typeof(FormViewMode), ViewState["FormMode"].ToString());
            }
            set { ViewState["FormMode"] = value; }
        }

        /// <summary>
        /// Gets or Sets the Direction
        /// </summary>
        public FormViewDirection Direction
        {
            get
            {
                if (ViewState["Direction"] == null)
                {
                    return FormViewDirection.RowColumn;
                }
                return (FormViewDirection)Enum.Parse(typeof(FormViewDirection), ViewState["Direction"].ToString());
            }
            set { ViewState["Direction"] = value; }
        }

        /// <summary>
        /// Gets or Sets the ItemStyle
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle ItemStyle
        {
            get
            {
                if (_ItemStyle == null)
                {
                    _ItemStyle = new TableItemStyle();
                }

                if (IsTrackingViewState)
                {
                    ((IStateManager)_ItemStyle).TrackViewState();
                }
                return _ItemStyle;
            }
        }

        /// <summary>
        /// Gets or Sets the AlternatingItemStyle
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle AlternatingItemStyle
        {
            get
            {
                if (_altItemStyle == null)
                {
                    _altItemStyle = new TableItemStyle();
                }

                if (IsTrackingViewState)
                {
                    ((IStateManager)_altItemStyle).TrackViewState();
                }

                return _altItemStyle;
            }
        }

        /// <summary>
        /// Gets or Sets the HeaderStyle
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle HeaderStyle
        {
            get
            {
                if (_headerStyle == null)
                {
                    _headerStyle = new TableItemStyle();
                }

                if (IsTrackingViewState)
                {
                    ((IStateManager)_headerStyle).TrackViewState();
                }

                return _headerStyle;
            }
        }

        /// <summary>
        /// Gets or Sets the FooterStyle
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle FooterStyle
        {
            get
            {
                if (_footerStyle == null)
                {
                    _footerStyle = new TableItemStyle();
                }

                if (IsTrackingViewState)
                {
                    ((IStateManager)_footerStyle).TrackViewState();
                }

                return _footerStyle;
            }
        }

        /// <summary>
        /// Gets or Sets the TemplateUrl
        /// </summary>
        public string TemplateUrl
        {
            get
            {
                if (ViewState["TemplateUrl"] != null)
                {
                    return ViewState["TemplateUrl"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["TemplateUrl"] = value; }
        }

        /// <summary>
        /// Gets or Sets the Items
        /// </summary>
        [Browsable(false)]
        public FormViewItems Items
        {
            get
            {
                if (ViewState["Items"] == null)
                {
                    ViewState["Items"] = new FormViewItems();
                }
                return (FormViewItems)ViewState["Items"];
            }
            set { ViewState["Items"] = value; }
        }

        /// <summary>
        /// Gets or Sets the DataSource
        /// </summary>
        public object DataSource
        {
            get { return _dataSource; }
            set
            {
                if ((value == null || (value) is DataTable || (value) is DataView))
                {
                    _dataSource = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        /// <summary>
        /// Gets or Sets the FormViewListItemDataSource
        /// </summary>
        public List<FormViewListItemData> FormViewListItemDataSource
        {
            get { return _listitem_dataSource; }
            set
            {
                _listitem_dataSource = value;
            }
        }

        /// <summary>
        /// Gets or Sets the LayoutDataSource
        /// </summary>
        public object LayoutDataSource
        {
            get { return _layoutDataSource; }
            set
            {
                if ((value == null | (value) is DataTable | (value) is DataView))
                {
                    _layoutDataSource = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        #endregion

        #region Data Binding

        /// <summary>
        /// Performs the data binding
        /// </summary>
        public override void DataBind()
        {
            // Call the base method
            base.OnDataBinding(EventArgs.Empty);

            Controls.Clear();
            ClearChildViewState();
            TrackViewState();
            _useDataSource = true;
            CreateControlHierarchy();
            ChildControlsCreated = true;
        }

        /// <summary>
        /// Recreates the children using the saved view state
        /// </summary>
        protected override void CreateChildControls()
        {
            Controls.Clear();
            if ((ViewState["Items"] != null))
            {
                _useDataSource = false;
                CreateControlHierarchy();
            }
        }

        /// <summary>
        /// Renders the control 
        /// </summary>
        protected override void Render(HtmlTextWriter writer)
        {
            PrepareControlForRendering(writer);
            base.RenderContents(writer);
        }

        /// <summary>
        /// Create the graph of controls to be rendered
        /// </summary>
        protected virtual void CreateControlHierarchy()
        {
            if (_useDataSource)
            {
                LoadItemsFromDataSource();
            }

            // Create the outermost table
            Table outerTable = new Table();
            Controls.Add(outerTable);

            // Create the header row
            CreateHeader(outerTable);

            // Create all item rows
            if (Items.Count == 0)
            {
                CreateEmptyTable(outerTable);
            }
            else
            {
                CreateView(outerTable);
            }

            // Create the footer row
            CreateFooter(outerTable);

        }

        /// <summary>
        /// Adjust the style of the various controls to display
        /// </summary>
        protected virtual void PrepareControlForRendering(HtmlTextWriter writer)
        {

            ControlCollection ctrls = Controls;
            if (ctrls.Count < 1)
            {
                return;
            }

            // Configure the table
            Table table = (Table)ctrls[0];
            table.CopyBaseAttributes(this);
            if (ControlStyleCreated)
            {
                table.ApplyStyle(ControlStyle);
            }

            // Configure the items (including header and footer)
            int tbRowsCount = table.Rows.Count;

            // Style the header (1st row in the table)
            TableRow headerRow = table.Rows[0];
            if ((_headerStyle != null))
            {
                headerRow.ApplyStyle(_headerStyle);
            }
            else
            {
                headerRow.ApplyStyle(ControlStyle);
            }

            //Prepare the alternating style by summing normal and alternating
            TableItemStyle altStyle = _ItemStyle;
            if ((_altItemStyle != null))
            {
                altStyle = new TableItemStyle();
                altStyle.CopyFrom(_ItemStyle);
                altStyle.CopyFrom(_altItemStyle);
            }

            // Style the rows corresponding to (alternating) data items
            for (int i = 1; i <= tbRowsCount - 1; i++)
            {
                TableRow itemRow = table.Rows[i];
                TableItemStyle itemStyle = _ItemStyle;
                // start from 1
                if ((i % 2) == 0)
                {
                    itemStyle = altStyle;
                }

                if ((itemStyle != null))
                {
                    itemRow.ApplyStyle(itemStyle);
                }
                else
                {
                    itemRow.ApplyStyle(ControlStyle);
                }

            }

            // Determine the footer and pager row
            TableRow footerRow = table.Rows[tbRowsCount - 1];
            if ((_footerStyle != null))
            {
                footerRow.ApplyStyle(_footerStyle);
            }
            else
            {
                footerRow.ApplyStyle(ControlStyle);
            }

        }

        protected void AutoPostBackRaised(object sender, EventArgs e)
        {
            AutoPostBackEventArgs evtArgs = new AutoPostBackEventArgs();
            FormViewItems oItems = Items;
            foreach (FormViewItem oItem in oItems)
            {
                string sKey = "fv_" + oItem.FieldCode;
                if (((Control)sender).ID == sKey)
                {
                    evtArgs.Control = (WebControl)sender;
                    evtArgs.FieldItem = oItem;
                    break;
                }
            }
            if (AutoPostBackEvent != null)
            {
                AutoPostBackEvent(this, evtArgs);
            }

        }

        protected void RaiseCreateFieldEvent(WebControl webCtrl, FormViewItem formViewItem)
        {
            CreateFieldEventArgs evtArgs = new CreateFieldEventArgs();
            evtArgs.FieldItem = formViewItem;
            evtArgs.Control = webCtrl;
            // evtArgs.Cell = formViewItem.Cell;
            if (CreateField != null)
            {
                CreateField(this, evtArgs);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the Items from Datasource
        /// </summary>
        private void LoadItemsFromDataSource()
        {
            DataView layoutDataView = ResolveLayoutDataSource();
            if (layoutDataView == null)
            {
                return;
            }

            Items = new FormViewItems();
            int totalVisibleFields = 0;
            int totalVisibleFillerFields = 0;
            DataView dataValueView = ResolveDataSource();
            DataRowView dataViewRow = dataValueView[0];

            FormViewItem item;

            foreach (DataRowView drItem in layoutDataView)
            {

                item = new FormViewItem(drItem);

                if (ColumnExists(dataViewRow, item.FieldName))
                {
                    if (dataViewRow[item.FieldName] != DBNull.Value)
                    {
                        item.DataValue = dataViewRow[item.FieldName].ToString();
                    }
                }

                item.DataText = item.DataValue;

                if (FormViewListItemDataSource != null && FormViewListItemDataSource.Count > 0)
                {
                    var ele = FormViewListItemDataSource.Where(fc => fc.FieldCode == item.FieldCode).FirstOrDefault();
                    if (ele != null)
                    {
                        item.SelectedListItems = ele.SelectedItems;
                    }
                }
                if (item.Display)
                {
                    totalVisibleFields = totalVisibleFields + 1;
                    if (item.FieldTypeCode == FormViewConstants.LabelType.ToUpper())
                    {
                        totalVisibleFillerFields = totalVisibleFillerFields + 1;
                    }
                }

                Items.Add(item);

            }

            this.TotalVisibleFields = totalVisibleFields;
            this.TotalVisibleFillerFields = totalVisibleFillerFields;

        }

        /// <summary>
        /// Resolves the Layout DataSource
        /// </summary>
        /// <returns>DataView</returns>
        private DataView ResolveLayoutDataSource()
        {

            if (_layoutDataSource == null)
            {
                return null;
            }

            if (_layoutDataSource is DataTable)
            {
                return ((DataTable)_layoutDataSource).DefaultView;
            }

            if (_layoutDataSource is DataView)
            {
                return (DataView)_layoutDataSource;
            }
            return null;
        }

        /// <summary>
        /// Resolves the DataSource
        /// </summary>
        /// <returns>DataView</returns>
        private DataView ResolveDataSource()
        {
            if (_dataSource == null)
            {
                return null;
            }

            if (_dataSource is DataTable)
            {
                return ((DataTable)_dataSource).DefaultView;
            }

            if (_dataSource is DataView)
            {
                return (DataView)_dataSource;
            }
            return null;
        }

        /// <summary>
        /// Retrieves the Data
        /// </summary>
        /// <returns>FormViewItems</returns>
        public FormViewItems RetriveData()
        {

            FormViewItems formViewItems = Items;
            string fieldCode = string.Empty;
            object ctrl = null;
            string valueText = string.Empty;
            bool isChecked = false;
            DataTable dtCheckedList = new DataTable();
            DataTable dtSelectedList = new DataTable();
            DataRow dr;

            foreach (FormViewItem formViewItem in formViewItems)
            {
                fieldCode = formViewItem.FieldCode;
                ctrl = this.FindControl("fv_" + fieldCode);

                valueText = string.Empty;
                isChecked = false;
                dtCheckedList = new DataTable();
                dtSelectedList = new DataTable();
                dr = null;


                if ((ctrl != null))
                {
                    if (ctrl.GetType().Name.ToUpper().IndexOf(FormViewConstants.TextBoxType.ToUpper()) > -1)
                    {
                        valueText = ((TextBox)ctrl).Text;
                        formViewItem.DataValue = valueText;
                        formViewItem.DataText = valueText;
                    }
                    else if (ctrl.GetType().Name.ToUpper().IndexOf(FormViewConstants.TextAreaType.ToUpper()) > -1)
                    {
                        valueText = ((TextBox)ctrl).Text;
                        formViewItem.DataValue = valueText;
                        formViewItem.DataText = valueText;
                    }
                    else if (ctrl.GetType().Name.ToUpper().IndexOf(FormViewConstants.DropDownType.ToUpper()) > -1)
                    {
                        DropDownList ddl = (DropDownList)ctrl;
                        if (ddl.SelectedItem != null)
                        {
                            formViewItem.DataValue = ddl.SelectedItem.Value;
                            formViewItem.DataText = ddl.SelectedItem.Text;
                        }
                    }
                    else if (ctrl.GetType().Name.ToUpper().IndexOf(FormViewConstants.CheckBoxType.ToUpper()) > -1)
                    {
                        isChecked = ((CheckBox)ctrl).Checked;
                        formViewItem.DataValue = isChecked.ToString();
                        formViewItem.DataText = isChecked.ToString();
                    }
                    else if (ctrl.GetType().Name.ToUpper().IndexOf(FormViewConstants.RadioButtonType.ToUpper()) > -1)
                    {
                        isChecked = ((RadioButton)ctrl).Checked;
                        formViewItem.DataValue = isChecked.ToString();
                        formViewItem.DataText = isChecked.ToString();
                    }
                    else if (ctrl.GetType().Name.ToUpper().IndexOf(FormViewConstants.CheckBoxListType.ToUpper()) > -1)
                    {
                        CheckBoxList chkList = (CheckBoxList)ctrl;

                        formViewItem.SelectedListItems = new List<ListItem>();
                        foreach (ListItem item in chkList.Items)
                        {
                            if (item.Selected)
                            {
                                formViewItem.SelectedListItems.Add(item);
                            }
                        }
                    }
                    else if (ctrl.GetType().Name.ToUpper().IndexOf(FormViewConstants.RadioButtonListType.ToUpper()) > -1)
                    {
                        RadioButtonList rdList = (RadioButtonList)ctrl;

                        formViewItem.SelectedListItems = new List<ListItem>();
                        foreach (ListItem item in rdList.Items)
                        {
                            if (item.Selected)
                            {
                                formViewItem.SelectedListItems.Add(item);
                            }
                        }
                    }
                }
            }

            return formViewItems;

        }

        /// <summary>
        /// Create the HTML structure for the header
        /// </summary>
        /// <param name="table">Table</param>
        private void CreateHeader(Table table)
        {
            // Create the row
            TableRow row = new TableRow();

            // Add a cell
            TableCell tbCell = new TableCell();

            //Assign Css Class Name
            string cssText = CssHeader;

            table.Rows.Add(row);

            if (!string.IsNullOrEmpty(cssText))
            {
                tbCell.CssClass = cssText;
            }
            if (table.Rows.Count > 0)
            {
                tbCell.ColumnSpan = table.Rows[0].Cells.Count;
            }
            else
                tbCell.ColumnSpan = 2;
            tbCell.Text = HeaderText;
            row.Cells.Add(tbCell);
        }

        /// <summary>
        /// Create the HTML structure for the footer
        /// </summary>
        /// <param name="table">Table</param>
        private void CreateFooter(Table table)
        {
            // Create the row
            TableRow row = new TableRow();

            // Add a cell
            TableCell tbCell = new TableCell();

            //Assign Css Class Name
            string cssText = CssFooter;

            table.Rows.Add(row);

            if (!string.IsNullOrEmpty(cssText))
            {
                tbCell.CssClass = cssText;
            }
            if (table.Rows.Count > 0)
            {
                tbCell.ColumnSpan = table.Rows[0].Cells.Count;
            }
            else
                tbCell.ColumnSpan = 2;
            tbCell.Text = FooterText;
            row.Cells.Add(tbCell);
        }
        /// <summary>
        /// Create the HTML structure for the individual data item 
        /// </summary>
        /// <param name="oRow">TableRow</param>
        /// <param name="formViewItem">FormViewItem</param>
        /// <param name="cellIndex">Cell Index</param>
        /// <param name="colSpanNbr">ColSpanNbr</param>
        private void CreateItemRowColumn(TableRow oRow, FormViewItem formViewItem, int cellIndex, int colSpanNbr)
        {
            //CellIndex:  1		2 		3		4		5
            //TableCells:0,1     2,3     4,5     6,7     8,9
            cellIndex = cellIndex * 2;

            // Add the label 
            TableCell tbCell = oRow.Cells[cellIndex - 2];
            tbCell.CssClass = CssCellCaption;
            tbCell.Text = formViewItem.Caption;
            string mandSpanStyle = "color:red;";
            if (!string.IsNullOrEmpty(MandatorySpanStyle))
            {
                mandSpanStyle = MandatorySpanStyle;
            }

            if (formViewItem.Mandatory)
            {
                tbCell.Text = tbCell.Text + "<span style='" + mandSpanStyle + "'>" + " *" + "</span>";
            }
            tbCell.ID = "tdcaption_fv_" + formViewItem.FieldCode;

            // Add the value 
            TableCell tbCell2 = oRow.Cells[cellIndex-1];             
            if (colSpanNbr > 0)
            {
                tbCell2.ColumnSpan = colSpanNbr * 2;
            }
            
            if(!string.IsNullOrEmpty(formViewItem.CellStyleText))
            {
                tbCell2.Attributes.Add("style", formViewItem.CellStyleText);
            }

            for (int i = cellIndex; i < tbCell.ColumnSpan - 1; i++)
            {
                oRow.Cells[i + 1].Attributes.Add("Remove", "1");
            }
            switch (FormMode)
            {
                case FormViewMode.View:
                    tbCell2 = CreateItemViewOnly(tbCell2, formViewItem);
                    break;
                case FormViewMode.Edit:
                    tbCell2 = CreateItemEdit(tbCell2, formViewItem);
                    break;
            }
        }

        /// <summary>
        /// Create the HTML structure for the individual data item 
        /// </summary>
        /// <param name="oRow">TableRow</param>
        /// <param name="formViewItem">FormViewItem</param>
        /// <param name="cellIndex">Cell Index</param>
        private void CreateItem(TableRow oRow, FormViewItem formViewItem, int cellIndex)
        {
            if (formViewItem.FieldTypeCode.ToUpper() != FormViewConstants.LabelType.ToUpper())
            {
                // Add the label 
                TableCell tbCell = oRow.Cells[cellIndex];
                tbCell.CssClass = CssCellCaption;
                tbCell.Text = formViewItem.Caption;
                string mandSpanStyle = "color:red;";
                if (!string.IsNullOrEmpty(MandatorySpanStyle))
                {
                    mandSpanStyle = MandatorySpanStyle;
                }

                if (formViewItem.Mandatory)
                {
                    tbCell.Text = tbCell.Text + "<span style='" + mandSpanStyle + "'>" + " *" + "</span>";
                }
                tbCell.ID = "tdcaption_fv_" + formViewItem.FieldCode;

                // Add the value 
                TableCell tbCell2 = oRow.Cells[cellIndex + 1];

                switch (FormMode)
                {
                    case FormViewMode.View:
                        tbCell2 = CreateItemViewOnly(tbCell2, formViewItem);
                        break;
                    case FormViewMode.Edit:
                        tbCell2 = CreateItemEdit(tbCell2, formViewItem);
                        break;
                }
            }
            else
            {
                // Add the label - used for instructions
                TableCell tbCell = oRow.Cells[cellIndex];
                string cssText = CssCellCaption;
                if (!string.IsNullOrEmpty(CssFillerCaption))
                {
                    cssText = CssFillerCaption;
                }
                tbCell.Text = formViewItem.Caption;
                if (formViewItem.Mandatory)
                {
                    string mandSpanStyle = "color:red;";
                    if (!string.IsNullOrEmpty(MandatorySpanStyle))
                    {
                        mandSpanStyle = MandatorySpanStyle;
                    }

                    tbCell.Text = tbCell.Text + "<span style='" + mandSpanStyle + "'>" + " *" + "</span>";
                }
                tbCell.CssClass = cssText;
                tbCell.ColumnSpan = oRow.Cells.Count;
                tbCell.ID = "tdcaption_fv_" + formViewItem.FieldCode;
                for (int i = cellIndex; i < tbCell.ColumnSpan - 1; i++)
                {
                    oRow.Cells.RemoveAt(cellIndex + 1);
                }
                // oRow.Cells.RemoveAt(cellIndex + 1);
                // tbCell.ColumnSpan = oRow.Cells.Count;

            }
        }


        /// <summary>
        ///  Create the item in view-only mode (pure text) 
        /// </summary>
        /// <param name="tbCell">TableCell</param>
        /// <param name="formViewItem">FormViewItem</param>
        /// <returns>TableCell</returns>
        private TableCell CreateItemViewOnly(TableCell tbCell, FormViewItem formViewItem)
        {

            //Assign Css Class Name
            string sCss = CssCellValue;
            if (!string.IsNullOrEmpty(sCss))
            {
                tbCell.CssClass = sCss;
            }
            tbCell.ID = "tdval_fv_" + formViewItem.FieldCode;
            if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.DropDownType.ToUpper())
            {
                DropDownList dropDownCtrl = new DropDownList();
                dropDownCtrl.ID = "fv_" + formViewItem.FieldCode;
                RaiseCreateFieldEvent(dropDownCtrl, formViewItem);

                if (_useDataSource)
                {
                    if (dropDownCtrl.Items.Count > 0)
                    {
                        string valueCode = formViewItem.DataValue;
                        ListItem oList = dropDownCtrl.Items.FindByValue(valueCode);
                        if ((oList != null))
                        {
                            dropDownCtrl.SelectedValue = valueCode;
                            tbCell.Text = dropDownCtrl.SelectedItem.Text;
                        }
                    }
                }
            }
            else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.CheckBoxListType.ToUpper())
            {
                CheckBoxList checkBoxListCtrl = new CheckBoxList();
                checkBoxListCtrl.ID = "fv_" + formViewItem.FieldCode;

                RaiseCreateFieldEvent(checkBoxListCtrl, formViewItem);
                foreach (ListItem item in checkBoxListCtrl.Items)
                {
                    if (item.Selected)
                        tbCell.Text = tbCell.Text + ", " + item.Text;
                }
            }
            else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.RadioButtonListType.ToUpper())
            {
                RadioButtonList radioButtonListCtrl = new RadioButtonList();
                radioButtonListCtrl.ID = "fv_" + formViewItem.FieldCode;

                RaiseCreateFieldEvent(radioButtonListCtrl, formViewItem);
                foreach (ListItem item in radioButtonListCtrl.Items)
                {
                    if (item.Selected)
                        tbCell.Text = tbCell.Text + ", " + item.Text;
                }
            }
            else
            {

                RaiseCreateFieldEvent(tbCell, formViewItem);
                tbCell.Text = formViewItem.DataText;
            }
            return tbCell;
        }

        /// <summary>
        ///  Create the item in edit mode (textbox)
        /// </summary>
        /// <param name="tbCell">TableCell</param>
        /// <param name="formViewItem">FormViewItem</param>
        /// <returns>TableCell</returns>
        private TableCell CreateItemEdit(TableCell tbCell, FormViewItem formViewItem)
        {

            string cssText = CssCellValue;
            if (cssText != string.Empty)
            {
                tbCell.CssClass = cssText;
            }
            tbCell.ID = "tdval_fv_" + formViewItem.FieldCode;
            if (formViewItem.Edit)
            {
                if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.TextBoxType.ToUpper())
                {
                    TextBox textBoxCtrl = new TextBox();
                    if(!string.IsNullOrEmpty(formViewItem.StyleText))
                    {
                        textBoxCtrl.Attributes.Add("style", formViewItem.StyleText);
                    }
                    cssText = CssTextBox;
                    if (cssText != string.Empty)
                    {
                        textBoxCtrl.CssClass = cssText;
                    }
                    textBoxCtrl.ID = "fv_" + formViewItem.FieldCode;
                    if (formViewItem.DiplayWidthNbr > 0)
                    {
                        textBoxCtrl.Width = Unit.Pixel(formViewItem.DiplayWidthNbr);
                    }
                    if (formViewItem.MaxLength > 0)
                    {
                        textBoxCtrl.MaxLength = formViewItem.MaxLength;
                    }



                    tbCell.Controls.Add(textBoxCtrl);

                    RaiseCreateFieldEvent(textBoxCtrl, formViewItem);

                    if (formViewItem.AutoPostBack == true)
                    {
                        textBoxCtrl.AutoPostBack = true;
                        textBoxCtrl.TextChanged += AutoPostBackRaised;
                    }

                    textBoxCtrl.Text = formViewItem.DataText;
                    textBoxCtrl.Enabled = EnableFields;
                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.TextAreaType.ToUpper())
                {
                    TextBox textBoxCtrl = new TextBox();
                    cssText = CssTextBox;
                    if (!string.IsNullOrEmpty(formViewItem.StyleText))
                    {
                        textBoxCtrl.Attributes.Add("style", formViewItem.StyleText);
                    }
                    if (cssText != string.Empty)
                    {
                        textBoxCtrl.CssClass = cssText;
                    }
                    textBoxCtrl.ID = "fv_" + formViewItem.FieldCode;
                    textBoxCtrl.TextMode = TextBoxMode.MultiLine;
                    if (formViewItem.MaxLength > 0)
                    {
                        textBoxCtrl.Width = Unit.Pixel(formViewItem.MaxLength);
                    }
                    tbCell.Controls.Add(textBoxCtrl);

                    RaiseCreateFieldEvent(textBoxCtrl, formViewItem);

                    if (formViewItem.AutoPostBack == true)
                    {
                        textBoxCtrl.AutoPostBack = true;
                        textBoxCtrl.TextChanged += AutoPostBackRaised;
                    }

                    textBoxCtrl.Text = formViewItem.DataText;
                    textBoxCtrl.Enabled = EnableFields;

                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.DropDownType.ToUpper())
                {
                    DropDownList dropDownCtrl = new DropDownList();
                    cssText = CssDropDown;
                    if (!string.IsNullOrEmpty(formViewItem.StyleText))
                    {
                        dropDownCtrl.Attributes.Add("style", formViewItem.StyleText);
                    }
                    if (cssText != string.Empty)
                    {
                        dropDownCtrl.CssClass = cssText;
                    }
                    dropDownCtrl.ID = "fv_" + formViewItem.FieldCode;
                    if (formViewItem.MaxLength > 0)
                    {
                        dropDownCtrl.Width = Unit.Pixel(formViewItem.MaxLength);
                    }
                    tbCell.Controls.Add(dropDownCtrl);

                    RaiseCreateFieldEvent(dropDownCtrl, formViewItem);

                    //If Casding Require then post back it to fetch records for details.
                    if (formViewItem.AutoPostBack == true)
                    {
                        dropDownCtrl.AutoPostBack = true;
                        dropDownCtrl.SelectedIndexChanged += AutoPostBackRaised;
                    }

                    if (_useDataSource)
                    {
                        if (dropDownCtrl.Items.Count > 0)
                        {
                            string valueCode = formViewItem.DataValue;
                            ListItem oList = dropDownCtrl.Items.FindByValue(valueCode);
                            if ((oList != null))
                            {
                                dropDownCtrl.SelectedValue = valueCode;
                            }
                        }
                    }
                    dropDownCtrl.Enabled = EnableFields;
                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.CheckBoxType.ToUpper())
                {
                    CheckBox checkBoxCtrl = new CheckBox();
                    cssText = CssCheckBox;
                    if (!string.IsNullOrEmpty(formViewItem.StyleText))
                    {
                        checkBoxCtrl.Attributes.Add("style", formViewItem.StyleText);
                    }
                    if (cssText != string.Empty)
                    {
                        checkBoxCtrl.CssClass = cssText;
                    }
                    checkBoxCtrl.ID = "fv_" + formViewItem.FieldCode;
                    tbCell.Controls.Add(checkBoxCtrl);
                    checkBoxCtrl.Checked = Convert.ToBoolean(formViewItem.DataValue);

                    RaiseCreateFieldEvent(checkBoxCtrl, formViewItem);
                    if (formViewItem.AutoPostBack == true)
                    {
                        checkBoxCtrl.AutoPostBack = true;
                        checkBoxCtrl.CheckedChanged += AutoPostBackRaised;
                    }
                    checkBoxCtrl.Enabled = EnableFields;
                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.RadioButtonType.ToUpper())
                {
                    RadioButton radioButtonCtrl = new RadioButton();
                    cssText = CssCheckBox;
                    if (!string.IsNullOrEmpty(formViewItem.StyleText))
                    {
                        radioButtonCtrl.Attributes.Add("style", formViewItem.StyleText);
                    }
                    if (cssText != string.Empty)
                    {
                        radioButtonCtrl.CssClass = cssText;
                    }
                    radioButtonCtrl.ID = "fv_" + formViewItem.FieldCode;
                    tbCell.Controls.Add(radioButtonCtrl);
                    radioButtonCtrl.Checked = Convert.ToBoolean(formViewItem.DataValue);

                    RaiseCreateFieldEvent(radioButtonCtrl, formViewItem);
                    if (formViewItem.AutoPostBack == true)
                    {
                        radioButtonCtrl.AutoPostBack = true;
                        radioButtonCtrl.CheckedChanged += AutoPostBackRaised;
                    }
                    radioButtonCtrl.Enabled = EnableFields;
                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.CheckBoxListType.ToUpper())
                {
                    CheckBoxList checkBoxListCtrl = new CheckBoxList();
                    cssText = CssCheckBox;
                    if (!string.IsNullOrEmpty(formViewItem.StyleText))
                    {
                        checkBoxListCtrl.Attributes.Add("style", formViewItem.StyleText);
                    }
                    if (cssText != string.Empty)
                    {
                        checkBoxListCtrl.CssClass = cssText;
                    }
                    checkBoxListCtrl.ID = "fv_" + formViewItem.FieldCode;
                    tbCell.Controls.Add(checkBoxListCtrl);
                    //Assign the list and value in the creat field event

                    RaiseCreateFieldEvent(checkBoxListCtrl, formViewItem);
                    if (formViewItem.AutoPostBack == true)
                    {
                        checkBoxListCtrl.AutoPostBack = true;
                        checkBoxListCtrl.SelectedIndexChanged += AutoPostBackRaised;
                    }
                    if (_useDataSource)
                    {
                        if (checkBoxListCtrl.Items.Count > 0 && formViewItem.SelectedListItems.Count > 0)
                        {
                            foreach (ListItem itemList in checkBoxListCtrl.Items)
                            {
                                if (formViewItem.SelectedListItems.Where(v => v.Value == itemList.Value).Count() > 0)
                                {
                                    itemList.Selected = true;
                                }
                            }
                        }
                    }
                    checkBoxListCtrl.Enabled = EnableFields;
                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.RadioButtonListType.ToUpper())
                {
                    RadioButtonList radioButtonListCtrl = new RadioButtonList();
                    cssText = CssCheckBox;
                    if (!string.IsNullOrEmpty(formViewItem.StyleText))
                    {
                        radioButtonListCtrl.Attributes.Add("style", formViewItem.StyleText);
                    }
                    if (cssText != string.Empty)
                    {
                        radioButtonListCtrl.CssClass = cssText;
                    }
                    radioButtonListCtrl.ID = "fv_" + formViewItem.FieldCode;
                    tbCell.Controls.Add(radioButtonListCtrl);
                    //Assign the list and value in the creat field event

                    RaiseCreateFieldEvent(radioButtonListCtrl, formViewItem);
                    if (formViewItem.AutoPostBack == true)
                    {
                        radioButtonListCtrl.AutoPostBack = true;
                        radioButtonListCtrl.SelectedIndexChanged += AutoPostBackRaised;
                    }
                    if (_useDataSource)
                    {
                        if (radioButtonListCtrl.Items.Count > 0 && formViewItem.SelectedListItems.Count > 0)
                        {
                            foreach (ListItem itemList in radioButtonListCtrl.Items)
                            {
                                if (formViewItem.SelectedListItems.Where(v => v.Value == itemList.Value).Count() > 0)
                                {
                                    itemList.Selected = true;
                                }
                            }

                        }
                    }
                    radioButtonListCtrl.Enabled = EnableFields;
                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.HorizontalLineType.ToUpper())
                {
                    HtmlGenericControl htmlGenericControl = new System.Web.UI.HtmlControls.HtmlGenericControl("hr");
                    if (!string.IsNullOrEmpty(formViewItem.StyleText))
                    {
                        htmlGenericControl.Attributes.Add("style", formViewItem.StyleText);
                    }
                    tbCell.Controls.Add(htmlGenericControl);
                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.CustomType.ToUpper()) //TODO:
                {
                    RaiseCreateFieldEvent(tbCell, formViewItem);
                }
                else
                {

                    RaiseCreateFieldEvent(tbCell, formViewItem);
                    tbCell.Text = formViewItem.DataText;
                }
            }
            else//not Allow Edit 
            {
                if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.DropDownType.ToUpper())
                {
                    DropDownList dropDownCtrl = new DropDownList();
                    dropDownCtrl.ID = "fv_" + formViewItem.FieldCode;

                    RaiseCreateFieldEvent(dropDownCtrl, formViewItem);

                    if (_useDataSource)
                    {
                        if (dropDownCtrl.Items.Count > 0)
                        {
                            string valueCode = formViewItem.DataValue;
                            ListItem oList = dropDownCtrl.Items.FindByValue(valueCode);
                            if ((oList != null))
                            {
                                dropDownCtrl.SelectedValue = valueCode;
                                tbCell.Text = dropDownCtrl.SelectedItem.Text;
                            }
                        }
                    }
                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.CheckBoxListType.ToUpper())
                {
                    CheckBoxList checkBoxListCtrl = new CheckBoxList();
                    checkBoxListCtrl.ID = "fv_" + formViewItem.FieldCode;

                    RaiseCreateFieldEvent(checkBoxListCtrl, formViewItem);
                    foreach (ListItem item in checkBoxListCtrl.Items)
                    {
                        if (item.Selected)
                            tbCell.Text = tbCell.Text + ", " + item.Text;
                    }
                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.RadioButtonListType.ToUpper())
                {
                    RadioButtonList radioButtonListCtrl = new RadioButtonList();
                    cssText = CssCheckBox;
                    if (cssText != string.Empty)
                    {
                        radioButtonListCtrl.CssClass = cssText;
                    }
                    radioButtonListCtrl.ID = "fv_" + formViewItem.FieldCode;

                    RaiseCreateFieldEvent(radioButtonListCtrl, formViewItem);
                    foreach (ListItem item in radioButtonListCtrl.Items)
                    {
                        if (item.Selected)
                            tbCell.Text = tbCell.Text + ", " + item.Text;
                    }
                }
                else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.CustomType.ToUpper()) //TODO:
                {
                    RaiseCreateFieldEvent(tbCell, formViewItem);
                }
                else
                {

                    RaiseCreateFieldEvent(tbCell, formViewItem);
                    tbCell.Text = formViewItem.DataText;
                }
            }
            return tbCell;
        }

        /// <summary>
        /// Govern the generation of the HTML markup for the data items  
        /// </summary>
        /// <param name="table">Table</param>
        private void CreateView(Table table)
        {
            table.Width = Unit.Percentage(100);
            string cssText = CssClass;

            if (!string.IsNullOrEmpty(TemplateUrl))
            {
                CreateTemplateItem(table);
                return;
            }
            if (!string.IsNullOrEmpty(cssText))
            {
                table.CssClass = cssText;
            }

            int colCount = this.ColumnCount;
            int totalVisibleFields = this.TotalVisibleFields;
            int totalVisibleFillerFields = this.TotalVisibleFillerFields;
            Table colTable = new Table();
            TableRow mainRow = default(TableRow);
            TableCell mainCell = default(TableCell);
            TableRow dataRow = new TableRow();

            if (colCount == 0)
            {
                colCount = 1;
            }

            int totalRowsPerCol = totalVisibleFields / colCount;
            int rowCntRemainder = totalVisibleFields % colCount;
            int tempColumnCount = 0;
            int tempRowCount = 0;

            if (Direction == FormViewDirection.RowColumn)
            {

                totalRowsPerCol = Items.GetMaxRowNbr();
                colCount = Items.GetMaxColNbr();

                mainRow = new TableRow();
                table.Rows.Add(mainRow);

                mainCell = new TableCell();
                mainRow.Cells.Add(mainCell);

                colTable.Width = Unit.Percentage(100);
                mainCell.Controls.Add(colTable);


                for (int i = 0; i < totalRowsPerCol; i++)
                {
                    mainRow = new TableRow();
                    for (int k = 0; k < (colCount * 2); k++)
                    {
                        mainCell = new TableCell();
                        mainRow.Cells.Add(mainCell);
                    }
                    colTable.Rows.Add(mainRow);
                }

                foreach (FormViewItem formViewItem in Items)
                {
                    if (formViewItem.Display)
                    {
                        CreateItemRowColumn(colTable.Rows[formViewItem.RowNbr], formViewItem, formViewItem.ColNbr, formViewItem.ColSpanNbr);

                    }
                }
                for (int i = 0; i < colTable.Rows.Count; i++)
                {
                    TableRow oRow = colTable.Rows[i];
                    for (int k = 0; k < oRow.Cells.Count; k++)
                    {
                        if (oRow.Cells[k].HasAttributes && oRow.Cells[k].Attributes["Remove"] == "1")
                        {
                            oRow.Cells.RemoveAt(k);
                            k--;
                        }
                    }
                }

            }
            else if (Direction == FormViewDirection.Vertical)
            {
                mainRow = new TableRow();
                table.Rows.Add(mainRow);

                mainCell = new TableCell();
                mainRow.Cells.Add(mainCell);

                colTable.Width = Unit.Percentage(100);
                mainCell.Controls.Add(colTable);



                //Add additional row if the remainder is not zero
                if (rowCntRemainder != 0)
                {
                    totalRowsPerCol = totalRowsPerCol + 1;
                }

                //Layout the rows and cells for the table
                for (int i = 0; i < totalRowsPerCol; i++)
                {
                    mainRow = new TableRow();
                    for (int k = 0; k < (colCount * 2); k++)
                    {
                        mainCell = new TableCell();
                        mainRow.Cells.Add(mainCell);
                    }
                    colTable.Rows.Add(mainRow);
                }

                //1 5 9
                //2 6 10
                //3 7
                //4 8
                foreach (FormViewItem formViewItem in Items)
                {
                    if (formViewItem.Display)
                    {
                        //if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.FillerType.ToUpper())
                        //{
                        //    if (tempRowCount == totalRowsPerCol)
                        //    {
                        //        tempRowCount = 0;
                        //        tempColumnCount = tempColumnCount + 2;
                        //        CreateItem(colTable.Rows[tempRowCount], formViewItem, 0);
                        //        tempRowCount = tempRowCount + 1;
                        //    }
                        //    else
                        //    {
                        //        CreateItem(colTable.Rows[tempRowCount], formViewItem, 0);
                        //        tempRowCount = tempRowCount + 1;
                        //    }
                        //}
                        //else
                        //{
                        if (tempRowCount == totalRowsPerCol)
                        {
                            tempRowCount = 0;
                            tempColumnCount = tempColumnCount + 2;
                            CreateItem(colTable.Rows[tempRowCount], formViewItem, tempColumnCount);
                            tempRowCount = tempRowCount + 1;
                        }
                        else
                        {
                            CreateItem(colTable.Rows[tempRowCount], formViewItem, tempColumnCount);
                            tempRowCount = tempRowCount + 1;
                        }
                        // }
                    }
                }
            }
            else if (Direction == FormViewDirection.Horizontal)
            {
                totalRowsPerCol = (totalVisibleFields - totalVisibleFillerFields) / colCount;
                rowCntRemainder = (totalVisibleFields - totalVisibleFillerFields) % colCount;
                tempColumnCount = 0;
                tempRowCount = 0;

                totalRowsPerCol = totalRowsPerCol + totalVisibleFillerFields;

                mainRow = new TableRow();
                table.Rows.Add(mainRow);

                mainCell = new TableCell();
                mainRow.Cells.Add(mainCell);

                colTable.Width = Unit.Percentage(100);
                mainCell.Controls.Add(colTable);

                //Add additional row if the remainder is not zero
                if (rowCntRemainder != 0)
                {
                    totalRowsPerCol = totalRowsPerCol + 1;
                }

                //Layout the rows and cells for the table
                for (int i = 0; i < totalRowsPerCol; i++)
                {
                    mainRow = new TableRow();
                    for (int k = 0; k < (colCount * 2); k++)
                    {
                        mainCell = new TableCell();
                        mainRow.Cells.Add(mainCell);
                    }
                    colTable.Rows.Add(mainRow);
                }

                //1 2 3
                //4 5 6
                //7 8 9
                //10 
                foreach (FormViewItem formViewItem in Items)
                {
                    if (formViewItem.Display)
                    {
                        if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.LabelType.ToUpper())
                        {
                            if (tempColumnCount != 0)
                            {
                                tempRowCount = tempRowCount + 1;
                            }
                            tempColumnCount = 0;
                            CreateItem(colTable.Rows[tempRowCount], formViewItem, tempColumnCount);
                            tempColumnCount = (colCount * 2);
                        }
                        else
                        {
                            CreateItem(colTable.Rows[tempRowCount], formViewItem, tempColumnCount);
                            tempColumnCount = tempColumnCount + 2;

                        }
                        if (tempColumnCount == (colCount * 2))
                        {
                            tempRowCount = tempRowCount + 1;
                            tempColumnCount = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Create the Empty Table
        /// </summary>
        /// <param name="table">Table</param>
        private void CreateEmptyTable(Table table)
        {
            // Create the row
            TableRow oRow = new TableRow();

            // Add a cell
            TableCell tbCell = new TableCell();

            table.Rows.Add(oRow);

            tbCell.ColumnSpan = 2;
            tbCell.Text = string.Empty;
            //EmptyText
            oRow.Cells.Add(tbCell);
        }

        /// <summary>
        /// Create the Template
        /// </summary>
        /// <param name="table">Table</param>
        private void CreateTemplateItem(Table table)
        {
            // Create the row
            TableRow oRow = new TableRow();


            // Add a cell
            TableCell tbCell = new TableCell();
            UserControl userControl = null;

            table.Rows.Add(oRow);

            tbCell.ColumnSpan = 2;
            oRow.Cells.Add(tbCell);

            //Load the User Control
            userControl = (UserControl)Page.LoadControl(TemplateUrl);
            if (userControl == null)
            {
                return;
            }
            tbCell.Controls.Add(userControl);
            foreach (FormViewItem formViewItem in Items)
            {
                InitTemplateItem(userControl, formViewItem);
            }

        }

        /// <summary>
        /// Initialize in Template
        /// </summary>
        /// <param name="userControl">UserControl</param>
        /// <param name="formViewItem">FormViewItem</param>
        private void InitTemplateItem(UserControl userControl, FormViewItem formViewItem)
        {
            string ctrlID = "fv_" + formViewItem.FieldCode;
            Label labelCtrl = userControl.FindControl(ctrlID + "_Title") as Label;
            if (labelCtrl != null)
            {
                labelCtrl.Text = formViewItem.Caption;
                if (formViewItem.Mandatory)
                {
                    labelCtrl.Text = labelCtrl.Text + " *";
                }
            }

            if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.TextBoxType.ToUpper())
            {
                TextBox textBoxCtrl = userControl.FindControl(ctrlID) as TextBox;
                if (textBoxCtrl != null)
                {
                    RaiseCreateFieldEvent(textBoxCtrl, formViewItem);
                    textBoxCtrl.Text = formViewItem.DataText;
                    if (formViewItem.AutoPostBack == true)
                    {
                        textBoxCtrl.AutoPostBack = true;
                        textBoxCtrl.TextChanged += AutoPostBackRaised;
                    }
                    textBoxCtrl.Enabled = EnableFields;
                }
            }
            else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.DropDownType.ToUpper())
            {
                DropDownList dropDownCtrl = userControl.FindControl(ctrlID) as DropDownList;
                if (dropDownCtrl != null)
                {
                    RaiseCreateFieldEvent(dropDownCtrl, formViewItem);
                    if (formViewItem.AutoPostBack == true)
                    {
                        dropDownCtrl.AutoPostBack = true;
                        dropDownCtrl.SelectedIndexChanged += AutoPostBackRaised;
                    }
                    if (_useDataSource)
                    {
                        if (dropDownCtrl.Items.Count > 0)
                        {
                            string dataValText = formViewItem.DataValue;
                            ListItem lstItem = dropDownCtrl.Items.FindByValue(dataValText);
                            if ((lstItem != null))
                            {
                                dropDownCtrl.SelectedValue = dataValText;
                            }
                        }
                    }
                    dropDownCtrl.Enabled = EnableFields;
                }
            }
            else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.CheckBoxType.ToUpper())
            {
                CheckBox checkBoxCtrl = userControl.FindControl(ctrlID) as CheckBox;
                if (checkBoxCtrl != null)
                {
                    RaiseCreateFieldEvent(checkBoxCtrl, formViewItem);
                    checkBoxCtrl.Checked = Convert.ToBoolean(formViewItem.DataValue);
                    if (formViewItem.AutoPostBack == true)
                    {
                        checkBoxCtrl.AutoPostBack = true;
                        checkBoxCtrl.CheckedChanged += AutoPostBackRaised;
                    }
                    checkBoxCtrl.Enabled = EnableFields;
                }
            }
            else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.RadioButtonType.ToUpper())
            {
                RadioButton radioButtonCtrl = userControl.FindControl(ctrlID) as RadioButton;
                if (radioButtonCtrl != null)
                {
                    RaiseCreateFieldEvent(radioButtonCtrl, formViewItem);
                    radioButtonCtrl.Checked = Convert.ToBoolean(formViewItem.DataValue);
                    if (formViewItem.AutoPostBack == true)
                    {
                        radioButtonCtrl.AutoPostBack = true;
                        radioButtonCtrl.CheckedChanged += AutoPostBackRaised;
                    }
                    radioButtonCtrl.Enabled = EnableFields;
                }
            }
            else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.CheckBoxListType.ToUpper())
            {
                CheckBoxList checkBoxListCtrl = userControl.FindControl(ctrlID) as CheckBoxList;
                if (checkBoxListCtrl != null)
                {
                    //Assign the list and value in the creat field event
                    RaiseCreateFieldEvent(checkBoxListCtrl, formViewItem);
                    if (formViewItem.AutoPostBack == true)
                    {
                        checkBoxListCtrl.AutoPostBack = true;
                        checkBoxListCtrl.SelectedIndexChanged += AutoPostBackRaised;
                    }
                    checkBoxListCtrl.Enabled = EnableFields;
                }
            }
            else if (formViewItem.FieldTypeCode.ToUpper() == FormViewConstants.RadioButtonListType.ToUpper())
            {
                RadioButtonList radioButtonListCtrl = userControl.FindControl(ctrlID) as RadioButtonList;
                if (radioButtonListCtrl != null)
                {
                    //Assign the list and value in the creat field event
                    RaiseCreateFieldEvent(radioButtonListCtrl, formViewItem);
                    if (formViewItem.AutoPostBack == true)
                    {
                        radioButtonListCtrl.AutoPostBack = true;
                        radioButtonListCtrl.SelectedIndexChanged += AutoPostBackRaised;
                    }
                    radioButtonListCtrl.Enabled = EnableFields;
                }
            }
            else if (formViewItem.FieldTypeCode.ToUpper() == "Label".ToUpper())
            {
                Label label1Ctrl = userControl.FindControl(ctrlID) as Label;
                if (label1Ctrl != null)
                {
                    label1Ctrl.Text = formViewItem.DataText;
                }
            }
        }

        /// <summary>
        /// Checks if the column exists
        /// </summary>
        /// <param name="rowData">RowData</param>
        /// <param name="column">Column</param>
        /// <returns>bool</returns>
        public bool ColumnExists(DataRowView rowData, string column)
        {
            return rowData.Row.Table.Columns.Contains(column);
        }

        /// <summary>
        /// Enables or Disables the Field based on Field Code
        /// </summary>
        /// <param name="fieldCode">fieldCode</param>
        /// <param name="isEnabled">isEnabled</param>
        public void EnableDisableField(string fieldCode, bool isEnabled)
        {
            object ctrl = this.FindControl("fv_" + fieldCode);
            if (ctrl != null)
            {
                ((WebControl)ctrl).Enabled = isEnabled;
            }
        }

        /// <summary>
        /// Gets WebControl based on Field Code
        /// </summary>
        /// <param name="fieldCode">fieldCode</param>
        /// <param name="cssClass">cssClass</param>
        public WebControl GetWebControl(string fieldCode)
        {
            object ctrl = this.FindControl("fv_" + fieldCode);
            if (ctrl != null)
            {
                return ((WebControl)ctrl);
            }
            return null;
        }

        /// <summary>
        /// Gets WebControl based on Field Code
        /// </summary>
        /// <param name="fieldCode">fieldCode</param>
        /// <param name="cssClass">cssClass</param>
        public WebControl GetTableCellCaptionWebControl(string fieldCode)
        {
            object ctrl = this.FindControl("tdcaption_fv_" + fieldCode);
            if (ctrl != null)
            {
                return ((WebControl)ctrl);
            }
            return null;
        }

        /// <summary>
        /// Gets WebControl based on Field Code
        /// </summary>
        /// <param name="fieldCode">fieldCode</param>
        /// <param name="cssClass">cssClass</param>
        public WebControl GetTableCellValueWebControl(string fieldCode)
        {
            object ctrl = this.FindControl("tdval_fv_" + fieldCode);
            if (ctrl != null)
            {
                return ((WebControl)ctrl);
            }
            return null;
        }

        /// <summary>
        /// Sets css class to Table Cell(Caption, Value) based on Field Code
        /// </summary>
        /// <param name="fieldCode">fieldCode</param>
        /// <param name="cssClass">cssClass</param>
        public void SetTableCellCss(string fieldCode, string cssClass)
        {
            object ctrl = this.FindControl("tdcaption_fv_" + fieldCode);
            if (ctrl != null)
            {
                ((WebControl)ctrl).CssClass = cssClass;
            }

            ctrl = this.FindControl("tdval_fv_" + fieldCode);
            if (ctrl != null)
            {
                ((WebControl)ctrl).CssClass = cssClass;
            }
        }

        /// <summary>
        /// Sets css class to Table Cell(Caption) based on Field Code
        /// </summary>
        /// <param name="fieldCode">fieldCode</param>
        /// <param name="cssClass">cssClass</param>
        public void SetTableCellCaptionCss(string fieldCode, string cssClass)
        {
            object ctrl = this.FindControl("tdcaption_fv_" + fieldCode);
            if (ctrl != null)
            {
                ((WebControl)ctrl).CssClass = cssClass;
            }
        }

        /// <summary>
        /// Sets css class to Table Cell(Value) based on Field Code
        /// </summary>
        /// <param name="fieldCode">fieldCode</param>
        /// <param name="cssClass">cssClass</param>
        public void SetTableCellValueCss(string fieldCode, string cssClass)
        {
            object ctrl = this.FindControl("tdval_fv_" + fieldCode);
            if (ctrl != null)
            {
                ((WebControl)ctrl).CssClass = cssClass;
            }
        }
        #endregion

        #region View State Management

        /// <summary>
        ///  Override viewstate restoration to manage styles
        /// </summary>
        /// <param name="savedState">SavedState</param>
        protected override void LoadViewState(object savedState)
        {
            object baseState = null;
            object[] oState = null;

            if ((savedState != null))
            {
                oState = (object[])savedState;
                baseState = oState[0];
            }

            // Do as usual
            base.LoadViewState(baseState);

            // Handle custom state
            if (oState == null)
            {
                return;
            }

            // Restores ItemStyle
            if (oState[1] != null)
            {
                ((IStateManager)ItemStyle).LoadViewState(oState[1]);
            }

            // Restores AlternatingItemStyle
            if ((oState[2] != null))
            {
                ((IStateManager)AlternatingItemStyle).LoadViewState(oState[2]);
            }

            // Restores HeaderStyle
            if ((oState[3] != null))
            {
                ((IStateManager)HeaderStyle).LoadViewState(oState[3]);
            }

            // Restores FooterStyle
            if ((oState[4] != null))
            {
                ((IStateManager)FooterStyle).LoadViewState(oState[4]);
            }

        }

        /// <summary>
        ///  Override viewstate storage to manage styles
        /// </summary>
        /// <returns>object</returns>
        protected override object SaveViewState()
        {
            object[] oState = new object[6];

            // Store the default viewstate
            oState[0] = base.SaveViewState();

            // Store the ItemStyle property
            if (_ItemStyle == null)
            {
                oState[1] = null;
            }
            else
            {
                oState[1] = ((IStateManager)_ItemStyle).SaveViewState();
            }

            // Store the AlternatingItemStyle property
            if (_altItemStyle == null)
            {
                oState[2] = null;
            }
            else
            {
                oState[2] = ((IStateManager)_altItemStyle).SaveViewState();
            }

            // Store the HeaderStyle property
            if (_headerStyle == null)
            {
                oState[3] = null;
            }
            else
            {
                oState[3] = ((IStateManager)_headerStyle).SaveViewState();
            }

            // Store the FooterStyle property
            if (_footerStyle == null)
            {
                oState[4] = null;
            }
            else
            {
                oState[4] = ((IStateManager)_footerStyle).SaveViewState();
            }

            return oState;
        }

        /// <summary>
        ///  Override viewstate tracking
        /// </summary>
        protected override void TrackViewState()
        {
            base.TrackViewState();

            // Track view state for the ItemStyle property
            if ((_ItemStyle != null))
            {
                ((IStateManager)_ItemStyle).TrackViewState();
            }

            // Track view state for the AlternatingItemStyle property
            if ((_altItemStyle != null))
            {
                ((IStateManager)_altItemStyle).TrackViewState();
            }

            // Track view state for the HeaderStyle property
            if ((_headerStyle != null))
            {
                ((IStateManager)_headerStyle).TrackViewState();
            }

            // Track view state for the FooterStyle property
            if ((_footerStyle != null))
            {
                ((IStateManager)_footerStyle).TrackViewState();
            }
        }

        #endregion

    }
}
