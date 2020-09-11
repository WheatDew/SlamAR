using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HotfixJsonbase : MonoSingleton<HotfixJsonbase>
{
    int videoCount = -1;
    public HotfixData hotfixJson;
    const string preName = "http://117.81.233.142:8081/MRGlass_Shadows/";
    //public Text test;

    IEnumerator Start()
    {
        yield return new WaitUntil(IsNeedDownloadJsonAndAssets.GetInstance.HasGetMsgFromServer);
        yield return new WaitUntil(DownloadAllPrepositionAssets.GetInstance.HasDownVuforiaData);
        //获取网络
        if (IsNeedDownloadJsonAndAssets.GetInstance.IsNeedDownload())//下载网络json并下载资源并获取各个对象名字
        {
            #region 下载json
            Debug.Log("开始下载json");
            string saveJsonDatPath = PathSet.GetJsonSavePath();
            string readJsonDatPath = PathSet.GetJsonReadPath();
            NetworkTools.GetInstance.Get(PathSet.GetJsonDownPath(), t =>
            {
                try
                {
                    if (!Directory.Exists(Application.persistentDataPath))
                    {
                        Directory.CreateDirectory(Application.persistentDataPath);
                    }
                    FileStream fs = new FileStream(saveJsonDatPath, FileMode.Create, FileAccess.ReadWrite);
                    fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                    fs.Close();

                    hotfixJson = JsonMapper.ToObject<HotfixData>(t.downloadHandler.text);
                    DownloadAllPrepositionAssets.GetInstance.hasDownJsonData = true;
                    Debug.Log("json下载完毕");
                    t = null;
                }
                catch (Exception ex) { Debug.LogError(ex); }
            }, () => { });

            yield return new WaitUntil(DownloadAllPrepositionAssets.GetInstance.HasDownJsonData);
            #endregion

            if (!string.IsNullOrEmpty(hotfixJson.texturePath))
            {
                #region 下载图片

                #region 加载景区缩略图
                Debug.Log("开始下载景区缩略图");
                NetworkTools.GetInstance.Get(hotfixJson.texturePath, t =>
                {
                    var temp = Regex.Split(hotfixJson.texturePath, preName, RegexOptions.IgnoreCase);
                    var tempName = temp[temp.Length - 1].Split('/');
                    var name = tempName[tempName.Length - 1];//得到文件名:ScenicArea.jpg
                    var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                    hotfixJson.imageBackgroundPathAndName = path + name;
                    try
                    {
                        if (!Directory.Exists(PathSet.GetImageSavePath(path)))
                        {
                            Directory.CreateDirectory(PathSet.GetImageSavePath(path));
                        }
                        FileStream fs = new FileStream(PathSet.GetImageSavePath(path + name), FileMode.Create, FileAccess.ReadWrite);
                        fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                        fs.Close();
                        if (!string.IsNullOrEmpty(path + name))
                            NetworkTools.GetInstance.DownloadTexture(PathSet.GetImageReadPath(path + name), e =>
                        {
                            hotfixJson.texture = e;
                        }, null);
                        Debug.Log("景区缩略图下载完毕");
                    }
                    catch (Exception ex) { Debug.LogError(ex); }
                }, () => { });
                yield return new WaitUntil(() => { return hotfixJson.texture; });
                hotfixJson.texture = null;
                MemFree();
                #endregion

                #region 加载演出组缩略图
                for (int i = 0; i < hotfixJson.showGroup.Length;)
                {
                    if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].showGroupTexturePath))
                    {
                        Debug.Log("开始下载演出组缩略图:   " + i);
                        var temp = Regex.Split(hotfixJson.showGroup[i].showGroupTexturePath, preName, RegexOptions.IgnoreCase);
                        var tempName = temp[temp.Length - 1].Split('/');
                        var name = tempName[tempName.Length - 1];//得到文件名:ScenicArea.jpg
                        var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                        hotfixJson.showGroup[i].imageBackgroundPathAndName = path + name;
                        NetworkTools.GetInstance.Get(hotfixJson.showGroup[i].showGroupTexturePath, t =>
                        {
                            try
                            {
                                if (!Directory.Exists(PathSet.GetImageSavePath(path)))
                                {
                                    Directory.CreateDirectory(PathSet.GetImageSavePath(path));
                                }
                                FileStream fs = new FileStream(PathSet.GetImageSavePath(path + name), FileMode.Create, FileAccess.ReadWrite);
                                fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                                fs.Close();
                                if (!string.IsNullOrEmpty(path + name))
                                    NetworkTools.GetInstance.DownloadTexture(PathSet.GetImageReadPath(path + name), e =>
                                {
                                    hotfixJson.showGroup[i].showGroupTexture = e;
                                }, null);
                                if (i == hotfixJson.showGroup.Length - 1)
                                {
                                    Debug.Log("演出组缩略图下载完毕");
                                }
                            }
                            catch (Exception ex) { Debug.LogError(ex); }
                        }, () => { });
                    }
                    else
                    {
                        i++;
                        continue;
                    }
                    yield return new WaitUntil(() => { return hotfixJson.showGroup[i].showGroupTexture; });
                    hotfixJson.showGroup[i].showGroupTexture = null;
                    MemFree();
                    i++;
                }
                #endregion

                #region 加载演出缩略图
                for (int i = 0; i < hotfixJson.showGroup.Length; i++)
                {
                    for (int j = 0; j < hotfixJson.showGroup[i].show.Length;)
                    {
                        if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].showTexturePath))
                        {
                            Debug.Log("开始下载演出缩略图:  " + i + "   " + j);
                            var temp = Regex.Split(hotfixJson.showGroup[i].show[j].showTexturePath, preName, RegexOptions.IgnoreCase);
                            var tempName = temp[temp.Length - 1].Split('/');
                            var name = tempName[tempName.Length - 1];//得到文件名:ScenicArea.jpg
                            var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                            hotfixJson.showGroup[i].show[j].imageBackgroundPathAndName = path + name;
                            NetworkTools.GetInstance.Get(hotfixJson.showGroup[i].show[j].showTexturePath, t =>
                            {
                                try
                                {
                                    if (!Directory.Exists(PathSet.GetImageSavePath(path)))
                                    {
                                        Directory.CreateDirectory(PathSet.GetImageSavePath(path));
                                    }
                                    FileStream fs = new FileStream(PathSet.GetImageSavePath(path + name), FileMode.Create, FileAccess.ReadWrite);
                                    fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                                    fs.Close();
                                    if (!string.IsNullOrEmpty(path + name))
                                        NetworkTools.GetInstance.DownloadTexture(PathSet.GetImageReadPath(path + name), e =>
                                    {
                                        hotfixJson.showGroup[i].show[j].showTexture = e;
                                    }, null);
                                    if (i == hotfixJson.showGroup.Length - 1 && j == hotfixJson.showGroup[i].show.Length - 1)
                                    {
                                        Debug.Log("演出组缩略下载完毕");
                                    }
                                }
                                catch (Exception ex) { Debug.LogError(ex); }
                            }, () => { });

                        }
                        else
                        {
                            j++;
                            continue;
                        }
                        yield return new WaitUntil(() => { return hotfixJson.showGroup[i].show[j].showTexture; });
                        hotfixJson.showGroup[i].show[j].showTexture = null;
                        MemFree();
                        j++;
                    }
                }
                #endregion

                #region 加载演出图片组背景
                for (int i = 0; i < hotfixJson.showGroup.Length; i++)
                {
                    if (hotfixJson.showGroup[i].show!=null)
                        for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                        {
                            if (hotfixJson.showGroup[i].show[j].imageGroup!=null)
                                for (int k = 0; k < hotfixJson.showGroup[i].show[j].imageGroup.Length;)
                                {
                                    Debug.Log("开始下载背景图片组:  " + i + "   " + j + "   " + k);
                                    if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].imageGroup[k].backgroundPath))
                                    {
                                        var temp = Regex.Split(hotfixJson.showGroup[i].show[j].imageGroup[k].backgroundPath, preName, RegexOptions.IgnoreCase);
                                        var tempName = temp[temp.Length - 1].Split('/');
                                        var name = tempName[tempName.Length - 1];//得到文件名:ScenicArea.jpg
                                        var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                                        hotfixJson.showGroup[i].show[j].imageGroup[k].imageBackgroundPathAndName = path + name;
                                        NetworkTools.GetInstance.Get(hotfixJson.showGroup[i].show[j].imageGroup[k].backgroundPath, t =>
                                        {
                                            try
                                            {
                                                if (!Directory.Exists(PathSet.GetImageSavePath(path)))
                                                {
                                                    Directory.CreateDirectory(PathSet.GetImageSavePath(path));
                                                }
                                                FileStream fs = new FileStream(PathSet.GetImageSavePath(path + name), FileMode.Create, FileAccess.ReadWrite);
                                                fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                                                fs.Close();
                                                if (!string.IsNullOrEmpty(path + name))
                                                    NetworkTools.GetInstance.DownloadTexture(PathSet.GetImageReadPath(path + name), e =>
                                                {
                                                    hotfixJson.showGroup[i].show[j].imageGroup[k].backgroundTexture = e;
                                                }, null);
                                                if (i == hotfixJson.showGroup.Length - 1 && j == hotfixJson.showGroup[i].show.Length - 1 && k == hotfixJson.showGroup[i].show[j].imageGroup.Length - 1)
                                                {
                                                    Debug.Log("图片组背景下载完毕");
                                                }
                                            }
                                            catch (Exception ex) { Debug.LogError(ex); }
                                        }, () => { });
                                        yield return new WaitUntil(() => { return hotfixJson.showGroup[i].show[j].imageGroup[k].backgroundTexture; });
                                        hotfixJson.showGroup[i].show[j].imageGroup[k].backgroundTexture = null;
                                        MemFree();
                                        k++;
                                    }
                                    else
                                    {
                                        k++;
                                        continue;
                                    }
                                }
                        }
                }
                #endregion

                #region 加载演出图片组内容
                for (int i = 0; i < hotfixJson.showGroup.Length; i++)
                {
                    for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                    {
                        if (hotfixJson.showGroup[i].show[j].imageGroup != null)
                            for (int k = 0; k < hotfixJson.showGroup[i].show[j].imageGroup.Length; k++)
                        {
                            hotfixJson.showGroup[i].show[j].imageGroup[k].textures = new Texture[hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths.Length];
                            hotfixJson.showGroup[i].show[j].imageGroup[k].imageContentPathAndName = new string[hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths.Length];
                            for (int x = 0; x < hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths.Length;)
                            {
                                Debug.Log("开始下载图片组内容图片:  " + i + "   " + j + "   " + k + "   " + x);
                                if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths[x]))
                                {
                                    var temp = Regex.Split(hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths[x], preName, RegexOptions.IgnoreCase);
                                    var tempName = temp[temp.Length - 1].Split('/');
                                    var name = tempName[tempName.Length - 1];//得到文件名:ScenicArea.jpg
                                    var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                                    hotfixJson.showGroup[i].show[j].imageGroup[k].imageContentPathAndName[x] = path + name;
                                    NetworkTools.GetInstance.Get(hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths[x], t =>
                                    {
                                        try
                                        {
                                            if (!Directory.Exists(PathSet.GetImageSavePath(path)))
                                            {
                                                Directory.CreateDirectory(PathSet.GetImageSavePath(path));
                                            }
                                            FileStream fs = new FileStream(PathSet.GetImageSavePath(path + name), FileMode.Create, FileAccess.ReadWrite);
                                            fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                                            fs.Close();
                                            if (!string.IsNullOrEmpty(path + name))
                                                NetworkTools.GetInstance.DownloadTexture(PathSet.GetImageReadPath(path + name), e =>
                                            {
                                                hotfixJson.showGroup[i].show[j].imageGroup[k].textures[x] = e;
                                            }, null);
                                            if (i == hotfixJson.showGroup.Length - 1 && j == hotfixJson.showGroup[i].show.Length - 1 && k == hotfixJson.showGroup[i].show[j].imageGroup.Length - 1 && x == hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths.Length - 1)
                                            {
                                                Debug.Log("图片组内容图片下载完毕");
                                            }
                                        }
                                        catch (Exception ex) { Debug.LogError(ex); }
                                    }, () => { });
                                    yield return new WaitUntil(() => { return hotfixJson.showGroup[i].show[j].imageGroup[k].textures[x]; });
                                    hotfixJson.showGroup[i].show[j].imageGroup[k].textures[x] = null;
                                    MemFree();
                                    x++;
                                }
                                else
                                {
                                    x++;
                                    continue;
                                }
                            }
                        }
                    }
                }
                #endregion

                #region 加载演出文字组背景
                for (int i = 0; i < hotfixJson.showGroup.Length; i++)
                {
                    for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                    {
                        for (int k = 0; k < hotfixJson.showGroup[i].show[j].textGroup.Length;)
                        {
                            Debug.Log("开始下载文字组背景:  " + i + "   " + j + "   " + k);
                            if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].textGroup[k].backgroundPath))
                            {
                                var temp = Regex.Split(hotfixJson.showGroup[i].show[j].textGroup[k].backgroundPath, preName, RegexOptions.IgnoreCase);
                                var tempName = temp[temp.Length - 1].Split('/');
                                var name = tempName[tempName.Length - 1];//得到文件名:ScenicArea.jpg
                                var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                                hotfixJson.showGroup[i].show[j].textGroup[k].imageBackgroundPathAndName = path + name;
                                NetworkTools.GetInstance.Get(hotfixJson.showGroup[i].show[j].textGroup[k].backgroundPath, t =>
                                {
                                    try
                                    {
                                        if (!Directory.Exists(PathSet.GetImageSavePath(path)))
                                        {
                                            Directory.CreateDirectory(PathSet.GetImageSavePath(path));
                                        }
                                        FileStream fs = new FileStream(PathSet.GetImageSavePath(path + name), FileMode.Create, FileAccess.ReadWrite);
                                        fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                                        fs.Close();
                                        if (!string.IsNullOrEmpty(path + name))
                                            NetworkTools.GetInstance.DownloadTexture(PathSet.GetImageReadPath(path + name), e =>
                                        {
                                            hotfixJson.showGroup[i].show[j].textGroup[k].backgroundText = e;
                                        }, null);
                                        if (i == hotfixJson.showGroup.Length - 1 && j == hotfixJson.showGroup[i].show.Length - 1 && k == hotfixJson.showGroup[i].show[j].textGroup.Length - 1)
                                        {
                                            Debug.Log("文字组背景下载完毕");
                                        }
                                    }
                                    catch (Exception ex) { Debug.LogError(ex); }
                                }, () => { });
                                yield return new WaitUntil(() => { return hotfixJson.showGroup[i].show[j].textGroup[k].backgroundText; });
                                hotfixJson.showGroup[i].show[j].textGroup[k].backgroundText = null;
                                MemFree();
                                k++;
                            }
                            else
                            {
                                k++;
                                continue;
                            }
                        }
                    }
                }
                #endregion

                #region 加载演出视频组背景
                for (int i = 0; i < hotfixJson.showGroup.Length; i++)
                {
                    for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                    {
                        if (hotfixJson.showGroup[i].show[j].videoGroup != null)
                            for (int k = 0; k < hotfixJson.showGroup[i].show[j].videoGroup.Length;)
                        {
                            Debug.Log("开始下载视频组背景:  " + i + "   " + j + "   " + k);
                            if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].videoGroup[k].backgroundPath))
                            {
                                var temp = Regex.Split(hotfixJson.showGroup[i].show[j].videoGroup[k].backgroundPath, preName, RegexOptions.IgnoreCase);
                                var tempName = temp[temp.Length - 1].Split('/');
                                var name = tempName[tempName.Length - 1];//得到文件名:ScenicArea.jpg
                                var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                                hotfixJson.showGroup[i].show[j].videoGroup[k].imageBackgroundPathAndName = path + name;
                                NetworkTools.GetInstance.Get(hotfixJson.showGroup[i].show[j].videoGroup[k].backgroundPath, t =>
                                {
                                    try
                                    {
                                        if (!Directory.Exists(PathSet.GetImageSavePath(path)))
                                        {
                                            Directory.CreateDirectory(PathSet.GetImageSavePath(path));
                                        }
                                        FileStream fs = new FileStream(PathSet.GetImageSavePath(path + name), FileMode.Create, FileAccess.ReadWrite);
                                        fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                                        fs.Close();
                                        if (!string.IsNullOrEmpty(path + name))
                                            NetworkTools.GetInstance.DownloadTexture(PathSet.GetImageReadPath(path + name), e =>
                                        {
                                            hotfixJson.showGroup[i].show[j].videoGroup[k].backgroundTexture = e;
                                        }, null);
                                        if (i == hotfixJson.showGroup.Length - 1 && j == hotfixJson.showGroup[i].show.Length - 1 && k == hotfixJson.showGroup[i].show[j].videoGroup.Length - 1)
                                        {
                                            Debug.Log("视频组背景下载完毕");
                                        }
                                    }
                                    catch (Exception ex) { Debug.LogError(ex); }
                                }, () => { });
                                yield return new WaitUntil(() => { return hotfixJson.showGroup[i].show[j].videoGroup[k].backgroundTexture; });
                                hotfixJson.showGroup[i].show[j].videoGroup[k].backgroundTexture = null;
                                MemFree();
                                k++;
                            }
                            else
                            {
                                k++;
                                continue;
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region  下载音频

                #region 模型音频
                for (int i = 0; i < hotfixJson.showGroup.Length; i++)
                {
                    for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                    {
                        if (hotfixJson.showGroup[i].show[j].modelGroup != null)
                            for (int k = 0; k < hotfixJson.showGroup[i].show[j].modelGroup.Length;)
                        {
                            Debug.Log("开始下载模型组音频:  " + i + "   " + j + "   " + k);
                            if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].modelGroup[k].audioPath))
                            {
                                var temp = Regex.Split(hotfixJson.showGroup[i].show[j].modelGroup[k].audioPath, preName, RegexOptions.IgnoreCase);
                                var tempName = temp[temp.Length - 1].Split('/');
                                var name = tempName[tempName.Length - 1];//得到文件名:ScenicArea.jpg
                                var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                                hotfixJson.showGroup[i].show[j].modelGroup[k].audioPathAndName = path + name;
                                NetworkTools.GetInstance.Get(hotfixJson.showGroup[i].show[j].modelGroup[k].audioPath, t =>
                                {
                                    try
                                    {
                                        if (!Directory.Exists(PathSet.GetImageSavePath(path)))
                                        {
                                            Directory.CreateDirectory(PathSet.GetImageSavePath(path));
                                        }
                                        FileStream fs = new FileStream(PathSet.GetImageSavePath(path + name), FileMode.Create, FileAccess.ReadWrite);
                                        fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                                        fs.Close();
                                        if (!string.IsNullOrEmpty(path + name))
                                            NetworkTools.GetInstance.DownloadAudioClip(PathSet.GetImageReadPath(path + name), e =>
                                        {
                                            hotfixJson.showGroup[i].show[j].modelGroup[k].audio = e;
                                        }, null);
                                        if (i == hotfixJson.showGroup.Length - 1 && j == hotfixJson.showGroup[i].show.Length - 1 && k == hotfixJson.showGroup[i].show[j].modelGroup.Length - 1)
                                        {
                                            Debug.Log("模型组音频下载完毕");
                                        }
                                    }
                                    catch (Exception ex) { Debug.LogError(ex); }
                                }, () => { });
                                yield return new WaitUntil(() => { return hotfixJson.showGroup[i].show[j].modelGroup[k].audio; });
                                hotfixJson.showGroup[i].show[j].modelGroup[k].audio = null;
                                MemFree();
                                k++;
                            }
                            else
                            {
                                k++;
                                continue;
                            }
                        }
                    }
                }
                #endregion

                #region 背景音频组
                for (int i = 0; i < hotfixJson.showGroup.Length; i++)
                {
                    for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                    {
                        if (hotfixJson.showGroup[i].show[j].audioGroup != null)
                            for (int k = 0; k < hotfixJson.showGroup[i].show[j].audioGroup.Length;)
                        {
                            Debug.Log("开始下载音频组音频:  " + i + "   " + j + "   " + k);
                            if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].audioGroup[k].audioPath))
                            {
                                var temp = Regex.Split(hotfixJson.showGroup[i].show[j].audioGroup[k].audioPath, preName, RegexOptions.IgnoreCase);

                                var tempName = temp[temp.Length - 1].Split('/');
                                var name = tempName[tempName.Length - 1];//得到文件名:ScenicArea.jpg
                                var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                                hotfixJson.showGroup[i].show[j].audioGroup[k].audioPathAndName = path + name;
                                NetworkTools.GetInstance.Get(hotfixJson.showGroup[i].show[j].audioGroup[k].audioPath, t =>
                                {
                                    try
                                    {
                                        if (!Directory.Exists(PathSet.GetImageSavePath(path)))
                                        {
                                            Directory.CreateDirectory(PathSet.GetImageSavePath(path));
                                        }
                                        FileStream fs = new FileStream(PathSet.GetImageSavePath(path + name), FileMode.Create, FileAccess.ReadWrite);
                                        fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                                        fs.Close();
                                        if (!string.IsNullOrEmpty(path + name))
                                            NetworkTools.GetInstance.DownloadAudioClip(PathSet.GetImageReadPath(path + name), e =>
                                        {
                                            hotfixJson.showGroup[i].show[j].audioGroup[k].audio = e;
                                        }, null);
                                        if (i == hotfixJson.showGroup.Length - 1 && j == hotfixJson.showGroup[i].show.Length - 1 && k == hotfixJson.showGroup[i].show[j].audioGroup.Length - 1)
                                        {
                                            Debug.Log("音频组音频下载完毕");
                                        }
                                    }
                                    catch (Exception ex) { Debug.LogError(ex); }
                                }, () => { });
                                yield return new WaitUntil(() => { return hotfixJson.showGroup[i].show[j].audioGroup[k].audio; });
                                hotfixJson.showGroup[i].show[j].audioGroup[k].audio = null;
                                MemFree();
                                k++;
                            }
                            else
                            {
                                k++;
                                continue;
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 下载视频

                for (int i = 0; i < hotfixJson.showGroup.Length; i++)
                {
                    for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                    {
                        if (hotfixJson.showGroup[i].show[j].videoGroup != null)
                            for (int k = 0; k < hotfixJson.showGroup[i].show[j].videoGroup.Length;)
                        {
                            if (k == 0)
                                videoCount = -1;
                            var tempCount = hotfixJson.showGroup[i].show[j].videoGroup.Length;
                            Debug.Log("开始下载视频组视频:  " + i + "   " + j + "   " + k);
                            if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].videoGroup[k].videoPath))
                            {
                                var temp = Regex.Split(hotfixJson.showGroup[i].show[j].videoGroup[k].videoPath, preName, RegexOptions.IgnoreCase);
                                var tempName = temp[temp.Length - 1].Split('/');
                                var name = tempName[tempName.Length - 1];//得到文件名:ScenicArea.jpg
                                var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                                hotfixJson.showGroup[i].show[j].videoGroup[k].videoPathAndName = path + name;
                                NetworkTools.GetInstance.Get(hotfixJson.showGroup[i].show[j].videoGroup[k].videoPath, t =>
                                {
                                    try
                                    {
                                        if (!Directory.Exists(PathSet.GetVideoSavePath(path)))
                                        {
                                            Directory.CreateDirectory(PathSet.GetVideoSavePath(path));
                                        }
                                        FileStream fs = new FileStream(PathSet.GetVideoSavePath(path + name), FileMode.Create, FileAccess.ReadWrite);
                                        fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                                        fs.Close();
                                        videoCount++;
                                        //NetworkTools.GetInstance.DownloadAudioClip(PathSet.GetImageReadPath(path + name), e =>
                                        //{
                                        //    hotfixJson.showGroup[i].show[j].videoGroup[k].audio = e;
                                        //}, null);
                                        if (i == hotfixJson.showGroup.Length - 1 && j == hotfixJson.showGroup[i].show.Length - 1 && k == hotfixJson.showGroup[i].show[j].videoGroup.Length - 1)
                                        {
                                            Debug.Log("视频组视频下载完毕");
                                        }
                                    }
                                    catch (Exception ex) { Debug.LogError(ex); }
                                }, () => { });
                                yield return new WaitUntil(() => { return k == videoCount; });
                                MemFree();
                                k++;
                            }
                            else
                            {
                                k++;
                                continue;
                            }
                        }
                    }
                }

                #endregion

            }
        }
        //获取本地
        else //下载本地json并获取各个对象名字
        {
            #region 加载json
            Debug.Log("加载本地json");
            //test.text = "加载本地json";
            string readJsonDatPath = PathSet.GetJsonReadPath();
            NetworkTools.GetInstance.DownloadText(readJsonDatPath, t =>
            {
                hotfixJson = JsonMapper.ToObject<HotfixData>(t);
                DownloadAllPrepositionAssets.GetInstance.hasDownJsonData = true;
                Debug.Log("json加载完毕");
                t = null;
            });
            yield return new WaitUntil(DownloadAllPrepositionAssets.GetInstance.HasDownJsonData);
            #endregion

            #region 获取名字
            if (!string.IsNullOrEmpty(hotfixJson.texturePath))//景区
            {
                var temp = Regex.Split(hotfixJson.texturePath, preName, RegexOptions.IgnoreCase);
                var tempName = temp[temp.Length - 1].Split('/');
                var name = tempName[tempName.Length - 1];
                var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                hotfixJson.imageBackgroundPathAndName = path + name;
            }

            for (int i = 0; i < hotfixJson.showGroup.Length; i++)//演出组
            {
                if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].showGroupTexturePath))
                {
                    var temp = Regex.Split(hotfixJson.showGroup[i].showGroupTexturePath, preName, RegexOptions.IgnoreCase);
                    var tempName = temp[temp.Length - 1].Split('/');
                    var name = tempName[tempName.Length - 1];
                    var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                    hotfixJson.showGroup[i].imageBackgroundPathAndName = path + name;

                }
            }

            for (int i = 0; i < hotfixJson.showGroup.Length; i++)//演出组
            {
                for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)//演出
                {
                    if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].showTexturePath))
                    {
                        var temp = Regex.Split(hotfixJson.showGroup[i].show[j].showTexturePath, preName, RegexOptions.IgnoreCase);
                        var tempName = temp[temp.Length - 1].Split('/');
                        var name = tempName[tempName.Length - 1];
                        var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                        hotfixJson.showGroup[i].show[j].imageBackgroundPathAndName = path + name;
                    }
                }
            }

            for (int i = 0; i < hotfixJson.showGroup.Length; i++)//演出组
            {
                for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)//演出
                {
                    if(hotfixJson.showGroup[i].show[j].imageGroup!=null)
                    for (int k = 0; k < hotfixJson.showGroup[i].show[j].imageGroup.Length; k++)//图片组
                    {
                        if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].imageGroup[k].backgroundPath))
                        {
                            var temp = Regex.Split(hotfixJson.showGroup[i].show[j].imageGroup[k].backgroundPath, preName, RegexOptions.IgnoreCase);
                            var tempName = temp[temp.Length - 1].Split('/');
                            var name = tempName[tempName.Length - 1];
                            var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                            hotfixJson.showGroup[i].show[j].imageGroup[k].imageBackgroundPathAndName = path + name;
                        }
                    }
                }
            }

            for (int i = 0; i < hotfixJson.showGroup.Length; i++)
            {
                for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                {
                    if (hotfixJson.showGroup[i].show[j].imageGroup != null)
                        for (int k = 0; k < hotfixJson.showGroup[i].show[j].imageGroup.Length; k++)
                    {
                        hotfixJson.showGroup[i].show[j].imageGroup[k].textures = new Texture[hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths.Length];
                        hotfixJson.showGroup[i].show[j].imageGroup[k].imageContentPathAndName = new string[hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths.Length];
                        for (int x = 0; x < hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths.Length; x++)
                        {
                            if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths[x]))
                            {
                                var temp = Regex.Split(hotfixJson.showGroup[i].show[j].imageGroup[k].texturePaths[x], preName, RegexOptions.IgnoreCase);
                                var tempName = temp[temp.Length - 1].Split('/');
                                var name = tempName[tempName.Length - 1];
                                var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                                hotfixJson.showGroup[i].show[j].imageGroup[k].imageContentPathAndName[x] = path + name;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < hotfixJson.showGroup.Length; i++)
            {
                for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                {
                    if (hotfixJson.showGroup[i].show[j].textGroup != null)
                        for (int k = 0; k < hotfixJson.showGroup[i].show[j].textGroup.Length; k++)
                    {
                        if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].textGroup[k].backgroundPath))
                        {
                            var temp = Regex.Split(hotfixJson.showGroup[i].show[j].textGroup[k].backgroundPath, preName, RegexOptions.IgnoreCase);
                            var tempName = temp[temp.Length - 1].Split('/');
                            var name = tempName[tempName.Length - 1];
                            var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                            hotfixJson.showGroup[i].show[j].textGroup[k].imageBackgroundPathAndName = path + name;
                        }
                    }
                }
            }

            for (int i = 0; i < hotfixJson.showGroup.Length; i++)
            {
                for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                {
                    if (hotfixJson.showGroup[i].show[j].videoGroup != null)
                        for (int k = 0; k < hotfixJson.showGroup[i].show[j].videoGroup.Length; k++)
                    {
                        if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].videoGroup[k].backgroundPath))
                        {
                            var temp = Regex.Split(hotfixJson.showGroup[i].show[j].videoGroup[k].backgroundPath, preName, RegexOptions.IgnoreCase);
                            var tempName = temp[temp.Length - 1].Split('/');
                            var name = tempName[tempName.Length - 1];
                            var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                            hotfixJson.showGroup[i].show[j].videoGroup[k].imageBackgroundPathAndName = path + name;
                        }
                    }
                }
            }

            for (int i = 0; i < hotfixJson.showGroup.Length; i++)
            {
                for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                {
                     if (hotfixJson.showGroup[i].show[j].modelGroup != null)
                    for (int k = 0; k < hotfixJson.showGroup[i].show[j].modelGroup.Length; k++)
                    {
                        if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].modelGroup[k].audioPath))
                        {
                            var temp = Regex.Split(hotfixJson.showGroup[i].show[j].modelGroup[k].audioPath, preName, RegexOptions.IgnoreCase);
                            var tempName = temp[temp.Length - 1].Split('/');
                            var name = tempName[tempName.Length - 1];
                            var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                            hotfixJson.showGroup[i].show[j].modelGroup[k].audioPathAndName = path + name;
                        }
                    }
                }
            }

            for (int i = 0; i < hotfixJson.showGroup.Length; i++)
            {
                for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                {
                    if (hotfixJson.showGroup[i].show[j].audioGroup != null)
                        for (int k = 0; k < hotfixJson.showGroup[i].show[j].audioGroup.Length; k++)
                    {
                        if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].audioGroup[k].audioPath))
                        {
                            var temp = Regex.Split(hotfixJson.showGroup[i].show[j].audioGroup[k].audioPath, preName, RegexOptions.IgnoreCase);

                            var tempName = temp[temp.Length - 1].Split('/');
                            var name = tempName[tempName.Length - 1];
                            var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                            hotfixJson.showGroup[i].show[j].audioGroup[k].audioPathAndName = path + name;

                        }
                    }
                }
            }

            for (int i = 0; i < hotfixJson.showGroup.Length; i++)
            {
                for (int j = 0; j < hotfixJson.showGroup[i].show.Length; j++)
                {
                    if (hotfixJson.showGroup[i].show[j].videoGroup != null)
                        for (int k = 0; k < hotfixJson.showGroup[i].show[j].videoGroup.Length; k++)
                    {
                        var tempCount = hotfixJson.showGroup[i].show[j].videoGroup.Length;
                        if (!string.IsNullOrEmpty(hotfixJson.showGroup[i].show[j].videoGroup[k].videoPath))
                        {
                            var temp = Regex.Split(hotfixJson.showGroup[i].show[j].videoGroup[k].videoPath, preName, RegexOptions.IgnoreCase);
                            var tempName = temp[temp.Length - 1].Split('/');
                            var name = tempName[tempName.Length - 1];
                            var path = temp[temp.Length - 1].Substring(0, temp[temp.Length - 1].Length - name.Length);
                            hotfixJson.showGroup[i].show[j].videoGroup[k].videoPathAndName = path + name;
                        }
                    }
                }
            }
            #endregion
        }

        DownloadAllPrepositionAssets.GetInstance.hasDownJsonAssets = true;
    }

    void MemFree()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
}