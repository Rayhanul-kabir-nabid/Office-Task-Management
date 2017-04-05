using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENTITY;
namespace BO
{
    public class BOftp
    {
        FTPServerConnection ftpsc;
        public BOftp(EntityClass ee)
        {
            ftpsc = new FTPServerConnection(ee);
        }
        public void Uploadfile(string f, List<string> filename)
        {
            ftpsc.CreateDirectory(f);
            for (int i = 0; i < filename.Count; i++)
            {
                ftpsc.Uploadfile(f, filename[i]);
            }
        }


        public List<string> GetFilesListinDirectory(string p)
        {
            return ftpsc.GetFilesListinDirectory(p);
        }
        public bool DownloadFileFTP(string inputfilepath, string ftpfolder, string ftpname)
        {
            return ftpsc.DownloadFileFTP(inputfilepath+"\\"+ftpname, ftpfolder, ftpname);
        }
        public bool DownloadFileFTP_All(string inputfilepath, string ftpfolder)
        {
            List<string> filelist = ftpsc.GetFilesListinDirectory(ftpfolder);

            for (int i = 0; i < filelist.Count; i++)
            {
                ftpsc.DownloadFileFTP(inputfilepath+"\\"+filelist[i], ftpfolder, filelist[i]);
            }
            return true;
        }
        public bool deletefile(string folder)
        {
            return ftpsc.deletefile(folder);
        }
    }
}
