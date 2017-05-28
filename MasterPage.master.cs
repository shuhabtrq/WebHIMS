using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AddAuthorMetaTag();
        }
    }

    protected void AddAuthorMetaTag()
    {
        System.Web.UI.HtmlControls.HtmlMeta metatagAuthor = new System.Web.UI.HtmlControls.HtmlMeta();         
        metatagAuthor.Name = "author";
        metatagAuthor.Content = "Shuhab (www.shuhab.com)";
        Page.Header.Controls.Add(metatagAuthor);
    }
}
