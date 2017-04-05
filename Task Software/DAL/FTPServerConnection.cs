using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
namespace DAL
{
    public class FTPServerConnection
    {
        // server info...............
        public string userName, password, ftpserver;

        public FTPServerConnection(EntityClass ee)
        {
            userName = ee.ftpusername;
            password = ee.ftppassword;
            ftpserver = ee.ftpserver;
        }

        public void Uploadfile(string f, string filename)
        {
            string fdir = ftpserver + "/" + f;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Credentials = new System.Net.NetworkCredential(userName, password);
                client.UploadFile(fdir + "/" + new FileInfo(filename).Name, "STOR", filename);
            }
        }

        public bool CreateDirectory(string dirPath)
        {
            bool done = true;
            string fullpath = ftpserver + "/" + dirPath;
            try
            {
                WebRequest request = WebRequest.Create(fullpath);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(userName, password);
                request.GetResponse();

            }
            catch (Exception)
            {
                done = false;
            }
            return done;
        }
        public List<string> GetFilesListinDirectory(string p)
        {
            string fullpath = ftpserver + "/" + p;
            List<string> directories = new List<string>();
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(fullpath);
                ftpRequest.Credentials = new NetworkCredential(userName, password);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());

                string line = streamReader.ReadLine();
                line = streamReader.ReadLine();
                line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    directories.Add(line);
                    //Console.WriteLine(line);
                    line = streamReader.ReadLine();

                }

                streamReader.Close();
            }
            catch (Exception)
            {
                
                
            }

            return directories;
        }
        public bool DownloadFileFTP(string inputfilepath, string ftpfolder, string ftpname)
        {
            string fullpath = ftpserver + "/" + ftpfolder + "/" + ftpname;
            try
            {
                FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(fullpath);
                requestFileDownload.Credentials = new NetworkCredential(userName, password);
                requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
                Stream responseStream = responseFileDownload.GetResponseStream();
                FileStream writeStream = new FileStream(inputfilepath, FileMode.Create);
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                responseStream.Close();
                writeStream.Close();
                requestFileDownload = null;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool deletefile(string folder)
        {
            string fullpath = ftpserver + "/" + folder;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fullpath);
            request.Credentials = new NetworkCredential(userName, password);

            request.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
            return true;
        }
    }
}
