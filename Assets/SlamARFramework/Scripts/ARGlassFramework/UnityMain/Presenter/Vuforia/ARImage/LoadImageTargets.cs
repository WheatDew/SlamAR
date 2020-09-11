using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

namespace Vuforia
{
    /// <summary>动态加载高通识别图目标</summary>
    public class LoadImageTargets : MonoSingleton<LoadImageTargets>
    {
        #region 数据申明
        DataSet dataset = null;
        ObjectTracker tracker;
        bool boolLoaded = false;
        string xmlPath;
        public int imageTargetCount;
        bool hasTargetCount;
        List<string> listImageTargetsName = new List<string>();
        Transform parent;
        #endregion

        #region Unity函数

        new void Awake()
        {
            base.Awake();
            parent = new GameObject("ImageTargets").transform;
            //parent.parent = transform;
            listImageTargetsName.Clear();
            imageTargetCount = 0;
            xmlPath = PathSet.GetVuforiaReadXmlPath();
            GetXML(xmlPath);
        }

        private void Start()
        {
            StartCoroutine(LoadTarget());
        }

        #endregion

        #region 私有函数
        private IEnumerator LoadTarget()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(DownloadAllPrepositionAssets.GetInstance.HasDownVuforiaData);
            yield return new WaitUntil(DownloadAllPrepositionAssets.GetInstance.HasDownJsonAssets);
            if (VuforiaRuntimeUtilities.IsVuforiaEnabled() && !boolLoaded)
            {
                if (dataset == null)
                {
                    tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
                    dataset = tracker.CreateDataSet();
                }
                boolLoaded = dataset.Load(xmlPath, VuforiaUnity.StorageType.STORAGE_ABSOLUTE);
                if (!boolLoaded)
                { }
                else
                {
                    tracker.Stop();
                    tracker.ActivateDataSet(dataset);
                    tracker.Start();
                    int i = imageTargetCount;
                    foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
                    {
                        if (go.name == "New Game Object")
                        {
                            int index = i - 1;
                            if (index >= listImageTargetsName.Count || index < 0)
                            {
                                Debug.LogWarning(GetType() + "/Update() +  index:索引非法！");
                            }
                            else
                            {
                                go.transform.parent = parent;
                                go.transform.localPosition = Vector3.zero;
                                go.transform.localEulerAngles = Vector3.zero;
                                go.name = listImageTargetsName[index];
                                go.AddComponent<TurnOffBehaviour>();
                                go.AddComponent<MyTrackableEventHandler>().id = imageTargetCount - i;
                                go.AddComponent<SetAssetsVuforiaChild>();
                                i--;
                            }
                        }
                    }
                }
            }
        }

        public bool HasImageTarget()
        {
            return hasTargetCount;
        }

        private void GetXML(string path)
        {
            int _ImageTargetCount = 0;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("QCARConfig").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                foreach (XmlElement x1 in xe.ChildNodes)
                {
                    if (x1.Name == "ImageTarget")
                    {
                        _ImageTargetCount++;
                        listImageTargetsName.Add(x1.GetAttribute("name"));
                    }
                }
            }
            Debug.Log("识别图总量:" + _ImageTargetCount);
            imageTargetCount = _ImageTargetCount;
            hasTargetCount = true;
        }
        #endregion
    }
}