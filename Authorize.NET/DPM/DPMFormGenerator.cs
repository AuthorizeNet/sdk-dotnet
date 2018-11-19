﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthorizeNet
{
    /// <summary>
    /// A class that builds out a DPM-ready form for submission to Auth.net.
    /// </summary>

    //@deprecated since version 1.9.8  
    //@deprecated Direct Post Method is replaced by Accept.JS https://developer.authorize.net/api/reference/features/acceptjs.html  
    //@deprecated Refer sample accept application: https://github.com/AuthorizeNet/accept-sample-app
    [Obsolete("AuthorizeNetDPM is deprecated, use Accept.JS instead. Refer sample accept application: https://github.com/AuthorizeNet/accept-sample-app.", false)]
    public static class DPMFormGenerator
    {
        public static string OpenForm(string apiLogin, string transactionKey, decimal amount, string returnUrl,
                                      bool isTest)
        {
            var sbForm = new StringBuilder("");

            var seq = Crypto.GenerateSequence();
            var stamp = Crypto.GenerateTimestamp();

            var fingerPrint = Crypto.GenerateFingerprint(transactionKey, apiLogin, amount, seq.ToString(),
                                                         stamp.ToString());

            var formAction = Gateway.LIVE_URL;

            //for testing
            if (isTest)
                formAction = Gateway.TEST_URL;

            sbForm.Append("<form action = '" + formAction + "' method = 'post'>\n");
            sbForm.Append("\t\t<input type = 'hidden' name = 'x_fp_hash' value = '" + fingerPrint + "' />\n");
            sbForm.Append("\t\t<input type = 'hidden' name = 'x_fp_sequence' value = '" + seq + "' />\n");
            sbForm.Append("\t\t<input type = 'hidden' name = 'x_fp_timestamp' value = '" + stamp + "' />\n");
            sbForm.Append("\t\t<input type = 'hidden' name = 'x_login' value = '" + apiLogin + "' />\n");
            sbForm.Append("\t\t<input type = 'hidden' name = 'x_amount' value = '" + amount + "' />\n");

            if ((!string.IsNullOrEmpty(returnUrl)) && (returnUrl.Trim().Length > 0))
            {
                sbForm.Append("\t\t<input type = 'hidden' name = 'x_relay_url' value = '" + returnUrl + "' />\n");
                sbForm.Append("\t\t<input type = 'hidden' name = 'x_relay_response' value = 'TRUE' />\n");
            }

            return sbForm.ToString();
        }

        public static string EndForm()
        {
            return "</form>";
        }
    }
}
