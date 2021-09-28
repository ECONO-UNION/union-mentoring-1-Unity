using System;
using System.Collections.Generic;

namespace Union.Util.Csv
{
    public class Storage<T> where T : IData
    {
        private static Storage<T> _instance;
        public static Storage<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Storage<T>();
                }
                return _instance;
            }
        }

        private Dictionary<int, T> datas;

        public void ChangeDatas(Dictionary<int, T> datas)
        {
            this.datas = datas;
        }

        public T GetData(int infoID)
        {
            if (this.datas == null)
            {
                ReadAndStoreDatas();
            }

            return this.datas[infoID];
        }

        private void ReadAndStoreDatas()
        {
            Type genericType = typeof(Reader<>);
            Type[] typeArgs = { Type.GetType("Union.Util.Csv." + typeof(T).Name) };
            Type readerType = genericType.MakeGenericType(typeArgs);
            Reader<T> reader = (Reader<T>)Activator.CreateInstance(readerType);

            reader.Read();
            reader.StoreDatasInStorage();
        }
    }
}
