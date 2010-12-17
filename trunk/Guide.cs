//
// Guide.cs
//

using System;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;


namespace Galaxy
{
    public class GuideUtil
    {
        public static IAsyncResult StorageDeviceResult { get; set; }
        public static StorageDevice StorageDevice { get; set; }
    }
}
