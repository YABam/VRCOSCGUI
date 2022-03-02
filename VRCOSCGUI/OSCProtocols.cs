using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRCOSCGUI
{
    static class OSCProtocols
    {
        public static bool ConvertToOSCArray(string addr, string data, Type t,out byte[] oscArr)
        {
            oscArr = null;
            try 
            {
                string typeChar = null;
                byte[] value;
                if (t.Equals(typeof(int)))
                {
                    typeChar = "i";
                    value = ReverseArray<byte>(BitConverter.GetBytes(Convert.ToInt32(data)));                    
                }
                else if (t.Equals(typeof(float)))
                {
                    typeChar = "f";
                    value = ReverseArray<byte>(BitConverter.GetBytes(Convert.ToSingle(data)));
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
                        value [3] = 0;
                    }
                }
                else
                {
                    throw new TypeNotSupportException();
                }

                List<byte> msg = new List<byte> ();
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
            catch(Exception e)
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
    }

    class TypeNotSupportException : Exception
    { 
        
    }
}
