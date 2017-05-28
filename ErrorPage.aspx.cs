using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int intErrCode = 0;

        try
        {
            intErrCode = int.Parse(Request.QueryString["Param"]);

            switch (intErrCode)
            {
                case 9999: //ErrorCode=9999: Application Level Error - Control transfers from Global.asax // Exception objErr = Server.GetLastError().GetBaseException();
                    Exception objErr = (Exception)SessionManager.GetSession(SessionManager.ApplicationErrorObject);
                    lblErrDescValue.Text = "Error Code : " + intErrCode + " - A server error occurred while executing request.";
                    lblErrReasonValue.Text = "Reason     : " + objErr.Message.ToString();
                    lblErrSolValue.Text = "Solution   : Please restrart your machine and try again. If issue still persists, contact Administrator for support, quoting your Error Code.";  //:  mailme@shuhab.com";
                    break;

                case 10000: // Error 10000: Called from web.config
                    lblErrDescValue.Text = "Error Code : " + intErrCode;
                    lblErrReasonValue.Text = "Most likely reasons     : 1. Session Expiry, 2. Machine failed to respond to a server side request, 3. Problem establishing a db connection";
                    lblErrSolValue.Text = "Solution   : Please restrart your machine and try again. If issue still persists, please contact Administrator at : support@ets-it.com";
                    break;

                default:
                    lblErrDescValue.Text = "Unknown Error";
                    lblErrReasonValue.Text = "Most likely reasons     : 1. Session Expiry, 2. Machine failed to respond to a server side request, 3. Problem establishing a db connection";
                    lblErrSolValue.Text = "Solution   : Please restrart your machine and try again. If issue still persists, please contact Administrator at : support@ets-it.com";
                    break;
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
            lblErrDescValue.Text = "Unknown Error";
            lblErrReasonValue.Text = "Unhandled exception";
            lblErrSolValue.Text = "Solution   : Please restrart your machine and try again. If issue still persists, please contact Administrator at : support@ets-it.com";
        }

    }
}