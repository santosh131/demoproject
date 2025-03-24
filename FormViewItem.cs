///===============================================================================================
///Object Name          : FormViewItem
///Object Type		    : Class
///Purpose			    : Each Item for Form

///Change History
///------------------------------------------------------------------------------------------------
///    Date         	Modified by	    Remarks  
///------------------------------------------------------------------------------------------------
///    09/26/2016	    Santosh	 	    Initial Version
///=============================================================================================== 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SSG.CommonBusinessEntity;
using SSG.CommonBusinessEntity.Base;
using System.Web.UI.WebControls;
using System.Globalization;

namespace SSG.CustomControls
{
    [Serializable]
    public class FormViewItem
    {
        #region Private Variables

        private string _field_name_txt;
        private string _field_cd;
        private string _display_txt;
        private string _data_value_txt;
        private string _data_desc_txt;
        private string _field_type_cd;
        private string _sub_field_type_cd; 
        private string _parent_field_cd;
        private bool _auto_postback_ind;
        private bool _mandatory_ind;
        private bool _display_ind;
        private bool _edit_ind;
        private int _max_length_nbr;
        private int _display_width_nbr;
        private string _cell_style_txt;
        private string _style_txt;
        private int _row_nbr;
        private int _col_nbr;
        private int _col_span_nbr;
        private List<ListItem> _selected_list_items;
        #endregion

        #region Constructors

        public FormViewItem()
        {
            _field_name_txt = string.Empty;
            _field_cd = string.Empty;
            _display_txt = string.Empty;
            _data_value_txt = string.Empty;
            _data_desc_txt = string.Empty;
            _field_type_cd = string.Empty;
            _sub_field_type_cd = string.Empty; 
            _parent_field_cd = string.Empty;
            _auto_postback_ind = false;
            _mandatory_ind = false;
            _display_ind = true;
            _edit_ind = true;
            _max_length_nbr = 0;
            _display_width_nbr = 0;
            _row_nbr = 0;
            _col_nbr = 0;
            _col_span_nbr = 0;
            _selected_list_items = new List<ListItem>();
            _cell_style_txt = string.Empty;
            _style_txt = string.Empty;
        } 
          
