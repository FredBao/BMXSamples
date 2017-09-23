namespace Random
{
    using System;

    using ServiceReference1;

    public class Program
    {

        static void Main(string[] args)
        {
            try
            {
                //CutterServiceClient client = new CutterServiceClient();

                //var cutterLifeListOfLeft = client.GetAllToolLifeInfo_T(
                //    new ConnectedInfo()
                //    {
                //        IP = "10.10.100.39",
                //        Port = 8193,
                //        TimeOut = 3000,
                //        MachineSystemType = 2,
                //        CutterCompensationSide = 1,
                //        FanucCutterStartIndex = 1,
                //        FanucCutterCompensationArrayLength = 50,
                //        FanucCutterCompensationAddress = 5013
                //    });

                //foreach (var item in cutterLifeListOfLeft)
                //{
                //    Console.WriteLine($"nToolNum:{item.nToolNum} nLife:{item.nLife} nUsed:{item.nUsed}");
                //}

                Type type = Type.GetTypeFromProgID("SAPI.SpVoice");

                dynamic spVoice = Activator.CreateInstance(type);

                spVoice.Speak("吃饭了,韩潇!");
                spVoice.Speak("韩潇!机床OP01有报警,赶紧飞过去看看!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }
    }
}