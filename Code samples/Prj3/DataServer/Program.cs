using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer
{
  class Program
  {
    static void Main(string[] args)
    {
      var tcplistener = new TcpListener(IPAddress.Any, 9999);
      tcplistener.Start();

      while (true)
      {
        TcpClient client = tcplistener.AcceptTcpClient();
        Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
        clientThread.Start(client);
      }
    }

    static private void HandleClientComm(object client)
    {
      TcpClient tcpClient = (TcpClient)client;
      var input = new BinaryReader(tcpClient.GetStream());
      var output = new BinaryWriter(tcpClient.GetStream());
      var running = true;
      while (running)
      {
        var request = input.ReadInt32();
        Console.WriteLine("Received request " + request);
        switch (request)
        {
          case 0: // quit
            running = false;
            break;
          case 1: // send all data
            using (var studentsDB = new StudentsDataContext())
            {
              var grades = studentsDB.Grades;
              output.Write(grades.Count());
              foreach (var grade in grades)
              {
                output.Write(grade.Name);
                output.Write(grade.Grade1);
              }
            }
            break;
        }
      }
    }
  }
}