        public FormViewItem(DataRowView drItem)
        { 
            if (drItem["field_name_txt"] != DBNull.Value)
            {
                _field_name_txt = drItem["field_name_txt"].ToString();
            }

            if (drItem["field_cd"] != DBNull.Value)
            {
                _field_cd = drItem["field_cd"].ToString();
            }

            if (drItem["display_name_txt"] != DBNull.Value)
            {
                _display_txt = drItem["display_name_txt"].ToString();
            }
             
            if (drItem["field_type_cd"] != DBNull.Value)
            {
                _field_type_cd= drItem["field_type_cd"].ToString();
            }

            if (drItem["sub_field_type_cd"] != DBNull.Value)
            {
                _sub_field_type_cd = drItem["sub_field_type_cd"].ToString();
            }

            if (drItem["parent_field_cd"] != DBNull.Value)
            {
                _parent_field_cd= drItem["parent_field_cd"].ToString();
            }

            if (drItem["auto_postback_ind"] != DBNull.Value)
            {
                bool.TryParse(drItem["auto_postback_ind"].ToString(), out _auto_postback_ind);
            }

            if (drItem["mand_ind"] != DBNull.Value)
            {
                bool.TryParse(drItem["mand_ind"].ToString(), out _mandatory_ind);
            }

            if (drItem["display_ind"] != DBNull.Value)
            {
                bool.TryParse(drItem["display_ind"].ToString(), out _display_ind);
            } 

            if (drItem["edit_ind"] != DBNull.Value)
            {
                bool.TryParse(drItem["edit_ind"].ToString(), out _edit_ind);
            }

            if (drItem["max_length"] != DBNull.Value)
            {
                if (drItem["max_length"].ToString().IsInteger())
                    _max_length_nbr= int.Parse(drItem["max_length"].ToString());
            }

            if (drItem["display_width_nbr"] != DBNull.Value)
            {
                if (drItem["display_width_nbr"].ToString().IsInteger())
                    _display_width_nbr = int.Parse(drItem["display_width_nbr"].ToString());
            }

            if (drItem["row_nbr"] != DBNull.Value)
            {
                int.TryParse(drItem["row_nbr"].ToString(), out _row_nbr);
            }

            if (drItem["col_nbr"] != DBNull.Value)
            {
                int.TryParse(drItem["col_nbr"].ToString(), out _col_nbr);
            }

            if (drItem["col_span_nbr"] != DBNull.Value)
            {
                int.TryParse(drItem["col_span_nbr"].ToString(), out _col_span_nbr);
            }

            if (drItem["cell_style_txt"] != DBNull.Value)
            {
                _cell_style_txt = drItem["cell_style_txt"].ToString();
            }

            if (drItem["style_txt"] != DBNull.Value)
            {
                 _style_txt = drItem["style_txt"].ToString();
            }
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the FieldName
        /// </summary>
        public string FieldName
        {
            get { return _field_name_txt; }
        }

        /// <summary>
        /// Gets the FieldCode
        /// </summary>
        public string FieldCode
        {
            get { return _field_cd; }
        }

        /// <summary>
        /// Gets the Caption
        /// </summary>
        public string Caption
        {
            get { return _display_txt; }
        }

        /// <summary>
        /// Gets or Sets the DataValue
        /// </summary>
        public string DataValue
        {
            get { return _data_value_txt; }
            set { _data_value_txt = value; }
        } 

        /// <summary>
        /// Gets or Sets  the DataText
        /// </summary>
        public string DataText
        {
            get { return _data_desc_txt; }
            set { _data_desc_txt = value; }
        }

        /// <summary>
        /// Gets the FieldTypeCode
        /// </summary>
        public string FieldTypeCode
        {
            get { return _field_type_cd; }
        } 

        /// <summary>
        /// Gets the ParentFieldCode
        /// </summary>
        public string ParentFieldCode
        {
            get { return _parent_field_cd; }
        }

        /// <summary>
        /// Gets the AutoPostBack
        /// </summary>
        public bool AutoPostBack
        {
            get { return _auto_postback_ind; }
        }

        /// <summary>
        /// Gets the Mandatory
        /// </summary>
        public bool Mandatory
        {
            get { return _mandatory_ind; }
        }

        /// <summary>
        /// Gets the Display
        /// </summary>
        public bool Display
        {
            get { return _display_ind; }
        }

        /// <summary>
        /// Gets the Edit
        /// </summary>
        public bool Edit
        {
            get { return _edit_ind; }
        }

        /// <summary>
        /// Gets the Size
        /// </summary>
        public int MaxLength
        {
            get { return _max_length_nbr; }
        }


        /// <summary>
        /// Gets the DiplayWidthNbr
        /// </summary>
        public int DiplayWidthNbr
        {
            get { return _display_width_nbr; }
        }

        /// <summary>
        /// Gets the SubFieldTypeCd
        /// </summary>
        public string SubFieldTypeCd { 
            get { return _sub_field_type_cd; }
        }

        /// <summary>
        /// Gets the RowNbr
        /// </summary>
        public int RowNbr
        {
            get { return _row_nbr; }
        }

        /// <summary>
        /// Gets the ColNbr
        /// </summary>
        public int ColNbr
        {
            get { return _col_nbr; }
        }

        /// <summary>
        /// Gets the ColSpanNbr
        /// </summary>
        public int ColSpanNbr
        {
            get { return _col_span_nbr; }
        }

        /// <summary>
        /// Gets the SelectedListItems
        /// </summary>
        public List<ListItem> SelectedListItems
        {
            get { return _selected_list_items; }
            set { _selected_list_items = value; }
        }

        /// <summary>
        /// Gets the CellStyleText
        /// </summary>
        public string CellStyleText
        {
            get { return _cell_style_txt; }
        }

        /// <summary>
        /// Gets the StyleText
        /// </summary>
        public string  StyleText
        {
            get { return _style_txt; }
        }
        #endregion
    }
}
