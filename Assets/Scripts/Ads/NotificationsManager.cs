using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Notifications.Android;

public class NotificationsManager : MonoBehaviour
{
    public static NotificationsManager instance;

    public static float time = 0;
    public static bool isAppPause = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void Start()
    {
        isAppPause = false;
        
        StartCoroutine(RegisterNotify());
    }

    IEnumerator RegisterNotify()
    {
        yield return new WaitForSeconds(15f);
        var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();
        int start_notify = PlayerPrefs.GetInt("start_notify", 0);

        if (notificationIntentData != null)
        {
            start_notify++;
            PlayerPrefs.SetInt("start_notify", start_notify);
            //FirebaseAnalyticManager.instance.AnalyticsStartNotify(start_notify);
        }
        CreatNotifyChannel();
        ShowNotifications();
        
    }

    // Update is called once per frame
    public static void CreatNotifyChannel()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        AndroidNotificationCenter.CancelAllNotifications();
        AndroidNotificationCenter.DeleteNotificationChannel("notify_1");
        var c = new AndroidNotificationChannel()
        {
            Id = "notify_1",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
            LockScreenVisibility = LockScreenVisibility.Public
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    public static void ShowNotifications()
    {
        var notify = new AndroidNotification();
        notify.Title = "Head Soccer";
        notify.Text = "Hey you, do you want to play game today?";
        notify.SmallIcon = "icon_0";
        notify.LargeIcon = "icon_1";
        notify.Color = Color.red;
        for (int i = 1; i < 4; i++)
        {

            notify.FireTime = DateTime.Now.AddHours(i * 30);
            AndroidNotificationCenter.SendNotification(notify, "notify_1");
        }
        for (int i = 4; i < 10; i++)
        {

            notify.FireTime = DateTime.Now.AddHours(i * 150);
            AndroidNotificationCenter.SendNotification(notify, "notify_1");
        }

    }
}
