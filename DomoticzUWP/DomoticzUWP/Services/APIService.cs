using DomoticzUWP.Models;
using DomoticzUWP.Models.JSON;
using DomoticzUWP.Services.SettingsServices;
using RestSharp.Portable;
using RestSharp.Portable.Authenticators;
using RestSharp.Portable.Deserializers;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DomoticzUWP.Services
{
    public sealed class APIService
    {
        private RestClient client { set; get; }
        public String apiURL { set; get; }
        public String username { set; get; }
        public String password { set; get; }
        public Boolean status { set; get; }

        private APIService()
        {
            apiURL = SettingsService.Instance.DomoticzHost;
            username = SettingsService.Instance.DomoticzUsername;
            password = SettingsService.Instance.DomoticzPassword;
            status = true;
            setClient();
        }

        public void setClient()
        {
            if (apiURL != null && apiURL != "")
            {
                client = new RestClient(apiURL);
            }
        }

        // this will be initialized only once
        private static APIService instance = new APIService();

        public static APIService Instance
        {
            get
            {
                if (instance.apiURL != SettingsService.Instance.DomoticzHost)
                {
                    instance.apiURL = SettingsService.Instance.DomoticzHost;
                    instance.setClient();
                }
                
                instance.username = SettingsService.Instance.DomoticzUsername;
                instance.password = SettingsService.Instance.DomoticzPassword;
                return instance;
            }
        }

        public async Task<List<Light>> getLights()
        {
            JSONLights lights = new JSONLights();
            String url = "json.htm?type=command&param=getlightswitches";
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))));

            try
            {
                var respons = await client.Execute(request);
                if (respons != null)
                {
                    JsonDeserializer deserial = new JsonDeserializer();
                    lights = deserial.Deserialize<JSONLights>(respons);
                    System.Diagnostics.Debug.WriteLine(lights);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return lights.result;
        }

        internal static ICommand getInstance()
        {
            throw new NotImplementedException();
        }

        public async Task<Boolean> TestConnection()
        {
            Boolean connection = false;
            if (client == null)
            {
                return connection;
            }
            
            String url = "json.htm?api-call";
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))));
            try
            {
                var respons = await client.Execute(request);
                if (respons != null && respons.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    connection = true;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                connection = false;
            }
            return connection;
        }

        public async Task<Floorplan> getFloorplan()
        {
            JSONFloorplans floorplans = new JSONFloorplans();
            String url = "json.htm?type=floorplans";
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))));

            try
            {
                var respons = await client.Execute(request);
                if (respons != null)
                {
                    JsonDeserializer deserial = new JsonDeserializer();
                    floorplans = deserial.Deserialize<JSONFloorplans>(respons);
                    System.Diagnostics.Debug.WriteLine(floorplans);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            if (floorplans.status != "OK")
            {
                status = false;
            }

            if (floorplans.result.Count != 0)
            {
                return floorplans.result[0];
            }
            return new Floorplan();
        }

        public async Task toggleSwitch(int idx)
        {
            String url = "json.htm?type=command&param=switchlight&idx="+idx+"&switchcmd=Toggle";
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))));

            try
            {
                var respons = await client.Execute(request);
                if (respons != null)
                {
                    JsonDeserializer deserial = new JsonDeserializer();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        public async Task<List<Device>> getDevicesByFloor(Floorplan floor)
        {
            JSONDevices devices = new JSONDevices();
            String url = "json.htm?type=devices&filter=all&used=true&order=Name&floor="+floor.idx;
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))));

            try
            {
                var respons = await client.Execute(request);
                if (respons != null)
                {
                    JsonDeserializer deserial = new JsonDeserializer();
                    devices = deserial.Deserialize<JSONDevices>(respons);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return devices.result;
        }

        public async Task<List<Device>> getDevices()
        {
            return await getDevices("");
        }

        public async Task<List<Device>> getDevices(String filter)
        {
            JSONDevices devices = new JSONDevices();
            String url = "json.htm?type=devices&used=true";
            if (filter != "")
            {
                url += "&filter=" + filter;
            }
            
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))));

            try
            {
                var respons = await client.Execute(request);
                if (respons != null)
                {
                    JsonDeserializer deserial = new JsonDeserializer();
                    devices = deserial.Deserialize<JSONDevices>(respons);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return devices.result;
        }

        public async Task<Device> getDeviceByIdx(int idx)
        {
            JSONDevices devices = new JSONDevices();
            String url = "json.htm?type=devices&rid=" + idx;
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))));

            try
            {
                var respons = await client.Execute(request);
                if (respons != null)
                {
                    JsonDeserializer deserial = new JsonDeserializer();
                    devices = deserial.Deserialize<JSONDevices>(respons);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            if (devices.result.Count == 1)
            {
                return devices.result[0];
            }
            return null;
        }
    }
    class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action _action;

        public CommandHandler(Action action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._action();
        }
    }
}
