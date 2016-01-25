using DomoticzUWP.Services;
using System;
using System.Windows.Input;

namespace DomoticzUWP.Models
{
    public class Device
    {
        public float AddjMulti { get; set; }
        public float AddjMulti2 { get; set; }
        public float AddjValue { get; set; }
        public float AddjValue2 { get; set; }
        public int BatteryLevel { get; set; }
        public float Counter { get; set; }
        public float CounterDeliv { get; set; }
        public String CounterDelivToday { get; set; }
        public String CounterToday { get; set; }
        public int CustomImage { get; set; }
        public String Data { get; set; }
        public String Description { get; set; }
        public int Favorite { get; set; }
        public int HardwareID { get; set; }
        public String HardwareName { get; set; }
        public String HardwareType { get; set; }
        public int HardwareTypeVal { get; set; }
        public Boolean HaveTimeout { get; set; }
        public String ID { get; set; }
        public String LastUpdate { get; set; }
        public String Name { get; set; }
        public String Image { get; set; }
        public String TypeImg { get; set; }
        public String Status { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int idx { get; set; }
        public String LastSeen { get { return "Last seen: " + LastUpdate; } }
        public String Type { get { return "Type: " + TypeImg; } }
        public Windows.UI.Xaml.Thickness Margin { get { return new Windows.UI.Xaml.Thickness(XOffset, YOffset, 0, 0); } }

        public ICommand Command {
            get
            {
                return new CommandHandler(async () =>
                    await FloorSwitch()
                );
            }
        }

        private async System.Threading.Tasks.Task FloorSwitch()
        {
            await APIService.Instance.FloorSwitch(idx);
            Device d = await APIService.Instance.getDeviceByIdx(idx);
            if (d != null)
            {
                Status = d.Status;
                Image = d.Image;
                TypeImg = d.TypeImg;
                LastUpdate = d.LastUpdate;
                Data = d.Data;
            }
        }

        public Windows.UI.Xaml.Media.Imaging.BitmapImage IconPath
        {
            get
            {
                
                if (Status == "Set Level")
                {
                    Status = "Off";
                }
                String imagePNG = Image +"48_"+ Status +".png";
                switch (TypeImg)
                {
                    case "counter":
                        imagePNG = "counter.png";
                        break;

                }
                return new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(APIService.Instance.apiURL + "/images/"+ imagePNG));
            }
        }
    }
}
