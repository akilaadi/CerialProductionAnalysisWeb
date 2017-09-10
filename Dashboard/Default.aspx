<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<html>
    <head></head>
    <body>
        <h2>Cerial Production effectivenes (M.T per Hect)</h2>
        <asp:Chart ID="Chart1" runat="server" Width="1000px" Height="500px">
            <Series>
                <asp:Series Name="Series1">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisX IsLabelAutoFit="true" LabelAutoFitMaxFontSize="10" Title="Cerial Product" Interval="1">
                        <LabelStyle Angle="90" IsStaggered="false"/>
                    </AxisX>
                    <AxisY Title="M.T per Hect">

                    </AxisY>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </body>
</html>

