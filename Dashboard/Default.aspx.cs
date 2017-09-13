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
    public Dictionary<string, float> job1Result, job2Result, job3Result;

    string url1 = "http://ec2-13-228-183-9.ap-southeast-1.compute.amazonaws.com:50075/webhdfs/v1/user/hduser/output/job1/part-r-00000?op=OPEN&namenoderpcaddress=master:54310&offset=0";
    string url2 = "http://ec2-13-228-183-9.ap-southeast-1.compute.amazonaws.com:50075/webhdfs/v1/user/hduser/output/job2/part-r-00000?op=OPEN&namenoderpcaddress=master:54310&offset=0";
    string url3 = "http://ec2-13-228-183-9.ap-southeast-1.compute.amazonaws.com:50075/webhdfs/v1/user/hduser/output/job3/part-r-00000?op=OPEN&namenoderpcaddress=master:54310&offset=0";


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            job1Result = ReadJobResult(url1);
            job2Result = ReadJobResult(url2);
            job3Result = ReadJobResult(url3);

            if (!this.IsPostBack)
            {
                DataBindProductDropDown();
                DataBindChart2();
            }

            if (job1Result.Count > 0)
            {
                Chart1.DataSource = job1Result;
                Chart1.Series["Series1"].XValueMember = "Key";
                Chart1.Series["Series1"].YValueMembers = "Value";
                Chart1.DataBind();
            }

            if (job3Result.Count > 0)
            {
                Chart3.DataSource = job1Result;
                Chart3.Series["Series1"].XValueMember = "Key";
                Chart3.Series["Series1"].YValueMembers = "Value";
                Chart3.DataBind();
            }
        }
        catch (Exception ex)
        { }

    }

    private Dictionary<string, float> ReadJobResult(string resultUrl)
    {
        WebClient myWebClient = new WebClient();
        try
        {
            Dictionary<string, float> jobResult = new Dictionary<string, float>();

            //Create a WebRequest to get the file
            HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(resultUrl);

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
                    jobResult.Add(line.Split('\t')[0], float.Parse(line.Split('\t')[1]));
                }
            }

            return jobResult;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public void DataBindChart2()
    {
        if (job2Result != null && job2Result.Count > 0)
        {
            Dictionary<string, float> selected = new Dictionary<string, float>();
            job2Result.Where(t => t.Key.Split('-')[0].Equals(DropDownList1.SelectedValue))
                .OrderBy(t => t.Value)
                .ToList()
                .ForEach(t =>
                {
                    selected.Add(t.Key.Split('-')[1], t.Value);
                });

            Chart2.DataSource = selected;
            Chart2.Series["Series1"].XValueMember = "Key";
            Chart2.Series["Series1"].YValueMembers = "Value";
            Chart2.DataBind();
        }
    }

    public void DataBindProductDropDown()
    {
        string[] districtList = job2Result.Select(t => t.Key.Split('-')[0]).Distinct().ToArray();
        DropDownList1.DataSource = districtList;
        DropDownList1.DataBind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBindChart2();
    }
}