﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Globalization;


/// <summary>
/// FTPNEw 的摘要说明
/// </summary>
public class FTPNEw
{
    public FTPNEw()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

    }

    string ftpServerIP;

    string ftpUserID;

    string ftpPassword;

    FtpWebRequest reqFTP;

    private void Connect(String path)//连接ftp
    {

        // 根据uri创建FtpWebRequest对象

        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));

        // 指定数据传输类型

        reqFTP.UseBinary = true;

        // ftp用户名和密码

        reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

    }

    public void FtpUpDown(string ftpServerIP, string ftpUserID, string ftpPassword)
    {
        this.ftpServerIP = ftpServerIP;

        this.ftpUserID = ftpUserID;

        this.ftpPassword = ftpPassword;
    }

    //都调用这个

    private string[] GetFileList(string path, string WRMethods)//上面的代码示例了如何从ftp服务器上获得文件列表
    {
        string[] downloadFiles;
        StringBuilder result = new StringBuilder();
        try
        {
            Connect(path);

            reqFTP.Method = WRMethods;

            WebResponse response = reqFTP.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);//中文文件名

            string line = reader.ReadLine();

            while (line != null)
            {

                result.Append(line);

                result.Append("\n");

                line = reader.ReadLine();

            }

            // to remove the trailing '\n'

            result.Remove(result.ToString().LastIndexOf('\n'), 1);

            reader.Close();

            response.Close();

            return result.ToString().Split('\n');

        }

        catch (Exception ex)
        {
            //System.Windows.Forms.//MessageBox.Show(ex.Message);

            downloadFiles = null;

            return downloadFiles;
        }
    }

    public string[] GetFileList(string path)//上面的代码示例了如何从ftp服务器上获得文件列表
    {
        return GetFileList("ftp://" + ftpServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectory);
    }

    public string[] GetFileList()//上面的代码示例了如何从ftp服务器上获得文件列表
    {
        return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectory);
    }

    public void Upload(string filename, String directory, String newFileName) //上面的代码实现了从ftp服务器上载文件的功能
    {

        FileInfo fileInf = new FileInfo(filename);

        string uri = "ftp://" + ftpServerIP + "/" + directory + "/" + newFileName;

        if (!DirectoryIsExist(directory))
        {
            MakeDir(directory);
        }

        Connect(uri);//连接

        // 默认为true，连接不会被关闭

        // 在一个命令之后被执行

        reqFTP.KeepAlive = false;

        // 指定执行什么命令

        reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

        // 上传文件时通知服务器文件的大小

        reqFTP.ContentLength = fileInf.Length;
        // 缓冲大小设置为kb
        int buffLength = 2048;
        byte[] buff = new byte[buffLength];

        int contentLen;

        // 打开一个文件流(System.IO.FileStream) 去读上传的文件

        FileStream fs = fileInf.OpenRead();

        try
        {

            // 把上传的文件写入流

            Stream strm = reqFTP.GetRequestStream();

            // 每次读文件流的kb

            contentLen = fs.Read(buff, 0, buffLength);

            // 流内容没有结束

            while (contentLen != 0)
            {
                // 把内容从file stream 写入upload stream
                strm.Write(buff, 0, contentLen);
                contentLen = fs.Read(buff, 0, buffLength);

            }

            // 关闭两个流

            strm.Close();

            fs.Close();

        }

        catch (Exception ex)
        {

            //MessageBox.Show(ex.Message, "Upload Error");
        }

    }

    public bool Upload(byte[] FileBytes, string RemoteFileName, bool OverWriteRemoteFile)
    {
        //string uri = "ftp://" + ftpServerIP + "/" + directory + "/" + newFileName;
        string uri = "ftp://" + ftpServerIP + "/" + RemoteFileName;
        Connect(uri);//连接
        // 默认为true，连接不会被关闭
        // 在一个命令之后被执行
        reqFTP.KeepAlive = false;
        // 指定执行什么命令
        reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
        // 上传文件时通知服务器文件的大小
        //reqFTP.ContentLength = fileInf.Length;
        // 缓冲大小设置为kb
        int buffLength = 2048;
        byte[] buff = new byte[buffLength];
        int bytesRead = 0;
        int TotalRead = 0;
        try
        {
            // 把上传的文件写入流
            Stream strm = reqFTP.GetRequestStream();
            MemoryStream mem = new MemoryStream(FileBytes);
            while (true)
            {
                bytesRead = mem.Read(buff, 0, buff.Length);
                if (bytesRead == 0)
                    break;
                TotalRead += bytesRead;
                strm.Write(buff, 0, bytesRead);
            }
            //FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            // 关闭两个流
            mem.Close();
            mem.Dispose();
            strm.Close();
            return true;
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message, "Upload Error");
            return false;
        }
    }

    public bool Download(string filePath, string fileName, out string errorinfo)////上面的代码实现了从ftp服务器下载文件的功能
    {
        try
        {
            String onlyFileName = Path.GetFileName(fileName);

            string newFileName = filePath + "\\" + onlyFileName;

            if (File.Exists(newFileName))
            {

                errorinfo = string.Format("本地文件{0}已存在,无法下载", newFileName);
                return false;
            }
            string url = "ftp://" + ftpServerIP + "/" + fileName;
            Connect(url);//连接
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            long cl = response.ContentLength;
            int bufferSize = 2048;
            int readCount;
            byte[] buffer = new byte[bufferSize];
            readCount = ftpStream.Read(buffer, 0, bufferSize);

            FileStream outputStream = new FileStream(newFileName, FileMode.Create);
            while (readCount > 0)
            {
                outputStream.Write(buffer, 0, readCount);
                readCount = ftpStream.Read(buffer, 0, bufferSize);
            }
            ftpStream.Close();
            outputStream.Close();
            response.Close();

            errorinfo = "";

            return true;

        }

        catch (Exception ex)
        {
            errorinfo = string.Format("因{0},无法下载", ex.Message);

            return false;

        }

    }

    // Download File With Byte
    public byte[] Download(string fileName)
    {
        try
        {
            string url = "ftp://" + ftpServerIP + fileName;
            Connect(url);//连接
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            MemoryStream mem = new MemoryStream(2048 * 500);
            long cl = response.ContentLength;
            int bufferSize = 2048;
            int readCount = 0;
            int TotalByteRead = 0;
            byte[] buffer = new byte[bufferSize];
            //readCount = ftpStream.Read(buffer, 0, bufferSize);
            while (true)
            {
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                TotalByteRead += readCount;
                if (readCount == 0)
                    break;
                mem.Write(buffer, 0, readCount);
            }
            if (mem.Length > 0)
            {
                return mem.ToArray();
            }
            else
            {
                return null;
            }
            mem.Close();
            mem.Dispose();
            ftpStream.Close();
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            return null;
        }
    }

    //删除文件

    public void DeleteFileName(string fileName)
    {
        try
        {
            FileInfo fileInf = new FileInfo(fileName);

            string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;

            Connect(uri);//连接

            // 默认为true，连接不会被关闭

            // 在一个命令之后被执行

            reqFTP.KeepAlive = false;

            // 指定执行什么命令

            reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            response.Close();

        }

        catch (Exception ex)
        {

            //MessageBox.Show(ex.Message, "删除错误");

        }

    }

    //创建目录

    public void MakeDir(string dirName)
    {
        try
        {
            string uri = "ftp://" + ftpServerIP + "/" + dirName;

            Connect(uri);//连接

            reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            response.Close();

        }

        catch (Exception ex)
        {

            //MessageBox.Show(ex.Message);

        }

    }

    //删除目录

    public void delDir(string dirName)
    {
        try
        {
            string uri = "ftp://" + ftpServerIP + "/" + dirName;

            Connect(uri);//连接

            reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;

            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            response.Close();

        }

        catch (Exception ex)
        {

            //MessageBox.Show(ex.Message);

        }

    }

    //获得文件大小

    public long GetFileSize(string filename)
    {


        long fileSize = 0;

        try
        {

            FileInfo fileInf = new FileInfo(filename);

            string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;

            Connect(uri);//连接

            reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;

            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            fileSize = response.ContentLength;

            response.Close();

        }

        catch (Exception ex)
        {

            //MessageBox.Show(ex.Message);

        }

        return fileSize;

    }

    //文件改名

    public void Rename(string currentFilename, string newFilename)
    {
        try
        {
            //FileInfo fileInf = new FileInfo(currentFilename);

            //string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            string uri = "ftp://" + ftpServerIP + currentFilename;

            Connect(uri);//连接

            reqFTP.Method = WebRequestMethods.Ftp.Rename;

            reqFTP.RenameTo = newFilename;


            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            //Stream ftpStream = response.GetResponseStream();

            //ftpStream.Close();

            response.Close();

        }

        catch (Exception ex)
        {

            //MessageBox.Show(ex.Message);

        }

    }

    public bool ReNameFolder(string RemoteDirectoryName, string NewDirectoryName, string Flag)
    {
        try
        {
            string uri = "ftp://" + ftpServerIP + RemoteDirectoryName;
            Connect(uri);
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.Method = WebRequestMethods.Ftp.Rename;
            reqFTP.RenameTo = NewDirectoryName;
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            return true;
        }
        catch (Exception ep)
        {
            string ErrorMsg = ep.ToString();
            //throw ep;
            return false;
        }
    }


    /// <summary>
    /// 把当前目录下面的一个文件拷贝到服务器上面另外的目录中，注意，拷贝文件之后，当前工作目录还是文件原来所在的目录
    /// </summary>
    /// <param name="RemoteFile">当前目录下的文件名</param>
    /// <param name="DirectoryName">新目录名称。
    /// 说明：如果新目录是当前目录的子目录，则直接指定子目录。如: SubDirectory1/SubDirectory2 ；
    /// 如果新目录不是当前目录的子目录，则必须从根目录一级一级的指定。如： ./NewDirectory/SubDirectory1/SubDirectory2
    /// </param>
    /// <returns></returns>
    public bool CopyFileToAnotherDirectory(string RemoteFile, string DirectoryName,string DestinationName)
    {
        try
        {
            byte[] bt = Download(DirectoryName);
            //bool bolReturn = GotoDirectory(DirectoryName);
            bool Success = Upload(bt, DestinationName, false);
            return Success;
        }
        catch (Exception ep)
        {
            string ErrorMsg = ep.ToString();
            //throw ep;
            return false;
        }
    }

    //获得文件明晰

    public string[] GetFilesDetailList()
    {

        return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectoryDetails);

    }

    //获得文件明晰

    public string[] GetFilesDetailList(string path)
    {

        return GetFileList("ftp://" + ftpServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectoryDetails);
    }

    ///
    /// 检测目录是否存在
    ///
    ///
    /// false不存在，true存在
    public Boolean DirectoryIsExist(string dirName)
    {
        string[] value = GetFileList(dirName);
        if (value == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}