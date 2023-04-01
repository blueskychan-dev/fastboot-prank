using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace fastboot
{
    class Program
    {
        public class Config_Fastboot
        {
            public string Device_name { get; set; }
            public string Device_State { get; set; }
        }
        static void Main(string[] args)
        {
            string debug = null;
            
            try
            {
                
                debug = Environment.GetCommandLineArgs()[1];
            }
            catch
            {
                Console.WriteLine("fastboot: usage: no command");
                return;
            }
            if (debug == "devices")
            {
                if (!File.Exists("config.json"))
                {
                    Console.WriteLine("[DEBUG] config.json is not found!\n[DEBUG] THIS WILL SHOW ONLY FIRST TIME!\n[DEBUG] NEW DEVICES NAME BEEN SAVED TO CONFIG.JSON!");

                    // Generate random device name
                    const string chars = "0123456789afg";
                    var random1 = new Random();
                    var devicename = new string(Enumerable.Repeat(chars, 8).Select(s => s[random1.Next(s.Length)]).ToArray());

                    // Create and save first config
                    var firstConfig = new Config_Fastboot { Device_name = devicename, Device_State = "Unlocked" };
                    var configjson = JsonConvert.SerializeObject(firstConfig, Formatting.Indented);
                    File.WriteAllText("config.json", configjson);
                }
                    // Load config and display device name
                    string json = File.ReadAllText("config.json");
                    var config = JsonConvert.DeserializeObject<Config_Fastboot>(json);
                    string deviceName = config.Device_name;
                    Console.WriteLine($"{deviceName}       fastboot");
                }

                else if (debug == "erase")
                {
                    string partition = null;
                    try
                    {
                        partition = Environment.GetCommandLineArgs()[2];
                    }
                    catch
                    {
                        Console.WriteLine("fastboot: usage: expected argument");
                        return; // Exit early to avoid errors
                    }

                    var random = new Random();
                    int sleepRandom = random.Next(100, 500);
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    Console.WriteLine($"Erasing '{partition}'...");
                    System.Threading.Thread.Sleep(sleepRandom);
                    stopwatch.Stop();

                    string elapsedTime = stopwatch.Elapsed.TotalSeconds.ToString("0.000s");
                    Console.WriteLine($"OKAY [ {elapsedTime}]");
                    Console.WriteLine($"Finished. Total time: {elapsedTime}");
                }
                else if (debug == "reboot")
                {
                    Console.WriteLine("rebooting...");
                    var random = new Random();
                    int sleepRandom = random.Next(100, 500);
                    System.Threading.Thread.Sleep(sleepRandom);
                    Console.WriteLine("\n");
                }
                else if (debug == "help")
                {
                    Console.WriteLine("usage: fastboot [OPTION...] COMMAND...\nnothing to do!\nCredit: Randomladyboyguy (Fusemeoww)");
                }
                else if (debug == "-w")
                {
                string[] lists = { "userdata", "cache" };
                Stopwatch stopwatch = Stopwatch.StartNew();
                foreach (string partition in lists)
                {
                    var random = new Random();
                    int sleepRandom = random.Next(500, 2000);
                    Console.WriteLine($"Erasing '{partition}'...");
                    System.Threading.Thread.Sleep(sleepRandom);
                }
                stopwatch.Stop();

                string elapsedTime = stopwatch.Elapsed.TotalSeconds.ToString("0.000s");
                Console.WriteLine($"OKAY [ {elapsedTime}]");
                Console.WriteLine($"Finished. Total time: {elapsedTime}");
            }
            else if (debug == "oem")
                {
                    if (!File.Exists("config.json"))
                    {
                        Console.WriteLine("[DEBUG] config.json is not found!\n[DEBUG] THIS WILL SHOW ONLY FIRST TIME!\n[DEBUG] NEW DEVICES NAME BEEN SAVED TO CONFIG.JSON!");

                        // Generate random device name
                        const string chars = "0123456789afg";
                        var random1 = new Random();
                        var devicename = new string(Enumerable.Repeat(chars, 8).Select(s => s[random1.Next(s.Length)]).ToArray());

                        // Create and save first config
                        var firstConfig = new Config_Fastboot { Device_name = devicename, Device_State = "Unlocked" };
                        var configjson = JsonConvert.SerializeObject(firstConfig, Formatting.Indented);
                        File.WriteAllText("config.json", configjson);
                    }
                    string command = null;
                    try
                    {
                        command = Environment.GetCommandLineArgs()[2];
                    }
                    catch
                    {
                        Console.WriteLine("fastboot: usage: expected argument");
                        return; // Exit early to avoid errors
                    }
                    if (command == "device-info")
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        string json = File.ReadAllText("config.json");
                        var config = JsonConvert.DeserializeObject<Config_Fastboot>(json);
                        string state = config.Device_State;
                        string unlock = null;
                        if (state == "Unlocked")
                        {
                            unlock = "true";
                        }
                        else if (state == "Locked")
                        {
                            unlock = "false";
                        }
                        else
                        {
                            Console.WriteLine("ERROR: fastboot cannot get a value of device unlocked, PANIC!");
                            return;
                        }
                        Console.WriteLine($"...\n<bootloader>   Device tampered: true\n<bootloader>   Device unlocked: {unlock}\n<bootloader>   Device critical unlocked: {unlock}\n<bootloader>   Charger screen enabled: true");
                        stopwatch.Stop();

                        string elapsedTime = stopwatch.Elapsed.TotalSeconds.ToString("0.000s");
                        Console.WriteLine($"OKAY [ {elapsedTime}]");
                        Console.WriteLine($"Finished. Total time: {elapsedTime}");
                    }

                    else if (command == "unlock")
                    {
                        Stopwatch stopwatch1 = Stopwatch.StartNew();
                        stopwatch1.Start();
                        Console.WriteLine("...");
                        var random = new Random();
                    int sleepRandom = random.Next(500, 2000);
                    System.Threading.Thread.Sleep(sleepRandom);
                        Console.WriteLine($"(Bootloader) Start unlock flow");
                        Console.WriteLine("(Bootloader) erasing userdata...");
                        System.Threading.Thread.Sleep(random.Next(500, 2000));
                        Console.WriteLine("(Bootloader) erasing userdata... done");
                        System.Threading.Thread.Sleep(random.Next(500, 2000));
                        Console.WriteLine("(Bootloader) erasing cache...");
                        System.Threading.Thread.Sleep(random.Next(500, 2000));
                        Console.WriteLine("(Bootloader) erasing cache... done");
                        Console.WriteLine("(Bootloader) erasing userdata... done");
                        string jsonString = File.ReadAllText("config.json");

                        // Parse JSON string into a JObject
                        JObject json1 = JObject.Parse(jsonString);

                        // Get the "device_state" property and set its value to "Locked"
                        json1["device_state"] = "Unlocked";

                        // Write the updated JSON back to the file
                        File.WriteAllText("config.json", json1.ToString());
                        Console.WriteLine("(Bootloader) Unlocking bootloader done!");
                    stopwatch1.Stop();

                    string elapsedTime = stopwatch1.Elapsed.TotalSeconds.ToString("0.000s");
                    Console.WriteLine($"OKAY [ {elapsedTime}]");
                    Console.WriteLine($"Finished. Total time: {elapsedTime}");


                }

                    else if (command == "lock")
                    {
                        Stopwatch stopwatch2 = Stopwatch.StartNew();
                        stopwatch2.Start();
                        Console.WriteLine("...");
                        var random2 = new Random();
                        int sleepRandom2 = random2.Next (500, 2000);
                        System.Threading.Thread.Sleep(sleepRandom2);
                        Console.WriteLine($"(Bootloader) Start lock flow");
                        Console.WriteLine("(Bootloader) erasing userdata...");
                        System.Threading.Thread.Sleep(random2.Next(500, 2000));
                        Console.WriteLine("(Bootloader) erasing userdata... done");
                        System.Threading.Thread.Sleep(random2.Next(500, 2000));
                        Console.WriteLine("(Bootloader) erasing cache...");
                        System.Threading.Thread.Sleep(random2.Next(500, 2000));
                        Console.WriteLine("(Bootloader) erasing cache... done");
                        Console.WriteLine("(Bootloader) erasing userdata... done");
                        string jsonString2 = File.ReadAllText("config.json");

                        // Parse JSON string into a JObject
                        JObject json2 = JObject.Parse(jsonString2);

                        // Get the "device_state" property and set its value to "Locked"
                        json2["device_state"] = "Locked";

                        // Write the updated JSON back to the file
                        File.WriteAllText("config.json", json2.ToString());
                        Console.WriteLine("(Bootloader) Locking bootloader done!");
                    stopwatch2.Stop();

                    string elapsedTime = stopwatch2.Elapsed.TotalSeconds.ToString("0.000s");
                    Console.WriteLine($"OKAY [ {elapsedTime}]");
                    Console.WriteLine($"Finished. Total time: {elapsedTime}");


                }
                }
                else
                    Console.WriteLine($"fastboot: usage: unknown command {debug}");

            }
        }
    }

