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
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int idx { get; set; }
        public Windows.UI.Xaml.Thickness Margin { get { return new Windows.UI.Xaml.Thickness(XOffset, YOffset, 0, 0); } }

        public ICommand Command {
            get
            {
                return new CommandHandler(async () =>
                    await APIService.GetInstance().FloorSwitch(idx)
                );
            }
        }
    }
}
