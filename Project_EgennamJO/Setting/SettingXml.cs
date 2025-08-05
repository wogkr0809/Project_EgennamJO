﻿using Common.Util.Helpers;
using Project_EgennamJO.Grab;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_EgennamJO.Setting
{
    public class SettingXml
    {
        private const string SETTING_DIR = "Setup";
        private const string SETTING_FILE_NAME = @"Setup\Setting.xml";

        #region Singleton Instance
        private static SettingXml _setting;

        public static SettingXml Inst
        {
            get
            {
                if (_setting is null)
                    Load();

                return _setting;
            }
        }// 환경설정을 계속 쓸수잇게 설정하는방법.
        #endregion

        //환경설정 로딩
        public static void Load()
        {
            if (_setting != null)
                return;

            //환경설정 경로 생성
            string settingFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, SETTING_FILE_NAME);
            if (File.Exists(settingFilePath) == true)
            {
                //환경설정 파일이 있다면 XmlHelper를 이용해 로딩
                _setting = XmlHelper.LoadXml<SettingXml>(settingFilePath);
            }

            if (_setting is null)
            {
                //환경설정 파일이 없다면 새로 생성
                _setting = CreateDefaultInstance();
            }
        }

        //환경설정 저장
        public static void Save()
        {
            string settingFilePath = Path.Combine(Environment.CurrentDirectory, SETTING_FILE_NAME);
            if (!File.Exists(settingFilePath))
            {
                //Setup 폴더가 없다면 생성
                string setupDir = Path.Combine(Environment.CurrentDirectory, SETTING_DIR);

                if (!Directory.Exists(setupDir))
                    Directory.CreateDirectory(setupDir);

                //Setting.xml 파일이 없다면 생성
                FileStream fs = File.Create(settingFilePath);
                fs.Close();
            }

            //XmlHelper를 이용해 Xml로 환경설정 정보 저장
            XmlHelper.SaveXml(settingFilePath, Inst);
        }

        //최초 환경설정 파일 생성
        private static SettingXml CreateDefaultInstance()
        {
            SettingXml setting = new SettingXml();
            setting.ModelDir = @"d:\model";
            return setting;
        }

        public SettingXml() { }

        public string MachineName { get; set; } = "Jidam";

        public string ModelDir { get; set; } = "";
        public string ImageDir { get; set; } = "";

        public CameraType CamType { get; set; } = CameraType.WebCam;

    }
}

