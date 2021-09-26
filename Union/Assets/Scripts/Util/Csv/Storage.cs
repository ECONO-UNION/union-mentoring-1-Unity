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

        public Dictionary<int, T> dataDictionary;

        public void ChangeDatas(Dictionary<int, T> datas)
        {
            this.dataDictionary = new Dictionary<int, T>(datas);
        }

        public T GetData(int infoID)
        {
            if (this.dataDictionary == null)
            {
                ReadAndStoreDatas();
            }

            return this.dataDictionary[infoID];
        }

        private void ReadAndStoreDatas()
        {
            Type genericType = typeof(Reader<>);
            Type[] typeArgs = { Type.GetType("Union.Util.Csv." + typeof(T).Name) };
            Type readerType = genericType.MakeGenericType(typeArgs);
            var reader = Activator.CreateInstance(readerType);

            readerType.GetMethod("Read").Invoke(reader, null);
            readerType.GetMethod("StoreDatasInStorage").Invoke(reader, null);
        }
    }
}
