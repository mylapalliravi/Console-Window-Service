using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
namespace mywinodwservice
{
    class heartbeat
    {
         //int ScheduleTime = 2;
        public Thread worker = null;
        public  void OnStart()
        {
            try
            {
                 ThreadStart start = new ThreadStart(Working);

                 worker = new Thread(start);
                Working();
                worker.Start();
            }
            catch (Exception)
            {

                throw;                                    
            }
        }

        //string longs;
        //int iserror;
        public void Working()
        {
            // string path = "C:\\whatsapdat.text";
            // StreamWriter writer = new StreamWriter(path, true);
            //  writer.WriteLine("consoel windowserviece"+DateTime.Now.ToString());
            // string[] lines = new string[] { DateTime.Now.ToString() };
            // File.AppendAllLines(@"C:\\whatsapdata.text", lines);
            //Thread.Sleep(ScheduleTime * 60 * 1000);
            int count = 1;
            while (count==1)
            {
                count++;
                string path = @"D:\Working_RND\Test3.text";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    DataSet data = new DataSet();
                    data = get_Whatsapdata();


                    for (int _i = 0; _i < data.Tables[0].Rows.Count; _i++)
                    {

                        writer.WriteLine(data.Tables[0].Rows[_i][0].ToString() + ' ' + data.Tables[0].Rows[_i][1].ToString() + ' ' + data.Tables[0].Rows[_i][2].ToString() + ' ' + data.Tables[0].Rows[_i][3].ToString() + ' ' + data.Tables[0].Rows[_i][4] + ' ' + data.Tables[0].Rows[_i][5] + ' ' + data.Tables[0].Rows[_i][6] + ' ' + data.Tables[0].Rows[_i][7] );
                        writer.WriteLine();
                    }
                   // writer.WriteLine(DateTime.Now.ToString());
                    writer.Close();
                  //  Whatsap_WidnowService(longs, iserror);


                }
                //Thread.Sleep(ScheduleTime * 60 * 1000);
            }


        }

        public void OnStop()
        {
            try
            {
                if ((worker != null) & worker.IsAlive)
                {
                    worker.Abort();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet get_Whatsapdata()
        {

            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
            SqlConnection con = new SqlConnection("server=192.168.1.7;database=saathi;uid=sa;password=!n1@Nd;");
            
            con.Open();
       
            SqlCommand cmd = new SqlCommand("[usp_WhatsAppMessage]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetUnsendMessagesAtt");
            cmd.Parameters.AddWithValue("@msgid", "3768");
            
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            sd.Fill(dt);
            con.Close();
            return dt;


        }

    }
}
