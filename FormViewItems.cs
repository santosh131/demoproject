///===============================================================================================
///Object Name          : FormViewItems
///Object Type		    : Class
///Purpose			    : Items for Form

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
using System.Collections;

namespace SSG.CustomControls
{
    [Serializable]
    public class FormViewItems : CollectionBase
    {
        private List<FormViewItem> _lst;

        public FormViewItems()
        {
            _lst = new List<FormViewItem>();
        }
        /// <summary>
        /// Adds the element to the InnerList
        /// </summary>
        /// <param name="value">value</param>
        public void Add(FormViewItem value)
        {
            _lst.Add(value);
            InnerList.Add(value);
        }

        /// <summary>
        /// Returns the selected element
        /// </summary>
        public FormViewItem this[int index]
        {
            get { return (FormViewItem)InnerList[index]; }
            set { InnerList[index] = value; }
        }

        /// <summary>
        /// Gets the  Form View Item based on field code
        /// </summary>
        /// <param name="fieldCode">fieldCode</param>
        public FormViewItem GetItem(string fieldCode)
        {
            return _lst.Where(l=>l.FieldCode==fieldCode).FirstOrDefault();
        }

        /// <summary>
        /// Gets the  Form View Item based on field code
        /// </summary>
        /// <param name="fieldCode">fieldCode</param>
        /// <param name="parentFieldCode">parentFieldCode</param>
        public FormViewItem GetItem(string fieldCode,string parentFieldCode)
        {
            return _lst.Where(l => l.FieldCode == fieldCode && l.ParentFieldCode == parentFieldCode).FirstOrDefault();
        }

        /// <summary>
        /// Gets the  Form View Items based on field type
        /// </summary>
        /// <param name="fieldCode">fieldCode</param>
        public FormViewItem GetItems(string fieldType)
        {
            return _lst.Where(l => l.FieldTypeCode == fieldType).FirstOrDefault();
        }

        /// <summary>
        /// Gets the Max Row Nbr
        /// </summary>
        /// <returns></returns>
        public int GetMaxRowNbr()
        {
            return _lst.Max(c=>c.RowNbr);
        }

        /// <summary>
        /// Gets the Max Row Nbr
        /// </summary>
        /// <returns></returns>
        public int GetMaxColNbr()
        {
            return _lst.Max(c => c.ColNbr);
        }
    }
}
