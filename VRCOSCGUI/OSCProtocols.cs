using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRCOSCGUI
{
    static class OSCProtocols
    {
        public static bool ConvertToOSCArray(string addr, string data, Type t, out byte[] oscArr)
        {
            oscArr = null;
            try
            {
                string typeChar = null;
                byte[] value;
                if (t.Equals(typeof(int)))
                {
                    typeChar = "i";
                    //value = ReverseArray<byte>(BitConverter.GetBytes(Convert.ToInt32(data)));
                    value = BitConverter.GetBytes(Convert.ToInt32(data)).Reverse().ToArray();
                }
                else if (t.Equals(typeof(float)))
                {
                    typeChar = "f";
                    //value = ReverseArray<byte>(BitConverter.GetBytes(Convert.ToSingle(data)));
                    value = BitConverter.GetBytes(Convert.ToSingle(data)).Reverse().ToArray();
                }
                else if (t.Equals(typeof(bool)))
                {
                    if (Convert.ToBoolean(data))
                    {
                        typeChar = "T";
                        value = new byte[4];
                        value[3] = 1;
                    }
                    else
                    {
                        typeChar = "F";
                        value = new byte[4];
                        value[3] = 0;
                    }
                }
                else
                {
                    throw new TypeNotSupportException();
                }

                List<byte> msg = new List<byte>();
                msg.AddRange(System.Text.Encoding.UTF8.GetBytes(addr));
                if (msg.Count % 4 == 0)
                {
                    msg.AddRange(new byte[4]);
                }
                else
                {
                    for (int i = 0; i < msg.Count % 4; i++)
                    {
                        msg.Add(0);
                    }
                }
                msg.Add(44);
                msg.AddRange(Encoding.UTF8.GetBytes(typeChar));
                msg.Add(0);
                msg.Add(0);
                msg.AddRange(value);

                oscArr = msg.ToArray();

                return true;
            }
            catch (Exception e)
            {
                if (e.GetType().Name == "TypeNotSupportException")
                {
                    //Add Console
                }
                else
                {

                }
                return false;
            }
        }

        /*
        [Obsolete]
        static T[] ReverseArray<T>(T[] inArray)
        {
            for (int i = 0; i < inArray.Length / 2; i++)
            {
                T temp = inArray[i];
                inArray[i] = inArray[inArray.Length - i - 1];
                inArray[inArray.Length - i - 1] = temp;
            }
            return inArray;
        }
        */

        public static bool OSCConvertToString(byte[] inOSC, out string addr, out string data, out Type t)
        {
            bool result = false;
            addr = null;
            data = null;
            t = null;
            try
            {
                for (int i = 4; i < inOSC.Length; i += 4)
                {
                    if (inOSC[i] == ',')
                    {
                        //before i is address
                        for (int j = i - 1; j > 0; j--)
                        {
                            if (inOSC[j] != 0)
                            {
                                //0 to j is address
                                byte[] strAddr = inOSC.Take(j + 1).ToArray();
                                //if it is string
                                addr = Encoding.UTF8.GetString(strAddr);
                                break;
                            }
                        }

                        //after i is type char
                        switch (inOSC[i + 1])
                        {
                            //i - int
                            case 105:
                                t = typeof(int);
                                data = BitConverter.ToInt32(inOSC.Skip(i + 3).ToArray().Reverse().ToArray(), 0).ToString();
                                result = true;
                                break;

                            //f - float
                            case 102:
                                t = typeof(float);
                                data = BitConverter.ToSingle(inOSC.Skip(i + 3).ToArray().Reverse().ToArray(), 0).ToString();
                                result = true;
                                break;

                            //T - true for bool
                            case 84:
                                t = typeof(bool);
                                data = "true";
                                result = true;
                                break;

                            //F - false for bool
                            case 70:
                                t = typeof(bool);
                                data = "false";
                                result = true;
                                break;

                            default: result = false; break;
                        }
                        break;
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }

    class TypeNotSupportException : Exception
    {

    }
}
