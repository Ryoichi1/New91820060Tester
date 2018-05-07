using System;
using static System.Threading.Thread;
using System.Threading.Tasks;

namespace New91820060Tester
{
    public static class 書き込み
    {
        public static async Task<bool> WriteFw(string path)
        {
            var result = false;
            try
            {
                Target.Close232();
                Sleep(500);
                return result = await FMC16LX.WriteFirmware(path);
            }
            catch
            {
                return false;
            }
            finally
            {
                General.ResetIo();
                Target.InitPort232();
            }
        }



    }
}
