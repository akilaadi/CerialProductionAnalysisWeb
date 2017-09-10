using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = "http://ec2-54-254-220-66.ap-southeast-1.compute.amazonaws.com:50075/webhdfs/v1/user/hduser/cerialProductionAnalysis-output/part-r-00000?op=OPEN&namenoderpcaddress=master:54310&offset=0";
        WebClient myWebClient = new WebClient();
        try
        {

            Dictionary<string, float> allLinesText = new Dictionary<string, float>();

            //Create a WebRequest to get the file
            HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(url);

            //Create a response for this request
            HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

            if (fileReq.ContentLength > 0)
                fileResp.ContentLength = fileReq.ContentLength;

            //Get the Stream returned from the response
            using (Stream stream = fileResp.GetResponseStream()) 
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    allLinesText.Add(line.Split('\t')[0], float.Parse(line.Split('\t')[1]));
                }
            }

            if (allLinesText.Count > 0)
            {
                Chart1.DataSource = allLinesText;
                Chart1.Series["Series1"].XValueMember = "Key";
                Chart1.Series["Series1"].YValueMembers = "Value";
                Chart1.DataBind();
            }
        }
        catch (Exception ex)
        { }

    }
}