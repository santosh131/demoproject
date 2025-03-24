///===============================================================================================
///Object Name          : FormViewItemData
///Object Type		    : Class
///Purpose			    : Data for Each Item in Form

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
using System.Web.UI.WebControls;

namespace SSG.CustomControls
{
    public class FormViewListItemData
    {
        public string FieldCode { get; set; }
        public  List<ListItem> SelectedItems { get; set; }
         
    }
}
