using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
public class HotfixData
{
    /// <summary>
    /// 景区名称
    /// </summary>
    [SerializeField]
    public string name;
    /// <summary>
    /// 景区缩略图本地路径
    /// </summary>
    public string imageBackgroundPathAndName;

    /// <summary>
    /// 景区介绍
    /// </summary>
    [SerializeField]
    public string content;
    [SerializeField]
    public string province;
    [SerializeField]
    public string city;
    /// <summary>
    /// 景区地址
    /// </summary>
    [SerializeField]
    public string address;
    /// <summary>
    /// 景区缩略图路径
    /// </summary>
    [SerializeField]
    public string texturePath;
    /// <summary>
    /// 景区缩略图
    /// </summary>
    public Texture2D texture;
    /// <summary>
    /// 演出组
    /// </summary>
    [SerializeField]
    public ShowGroup[] showGroup;

    [SerializeField]
    public ModelGroup[] modelGroup;

    /// <summary>
    /// 演出组
    /// </summary>
    [Serializable]
    public class ShowGroup
    {
        /// <summary>
        /// 演出组名称
        /// </summary>
        [SerializeField]
        public string showGroupName;

        /// <summary>
        /// 演出组缩略图本地路径
        /// </summary>
        public string imageBackgroundPathAndName;

        /// <summary>
        /// 演出序号
        /// </summary>
        [SerializeField]
        public int showGroupId;

        /// <summary>
        /// 演出缩略图路径
        /// </summary>
        [SerializeField]
        public string showGroupTexturePath;

        /// <summary>
        /// 演出缩略图
        /// </summary>
        public Texture2D showGroupTexture;

        /// <summary>
        /// 演出组介绍
        /// </summary>
        [SerializeField]
        public string showGroupContent;

        /// <summary>
        /// 单个演出
        /// </summary>
        [SerializeField]
        public Show[] show;

        [SerializeField]
        public ModelGroup[] modelGroup;

    }
    [Serializable]
    public class Show
    {
        [SerializeField]
        public string showName;
        /// <summary>
        /// 演出缩略图本地路径
        /// </summary>
        public string imageBackgroundPathAndName;
        [SerializeField]
        public int showID;
        [SerializeField]
        public string showContent;
        [SerializeField]
        public string showTexturePath;
        [SerializeField]
        public Texture showTexture;

        /// <summary>
        /// 模型组
        /// </summary>
        [SerializeField]
        public ModelGroup[] modelGroup;

        /// <summary>
        /// 图片组
        /// </summary>
        [SerializeField]
        public TextureGroup[] imageGroup;

        /// <summary>
        /// 视频组
        /// </summary>
        [SerializeField]
        public VideoGroup[] videoGroup;

        /// <summary>
        /// 音频组
        /// </summary>
        [SerializeField]
        public AudioGroup[] audioGroup;

        /// <summary>
        /// 文字组
        /// </summary>
        [SerializeField]
        public TextGroup[] textGroup;
    }

    /// <summary>
    /// 模型相关
    /// </summary>
    [Serializable]
    public class ModelGroup
    {
        /// <summary>
        /// 模型名称
        /// </summary>
        [SerializeField]
        public string name;
        /// <summary>
        /// 模型音频本地路径
        /// </summary>
        public string audioPathAndName;
        /// <summary>
        /// 模型序号
        /// </summary>
        [SerializeField]
        public int id;

        [SerializeField]
        public double positionX;
        [SerializeField]
        public double positionY;
        [SerializeField]
        public double positionZ;

        [SerializeField]
        public double rotateX;
        [SerializeField]
        public double rotateY;
        [SerializeField]
        public double rotateZ;

        [SerializeField]
        public double scaleX;
        [SerializeField]
        public double scaleY;
        [SerializeField]
        public double scaleZ;

        [SerializeField]
        public double moveX;
        [SerializeField]
        public double moveY;
        [SerializeField]
        public double moveZ;
        [SerializeField]
        public double moveTime;

        [SerializeField]
        public string audioPath;
        public AudioClip audio;
        /// <summary>
        /// 模型介绍
        /// </summary>
        [SerializeField]
        public string content;
    }


    /// <summary>
    /// 图片组
    /// </summary>
    [Serializable]
    public class TextureGroup
    {
        [SerializeField]
        public string name;
        /// <summary>
        /// 图片组背景本地路径
        /// </summary>
        public string imageBackgroundPathAndName;
        /// <summary>
        /// 图片组内容本地路径
        /// </summary>
        public string[] imageContentPathAndName;
        [SerializeField]
        public int typeID;
        [SerializeField]
        public string content;
        [SerializeField]
        public string backgroundPath;
        [SerializeField]
        public Texture backgroundTexture;
        [SerializeField]
        public string[] texturePaths;
        [SerializeField]
        public Texture[] textures;

        [SerializeField]
        public double positionX;
        [SerializeField]
        public double positionY;
        [SerializeField]
        public double positionZ;

        [SerializeField]
        public double rotateX;
        [SerializeField]
        public double rotateY;
        [SerializeField]
        public double rotateZ;

        [SerializeField]
        public double scaleX;
        [SerializeField]
        public double scaleY;
        [SerializeField]
        public double scaleZ;
    }

    /// <summary>
    /// 视频组
    /// </summary>
    [Serializable]
    public class VideoGroup
    {
        [SerializeField]
        public string name;
        /// <summary>
        /// 视频组背景本地路径
        /// </summary>
        public string imageBackgroundPathAndName;
        /// <summary>
        /// 视频组背景本地路径
        /// </summary>
        public string videoPathAndName;
        [SerializeField]
        public int typeID;
        [SerializeField]
        public string content;
        [SerializeField]
        public string backgroundPath;
        [SerializeField]
        public Texture backgroundTexture;
        [SerializeField]
        public string videoPath;
        [SerializeField]
        public VideoClip video;

        [SerializeField]
        public double positionX;
        [SerializeField]
        public double positionY;
        [SerializeField]
        public double positionZ;

        [SerializeField]
        public double rotateX;
        [SerializeField]
        public double rotateY;
        [SerializeField]
        public double rotateZ;

        [SerializeField]
        public double scaleX;
        [SerializeField]
        public double scaleY;
        [SerializeField]
        public double scaleZ;
    }

    /// <summary>
    /// 音频组
    /// </summary>
    [Serializable]
    public class AudioGroup
    {
        [SerializeField]
        public string name;
        /// <summary>
        /// 音频组本地路径
        /// </summary>
        public string audioPathAndName;
        [SerializeField]
        public string audioPath;
        [SerializeField]
        public AudioClip audio;
    }

    /// <summary>
    /// 文字组
    /// </summary>
    [Serializable]
    public class TextGroup
    {
        [SerializeField]
        public string name;
        /// <summary>
        /// 文字组背景本地路径
        /// </summary>
        public string imageBackgroundPathAndName;
        [SerializeField]
        public int typeID;
        [SerializeField]
        public string content;
        [SerializeField]
        public int size;
        [SerializeField]
        public string backgroundPath;
        [SerializeField]
        public Texture backgroundText;

        [SerializeField]
        public double positionX;
        [SerializeField]
        public double positionY;
        [SerializeField]
        public double positionZ;

        [SerializeField]
        public double rotateX;
        [SerializeField]
        public double rotateY;
        [SerializeField]
        public double rotateZ;

        [SerializeField]
        public double scaleX;
        [SerializeField]
        public double scaleY;
        [SerializeField]
        public double scaleZ;
    }
}