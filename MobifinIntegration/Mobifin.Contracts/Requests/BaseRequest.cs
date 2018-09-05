using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests
{
    public class DeviceInfo
    {

        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("platformVersion")]
        public string PlatformVersion { get; set; }
    }

    public class AppInfo
    {

        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }

        [JsonProperty("applicationVersion")]
        public string ApplicationVersion { get; set; }
    }

    public class BasicInfo
    {
        public override string ToString()
        {
            return DeviceInfo.DeviceId + "--"
                + DeviceInfo.Platform + "--"
                + DeviceInfo.PlatformVersion + "--"
                + this.AppInfo.ApplicationId + "--"
                + this.AppInfo.ApplicationVersion + "--"
                + this.MobileNumberInfo.Region + "--"
                + this.MobileNumberInfo.Number;
        }

        [JsonProperty("deviceInfo")]
        public DeviceInfo DeviceInfo { get; set; }

        [JsonProperty("appInfo")]
        public AppInfo AppInfo { get; set; }

        [JsonProperty("mobileNO")]
        public MobileNumber MobileNumberInfo { get; set; }
    }

    public class MobileNumber
    {

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }
    }
    [Serializable]
    public abstract class BaseRequest
    {

        [JsonProperty("basicInfo")]
        public BasicInfo BasicInfo { get; set; }


        public virtual bool ValidateObject()
        {
            if (BasicInfo != null)
            {
                if (BasicInfo.AppInfo != null && BasicInfo.DeviceInfo != null && BasicInfo.MobileNumberInfo != null)
                {
                    if (BasicInfo.AppInfo.ApplicationId != null && BasicInfo.AppInfo.ApplicationVersion != null
                        && BasicInfo.DeviceInfo.Platform != null && BasicInfo.DeviceInfo.PlatformVersion != null && BasicInfo.DeviceInfo.DeviceId != null
                        && BasicInfo.MobileNumberInfo.Number != null && BasicInfo.MobileNumberInfo.Region != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }


}
