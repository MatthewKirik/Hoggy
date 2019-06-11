﻿using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly string _standartPath;
        private readonly string _standartExtension;
        private FileStream _stream;

        public FileRepository()
        {
            _standartPath = @"UserFiles\";
            _standartExtension = ".hoggyData";
            if (!Directory.Exists(_standartPath))
                Directory.CreateDirectory(_standartPath);
        }

        public string AddFile(byte[] file)
        {
            string key = KeyGenerator();
            string path = _standartPath + key + _standartExtension;
            try
            {
                using (_stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    _stream.Write(file, 0, file.Length);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return key;
        }

        public byte[] GetFile(string key)
        {
            string path = _standartPath + key + _standartExtension;
            try
            {
                using (_stream = File.OpenRead(path))
                {
                    byte[] file = new byte[_stream.Length];
                    _stream.Read(file, 0, file.Length);
                    return file;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        private string KeyGenerator()
        {
            return DateTime.Now.Ticks.ToString() + "_" + Guid.NewGuid();
        }
    }
}
