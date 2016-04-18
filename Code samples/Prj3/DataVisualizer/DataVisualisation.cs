using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataVisualizer
{
  public partial class DataVisualisation : Form
  {
    TcpClient tcpClient;
    BinaryReader input;
    BinaryWriter output;
    public DataVisualisation()
    {
      tcpClient = new TcpClient();
      tcpClient.Connect("localhost", 9999);
      var networkStream = tcpClient.GetStream();
      input = new BinaryReader(networkStream);
      output = new BinaryWriter(networkStream);
      InitializeComponent();
    }

    private void getData_Click(object sender, EventArgs ea)
    {
      gradesChart.Series.Clear();
      output.Write(1);
      var grades = new System.Windows.Forms.DataVisualization.Charting.Series
      {
        Name = "Grade",
        Color = System.Drawing.Color.CornflowerBlue,
        IsVisibleInLegend = true,
        IsXValueIndexed = true,
        ChartType = SeriesChartType.Bar,
        XValueType = ChartValueType.String,
        YValueType = ChartValueType.Double
      };

      gradesChart.Series.Add(grades);
      var numEntries = input.ReadInt32();
      for (int i = 0; i < numEntries; i++)
      {
        var name = input.ReadString();
        var grade = input.ReadDouble();
        var point = new DataPoint();
        point.SetValueXY(name, new object[] { grade + 1.0 });
        grades.Points.Add(point);
      }

    }

    private void disconnect_Click(object sender, EventArgs e)
    {
      output.Write(0);
      this.Close();
    }
  }
}
