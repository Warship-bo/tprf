using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using Renci.SshNet; // 需要安装SSH.NET库 (NuGet包: Renci.SshNet)

/// <summary>
/// SFTP操作帮助类
/// </summary>
public class SftpHelper : IDisposable
{
    private SftpClient _sftpClient;
    private bool _disposed = false;

    // 服务器连接信息
    private string _host;
    private int _port;
    private string _username;
    private string _password;

    public SftpHelper(string host = "172.16.253.100", int port = 22,
                      string username = "tprf", string password = "Tprf!ftp0427")
    {
        _host = host;
        _port = port;
        _username = username;
        _password = password;
    }

    // 连接到SFTP服务器
    private void Connect()
    {
        if (_sftpClient == null || !_sftpClient.IsConnected)
        {
            _sftpClient = new SftpClient(_host, _port, _username, _password);
            _sftpClient.Connect();
        }
    }

    // 断开连接
    private void Disconnect()
    {
        if (_sftpClient != null && _sftpClient.IsConnected)
        {
            _sftpClient.Disconnect();
        }
    }

    // 获取文件列表
    public string[] GetFileList(string path = "")
    {
        try
        {
            Connect();
            var files = _sftpClient.ListDirectory(path);
            var fileNames = new List<string>();

            foreach (var file in files)
            {
                if (!file.IsDirectory && !file.IsSymbolicLink)
                {
                    fileNames.Add(file.Name);
                }
            }

            return fileNames.ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine("获取文件列表时出错: {ex.Message}");
            return null;
        }
    }

    // 获取文件和目录的详细列表
    public string[] GetFilesDetailList(string path = "")
    {
        try
        {
            Connect();
            var files = _sftpClient.ListDirectory(path);
            var details = new List<string>();

            foreach (var file in files)
            {
                string fileType = file.IsDirectory ? "d" : "-";
                details.Add("{fileType} {file.LastWriteTime} {file.Name}");
            }

            return details.ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine("获取详细文件列表时出错: {ex.Message}");
            return null;
        }
    }

    // 上传文件
    public bool Upload(string localFilePath, string remoteFilePath)
    {
        try
        {
            Connect();

            // 确保目录存在
            string remoteDirectory = Path.GetDirectoryName(remoteFilePath);
            if (!DirectoryExists(remoteDirectory))
            {
                CreateDirectory(remoteDirectory);
            }

            using (var fileStream = new FileStream(localFilePath, FileMode.Open))
            {
                _sftpClient.UploadFile(fileStream, remoteFilePath);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("上传文件时出错: {ex.Message}");
            return false;
        }
    }

    // 上传字节数组
    public bool Upload(byte[] fileBytes, string remoteFilePath)
    {
        try
        {
            Connect();

            // 确保目录存在
            string remoteDirectory = Path.GetDirectoryName(remoteFilePath);
            if (!DirectoryExists(remoteDirectory))
            {
                CreateDirectory(remoteDirectory);
            }

            using (var memoryStream = new MemoryStream(fileBytes))
            {
                _sftpClient.UploadFile(memoryStream, remoteFilePath);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("上传字节数组时出错: {ex.Message}");
            return false;
        }
    }

    // 下载文件
    public bool Download(string remoteFilePath, string localFilePath, out string errorInfo)
    {
        try
        {
            if (File.Exists(localFilePath))
            {
                errorInfo = "本地文件 {localFilePath} 已存在，无法下载";
                return false;
            }

            Connect();

            using (var fileStream = new FileStream(localFilePath, FileMode.Create))
            {
                _sftpClient.DownloadFile(remoteFilePath, fileStream);
            }

            errorInfo = "";
            return true;
        }
        catch (Exception ex)
        {
            errorInfo = "下载文件时出错: {ex.Message}";
            return false;
        }
    }

    // 下载文件为字节数组
    public byte[] Download(string remoteFilePath)
    {
        try
        {
            Connect();

            using (var memoryStream = new MemoryStream())
            {
                _sftpClient.DownloadFile(remoteFilePath, memoryStream);
                return memoryStream.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("下载文件为字节数组时出错: {ex.Message}");
            return null;
        }
    }

    // 删除文件
    public void DeleteFile(string remoteFilePath)
    {
        try
        {
            Connect();
            _sftpClient.DeleteFile(remoteFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("删除文件时出错: {ex.Message}");
        }
    }

    // 创建目录
    public void CreateDirectory(string remoteDirectory)
    {
        try
        {
            Connect();

            // 如果目录已经存在，则不创建
            if (DirectoryExists(remoteDirectory))
                return;

            // 递归创建父目录
            string parentDirectory = Path.GetDirectoryName(remoteDirectory);
            if (!string.IsNullOrEmpty(parentDirectory) && !DirectoryExists(parentDirectory))
            {
                CreateDirectory(parentDirectory);
            }

            _sftpClient.CreateDirectory(remoteDirectory);
        }
        catch (Exception ex)
        {
            Console.WriteLine("创建目录时出错: {ex.Message}");
        }
    }

    // 删除目录
    public void DeleteDirectory(string remoteDirectory)
    {
        try
        {
            Connect();

            // 先删除目录中的所有文件和子目录
            foreach (var file in _sftpClient.ListDirectory(remoteDirectory))
            {
                if (file.IsDirectory && !file.IsSymbolicLink)
                {
                    if (file.Name != "." && file.Name != "..")
                    {
                        DeleteDirectory(file.FullName);
                    }
                }
                else
                {
                    _sftpClient.DeleteFile(file.FullName);
                }
            }

            // 然后删除空目录
            _sftpClient.DeleteDirectory(remoteDirectory);
        }
        catch (Exception ex)
        {
            Console.WriteLine("删除目录时出错: {ex.Message}");
        }
    }

    // 获取文件大小
    public long GetFileSize(string remoteFilePath)
    {
        try
        {
            Connect();
            return _sftpClient.GetAttributes(remoteFilePath).Size;
        }
        catch (Exception ex)
        {
            Console.WriteLine("获取文件大小时出错: {ex.Message}");
            return -1;
        }
    }

    // 重命名文件或目录
    public void Rename(string oldPath, string newPath)
    {
        try
        {
            Connect();
            _sftpClient.RenameFile(oldPath, newPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("重命名时出错: {ex.Message}");
        }
    }

    // 检查目录是否存在
    public bool DirectoryExists(string remoteDirectory)
    {
        try
        {
            Connect();

            if (string.IsNullOrEmpty(remoteDirectory))
                return true;

            var directory = _sftpClient.Get(remoteDirectory);
            return directory.IsDirectory;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // 复制文件到另一个目录
    public bool CopyFileToAnotherDirectory(string sourceFilePath, string destinationDirectory, string newFileName)
    {
        try
        {
            Connect();

            // 确保目标目录存在
            if (!DirectoryExists(destinationDirectory))
            {
                CreateDirectory(destinationDirectory);
            }

            // 构建目标文件路径
            string destinationFilePath = Path.Combine(destinationDirectory, newFileName);

            // 下载源文件到内存并上传到目标位置
            byte[] fileBytes = Download(sourceFilePath);
            if (fileBytes != null)
            {
                return Upload(fileBytes, destinationFilePath);
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("复制文件时出错: {ex.Message}");
            return false;
        }
    }

    // 实现IDisposable接口
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // 释放托管资源
                if (_sftpClient != null)
                {
                    _sftpClient.Dispose();
                    _sftpClient = null;
                }
            }

            _disposed = true;
        }
    }

    ~SftpHelper()
    {
        Dispose(false);
    }
}