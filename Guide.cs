﻿//
// Guide.cs
//

using System;
using System.Threading;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;


namespace Galaxy
{
    public class GuideUtil
    {
        public static CGalaxy Game { get; set; }
        public static IAsyncResult StorageDeviceResult { get; set; }
        public static StorageDevice StorageDevice { get; set; }
        public static Thread StorageDeviceInitializeThread { get; set; }
        public static bool StorageDeviceReady { get; set; }

        public static void Start()
        {
            StorageDeviceReady = false;
            ThreadStart start = new ThreadStart(StartImpl);     
            StorageDeviceInitializeThread = new Thread(start);
            StorageDeviceInitializeThread.Start();
        }

        public static void StartImpl()
        {
            while (!StorageDeviceReady)
            {
                try
                {
                    // wait for user to press start
                    while (Game.Input.CountConnectedControllers() == 0)
                    {
                        Thread.Sleep(0);
                    }

                    while (Guide.IsVisible)
                        Thread.Sleep(0);
                    GuideUtil.StorageDeviceResult = Guide.BeginShowStorageDeviceSelector(null, null);
                    GuideUtil.StorageDeviceResult.AsyncWaitHandle.WaitOne();
                    GuideUtil.StorageDevice = Guide.EndShowStorageDeviceSelector(GuideUtil.StorageDeviceResult);

                    if (GuideUtil.StorageDevice == null)
                    {
                        while (Guide.IsVisible)
                            Thread.Sleep(0);
                        IAsyncResult result = Guide.BeginShowMessageBox("Unable To Save", "Storage device selection was cancelled.\nGame data will not be saved.", new string[] { "Ok" }, 0, MessageBoxIcon.Alert, null, null);
                        result.AsyncWaitHandle.WaitOne();
                        Guide.EndShowMessageBox(result);
                        continue;
                    }
                    else if (GuideUtil.StorageDevice.IsConnected == false)
                    {
                        while (Guide.IsVisible)
                            Thread.Sleep(0);
                        IAsyncResult result = Guide.BeginShowMessageBox("Unable To Save", "Storage device is not connected.\nGame data will not be saved.", new string[] { "Ok" }, 0, MessageBoxIcon.Warning, null, null);
                        result.AsyncWaitHandle.WaitOne();
                        Guide.EndShowMessageBox(result);
                        continue;
                    }

                    while (SignedInGamer.SignedInGamers.Count == 0)
                    {
                        Thread.Sleep(0);    
                    }

                    //
                    // NOTE: this array may not be in order as expected
                    //
                    SignedInGamer gamer = null;
                    for (int i = 0; i < 4; ++i)
                    {
                        gamer = SignedInGamer.SignedInGamers[i];
                        if (gamer != null)
                            break;
                    }

                    string tag = "Default";
                    if (gamer != null)
                        tag = (string)gamer.Tag;

                    CSaveData.Load();
                    CSaveData.AddNewProfile(tag);
                    CSaveData.SetCurrentProfile(tag);

                    SProfileOptionsData options = CSaveData.GetCurrentProfile().Options;
                    CAudio.SetSFXVolume(options.SFXVolume);
                    CAudio.SetMusicVolume(options.MusicVolume);
                    Game.SetUserScaleValue(options.UserScale);

                    StorageDeviceReady = true;
                }
                catch (Exception e)
                {
                    while (Guide.IsVisible)
                        Thread.Sleep(0);
                    IAsyncResult result = Guide.BeginShowMessageBox("Error", e.ToString(), new string[] { "Ok" }, 0, MessageBoxIcon.Error, null, null);
                    result.AsyncWaitHandle.WaitOne();
                    Guide.EndShowMessageBox(result);
                }
            }
        }
    }
}
